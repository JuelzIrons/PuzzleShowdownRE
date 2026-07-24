namespace UnityEngine.U2D.Animation
{
	internal static class SpriteSkinHelpers
	{
		public static void CacheChildren(global::UnityEngine.Transform current, global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>> cache)
		{
			int hashCode = current.name.GetHashCode();
			global::UnityEngine.U2D.Animation.SpriteSkin.TransformData item = new global::UnityEngine.U2D.Animation.SpriteSkin.TransformData
			{
				fullName = string.Empty,
				transform = current
			};
			if (cache.TryGetValue(hashCode, out var value))
			{
				value.Add(item);
			}
			else
			{
				cache.Add(hashCode, new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>(1) { item });
			}
			for (int i = 0; i < current.childCount; i++)
			{
				CacheChildren(current.GetChild(i), cache);
			}
		}

		public static string GenerateTransformPath(global::UnityEngine.Transform rootBone, global::UnityEngine.Transform child)
		{
			string text = child.name;
			if (child == rootBone)
			{
				return text;
			}
			global::UnityEngine.Transform parent = child.parent;
			do
			{
				text = parent.name + "/" + text;
				parent = parent.parent;
			}
			while (parent != rootBone && parent != null);
			return text;
		}

		public static bool GetSpriteBonesTransforms(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin, out global::UnityEngine.Transform[] outTransform, bool forceCreateCache = false)
		{
			global::UnityEngine.Transform rootBone = spriteSkin.rootBone;
			global::UnityEngine.U2D.SpriteBone[] bones = spriteSkin.sprite.GetBones();
			if (rootBone == null)
			{
				throw new global::System.ArgumentException("rootBone parameter cannot be null");
			}
			if (bones == null)
			{
				throw new global::System.ArgumentException("spriteBones parameter cannot be null");
			}
			outTransform = new global::UnityEngine.Transform[bones.Length];
			global::UnityEngine.U2D.Animation.Bone[] componentsInChildren = rootBone.GetComponentsInChildren<global::UnityEngine.U2D.Animation.Bone>();
			if (componentsInChildren != null && componentsInChildren.Length >= bones.Length)
			{
				using (global::UnityEngine.U2D.Animation.SpriteSkin.Profiling.getSpriteBonesTransformFromGuid.Auto())
				{
					int i;
					for (i = 0; i < bones.Length; i++)
					{
						string boneHash = bones[i].guid;
						global::UnityEngine.U2D.Animation.Bone bone = global::System.Array.Find(componentsInChildren, (global::UnityEngine.U2D.Animation.Bone x) => x.guid == boneHash);
						if (bone == null)
						{
							break;
						}
						outTransform[i] = bone.transform;
					}
					if (i >= bones.Length)
					{
						return true;
					}
				}
			}
			global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>> hierarchyCache = spriteSkin.hierarchyCache;
			if (hierarchyCache.Count == 0)
			{
				spriteSkin.CacheHierarchy(forceCreateCache);
			}
			return GetSpriteBonesTransformFromPath(bones, hierarchyCache, outTransform);
		}

		private static bool GetSpriteBonesTransformFromPath(global::UnityEngine.U2D.SpriteBone[] spriteBones, global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin.TransformData>> hierarchyCache, global::UnityEngine.Transform[] outNewBoneTransform)
		{
			using (global::UnityEngine.U2D.Animation.SpriteSkin.Profiling.getSpriteBonesTransformFromPath.Auto())
			{
				string[] array = null;
				bool result = true;
				for (int i = 0; i < spriteBones.Length; i++)
				{
					int hashCode = spriteBones[i].name.GetHashCode();
					if (!hierarchyCache.TryGetValue(hashCode, out var value))
					{
						outNewBoneTransform[i] = null;
						result = false;
						continue;
					}
					if (value.Count == 1)
					{
						outNewBoneTransform[i] = value[0].transform;
						continue;
					}
					if (array == null)
					{
						array = new string[spriteBones.Length];
					}
					if (array[i] == null)
					{
						CalculateBoneTransformsPath(i, spriteBones, array);
					}
					int j;
					for (j = 0; j < value.Count; j++)
					{
						if (value[j].fullName.Contains(array[i]))
						{
							outNewBoneTransform[i] = value[j].transform;
							break;
						}
					}
					if (j >= value.Count)
					{
						outNewBoneTransform[i] = null;
						result = false;
					}
				}
				return result;
			}
		}

		private static void CalculateBoneTransformsPath(int index, global::UnityEngine.U2D.SpriteBone[] spriteBones, string[] paths)
		{
			global::UnityEngine.U2D.SpriteBone spriteBone = spriteBones[index];
			int parentId = spriteBone.parentId;
			string name = spriteBone.name;
			if (parentId != -1)
			{
				if (paths[parentId] == null)
				{
					CalculateBoneTransformsPath(spriteBone.parentId, spriteBones, paths);
				}
				paths[index] = paths[parentId] + "/" + name;
			}
			else
			{
				paths[index] = name;
			}
		}
	}
}
