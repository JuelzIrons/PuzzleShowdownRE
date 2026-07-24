namespace UnityEngine.InputSystem.Layouts
{
	public class InputControlLayout
	{
		public struct ControlItem
		{
			[global::System.Flags]
			private enum Flags
			{
				isModifyingExistingControl = 1,
				IsNoisy = 2,
				IsSynthetic = 4,
				IsFirstDefinedInThisLayout = 8,
				DontReset = 0x10
			}

			public global::UnityEngine.InputSystem.Utilities.InternedString name { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.InternedString layout { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.InternedString variants { get; internal set; }

			public string useStateFrom { get; internal set; }

			public string displayName { get; internal set; }

			public string shortDisplayName { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> usages { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> aliases { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue> parameters { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NameAndParameters> processors { get; internal set; }

			public uint offset { get; internal set; }

			public uint bit { get; internal set; }

			public uint sizeInBits { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.FourCC format { get; internal set; }

			private global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags flags { get; set; }

			public int arraySize { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.PrimitiveValue defaultState { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.PrimitiveValue minValue { get; internal set; }

			public global::UnityEngine.InputSystem.Utilities.PrimitiveValue maxValue { get; internal set; }

			public bool isModifyingExistingControl
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.isModifyingExistingControl) == global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
				internal set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
					}
				}
			}

			public bool isNoisy
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsNoisy) == global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsNoisy;
				}
				internal set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsNoisy;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsNoisy;
					}
				}
			}

			public bool isSynthetic
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsSynthetic) == global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
				internal set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsSynthetic;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsSynthetic;
					}
				}
			}

			public bool dontReset
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.DontReset) == global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.DontReset;
				}
				internal set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.DontReset;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.DontReset;
					}
				}
			}

			public bool isFirstDefinedInThisLayout
			{
				get
				{
					return (flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout) != 0;
				}
				internal set
				{
					if (value)
					{
						flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
					}
					else
					{
						flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
					}
				}
			}

			public bool isArray => arraySize != 0;

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem Merge(global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem other)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem result = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem
				{
					name = name,
					isModifyingExistingControl = isModifyingExistingControl,
					displayName = (string.IsNullOrEmpty(displayName) ? other.displayName : displayName),
					shortDisplayName = (string.IsNullOrEmpty(shortDisplayName) ? other.shortDisplayName : shortDisplayName),
					layout = (layout.IsEmpty() ? other.layout : layout),
					variants = (variants.IsEmpty() ? other.variants : variants),
					useStateFrom = (useStateFrom ?? other.useStateFrom),
					arraySize = ((!isArray) ? other.arraySize : arraySize),
					isNoisy = (isNoisy || other.isNoisy),
					dontReset = (dontReset || other.dontReset),
					isSynthetic = (isSynthetic || other.isSynthetic),
					isFirstDefinedInThisLayout = false
				};
				if (offset != uint.MaxValue)
				{
					result.offset = offset;
				}
				else
				{
					result.offset = other.offset;
				}
				if (bit != uint.MaxValue)
				{
					result.bit = bit;
				}
				else
				{
					result.bit = other.bit;
				}
				if (format != 0)
				{
					result.format = format;
				}
				else
				{
					result.format = other.format;
				}
				if (sizeInBits != 0)
				{
					result.sizeInBits = sizeInBits;
				}
				else
				{
					result.sizeInBits = other.sizeInBits;
				}
				if (aliases.Count > 0)
				{
					result.aliases = aliases;
				}
				else
				{
					result.aliases = other.aliases;
				}
				if (usages.Count > 0)
				{
					result.usages = usages;
				}
				else
				{
					result.usages = other.usages;
				}
				if (parameters.Count == 0)
				{
					result.parameters = other.parameters;
				}
				else
				{
					result.parameters = parameters;
				}
				if (processors.Count == 0)
				{
					result.processors = other.processors;
				}
				else
				{
					result.processors = processors;
				}
				if (!string.IsNullOrEmpty(displayName))
				{
					result.displayName = displayName;
				}
				else
				{
					result.displayName = other.displayName;
				}
				if (!defaultState.isEmpty)
				{
					result.defaultState = defaultState;
				}
				else
				{
					result.defaultState = other.defaultState;
				}
				if (!minValue.isEmpty)
				{
					result.minValue = minValue;
				}
				else
				{
					result.minValue = other.minValue;
				}
				if (!maxValue.isEmpty)
				{
					result.maxValue = maxValue;
				}
				else
				{
					result.maxValue = other.maxValue;
				}
				return result;
			}
		}

		public class Builder
		{
			public struct ControlBuilder
			{
				internal global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder builder;

				internal int index;

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithDisplayName(string displayName)
				{
					builder.m_Controls[index].displayName = displayName;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithLayout(string layout)
				{
					if (string.IsNullOrEmpty(layout))
					{
						throw new global::System.ArgumentException("Layout name cannot be null or empty", "layout");
					}
					builder.m_Controls[index].layout = new global::UnityEngine.InputSystem.Utilities.InternedString(layout);
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithFormat(global::UnityEngine.InputSystem.Utilities.FourCC format)
				{
					builder.m_Controls[index].format = format;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithFormat(string format)
				{
					return WithFormat(new global::UnityEngine.InputSystem.Utilities.FourCC(format));
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithByteOffset(uint offset)
				{
					builder.m_Controls[index].offset = offset;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithBitOffset(uint bit)
				{
					builder.m_Controls[index].bit = bit;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder IsSynthetic(bool value)
				{
					builder.m_Controls[index].isSynthetic = value;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder IsNoisy(bool value)
				{
					builder.m_Controls[index].isNoisy = value;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder DontReset(bool value)
				{
					builder.m_Controls[index].dontReset = value;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithSizeInBits(uint sizeInBits)
				{
					builder.m_Controls[index].sizeInBits = sizeInBits;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithRange(float minValue, float maxValue)
				{
					builder.m_Controls[index].minValue = minValue;
					builder.m_Controls[index].maxValue = maxValue;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithUsages(params global::UnityEngine.InputSystem.Utilities.InternedString[] usages)
				{
					if (usages == null || usages.Length == 0)
					{
						return this;
					}
					for (int i = 0; i < usages.Length; i++)
					{
						if (usages[i].IsEmpty())
						{
							throw new global::System.ArgumentException($"Empty usage entry at index {i} for control '{builder.m_Controls[index].name}' in layout '{builder.name}'", "usages");
						}
					}
					builder.m_Controls[index].usages = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(usages);
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithUsages(global::System.Collections.Generic.IEnumerable<string> usages)
				{
					global::UnityEngine.InputSystem.Utilities.InternedString[] usages2 = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(usages, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x)));
					return WithUsages(usages2);
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithUsages(params string[] usages)
				{
					return WithUsages((global::System.Collections.Generic.IEnumerable<string>)usages);
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithParameters(string parameters)
				{
					if (string.IsNullOrEmpty(parameters))
					{
						return this;
					}
					global::UnityEngine.InputSystem.Utilities.NamedValue[] array = global::UnityEngine.InputSystem.Utilities.NamedValue.ParseMultiple(parameters);
					builder.m_Controls[index].parameters = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue>(array);
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithProcessors(string processors)
				{
					if (string.IsNullOrEmpty(processors))
					{
						return this;
					}
					global::UnityEngine.InputSystem.Utilities.NameAndParameters[] array = global::System.Linq.Enumerable.ToArray(global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseMultiple(processors));
					builder.m_Controls[index].processors = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NameAndParameters>(array);
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder WithDefaultState(global::UnityEngine.InputSystem.Utilities.PrimitiveValue value)
				{
					builder.m_Controls[index].defaultState = value;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder UsingStateFrom(string path)
				{
					if (string.IsNullOrEmpty(path))
					{
						return this;
					}
					builder.m_Controls[index].useStateFrom = path;
					return this;
				}

				public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder AsArrayOfControlsWithSize(int arraySize)
				{
					builder.m_Controls[index].arraySize = arraySize;
					return this;
				}
			}

			private string m_ExtendsLayout;

			private int m_ControlCount;

			private global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] m_Controls;

			public string name { get; set; }

			public string displayName { get; set; }

			public global::System.Type type { get; set; }

			public global::UnityEngine.InputSystem.Utilities.FourCC stateFormat { get; set; }

			public int stateSizeInBytes { get; set; }

			public string extendsLayout
			{
				get
				{
					return m_ExtendsLayout;
				}
				set
				{
					if (!string.IsNullOrEmpty(value))
					{
						m_ExtendsLayout = value;
					}
					else
					{
						m_ExtendsLayout = null;
					}
				}
			}

			public bool? updateBeforeRender { get; set; }

			public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controls => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>(m_Controls, 0, m_ControlCount);

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder AddControl(string name)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new global::System.ArgumentException(name);
				}
				int index = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendWithCapacity(ref m_Controls, ref m_ControlCount, new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem
				{
					name = new global::UnityEngine.InputSystem.Utilities.InternedString(name),
					isModifyingExistingControl = (name.IndexOf('/') != -1),
					offset = uint.MaxValue,
					bit = uint.MaxValue
				});
				return new global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder
				{
					builder = this,
					index = index
				};
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithName(string name)
			{
				this.name = name;
				return this;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithDisplayName(string displayName)
			{
				this.displayName = displayName;
				return this;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithType<T>() where T : global::UnityEngine.InputSystem.InputControl
			{
				type = typeof(T);
				return this;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithFormat(global::UnityEngine.InputSystem.Utilities.FourCC format)
			{
				stateFormat = format;
				return this;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithFormat(string format)
			{
				return WithFormat(new global::UnityEngine.InputSystem.Utilities.FourCC(format));
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder WithSizeInBytes(int sizeInBytes)
			{
				stateSizeInBytes = sizeInBytes;
				return this;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder Extend(string baseLayoutName)
			{
				extendsLayout = baseLayoutName;
				return this;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout Build()
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] destinationArray = null;
				if (m_ControlCount > 0)
				{
					destinationArray = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[m_ControlCount];
					global::System.Array.Copy(m_Controls, destinationArray, m_ControlCount);
				}
				return new global::UnityEngine.InputSystem.Layouts.InputControlLayout(new global::UnityEngine.InputSystem.Utilities.InternedString(name), (type == null && string.IsNullOrEmpty(extendsLayout)) ? typeof(global::UnityEngine.InputSystem.InputDevice) : type)
				{
					m_DisplayName = displayName,
					m_StateFormat = stateFormat,
					m_StateSizeInBytes = stateSizeInBytes,
					m_BaseLayouts = ((!string.IsNullOrEmpty(extendsLayout)) ? new global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString>(new global::UnityEngine.InputSystem.Utilities.InternedString(extendsLayout)) : default(global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString>)),
					m_Controls = destinationArray,
					m_UpdateBeforeRender = updateBeforeRender
				};
			}
		}

		[global::System.Flags]
		private enum Flags
		{
			IsGenericTypeOfDevice = 1,
			HideInUI = 2,
			IsOverride = 4,
			CanRunInBackground = 8,
			CanRunInBackgroundIsSet = 0x10,
			IsNoisy = 0x20
		}

		[global::System.Serializable]
		internal struct LayoutJsonNameAndDescriptorOnly
		{
			public string name;

			public string extend;

			public string[] extendMultiple;

			public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.MatcherJson device;
		}

		[global::System.Serializable]
		private struct LayoutJson
		{
			public string name;

			public string extend;

			public string[] extendMultiple;

			public string format;

			public string beforeRender;

			public string runInBackground;

			public string[] commonUsages;

			public string displayName;

			public string description;

			public string type;

			public string variant;

			public bool isGenericTypeOfDevice;

			public bool hideInUI;

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[] controls;

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout ToLayout()
			{
				global::System.Type type = null;
				if (!string.IsNullOrEmpty(this.type))
				{
					type = global::System.Type.GetType(this.type, throwOnError: false);
					if (type == null)
					{
						global::UnityEngine.Debug.Log("Cannot find type '" + this.type + "' used by layout '" + name + "'; falling back to using InputDevice");
						type = typeof(global::UnityEngine.InputSystem.InputDevice);
					}
					else if (!typeof(global::UnityEngine.InputSystem.InputControl).IsAssignableFrom(type))
					{
						throw new global::System.InvalidOperationException("'" + this.type + "' used by layout '" + name + "' is not an InputControl");
					}
				}
				else if (string.IsNullOrEmpty(extend))
				{
					type = typeof(global::UnityEngine.InputSystem.InputDevice);
				}
				global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout = new global::UnityEngine.InputSystem.Layouts.InputControlLayout(name, type)
				{
					m_DisplayName = displayName,
					m_Description = description,
					isGenericTypeOfDevice = isGenericTypeOfDevice,
					hideInUI = hideInUI,
					m_Variants = new global::UnityEngine.InputSystem.Utilities.InternedString(variant),
					m_CommonUsages = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Select(commonUsages, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x))
				};
				if (!string.IsNullOrEmpty(format))
				{
					inputControlLayout.m_StateFormat = new global::UnityEngine.InputSystem.Utilities.FourCC(format);
				}
				if (!string.IsNullOrEmpty(extend))
				{
					inputControlLayout.m_BaseLayouts.Append(new global::UnityEngine.InputSystem.Utilities.InternedString(extend));
				}
				if (extendMultiple != null)
				{
					string[] array = extendMultiple;
					foreach (string text in array)
					{
						inputControlLayout.m_BaseLayouts.Append(new global::UnityEngine.InputSystem.Utilities.InternedString(text));
					}
				}
				if (!string.IsNullOrEmpty(beforeRender))
				{
					string text2 = beforeRender.ToLowerInvariant();
					if (text2 == "ignore")
					{
						inputControlLayout.m_UpdateBeforeRender = false;
					}
					else
					{
						if (!(text2 == "update"))
						{
							throw new global::System.InvalidOperationException("Invalid beforeRender setting '" + beforeRender + "' (should be 'ignore' or 'update')");
						}
						inputControlLayout.m_UpdateBeforeRender = true;
					}
				}
				if (!string.IsNullOrEmpty(runInBackground))
				{
					string text3 = runInBackground.ToLowerInvariant();
					if (text3 == "enabled")
					{
						inputControlLayout.canRunInBackground = true;
					}
					else
					{
						if (!(text3 == "disabled"))
						{
							throw new global::System.InvalidOperationException("Invalid runInBackground setting '" + beforeRender + "' (should be 'enabled' or 'disabled')");
						}
						inputControlLayout.canRunInBackground = false;
					}
				}
				if (controls != null)
				{
					global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>();
					global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[] array2 = controls;
					foreach (global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson obj in array2)
					{
						if (string.IsNullOrEmpty(obj.name))
						{
							throw new global::System.InvalidOperationException("Control with no name in layout '" + name);
						}
						global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item = obj.ToLayout();
						list.Add(item);
					}
					inputControlLayout.m_Controls = list.ToArray();
				}
				return inputControlLayout;
			}

			public static global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJson FromLayout(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout)
			{
				return new global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJson
				{
					name = layout.m_Name,
					type = layout.type?.AssemblyQualifiedName,
					variant = layout.m_Variants,
					displayName = layout.m_DisplayName,
					description = layout.m_Description,
					isGenericTypeOfDevice = layout.isGenericTypeOfDevice,
					hideInUI = layout.hideInUI,
					extend = ((layout.m_BaseLayouts.length == 1) ? layout.m_BaseLayouts[0].ToString() : null),
					extendMultiple = ((layout.m_BaseLayouts.length > 1) ? layout.m_BaseLayouts.ToArray((global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString()) : null),
					format = layout.stateFormat.ToString(),
					commonUsages = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Select(layout.m_CommonUsages, (global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString()),
					controls = global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson.FromControlItems(layout.m_Controls),
					beforeRender = ((!layout.m_UpdateBeforeRender.HasValue) ? null : (layout.m_UpdateBeforeRender.Value ? "Update" : "Ignore"))
				};
			}
		}

		[global::System.Serializable]
		private class ControlItemJson
		{
			public string name;

			public string layout;

			public string variants;

			public string usage;

			public string alias;

			public string useStateFrom;

			public uint offset;

			public uint bit;

			public uint sizeInBits;

			public string format;

			public int arraySize;

			public string[] usages;

			public string[] aliases;

			public string parameters;

			public string processors;

			public string displayName;

			public string shortDisplayName;

			public bool noisy;

			public bool dontReset;

			public bool synthetic;

			public string defaultState;

			public string minValue;

			public string maxValue;

			public ControlItemJson()
			{
				offset = uint.MaxValue;
				bit = uint.MaxValue;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem ToLayout()
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem result = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem
				{
					name = new global::UnityEngine.InputSystem.Utilities.InternedString(name),
					layout = new global::UnityEngine.InputSystem.Utilities.InternedString(layout),
					variants = new global::UnityEngine.InputSystem.Utilities.InternedString(variants),
					displayName = displayName,
					shortDisplayName = shortDisplayName,
					offset = offset,
					useStateFrom = useStateFrom,
					bit = bit,
					sizeInBits = sizeInBits,
					isModifyingExistingControl = (name.IndexOf('/') != -1),
					isNoisy = noisy,
					dontReset = dontReset,
					isSynthetic = synthetic,
					isFirstDefinedInThisLayout = true,
					arraySize = arraySize
				};
				if (!string.IsNullOrEmpty(format))
				{
					result.format = new global::UnityEngine.InputSystem.Utilities.FourCC(format);
				}
				if (!string.IsNullOrEmpty(usage) || usages != null)
				{
					global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
					if (!string.IsNullOrEmpty(usage))
					{
						list.Add(usage);
					}
					if (usages != null)
					{
						list.AddRange(usages);
					}
					result.usages = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(list, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x))));
				}
				if (!string.IsNullOrEmpty(alias) || aliases != null)
				{
					global::System.Collections.Generic.List<string> list2 = new global::System.Collections.Generic.List<string>();
					if (!string.IsNullOrEmpty(alias))
					{
						list2.Add(alias);
					}
					if (aliases != null)
					{
						list2.AddRange(aliases);
					}
					result.aliases = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(list2, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x))));
				}
				if (!string.IsNullOrEmpty(parameters))
				{
					result.parameters = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue>(global::UnityEngine.InputSystem.Utilities.NamedValue.ParseMultiple(parameters));
				}
				if (!string.IsNullOrEmpty(processors))
				{
					result.processors = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NameAndParameters>(global::System.Linq.Enumerable.ToArray(global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseMultiple(processors)));
				}
				if (defaultState != null)
				{
					result.defaultState = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(defaultState);
				}
				if (minValue != null)
				{
					result.minValue = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(minValue);
				}
				if (maxValue != null)
				{
					result.maxValue = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(maxValue);
				}
				return result;
			}

			public static global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[] FromControlItems(global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] items)
			{
				if (items == null)
				{
					return null;
				}
				int num = items.Length;
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[] array = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson[num];
				for (int i = 0; i < num; i++)
				{
					global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem = items[i];
					array[i] = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItemJson
					{
						name = controlItem.name,
						layout = controlItem.layout,
						variants = controlItem.variants,
						displayName = controlItem.displayName,
						shortDisplayName = controlItem.shortDisplayName,
						bit = controlItem.bit,
						offset = controlItem.offset,
						sizeInBits = controlItem.sizeInBits,
						format = controlItem.format.ToString(),
						parameters = string.Join(",", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(controlItem.parameters, (global::UnityEngine.InputSystem.Utilities.NamedValue x) => x.ToString()))),
						processors = string.Join(",", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(controlItem.processors, (global::UnityEngine.InputSystem.Utilities.NameAndParameters x) => x.ToString()))),
						usages = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(controlItem.usages, (global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString())),
						aliases = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(controlItem.aliases, (global::UnityEngine.InputSystem.Utilities.InternedString x) => x.ToString())),
						noisy = controlItem.isNoisy,
						dontReset = controlItem.dontReset,
						synthetic = controlItem.isSynthetic,
						arraySize = controlItem.arraySize,
						defaultState = controlItem.defaultState.ToString(),
						minValue = controlItem.minValue.ToString(),
						maxValue = controlItem.maxValue.ToString()
					};
				}
				return array;
			}
		}

		internal struct Collection
		{
			public struct LayoutMatcher
			{
				public global::UnityEngine.InputSystem.Utilities.InternedString layoutName;

				public global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher deviceMatcher;
			}

			public struct PrecompiledLayout
			{
				public global::System.Func<global::UnityEngine.InputSystem.InputDevice> factoryMethod;

				public string metadata;
			}

			public const float kBaseScoreForNonGeneratedLayouts = 1f;

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> layoutTypes;

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, string> layoutStrings;

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout>> layoutBuilders;

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Utilities.InternedString> baseLayoutTable;

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Utilities.InternedString[]> layoutOverrides;

			public global::System.Collections.Generic.HashSet<global::UnityEngine.InputSystem.Utilities.InternedString> layoutOverrideNames;

			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts;

			public global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.LayoutMatcher> layoutMatchers;

			public void Allocate()
			{
				layoutTypes = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type>();
				layoutStrings = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, string>();
				layoutBuilders = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Func<global::UnityEngine.InputSystem.Layouts.InputControlLayout>>();
				baseLayoutTable = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Utilities.InternedString>();
				layoutOverrides = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Utilities.InternedString[]>();
				layoutOverrideNames = new global::System.Collections.Generic.HashSet<global::UnityEngine.InputSystem.Utilities.InternedString>();
				layoutMatchers = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.LayoutMatcher>();
				precompiledLayouts = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.PrecompiledLayout>();
			}

			public global::UnityEngine.InputSystem.Utilities.InternedString TryFindLayoutForType(global::System.Type layoutType)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.InputSystem.Utilities.InternedString, global::System.Type> layoutType2 in layoutTypes)
				{
					if (layoutType2.Value == layoutType)
					{
						return layoutType2.Key;
					}
				}
				return default(global::UnityEngine.InputSystem.Utilities.InternedString);
			}

			public global::UnityEngine.InputSystem.Utilities.InternedString TryFindMatchingLayout(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription)
			{
				float num = 0f;
				global::UnityEngine.InputSystem.Utilities.InternedString result = default(global::UnityEngine.InputSystem.Utilities.InternedString);
				int count = layoutMatchers.Count;
				for (int i = 0; i < count; i++)
				{
					float num2 = layoutMatchers[i].deviceMatcher.MatchPercentage(deviceDescription);
					if (num2 > 0f && !layoutBuilders.ContainsKey(layoutMatchers[i].layoutName))
					{
						num2 += 1f;
					}
					if (num2 > num)
					{
						num = num2;
						result = layoutMatchers[i].layoutName;
					}
				}
				return result;
			}

			public bool HasLayout(global::UnityEngine.InputSystem.Utilities.InternedString name)
			{
				if (!layoutTypes.ContainsKey(name) && !layoutStrings.ContainsKey(name))
				{
					return layoutBuilders.ContainsKey(name);
				}
				return true;
			}

			private global::UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadLayoutInternal(global::UnityEngine.InputSystem.Utilities.InternedString name)
			{
				if (layoutStrings.TryGetValue(name, out var value))
				{
					return FromJson(value);
				}
				if (layoutTypes.TryGetValue(name, out var value2))
				{
					return FromType(name, value2);
				}
				if (layoutBuilders.TryGetValue(name, out var value3))
				{
					return value3() ?? throw new global::System.InvalidOperationException($"Layout builder '{name}' returned null when invoked");
				}
				return null;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout TryLoadLayout(global::UnityEngine.InputSystem.Utilities.InternedString name, global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Layouts.InputControlLayout> table = null)
			{
				if (table != null && table.TryGetValue(name, out var value))
				{
					return value;
				}
				value = TryLoadLayoutInternal(name);
				if (value != null)
				{
					value.m_Name = name;
					if (layoutOverrideNames.Contains(name))
					{
						value.isOverride = true;
					}
					global::UnityEngine.InputSystem.Utilities.InternedString value2 = default(global::UnityEngine.InputSystem.Utilities.InternedString);
					if (!value.isOverride && baseLayoutTable.TryGetValue(name, out value2))
					{
						global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout = TryLoadLayout(value2, table);
						if (inputControlLayout == null)
						{
							throw new global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutNotFoundException($"Cannot find base layout '{value2}' of layout '{name}'");
						}
						value.MergeLayout(inputControlLayout);
						if (value.m_BaseLayouts.length == 0)
						{
							value.m_BaseLayouts.Append(value2);
						}
					}
					if (layoutOverrides.TryGetValue(name, out var value3))
					{
						foreach (global::UnityEngine.InputSystem.Utilities.InternedString internedString in value3)
						{
							global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout2 = TryLoadLayout(internedString);
							inputControlLayout2.MergeLayout(value);
							inputControlLayout2.m_BaseLayouts.Clear();
							inputControlLayout2.isOverride = false;
							inputControlLayout2.isGenericTypeOfDevice = value.isGenericTypeOfDevice;
							inputControlLayout2.m_Name = value.name;
							inputControlLayout2.m_BaseLayouts = value.m_BaseLayouts;
							value = inputControlLayout2;
							value.m_AppliedOverrides.Append(internedString);
						}
					}
					if (table != null)
					{
						table[name] = value;
					}
				}
				return value;
			}

			public global::UnityEngine.InputSystem.Utilities.InternedString GetBaseLayoutName(global::UnityEngine.InputSystem.Utilities.InternedString layoutName)
			{
				if (baseLayoutTable.TryGetValue(layoutName, out var value))
				{
					return value;
				}
				return default(global::UnityEngine.InputSystem.Utilities.InternedString);
			}

			public global::UnityEngine.InputSystem.Utilities.InternedString GetRootLayoutName(global::UnityEngine.InputSystem.Utilities.InternedString layoutName)
			{
				global::UnityEngine.InputSystem.Utilities.InternedString value;
				while (baseLayoutTable.TryGetValue(layoutName, out value))
				{
					layoutName = value;
				}
				return layoutName;
			}

			public bool ComputeDistanceInInheritanceHierarchy(global::UnityEngine.InputSystem.Utilities.InternedString firstLayout, global::UnityEngine.InputSystem.Utilities.InternedString secondLayout, out int distance)
			{
				distance = 0;
				int num = 0;
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = secondLayout;
				while (!internedString.IsEmpty() && internedString != firstLayout)
				{
					internedString = GetBaseLayoutName(internedString);
					num++;
				}
				if (internedString == firstLayout)
				{
					distance = num;
					return true;
				}
				int num2 = 0;
				internedString = firstLayout;
				while (!internedString.IsEmpty() && internedString != secondLayout)
				{
					internedString = GetBaseLayoutName(internedString);
					num2++;
				}
				if (internedString == secondLayout)
				{
					distance = num2;
					return true;
				}
				return false;
			}

			public global::UnityEngine.InputSystem.Utilities.InternedString FindLayoutThatIntroducesControl(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.Layouts.InputControlLayout.Cache cache)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = control;
				while (inputControl.parent != control.device)
				{
					inputControl = inputControl.parent;
				}
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = control.device.m_Layout;
				global::UnityEngine.InputSystem.Utilities.InternedString value = internedString;
				while (baseLayoutTable.TryGetValue(value, out value))
				{
					if (cache.FindOrLoadLayout(value).FindControl(inputControl.m_Name).HasValue)
					{
						internedString = value;
					}
				}
				return internedString;
			}

			public global::System.Type GetControlTypeForLayout(global::UnityEngine.InputSystem.Utilities.InternedString layoutName)
			{
				while (layoutStrings.ContainsKey(layoutName))
				{
					if (baseLayoutTable.TryGetValue(layoutName, out var value))
					{
						layoutName = value;
						continue;
					}
					return typeof(global::UnityEngine.InputSystem.InputDevice);
				}
				layoutTypes.TryGetValue(layoutName, out var value2);
				return value2;
			}

			public bool ValueTypeIsAssignableFrom(global::UnityEngine.InputSystem.Utilities.InternedString layoutName, global::System.Type valueType)
			{
				global::System.Type controlTypeForLayout = GetControlTypeForLayout(layoutName);
				if (controlTypeForLayout == null)
				{
					return false;
				}
				global::System.Type genericTypeArgumentFromHierarchy = global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetGenericTypeArgumentFromHierarchy(controlTypeForLayout, typeof(global::UnityEngine.InputSystem.InputControl<>), 0);
				if (genericTypeArgumentFromHierarchy == null)
				{
					return false;
				}
				return valueType.IsAssignableFrom(genericTypeArgumentFromHierarchy);
			}

			public bool IsGeneratedLayout(global::UnityEngine.InputSystem.Utilities.InternedString layout)
			{
				return layoutBuilders.ContainsKey(layout);
			}

			public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.InternedString> GetBaseLayouts(global::UnityEngine.InputSystem.Utilities.InternedString layout, bool includeSelf = true)
			{
				if (includeSelf)
				{
					yield return layout;
				}
				while (baseLayoutTable.TryGetValue(layout, out layout))
				{
					yield return layout;
				}
			}

			public bool IsBasedOn(global::UnityEngine.InputSystem.Utilities.InternedString parentLayout, global::UnityEngine.InputSystem.Utilities.InternedString childLayout)
			{
				global::UnityEngine.InputSystem.Utilities.InternedString value = childLayout;
				while (baseLayoutTable.TryGetValue(value, out value))
				{
					if (value == parentLayout)
					{
						return true;
					}
				}
				return false;
			}

			public void AddMatcher(global::UnityEngine.InputSystem.Utilities.InternedString layout, global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher matcher)
			{
				int count = layoutMatchers.Count;
				for (int i = 0; i < count; i++)
				{
					if (layoutMatchers[i].deviceMatcher == matcher)
					{
						return;
					}
				}
				layoutMatchers.Add(new global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection.LayoutMatcher
				{
					layoutName = layout,
					deviceMatcher = matcher
				});
			}
		}

		public class LayoutNotFoundException : global::System.Exception
		{
			public string layout { get; }

			public LayoutNotFoundException()
			{
			}

			public LayoutNotFoundException(string name, string message)
				: base(message)
			{
				layout = name;
			}

			public LayoutNotFoundException(string name)
				: base("Cannot find control layout '" + name + "'")
			{
				layout = name;
			}

			public LayoutNotFoundException(string message, global::System.Exception innerException)
				: base(message, innerException)
			{
			}

			protected LayoutNotFoundException(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context)
				: base(info, context)
			{
			}
		}

		internal struct Cache
		{
			public global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Layouts.InputControlLayout> table;

			public void Clear()
			{
				table = null;
			}

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout FindOrLoadLayout(string name, bool throwIfNotFound = true)
			{
				global::UnityEngine.InputSystem.Utilities.InternedString name2 = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
				if (table == null)
				{
					table = new global::System.Collections.Generic.Dictionary<global::UnityEngine.InputSystem.Utilities.InternedString, global::UnityEngine.InputSystem.Layouts.InputControlLayout>();
				}
				global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout = s_Layouts.TryLoadLayout(name2, table);
				if (inputControlLayout != null)
				{
					return inputControlLayout;
				}
				if (throwIfNotFound)
				{
					throw new global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutNotFoundException(name);
				}
				return null;
			}
		}

		internal struct CacheRefInstance : global::System.IDisposable
		{
			public bool valid;

			public void Dispose()
			{
				if (valid)
				{
					s_CacheInstanceRef--;
					if (s_CacheInstanceRef <= 0)
					{
						s_CacheInstance = default(global::UnityEngine.InputSystem.Layouts.InputControlLayout.Cache);
						s_CacheInstanceRef = 0;
					}
					valid = false;
				}
			}
		}

		private static global::UnityEngine.InputSystem.Utilities.InternedString s_DefaultVariant = new global::UnityEngine.InputSystem.Utilities.InternedString("Default");

		public const string VariantSeparator = ";";

		private global::UnityEngine.InputSystem.Utilities.InternedString m_Name;

		private global::System.Type m_Type;

		private global::UnityEngine.InputSystem.Utilities.InternedString m_Variants;

		private global::UnityEngine.InputSystem.Utilities.FourCC m_StateFormat;

		internal int m_StateSizeInBytes;

		internal bool? m_UpdateBeforeRender;

		internal global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString> m_BaseLayouts;

		private global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString> m_AppliedOverrides;

		private global::UnityEngine.InputSystem.Utilities.InternedString[] m_CommonUsages;

		internal global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] m_Controls;

		internal string m_DisplayName;

		private string m_Description;

		private global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags m_Flags;

		internal static global::UnityEngine.InputSystem.Layouts.InputControlLayout.Collection s_Layouts;

		internal static global::UnityEngine.InputSystem.Layouts.InputControlLayout.Cache s_CacheInstance;

		internal static int s_CacheInstanceRef;

		public static global::UnityEngine.InputSystem.Utilities.InternedString DefaultVariant => s_DefaultVariant;

		public global::UnityEngine.InputSystem.Utilities.InternedString name => m_Name;

		public string displayName => m_DisplayName ?? ((string)m_Name);

		public global::System.Type type => m_Type;

		public global::UnityEngine.InputSystem.Utilities.InternedString variants => m_Variants;

		public global::UnityEngine.InputSystem.Utilities.FourCC stateFormat => m_StateFormat;

		public int stateSizeInBytes => m_StateSizeInBytes;

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.InternedString> baseLayouts => m_BaseLayouts;

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.InternedString> appliedOverrides => m_AppliedOverrides;

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> commonUsages => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(m_CommonUsages);

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controls => new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>(m_Controls);

		public bool updateBeforeRender => m_UpdateBeforeRender == true;

		public bool isDeviceLayout => typeof(global::UnityEngine.InputSystem.InputDevice).IsAssignableFrom(m_Type);

		public bool isControlLayout => !isDeviceLayout;

		public bool isOverride
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsOverride) != 0;
			}
			internal set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsOverride;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsOverride;
				}
			}
		}

		public bool isGenericTypeOfDevice
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsGenericTypeOfDevice) != 0;
			}
			internal set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsGenericTypeOfDevice;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsGenericTypeOfDevice;
				}
			}
		}

		public bool hideInUI
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.HideInUI) != 0;
			}
			internal set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.HideInUI;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.HideInUI;
				}
			}
		}

		public bool isNoisy
		{
			get
			{
				return (m_Flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsNoisy) != 0;
			}
			internal set
			{
				if (value)
				{
					m_Flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsNoisy;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.IsNoisy;
				}
			}
		}

		public bool? canRunInBackground
		{
			get
			{
				if ((m_Flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.CanRunInBackgroundIsSet) == 0)
				{
					return null;
				}
				return (m_Flags & global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.CanRunInBackground) != 0;
			}
			internal set
			{
				if (!value.HasValue)
				{
					m_Flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.CanRunInBackgroundIsSet;
					return;
				}
				m_Flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.CanRunInBackgroundIsSet;
				if (value.Value)
				{
					m_Flags |= global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.CanRunInBackground;
				}
				else
				{
					m_Flags &= ~global::UnityEngine.InputSystem.Layouts.InputControlLayout.Flags.CanRunInBackground;
				}
			}
		}

		public global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem this[string path]
		{
			get
			{
				if (string.IsNullOrEmpty(path))
				{
					throw new global::System.ArgumentNullException("path");
				}
				if (m_Controls != null)
				{
					for (int i = 0; i < m_Controls.Length; i++)
					{
						if (m_Controls[i].name == path)
						{
							return m_Controls[i];
						}
					}
				}
				throw new global::System.Collections.Generic.KeyNotFoundException($"Cannot find control '{path}' in layout '{name}'");
			}
		}

		internal static ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.Cache cache => ref s_CacheInstance;

		public global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem? FindControl(global::UnityEngine.InputSystem.Utilities.InternedString path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			if (m_Controls == null)
			{
				return null;
			}
			for (int i = 0; i < m_Controls.Length; i++)
			{
				if (m_Controls[i].name == path)
				{
					return m_Controls[i];
				}
			}
			return null;
		}

		public global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem? FindControlIncludingArrayElements(string path, out int arrayIndex)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new global::System.ArgumentNullException("path");
			}
			arrayIndex = -1;
			if (m_Controls == null)
			{
				return null;
			}
			int num = 0;
			int num2 = path.Length;
			while (num2 > 0 && char.IsDigit(path[num2 - 1]))
			{
				num2--;
				num *= 10;
				num += path[num2] - 48;
			}
			int num3 = 0;
			if (num2 < path.Length && num2 > 0)
			{
				num3 = num2;
			}
			for (int i = 0; i < m_Controls.Length; i++)
			{
				ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem reference = ref m_Controls[i];
				if (string.Compare(reference.name, path, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return reference;
				}
				if (reference.isArray && num3 > 0 && num3 == reference.name.length && string.Compare(reference.name.ToString(), 0, path, 0, num3, global::System.StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					arrayIndex = num;
					return reference;
				}
			}
			return null;
		}

		public global::System.Type GetValueType()
		{
			return global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetGenericTypeArgumentFromHierarchy(type, typeof(global::UnityEngine.InputSystem.InputControl<>), 0);
		}

		public static global::UnityEngine.InputSystem.Layouts.InputControlLayout FromType(string name, global::System.Type type)
		{
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>();
			global::UnityEngine.InputSystem.Layouts.InputControlLayoutAttribute customAttribute = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.InputSystem.Layouts.InputControlLayoutAttribute>(type, inherit: true);
			global::UnityEngine.InputSystem.Utilities.FourCC fourCC = default(global::UnityEngine.InputSystem.Utilities.FourCC);
			if (customAttribute != null && customAttribute.stateType != null)
			{
				AddControlItems(customAttribute.stateType, list, name);
				if (typeof(global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo).IsAssignableFrom(customAttribute.stateType))
				{
					fourCC = ((global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo)global::System.Activator.CreateInstance(customAttribute.stateType)).format;
				}
			}
			else
			{
				AddControlItems(type, list, name);
			}
			if (customAttribute != null && !string.IsNullOrEmpty(customAttribute.stateFormat))
			{
				fourCC = new global::UnityEngine.InputSystem.Utilities.FourCC(customAttribute.stateFormat);
			}
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = default(global::UnityEngine.InputSystem.Utilities.InternedString);
			if (customAttribute != null)
			{
				internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(customAttribute.variants);
			}
			global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout = new global::UnityEngine.InputSystem.Layouts.InputControlLayout(name, type)
			{
				m_Controls = list.ToArray(),
				m_StateFormat = fourCC,
				m_Variants = internedString,
				m_UpdateBeforeRender = customAttribute?.updateBeforeRenderInternal,
				isGenericTypeOfDevice = (customAttribute?.isGenericTypeOfDevice ?? false),
				hideInUI = (customAttribute?.hideInUI ?? false),
				m_Description = customAttribute?.description,
				m_DisplayName = customAttribute?.displayName,
				canRunInBackground = customAttribute?.canRunInBackgroundInternal,
				isNoisy = (customAttribute?.isNoisy ?? false)
			};
			if (customAttribute?.commonUsages != null)
			{
				inputControlLayout.m_CommonUsages = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Select(customAttribute.commonUsages, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x));
			}
			return inputControlLayout;
		}

		public string ToJson()
		{
			return global::UnityEngine.JsonUtility.ToJson(global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJson.FromLayout(this), prettyPrint: true);
		}

		public static global::UnityEngine.InputSystem.Layouts.InputControlLayout FromJson(string json)
		{
			return global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJson>(json).ToLayout();
		}

		private InputControlLayout(string name, global::System.Type type)
		{
			m_Name = new global::UnityEngine.InputSystem.Utilities.InternedString(name);
			m_Type = type;
		}

		private static void AddControlItems(global::System.Type type, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			AddControlItemsFromFields(type, controlLayouts, layoutName);
			AddControlItemsFromProperties(type, controlLayouts, layoutName);
		}

		private static void AddControlItemsFromFields(global::System.Type type, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			global::System.Reflection.MemberInfo[] fields = type.GetFields(global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			AddControlItemsFromMembers(fields, controlLayouts, layoutName);
		}

		private static void AddControlItemsFromProperties(global::System.Type type, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			global::System.Reflection.MemberInfo[] properties = type.GetProperties(global::System.Reflection.BindingFlags.DeclaredOnly | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			AddControlItemsFromMembers(properties, controlLayouts, layoutName);
		}

		private static void AddControlItemsFromMembers(global::System.Reflection.MemberInfo[] members, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlItems, string layoutName)
		{
			foreach (global::System.Reflection.MemberInfo memberInfo in members)
			{
				if (memberInfo.DeclaringType == typeof(global::UnityEngine.InputSystem.InputControl))
				{
					continue;
				}
				global::System.Type valueType = global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetValueType(memberInfo);
				if (valueType != null && valueType.IsValueType && typeof(global::UnityEngine.InputSystem.LowLevel.IInputStateTypeInfo).IsAssignableFrom(valueType))
				{
					int count = controlItems.Count;
					AddControlItems(valueType, controlItems, layoutName);
					if (memberInfo as global::System.Reflection.FieldInfo != null)
					{
						int num = global::System.Runtime.InteropServices.Marshal.OffsetOf(memberInfo.DeclaringType, memberInfo.Name).ToInt32();
						int count2 = controlItems.Count;
						for (int j = count; j < count2; j++)
						{
							global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem value = controlItems[j];
							if (controlItems[j].offset != uint.MaxValue)
							{
								value.offset += (uint)num;
								controlItems[j] = value;
							}
						}
					}
				}
				global::UnityEngine.InputSystem.Layouts.InputControlAttribute[] array = global::System.Linq.Enumerable.ToArray(global::System.Reflection.CustomAttributeExtensions.GetCustomAttributes<global::UnityEngine.InputSystem.Layouts.InputControlAttribute>(memberInfo, inherit: false));
				if (array.Length != 0 || (!(valueType == null) && typeof(global::UnityEngine.InputSystem.InputControl).IsAssignableFrom(valueType) && !(memberInfo is global::System.Reflection.PropertyInfo)))
				{
					AddControlItemsFromMember(memberInfo, array, controlItems);
				}
			}
		}

		private static void AddControlItemsFromMember(global::System.Reflection.MemberInfo member, global::UnityEngine.InputSystem.Layouts.InputControlAttribute[] attributes, global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controlItems)
		{
			if (attributes.Length == 0)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item = CreateControlItemFromMember(member, null);
				controlItems.Add(item);
				return;
			}
			foreach (global::UnityEngine.InputSystem.Layouts.InputControlAttribute attribute in attributes)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item2 = CreateControlItemFromMember(member, attribute);
				controlItems.Add(item2);
			}
		}

		private static global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem CreateControlItemFromMember(global::System.Reflection.MemberInfo member, global::UnityEngine.InputSystem.Layouts.InputControlAttribute attribute)
		{
			string text = attribute?.name;
			if (string.IsNullOrEmpty(text))
			{
				text = member.Name;
			}
			bool flag = text.IndexOf('/') != -1;
			string text2 = attribute?.displayName;
			string shortDisplayName = attribute?.shortDisplayName;
			string text3 = attribute?.layout;
			if (string.IsNullOrEmpty(text3) && !flag && (!(member is global::System.Reflection.FieldInfo) || global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::System.Runtime.CompilerServices.FixedBufferAttribute>(member, inherit: false) == null))
			{
				text3 = InferLayoutFromValueType(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetValueType(member));
			}
			string text4 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.variants))
			{
				text4 = attribute.variants;
			}
			uint offset = uint.MaxValue;
			if (attribute != null && attribute.offset != uint.MaxValue)
			{
				offset = attribute.offset;
			}
			else if (member is global::System.Reflection.FieldInfo && !flag)
			{
				offset = (uint)global::System.Runtime.InteropServices.Marshal.OffsetOf(member.DeclaringType, member.Name).ToInt32();
			}
			uint num = uint.MaxValue;
			if (attribute != null)
			{
				num = attribute.bit;
			}
			uint sizeInBits = 0u;
			if (attribute != null)
			{
				sizeInBits = attribute.sizeInBits;
			}
			global::UnityEngine.InputSystem.Utilities.FourCC format = default(global::UnityEngine.InputSystem.Utilities.FourCC);
			if (attribute != null && !string.IsNullOrEmpty(attribute.format))
			{
				format = new global::UnityEngine.InputSystem.Utilities.FourCC(attribute.format);
			}
			else if (!flag && num == uint.MaxValue)
			{
				format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.GetPrimitiveFormatFromType(global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetValueType(member));
			}
			global::UnityEngine.InputSystem.Utilities.InternedString[] array = null;
			if (attribute != null)
			{
				string[] array2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Join(attribute.alias, attribute.aliases);
				if (array2 != null)
				{
					array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(array2, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x)));
				}
			}
			global::UnityEngine.InputSystem.Utilities.InternedString[] array3 = null;
			if (attribute != null)
			{
				string[] array4 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Join(attribute.usage, attribute.usages);
				if (array4 != null)
				{
					array3 = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(array4, (string x) => new global::UnityEngine.InputSystem.Utilities.InternedString(x)));
				}
			}
			global::UnityEngine.InputSystem.Utilities.NamedValue[] array5 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.parameters))
			{
				array5 = global::UnityEngine.InputSystem.Utilities.NamedValue.ParseMultiple(attribute.parameters);
			}
			global::UnityEngine.InputSystem.Utilities.NameAndParameters[] array6 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.processors))
			{
				array6 = global::System.Linq.Enumerable.ToArray(global::UnityEngine.InputSystem.Utilities.NameAndParameters.ParseMultiple(attribute.processors));
			}
			string useStateFrom = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.useStateFrom))
			{
				useStateFrom = attribute.useStateFrom;
			}
			bool flag2 = false;
			if (attribute != null)
			{
				flag2 = attribute.noisy;
			}
			bool dontReset = false;
			if (attribute != null)
			{
				dontReset = attribute.dontReset;
			}
			bool isSynthetic = false;
			if (attribute != null)
			{
				isSynthetic = attribute.synthetic;
			}
			int arraySize = 0;
			if (attribute != null)
			{
				arraySize = attribute.arraySize;
			}
			global::UnityEngine.InputSystem.Utilities.PrimitiveValue defaultState = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			if (attribute != null)
			{
				defaultState = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(attribute.defaultState);
			}
			global::UnityEngine.InputSystem.Utilities.PrimitiveValue minValue = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			global::UnityEngine.InputSystem.Utilities.PrimitiveValue maxValue = default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			if (attribute != null)
			{
				minValue = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(attribute.minValue);
				maxValue = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromObject(attribute.maxValue);
			}
			return new global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem
			{
				name = new global::UnityEngine.InputSystem.Utilities.InternedString(text),
				displayName = text2,
				shortDisplayName = shortDisplayName,
				layout = new global::UnityEngine.InputSystem.Utilities.InternedString(text3),
				variants = new global::UnityEngine.InputSystem.Utilities.InternedString(text4),
				useStateFrom = useStateFrom,
				format = format,
				offset = offset,
				bit = num,
				sizeInBits = sizeInBits,
				parameters = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue>(array5),
				processors = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NameAndParameters>(array6),
				usages = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(array3),
				aliases = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString>(array),
				isModifyingExistingControl = flag,
				isFirstDefinedInThisLayout = true,
				isNoisy = flag2,
				dontReset = dontReset,
				isSynthetic = isSynthetic,
				arraySize = arraySize,
				defaultState = defaultState,
				minValue = minValue,
				maxValue = maxValue
			};
		}

		private static string InferLayoutFromValueType(global::System.Type type)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = s_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty())
			{
				global::UnityEngine.InputSystem.Utilities.InternedString internedString2 = new global::UnityEngine.InputSystem.Utilities.InternedString(type.Name);
				if (s_Layouts.HasLayout(internedString2))
				{
					internedString = internedString2;
				}
				else if (type.Name.EndsWith("Control"))
				{
					internedString2 = new global::UnityEngine.InputSystem.Utilities.InternedString(type.Name.Substring(0, type.Name.Length - "Control".Length));
					if (s_Layouts.HasLayout(internedString2))
					{
						internedString = internedString2;
					}
				}
			}
			return internedString;
		}

		public void MergeLayout(global::UnityEngine.InputSystem.Layouts.InputControlLayout other)
		{
			if (other == null)
			{
				throw new global::System.ArgumentNullException("other");
			}
			m_UpdateBeforeRender = m_UpdateBeforeRender ?? other.m_UpdateBeforeRender;
			if (m_Variants.IsEmpty())
			{
				m_Variants = other.m_Variants;
			}
			if (m_Type == null)
			{
				m_Type = other.m_Type;
			}
			else if (m_Type.IsAssignableFrom(other.m_Type))
			{
				m_Type = other.m_Type;
			}
			bool flag = !m_Variants.IsEmpty();
			if (m_StateFormat == default(global::UnityEngine.InputSystem.Utilities.FourCC))
			{
				m_StateFormat = other.m_StateFormat;
			}
			m_CommonUsages = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.Merge(other.m_CommonUsages, m_CommonUsages);
			m_AppliedOverrides.Merge(other.m_AppliedOverrides);
			if (string.IsNullOrEmpty(m_DisplayName))
			{
				m_DisplayName = other.m_DisplayName;
			}
			if (m_Controls == null)
			{
				m_Controls = other.m_Controls;
			}
			else
			{
				if (other.m_Controls == null)
				{
					return;
				}
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] controlItems = other.m_Controls;
				global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>();
				global::System.Collections.Generic.List<string> list2 = new global::System.Collections.Generic.List<string>();
				global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> dictionary = CreateLookupTableForControls(controlItems, list2);
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> item5 in CreateLookupTableForControls(m_Controls))
				{
					if (dictionary.TryGetValue(item5.Key, out var value))
					{
						global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item = item5.Value.Merge(value);
						list.Add(item);
						dictionary.Remove(item5.Key);
					}
					else if (item5.Value.variants.IsEmpty() || item5.Value.variants == DefaultVariant)
					{
						bool flag2 = false;
						if (flag)
						{
							for (int i = 0; i < list2.Count; i++)
							{
								if (VariantsMatch(m_Variants.ToLower(), list2[i]))
								{
									string key = item5.Key + "@" + list2[i];
									if (dictionary.TryGetValue(key, out value))
									{
										global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item2 = item5.Value.Merge(value);
										list.Add(item2);
										dictionary.Remove(key);
										flag2 = true;
									}
								}
							}
						}
						else
						{
							foreach (string item6 in list2)
							{
								string key2 = item5.Key + "@" + item6;
								if (dictionary.TryGetValue(key2, out value))
								{
									global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item3 = item5.Value.Merge(value);
									list.Add(item3);
									dictionary.Remove(key2);
									flag2 = true;
								}
							}
						}
						if (!flag2)
						{
							list.Add(item5.Value);
						}
					}
					else if (dictionary.TryGetValue(item5.Value.name.ToLower(), out value))
					{
						global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem item4 = item5.Value.Merge(value);
						list.Add(item4);
						dictionary.Remove(item5.Value.name.ToLower());
					}
					else if (VariantsMatch(m_Variants, item5.Value.variants))
					{
						list.Add(item5.Value);
					}
				}
				if (!flag)
				{
					int count = list.Count;
					list.AddRange(dictionary.Values);
					for (int j = count; j < list.Count; j++)
					{
						global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem value2 = list[j];
						value2.isFirstDefinedInThisLayout = false;
						list[j] = value2;
					}
				}
				else
				{
					int count2 = list.Count;
					list.AddRange(global::System.Linq.Enumerable.Where(dictionary.Values, (global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem x) => VariantsMatch(m_Variants, x.variants)));
					for (int num = count2; num < list.Count; num++)
					{
						global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem value3 = list[num];
						value3.isFirstDefinedInThisLayout = false;
						list[num] = value3;
					}
				}
				m_Controls = list.ToArray();
			}
		}

		private static global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> CreateLookupTableForControls(global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] controlItems, global::System.Collections.Generic.List<string> variants = null)
		{
			global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> dictionary = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>();
			for (int i = 0; i < controlItems.Length; i++)
			{
				string text = controlItems[i].name.ToLower();
				global::UnityEngine.InputSystem.Utilities.InternedString internedString = controlItems[i].variants;
				if (!internedString.IsEmpty() && internedString != DefaultVariant)
				{
					if (internedString.ToString().IndexOf(";"[0]) != -1)
					{
						string[] array = internedString.ToLower().Split(";"[0]);
						foreach (string text2 in array)
						{
							variants?.Add(text2);
							text = text + "@" + text2;
							dictionary[text] = controlItems[i];
						}
						continue;
					}
					text = text + "@" + internedString.ToLower();
					variants?.Add(internedString.ToLower());
				}
				dictionary[text] = controlItems[i];
			}
			return dictionary;
		}

		internal static bool VariantsMatch(global::UnityEngine.InputSystem.Utilities.InternedString expected, global::UnityEngine.InputSystem.Utilities.InternedString actual)
		{
			return VariantsMatch(expected.ToLower(), actual.ToLower());
		}

		internal static bool VariantsMatch(string expected, string actual)
		{
			if (actual != null && global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(DefaultVariant, actual, ";"[0]))
			{
				return true;
			}
			if (expected == null)
			{
				return true;
			}
			if (actual == null)
			{
				return true;
			}
			return global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(expected, actual, ";"[0]);
		}

		internal static void ParseHeaderFieldsFromJson(string json, out global::UnityEngine.InputSystem.Utilities.InternedString name, out global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString> baseLayouts, out global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher deviceMatcher)
		{
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJsonNameAndDescriptorOnly layoutJsonNameAndDescriptorOnly = global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutJsonNameAndDescriptorOnly>(json);
			name = new global::UnityEngine.InputSystem.Utilities.InternedString(layoutJsonNameAndDescriptorOnly.name);
			baseLayouts = default(global::UnityEngine.InputSystem.Utilities.InlinedArray<global::UnityEngine.InputSystem.Utilities.InternedString>);
			if (!string.IsNullOrEmpty(layoutJsonNameAndDescriptorOnly.extend))
			{
				baseLayouts.Append(new global::UnityEngine.InputSystem.Utilities.InternedString(layoutJsonNameAndDescriptorOnly.extend));
			}
			if (layoutJsonNameAndDescriptorOnly.extendMultiple != null)
			{
				string[] extendMultiple = layoutJsonNameAndDescriptorOnly.extendMultiple;
				foreach (string text in extendMultiple)
				{
					baseLayouts.Append(new global::UnityEngine.InputSystem.Utilities.InternedString(text));
				}
			}
			deviceMatcher = layoutJsonNameAndDescriptorOnly.device.ToMatcher();
		}

		internal static global::UnityEngine.InputSystem.Layouts.InputControlLayout.CacheRefInstance CacheRef()
		{
			s_CacheInstanceRef++;
			return new global::UnityEngine.InputSystem.Layouts.InputControlLayout.CacheRefInstance
			{
				valid = true
			};
		}
	}
}
