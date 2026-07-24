namespace UnityEngine.U2D.Animation
{
	internal class SpriteSkinContainer : global::UnityEngine.ScriptableObject
	{
		private static global::UnityEngine.U2D.Animation.SpriteSkinContainer s_Instance;

		private global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin> m_SpriteSkin = new global::System.Collections.Generic.List<global::UnityEngine.U2D.Animation.SpriteSkin>();

		public static global::UnityEngine.U2D.Animation.SpriteSkinContainer instance
		{
			get
			{
				if (s_Instance == null)
				{
					global::UnityEngine.U2D.Animation.SpriteSkinContainer[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.U2D.Animation.SpriteSkinContainer>();
					if (array.Length != 0)
					{
						s_Instance = array[0];
					}
					else
					{
						s_Instance = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.U2D.Animation.SpriteSkinContainer>();
					}
					s_Instance.hideFlags = global::UnityEngine.HideFlags.HideAndDontSave;
				}
				return s_Instance;
			}
		}

		public global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.U2D.Animation.SpriteSkin> spriteSkins => m_SpriteSkin;

		public static event global::System.Action<global::UnityEngine.U2D.Animation.SpriteSkin> onAddedSpriteSkin;

		public static event global::System.Action<global::UnityEngine.U2D.Animation.SpriteSkin> onRemovedSpriteSkin;

		public static event global::System.Action<global::UnityEngine.U2D.Animation.SpriteSkin> onBoneTransformChanged;

		public void AddSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			m_SpriteSkin.Add(spriteSkin);
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onAddedSpriteSkin?.Invoke(spriteSkin);
		}

		public void RemoveSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			m_SpriteSkin.Remove(spriteSkin);
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onRemovedSpriteSkin?.Invoke(spriteSkin);
		}

		public void BoneTransformsChanged(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onBoneTransformChanged?.Invoke(spriteSkin);
		}
	}
}
