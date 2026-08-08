using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RenPy.Python
{
    public enum TokType
    {
        EndOfFile,
        Newline,
        Indent,
        Dedent,
        Name,
        Keyword,
        Number,
        String,
        FString,
        Op,
    }

    public struct Token
    {
        public TokType Type;
        public string Text;
        /// <summary>Literal value for Number tokens and decoded text for String tokens.</summary>
        public object Value;
        public int Line;
        public int Column;

        public override string ToString()
        {
            return Type + "(" + Text + ")";
        }
    }

    /// <summary>
    /// Tokenizes the Python dialect embedded in Ren'Py scripts, including
    /// indentation tracking, implicit line joining inside brackets, and the string
    /// prefixes (r, b, u, f) that appear in real game code.
    /// </summary>
    public class PyLexer
    {
        static readonly HashSet<string> Keywords = new HashSet<string>
        {
            "False", "None", "True", "and", "as", "assert", "async", "await", "break",
            "class", "continue", "def", "del", "elif", "else", "except", "finally",
            "for", "from", "global", "if", "import", "in", "is", "lambda", "nonlocal",
            "not", "or", "pass", "raise", "return", "try", "while", "with", "yield",
            // Python 2 spellings still found in older Ren'Py games.
            "print", "exec",
        };

        // Longest first so that greedy matching picks the right operator.
        static readonly string[] Operators =
        {
            "**=", "//=", ">>=", "<<=", "...", "!==",
            "==", "!=", "<=", ">=", "->", ":=", "**", "//", "<<", ">>",
            "+=", "-=", "*=", "/=", "%=", "&=", "|=", "^=", "@=",
            "+", "-", "*", "/", "%", "@", "&", "|", "^", "~", "<", ">",
            "(", ")", "[", "]", "{", "}", ",", ":", ".", ";", "=",
        };

        readonly string src;
        readonly string filename;
        int pos;
        int line = 1;
        int lineStart;

        readonly List<int> indents = new List<int> { 0 };
        readonly List<Token> tokens = new List<Token>();
        int bracketDepth;
        bool atLineStart = true;

        public PyLexer(string source, string filename = "<python>")
        {
            src = source ?? "";
            this.filename = filename;
        }

        public static List<Token> Tokenize(string source, string filename = "<python>")
        {
            return new PyLexer(source, filename).Run();
        }

        public List<Token> Run()
        {
            while (pos < src.Length)
            {
                if (atLineStart && bracketDepth == 0)
                {
                    if (!HandleIndentation()) continue;
                }

                char c = Peek();

                if (c == '\0') break;

                if (c == '#')
                {
                    while (pos < src.Length && src[pos] != '\n') pos++;
                    continue;
                }

                if (c == '\\' && Peek(1) == '\n')
                {
                    pos += 2;
                    NewLine();
                    continue;
                }

                if (c == '\r') { pos++; continue; }

                if (c == '\n')
                {
                    pos++;
                    if (bracketDepth == 0)
                    {
                        EmitNewline();
                        atLineStart = true;
                    }
                    NewLine();
                    continue;
                }

                if (c == ' ' || c == '\t' || c == '\f') { pos++; continue; }

                if (TryReadString()) continue;

                if (char.IsDigit(c) || (c == '.' && char.IsDigit(Peek(1)))) { ReadNumber(); continue; }

                if (IsNameStart(c)) { ReadName(); continue; }

                if (!ReadOperator())
                    throw Error("invalid character '" + c + "'");
            }

            // Close out any open block at end of input.
            if (tokens.Count > 0 && tokens[tokens.Count - 1].Type != TokType.Newline)
                EmitNewline();

            while (indents.Count > 1)
            {
                indents.RemoveAt(indents.Count - 1);
                Emit(TokType.Dedent, "");
            }

            Emit(TokType.EndOfFile, "");
            return tokens;
        }

        // ---------------------------------------------------------------- indentation

        /// <summary>
        /// Measures the indentation of a fresh logical line. Returns false when the
        /// line was blank or a comment and should be skipped without emitting tokens.
        /// </summary>
        bool HandleIndentation()
        {
            int width = 0;
            int scan = pos;

            while (scan < src.Length)
            {
                char c = src[scan];
                if (c == ' ') { width++; scan++; }
                else if (c == '\t') { width += 8 - (width % 8); scan++; }
                else if (c == '\f') { width = 0; scan++; }
                else break;
            }

            // Blank lines and comment-only lines carry no indentation meaning.
            if (scan >= src.Length) { pos = scan; return true; }
            if (src[scan] == '\n' || src[scan] == '\r' || src[scan] == '#')
            {
                pos = scan;
                atLineStart = true;

                if (src[scan] == '#')
                {
                    while (pos < src.Length && src[pos] != '\n') pos++;
                }
                if (pos < src.Length && src[pos] == '\r') pos++;
                if (pos < src.Length && src[pos] == '\n') { pos++; NewLine(); }
                return false;
            }

            pos = scan;
            atLineStart = false;

            int current = indents[indents.Count - 1];

            if (width > current)
            {
                indents.Add(width);
                Emit(TokType.Indent, "");
            }
            else
            {
                while (indents.Count > 1 && width < indents[indents.Count - 1])
                {
                    indents.RemoveAt(indents.Count - 1);
                    Emit(TokType.Dedent, "");
                }
                if (width != indents[indents.Count - 1])
                    throw Error("unindent does not match any outer indentation level");
            }

            return true;
        }

        void EmitNewline()
        {
            // Collapse runs so the parser never sees an empty logical line.
            if (tokens.Count == 0) return;
            var last = tokens[tokens.Count - 1].Type;
            if (last == TokType.Newline || last == TokType.Indent || last == TokType.Dedent) return;
            Emit(TokType.Newline, "");
        }

        // ---------------------------------------------------------------- scanners

        void ReadName()
        {
            int start = pos;
            while (pos < src.Length && IsNameChar(src[pos])) pos++;
            string text = src.Substring(start, pos - start);

            // A string prefix directly followed by a quote is part of the literal.
            if (pos < src.Length && (src[pos] == '"' || src[pos] == '\'') && IsStringPrefix(text))
            {
                pos = start;
                TryReadString();
                return;
            }

            Emit(Keywords.Contains(text) ? TokType.Keyword : TokType.Name, text);
        }

        static bool IsStringPrefix(string s)
        {
            if (s.Length == 0 || s.Length > 3) return false;
            foreach (char c in s.ToLowerInvariant())
                if (c != 'r' && c != 'b' && c != 'u' && c != 'f') return false;
            return true;
        }

        void ReadNumber()
        {
            int start = pos;

            if (Peek() == '0' && (Peek(1) == 'x' || Peek(1) == 'X'))
            {
                pos += 2;
                while (pos < src.Length && (Uri.IsHexDigit(src[pos]) || src[pos] == '_')) pos++;
                EmitNumber(Convert.ToInt64(Clean(src.Substring(start + 2, pos - start - 2)), 16), start);
                return;
            }

            if (Peek() == '0' && (Peek(1) == 'b' || Peek(1) == 'B'))
            {
                pos += 2;
                while (pos < src.Length && (src[pos] == '0' || src[pos] == '1' || src[pos] == '_')) pos++;
                EmitNumber(Convert.ToInt64(Clean(src.Substring(start + 2, pos - start - 2)), 2), start);
                return;
            }

            if (Peek() == '0' && (Peek(1) == 'o' || Peek(1) == 'O'))
            {
                pos += 2;
                while (pos < src.Length && ((src[pos] >= '0' && src[pos] <= '7') || src[pos] == '_')) pos++;
                EmitNumber(Convert.ToInt64(Clean(src.Substring(start + 2, pos - start - 2)), 8), start);
                return;
            }

            bool isFloat = false;

            while (pos < src.Length && (char.IsDigit(src[pos]) || src[pos] == '_')) pos++;

            if (pos < src.Length && src[pos] == '.' && char.IsDigit(Peek(1)))
            {
                isFloat = true;
                pos++;
                while (pos < src.Length && (char.IsDigit(src[pos]) || src[pos] == '_')) pos++;
            }
            else if (pos < src.Length && src[pos] == '.' && !IsNameStart(Peek(1)))
            {
                // Trailing dot, as in "1." — a float with no fractional digits.
                isFloat = true;
                pos++;
            }

            if (pos < src.Length && (src[pos] == 'e' || src[pos] == 'E'))
            {
                int save = pos;
                pos++;
                if (pos < src.Length && (src[pos] == '+' || src[pos] == '-')) pos++;
                if (pos < src.Length && char.IsDigit(src[pos]))
                {
                    isFloat = true;
                    while (pos < src.Length && char.IsDigit(src[pos])) pos++;
                }
                else pos = save;
            }

            string text = Clean(src.Substring(start, pos - start));

            // Python 2 long suffix.
            if (pos < src.Length && (src[pos] == 'L' || src[pos] == 'l')) pos++;

            // Complex literals are not supported; treat the suffix as a float.
            if (pos < src.Length && (src[pos] == 'j' || src[pos] == 'J')) { pos++; isFloat = true; }

            if (isFloat) EmitNumber(double.Parse(text, CultureInfo.InvariantCulture), start);
            else EmitNumber(long.Parse(text, CultureInfo.InvariantCulture), start);
        }

        static string Clean(string s) { return s.Replace("_", ""); }

        void EmitNumber(object value, int start)
        {
            var t = new Token
            {
                Type = TokType.Number,
                Text = src.Substring(start, pos - start),
                Value = value,
                Line = line,
                Column = start - lineStart,
            };
            tokens.Add(t);
        }

        /// <summary>Reads a string literal, handling prefixes and triple quotes.</summary>
        bool TryReadString()
        {
            int start = pos;
            int scan = pos;

            bool raw = false, isFormat = false, isBytes = false;

            while (scan < src.Length && IsNameChar(src[scan])) scan++;
            string prefix = src.Substring(pos, scan - pos);

            if (prefix.Length > 0)
            {
                if (!IsStringPrefix(prefix)) return false;
                if (scan >= src.Length || (src[scan] != '"' && src[scan] != '\'')) return false;

                foreach (char c in prefix.ToLowerInvariant())
                {
                    if (c == 'r') raw = true;
                    else if (c == 'f') isFormat = true;
                    else if (c == 'b') isBytes = true;
                }
            }
            else if (scan >= src.Length || (src[scan] != '"' && src[scan] != '\'')) return false;

            pos = scan;
            char quote = src[pos];

            bool triple = pos + 2 < src.Length && src[pos + 1] == quote && src[pos + 2] == quote;
            pos += triple ? 3 : 1;

            var sb = new StringBuilder();

            while (true)
            {
                if (pos >= src.Length) throw Error("unterminated string literal");

                char c = src[pos];

                if (c == '\n')
                {
                    if (!triple) throw Error("unterminated string literal");
                    sb.Append('\n');
                    pos++;
                    NewLine();
                    continue;
                }

                if (c == '\\')
                {
                    if (raw)
                    {
                        // Raw strings keep the backslash but a quote can still be escaped.
                        sb.Append(c);
                        pos++;
                        if (pos < src.Length) { sb.Append(src[pos]); pos++; }
                        continue;
                    }
                    pos++;
                    AppendEscape(sb);
                    continue;
                }

                if (c == quote)
                {
                    if (!triple) { pos++; break; }
                    if (pos + 2 < src.Length && src[pos + 1] == quote && src[pos + 2] == quote)
                    {
                        pos += 3;
                        break;
                    }
                }

                sb.Append(c);
                pos++;
            }

            var token = new Token
            {
                Type = isFormat ? TokType.FString : TokType.String,
                Text = src.Substring(start, pos - start),
                Value = isBytes ? (object)Latin1Bytes(sb.ToString()) : sb.ToString(),
                Line = line,
                Column = start - lineStart,
            };
            tokens.Add(token);
            return true;
        }

        static byte[] Latin1Bytes(string s)
        {
            var rv = new byte[s.Length];
            for (int i = 0; i < s.Length; i++) rv[i] = (byte)s[i];
            return rv;
        }

        void AppendEscape(StringBuilder sb)
        {
            if (pos >= src.Length) return;
            char c = src[pos++];

            switch (c)
            {
                case 'n': sb.Append('\n'); break;
                case 't': sb.Append('\t'); break;
                case 'r': sb.Append('\r'); break;
                case 'a': sb.Append('\a'); break;
                case 'b': sb.Append('\b'); break;
                case 'f': sb.Append('\f'); break;
                case 'v': sb.Append('\v'); break;
                case '0': sb.Append('\0'); break;
                case '\\': sb.Append('\\'); break;
                case '\'': sb.Append('\''); break;
                case '"': sb.Append('"'); break;
                case '\n': NewLine(); break; // line continuation inside a string
                case 'x':
                    sb.Append((char)Convert.ToInt32(src.Substring(pos, 2), 16));
                    pos += 2;
                    break;
                case 'u':
                    sb.Append((char)Convert.ToInt32(src.Substring(pos, 4), 16));
                    pos += 4;
                    break;
                case 'U':
                {
                    int cp = Convert.ToInt32(src.Substring(pos, 8), 16);
                    pos += 8;
                    sb.Append(char.ConvertFromUtf32(cp));
                    break;
                }
                default:
                    // Unknown escapes keep the backslash, as Python does.
                    sb.Append('\\').Append(c);
                    break;
            }
        }

        bool ReadOperator()
        {
            for (int i = 0; i < Operators.Length; i++)
            {
                string op = Operators[i];
                if (pos + op.Length > src.Length) continue;
                if (string.CompareOrdinal(src, pos, op, 0, op.Length) != 0) continue;

                if (op == "(" || op == "[" || op == "{") bracketDepth++;
                else if (op == ")" || op == "]" || op == "}") bracketDepth = Math.Max(0, bracketDepth - 1);

                Emit(TokType.Op, op);
                pos += op.Length;
                return true;
            }
            return false;
        }

        // ---------------------------------------------------------------- utilities

        char Peek(int offset = 0)
        {
            int i = pos + offset;
            return i < src.Length ? src[i] : '\0';
        }

        static bool IsNameStart(char c) { return char.IsLetter(c) || c == '_'; }
        static bool IsNameChar(char c) { return char.IsLetterOrDigit(c) || c == '_'; }

        void NewLine() { line++; lineStart = pos; }

        void Emit(TokType type, string text)
        {
            tokens.Add(new Token
            {
                Type = type,
                Text = text,
                Line = line,
                Column = pos - lineStart,
            });
        }

        PyError Error(string message)
        {
            return new PyError("SyntaxError", message + " (" + filename + ", line " + line + ")");
        }
    }
}
