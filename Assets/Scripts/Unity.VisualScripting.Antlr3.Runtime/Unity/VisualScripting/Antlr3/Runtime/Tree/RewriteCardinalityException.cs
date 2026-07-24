namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	[global::System.Serializable]
	public class RewriteCardinalityException : global::System.Exception
	{
		public string elementDescription;

		public override string Message
		{
			get
			{
				if (elementDescription != null)
				{
					return elementDescription;
				}
				return null;
			}
		}

		public RewriteCardinalityException(string elementDescription)
		{
			this.elementDescription = elementDescription;
		}
	}
}
