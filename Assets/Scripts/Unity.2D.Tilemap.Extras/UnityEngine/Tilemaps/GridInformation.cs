namespace UnityEngine.Tilemaps
{
	[global::System.Serializable]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.tilemap.extras@latest/index.html?subfolder=/manual/GridInformation.html")]
	[global::UnityEngine.AddComponentMenu("Tilemap/Grid Information")]
	public class GridInformation : global::UnityEngine.MonoBehaviour, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::System.Serializable]
		internal struct GridInformationValue
		{
			public global::UnityEngine.Tilemaps.GridInformationType type;

			public object data;
		}

		[global::System.Serializable]
		internal struct GridInformationKey : global::System.IEquatable<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>
		{
			public global::UnityEngine.Vector3Int position;

			public string name;

			public bool Equals(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key)
			{
				if (position == key.position)
				{
					return name == key.name;
				}
				return false;
			}

			public override int GetHashCode()
			{
				return global::System.HashCode.Combine(position.GetHashCode(), name.GetHashCode());
			}
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey> m_PositionIntKeys = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<int> m_PositionIntValues = new global::System.Collections.Generic.List<int>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey> m_PositionStringKeys = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<string> m_PositionStringValues = new global::System.Collections.Generic.List<string>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey> m_PositionFloatKeys = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<float> m_PositionFloatValues = new global::System.Collections.Generic.List<float>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey> m_PositionDoubleKeys = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<double> m_PositionDoubleValues = new global::System.Collections.Generic.List<double>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey> m_PositionObjectKeys = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Object> m_PositionObjectValues = new global::System.Collections.Generic.List<global::UnityEngine.Object>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey> m_PositionColorKeys = new global::System.Collections.Generic.List<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey>();

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::System.Collections.Generic.List<global::UnityEngine.Color> m_PositionColorValues = new global::System.Collections.Generic.List<global::UnityEngine.Color>();

		internal global::System.Collections.Generic.Dictionary<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey, global::UnityEngine.Tilemaps.GridInformation.GridInformationValue> PositionProperties { get; } = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey, global::UnityEngine.Tilemaps.GridInformation.GridInformationValue>();

		public virtual void Reset()
		{
			PositionProperties.Clear();
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			m_PositionIntKeys.Clear();
			m_PositionIntValues.Clear();
			m_PositionStringKeys.Clear();
			m_PositionStringValues.Clear();
			m_PositionFloatKeys.Clear();
			m_PositionFloatValues.Clear();
			m_PositionDoubleKeys.Clear();
			m_PositionDoubleValues.Clear();
			m_PositionObjectKeys.Clear();
			m_PositionObjectValues.Clear();
			m_PositionColorKeys.Clear();
			m_PositionColorValues.Clear();
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Tilemaps.GridInformation.GridInformationKey, global::UnityEngine.Tilemaps.GridInformation.GridInformationValue> positionProperty in PositionProperties)
			{
				switch (positionProperty.Value.type)
				{
				case global::UnityEngine.Tilemaps.GridInformationType.Integer:
					m_PositionIntKeys.Add(positionProperty.Key);
					m_PositionIntValues.Add((int)positionProperty.Value.data);
					break;
				case global::UnityEngine.Tilemaps.GridInformationType.String:
					m_PositionStringKeys.Add(positionProperty.Key);
					m_PositionStringValues.Add(positionProperty.Value.data as string);
					break;
				case global::UnityEngine.Tilemaps.GridInformationType.Float:
					m_PositionFloatKeys.Add(positionProperty.Key);
					m_PositionFloatValues.Add((float)positionProperty.Value.data);
					break;
				case global::UnityEngine.Tilemaps.GridInformationType.Double:
					m_PositionDoubleKeys.Add(positionProperty.Key);
					m_PositionDoubleValues.Add((double)positionProperty.Value.data);
					break;
				case global::UnityEngine.Tilemaps.GridInformationType.Color:
					m_PositionColorKeys.Add(positionProperty.Key);
					m_PositionColorValues.Add((global::UnityEngine.Color)positionProperty.Value.data);
					break;
				default:
					m_PositionObjectKeys.Add(positionProperty.Key);
					m_PositionObjectValues.Add(positionProperty.Value.data as global::UnityEngine.Object);
					break;
				}
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			PositionProperties.Clear();
			global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
			for (int i = 0; i != global::System.Math.Min(m_PositionIntKeys.Count, m_PositionIntValues.Count); i++)
			{
				value.type = global::UnityEngine.Tilemaps.GridInformationType.Integer;
				value.data = m_PositionIntValues[i];
				PositionProperties.Add(m_PositionIntKeys[i], value);
			}
			global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value2 = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
			for (int j = 0; j != global::System.Math.Min(m_PositionStringKeys.Count, m_PositionStringValues.Count); j++)
			{
				value2.type = global::UnityEngine.Tilemaps.GridInformationType.String;
				value2.data = m_PositionStringValues[j];
				PositionProperties.Add(m_PositionStringKeys[j], value2);
			}
			global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value3 = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
			for (int k = 0; k != global::System.Math.Min(m_PositionFloatKeys.Count, m_PositionFloatValues.Count); k++)
			{
				value3.type = global::UnityEngine.Tilemaps.GridInformationType.Float;
				value3.data = m_PositionFloatValues[k];
				PositionProperties.Add(m_PositionFloatKeys[k], value3);
			}
			global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value4 = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
			for (int l = 0; l != global::System.Math.Min(m_PositionDoubleKeys.Count, m_PositionDoubleValues.Count); l++)
			{
				value4.type = global::UnityEngine.Tilemaps.GridInformationType.Double;
				value4.data = m_PositionDoubleValues[l];
				PositionProperties.Add(m_PositionDoubleKeys[l], value4);
			}
			global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value5 = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
			for (int m = 0; m != global::System.Math.Min(m_PositionObjectKeys.Count, m_PositionObjectValues.Count); m++)
			{
				value5.type = global::UnityEngine.Tilemaps.GridInformationType.UnityObject;
				value5.data = m_PositionObjectValues[m];
				PositionProperties.Add(m_PositionObjectKeys[m], value5);
			}
			global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value6 = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
			for (int n = 0; n != global::System.Math.Min(m_PositionColorKeys.Count, m_PositionColorValues.Count); n++)
			{
				value6.type = global::UnityEngine.Tilemaps.GridInformationType.Color;
				value6.data = m_PositionColorValues[n];
				PositionProperties.Add(m_PositionColorKeys[n], value6);
			}
		}

		public bool SetPositionProperty<T>(global::UnityEngine.Vector3Int position, string name, T positionProperty)
		{
			throw new global::System.NotImplementedException("Storing this type is not accepted in GridInformation");
		}

		public bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, int positionProperty)
		{
			return SetPositionProperty(position, name, global::UnityEngine.Tilemaps.GridInformationType.Integer, positionProperty);
		}

		public bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, string positionProperty)
		{
			return SetPositionProperty(position, name, global::UnityEngine.Tilemaps.GridInformationType.String, positionProperty);
		}

		public bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, float positionProperty)
		{
			return SetPositionProperty(position, name, global::UnityEngine.Tilemaps.GridInformationType.Float, positionProperty);
		}

		public bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, double positionProperty)
		{
			return SetPositionProperty(position, name, global::UnityEngine.Tilemaps.GridInformationType.Double, positionProperty);
		}

		public bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, global::UnityEngine.Object positionProperty)
		{
			return SetPositionProperty(position, name, global::UnityEngine.Tilemaps.GridInformationType.UnityObject, positionProperty);
		}

		public bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, global::UnityEngine.Color positionProperty)
		{
			return SetPositionProperty(position, name, global::UnityEngine.Tilemaps.GridInformationType.Color, positionProperty);
		}

		private bool SetPositionProperty(global::UnityEngine.Vector3Int position, string name, global::UnityEngine.Tilemaps.GridInformationType dataType, object positionProperty)
		{
			if (GetComponentInParent<global::UnityEngine.Grid>() != null && positionProperty != null)
			{
				global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
				key.position = position;
				key.name = name;
				global::UnityEngine.Tilemaps.GridInformation.GridInformationValue value = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationValue);
				value.type = dataType;
				value.data = positionProperty;
				PositionProperties[key] = value;
				return true;
			}
			return false;
		}

		public T GetPositionProperty<T>(global::UnityEngine.Vector3Int position, string name, T defaultValue) where T : global::UnityEngine.Object
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			if (PositionProperties.TryGetValue(key, out var value))
			{
				if (value.type != global::UnityEngine.Tilemaps.GridInformationType.UnityObject)
				{
					throw new global::System.InvalidCastException("Value stored in GridInformation is not of the right type");
				}
				return value.data as T;
			}
			return defaultValue;
		}

		public int GetPositionProperty(global::UnityEngine.Vector3Int position, string name, int defaultValue)
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			if (PositionProperties.TryGetValue(key, out var value))
			{
				if (value.type != global::UnityEngine.Tilemaps.GridInformationType.Integer)
				{
					throw new global::System.InvalidCastException("Value stored in GridInformation is not of the right type");
				}
				return (int)value.data;
			}
			return defaultValue;
		}

		public string GetPositionProperty(global::UnityEngine.Vector3Int position, string name, string defaultValue)
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			if (PositionProperties.TryGetValue(key, out var value))
			{
				if (value.type != global::UnityEngine.Tilemaps.GridInformationType.String)
				{
					throw new global::System.InvalidCastException("Value stored in GridInformation is not of the right type");
				}
				return (string)value.data;
			}
			return defaultValue;
		}

		public float GetPositionProperty(global::UnityEngine.Vector3Int position, string name, float defaultValue)
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			if (PositionProperties.TryGetValue(key, out var value))
			{
				if (value.type != global::UnityEngine.Tilemaps.GridInformationType.Float)
				{
					throw new global::System.InvalidCastException("Value stored in GridInformation is not of the right type");
				}
				return (float)value.data;
			}
			return defaultValue;
		}

		public double GetPositionProperty(global::UnityEngine.Vector3Int position, string name, double defaultValue)
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			if (PositionProperties.TryGetValue(key, out var value))
			{
				if (value.type != global::UnityEngine.Tilemaps.GridInformationType.Double)
				{
					throw new global::System.InvalidCastException("Value stored in GridInformation is not of the right type");
				}
				return (double)value.data;
			}
			return defaultValue;
		}

		public global::UnityEngine.Color GetPositionProperty(global::UnityEngine.Vector3Int position, string name, global::UnityEngine.Color defaultValue)
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			if (PositionProperties.TryGetValue(key, out var value))
			{
				if (value.type != global::UnityEngine.Tilemaps.GridInformationType.Color)
				{
					throw new global::System.InvalidCastException("Value stored in GridInformation is not of the right type");
				}
				return (global::UnityEngine.Color)value.data;
			}
			return defaultValue;
		}

		public bool ErasePositionProperty(global::UnityEngine.Vector3Int position, string name)
		{
			global::UnityEngine.Tilemaps.GridInformation.GridInformationKey key = default(global::UnityEngine.Tilemaps.GridInformation.GridInformationKey);
			key.position = position;
			key.name = name;
			return PositionProperties.Remove(key);
		}

		public global::UnityEngine.Vector3Int[] GetAllPositions(string propertyName)
		{
			return global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.ToList(PositionProperties.Keys).FindAll((global::UnityEngine.Tilemaps.GridInformation.GridInformationKey x) => x.name == propertyName), (global::UnityEngine.Tilemaps.GridInformation.GridInformationKey x) => x.position));
		}
	}
}
