namespace UnityEngine.InputSystem.Layouts
{
	[global::System.Serializable]
	public struct InputDeviceDescription : global::System.IEquatable<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription>
	{
		private struct DeviceDescriptionJson
		{
			public string @interface;

			public string type;

			public string product;

			public string serial;

			public string version;

			public string manufacturer;

			public string capabilities;
		}

		[global::UnityEngine.SerializeField]
		private string m_InterfaceName;

		[global::UnityEngine.SerializeField]
		private string m_DeviceClass;

		[global::UnityEngine.SerializeField]
		private string m_Manufacturer;

		[global::UnityEngine.SerializeField]
		private string m_Product;

		[global::UnityEngine.SerializeField]
		private string m_Serial;

		[global::UnityEngine.SerializeField]
		private string m_Version;

		[global::UnityEngine.SerializeField]
		private string m_Capabilities;

		public string interfaceName
		{
			get
			{
				return m_InterfaceName;
			}
			set
			{
				m_InterfaceName = value;
			}
		}

		public string deviceClass
		{
			get
			{
				return m_DeviceClass;
			}
			set
			{
				m_DeviceClass = value;
			}
		}

		public string manufacturer
		{
			get
			{
				return m_Manufacturer;
			}
			set
			{
				m_Manufacturer = value;
			}
		}

		public string product
		{
			get
			{
				return m_Product;
			}
			set
			{
				m_Product = value;
			}
		}

		public string serial
		{
			get
			{
				return m_Serial;
			}
			set
			{
				m_Serial = value;
			}
		}

		public string version
		{
			get
			{
				return m_Version;
			}
			set
			{
				m_Version = value;
			}
		}

		public string capabilities
		{
			get
			{
				return m_Capabilities;
			}
			set
			{
				m_Capabilities = value;
			}
		}

		public bool empty
		{
			get
			{
				if (string.IsNullOrEmpty(m_InterfaceName) && string.IsNullOrEmpty(m_DeviceClass) && string.IsNullOrEmpty(m_Manufacturer) && string.IsNullOrEmpty(m_Product) && string.IsNullOrEmpty(m_Serial) && string.IsNullOrEmpty(m_Version))
				{
					return string.IsNullOrEmpty(m_Capabilities);
				}
				return false;
			}
		}

		public override string ToString()
		{
			bool flag = !string.IsNullOrEmpty(product);
			bool flag2 = !string.IsNullOrEmpty(manufacturer);
			bool flag3 = !string.IsNullOrEmpty(interfaceName);
			if (flag && flag2)
			{
				if (flag3)
				{
					return manufacturer + " " + product + " (" + interfaceName + ")";
				}
				return manufacturer + " " + product;
			}
			if (flag)
			{
				if (flag3)
				{
					return product + " (" + interfaceName + ")";
				}
				return product;
			}
			if (!string.IsNullOrEmpty(deviceClass))
			{
				if (flag3)
				{
					return deviceClass + " (" + interfaceName + ")";
				}
				return deviceClass;
			}
			if (!string.IsNullOrEmpty(capabilities))
			{
				string text = capabilities;
				if (capabilities.Length > 40)
				{
					text = text.Substring(0, 40) + "...";
				}
				if (flag3)
				{
					return text + " (" + interfaceName + ")";
				}
				return text;
			}
			if (flag3)
			{
				return interfaceName;
			}
			return "<Empty Device Description>";
		}

		public bool Equals(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription other)
		{
			if (global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_InterfaceName, other.m_InterfaceName) && global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_DeviceClass, other.m_DeviceClass) && global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_Manufacturer, other.m_Manufacturer) && global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_Product, other.m_Product) && global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_Serial, other.m_Serial) && global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_Version, other.m_Version))
			{
				return global::UnityEngine.InputSystem.Utilities.StringHelpers.InvariantEqualsIgnoreCase(m_Capabilities, other.m_Capabilities);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Layouts.InputDeviceDescription other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((((((((((((m_InterfaceName != null) ? m_InterfaceName.GetHashCode() : 0) * 397) ^ ((m_DeviceClass != null) ? m_DeviceClass.GetHashCode() : 0)) * 397) ^ ((m_Manufacturer != null) ? m_Manufacturer.GetHashCode() : 0)) * 397) ^ ((m_Product != null) ? m_Product.GetHashCode() : 0)) * 397) ^ ((m_Serial != null) ? m_Serial.GetHashCode() : 0)) * 397) ^ ((m_Version != null) ? m_Version.GetHashCode() : 0)) * 397) ^ ((m_Capabilities != null) ? m_Capabilities.GetHashCode() : 0);
		}

		public static bool operator ==(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription left, global::UnityEngine.InputSystem.Layouts.InputDeviceDescription right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription left, global::UnityEngine.InputSystem.Layouts.InputDeviceDescription right)
		{
			return !left.Equals(right);
		}

		public string ToJson()
		{
			return global::UnityEngine.JsonUtility.ToJson(new global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.DeviceDescriptionJson
			{
				@interface = interfaceName,
				type = deviceClass,
				product = product,
				manufacturer = manufacturer,
				serial = serial,
				version = version,
				capabilities = capabilities
			}, prettyPrint: true);
		}

		public static global::UnityEngine.InputSystem.Layouts.InputDeviceDescription FromJson(string json)
		{
			if (json == null)
			{
				throw new global::System.ArgumentNullException("json");
			}
			global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.DeviceDescriptionJson deviceDescriptionJson = global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.Layouts.InputDeviceDescription.DeviceDescriptionJson>(json);
			return new global::UnityEngine.InputSystem.Layouts.InputDeviceDescription
			{
				interfaceName = deviceDescriptionJson.@interface,
				deviceClass = deviceDescriptionJson.type,
				product = deviceDescriptionJson.product,
				manufacturer = deviceDescriptionJson.manufacturer,
				serial = deviceDescriptionJson.serial,
				version = deviceDescriptionJson.version,
				capabilities = deviceDescriptionJson.capabilities
			};
		}

		internal static bool ComparePropertyToDeviceDescriptor(string propertyName, global::UnityEngine.InputSystem.Utilities.JsonParser.JsonString propertyValue, string deviceDescriptor)
		{
			global::UnityEngine.InputSystem.Utilities.JsonParser jsonParser = new global::UnityEngine.InputSystem.Utilities.JsonParser(deviceDescriptor);
			if (!jsonParser.NavigateToProperty(propertyName))
			{
				if (propertyValue.text.isEmpty)
				{
					return true;
				}
				return false;
			}
			return jsonParser.CurrentPropertyHasValueEqualTo(propertyValue);
		}
	}
}
