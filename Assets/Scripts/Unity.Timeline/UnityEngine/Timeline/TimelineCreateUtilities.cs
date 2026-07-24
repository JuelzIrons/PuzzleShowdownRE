namespace UnityEngine.Timeline
{
	internal static class TimelineCreateUtilities
	{
		public static string GenerateUniqueActorName(global::System.Collections.Generic.List<global::UnityEngine.ScriptableObject> tracks, string name)
		{
			if (!tracks.Exists((global::UnityEngine.ScriptableObject x) => (object)x != null && x.name == name))
			{
				return name;
			}
			int result = 0;
			string text = name;
			if (!string.IsNullOrEmpty(name) && name[name.Length - 1] == ')')
			{
				int num = name.LastIndexOf('(');
				if (num > 0 && int.TryParse(name.Substring(num + 1, name.Length - num - 2), out result))
				{
					result++;
					text = name.Substring(0, num);
				}
			}
			text = text.TrimEnd();
			for (int num2 = result; num2 < result + 5000; num2++)
			{
				if (num2 > 0)
				{
					string result2 = $"{text} ({num2})";
					if (!tracks.Exists((global::UnityEngine.ScriptableObject x) => (object)x != null && x.name == result2))
					{
						return result2;
					}
				}
			}
			return name;
		}

		public static void SaveAssetIntoObject(global::UnityEngine.Object childAsset, global::UnityEngine.Object masterAsset)
		{
			if (!(childAsset == null) && !(masterAsset == null))
			{
				if ((masterAsset.hideFlags & global::UnityEngine.HideFlags.DontSave) != global::UnityEngine.HideFlags.None)
				{
					childAsset.hideFlags |= global::UnityEngine.HideFlags.DontSave;
				}
				else
				{
					childAsset.hideFlags |= global::UnityEngine.HideFlags.HideInHierarchy;
				}
			}
		}

		public static void RemoveAssetFromObject(global::UnityEngine.Object childAsset, global::UnityEngine.Object masterAsset)
		{
			if (!(childAsset == null))
			{
				_ = masterAsset == null;
			}
		}

		public static global::UnityEngine.AnimationClip CreateAnimationClipForTrack(string name, global::UnityEngine.Timeline.TrackAsset track, bool isLegacy)
		{
			global::UnityEngine.Timeline.TimelineAsset timelineAsset = ((track != null) ? track.timelineAsset : null);
			global::UnityEngine.HideFlags hideFlags = ((track != null) ? track.hideFlags : global::UnityEngine.HideFlags.None);
			global::UnityEngine.AnimationClip obj = new global::UnityEngine.AnimationClip
			{
				legacy = isLegacy,
				name = name,
				frameRate = ((timelineAsset == null) ? ((float)global::UnityEngine.Timeline.TimelineAsset.EditorSettings.kDefaultFrameRate) : ((float)timelineAsset.editorSettings.frameRate))
			};
			SaveAssetIntoObject(obj, timelineAsset);
			obj.hideFlags = hideFlags & ~global::UnityEngine.HideFlags.HideInHierarchy;
			return obj;
		}

		public static bool ValidateParentTrack(global::UnityEngine.Timeline.TrackAsset parent, global::System.Type childType)
		{
			if (childType == null || !typeof(global::UnityEngine.Timeline.TrackAsset).IsAssignableFrom(childType))
			{
				return false;
			}
			if (parent == null)
			{
				return true;
			}
			if (parent is global::UnityEngine.Timeline.ILayerable && !parent.isSubTrack && parent.GetType() == childType)
			{
				return true;
			}
			if (!(global::System.Attribute.GetCustomAttribute(parent.GetType(), typeof(global::UnityEngine.Timeline.SupportsChildTracksAttribute)) is global::UnityEngine.Timeline.SupportsChildTracksAttribute supportsChildTracksAttribute))
			{
				return false;
			}
			if (supportsChildTracksAttribute.childType == null)
			{
				return true;
			}
			if (childType == supportsChildTracksAttribute.childType)
			{
				int num = 0;
				global::UnityEngine.Timeline.TrackAsset trackAsset = parent;
				while (trackAsset != null && trackAsset.isSubTrack)
				{
					num++;
					trackAsset = trackAsset.parent as global::UnityEngine.Timeline.TrackAsset;
				}
				return num < supportsChildTracksAttribute.levels;
			}
			return false;
		}
	}
}
