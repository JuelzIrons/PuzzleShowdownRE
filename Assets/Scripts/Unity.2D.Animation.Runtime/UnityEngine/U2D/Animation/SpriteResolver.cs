namespace UnityEngine.U2D.Animation
{
	[global::UnityEngine.ExecuteInEditMode]
	[global::UnityEngine.DisallowMultipleComponent]
	[global::UnityEngine.AddComponentMenu("2D Animation/Sprite Resolver")]
	[global::UnityEngine.DefaultExecutionOrder(-20)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.animation@latest/index.html?subfolder=/manual/SL-Resolver.html")]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom("UnityEngine.Experimental.U2D.Animation")]
	public class SpriteResolver : global::UnityEngine.MonoBehaviour, global::UnityEngine.U2D.Common.IPreviewable, global::UnityEngine.Animations.IAnimationPreviewable
	{
		[global::UnityEngine.SerializeField]
		private float m_CategoryHash;

		[global::UnityEngine.SerializeField]
		private float m_labelHash;

		[global::UnityEngine.SerializeField]
		private float m_SpriteKey;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Animations.DiscreteEvaluation]
		private int m_SpriteHash;

		private int m_CategoryHashInt;

		private int m_LabelHashInt;

		private int m_PreviousCategoryHash;

		private int m_PreviousLabelHash;

		private int m_PreviousSpriteKeyInt;

		private int m_PreviousSpriteHash;

		private global::UnityEngine.SpriteRenderer spriteRenderer => GetComponent<global::UnityEngine.SpriteRenderer>();

		public global::UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary => base.gameObject.GetComponentInParent<global::UnityEngine.U2D.Animation.SpriteLibrary>(includeInactive: true);

		private void Reset()
		{
			if ((bool)spriteRenderer)
			{
				SetSprite(spriteRenderer.sprite);
			}
		}

		private void SetSprite(global::UnityEngine.Sprite sprite)
		{
			global::UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary = this.spriteLibrary;
			if (!(spriteLibrary != null) || !(sprite != null))
			{
				return;
			}
			foreach (string categoryName in spriteLibrary.categoryNames)
			{
				foreach (string entryName in spriteLibrary.GetEntryNames(categoryName))
				{
					if (spriteLibrary.GetSprite(categoryName, entryName) == sprite)
					{
						m_SpriteHash = global::UnityEngine.U2D.Animation.SpriteLibrary.GetHashForCategoryAndEntry(categoryName, entryName);
						return;
					}
				}
			}
		}

		private void OnEnable()
		{
			InitializeSerializedData();
			ResolveSpriteToSpriteRenderer();
		}

		private void InitializeSerializedData()
		{
			m_CategoryHashInt = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_CategoryHash);
			m_LabelHashInt = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_labelHash);
			m_PreviousSpriteKeyInt = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.Convert32BitTo30BitHash(global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_SpriteKey));
			m_SpriteKey = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertIntToFloat(m_PreviousSpriteKeyInt);
			if (m_SpriteHash == 0)
			{
				if (m_SpriteKey != 0f)
				{
					m_SpriteHash = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_SpriteKey);
				}
				else
				{
					m_SpriteHash = ConvertCategoryLabelHashToSpriteKey(spriteLibrary, global::UnityEngine.U2D.Animation.SpriteLibraryUtility.Convert32BitTo30BitHash(m_CategoryHashInt), global::UnityEngine.U2D.Animation.SpriteLibraryUtility.Convert32BitTo30BitHash(m_LabelHashInt));
				}
			}
			m_PreviousSpriteHash = m_SpriteHash;
			if (spriteLibrary != null && spriteLibrary.GetCategoryAndEntryNameFromHash(m_SpriteHash, out var category, out var entry))
			{
				m_CategoryHashInt = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(category);
				m_LabelHashInt = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(entry);
				m_CategoryHash = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertIntToFloat(m_CategoryHashInt);
				m_labelHash = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertIntToFloat(m_LabelHashInt);
			}
			m_PreviousLabelHash = m_LabelHashInt;
			m_PreviousCategoryHash = m_CategoryHashInt;
		}

		public bool SetCategoryAndLabel(string category, string label)
		{
			m_SpriteHash = global::UnityEngine.U2D.Animation.SpriteLibrary.GetHashForCategoryAndEntry(category, label);
			m_PreviousSpriteHash = m_SpriteHash;
			return ResolveSpriteToSpriteRenderer();
		}

		public string GetCategory()
		{
			string category = "";
			global::UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary = this.spriteLibrary;
			if ((bool)spriteLibrary)
			{
				spriteLibrary.GetCategoryAndEntryNameFromHash(m_SpriteHash, out category, out var _);
			}
			return category;
		}

		public string GetLabel()
		{
			string entry = "";
			global::UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary = this.spriteLibrary;
			if ((bool)spriteLibrary)
			{
				spriteLibrary.GetCategoryAndEntryNameFromHash(m_SpriteHash, out var _, out entry);
			}
			return entry;
		}

		public void OnPreviewUpdate()
		{
		}

		private static bool IsInGUIUpdateLoop()
		{
			return global::UnityEngine.Event.current != null;
		}

		internal void LateUpdate()
		{
			ResolveUpdatedValue();
		}

		private void ResolveUpdatedValue()
		{
			if (m_SpriteHash != m_PreviousSpriteHash)
			{
				m_PreviousSpriteHash = m_SpriteHash;
				ResolveSpriteToSpriteRenderer();
				return;
			}
			int num = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_SpriteKey);
			if (num != m_PreviousSpriteKeyInt)
			{
				m_SpriteHash = global::UnityEngine.U2D.Animation.SpriteLibraryUtility.Convert32BitTo30BitHash(num);
				m_PreviousSpriteKeyInt = num;
				ResolveSpriteToSpriteRenderer();
				return;
			}
			m_CategoryHashInt = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_CategoryHash);
			m_LabelHashInt = global::UnityEngine.U2D.Common.InternalEngineBridge.ConvertFloatToInt(m_labelHash);
			if ((m_LabelHashInt != m_PreviousLabelHash || m_CategoryHashInt != m_PreviousCategoryHash) && spriteLibrary != null)
			{
				m_PreviousCategoryHash = m_CategoryHashInt;
				m_PreviousLabelHash = m_LabelHashInt;
				m_SpriteHash = ConvertCategoryLabelHashToSpriteKey(spriteLibrary, global::UnityEngine.U2D.Animation.SpriteLibraryUtility.Convert32BitTo30BitHash(m_CategoryHashInt), global::UnityEngine.U2D.Animation.SpriteLibraryUtility.Convert32BitTo30BitHash(m_LabelHashInt));
				m_PreviousSpriteHash = m_SpriteHash;
				ResolveSpriteToSpriteRenderer();
			}
		}

		internal static int ConvertCategoryLabelHashToSpriteKey(global::UnityEngine.U2D.Animation.SpriteLibrary library, int categoryHash, int labelHash)
		{
			if (library != null)
			{
				foreach (string categoryName in library.categoryNames)
				{
					if (categoryHash != global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(categoryName))
					{
						continue;
					}
					global::System.Collections.Generic.IEnumerable<string> entryNames = library.GetEntryNames(categoryName);
					if (entryNames == null)
					{
						continue;
					}
					foreach (string item in entryNames)
					{
						if (labelHash == global::UnityEngine.U2D.Animation.SpriteLibraryUtility.GetStringHash(item))
						{
							return global::UnityEngine.U2D.Animation.SpriteLibrary.GetHashForCategoryAndEntry(categoryName, item);
						}
					}
				}
			}
			return 0;
		}

		internal global::UnityEngine.Sprite GetSprite(out bool validEntry)
		{
			global::UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary = this.spriteLibrary;
			if (spriteLibrary != null)
			{
				return spriteLibrary.GetSpriteFromCategoryAndEntryHash(m_SpriteHash, out validEntry);
			}
			validEntry = false;
			return null;
		}

		public bool ResolveSpriteToSpriteRenderer()
		{
			m_PreviousSpriteHash = m_SpriteHash;
			bool validEntry;
			global::UnityEngine.Sprite sprite = GetSprite(out validEntry);
			global::UnityEngine.SpriteRenderer spriteRenderer = this.spriteRenderer;
			if (spriteRenderer != null && (sprite != null || validEntry))
			{
				spriteRenderer.sprite = sprite;
			}
			return validEntry;
		}

		private void OnTransformParentChanged()
		{
			ResolveSpriteToSpriteRenderer();
		}
	}
}
