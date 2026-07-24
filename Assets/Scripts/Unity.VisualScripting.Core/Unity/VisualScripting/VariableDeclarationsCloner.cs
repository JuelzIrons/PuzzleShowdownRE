namespace Unity.VisualScripting
{
	public sealed class VariableDeclarationsCloner : global::Unity.VisualScripting.Cloner<global::Unity.VisualScripting.VariableDeclarations>
	{
		public static readonly global::Unity.VisualScripting.VariableDeclarationsCloner instance = new global::Unity.VisualScripting.VariableDeclarationsCloner();

		public override bool Handles(global::System.Type type)
		{
			return type == typeof(global::Unity.VisualScripting.VariableDeclarations);
		}

		public override global::Unity.VisualScripting.VariableDeclarations ConstructClone(global::System.Type type, global::Unity.VisualScripting.VariableDeclarations original)
		{
			return new global::Unity.VisualScripting.VariableDeclarations();
		}

		public override void FillClone(global::System.Type type, ref global::Unity.VisualScripting.VariableDeclarations clone, global::Unity.VisualScripting.VariableDeclarations original, global::Unity.VisualScripting.CloningContext context)
		{
			foreach (global::Unity.VisualScripting.VariableDeclaration item in original)
			{
				clone[item.name] = item.value.CloneViaFakeSerialization();
			}
		}
	}
}
