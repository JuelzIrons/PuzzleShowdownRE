namespace UnityEngine.Rendering
{
	[global::System.Obsolete("This is not longer supported Please use DebugDisplaySettingsVolume. #from(6000.2)")]
	public abstract class VolumeDebugSettings<T> : global::UnityEngine.Rendering.IVolumeDebugSettings where T : global::UnityEngine.MonoBehaviour, global::UnityEngine.Rendering.IAdditionalData
	{
		protected int m_SelectedCameraIndex = -1;

		private global::UnityEngine.Camera[] m_CamerasArray;

		private global::System.Collections.Generic.List<global::UnityEngine.Camera> m_Cameras = new global::System.Collections.Generic.List<global::UnityEngine.Camera>();

		private float[] weights;

		private global::UnityEngine.Rendering.Volume[] volumes;

		private global::UnityEngine.Rendering.VolumeParameter[,] savedStates;

		private static global::System.Collections.Generic.List<global::System.Type> s_ComponentTypes;

		public int selectedComponent { get; set; }

		public global::UnityEngine.Camera selectedCamera
		{
			get
			{
				if (selectedCameraIndex >= 0)
				{
					return global::System.Linq.Enumerable.ElementAt(cameras, selectedCameraIndex);
				}
				return null;
			}
		}

		public int selectedCameraIndex
		{
			get
			{
				int num = global::System.Linq.Enumerable.Count(cameras);
				if (num <= 0)
				{
					return -1;
				}
				return global::System.Math.Clamp(m_SelectedCameraIndex, 0, num - 1);
			}
			set
			{
				int num = global::System.Linq.Enumerable.Count(cameras);
				m_SelectedCameraIndex = global::System.Math.Clamp(value, 0, num - 1);
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.Camera> cameras
		{
			get
			{
				m_Cameras.Clear();
				if (m_CamerasArray == null || m_CamerasArray.Length != global::UnityEngine.Camera.allCamerasCount)
				{
					m_CamerasArray = new global::UnityEngine.Camera[global::UnityEngine.Camera.allCamerasCount];
				}
				global::UnityEngine.Camera.GetAllCameras(m_CamerasArray);
				global::UnityEngine.Camera[] camerasArray = m_CamerasArray;
				foreach (global::UnityEngine.Camera camera in camerasArray)
				{
					if (!(camera == null) && camera.cameraType != global::UnityEngine.CameraType.Preview && camera.cameraType != global::UnityEngine.CameraType.Reflection)
					{
						if (!camera.TryGetComponent<T>(out var component))
						{
							component = camera.gameObject.AddComponent<T>();
						}
						if (component != null)
						{
							m_Cameras.Add(camera);
						}
					}
				}
				return m_Cameras;
			}
		}

		public abstract global::UnityEngine.Rendering.VolumeStack selectedCameraVolumeStack { get; }

		public abstract global::UnityEngine.LayerMask selectedCameraLayerMask { get; }

		public abstract global::UnityEngine.Vector3 selectedCameraPosition { get; }

		public global::System.Type selectedComponentType
		{
			get
			{
				if (selectedComponent <= 0)
				{
					return null;
				}
				return volumeComponentsPathAndType[selectedComponent - 1].Item2;
			}
			set
			{
				int num = volumeComponentsPathAndType.FindIndex(((string, global::System.Type) t) => t.Item2 == value);
				if (num != -1)
				{
					selectedComponent = num + 1;
				}
			}
		}

		public global::System.Collections.Generic.List<(string, global::System.Type)> volumeComponentsPathAndType => global::UnityEngine.Rendering.VolumeManager.instance.GetVolumeComponentsForDisplay(global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipelineAssetType);

		[global::System.Obsolete("This property is obsolete and kept only for not breaking user code. VolumeDebugSettings will use current pipeline when it needs to gather volume component types and paths. #from(2023.2)")]
		public virtual global::System.Type targetRenderPipeline { get; }

		[global::System.Obsolete("Please use volumeComponentsPathAndType instead, and get the second element of the tuple #from(2022.2)")]
		public static global::System.Collections.Generic.List<global::System.Type> componentTypes
		{
			get
			{
				if (s_ComponentTypes == null)
				{
					s_ComponentTypes = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Where(global::UnityEngine.Rendering.VolumeManager.instance.baseComponentTypeArray, (global::System.Type t) => !t.IsDefined(typeof(global::UnityEngine.HideInInspector), inherit: false)), (global::System.Type t) => !t.IsDefined(typeof(global::System.ObsoleteAttribute), inherit: false)), (global::System.Type t) => ComponentDisplayName(t)));
				}
				return s_ComponentTypes;
			}
		}

		[global::System.Obsolete("Cameras are auto registered/unregistered, use property cameras #from(2022.2)")]
		protected static global::System.Collections.Generic.List<T> additionalCameraDatas { get; private set; } = new global::System.Collections.Generic.List<T>();

		internal global::UnityEngine.Rendering.VolumeParameter GetParameter(global::UnityEngine.Rendering.VolumeComponent component, global::System.Reflection.FieldInfo field)
		{
			return (global::UnityEngine.Rendering.VolumeParameter)field.GetValue(component);
		}

		internal global::UnityEngine.Rendering.VolumeParameter GetParameter(global::System.Reflection.FieldInfo field)
		{
			global::UnityEngine.Rendering.VolumeStack volumeStack = selectedCameraVolumeStack;
			if (volumeStack != null)
			{
				return GetParameter(volumeStack.GetComponent(selectedComponentType), field);
			}
			return null;
		}

		internal global::UnityEngine.Rendering.VolumeParameter GetParameter(global::UnityEngine.Rendering.Volume volume, global::System.Reflection.FieldInfo field)
		{
			if (!(volume.HasInstantiatedProfile() ? volume.profile : volume.sharedProfile).TryGet<global::UnityEngine.Rendering.VolumeComponent>(selectedComponentType, out var component))
			{
				return null;
			}
			global::UnityEngine.Rendering.VolumeParameter parameter = GetParameter(component, field);
			if (!parameter.overrideState)
			{
				return null;
			}
			return parameter;
		}

		private float ComputeWeight(global::UnityEngine.Rendering.Volume volume, global::UnityEngine.Vector3 triggerPos)
		{
			if (volume == null)
			{
				return 0f;
			}
			global::UnityEngine.Rendering.VolumeProfile volumeProfile = (volume.HasInstantiatedProfile() ? volume.profile : volume.sharedProfile);
			if (!volume.gameObject.activeInHierarchy)
			{
				return 0f;
			}
			if (!volume.enabled || volumeProfile == null || volume.weight <= 0f)
			{
				return 0f;
			}
			if (!volumeProfile.TryGet<global::UnityEngine.Rendering.VolumeComponent>(selectedComponentType, out var component))
			{
				return 0f;
			}
			if (!component.active)
			{
				return 0f;
			}
			float num = global::UnityEngine.Mathf.Clamp01(volume.weight);
			if (!volume.isGlobal)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Collider> colliders = volume.colliders;
				float num2 = float.PositiveInfinity;
				foreach (global::UnityEngine.Collider item in colliders)
				{
					if (item.enabled)
					{
						float sqrMagnitude = (item.ClosestPoint(triggerPos) - triggerPos).sqrMagnitude;
						if (sqrMagnitude < num2)
						{
							num2 = sqrMagnitude;
						}
					}
				}
				float num3 = volume.blendDistance * volume.blendDistance;
				if (num2 > num3)
				{
					num = 0f;
				}
				else if (num3 > 0f)
				{
					num *= 1f - num2 / num3;
				}
			}
			return num;
		}

		public global::UnityEngine.Rendering.Volume[] GetVolumes()
		{
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Reverse(global::System.Linq.Enumerable.Where(global::UnityEngine.Rendering.VolumeManager.instance.GetVolumes(selectedCameraLayerMask), (global::UnityEngine.Rendering.Volume v) => v.sharedProfile != null)));
		}

		private global::UnityEngine.Rendering.VolumeParameter[,] GetStates()
		{
			global::System.Reflection.FieldInfo[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Where(selectedComponentType.GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic), (global::System.Reflection.FieldInfo t) => t.FieldType.IsSubclassOf(typeof(global::UnityEngine.Rendering.VolumeParameter))));
			global::UnityEngine.Rendering.VolumeParameter[,] array2 = new global::UnityEngine.Rendering.VolumeParameter[volumes.Length, array.Length];
			for (int num = 0; num < volumes.Length; num++)
			{
				if ((volumes[num].HasInstantiatedProfile() ? volumes[num].profile : volumes[num].sharedProfile).TryGet<global::UnityEngine.Rendering.VolumeComponent>(selectedComponentType, out var component))
				{
					for (int num2 = 0; num2 < array.Length; num2++)
					{
						global::UnityEngine.Rendering.VolumeParameter parameter = GetParameter(component, array[num2]);
						array2[num, num2] = (parameter.overrideState ? parameter : null);
					}
				}
			}
			return array2;
		}

		private bool ChangedStates(global::UnityEngine.Rendering.VolumeParameter[,] newStates)
		{
			if (savedStates.GetLength(1) != newStates.GetLength(1))
			{
				return true;
			}
			for (int i = 0; i < savedStates.GetLength(0); i++)
			{
				for (int j = 0; j < savedStates.GetLength(1); j++)
				{
					if (savedStates[i, j] == null != (newStates[i, j] == null))
					{
						return true;
					}
				}
			}
			return false;
		}

		public bool RefreshVolumes(global::UnityEngine.Rendering.Volume[] newVolumes)
		{
			bool result = false;
			if (volumes == null || !global::System.Linq.Enumerable.SequenceEqual(newVolumes, volumes))
			{
				volumes = (global::UnityEngine.Rendering.Volume[])newVolumes.Clone();
				savedStates = GetStates();
				result = true;
			}
			else
			{
				global::UnityEngine.Rendering.VolumeParameter[,] states = GetStates();
				if (savedStates == null || ChangedStates(states))
				{
					savedStates = states;
					result = true;
				}
			}
			global::UnityEngine.Vector3 triggerPos = selectedCameraPosition;
			weights = new float[volumes.Length];
			for (int i = 0; i < volumes.Length; i++)
			{
				weights[i] = ComputeWeight(volumes[i], triggerPos);
			}
			return result;
		}

		public float GetVolumeWeight(global::UnityEngine.Rendering.Volume volume)
		{
			global::UnityEngine.Vector3 triggerPos = selectedCameraPosition;
			return ComputeWeight(volume, triggerPos);
		}

		public bool VolumeHasInfluence(global::UnityEngine.Rendering.Volume volume)
		{
			global::UnityEngine.Vector3 triggerPos = selectedCameraPosition;
			return ComputeWeight(volume, triggerPos) > 0f;
		}

		[global::System.Obsolete("Please use componentPathAndType instead, and get the first element of the tuple #from(2022.2)")]
		public static string ComponentDisplayName(global::System.Type component)
		{
			if (global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute(component, typeof(global::UnityEngine.Rendering.VolumeComponentMenuForRenderPipeline), inherit: false) is global::UnityEngine.Rendering.VolumeComponentMenuForRenderPipeline volumeComponentMenuForRenderPipeline)
			{
				return volumeComponentMenuForRenderPipeline.menu;
			}
			if (global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute(component, typeof(global::UnityEngine.Rendering.VolumeComponentMenu), inherit: false) is global::UnityEngine.Rendering.VolumeComponentMenuForRenderPipeline volumeComponentMenuForRenderPipeline2)
			{
				return volumeComponentMenuForRenderPipeline2.menu;
			}
			return component.Name;
		}

		[global::System.Obsolete("Cameras are auto registered/unregistered #from(2022.2)")]
		public static void RegisterCamera(T additionalCamera)
		{
			if (!additionalCameraDatas.Contains(additionalCamera))
			{
				additionalCameraDatas.Add(additionalCamera);
			}
		}

		[global::System.Obsolete("Cameras are auto registered/unregistered #from(2022.2)")]
		public static void UnRegisterCamera(T additionalCamera)
		{
			if (additionalCameraDatas.Contains(additionalCamera))
			{
				additionalCameraDatas.Remove(additionalCamera);
			}
		}
	}
}
