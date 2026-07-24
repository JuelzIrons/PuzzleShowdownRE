namespace Unity.Mathematics
{
	[global::System.Serializable]
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public struct RigidTransform
	{
		public global::Unity.Mathematics.quaternion rot;

		public global::Unity.Mathematics.float3 pos;

		public static readonly global::Unity.Mathematics.RigidTransform identity = new global::Unity.Mathematics.RigidTransform(new global::Unity.Mathematics.quaternion(0f, 0f, 0f, 1f), new global::Unity.Mathematics.float3(0f, 0f, 0f));

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public RigidTransform(global::Unity.Mathematics.quaternion rotation, global::Unity.Mathematics.float3 translation)
		{
			rot = rotation;
			pos = translation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public RigidTransform(global::Unity.Mathematics.float3x3 rotation, global::Unity.Mathematics.float3 translation)
		{
			rot = new global::Unity.Mathematics.quaternion(rotation);
			pos = translation;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public RigidTransform(global::Unity.Mathematics.float4x4 transform)
		{
			rot = new global::Unity.Mathematics.quaternion(transform);
			pos = transform.c3.xyz;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform AxisAngle(global::Unity.Mathematics.float3 axis, float angle)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.AxisAngle(axis, angle), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerXYZ(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.EulerXYZ(xyz), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerXZY(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.EulerXZY(xyz), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerYXZ(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.EulerYXZ(xyz), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerYZX(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.EulerYZX(xyz), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerZXY(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.EulerZXY(xyz), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerZYX(global::Unity.Mathematics.float3 xyz)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.EulerZYX(xyz), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerXYZ(float x, float y, float z)
		{
			return EulerXYZ(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerXZY(float x, float y, float z)
		{
			return EulerXZY(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerYXZ(float x, float y, float z)
		{
			return EulerYXZ(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerYZX(float x, float y, float z)
		{
			return EulerYZX(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerZXY(float x, float y, float z)
		{
			return EulerZXY(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform EulerZYX(float x, float y, float z)
		{
			return EulerZYX(global::Unity.Mathematics.math.float3(x, y, z));
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform Euler(global::Unity.Mathematics.float3 xyz, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
		{
			return order switch
			{
				global::Unity.Mathematics.math.RotationOrder.XYZ => EulerXYZ(xyz), 
				global::Unity.Mathematics.math.RotationOrder.XZY => EulerXZY(xyz), 
				global::Unity.Mathematics.math.RotationOrder.YXZ => EulerYXZ(xyz), 
				global::Unity.Mathematics.math.RotationOrder.YZX => EulerYZX(xyz), 
				global::Unity.Mathematics.math.RotationOrder.ZXY => EulerZXY(xyz), 
				global::Unity.Mathematics.math.RotationOrder.ZYX => EulerZYX(xyz), 
				_ => identity, 
			};
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform Euler(float x, float y, float z, global::Unity.Mathematics.math.RotationOrder order = global::Unity.Mathematics.math.RotationOrder.ZXY)
		{
			return Euler(global::Unity.Mathematics.math.float3(x, y, z), order);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform RotateX(float angle)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.RotateX(angle), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform RotateY(float angle)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.RotateY(angle), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform RotateZ(float angle)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.RotateZ(angle), global::Unity.Mathematics.float3.zero);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Unity.Mathematics.RigidTransform Translate(global::Unity.Mathematics.float3 vector)
		{
			return new global::Unity.Mathematics.RigidTransform(global::Unity.Mathematics.quaternion.identity, vector);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::Unity.Mathematics.RigidTransform x)
		{
			if (rot.Equals(x.rot))
			{
				return pos.Equals(x.pos);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object x)
		{
			if (x is global::Unity.Mathematics.RigidTransform x2)
			{
				return Equals(x2);
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)global::Unity.Mathematics.math.hash(this);
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return $"RigidTransform(({rot.value.x}f, {rot.value.y}f, {rot.value.z}f, {rot.value.w}f),  ({pos.x}f, {pos.y}f, {pos.z}f))";
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, global::System.IFormatProvider formatProvider)
		{
			return $"float4x4(({rot.value.x.ToString(format, formatProvider)}f, {rot.value.y.ToString(format, formatProvider)}f, {rot.value.z.ToString(format, formatProvider)}f, {rot.value.w.ToString(format, formatProvider)}f),  ({pos.x.ToString(format, formatProvider)}f, {pos.y.ToString(format, formatProvider)}f, {pos.z.ToString(format, formatProvider)}f))";
		}
	}
}
