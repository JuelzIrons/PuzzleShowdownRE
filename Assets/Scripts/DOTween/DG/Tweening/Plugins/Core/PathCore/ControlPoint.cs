namespace DG.Tweening.Plugins.Core.PathCore
{
	[global::System.Serializable]
	public struct ControlPoint
	{
		public global::UnityEngine.Vector3 a;

		public global::UnityEngine.Vector3 b;

		public ControlPoint(global::UnityEngine.Vector3 a, global::UnityEngine.Vector3 b)
		{
			this.a = a;
			this.b = b;
		}

		public static global::DG.Tweening.Plugins.Core.PathCore.ControlPoint operator +(global::DG.Tweening.Plugins.Core.PathCore.ControlPoint cp, global::UnityEngine.Vector3 v)
		{
			return new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint(cp.a + v, cp.b + v);
		}

		public override string ToString()
		{
			return "[" + a.ToString() + " | " + b.ToString() + "]";
		}
	}
}
