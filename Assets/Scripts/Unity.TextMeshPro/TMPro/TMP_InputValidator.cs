namespace TMPro
{
	[global::System.Serializable]
	public abstract class TMP_InputValidator : global::UnityEngine.ScriptableObject
	{
		public abstract char Validate(ref string text, ref int pos, char ch);
	}
}
