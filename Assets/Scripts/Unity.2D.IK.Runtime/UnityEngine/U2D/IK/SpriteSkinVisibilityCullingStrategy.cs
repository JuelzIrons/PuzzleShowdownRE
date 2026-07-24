namespace UnityEngine.U2D.IK
{
	internal class SpriteSkinVisibilityCullingStrategy : global::UnityEngine.U2D.IK.BaseCullingStrategy
	{
		internal class SpriteSkinRegistry
		{
			public int[] boneIds;

			public bool isVisible;

			public SpriteSkinRegistry(int[] boneIds, bool isSkinVisible)
			{
				this.boneIds = boneIds;
				isVisible = isSkinVisible;
			}
		}

		private global::System.Collections.Generic.Dictionary<global::UnityEngine.U2D.Animation.SpriteSkin, global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry> m_SpriteSkinRegistries;

		private global::System.Collections.Generic.Dictionary<int, int> m_BoneVisibilityCount;

		public override bool AreBonesVisible(global::System.Collections.Generic.IList<int> boneTransformIds)
		{
			for (int i = 0; i < boneTransformIds.Count; i++)
			{
				int key = boneTransformIds[i];
				if (m_BoneVisibilityCount.ContainsKey(key))
				{
					return m_BoneVisibilityCount[key] > 0;
				}
			}
			return false;
		}

		protected override void OnInitialize()
		{
			m_SpriteSkinRegistries = new global::System.Collections.Generic.Dictionary<global::UnityEngine.U2D.Animation.SpriteSkin, global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry>();
			m_BoneVisibilityCount = new global::System.Collections.Generic.Dictionary<int, int>();
			global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.U2D.Animation.SpriteSkin> spriteSkins = global::UnityEngine.U2D.Animation.SpriteSkinContainer.instance.spriteSkins;
			for (int i = 0; i < spriteSkins.Count; i++)
			{
				UpdateSpriteSkinVisibility(spriteSkins[i]);
			}
			AddListeners();
		}

		protected override void OnDisable()
		{
			m_SpriteSkinRegistries.Clear();
			m_BoneVisibilityCount.Clear();
			RemoveListeners();
		}

		private void AddListeners()
		{
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onAddedSpriteSkin += UpdateSpriteSkinVisibility;
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onRemovedSpriteSkin += UnregisterSpriteSkin;
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onBoneTransformChanged += OnBoneTransformChanged;
		}

		private void RemoveListeners()
		{
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onAddedSpriteSkin -= UpdateSpriteSkinVisibility;
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onRemovedSpriteSkin -= UnregisterSpriteSkin;
			global::UnityEngine.U2D.Animation.SpriteSkinContainer.onBoneTransformChanged -= OnBoneTransformChanged;
		}

		protected override void OnUpdate()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.U2D.Animation.SpriteSkin, global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry> spriteSkinRegistry2 in m_SpriteSkinRegistries)
			{
				spriteSkinRegistry2.Deconstruct(out var key, out var value);
				global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin = key;
				global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry spriteSkinRegistry = value;
				bool isVisible = spriteSkin.spriteRenderer.isVisible;
				if (spriteSkinRegistry.isVisible != isVisible)
				{
					spriteSkinRegistry.isVisible = isVisible;
					RecalculateVisibility(spriteSkinRegistry);
				}
			}
		}

		private void OnBoneTransformChanged(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			UnregisterSpriteSkinBonesMapping(spriteSkin);
			RegisterSpriteSkinBonesMapping(spriteSkin);
			UpdateSpriteSkinVisibility(spriteSkin);
		}

		private bool IsSpriteSkinRegistered(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			return m_SpriteSkinRegistries.ContainsKey(spriteSkin);
		}

		private void UnregisterSpriteSkin(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			UnregisterSpriteSkinBonesMapping(spriteSkin);
		}

		private void UpdateSpriteSkinVisibility(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			bool isVisible = spriteSkin.spriteRenderer.isVisible;
			global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry spriteSkinRegistry = RegisterSpriteSkinBonesMapping(spriteSkin);
			if (spriteSkinRegistry.isVisible != isVisible)
			{
				spriteSkinRegistry.isVisible = isVisible;
				RecalculateVisibility(spriteSkinRegistry);
			}
		}

		private global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry RegisterSpriteSkinBonesMapping(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (IsSpriteSkinRegistered(spriteSkin))
			{
				return m_SpriteSkinRegistries[spriteSkin];
			}
			global::UnityEngine.Transform[] array = spriteSkin.boneTransforms ?? global::System.Array.Empty<global::UnityEngine.Transform>();
			int[] array2 = new int[array.Length];
			global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry spriteSkinRegistry = new global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry(array2, isSkinVisible: false);
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.Transform transform = array[i];
				if (!(transform == null))
				{
					int instanceID = transform.GetInstanceID();
					array2[i] = instanceID;
				}
			}
			m_SpriteSkinRegistries[spriteSkin] = spriteSkinRegistry;
			return spriteSkinRegistry;
		}

		private void UnregisterSpriteSkinBonesMapping(global::UnityEngine.U2D.Animation.SpriteSkin spriteSkin)
		{
			if (IsSpriteSkinRegistered(spriteSkin))
			{
				global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry spriteSkinRegistry = m_SpriteSkinRegistries[spriteSkin];
				spriteSkinRegistry.isVisible = false;
				m_SpriteSkinRegistries.Remove(spriteSkin);
				RecalculateVisibility(spriteSkinRegistry);
			}
		}

		private void RecalculateVisibility(global::UnityEngine.U2D.IK.SpriteSkinVisibilityCullingStrategy.SpriteSkinRegistry registry)
		{
			int[] boneIds = registry.boneIds;
			bool isVisible = registry.isVisible;
			int num = (isVisible ? 1 : (-1));
			foreach (int key in boneIds)
			{
				if (m_BoneVisibilityCount.ContainsKey(key))
				{
					int num2 = m_BoneVisibilityCount[key] + num;
					if (num2 <= 0)
					{
						m_BoneVisibilityCount.Remove(key);
					}
					else
					{
						m_BoneVisibilityCount[key] = num2;
					}
				}
				else if (isVisible)
				{
					m_BoneVisibilityCount[key] = 1;
				}
			}
		}
	}
}
