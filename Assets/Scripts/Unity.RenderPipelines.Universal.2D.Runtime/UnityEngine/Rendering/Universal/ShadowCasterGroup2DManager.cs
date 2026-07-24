namespace UnityEngine.Rendering.Universal
{
	internal class ShadowCasterGroup2DManager
	{
		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> s_ShadowCasterGroups;

		public static global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> shadowCasterGroups => s_ShadowCasterGroups;

		public static void CacheValues()
		{
			if (shadowCasterGroups == null)
			{
				return;
			}
			for (int i = 0; i < shadowCasterGroups.Count; i++)
			{
				if (shadowCasterGroups[i] != null)
				{
					shadowCasterGroups[i].CacheValues();
				}
			}
		}

		public static void AddShadowCasterGroupToList(global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D shadowCaster, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> list)
		{
			if (!list.Contains(shadowCaster))
			{
				int num = 0;
				for (num = 0; num < list.Count && shadowCaster.m_Priority >= list[num].m_Priority; num++)
				{
				}
				list.Insert(num, shadowCaster);
			}
		}

		public static void RemoveShadowCasterGroupFromList(global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D shadowCaster, global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D> list)
		{
			list.Remove(shadowCaster);
		}

		private static global::UnityEngine.Rendering.Universal.CompositeShadowCaster2D FindTopMostCompositeShadowCaster(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster)
		{
			global::UnityEngine.Rendering.Universal.CompositeShadowCaster2D result = null;
			global::UnityEngine.Transform parent = shadowCaster.transform.parent;
			while (parent != null)
			{
				if (parent.TryGetComponent<global::UnityEngine.Rendering.Universal.CompositeShadowCaster2D>(out var component))
				{
					result = component;
				}
				parent = parent.parent;
			}
			return result;
		}

		public static int GetRendereringPriority(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster)
		{
			int result = 0;
			if (shadowCaster.TryGetComponent<global::UnityEngine.Renderer>(out var component))
			{
				result = component.sortingOrder;
			}
			return result;
		}

		public static bool AddToShadowCasterGroup(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster, ref global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D shadowCasterGroup, ref int priority)
		{
			global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D component = FindTopMostCompositeShadowCaster(shadowCaster);
			int num = 0;
			if (component == null)
			{
				num = GetRendereringPriority(shadowCaster);
				shadowCaster.TryGetComponent<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D>(out component);
			}
			if (component != null && (shadowCasterGroup != component || priority != num))
			{
				component.RegisterShadowCaster2D(shadowCaster);
				shadowCasterGroup = component;
				priority = num;
				return true;
			}
			return false;
		}

		public static void RemoveFromShadowCasterGroup(global::UnityEngine.Rendering.Universal.ShadowCaster2D shadowCaster, global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D shadowCasterGroup)
		{
			if (shadowCasterGroup != null)
			{
				shadowCasterGroup.UnregisterShadowCaster2D(shadowCaster);
			}
			if (shadowCasterGroup == shadowCaster)
			{
				RemoveGroup(shadowCasterGroup);
			}
		}

		public static void AddGroup(global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D group)
		{
			if (!(group == null))
			{
				if (s_ShadowCasterGroups == null)
				{
					s_ShadowCasterGroups = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D>();
				}
				AddShadowCasterGroupToList(group, s_ShadowCasterGroups);
			}
		}

		public static void RemoveGroup(global::UnityEngine.Rendering.Universal.ShadowCasterGroup2D group)
		{
			if (group != null && s_ShadowCasterGroups != null)
			{
				RemoveShadowCasterGroupFromList(group, s_ShadowCasterGroups);
			}
		}
	}
}
