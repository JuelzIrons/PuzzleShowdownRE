namespace UnityEngine.Rendering
{
	public sealed class VolumeManager
	{
		private static readonly global::Unity.Profiling.ProfilerMarker k_ProfilerMarkerUpdate = new global::Unity.Profiling.ProfilerMarker("VolumeManager.Update");

		private static readonly global::Unity.Profiling.ProfilerMarker k_ProfilerMarkerReplaceData = new global::Unity.Profiling.ProfilerMarker("VolumeManager.ReplaceData");

		private static readonly global::Unity.Profiling.ProfilerMarker k_ProfilerMarkerEvaluateVolumeDefaultState = new global::Unity.Profiling.ProfilerMarker("VolumeManager.EvaluateVolumeDefaultState");

		private static readonly global::System.Lazy<global::UnityEngine.Rendering.VolumeManager> s_Instance = new global::System.Lazy<global::UnityEngine.Rendering.VolumeManager>(() => new global::UnityEngine.Rendering.VolumeManager());

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.List<(string, global::System.Type)>> s_SupportedVolumeComponentsForRenderPipeline = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Collections.Generic.List<(string, global::System.Type)>>();

		private global::System.Type[] m_BaseComponentTypeArray;

		private readonly global::UnityEngine.Rendering.VolumeCollection m_VolumeCollection = new global::UnityEngine.Rendering.VolumeCollection();

		private global::UnityEngine.Rendering.VolumeComponent[] m_ComponentsDefaultState;

		internal global::UnityEngine.Rendering.VolumeParameter[] m_ParametersDefaultState;

		private global::UnityEngine.Rendering.VolumeStack m_DefaultStack;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeStack> m_CreatedVolumeStacks = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeStack>();

		public static global::UnityEngine.Rendering.VolumeManager instance => s_Instance.Value;

		public global::UnityEngine.Rendering.VolumeStack stack { get; set; }

		[global::System.Obsolete("Please use baseComponentTypeArray instead. #from(2021.2)")]
		public global::System.Collections.Generic.IEnumerable<global::System.Type> baseComponentTypes => baseComponentTypeArray;

		public global::System.Type[] baseComponentTypeArray
		{
			get
			{
				if (isInitialized)
				{
					return m_BaseComponentTypeArray;
				}
				throw new global::System.InvalidOperationException("VolumeManager.instance.baseComponentTypeArray cannot be called before the VolumeManager is initialized. (See VolumeManager.instance.isInitialized and RenderPipelineManager for creation callback).");
			}
			internal set
			{
				m_BaseComponentTypeArray = value;
			}
		}

		public global::UnityEngine.Rendering.VolumeProfile globalDefaultProfile { get; private set; }

		public global::UnityEngine.Rendering.VolumeProfile qualityDefaultProfile { get; private set; }

		public global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeProfile> customDefaultProfiles { get; private set; }

		public bool isInitialized { get; private set; }

		[global::System.Obsolete("Please use the Register without a given layer index. #from(6000.0)")]
		public void Register(global::UnityEngine.Rendering.Volume volume, int layer)
		{
			if (volume.gameObject.layer != layer)
			{
				global::UnityEngine.Debug.LogWarning($"Trying to register Volume {volume.name} on layer index {layer}, when the GameObject {volume.gameObject.name} is on layer index {volume.gameObject.layer}." + global::System.Environment.NewLine + "The Volume Manager will respect the GameObject's layer.");
			}
			Register(volume);
		}

		[global::System.Obsolete("Please use the Register without a given layer index. #from(6000.0)")]
		public void Unregister(global::UnityEngine.Rendering.Volume volume, int layer)
		{
			if (volume.gameObject.layer != layer)
			{
				global::UnityEngine.Debug.LogWarning($"Trying to unregister Volume {volume.name} on layer index {layer}, when the GameObject {volume.gameObject.name} is on layer index {volume.gameObject.layer}." + global::System.Environment.NewLine + "The Volume Manager will respect the GameObject's layer.");
			}
			Unregister(volume);
		}

		internal global::System.Collections.Generic.List<(string, global::System.Type)> GetVolumeComponentsForDisplay(global::System.Type currentPipelineAssetType)
		{
			if (currentPipelineAssetType == null)
			{
				return new global::System.Collections.Generic.List<(string, global::System.Type)>();
			}
			if (!currentPipelineAssetType.IsSubclassOf(typeof(global::UnityEngine.Rendering.RenderPipelineAsset)))
			{
				throw new global::System.ArgumentException("currentPipelineAssetType");
			}
			if (s_SupportedVolumeComponentsForRenderPipeline.TryGetValue(currentPipelineAssetType, out var value))
			{
				return value;
			}
			if (baseComponentTypeArray == null)
			{
				LoadBaseTypes(currentPipelineAssetType);
			}
			value = BuildVolumeComponentDisplayList(baseComponentTypeArray);
			s_SupportedVolumeComponentsForRenderPipeline[currentPipelineAssetType] = value;
			return value;
		}

		private global::System.Collections.Generic.List<(string, global::System.Type)> BuildVolumeComponentDisplayList(global::System.Type[] types)
		{
			if (types == null)
			{
				throw new global::System.ArgumentNullException("types");
			}
			global::System.Collections.Generic.List<(string, global::System.Type)> list = new global::System.Collections.Generic.List<(string, global::System.Type)>();
			foreach (global::System.Type type in types)
			{
				string text = string.Empty;
				bool flag = false;
				object[] customAttributes = type.GetCustomAttributes(inherit: false);
				foreach (object obj in customAttributes)
				{
					if (!(obj is global::UnityEngine.Rendering.VolumeComponentMenu volumeComponentMenu))
					{
						if (obj is global::UnityEngine.HideInInspector || obj is global::System.ObsoleteAttribute)
						{
							flag = true;
						}
					}
					else
					{
						text = volumeComponentMenu.menu;
					}
				}
				if (!flag)
				{
					if (string.IsNullOrEmpty(text))
					{
						text = type.Name;
					}
					list.Add((text, type));
				}
			}
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(list, ((string, global::System.Type) tuple) => tuple.Item1));
		}

		public global::UnityEngine.Rendering.VolumeComponent GetVolumeComponentDefaultState(global::System.Type volumeComponentType)
		{
			if (!typeof(global::UnityEngine.Rendering.VolumeComponent).IsAssignableFrom(volumeComponentType))
			{
				return null;
			}
			global::UnityEngine.Rendering.VolumeComponent[] componentsDefaultState = m_ComponentsDefaultState;
			foreach (global::UnityEngine.Rendering.VolumeComponent volumeComponent in componentsDefaultState)
			{
				if (volumeComponent.GetType() == volumeComponentType)
				{
					return volumeComponent;
				}
			}
			return null;
		}

		internal VolumeManager()
		{
		}

		public void Initialize(global::UnityEngine.Rendering.VolumeProfile globalDefaultVolumeProfile = null, global::UnityEngine.Rendering.VolumeProfile qualityDefaultVolumeProfile = null)
		{
			LoadBaseTypes(global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipelineAssetType);
			InitializeInternal(globalDefaultVolumeProfile, qualityDefaultVolumeProfile);
		}

		internal void InitializeInternal(global::UnityEngine.Rendering.VolumeProfile globalDefaultVolumeProfile = null, global::UnityEngine.Rendering.VolumeProfile qualityDefaultVolumeProfile = null)
		{
			InitializeVolumeComponents();
			globalDefaultProfile = globalDefaultVolumeProfile;
			qualityDefaultProfile = qualityDefaultVolumeProfile;
			EvaluateVolumeDefaultState();
			m_DefaultStack = CreateStackInternal();
			stack = m_DefaultStack;
			isInitialized = true;
		}

		public void Deinitialize()
		{
			DestroyStack(m_DefaultStack);
			m_DefaultStack = null;
			foreach (global::UnityEngine.Rendering.VolumeStack createdVolumeStack in m_CreatedVolumeStacks)
			{
				createdVolumeStack.Dispose();
			}
			m_CreatedVolumeStacks.Clear();
			baseComponentTypeArray = null;
			globalDefaultProfile = null;
			qualityDefaultProfile = null;
			customDefaultProfiles = null;
			isInitialized = false;
		}

		public void SetGlobalDefaultProfile(global::UnityEngine.Rendering.VolumeProfile profile)
		{
			globalDefaultProfile = profile;
			EvaluateVolumeDefaultState();
		}

		public void SetQualityDefaultProfile(global::UnityEngine.Rendering.VolumeProfile profile)
		{
			qualityDefaultProfile = profile;
			EvaluateVolumeDefaultState();
		}

		public void SetCustomDefaultProfiles(global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeProfile> profiles)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeProfile> list = profiles ?? new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeProfile>();
			list.RemoveAll((global::UnityEngine.Rendering.VolumeProfile x) => x == null);
			customDefaultProfiles = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeProfile>(list);
			EvaluateVolumeDefaultState();
		}

		public void OnVolumeProfileChanged(global::UnityEngine.Rendering.VolumeProfile profile)
		{
			if (isInitialized && (globalDefaultProfile == profile || qualityDefaultProfile == profile || (customDefaultProfiles != null && customDefaultProfiles.Contains(profile))))
			{
				EvaluateVolumeDefaultState();
			}
		}

		public void OnVolumeComponentChanged(global::UnityEngine.Rendering.VolumeComponent component)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeProfile> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeProfile> { globalDefaultProfile, globalDefaultProfile };
			if (customDefaultProfiles != null)
			{
				list.AddRange(customDefaultProfiles);
			}
			foreach (global::UnityEngine.Rendering.VolumeProfile item in list)
			{
				if (item.components.Contains(component))
				{
					EvaluateVolumeDefaultState();
					break;
				}
			}
		}

		public global::UnityEngine.Rendering.VolumeStack CreateStack()
		{
			if (!isInitialized)
			{
				throw new global::System.InvalidOperationException("VolumeManager.instance.CreateStack() cannot be called before the VolumeManager is initialized. (See VolumeManager.instance.isInitialized and RenderPipelineManager for creation callback).");
			}
			return CreateStackInternal();
		}

		private global::UnityEngine.Rendering.VolumeStack CreateStackInternal()
		{
			global::UnityEngine.Rendering.VolumeStack volumeStack = new global::UnityEngine.Rendering.VolumeStack();
			volumeStack.Reload(m_BaseComponentTypeArray);
			m_CreatedVolumeStacks.Add(volumeStack);
			return volumeStack;
		}

		public void ResetMainStack()
		{
			stack = m_DefaultStack;
		}

		public void DestroyStack(global::UnityEngine.Rendering.VolumeStack stack)
		{
			m_CreatedVolumeStacks.Remove(stack);
			stack.Dispose();
		}

		private bool IsSupportedByObsoleteVolumeComponentMenuForRenderPipeline(global::System.Type t, global::System.Type pipelineAssetType)
		{
			bool result = false;
			if (global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.Rendering.VolumeComponentMenuForRenderPipeline>(t) != null)
			{
				global::UnityEngine.Debug.LogWarning(string.Format("{0} is deprecated, use {1} and {2} with {3} instead. #from(2023.1)", "VolumeComponentMenuForRenderPipeline", "SupportedOnRenderPipelineAttribute", "VolumeComponentMenu", t));
			}
			return result;
		}

		internal void LoadBaseTypes(global::System.Type pipelineAssetType)
		{
			global::System.Collections.Generic.List<global::System.Type> value;
			using (global::UnityEngine.Rendering.ListPool<global::System.Type>.Get(out value))
			{
				foreach (global::System.Type item in global::UnityEngine.Rendering.CoreUtils.GetAllTypesDerivedFrom<global::UnityEngine.Rendering.VolumeComponent>())
				{
					if (!item.IsAbstract && (global::UnityEngine.Rendering.SupportedOnRenderPipelineAttribute.IsTypeSupportedOnRenderPipeline(item, pipelineAssetType) || IsSupportedByObsoleteVolumeComponentMenuForRenderPipeline(item, pipelineAssetType)))
					{
						value.Add(item);
					}
				}
				m_BaseComponentTypeArray = value.ToArray();
			}
		}

		internal void InitializeVolumeComponents()
		{
			if (m_BaseComponentTypeArray == null || m_BaseComponentTypeArray.Length == 0)
			{
				return;
			}
			global::System.Reflection.BindingFlags bindingAttr = global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic;
			global::System.Type[] array = m_BaseComponentTypeArray;
			for (int i = 0; i < array.Length; i++)
			{
				global::System.Reflection.MethodInfo method = array[i].GetMethod("Init", bindingAttr);
				if (method != null)
				{
					method.Invoke(null, null);
				}
			}
		}

		private void EvaluateVolumeDefaultState()
		{
			if (m_BaseComponentTypeArray == null || m_BaseComponentTypeArray.Length == 0)
			{
				return;
			}
			using (k_ProfilerMarkerEvaluateVolumeDefaultState.Auto())
			{
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeComponent> componentsDefaultStateList = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeComponent>();
				global::System.Type[] array = m_BaseComponentTypeArray;
				foreach (global::System.Type type in array)
				{
					componentsDefaultStateList.Add((global::UnityEngine.Rendering.VolumeComponent)global::UnityEngine.ScriptableObject.CreateInstance(type));
				}
				ApplyDefaultProfile(globalDefaultProfile);
				ApplyDefaultProfile(qualityDefaultProfile);
				if (customDefaultProfiles != null)
				{
					foreach (global::UnityEngine.Rendering.VolumeProfile customDefaultProfile in customDefaultProfiles)
					{
						ApplyDefaultProfile(customDefaultProfile);
					}
				}
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeParameter> list = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeParameter>();
				foreach (global::UnityEngine.Rendering.VolumeComponent item in componentsDefaultStateList)
				{
					list.AddRange(item.parameters);
				}
				m_ComponentsDefaultState = componentsDefaultStateList.ToArray();
				m_ParametersDefaultState = list.ToArray();
				foreach (global::UnityEngine.Rendering.VolumeStack createdVolumeStack in m_CreatedVolumeStacks)
				{
					createdVolumeStack.requiresReset = true;
					createdVolumeStack.requiresResetForAllProperties = true;
				}
				void ApplyDefaultProfile(global::UnityEngine.Rendering.VolumeProfile profile)
				{
					if (!(profile == null))
					{
						for (int j = 0; j < profile.components.Count; j++)
						{
							global::UnityEngine.Rendering.VolumeComponent profileComponent = profile.components[j];
							global::UnityEngine.Rendering.VolumeComponent volumeComponent = global::System.Linq.Enumerable.FirstOrDefault(componentsDefaultStateList, (global::UnityEngine.Rendering.VolumeComponent x) => x.GetType() == profileComponent.GetType());
							if (volumeComponent != null && profileComponent.active)
							{
								profileComponent.Override(volumeComponent, 1f);
							}
						}
					}
				}
			}
		}

		public void Register(global::UnityEngine.Rendering.Volume volume)
		{
			m_VolumeCollection.Register(volume, volume.gameObject.layer);
		}

		public void Unregister(global::UnityEngine.Rendering.Volume volume)
		{
			m_VolumeCollection.Unregister(volume, volume.gameObject.layer);
		}

		public bool IsComponentActiveInMask<T>(global::UnityEngine.LayerMask layerMask) where T : global::UnityEngine.Rendering.VolumeComponent
		{
			return m_VolumeCollection.IsComponentActiveInMask<T>(layerMask);
		}

		internal void SetLayerDirty(int layer)
		{
			m_VolumeCollection.SetLayerIndexDirty(layer);
		}

		internal void UpdateVolumeLayer(global::UnityEngine.Rendering.Volume volume, int prevLayer, int newLayer)
		{
			m_VolumeCollection.ChangeLayer(volume, prevLayer, newLayer);
		}

		private void OverrideData(global::UnityEngine.Rendering.VolumeStack stack, global::UnityEngine.Rendering.Volume volume, float interpFactor)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeComponent> components = volume.profileRef.components;
			int count = components.Count;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.Rendering.VolumeComponent volumeComponent = components[i];
				if (volumeComponent.active)
				{
					global::UnityEngine.Rendering.VolumeComponent component = stack.GetComponent(volumeComponent.GetType());
					if (component != null)
					{
						volumeComponent.Override(component, interpFactor);
					}
				}
			}
		}

		internal void ReplaceData(global::UnityEngine.Rendering.VolumeStack stack)
		{
			using (k_ProfilerMarkerReplaceData.Auto())
			{
				global::UnityEngine.Rendering.VolumeParameter[] parameters = stack.parameters;
				bool requiresResetForAllProperties = stack.requiresResetForAllProperties;
				int num = parameters.Length;
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.Rendering.VolumeParameter volumeParameter = parameters[i];
					if (volumeParameter.overrideState || requiresResetForAllProperties)
					{
						volumeParameter.overrideState = false;
						volumeParameter.SetValue(m_ParametersDefaultState[i]);
					}
				}
				stack.requiresResetForAllProperties = false;
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public void CheckDefaultVolumeState()
		{
			if (m_ComponentsDefaultState == null || (m_ComponentsDefaultState.Length != 0 && m_ComponentsDefaultState[0] == null))
			{
				EvaluateVolumeDefaultState();
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public void CheckStack(global::UnityEngine.Rendering.VolumeStack stack)
		{
			if (stack.components == null)
			{
				stack.Reload(baseComponentTypeArray);
				return;
			}
			foreach (global::System.Collections.Generic.KeyValuePair<global::System.Type, global::UnityEngine.Rendering.VolumeComponent> component in stack.components)
			{
				if (component.Key == null || component.Value == null)
				{
					stack.Reload(baseComponentTypeArray);
					break;
				}
			}
		}

		private bool CheckUpdateRequired(global::UnityEngine.Rendering.VolumeStack stack)
		{
			if (m_VolumeCollection.count == 0)
			{
				if (stack.requiresReset)
				{
					stack.requiresReset = false;
					return true;
				}
				return false;
			}
			stack.requiresReset = true;
			return true;
		}

		public void Update(global::UnityEngine.Transform trigger, global::UnityEngine.LayerMask layerMask)
		{
			Update(stack, trigger, layerMask);
		}

		public void Update(global::UnityEngine.Rendering.VolumeStack stack, global::UnityEngine.Transform trigger, global::UnityEngine.LayerMask layerMask)
		{
			using (k_ProfilerMarkerUpdate.Auto())
			{
				if (!isInitialized || !CheckUpdateRequired(stack))
				{
					return;
				}
				ReplaceData(stack);
				bool flag = trigger == null;
				global::UnityEngine.Vector3 vector = (flag ? global::UnityEngine.Vector3.zero : trigger.position);
				global::System.Collections.Generic.List<global::UnityEngine.Rendering.Volume> list = GrabVolumes(layerMask);
				global::UnityEngine.Camera component = null;
				if (!flag)
				{
					trigger.TryGetComponent<global::UnityEngine.Camera>(out component);
				}
				int count = list.Count;
				for (int i = 0; i < count; i++)
				{
					global::UnityEngine.Rendering.Volume volume = list[i];
					if (volume == null || !volume.enabled || volume.profileRef == null || volume.weight <= 0f)
					{
						continue;
					}
					if (volume.isGlobal)
					{
						OverrideData(stack, volume, global::UnityEngine.Mathf.Clamp01(volume.weight));
					}
					else
					{
						if (flag)
						{
							continue;
						}
						global::System.Collections.Generic.List<global::UnityEngine.Collider> colliders = volume.colliders;
						int count2 = colliders.Count;
						if (count2 == 0)
						{
							continue;
						}
						float num = float.PositiveInfinity;
						for (int j = 0; j < count2; j++)
						{
							global::UnityEngine.Collider collider = colliders[j];
							if (collider.enabled)
							{
								float sqrMagnitude = (collider.ClosestPoint(vector) - vector).sqrMagnitude;
								if (sqrMagnitude < num)
								{
									num = sqrMagnitude;
								}
							}
						}
						float num2 = volume.blendDistance * volume.blendDistance;
						if (!(num > num2))
						{
							float num3 = 1f;
							if (num2 > 0f)
							{
								num3 = 1f - num / num2;
							}
							OverrideData(stack, volume, num3 * global::UnityEngine.Mathf.Clamp01(volume.weight));
						}
					}
				}
			}
		}

		public global::UnityEngine.Rendering.Volume[] GetVolumes(global::UnityEngine.LayerMask layerMask)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.Volume> list = GrabVolumes(layerMask);
			list.RemoveAll((global::UnityEngine.Rendering.Volume v) => v == null);
			return list.ToArray();
		}

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.Volume> GrabVolumes(global::UnityEngine.LayerMask mask)
		{
			return m_VolumeCollection.GrabVolumes(mask);
		}

		private static bool IsVolumeRenderedByCamera(global::UnityEngine.Rendering.Volume volume, global::UnityEngine.Camera camera)
		{
			return true;
		}
	}
}
