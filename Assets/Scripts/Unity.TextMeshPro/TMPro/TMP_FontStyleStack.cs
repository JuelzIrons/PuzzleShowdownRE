namespace TMPro
{
	public struct TMP_FontStyleStack
	{
		public byte bold;

		public byte italic;

		public byte underline;

		public byte strikethrough;

		public byte highlight;

		public byte superscript;

		public byte subscript;

		public byte uppercase;

		public byte lowercase;

		public byte smallcaps;

		public void Clear()
		{
			bold = 0;
			italic = 0;
			underline = 0;
			strikethrough = 0;
			highlight = 0;
			superscript = 0;
			subscript = 0;
			uppercase = 0;
			lowercase = 0;
			smallcaps = 0;
		}

		public byte Add(global::TMPro.FontStyles style)
		{
			switch (style)
			{
			case global::TMPro.FontStyles.Bold:
				bold++;
				return bold;
			case global::TMPro.FontStyles.Italic:
				italic++;
				return italic;
			case global::TMPro.FontStyles.Underline:
				underline++;
				return underline;
			case global::TMPro.FontStyles.UpperCase:
				uppercase++;
				return uppercase;
			case global::TMPro.FontStyles.LowerCase:
				lowercase++;
				return lowercase;
			case global::TMPro.FontStyles.Strikethrough:
				strikethrough++;
				return strikethrough;
			case global::TMPro.FontStyles.Superscript:
				superscript++;
				return superscript;
			case global::TMPro.FontStyles.Subscript:
				subscript++;
				return subscript;
			case global::TMPro.FontStyles.Highlight:
				highlight++;
				return highlight;
			default:
				return 0;
			}
		}

		public byte Remove(global::TMPro.FontStyles style)
		{
			switch (style)
			{
			case global::TMPro.FontStyles.Bold:
				if (bold > 1)
				{
					bold--;
				}
				else
				{
					bold = 0;
				}
				return bold;
			case global::TMPro.FontStyles.Italic:
				if (italic > 1)
				{
					italic--;
				}
				else
				{
					italic = 0;
				}
				return italic;
			case global::TMPro.FontStyles.Underline:
				if (underline > 1)
				{
					underline--;
				}
				else
				{
					underline = 0;
				}
				return underline;
			case global::TMPro.FontStyles.UpperCase:
				if (uppercase > 1)
				{
					uppercase--;
				}
				else
				{
					uppercase = 0;
				}
				return uppercase;
			case global::TMPro.FontStyles.LowerCase:
				if (lowercase > 1)
				{
					lowercase--;
				}
				else
				{
					lowercase = 0;
				}
				return lowercase;
			case global::TMPro.FontStyles.Strikethrough:
				if (strikethrough > 1)
				{
					strikethrough--;
				}
				else
				{
					strikethrough = 0;
				}
				return strikethrough;
			case global::TMPro.FontStyles.Highlight:
				if (highlight > 1)
				{
					highlight--;
				}
				else
				{
					highlight = 0;
				}
				return highlight;
			case global::TMPro.FontStyles.Superscript:
				if (superscript > 1)
				{
					superscript--;
				}
				else
				{
					superscript = 0;
				}
				return superscript;
			case global::TMPro.FontStyles.Subscript:
				if (subscript > 1)
				{
					subscript--;
				}
				else
				{
					subscript = 0;
				}
				return subscript;
			default:
				return 0;
			}
		}
	}
}
