namespace Unity.VisualScripting
{
	public static class SavedVariables
	{
		public const string assetPath = "SavedVariables";

		public const string playerPrefsKey = "LudiqSavedVariables";

		private static global::Unity.VisualScripting.VariablesAsset _asset;

		public static global::Unity.VisualScripting.VariablesAsset asset
		{
			get
			{
				if (_asset == null)
				{
					Load();
				}
				return _asset;
			}
		}

		public static global::Unity.VisualScripting.VariableDeclarations initial => asset.declarations;

		public static global::Unity.VisualScripting.VariableDeclarations saved { get; private set; }

		public static global::Unity.VisualScripting.VariableDeclarations merged { get; private set; }

		public static global::Unity.VisualScripting.VariableDeclarations current
		{
			get
			{
				if (!global::UnityEngine.Application.isPlaying)
				{
					return initial;
				}
				return merged;
			}
		}

		public static void Load()
		{
			_asset = global::UnityEngine.Resources.Load<global::Unity.VisualScripting.VariablesAsset>("SavedVariables") ?? global::UnityEngine.ScriptableObject.CreateInstance<global::Unity.VisualScripting.VariablesAsset>();
		}

		public static void OnEnterEditMode()
		{
			FetchSavedDeclarations();
			DestroyMergedDeclarations();
		}

		public static void OnExitEditMode()
		{
			SaveDeclarations(saved);
		}

		internal static void OnEnterPlayMode()
		{
			FetchSavedDeclarations();
			MergeInitialAndSavedDeclarations();
			global::Unity.VisualScripting.VariableDeclarations variableDeclarations = merged;
			variableDeclarations.OnVariableChanged = (global::System.Action)global::System.Delegate.Combine(variableDeclarations.OnVariableChanged, (global::System.Action)delegate
			{
				if (global::Unity.VisualScripting.VariablesSaver.instance == null)
				{
					global::Unity.VisualScripting.VariablesSaver.Instantiate();
				}
			});
		}

		internal static void OnExitPlayMode()
		{
			SaveDeclarations(merged);
		}

		public static void SaveDeclarations(global::Unity.VisualScripting.VariableDeclarations declarations)
		{
			WarnAndNullifyUnityObjectReferences(declarations);
			try
			{
				global::Unity.VisualScripting.SerializationData serializationData = declarations.Serialize();
				if (serializationData.objectReferences.Length != 0)
				{
					throw new global::System.InvalidOperationException("Cannot use Unity object variable references in saved variables.");
				}
				global::UnityEngine.PlayerPrefs.SetString("LudiqSavedVariables", serializationData.json);
				global::UnityEngine.PlayerPrefs.Save();
			}
			catch (global::System.Exception arg)
			{
				global::UnityEngine.Debug.LogWarning($"Failed to save variables to player prefs: \n{arg}");
			}
		}

		public static void FetchSavedDeclarations()
		{
			if (global::UnityEngine.PlayerPrefs.HasKey("LudiqSavedVariables"))
			{
				try
				{
					saved = (global::Unity.VisualScripting.VariableDeclarations)new global::Unity.VisualScripting.SerializationData(global::UnityEngine.PlayerPrefs.GetString("LudiqSavedVariables")).Deserialize();
					return;
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch saved variables from player prefs: \n{arg}");
					saved = new global::Unity.VisualScripting.VariableDeclarations();
					return;
				}
			}
			saved = new global::Unity.VisualScripting.VariableDeclarations();
		}

		private static void MergeInitialAndSavedDeclarations()
		{
			merged = initial.CloneViaFakeSerialization();
			WarnAndNullifyUnityObjectReferences(merged);
			foreach (string item in global::System.Linq.Enumerable.Select(saved, (global::Unity.VisualScripting.VariableDeclaration vd) => vd.name))
			{
				if (!merged.IsDefined(item))
				{
					merged[item] = saved[item];
				}
				else if (merged[item] == null)
				{
					if (saved[item] == null || saved[item].GetType().IsNullable())
					{
						merged[item] = saved[item];
					}
					else
					{
						global::UnityEngine.Debug.LogWarning("Cannot convert saved player pref '" + item + "' to null.\n");
					}
				}
				else if (saved[item].IsConvertibleTo(merged[item].GetType(), guaranteed: true))
				{
					merged[item] = saved[item];
				}
				else
				{
					global::UnityEngine.Debug.LogWarning($"Cannot convert saved player pref '{item}' to expected type ({merged[item].GetType()}).\nReverting to initial value.");
				}
			}
		}

		private static void DestroyMergedDeclarations()
		{
			merged = null;
		}

		private static void WarnAndNullifyUnityObjectReferences(global::Unity.VisualScripting.VariableDeclarations declarations)
		{
			global::Unity.VisualScripting.Ensure.That("declarations").IsNotNull(declarations);
			foreach (global::Unity.VisualScripting.VariableDeclaration declaration in declarations)
			{
				if (declaration.value is global::UnityEngine.Object)
				{
					global::UnityEngine.Debug.LogWarning("Saved variable '" + declaration.name + "' refers to a Unity object. This is not supported. Its value will be null.");
					declarations[declaration.name] = null;
				}
			}
		}
	}
}
