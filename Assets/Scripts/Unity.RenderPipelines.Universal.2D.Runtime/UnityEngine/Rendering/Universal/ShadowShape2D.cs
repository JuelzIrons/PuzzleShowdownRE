namespace UnityEngine.Rendering.Universal
{
	public abstract class ShadowShape2D
	{
		public enum OutlineTopology
		{
			Lines = 0,
			Triangles = 1
		}

		public enum WindingOrder
		{
			Clockwise = 0,
			CounterClockwise = 1
		}

		public abstract void SetFlip(bool flipX, bool flipY);

		public abstract void GetFlip(out bool flipX, out bool flipY);

		public abstract void SetDefaultTrim(float trim);

		public abstract void SetShape(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, global::Unity.Collections.NativeArray<int> indices, global::Unity.Collections.NativeArray<float> radii, global::UnityEngine.Matrix4x4 transform, global::UnityEngine.Rendering.Universal.ShadowShape2D.WindingOrder windingOrder = global::UnityEngine.Rendering.Universal.ShadowShape2D.WindingOrder.Clockwise, bool allowContraction = true, bool createInteriorGeometry = false);

		public abstract void SetShape(global::Unity.Collections.NativeArray<global::UnityEngine.Vector3> vertices, global::Unity.Collections.NativeArray<int> indices, global::UnityEngine.Rendering.Universal.ShadowShape2D.OutlineTopology outlineTopology, global::UnityEngine.Rendering.Universal.ShadowShape2D.WindingOrder windingOrder = global::UnityEngine.Rendering.Universal.ShadowShape2D.WindingOrder.Clockwise, bool allowContraction = true, bool createInteriorGeometry = false);
	}
}
