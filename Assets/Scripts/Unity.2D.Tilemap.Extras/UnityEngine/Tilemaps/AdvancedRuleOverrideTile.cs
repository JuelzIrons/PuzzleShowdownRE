namespace UnityEngine.Tilemaps
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(true, "UnityEngine", null, null)]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/RuleOverrideTile.html")]
	public class AdvancedRuleOverrideTile : global::UnityEngine.Tilemaps.RuleOverrideTile
	{
		public global::UnityEngine.Sprite m_DefaultSprite;

		public global::UnityEngine.GameObject m_DefaultGameObject;

		public global::UnityEngine.Tilemaps.Tile.ColliderType m_DefaultColliderType = global::UnityEngine.Tilemaps.Tile.ColliderType.Sprite;

		public global::System.Collections.Generic.List<global::UnityEngine.RuleTile.TilingRuleOutput> m_OverrideTilingRules = new global::System.Collections.Generic.List<global::UnityEngine.RuleTile.TilingRuleOutput>();

		public global::UnityEngine.RuleTile.TilingRuleOutput this[global::UnityEngine.RuleTile.TilingRule originalRule]
		{
			get
			{
				foreach (global::UnityEngine.RuleTile.TilingRuleOutput overrideTilingRule in m_OverrideTilingRules)
				{
					if (overrideTilingRule.m_Id == originalRule.m_Id)
					{
						return overrideTilingRule;
					}
				}
				return null;
			}
			set
			{
				for (int num = m_OverrideTilingRules.Count - 1; num >= 0; num--)
				{
					if (m_OverrideTilingRules[num].m_Id == originalRule.m_Id)
					{
						m_OverrideTilingRules.RemoveAt(num);
						break;
					}
				}
				if (value != null)
				{
					global::UnityEngine.RuleTile.TilingRuleOutput item = global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.RuleTile.TilingRuleOutput>(global::UnityEngine.JsonUtility.ToJson(value));
					m_OverrideTilingRules.Add(item);
				}
			}
		}

		public void ApplyOverrides(global::System.Collections.Generic.IList<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.RuleTile.TilingRule, global::UnityEngine.RuleTile.TilingRuleOutput>> overrides)
		{
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				this[overrides[i].Key] = overrides[i].Value;
			}
		}

		public void GetOverrides(global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<global::UnityEngine.RuleTile.TilingRule, global::UnityEngine.RuleTile.TilingRuleOutput>> overrides, ref int validCount)
		{
			if (overrides == null)
			{
				throw new global::System.ArgumentNullException("overrides");
			}
			overrides.Clear();
			if ((bool)m_Tile)
			{
				foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in m_Tile.m_TilingRules)
				{
					global::UnityEngine.RuleTile.TilingRuleOutput value = this[tilingRule];
					overrides.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.RuleTile.TilingRule, global::UnityEngine.RuleTile.TilingRuleOutput>(tilingRule, value));
				}
			}
			validCount = overrides.Count;
			foreach (global::UnityEngine.RuleTile.TilingRuleOutput overrideRule in m_OverrideTilingRules)
			{
				if (!overrides.Exists((global::System.Collections.Generic.KeyValuePair<global::UnityEngine.RuleTile.TilingRule, global::UnityEngine.RuleTile.TilingRuleOutput> o) => o.Key.m_Id == overrideRule.m_Id))
				{
					global::UnityEngine.RuleTile.TilingRule key = new global::UnityEngine.RuleTile.TilingRule
					{
						m_Id = overrideRule.m_Id
					};
					overrides.Add(new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.RuleTile.TilingRule, global::UnityEngine.RuleTile.TilingRuleOutput>(key, overrideRule));
				}
			}
		}

		public override void Override()
		{
			if (!m_Tile || !m_InstanceTile)
			{
				return;
			}
			PrepareOverride();
			global::UnityEngine.RuleTile instanceTile = m_InstanceTile;
			instanceTile.m_DefaultSprite = m_DefaultSprite;
			instanceTile.m_DefaultGameObject = m_DefaultGameObject;
			instanceTile.m_DefaultColliderType = m_DefaultColliderType;
			foreach (global::UnityEngine.RuleTile.TilingRule tilingRule in instanceTile.m_TilingRules)
			{
				global::UnityEngine.RuleTile.TilingRuleOutput tilingRuleOutput = this[tilingRule];
				if (tilingRuleOutput != null)
				{
					global::UnityEngine.JsonUtility.FromJsonOverwrite(global::UnityEngine.JsonUtility.ToJson(tilingRuleOutput), tilingRule);
				}
			}
		}
	}
}
