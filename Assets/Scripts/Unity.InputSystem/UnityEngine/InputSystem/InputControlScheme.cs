namespace UnityEngine.InputSystem
{
	[global::System.Serializable]
	public struct InputControlScheme : global::System.IEquatable<global::UnityEngine.InputSystem.InputControlScheme>
	{
		public struct MatchResult : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match>, global::System.Collections.IEnumerable, global::System.IDisposable
		{
			internal enum Result
			{
				AllSatisfied = 0,
				MissingRequired = 1,
				MissingOptional = 2
			}

			public struct Match
			{
				internal int m_RequirementIndex;

				internal global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[] m_Requirements;

				internal global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> m_Controls;

				public global::UnityEngine.InputSystem.InputControl control => m_Controls[m_RequirementIndex];

				public global::UnityEngine.InputSystem.InputDevice device => control?.device;

				public int requirementIndex => m_RequirementIndex;

				public global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement requirement => m_Requirements[m_RequirementIndex];

				public bool isOptional => requirement.isOptional;
			}

			private struct Enumerator : global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match>, global::System.Collections.IEnumerator, global::System.IDisposable
			{
				internal int m_Index;

				internal global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[] m_Requirements;

				internal global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> m_Controls;

				public global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match Current
				{
					get
					{
						if (m_Requirements == null || m_Index < 0 || m_Index >= m_Requirements.Length)
						{
							throw new global::System.InvalidOperationException("Enumerator is not valid");
						}
						return new global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match
						{
							m_RequirementIndex = m_Index,
							m_Requirements = m_Requirements,
							m_Controls = m_Controls
						};
					}
				}

				object global::System.Collections.IEnumerator.Current => Current;

				public bool MoveNext()
				{
					m_Index++;
					if (m_Requirements != null)
					{
						return m_Index < m_Requirements.Length;
					}
					return false;
				}

				public void Reset()
				{
					m_Index = -1;
				}

				public void Dispose()
				{
				}
			}

			internal global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result m_Result;

			internal float m_Score;

			internal global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> m_Devices;

			internal global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> m_Controls;

			internal global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[] m_Requirements;

			public float score => m_Score;

			public bool isSuccessfulMatch => m_Result != global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.MissingRequired;

			public bool hasMissingRequiredDevices => m_Result == global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.MissingRequired;

			public bool hasMissingOptionalDevices => m_Result == global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.MissingOptional;

			public global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputDevice> devices
			{
				get
				{
					if (m_Devices.Count == 0 && !hasMissingRequiredDevices)
					{
						int count = m_Controls.Count;
						if (count != 0)
						{
							m_Devices.Capacity = count;
							for (int i = 0; i < count; i++)
							{
								global::UnityEngine.InputSystem.InputControl inputControl = m_Controls[i];
								if (inputControl != null)
								{
									global::UnityEngine.InputSystem.InputDevice device = inputControl.device;
									if (!m_Devices.Contains(device))
									{
										m_Devices.Add(device);
									}
								}
							}
						}
					}
					return m_Devices;
				}
			}

			public global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match this[int index]
			{
				get
				{
					if (index < 0 || m_Requirements == null || index >= m_Requirements.Length)
					{
						throw new global::System.ArgumentOutOfRangeException("index");
					}
					return new global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match
					{
						m_RequirementIndex = index,
						m_Requirements = m_Requirements,
						m_Controls = m_Controls
					};
				}
			}

			public global::System.Collections.Generic.IEnumerator<global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Match> GetEnumerator()
			{
				return new global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Enumerator
				{
					m_Index = -1,
					m_Requirements = m_Requirements,
					m_Controls = m_Controls
				};
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Dispose()
			{
				m_Controls.Dispose();
				m_Devices.Dispose();
			}
		}

		[global::System.Serializable]
		public struct DeviceRequirement : global::System.IEquatable<global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement>
		{
			[global::System.Flags]
			internal enum Flags
			{
				None = 0,
				Optional = 1,
				Or = 2
			}

			[global::UnityEngine.SerializeField]
			internal string m_ControlPath;

			[global::UnityEngine.SerializeField]
			internal global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags m_Flags;

			public string controlPath
			{
				get
				{
					return m_ControlPath;
				}
				set
				{
					m_ControlPath = value;
				}
			}

			public bool isOptional
			{
				get
				{
					return (m_Flags & global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Optional) != 0;
				}
				set
				{
					if (value)
					{
						m_Flags |= global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Optional;
					}
					else
					{
						m_Flags &= ~global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Optional;
					}
				}
			}

			public bool isAND
			{
				get
				{
					return !isOR;
				}
				set
				{
					isOR = !value;
				}
			}

			public bool isOR
			{
				get
				{
					return (m_Flags & global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Or) != 0;
				}
				set
				{
					if (value)
					{
						m_Flags |= global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Or;
					}
					else
					{
						m_Flags &= ~global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement.Flags.Or;
					}
				}
			}

			public override string ToString()
			{
				if (!string.IsNullOrEmpty(controlPath))
				{
					if (isOptional)
					{
						return controlPath + " (Optional)";
					}
					return controlPath + " (Required)";
				}
				return base.ToString();
			}

			public bool Equals(global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement other)
			{
				if (string.Equals(m_ControlPath, other.m_ControlPath) && m_Flags == other.m_Flags && string.Equals(controlPath, other.controlPath))
				{
					return isOptional == other.isOptional;
				}
				return false;
			}

			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (obj is global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement)
				{
					return Equals((global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement)obj);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return (((((((m_ControlPath != null) ? m_ControlPath.GetHashCode() : 0) * 397) ^ m_Flags.GetHashCode()) * 397) ^ ((controlPath != null) ? controlPath.GetHashCode() : 0)) * 397) ^ isOptional.GetHashCode();
			}

			public static bool operator ==(global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement left, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement right)
			{
				return left.Equals(right);
			}

			public static bool operator !=(global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement left, global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement right)
			{
				return !left.Equals(right);
			}
		}

		[global::System.Serializable]
		internal struct SchemeJson
		{
			[global::System.Serializable]
			public struct DeviceJson
			{
				public string devicePath;

				public bool isOptional;

				public bool isOR;

				public global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement ToDeviceEntry()
				{
					return new global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement
					{
						controlPath = devicePath,
						isOptional = isOptional,
						isOR = isOR
					};
				}

				public static global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson From(global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement requirement)
				{
					return new global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson
					{
						devicePath = requirement.controlPath,
						isOptional = requirement.isOptional,
						isOR = requirement.isOR
					};
				}
			}

			public string name;

			public string bindingGroup;

			public global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson[] devices;

			public global::UnityEngine.InputSystem.InputControlScheme ToScheme()
			{
				global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[] array = null;
				if (devices != null && devices.Length != 0)
				{
					int num = devices.Length;
					array = new global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = devices[i].ToDeviceEntry();
					}
				}
				return new global::UnityEngine.InputSystem.InputControlScheme
				{
					m_Name = (string.IsNullOrEmpty(name) ? null : name),
					m_BindingGroup = (string.IsNullOrEmpty(bindingGroup) ? null : bindingGroup),
					m_DeviceRequirements = array
				};
			}

			public static global::UnityEngine.InputSystem.InputControlScheme.SchemeJson ToJson(global::UnityEngine.InputSystem.InputControlScheme scheme)
			{
				global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson[] array = null;
				if (scheme.m_DeviceRequirements != null && scheme.m_DeviceRequirements.Length != 0)
				{
					int num = scheme.m_DeviceRequirements.Length;
					array = new global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = global::UnityEngine.InputSystem.InputControlScheme.SchemeJson.DeviceJson.From(scheme.m_DeviceRequirements[i]);
					}
				}
				return new global::UnityEngine.InputSystem.InputControlScheme.SchemeJson
				{
					name = scheme.m_Name,
					bindingGroup = scheme.m_BindingGroup,
					devices = array
				};
			}

			public static global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[] ToJson(global::UnityEngine.InputSystem.InputControlScheme[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int num = schemes.Length;
				global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[] array = new global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = ToJson(schemes[i]);
				}
				return array;
			}

			public static global::UnityEngine.InputSystem.InputControlScheme[] ToSchemes(global::UnityEngine.InputSystem.InputControlScheme.SchemeJson[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int num = schemes.Length;
				global::UnityEngine.InputSystem.InputControlScheme[] array = new global::UnityEngine.InputSystem.InputControlScheme[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = schemes[i].ToScheme();
				}
				return array;
			}
		}

		[global::UnityEngine.SerializeField]
		internal string m_Name;

		[global::UnityEngine.SerializeField]
		internal string m_BindingGroup;

		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[] m_DeviceRequirements;

		public string name => m_Name;

		public string bindingGroup
		{
			get
			{
				return m_BindingGroup;
			}
			set
			{
				m_BindingGroup = value;
			}
		}

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement> deviceRequirements => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement>(m_DeviceRequirements);

		public InputControlScheme(string name, global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement> devices = null, string bindingGroup = null)
		{
			this = default(global::UnityEngine.InputSystem.InputControlScheme);
			if (string.IsNullOrEmpty(name))
			{
				throw new global::System.ArgumentNullException("name");
			}
			SetNameAndBindingGroup(name, bindingGroup);
			m_DeviceRequirements = null;
			if (devices != null)
			{
				m_DeviceRequirements = global::System.Linq.Enumerable.ToArray(devices);
				if (m_DeviceRequirements.Length == 0)
				{
					m_DeviceRequirements = null;
				}
			}
		}

		internal void SetNameAndBindingGroup(string name, string bindingGroup = null)
		{
			m_Name = name;
			if (!string.IsNullOrEmpty(bindingGroup))
			{
				m_BindingGroup = bindingGroup;
			}
			else
			{
				m_BindingGroup = (name.Contains(';') ? name.Replace(";", "") : name);
			}
		}

		public static global::UnityEngine.InputSystem.InputControlScheme? FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, global::UnityEngine.InputSystem.InputDevice mustIncludeDevice = null, bool allowUnsuccesfulMatch = false) where TDevices : global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.InputSystem.InputDevice> where TSchemes : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControlScheme>
		{
			if (devices == null)
			{
				throw new global::System.ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new global::System.ArgumentNullException("schemes");
			}
			if (!FindControlSchemeForDevices(devices, schemes, out var controlScheme, out var matchResult, mustIncludeDevice, allowUnsuccesfulMatch))
			{
				return null;
			}
			matchResult.Dispose();
			return controlScheme;
		}

		public static bool FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, out global::UnityEngine.InputSystem.InputControlScheme controlScheme, out global::UnityEngine.InputSystem.InputControlScheme.MatchResult matchResult, global::UnityEngine.InputSystem.InputDevice mustIncludeDevice = null, bool allowUnsuccessfulMatch = false) where TDevices : global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.InputSystem.InputDevice> where TSchemes : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControlScheme>
		{
			if (devices == null)
			{
				throw new global::System.ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new global::System.ArgumentNullException("schemes");
			}
			global::UnityEngine.InputSystem.InputControlScheme.MatchResult? matchResult2 = null;
			global::UnityEngine.InputSystem.InputControlScheme? inputControlScheme = null;
			foreach (global::UnityEngine.InputSystem.InputControlScheme item in schemes)
			{
				global::UnityEngine.InputSystem.InputControlScheme.MatchResult value = item.PickDevicesFrom(devices, mustIncludeDevice);
				if (!value.isSuccessfulMatch && (!allowUnsuccessfulMatch || value.score <= 0f))
				{
					value.Dispose();
					continue;
				}
				if (mustIncludeDevice != null && !value.devices.Contains(mustIncludeDevice))
				{
					value.Dispose();
					continue;
				}
				if (matchResult2.HasValue && matchResult2.Value.score >= value.score)
				{
					value.Dispose();
					continue;
				}
				matchResult2?.Dispose();
				matchResult2 = value;
				inputControlScheme = item;
			}
			matchResult = matchResult2.GetValueOrDefault();
			controlScheme = inputControlScheme.GetValueOrDefault();
			return matchResult2.HasValue;
		}

		public static global::UnityEngine.InputSystem.InputControlScheme? FindControlSchemeForDevice<TSchemes>(global::UnityEngine.InputSystem.InputDevice device, TSchemes schemes) where TSchemes : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.InputControlScheme>
		{
			if (schemes == null)
			{
				throw new global::System.ArgumentNullException("schemes");
			}
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			return FindControlSchemeForDevices(new global::UnityEngine.InputSystem.Utilities.OneOrMore<global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputDevice>>(device), schemes);
		}

		public bool SupportsDevice(global::UnityEngine.InputSystem.InputDevice device)
		{
			if (device == null)
			{
				throw new global::System.ArgumentNullException("device");
			}
			for (int i = 0; i < m_DeviceRequirements.Length; i++)
			{
				if (global::UnityEngine.InputSystem.InputControlPath.TryFindControl(device, m_DeviceRequirements[i].controlPath) != null)
				{
					return true;
				}
			}
			return false;
		}

		public global::UnityEngine.InputSystem.InputControlScheme.MatchResult PickDevicesFrom<TDevices>(TDevices devices, global::UnityEngine.InputSystem.InputDevice favorDevice = null) where TDevices : global::System.Collections.Generic.IReadOnlyList<global::UnityEngine.InputSystem.InputDevice>
		{
			if (m_DeviceRequirements == null || m_DeviceRequirements.Length == 0)
			{
				return new global::UnityEngine.InputSystem.InputControlScheme.MatchResult
				{
					m_Result = global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.AllSatisfied,
					m_Score = 0.5f
				};
			}
			bool flag = true;
			bool flag2 = true;
			int num = m_DeviceRequirements.Length;
			float num2 = 0f;
			global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl> controls = new global::UnityEngine.InputSystem.InputControlList<global::UnityEngine.InputSystem.InputControl>(global::Unity.Collections.Allocator.Persistent, num);
			try
			{
				bool flag3 = false;
				bool flag4 = false;
				for (int i = 0; i < num; i++)
				{
					bool isOR = m_DeviceRequirements[i].isOR;
					bool isOptional = m_DeviceRequirements[i].isOptional;
					if (isOR && flag3)
					{
						controls.Add(null);
						continue;
					}
					string controlPath = m_DeviceRequirements[i].controlPath;
					if (string.IsNullOrEmpty(controlPath))
					{
						num2 += 1f;
						controls.Add(null);
						continue;
					}
					global::UnityEngine.InputSystem.InputControl inputControl = null;
					for (int j = 0; j < devices.Count; j++)
					{
						global::UnityEngine.InputSystem.InputDevice inputDevice = devices[j];
						if (favorDevice != null)
						{
							if (j == 0)
							{
								inputDevice = favorDevice;
							}
							else if (inputDevice == favorDevice)
							{
								inputDevice = devices[0];
							}
						}
						global::UnityEngine.InputSystem.InputControl inputControl2 = global::UnityEngine.InputSystem.InputControlPath.TryFindControl(inputDevice, controlPath);
						if (inputControl2 != null && !controls.Contains(inputControl2))
						{
							inputControl = inputControl2;
							global::UnityEngine.InputSystem.Utilities.InternedString firstLayout = new global::UnityEngine.InputSystem.Utilities.InternedString(global::UnityEngine.InputSystem.InputControlPath.TryGetDeviceLayout(controlPath));
							if (firstLayout.IsEmpty())
							{
								num2 += 1f;
								break;
							}
							global::UnityEngine.InputSystem.Utilities.InternedString layout = inputControl2.device.m_Layout;
							num2 = ((!global::UnityEngine.InputSystem.Layouts.InputControlLayout.s_Layouts.ComputeDistanceInInheritanceHierarchy(firstLayout, layout, out var distance)) ? (num2 + 1f) : (num2 + (1f + 1f / (float)(global::System.Math.Abs(distance) + 1))));
							break;
						}
					}
					if (i + 1 < num && m_DeviceRequirements[i + 1].isOR)
					{
						if (inputControl != null)
						{
							flag3 = true;
						}
						else if (!isOptional)
						{
							flag4 = true;
						}
					}
					else if (isOR && i == num - 1)
					{
						if (inputControl == null)
						{
							if (flag4)
							{
								flag = false;
							}
							else
							{
								flag2 = false;
							}
						}
					}
					else
					{
						if (inputControl == null)
						{
							if (isOptional)
							{
								flag2 = false;
							}
							else
							{
								flag = false;
							}
						}
						if (i > 0 && m_DeviceRequirements[i - 1].isOR)
						{
							if (!flag3)
							{
								if (flag4)
								{
									flag = false;
								}
								else
								{
									flag2 = false;
								}
							}
							flag3 = false;
						}
					}
					controls.Add(inputControl);
				}
			}
			catch (global::System.Exception)
			{
				controls.Dispose();
				throw;
			}
			return new global::UnityEngine.InputSystem.InputControlScheme.MatchResult
			{
				m_Result = ((!flag) ? global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.MissingRequired : ((!flag2) ? global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.MissingOptional : global::UnityEngine.InputSystem.InputControlScheme.MatchResult.Result.AllSatisfied)),
				m_Controls = controls,
				m_Requirements = m_DeviceRequirements,
				m_Score = num2
			};
		}

		public bool Equals(global::UnityEngine.InputSystem.InputControlScheme other)
		{
			if (!string.Equals(m_Name, other.m_Name, global::System.StringComparison.InvariantCultureIgnoreCase) || !string.Equals(m_BindingGroup, other.m_BindingGroup, global::System.StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (m_DeviceRequirements == null || m_DeviceRequirements.Length == 0)
			{
				if (other.m_DeviceRequirements != null)
				{
					return other.m_DeviceRequirements.Length == 0;
				}
				return true;
			}
			if (other.m_DeviceRequirements == null || m_DeviceRequirements.Length != other.m_DeviceRequirements.Length)
			{
				return false;
			}
			int num = m_DeviceRequirements.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement deviceRequirement = m_DeviceRequirements[i];
				bool flag = false;
				for (int j = 0; j < num; j++)
				{
					if (other.m_DeviceRequirements[j] == deviceRequirement)
					{
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
			if (obj is global::UnityEngine.InputSystem.InputControlScheme)
			{
				return Equals((global::UnityEngine.InputSystem.InputControlScheme)obj);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((((m_Name != null) ? m_Name.GetHashCode() : 0) * 397) ^ ((m_BindingGroup != null) ? m_BindingGroup.GetHashCode() : 0)) * 397) ^ ((m_DeviceRequirements != null) ? m_DeviceRequirements.GetHashCode() : 0);
		}

		public override string ToString()
		{
			if (string.IsNullOrEmpty(m_Name))
			{
				return base.ToString();
			}
			if (m_DeviceRequirements == null)
			{
				return m_Name;
			}
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append(m_Name);
			stringBuilder.Append('(');
			bool flag = true;
			global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement[] array = m_DeviceRequirements;
			foreach (global::UnityEngine.InputSystem.InputControlScheme.DeviceRequirement deviceRequirement in array)
			{
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(deviceRequirement.controlPath);
				flag = false;
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		public static bool operator ==(global::UnityEngine.InputSystem.InputControlScheme left, global::UnityEngine.InputSystem.InputControlScheme right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.InputControlScheme left, global::UnityEngine.InputSystem.InputControlScheme right)
		{
			return !left.Equals(right);
		}
	}
}
