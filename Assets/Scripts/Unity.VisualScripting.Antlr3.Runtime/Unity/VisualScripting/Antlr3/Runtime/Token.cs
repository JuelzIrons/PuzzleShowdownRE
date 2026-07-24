namespace Unity.VisualScripting.Antlr3.Runtime
{
	public static class Token
	{
		public const int EOR_TOKEN_TYPE = 1;

		public const int DOWN = 2;

		public const int UP = 3;

		public const int INVALID_TOKEN_TYPE = 0;

		public const int DEFAULT_CHANNEL = 0;

		public const int HIDDEN_CHANNEL = 99;

		public static readonly int MIN_TOKEN_TYPE = 4;

		public static readonly int EOF = -1;

		public static readonly global::Unity.VisualScripting.Antlr3.Runtime.IToken EOF_TOKEN = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(EOF);

		public static readonly global::Unity.VisualScripting.Antlr3.Runtime.IToken INVALID_TOKEN = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(0);

		public static readonly global::Unity.VisualScripting.Antlr3.Runtime.IToken SKIP_TOKEN = new global::Unity.VisualScripting.Antlr3.Runtime.CommonToken(0);
	}
}
