namespace Unity.VisualScripting
{
	public static class ObjectVariables
	{
		public static global::Unity.VisualScripting.VariableDeclarations Declarations(global::UnityEngine.GameObject source, bool autoAddComponent, bool throwOnMissing)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			global::Unity.VisualScripting.Variables variables = source.GetComponent<global::Unity.VisualScripting.Variables>();
			if (variables == null && autoAddComponent)
			{
				variables = source.AddComponent<global::Unity.VisualScripting.Variables>();
			}
			if (variables != null)
			{
				return variables.declarations;
			}
			if (throwOnMissing)
			{
				throw new global::System.InvalidOperationException("Game object '" + source.name + "' does not have variables.");
			}
			return null;
		}
	}
}
