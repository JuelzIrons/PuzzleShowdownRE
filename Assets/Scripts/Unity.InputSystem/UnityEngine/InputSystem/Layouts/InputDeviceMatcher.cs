namespace UnityEngine.InputSystem.Layouts
{
	public struct InputDeviceMatcher : global::System.IEquatable<global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher>
	{
		[global::System.Serializable]
		internal struct MatcherJson
		{
			public struct Capability
			{
				public string path;

				public string value;
			}

			public string @interface;

			public string[] interfaces;

			public string deviceClass;

			public string[] deviceClasses;

			public string manufacturer;

			public string manufacturerContains;

			public string[] manufacturers;

			public string product;

			public string[] products;

			public string version;

			public string[] versions;

			public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson.Capability[] capabilities;

			public static global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson FromMatcher(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
			{
				if (matcher.empty)
				{
					return default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson);
				}
				global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson result = default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson);
				global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object>[] patterns = matcher.m_Patterns;
				for (int i = 0; i < patterns.Length; i++)
				{
					global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object> keyValuePair = patterns[i];
					global::UnityEngine.InputSystem.Utilities.InternedString key = keyValuePair.Key;
					string text = keyValuePair.Value.ToString();
					if (key == kInterfaceKey)
					{
						if (result.@interface == null)
						{
							result.@interface = text;
						}
						else
						{
							global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result.interfaces, text);
						}
					}
					else if (key == kDeviceClassKey)
					{
						if (result.deviceClass == null)
						{
							result.deviceClass = text;
						}
						else
						{
							global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result.deviceClasses, text);
						}
					}
					else if (key == kManufacturerKey)
					{
						if (result.manufacturer == null)
						{
							result.manufacturer = text;
						}
						else
						{
							global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result.manufacturers, text);
						}
					}
					else if (key == kProductKey)
					{
						if (result.product == null)
						{
							result.product = text;
						}
						else
						{
							global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result.products, text);
						}
					}
					else if (key == kVersionKey)
					{
						if (result.version == null)
						{
							result.version = text;
						}
						else
						{
							global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result.versions, text);
						}
					}
					else
					{
						global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result.capabilities, new global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson.Capability
						{
							path = key,
							value = text
						});
					}
				}
				return result;
			}

			public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher ToMatcher()
			{
				global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher result = default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher);
				if (!string.IsNullOrEmpty(@interface))
				{
					result = result.WithInterface(@interface);
				}
				if (interfaces != null)
				{
					string[] array = interfaces;
					foreach (string pattern in array)
					{
						result = result.WithInterface(pattern);
					}
				}
				if (!string.IsNullOrEmpty(deviceClass))
				{
					result = result.WithDeviceClass(deviceClass);
				}
				if (deviceClasses != null)
				{
					string[] array = deviceClasses;
					foreach (string pattern2 in array)
					{
						result = result.WithDeviceClass(pattern2);
					}
				}
				if (!string.IsNullOrEmpty(manufacturer))
				{
					result = result.WithManufacturer(manufacturer);
				}
				if (manufacturers != null)
				{
					string[] array = manufacturers;
					foreach (string pattern3 in array)
					{
						result = result.WithManufacturer(pattern3);
					}
				}
				if (!string.IsNullOrEmpty(manufacturerContains))
				{
					result = result.WithManufacturerContains(manufacturerContains);
				}
				if (!string.IsNullOrEmpty(product))
				{
					result = result.WithProduct(product);
				}
				if (products != null)
				{
					string[] array = products;
					foreach (string pattern4 in array)
					{
						result = result.WithProduct(pattern4);
					}
				}
				if (!string.IsNullOrEmpty(version))
				{
					result = result.WithVersion(version);
				}
				if (versions != null)
				{
					string[] array = versions;
					foreach (string pattern5 in array)
					{
						result = result.WithVersion(pattern5);
					}
				}
				if (capabilities != null)
				{
					global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson.Capability[] array2 = capabilities;
					for (int i = 0; i < array2.Length; i++)
					{
						global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson.Capability capability = array2[i];
						result = result.WithCapability(capability.path, capability.value);
					}
				}
				return result;
			}
		}

		private global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object>[] m_Patterns;

		private static readonly global::UnityEngine.InputSystem.Utilities.InternedString kInterfaceKey = new global::UnityEngine.InputSystem.Utilities.InternedString("interface");

		private static readonly global::UnityEngine.InputSystem.Utilities.InternedString kDeviceClassKey = new global::UnityEngine.InputSystem.Utilities.InternedString("deviceClass");

		private static readonly global::UnityEngine.InputSystem.Utilities.InternedString kManufacturerKey = new global::UnityEngine.InputSystem.Utilities.InternedString("manufacturer");

		private static readonly global::UnityEngine.InputSystem.Utilities.InternedString kManufacturerContainsKey = new global::UnityEngine.InputSystem.Utilities.InternedString("manufacturerContains");

		private static readonly global::UnityEngine.InputSystem.Utilities.InternedString kProductKey = new global::UnityEngine.InputSystem.Utilities.InternedString("product");

		private static readonly global::UnityEngine.InputSystem.Utilities.InternedString kVersionKey = new global::UnityEngine.InputSystem.Utilities.InternedString("version");

		public bool empty => m_Patterns == null;

		public global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<string, object>> patterns
		{
			get
			{
				if (m_Patterns != null)
				{
					int count = m_Patterns.Length;
					int i = 0;
					while (i < count)
					{
						yield return new global::System.Collections.Generic.KeyValuePair<string, object>(m_Patterns[i].Key.ToString(), m_Patterns[i].Value);
						int num = i + 1;
						i = num;
					}
				}
			}
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithInterface(string pattern, bool supportRegex = true)
		{
			return With(kInterfaceKey, pattern, supportRegex);
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithDeviceClass(string pattern, bool supportRegex = true)
		{
			return With(kDeviceClassKey, pattern, supportRegex);
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithManufacturer(string pattern, bool supportRegex = true)
		{
			return With(kManufacturerKey, pattern, supportRegex);
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithManufacturerContains(string noRegExPattern)
		{
			return With(kManufacturerContainsKey, noRegExPattern, supportRegex: false);
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithProduct(string pattern, bool supportRegex = true)
		{
			return With(kProductKey, pattern, supportRegex);
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithVersion(string pattern, bool supportRegex = true)
		{
			return With(kVersionKey, pattern, supportRegex);
		}

		public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher WithCapability<TValue>(string path, TValue value)
		{
			return With(new global::UnityEngine.InputSystem.Utilities.InternedString(path), value);
		}

		private global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher With(global::UnityEngine.InputSystem.Utilities.InternedString key, object value, bool supportRegex = true)
		{
			if (supportRegex && value is string text && !global::System.Linq.Enumerable.All(text, (char ch) => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)) && !double.TryParse(text, out var _))
			{
				value = new global::System.Text.RegularExpressions.Regex(text, global::System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			}
			global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher result2 = this;
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Append(ref result2.m_Patterns, new global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object>(key, value));
			return result2;
		}

		public float MatchPercentage(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
		{
			if (empty)
			{
				return 0f;
			}
			int num = m_Patterns.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.InputSystem.Utilities.InternedString key = m_Patterns[i].Key;
				object value = m_Patterns[i].Value;
				if (key == kInterfaceKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.interfaceName) || !MatchSingleProperty(value, deviceDescription.interfaceName))
					{
						return 0f;
					}
					continue;
				}
				if (key == kDeviceClassKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.deviceClass) || !MatchSingleProperty(value, deviceDescription.deviceClass))
					{
						return 0f;
					}
					continue;
				}
				if (key == kManufacturerKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.manufacturer) || !MatchSingleProperty(value, deviceDescription.manufacturer))
					{
						return 0f;
					}
					continue;
				}
				if (key == kManufacturerContainsKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.manufacturer) || !MatchSinglePropertyContains(value, deviceDescription.manufacturer))
					{
						return 0f;
					}
					continue;
				}
				if (key == kProductKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.product) || !MatchSingleProperty(value, deviceDescription.product))
					{
						return 0f;
					}
					continue;
				}
				if (key == kVersionKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.version) || !MatchSingleProperty(value, deviceDescription.version))
					{
						return 0f;
					}
					continue;
				}
				if (string.IsNullOrEmpty(deviceDescription.capabilities))
				{
					return 0f;
				}
				global::UnityEngine.InputSystem.Utilities.JsonParser jsonParser = new global::UnityEngine.InputSystem.Utilities.JsonParser(deviceDescription.capabilities);
				if (!jsonParser.NavigateToProperty(key.ToString()) || !jsonParser.CurrentPropertyHasValueEqualTo(new global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValue
				{
					type = global::UnityEngine.InputSystem.Utilities.JsonParser.JsonValueType.Any,
					anyValue = value
				}))
				{
					return 0f;
				}
			}
			int numPropertiesIn = GetNumPropertiesIn(deviceDescription);
			float num2 = 1f / (float)numPropertiesIn;
			return (float)num * num2;
		}

		private static bool MatchSingleProperty(object pattern, string value)
		{
			if (pattern is string strA)
			{
				return string.Compare(strA, value, global::System.StringComparison.OrdinalIgnoreCase) == 0;
			}
			if (pattern is global::System.Text.RegularExpressions.Regex regex)
			{
				return regex.IsMatch(value);
			}
			return false;
		}

		private static bool MatchSinglePropertyContains(object pattern, string value)
		{
			if (pattern is string value2)
			{
				return value.Contains(value2, global::System.StringComparison.OrdinalIgnoreCase);
			}
			return false;
		}

		private static int GetNumPropertiesIn(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(description.interfaceName))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.deviceClass))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.manufacturer))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.product))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.version))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.capabilities))
			{
				num++;
			}
			return num;
		}

		public static global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher FromDeviceDescription(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
		{
			global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher result = default(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher);
			if (!string.IsNullOrEmpty(deviceDescription.interfaceName))
			{
				result = result.WithInterface(deviceDescription.interfaceName, supportRegex: false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.deviceClass))
			{
				result = result.WithDeviceClass(deviceDescription.deviceClass, supportRegex: false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.manufacturer))
			{
				result = result.WithManufacturer(deviceDescription.manufacturer, supportRegex: false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.product))
			{
				result = result.WithProduct(deviceDescription.product, supportRegex: false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.version))
			{
				result = result.WithVersion(deviceDescription.version, supportRegex: false);
			}
			return result;
		}

		public override string ToString()
		{
			if (empty)
			{
				return "<empty>";
			}
			string text = string.Empty;
			global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object>[] array = m_Patterns;
			for (int i = 0; i < array.Length; i++)
			{
				global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object> keyValuePair = array[i];
				text = ((text.Length <= 0) ? (text + $"{keyValuePair.Key}={keyValuePair.Value}") : (text + $",{keyValuePair.Key}={keyValuePair.Value}"));
			}
			return text;
		}

		public bool Equals(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher other)
		{
			if (m_Patterns == other.m_Patterns)
			{
				return true;
			}
			if (m_Patterns == null || other.m_Patterns == null)
			{
				return false;
			}
			if (m_Patterns.Length != other.m_Patterns.Length)
			{
				return false;
			}
			for (int i = 0; i < m_Patterns.Length; i++)
			{
				global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object> keyValuePair = m_Patterns[i];
				bool flag = false;
				for (int j = 0; j < m_Patterns.Length; j++)
				{
					global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, object> keyValuePair2 = other.m_Patterns[j];
					if (!(keyValuePair.Key != keyValuePair2.Key))
					{
						if (!keyValuePair.Value.Equals(keyValuePair2.Value))
						{
							return false;
						}
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher other)
			{
				return Equals(other);
			}
			return false;
		}

		public static bool operator ==(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher left, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher left, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			if (m_Patterns == null)
			{
				return 0;
			}
			return m_Patterns.GetHashCode();
		}
	}
}
