namespace TMPro.SpriteAssetUtilities
{
	public class TexturePacker_JsonArray
	{
		[global::System.Serializable]
		public struct SpriteFrame
		{
			public float x;

			public float y;

			public float w;

			public float h;

			public override string ToString()
			{
				return "x: " + x.ToString("f2") + " y: " + y.ToString("f2") + " h: " + h.ToString("f2") + " w: " + w.ToString("f2");
			}
		}

		[global::System.Serializable]
		public struct SpriteSize
		{
			public float w;

			public float h;

			public override string ToString()
			{
				return "w: " + w.ToString("f2") + " h: " + h.ToString("f2");
			}
		}

		[global::System.Serializable]
		public struct Frame
		{
			public string filename;

			public global::TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteFrame frame;

			public bool rotated;

			public bool trimmed;

			public global::TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteFrame spriteSourceSize;

			public global::TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteSize sourceSize;

			public global::UnityEngine.Vector2 pivot;
		}

		[global::System.Serializable]
		public struct Meta
		{
			public string app;

			public string version;

			public string image;

			public string format;

			public global::TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteSize size;

			public float scale;

			public string smartupdate;
		}

		[global::System.Serializable]
		public class SpriteDataObject
		{
			public global::System.Collections.Generic.List<global::TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.Frame> frames;

			public global::TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.Meta meta;
		}
	}
}
