namespace TMPro
{
	public struct MaterialReference
	{
		public int index;

		public global::TMPro.TMP_FontAsset fontAsset;

		public global::TMPro.TMP_SpriteAsset spriteAsset;

		public global::UnityEngine.Material material;

		public bool isDefaultMaterial;

		public bool isFallbackMaterial;

		public global::UnityEngine.Material fallbackMaterial;

		public float padding;

		public int referenceCount;

		public MaterialReference(int index, global::TMPro.TMP_FontAsset fontAsset, global::TMPro.TMP_SpriteAsset spriteAsset, global::UnityEngine.Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			isDefaultMaterial = material.GetInstanceID() == fontAsset.material.GetInstanceID();
			isFallbackMaterial = false;
			fallbackMaterial = null;
			this.padding = padding;
			referenceCount = 0;
		}

		public static bool Contains(global::TMPro.MaterialReference[] materialReferences, global::TMPro.TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.GetInstanceID();
			for (int i = 0; i < materialReferences.Length && materialReferences[i].fontAsset != null; i++)
			{
				if (materialReferences[i].fontAsset.GetInstanceID() == instanceID)
				{
					return true;
				}
			}
			return false;
		}

		public static int AddMaterialReference(global::UnityEngine.Material material, global::TMPro.TMP_FontAsset fontAsset, ref global::TMPro.MaterialReference[] materialReferences, global::System.Collections.Generic.Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			if (materialReferenceIndexLookup.TryGetValue(instanceID, out var value))
			{
				return value;
			}
			value = (materialReferenceIndexLookup[instanceID] = materialReferenceIndexLookup.Count);
			if (value >= materialReferences.Length)
			{
				global::System.Array.Resize(ref materialReferences, global::UnityEngine.Mathf.NextPowerOfTwo(value + 1));
			}
			materialReferences[value].index = value;
			materialReferences[value].fontAsset = fontAsset;
			materialReferences[value].spriteAsset = null;
			materialReferences[value].material = material;
			materialReferences[value].isDefaultMaterial = instanceID == fontAsset.material.GetInstanceID();
			materialReferences[value].referenceCount = 0;
			return value;
		}

		public static int AddMaterialReference(global::UnityEngine.Material material, global::TMPro.TMP_SpriteAsset spriteAsset, ref global::TMPro.MaterialReference[] materialReferences, global::System.Collections.Generic.Dictionary<int, int> materialReferenceIndexLookup)
		{
			int instanceID = material.GetInstanceID();
			if (materialReferenceIndexLookup.TryGetValue(instanceID, out var value))
			{
				return value;
			}
			value = (materialReferenceIndexLookup[instanceID] = materialReferenceIndexLookup.Count);
			if (value >= materialReferences.Length)
			{
				global::System.Array.Resize(ref materialReferences, global::UnityEngine.Mathf.NextPowerOfTwo(value + 1));
			}
			materialReferences[value].index = value;
			materialReferences[value].fontAsset = materialReferences[0].fontAsset;
			materialReferences[value].spriteAsset = spriteAsset;
			materialReferences[value].material = material;
			materialReferences[value].isDefaultMaterial = true;
			materialReferences[value].referenceCount = 0;
			return value;
		}
	}
}
