# tiny-language

## Tiny lexical rules (regex)

These regular expressions are extracted from the scanner implementation and can be used as the token spec for DFA construction.

### 1) Ignored patterns

- Whitespace: `\s+`
- Block comment: `/\*[\s\S]*?\*/`

### 2) Reserved words

- `\b(?:if|repeat|read|write|then|else|elseif|end|return|int|float|string)\b`

Note:
- `endl` is treated as an identifier in the current scanner.

### 3) Identifier

- `[A-Za-z][A-Za-z0-9]*`

### 4) Number

- As currently implemented: `[0-9][0-9.]*`

Note:
- This accepts multiple dots (example: `12.3.4`) because the scanner allows any sequence of digits and `.` after the first digit.

### 5) String literal

- `"([^"]*)"`

Note:
- Escape sequences inside strings are not handled in the current implementation.

### 6) Operators and symbols

- Two-character operators: `:=|&&|\|\|`
- One-character operators/symbols: `[+\-*/<>=(),;{}]`

### 7) Suggested matching priority for DFA

1. Whitespace/comment (skip)
2. Two-character operators
3. Identifier/keyword (then classify keyword by lookup)
4. Number
5. String literal
6. One-character operators/symbols
7. Unknown token (error)

### 8) Token enum mismatch to note

- `NotEqualOp` exists in token enum, but `!=` is not currently recognized by the scanner operators table.