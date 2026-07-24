namespace Unity.VisualScripting.Antlr3.Runtime
{
	public abstract class Lexer : global::Unity.VisualScripting.Antlr3.Runtime.BaseRecognizer, global::Unity.VisualScripting.Antlr3.Runtime.ITokenSource
	{
		private const int TOKEN_dot_EOF = -1;

		protected internal global::Unity.VisualScripting.Antlr3.Runtime.ICharStream input;

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.ICharStream CharStream
		{
			get
			{
				return input;
			}
			set
			{
				input = null;
				Reset();
				input = value;
			}
		}

		public override string SourceName => input.SourceName;

		public override global::Unity.VisualScripting.Antlr3.Runtime.IIntStream Input => input;

		public virtual int Line => input.Line;

		public virtual int CharPositionInLine => input.CharPositionInLine;

		public virtual int CharIndex => input.Index();

		public virtual string Text
		{
			get
			{
				if (state.text != null)
				{
					return state.text;
				}
				return input.Substring(state.tokenStartCharIndex, CharIndex - 1);
			}
			set
			{
				state.text = value;
			}
		}

		public Lexer()
		{
		}

		public Lexer(global::Unity.VisualScripting.Antlr3.Runtime.ICharStream input)
		{
			this.input = input;
		}

		public Lexer(global::Unity.VisualScripting.Antlr3.Runtime.ICharStream input, global::Unity.VisualScripting.Antlr3.Runtime.RecognizerSharedState state)
			: base(state)
		{
			this.input = input;
		}

		public override void Reset()
		{
			base.Reset();
			if (input != null)
			{
				input.Seek(0);
			}
			if (state != null)
			{
				state.token = null;
				state.type = 0;
				state.channel = 0;
				state.tokenStartCharIndex = -1;
				state.tokenStartCharPositionInLine = -1;
				state.tokenStartLine = -1;
				state.text = null;
			}
		}

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.IToken NextToken()
		{
			while (true)
			{
				state.token = null;
				state.channel = 0;
				state.tokenStartCharIndex = input.Index();
				state.tokenStartCharPositionInLine = input.CharPositionInLine;
				state.tokenStartLine = input.Line;
				state.text = null;
				if (input.LA(1) == -1)
				{
					break;
				}
				try
				{
					mTokens();
					if (state.token == null)
					{
						Emit();
						goto IL_00ae;
					}
					if (state.token != global::Unity.VisualScripting.Antlr3.Runtime.Token.SKIP_TOKEN)
					{
						goto IL_00ae;
					}
					goto end_IL_007b;
					IL_00ae:
					return state.token;
					end_IL_007b:;
				}
				catch (global::Unity.VisualScripting.Antlr3.Runtime.NoViableAltException ex)
				{
					ReportError(ex);
					Recover(ex);
				}
				catch (global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e)
				{
					ReportError(e);
				}
			}
			return global::Unity.VisualScripting.Antlr3.Runtime.Token.EOF_TOKEN;
		}

		public void Skip()
		{
			state.token = global::Unity.VisualScripting.Antlr3.Runtime.Token.SKIP_TOKEN;
		}

		public abstract void mTokens();

		public virtual void Emit(global::Unity.VisualScripting.Antlr3.Runtime.IToken token)
		{
			state.token = token;
		}

		public virtual global::Unity.VisualScripting.Antlr3.Runtime.IToken Emit()
		{
			global::Unity.VisualScripting.Antlr3.Runtime.IToken token = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(input, state.type, state.channel, state.tokenStartCharIndex, CharIndex - 1);
			token.Line = state.tokenStartLine;
			token.Text = state.text;
			token.CharPositionInLine = state.tokenStartCharPositionInLine;
			Emit(token);
			return token;
		}

		public virtual void Match(string s)
		{
			int num = 0;
			while (num < s.Length)
			{
				if (input.LA(1) != s[num])
				{
					if (state.backtracking > 0)
					{
						state.failed = true;
						break;
					}
					global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException ex = new global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException(s[num], input);
					Recover(ex);
					throw ex;
				}
				num++;
				input.Consume();
				state.failed = false;
			}
		}

		public virtual void MatchAny()
		{
			input.Consume();
		}

		public virtual void Match(int c)
		{
			if (input.LA(1) != c)
			{
				if (state.backtracking <= 0)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException ex = new global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException(c, input);
					Recover(ex);
					throw ex;
				}
				state.failed = true;
			}
			else
			{
				input.Consume();
				state.failed = false;
			}
		}

		public virtual void MatchRange(int a, int b)
		{
			if (input.LA(1) < a || input.LA(1) > b)
			{
				if (state.backtracking <= 0)
				{
					global::Unity.VisualScripting.Antlr3.Runtime.MismatchedRangeException ex = new global::Unity.VisualScripting.Antlr3.Runtime.MismatchedRangeException(a, b, input);
					Recover(ex);
					throw ex;
				}
				state.failed = true;
			}
			else
			{
				input.Consume();
				state.failed = false;
			}
		}

		public virtual void Recover(global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException re)
		{
			input.Consume();
		}

		public override void ReportError(global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e)
		{
			DisplayRecognitionError(TokenNames, e);
		}

		public override string GetErrorMessage(global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException e, string[] tokenNames)
		{
			string text = null;
			if (e is global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException ex = (global::Unity.VisualScripting.Antlr3.Runtime.MismatchedTokenException)e;
				return "mismatched character " + GetCharErrorDisplay(e.Char) + " expecting " + GetCharErrorDisplay(ex.Expecting);
			}
			if (e is global::Unity.VisualScripting.Antlr3.Runtime.NoViableAltException)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.NoViableAltException ex2 = (global::Unity.VisualScripting.Antlr3.Runtime.NoViableAltException)e;
				return "no viable alternative at character " + GetCharErrorDisplay(ex2.Char);
			}
			if (e is global::Unity.VisualScripting.Antlr3.Runtime.EarlyExitException)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.EarlyExitException ex3 = (global::Unity.VisualScripting.Antlr3.Runtime.EarlyExitException)e;
				return "required (...)+ loop did not match anything at character " + GetCharErrorDisplay(ex3.Char);
			}
			if (e is global::Unity.VisualScripting.Antlr3.Runtime.MismatchedNotSetException)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.MismatchedSetException ex4 = (global::Unity.VisualScripting.Antlr3.Runtime.MismatchedSetException)e;
				return "mismatched character " + GetCharErrorDisplay(ex4.Char) + " expecting set " + ex4.expecting;
			}
			if (e is global::Unity.VisualScripting.Antlr3.Runtime.MismatchedSetException)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.MismatchedSetException ex5 = (global::Unity.VisualScripting.Antlr3.Runtime.MismatchedSetException)e;
				return "mismatched character " + GetCharErrorDisplay(ex5.Char) + " expecting set " + ex5.expecting;
			}
			if (e is global::Unity.VisualScripting.Antlr3.Runtime.MismatchedRangeException)
			{
				global::Unity.VisualScripting.Antlr3.Runtime.MismatchedRangeException ex6 = (global::Unity.VisualScripting.Antlr3.Runtime.MismatchedRangeException)e;
				return "mismatched character " + GetCharErrorDisplay(ex6.Char) + " expecting set " + GetCharErrorDisplay(ex6.A) + ".." + GetCharErrorDisplay(ex6.B);
			}
			return base.GetErrorMessage(e, tokenNames);
		}

		public string GetCharErrorDisplay(int c)
		{
			return "'" + c switch
			{
				-1 => "<EOF>", 
				10 => "\\n", 
				9 => "\\t", 
				13 => "\\r", 
				_ => global::System.Convert.ToString((char)c), 
			} + "'";
		}

		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			string inputSymbol = (char)input.LT(1) + " line=" + Line + ":" + CharPositionInLine;
			base.TraceIn(ruleName, ruleIndex, inputSymbol);
		}

		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			string inputSymbol = (char)input.LT(1) + " line=" + Line + ":" + CharPositionInLine;
			base.TraceOut(ruleName, ruleIndex, inputSymbol);
		}
	}
}
