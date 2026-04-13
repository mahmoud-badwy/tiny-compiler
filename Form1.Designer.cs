namespace Tiny
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.panelEditorHost = new System.Windows.Forms.Panel();
            this.panelTabBar = new System.Windows.Forms.Panel();
            this.tabControlSources = new System.Windows.Forms.TabControl();
            this.buttonAddTab = new System.Windows.Forms.Button();
            this.buttonRunTab = new System.Windows.Forms.Button();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.tableLayoutPanelOutput = new System.Windows.Forms.TableLayoutPanel();
            this.panelTokens = new System.Windows.Forms.Panel();
            this.labelTokens = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lexeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelErrors = new System.Windows.Forms.Panel();
            this.labelErrors = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.panelActions = new System.Windows.Forms.Panel();
            this.statusLabel = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.panelTabBar.SuspendLayout();
            this.panelOutput.SuspendLayout();
            this.tableLayoutPanelOutput.SuspendLayout();
            this.panelTokens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelErrors.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(24, 33, 48);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 18, 24, 18);
            this.panelHeader.Size = new System.Drawing.Size(1360, 84);
            this.panelHeader.TabIndex = 0;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(185, 196, 214);
            this.labelSubtitle.Location = new System.Drawing.Point(28, 52);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(350, 23);
            this.labelSubtitle.TabIndex = 1;
            this.labelSubtitle.Text = "Scan code, inspect tokens, and review errors.";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(24, 14);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(171, 41);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Tiny Compiler";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanelMain.Controls.Add(this.panelEditor, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.panelOutput, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 84);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(16);
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1360, 572);
            this.tableLayoutPanelMain.TabIndex = 1;
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.Color.White;
            this.panelEditor.Controls.Add(this.panelEditorHost);
            this.panelEditor.Controls.Add(this.panelTabBar);
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(19, 19);
            this.panelEditor.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Padding = new System.Windows.Forms.Padding(16);
            this.panelEditor.Size = new System.Drawing.Size(645, 534);
            this.panelEditor.TabIndex = 0;
            // 
            // panelEditorHost
            // 
            this.panelEditorHost.BackColor = System.Drawing.Color.White;
            this.panelEditorHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditorHost.Location = new System.Drawing.Point(16, 56);
            this.panelEditorHost.Name = "panelEditorHost";
            this.panelEditorHost.Size = new System.Drawing.Size(613, 462);
            this.panelEditorHost.TabIndex = 1;
            // 
            // panelTabBar
            // 
            this.panelTabBar.BackColor = System.Drawing.Color.Transparent;
            this.panelTabBar.Controls.Add(this.tabControlSources);
            this.panelTabBar.Controls.Add(this.buttonAddTab);
            this.panelTabBar.Controls.Add(this.buttonRunTab);
            this.panelTabBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTabBar.Location = new System.Drawing.Point(16, 16);
            this.panelTabBar.Name = "panelTabBar";
            this.panelTabBar.Size = new System.Drawing.Size(613, 40);
            this.panelTabBar.TabIndex = 0;
            // 
            // tabControlSources
            // 
            this.tabControlSources.Alignment = System.Windows.Forms.TabAlignment.Top;
            this.tabControlSources.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlSources.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlSources.ItemSize = new System.Drawing.Size(150, 30);
            this.tabControlSources.Location = new System.Drawing.Point(36, 2);
            this.tabControlSources.Name = "tabControlSources";
            this.tabControlSources.Padding = new System.Drawing.Point(14, 5);
            this.tabControlSources.SelectedIndex = 0;
            this.tabControlSources.Size = new System.Drawing.Size(541, 36);
            this.tabControlSources.TabIndex = 1;
            this.tabControlSources.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlSources_DrawItem);
            this.tabControlSources.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControlSources_MouseDown);
            this.tabControlSources.SelectedIndexChanged += new System.EventHandler(this.tabControlSources_SelectedIndexChanged);
            // 
            // buttonAddTab
            // 
            this.buttonAddTab.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            this.buttonAddTab.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(210, 220, 230);
            this.buttonAddTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTab.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddTab.ForeColor = System.Drawing.Color.FromArgb(34, 120, 205);
            this.buttonAddTab.Location = new System.Drawing.Point(0, 4);
            this.buttonAddTab.Name = "buttonAddTab";
            this.buttonAddTab.Size = new System.Drawing.Size(32, 30);
            this.buttonAddTab.TabIndex = 0;
            this.buttonAddTab.Text = "+";
            this.buttonAddTab.UseVisualStyleBackColor = false;
            this.buttonAddTab.Click += new System.EventHandler(this.buttonAddTab_Click);
            // 
            // buttonRunTab
            // 
            this.buttonRunTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRunTab.BackColor = System.Drawing.Color.FromArgb(32, 168, 76);
            this.buttonRunTab.FlatAppearance.BorderSize = 0;
            this.buttonRunTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRunTab.Font = new System.Drawing.Font("Segoe UI Symbol", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRunTab.ForeColor = System.Drawing.Color.White;
            this.buttonRunTab.Location = new System.Drawing.Point(581, 4);
            this.buttonRunTab.Name = "buttonRunTab";
            this.buttonRunTab.Size = new System.Drawing.Size(32, 30);
            this.buttonRunTab.TabIndex = 2;
            this.buttonRunTab.Text = "▶";
            this.buttonRunTab.UseVisualStyleBackColor = false;
            this.buttonRunTab.Click += new System.EventHandler(this.buttonRunTab_Click);
            // 
            // panelOutput
            // 
            this.panelOutput.BackColor = System.Drawing.Color.Transparent;
            this.panelOutput.Controls.Add(this.tableLayoutPanelOutput);
            this.panelOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOutput.Location = new System.Drawing.Point(676, 19);
            this.panelOutput.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(665, 534);
            this.panelOutput.TabIndex = 1;
            // 
            // tableLayoutPanelOutput
            // 
            this.tableLayoutPanelOutput.ColumnCount = 1;
            this.tableLayoutPanelOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelOutput.Controls.Add(this.panelTokens, 0, 0);
            this.tableLayoutPanelOutput.Controls.Add(this.panelErrors, 0, 1);
            this.tableLayoutPanelOutput.Controls.Add(this.panelActions, 0, 2);
            this.tableLayoutPanelOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOutput.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelOutput.Name = "tableLayoutPanelOutput";
            this.tableLayoutPanelOutput.RowCount = 3;
            this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanelOutput.Size = new System.Drawing.Size(665, 534);
            this.tableLayoutPanelOutput.TabIndex = 0;
            // 
            // panelTokens
            // 
            this.panelTokens.BackColor = System.Drawing.Color.White;
            this.panelTokens.Controls.Add(this.dataGridView1);
            this.panelTokens.Controls.Add(this.labelTokens);
            this.panelTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTokens.Location = new System.Drawing.Point(3, 3);
            this.panelTokens.Name = "panelTokens";
            this.panelTokens.Padding = new System.Windows.Forms.Padding(16);
            this.panelTokens.Size = new System.Drawing.Size(659, 303);
            this.panelTokens.TabIndex = 0;
            // 
            // labelTokens
            // 
            this.labelTokens.AutoSize = true;
            this.labelTokens.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTokens.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.labelTokens.Location = new System.Drawing.Point(16, 16);
            this.labelTokens.Name = "labelTokens";
            this.labelTokens.Size = new System.Drawing.Size(65, 23);
            this.labelTokens.TabIndex = 0;
            this.labelTokens.Text = "Tokens";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 251);
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(34, 120, 205);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(34, 120, 205);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 36;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lexeme,
            this.Column2});
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(225, 236, 250);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.dataGridView1.Location = new System.Drawing.Point(19, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(621, 236);
            this.dataGridView1.TabIndex = 1;
            // 
            // lexeme
            // 
            this.lexeme.HeaderText = "Lexeme";
            this.lexeme.Name = "lexeme";
            this.lexeme.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Token";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // panelErrors
            // 
            this.panelErrors.BackColor = System.Drawing.Color.White;
            this.panelErrors.Controls.Add(this.textBox2);
            this.panelErrors.Controls.Add(this.labelErrors);
            this.panelErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelErrors.Location = new System.Drawing.Point(3, 312);
            this.panelErrors.Name = "panelErrors";
            this.panelErrors.Padding = new System.Windows.Forms.Padding(16);
            this.panelErrors.Size = new System.Drawing.Size(659, 143);
            this.panelErrors.TabIndex = 1;
            // 
            // labelErrors
            // 
            this.labelErrors.AutoSize = true;
            this.labelErrors.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrors.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            this.labelErrors.Location = new System.Drawing.Point(16, 16);
            this.labelErrors.Name = "labelErrors";
            this.labelErrors.Size = new System.Drawing.Size(60, 23);
            this.labelErrors.TabIndex = 0;
            this.labelErrors.Text = "Errors";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(255, 247, 247);
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(164, 42, 42);
            this.textBox2.Location = new System.Drawing.Point(19, 50);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(621, 73);
            this.textBox2.TabIndex = 1;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Transparent;
            this.panelActions.Controls.Add(this.button2);
            this.panelActions.Controls.Add(this.statusLabel);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelActions.Location = new System.Drawing.Point(3, 461);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(16, 8, 16, 16);
            this.panelActions.Size = new System.Drawing.Size(659, 70);
            this.panelActions.TabIndex = 2;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(90, 98, 108);
            this.statusLabel.Location = new System.Drawing.Point(19, 23);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(196, 20);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Ready to scan the input text.";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(101, 112, 124);
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(493, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 44);
            this.button2.TabIndex = 1;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(240, 243, 247);
            this.ClientSize = new System.Drawing.Size(1360, 656);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(1180, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiny Compiler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelEditor.ResumeLayout(false);
            this.panelTabBar.ResumeLayout(false);
            this.panelOutput.ResumeLayout(false);
            this.tableLayoutPanelOutput.ResumeLayout(false);
            this.panelTokens.ResumeLayout(false);
            this.panelTokens.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelErrors.ResumeLayout(false);
            this.panelErrors.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.panelActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Panel panelEditorHost;
        private System.Windows.Forms.Panel panelTabBar;
        private System.Windows.Forms.TabControl tabControlSources;
        private System.Windows.Forms.Button buttonAddTab;
        private System.Windows.Forms.Button buttonRunTab;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOutput;
        private System.Windows.Forms.Panel panelTokens;
        private System.Windows.Forms.Label labelTokens;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn lexeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Panel panelErrors;
        private System.Windows.Forms.Label labelErrors;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button button2;
    }
}
