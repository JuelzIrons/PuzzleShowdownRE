namespace UnityEngine.U2D
{
	public abstract class SpriteShapeGeometryModifier : global::UnityEngine.ScriptableObject
	{
		public abstract global::Unity.Jobs.JobHandle MakeModifierJob(global::Unity.Jobs.JobHandle generator, global::UnityEngine.U2D.SpriteShapeController spriteShapeController, global::Unity.Collections.NativeArray<ushort> indices, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector3> positions, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector2> texCoords, global::Unity.Collections.NativeSlice<global::UnityEngine.Vector4> tangents, global::Unity.Collections.NativeArray<global::UnityEngine.U2D.SpriteShapeSegment> segments, global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> colliderData);

		public virtual int GetVersion()
		{
			return GetInstanceID();
		}
	}
}
