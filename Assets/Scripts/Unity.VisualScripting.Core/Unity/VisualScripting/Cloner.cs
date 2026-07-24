namespace Unity.VisualScripting
{
	public abstract class Cloner<T> : global::Unity.VisualScripting.ICloner
	{
		public abstract bool Handles(global::System.Type type);

		void global::Unity.VisualScripting.ICloner.BeforeClone(global::System.Type type, object original)
		{
			BeforeClone(type, (T)original);
		}

		public virtual void BeforeClone(global::System.Type type, T original)
		{
		}

		object global::Unity.VisualScripting.ICloner.ConstructClone(global::System.Type type, object original)
		{
			return ConstructClone(type, (T)original);
		}

		public virtual T ConstructClone(global::System.Type type, T original)
		{
			return (T)global::System.Activator.CreateInstance(type, nonPublic: true);
		}

		void global::Unity.VisualScripting.ICloner.FillClone(global::System.Type type, ref object clone, object original, global::Unity.VisualScripting.CloningContext context)
		{
			T clone2 = (T)clone;
			FillClone(type, ref clone2, (T)original, context);
			clone = clone2;
		}

		public virtual void FillClone(global::System.Type type, ref T clone, T original, global::Unity.VisualScripting.CloningContext context)
		{
		}

		void global::Unity.VisualScripting.ICloner.AfterClone(global::System.Type type, object clone)
		{
			AfterClone(type, (T)clone);
		}

		public virtual void AfterClone(global::System.Type type, T clone)
		{
		}
	}
}
