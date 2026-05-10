using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Tiny_Compiler;

namespace Tiny
{
    public partial class Form1 : Form
    {
        private readonly Scanner scanner = new Scanner();
        private readonly Parser parser = new Parser();  // ADDED: Parser instance
        private readonly Dictionary<TabPage, TextBox> tabEditors = new Dictionary<TabPage, TextBox>();
        private int nextTabNumber = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Clear();
            dataGridView1.Rows.Clear();
            statusLabel.Text = "Ready to scan the input text.";
            AddNewTab();
        }

        private void buttonRunTab_Click(object sender, EventArgs e)
        {
            CompileActiveTab();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var activeEditor = GetActiveEditor();
            if (activeEditor != null)
            {
                activeEditor.Clear();
            }

            textBox2.Clear();
            dataGridView1.Rows.Clear();
            Errors.Error_List.Clear();
            statusLabel.Text = $"Cleared {GetActiveTabTitle()}.";
        }

        private void buttonAddTab_Click(object sender, EventArgs e)
        {
            AddNewTab();
        }

        private void tabControlSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowActiveEditor();
        }

        private void tabControlSources_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= tabControlSources.TabPages.Count)
            {
                return;
            }

            var tabPage = tabControlSources.TabPages[e.Index];
            var tabBounds = tabControlSources.GetTabRect(e.Index);
            var background = e.Index == tabControlSources.SelectedIndex ? Color.White : Color.FromArgb(232, 237, 243);
            var border = Color.FromArgb(208, 216, 224);

            using (var backgroundBrush = new SolidBrush(background))
            using (var borderPen = new Pen(border))
            using (var closeFont = new Font("Segoe UI", 8F, FontStyle.Bold))
            {
                e.Graphics.FillRectangle(backgroundBrush, tabBounds);
                e.Graphics.DrawRectangle(borderPen, tabBounds);

                var textRect = new Rectangle(tabBounds.X + 10, tabBounds.Y + 7, tabBounds.Width - 24, tabBounds.Height - 10);
                TextRenderer.DrawText(
                    e.Graphics,
                    tabPage.Text,
                    tabPage.Font ?? Font,
                    textRect,
                    Color.FromArgb(33, 37, 41),
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

                var closeRect = GetCloseButtonBounds(tabBounds);
                TextRenderer.DrawText(
                    e.Graphics,
                    "x",
                    closeFont,
                    closeRect,
                    Color.FromArgb(120, 128, 138),
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private void tabControlSources_MouseDown(object sender, MouseEventArgs e)
        {
            for (var index = 0; index < tabControlSources.TabCount; index++)
            {
                var tabBounds = tabControlSources.GetTabRect(index);
                var closeBounds = GetCloseButtonBounds(tabBounds);

                if (closeBounds.Contains(e.Location))
                {
                    CloseTab(tabControlSources.TabPages[index]);
                    return;
                }
            }
        }

        private void CompileActiveTab()
        {
            var activeEditor = GetActiveEditor();
            if (activeEditor == null)
            {
                statusLabel.Text = "No source tab is available.";
                return;
            }

            CompileSource(activeEditor.Text, GetActiveTabTitle());
        }

        // UPDATED: Now includes parsing phase
        private void CompileSource(string sourceCode, string tabName)
        {
            dataGridView1.Rows.Clear();
            textBox2.Clear();
            Errors.Error_List.Clear();
            scanner.Tokens.Clear();

            // PHASE 1: SCANNING (Lexical Analysis)
            scanner.StartScanning(sourceCode);

            // Display tokens
            foreach (var token in scanner.Tokens)
            {
                dataGridView1.Rows.Add(token.lex, token.token_type.ToString());
            }

            // Check for scanning errors
            if (Errors.Error_List.Count > 0)
            {
                foreach (var error in Errors.Error_List)
                {
                    textBox2.AppendText(error + "\r\n");
                }
                statusLabel.Text = $"{tabName}: {scanner.Tokens.Count} token(s), {Errors.Error_List.Count} error(s).";
                return; // Don't proceed to parsing if scanning failed
            }

            // PHASE 2: PARSING (Syntax Analysis)
            try
            {
                Node parseTree = parser.StartParsing(scanner.Tokens);

                // Check for parsing errors
                if (Errors.Error_List.Count > 0)
                {
                    textBox2.Text = "Parsing Errors:\r\n";
                    foreach (var error in Errors.Error_List)
                    {
                        textBox2.AppendText(error + "\r\n");
                    }
                    statusLabel.Text = $"{tabName}: Parsing failed with {Errors.Error_List.Count} error(s).";
                }
                else
                {
                    textBox2.Text = "Compilation Successful!\r\n";
                    textBox2.AppendText($"Tokens: {scanner.Tokens.Count}\r\n");
                    textBox2.AppendText("No errors found.\r\n");
                    textBox2.AppendText("\r\nParse tree generated successfully.");
                    statusLabel.Text = $"{tabName}: Compilation successful!";

                    // Display parse tree only after a successful parse.
                    ShowParseTree(parseTree);
                }
            }
            catch (Exception ex)
            {
                textBox2.AppendText($"\r\nParser Error: {ex.Message}");
                statusLabel.Text = $"{tabName}: Parser crashed.";
            }
        }

        // ADDED: Show parse tree in a separate form
        private void ShowParseTree(Node parseTree)
        {
            if (parseTree == null)
                return;

            // Create a new form to display the parse tree
            Form treeForm = new Form
            {
                Text = "Parse Tree",
                Size = new Size(600, 700),
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 9F)
            };

            // Create TreeView control
            TreeView treeView = new TreeView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9.5F),
                BackColor = Color.FromArgb(248, 249, 251),
                BorderStyle = BorderStyle.None,
                Indent = 20
            };

            // Create panel for TreeView
            Panel treePanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                BackColor = Color.White
            };

            // Create header label
            Label headerLabel = new Label
            {
                Text = "Parse Tree Structure",
                Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoSize = true,
                Location = new Point(16, 16),
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 16)
            };

            // Build the tree
            TreeNode treeRoot = Parser.PrintParseTree(parseTree);
            if (treeRoot != null)
            {
                treeView.Nodes.Add(treeRoot);
                treeView.ExpandAll();
            }

            // Add controls to form
            treePanel.Controls.Add(treeView);
            treeForm.Controls.Add(treePanel);
            treeForm.Controls.Add(headerLabel);

            // Show the form
            treeForm.ShowDialog(this);
        }

        private void AddNewTab()
        {
            var tabPage = new TabPage($"Source {nextTabNumber++}");

            var editor = new TextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 251),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                AcceptsTab = true,
                WordWrap = false
            };

            tabEditors[tabPage] = editor;
            tabControlSources.TabPages.Add(tabPage);
            tabControlSources.SelectedTab = tabPage;
            ShowActiveEditor();
            statusLabel.Text = $"Opened {tabPage.Text}.";
            editor.Focus();
        }

        private void ShowActiveEditor()
        {
            panelEditorHost.Controls.Clear();
            var activeEditor = GetActiveEditor();
            if (activeEditor == null)
            {
                return;
            }

            panelEditorHost.Controls.Add(activeEditor);
            activeEditor.Focus();
        }

        private void CloseTab(TabPage tabPage)
        {
            if (tabControlSources.TabPages.Count <= 1)
            {
                statusLabel.Text = "At least one source tab must remain open.";
                return;
            }

            if (tabEditors.TryGetValue(tabPage, out var editor))
            {
                tabEditors.Remove(tabPage);
                editor.Dispose();
            }

            var wasSelected = tabControlSources.SelectedTab == tabPage;
            tabControlSources.TabPages.Remove(tabPage);
            tabPage.Dispose();

            if (wasSelected && tabControlSources.TabCount > 0)
            {
                tabControlSources.SelectedIndex = 0;
            }

            ShowActiveEditor();
            statusLabel.Text = "Tab closed.";
        }

        private TextBox GetActiveEditor()
        {
            var activeTab = tabControlSources.SelectedTab;
            if (activeTab == null)
            {
                return null;
            }

            return tabEditors.TryGetValue(activeTab, out var editor) ? editor : null;
        }

        private string GetActiveTabTitle()
        {
            return tabControlSources.SelectedTab?.Text ?? "Active source";
        }

        private Rectangle GetCloseButtonBounds(Rectangle tabBounds)
        {
            return new Rectangle(tabBounds.Right - 18, tabBounds.Top + 9, 12, 12);
        }
    }
}