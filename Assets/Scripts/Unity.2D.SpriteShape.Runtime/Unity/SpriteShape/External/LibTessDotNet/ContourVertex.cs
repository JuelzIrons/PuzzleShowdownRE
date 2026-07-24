namespace Unity.SpriteShape.External.LibTessDotNet
{
	internal struct ContourVertex
	{
		public global::Unity.SpriteShape.External.LibTessDotNet.Vec3 Position;

		public object Data;

		public override string ToString()
		{
			return $"{Position}, {Data}";
		}
	}
}
