namespace Unity.VisualScripting.Antlr3.Runtime
{
	[global::System.Serializable]
	public class MismatchedRangeException : global::Unity.VisualScripting.Antlr3.Runtime.RecognitionException
	{
		private int a;

		private int b;

		public int A
		{
			get
			{
				return a;
			}
			set
			{
				a = value;
			}
		}

		public int B
		{
			get
			{
				return b;
			}
			set
			{
				b = value;
			}
		}

		public MismatchedRangeException()
		{
		}

		public MismatchedRangeException(int a, int b, global::Unity.VisualScripting.Antlr3.Runtime.IIntStream input)
			: base(input)
		{
			this.a = a;
			this.b = b;
		}

		public override string ToString()
		{
			return "MismatchedNotSetException(" + UnexpectedType + " not in [" + a + "," + b + "])";
		}
	}
}
