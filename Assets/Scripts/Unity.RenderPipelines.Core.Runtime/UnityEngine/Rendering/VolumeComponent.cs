namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class VolumeComponent : global::UnityEngine.ScriptableObject
	{
		public sealed class Indent : global::UnityEngine.PropertyAttribute
		{
			public readonly int relativeAmount;

			public Indent(int relativeAmount = 1)
			{
				this.relativeAmount = relativeAmount;
			}
		}

		public bool active = true;

		internal global::UnityEngine.Rendering.VolumeParameter[] parameterList;

		private global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> m_ParameterReadOnlyCollection;

		[global::System.Obsolete("Use DisplayInfo attribute to define a display name instead. #from(6000.3)", false)]
		public string displayName { get; protected set; }

		public global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> parameters => m_ParameterReadOnlyCollection ?? (m_ParameterReadOnlyCollection = new global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter>(parameterList));

		internal static void FindParameters(object o, global::System.Collections.Generic.List<global::UnityEngine.Rendering.VolumeParameter> parameters, global::System.Func<global::System.Reflection.FieldInfo, bool> filter = null)
		{
			if (o == null)
			{
				return;
			}
			foreach (global::System.Reflection.FieldInfo item2 in global::System.Linq.Enumerable.OrderBy(o.GetType().GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic), (global::System.Reflection.FieldInfo t) => t.MetadataToken))
			{
				global::System.Type fieldType = item2.FieldType;
				if (fieldType.IsSubclassOf(typeof(global::UnityEngine.Rendering.VolumeParameter)))
				{
					if (filter == null || filter(item2))
					{
						global::UnityEngine.Rendering.VolumeParameter item = (global::UnityEngine.Rendering.VolumeParameter)item2.GetValue(o);
						parameters.Add(item);
					}
				}
				else if (!fieldType.IsArray && fieldType.IsClass)
				{
					FindParameters(item2.GetValue(o), parameters, filter);
				}
			}
		}

		protected virtual void OnEnable()
		{
			global::UnityEngine.Rendering.ListPool<global::UnityEngine.Rendering.VolumeParameter>.Get(out var value);
			FindParameters(this, value);
			parameterList = value.ToArray();
			global::UnityEngine.Rendering.ListPool<global::UnityEngine.Rendering.VolumeParameter>.Release(value);
			global::UnityEngine.Rendering.VolumeParameter[] array = parameterList;
			foreach (global::UnityEngine.Rendering.VolumeParameter volumeParameter in array)
			{
				if (volumeParameter != null)
				{
					volumeParameter.OnEnable();
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("Volume Component " + GetType().Name + " contains a null parameter; please make sure all parameters are initialized to a default value. Until this is fixed the null parameters will not be considered by the system.");
				}
			}
		}

		protected virtual void OnDisable()
		{
			global::UnityEngine.Rendering.VolumeParameter[] array = parameterList;
			for (int i = 0; i < array.Length; i++)
			{
				array[i]?.OnDisable();
			}
		}

		public virtual void Override(global::UnityEngine.Rendering.VolumeComponent state, float interpFactor)
		{
			int num = parameterList.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.Rendering.VolumeParameter volumeParameter = state.parameterList[i];
				global::UnityEngine.Rendering.VolumeParameter volumeParameter2 = parameterList[i];
				if (volumeParameter2.overrideState)
				{
					volumeParameter.overrideState = volumeParameter2.overrideState;
					volumeParameter.Interp(volumeParameter, volumeParameter2, interpFactor);
				}
			}
		}

		public void SetAllOverridesTo(bool state)
		{
			SetOverridesTo(parameterList, state);
		}

		internal void SetOverridesTo(global::System.Collections.Generic.IEnumerable<global::UnityEngine.Rendering.VolumeParameter> enumerable, bool state)
		{
			foreach (global::UnityEngine.Rendering.VolumeParameter item in enumerable)
			{
				item.overrideState = state;
				global::System.Type type = item.GetType();
				if (global::UnityEngine.Rendering.VolumeParameter.IsObjectParameter(type))
				{
					global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> readOnlyCollection = (global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter>)type.GetProperty("parameters", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic).GetValue(item, null);
					if (readOnlyCollection != null)
					{
						SetOverridesTo(readOnlyCollection, state);
					}
				}
			}
		}

		public override int GetHashCode()
		{
			int num = 17;
			for (int i = 0; i < parameterList.Length; i++)
			{
				num = num * 23 + parameterList[i].GetHashCode();
			}
			return num;
		}

		public bool AnyPropertiesIsOverridden()
		{
			for (int i = 0; i < parameterList.Length; i++)
			{
				if (parameterList[i].overrideState)
				{
					return true;
				}
			}
			return false;
		}

		protected virtual void OnDestroy()
		{
			Release();
		}

		public void Release()
		{
			if (parameterList == null)
			{
				return;
			}
			for (int i = 0; i < parameterList.Length; i++)
			{
				if (parameterList[i] != null)
				{
					parameterList[i].Release();
				}
			}
		}
	}
}
