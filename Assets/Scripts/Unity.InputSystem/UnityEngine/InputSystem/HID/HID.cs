namespace UnityEngine.InputSystem.HID
{
	public class HID : global::UnityEngine.InputSystem.InputDevice
	{
		[global::System.Serializable]
		private class HIDLayoutBuilder
		{
			public string displayName;

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor hidDescriptor;

			public string parentLayout;

			public global::System.Type deviceType;

			public global::UnityEngine.InputSystem.Layouts.InputControlLayout Build()
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder builder = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder
				{
					displayName = displayName,
					type = deviceType,
					extendsLayout = parentLayout,
					stateFormat = new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D')
				};
				global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor hIDElementDescriptor = global::System.Array.Find(hidDescriptor.elements, (global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor element) => element.usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop && element.usage == 48);
				global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor hIDElementDescriptor2 = global::System.Array.Find(hidDescriptor.elements, (global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor element) => element.usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop && element.usage == 49);
				bool flag = hIDElementDescriptor.usage == 48 && hIDElementDescriptor2.usage == 49;
				if (flag)
				{
					int bit;
					int num;
					int sizeInBits;
					if (hIDElementDescriptor.reportOffsetInBits <= hIDElementDescriptor2.reportOffsetInBits)
					{
						bit = hIDElementDescriptor.reportOffsetInBits % 8;
						num = hIDElementDescriptor.reportOffsetInBits / 8;
						sizeInBits = hIDElementDescriptor2.reportOffsetInBits + hIDElementDescriptor2.reportSizeInBits - hIDElementDescriptor.reportOffsetInBits;
					}
					else
					{
						bit = hIDElementDescriptor2.reportOffsetInBits % 8;
						num = hIDElementDescriptor2.reportOffsetInBits / 8;
						sizeInBits = hIDElementDescriptor.reportOffsetInBits + hIDElementDescriptor.reportSizeInBits - hIDElementDescriptor2.reportSizeInBits;
					}
					builder.AddControl("stick").WithDisplayName("Stick").WithLayout("Stick")
						.WithBitOffset((uint)bit)
						.WithByteOffset((uint)num)
						.WithSizeInBits((uint)sizeInBits)
						.WithUsages(global::UnityEngine.InputSystem.CommonUsages.Primary2DMotion);
					string text = hIDElementDescriptor.DetermineParameters();
					string text2 = hIDElementDescriptor2.DetermineParameters();
					builder.AddControl("stick/x").WithFormat(hIDElementDescriptor.DetermineFormat()).WithByteOffset((uint)(hIDElementDescriptor.reportOffsetInBits / 8 - num))
						.WithBitOffset((uint)(hIDElementDescriptor.reportOffsetInBits % 8))
						.WithSizeInBits((uint)hIDElementDescriptor.reportSizeInBits)
						.WithParameters(text)
						.WithDefaultState(hIDElementDescriptor.DetermineDefaultState())
						.WithProcessors(hIDElementDescriptor.DetermineProcessors());
					builder.AddControl("stick/y").WithFormat(hIDElementDescriptor2.DetermineFormat()).WithByteOffset((uint)(hIDElementDescriptor2.reportOffsetInBits / 8 - num))
						.WithBitOffset((uint)(hIDElementDescriptor2.reportOffsetInBits % 8))
						.WithSizeInBits((uint)hIDElementDescriptor2.reportSizeInBits)
						.WithParameters(text2)
						.WithDefaultState(hIDElementDescriptor2.DetermineDefaultState())
						.WithProcessors(hIDElementDescriptor2.DetermineProcessors());
					builder.AddControl("stick/up").WithParameters(global::UnityEngine.InputSystem.Utilities.StringHelpers.Join<string>(",", text2, "clamp=2,clampMin=-1,clampMax=0,invert=true"));
					builder.AddControl("stick/down").WithParameters(global::UnityEngine.InputSystem.Utilities.StringHelpers.Join<string>(",", text2, "clamp=2,clampMin=0,clampMax=1,invert=false"));
					builder.AddControl("stick/left").WithParameters(global::UnityEngine.InputSystem.Utilities.StringHelpers.Join<string>(",", text, "clamp=2,clampMin=-1,clampMax=0,invert"));
					builder.AddControl("stick/right").WithParameters(global::UnityEngine.InputSystem.Utilities.StringHelpers.Join<string>(",", text, "clamp=2,clampMin=0,clampMax=1"));
				}
				global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor[] elements = hidDescriptor.elements;
				int num2 = elements.Length;
				for (int num3 = 0; num3 < num2; num3++)
				{
					ref global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor reference = ref elements[num3];
					if (reference.reportType != global::UnityEngine.InputSystem.HID.HID.HIDReportType.Input || (flag && (reference.Is(global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop, 48) || reference.Is(global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop, 49))))
					{
						continue;
					}
					string text3 = reference.DetermineLayout();
					if (text3 != null)
					{
						string baseName = reference.DetermineName();
						baseName = global::UnityEngine.InputSystem.Utilities.StringHelpers.MakeUniqueName(baseName, builder.controls, (global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem x) => x.name);
						global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder.ControlBuilder controlBuilder = builder.AddControl(baseName).WithDisplayName(reference.DetermineDisplayName()).WithLayout(text3)
							.WithByteOffset((uint)reference.reportOffsetInBits / 8u)
							.WithBitOffset((uint)reference.reportOffsetInBits % 8u)
							.WithSizeInBits((uint)reference.reportSizeInBits)
							.WithFormat(reference.DetermineFormat())
							.WithDefaultState(reference.DetermineDefaultState())
							.WithProcessors(reference.DetermineProcessors());
						string text4 = reference.DetermineParameters();
						if (!string.IsNullOrEmpty(text4))
						{
							controlBuilder.WithParameters(text4);
						}
						global::UnityEngine.InputSystem.Utilities.InternedString[] array = reference.DetermineUsages();
						if (array != null)
						{
							controlBuilder.WithUsages(array);
						}
						reference.AddChildControls(ref reference, baseName, ref builder);
					}
				}
				return builder.Build();
			}
		}

		public enum HIDReportType
		{
			Unknown = 0,
			Input = 1,
			Output = 2,
			Feature = 3
		}

		public enum HIDCollectionType
		{
			Physical = 0,
			Application = 1,
			Logical = 2,
			Report = 3,
			NamedArray = 4,
			UsageSwitch = 5,
			UsageModifier = 6
		}

		[global::System.Flags]
		public enum HIDElementFlags
		{
			Constant = 1,
			Variable = 2,
			Relative = 4,
			Wrap = 8,
			NonLinear = 0x10,
			NoPreferred = 0x20,
			NullState = 0x40,
			Volatile = 0x80,
			BufferedBytes = 0x100
		}

		[global::System.Serializable]
		public struct HIDElementDescriptor
		{
			public int usage;

			public global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage;

			public int unit;

			public int unitExponent;

			public int logicalMin;

			public int logicalMax;

			public int physicalMin;

			public int physicalMax;

			public global::UnityEngine.InputSystem.HID.HID.HIDReportType reportType;

			public int collectionIndex;

			public int reportId;

			public int reportSizeInBits;

			public int reportOffsetInBits;

			public global::UnityEngine.InputSystem.HID.HID.HIDElementFlags flags;

			public int? usageMin;

			public int? usageMax;

			public bool hasNullState => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.NullState) == global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.NullState;

			public bool hasPreferredState => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.NoPreferred) != global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.NoPreferred;

			public bool isArray => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Variable) != global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Variable;

			public bool isNonLinear => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.NonLinear) == global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.NonLinear;

			public bool isRelative => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Relative) == global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Relative;

			public bool isConstant => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Constant) == global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Constant;

			public bool isWrapping => (flags & global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Wrap) == global::UnityEngine.InputSystem.HID.HID.HIDElementFlags.Wrap;

			internal bool isSigned => logicalMin < 0;

			internal float minFloatValue
			{
				get
				{
					if (isSigned)
					{
						int minValue = (int)(-(1L << reportSizeInBits - 1));
						int maxValue = (int)((1L << reportSizeInBits - 1) - 1);
						return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(logicalMin, minValue, maxValue) * 2f - 1f;
					}
					uint maxValue2 = (uint)((1L << reportSizeInBits) - 1);
					return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat((uint)logicalMin, 0u, maxValue2);
				}
			}

			internal float maxFloatValue
			{
				get
				{
					if (isSigned)
					{
						int minValue = (int)(-(1L << reportSizeInBits - 1));
						int maxValue = (int)((1L << reportSizeInBits - 1) - 1);
						return global::UnityEngine.InputSystem.Utilities.NumberHelpers.IntToNormalizedFloat(logicalMax, minValue, maxValue) * 2f - 1f;
					}
					uint maxValue2 = (uint)((1L << reportSizeInBits) - 1);
					return global::UnityEngine.InputSystem.Utilities.NumberHelpers.UIntToNormalizedFloat((uint)logicalMax, 0u, maxValue2);
				}
			}

			public bool Is(global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage, int usage)
			{
				if (usagePage == this.usagePage)
				{
					return usage == this.usage;
				}
				return false;
			}

			internal string DetermineName()
			{
				switch (usagePage)
				{
				case global::UnityEngine.InputSystem.HID.HID.UsagePage.Button:
					if (usage == 1)
					{
						return "trigger";
					}
					return $"button{usage}";
				case global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop:
				{
					if (usage == 57)
					{
						return "hat";
					}
					global::UnityEngine.InputSystem.HID.HID.GenericDesktop genericDesktop = (global::UnityEngine.InputSystem.HID.HID.GenericDesktop)usage;
					string text = genericDesktop.ToString();
					return char.ToLowerInvariant(text[0]) + text.Substring(1);
				}
				default:
					return $"UsagePage({usagePage:X}) Usage({usage:X})";
				}
			}

			internal string DetermineDisplayName()
			{
				switch (usagePage)
				{
				case global::UnityEngine.InputSystem.HID.HID.UsagePage.Button:
					if (usage == 1)
					{
						return "Trigger";
					}
					return $"Button {usage}";
				case global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop:
				{
					global::UnityEngine.InputSystem.HID.HID.GenericDesktop genericDesktop = (global::UnityEngine.InputSystem.HID.HID.GenericDesktop)usage;
					return genericDesktop.ToString();
				}
				default:
					return null;
				}
			}

			internal bool IsUsableElement()
			{
				int num = usage;
				if ((uint)(num - 48) <= 1u)
				{
					return usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop;
				}
				return DetermineLayout() != null;
			}

			internal string DetermineLayout()
			{
				if (reportType != global::UnityEngine.InputSystem.HID.HID.HIDReportType.Input)
				{
					return null;
				}
				switch (usagePage)
				{
				case global::UnityEngine.InputSystem.HID.HID.UsagePage.Button:
					return "Button";
				case global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop:
					switch (usage)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						return "Axis";
					case 61:
					case 62:
					case 144:
					case 145:
					case 146:
					case 147:
						return "Button";
					case 57:
						if (logicalMax - logicalMin + 1 == 8)
						{
							return "Dpad";
						}
						break;
					}
					break;
				}
				return null;
			}

			internal global::UnityEngine.InputSystem.Utilities.FourCC DetermineFormat()
			{
				switch (reportSizeInBits)
				{
				case 8:
					if (!isSigned)
					{
						return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatByte;
					}
					return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatSByte;
				case 16:
					if (!isSigned)
					{
						return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatUShort;
					}
					return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatShort;
				case 32:
					if (!isSigned)
					{
						return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatUInt;
					}
					return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInt;
				default:
					return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit;
				}
			}

			internal global::UnityEngine.InputSystem.Utilities.InternedString[] DetermineUsages()
			{
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.Button && usage == 1)
				{
					return new global::UnityEngine.InputSystem.Utilities.InternedString[2]
					{
						global::UnityEngine.InputSystem.CommonUsages.PrimaryTrigger,
						global::UnityEngine.InputSystem.CommonUsages.PrimaryAction
					};
				}
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.Button && usage == 2)
				{
					return new global::UnityEngine.InputSystem.Utilities.InternedString[2]
					{
						global::UnityEngine.InputSystem.CommonUsages.SecondaryTrigger,
						global::UnityEngine.InputSystem.CommonUsages.SecondaryAction
					};
				}
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop && usage == 53)
				{
					return new global::UnityEngine.InputSystem.Utilities.InternedString[1] { global::UnityEngine.InputSystem.CommonUsages.Twist };
				}
				return null;
			}

			internal string DetermineParameters()
			{
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop)
				{
					switch (usage)
					{
					case 48:
					case 50:
					case 51:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 66:
					case 67:
					case 69:
						return DetermineAxisNormalizationParameters();
					case 49:
					case 52:
					case 65:
					case 68:
						return global::UnityEngine.InputSystem.Utilities.StringHelpers.Join<string>(",", "invert", DetermineAxisNormalizationParameters());
					}
				}
				return null;
			}

			private string DetermineAxisNormalizationParameters()
			{
				if (logicalMin == 0 && logicalMax == 0)
				{
					return "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5";
				}
				float num = minFloatValue;
				float num2 = maxFloatValue;
				if (global::UnityEngine.Mathf.Approximately(0f, num) && global::UnityEngine.Mathf.Approximately(0f, num2))
				{
					return null;
				}
				float num3 = num + (num2 - num) / 2f;
				return string.Format(global::System.Globalization.CultureInfo.InvariantCulture, "normalize,normalizeMin={0},normalizeMax={1},normalizeZero={2}", num, num2, num3);
			}

			internal string DetermineProcessors()
			{
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop)
				{
					int num = usage;
					if ((uint)(num - 48) <= 8u || (uint)(num - 64) <= 5u)
					{
						return "axisDeadzone";
					}
				}
				return null;
			}

			internal global::UnityEngine.InputSystem.Utilities.PrimitiveValue DetermineDefaultState()
			{
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop)
				{
					switch (usage)
					{
					case 57:
						if (hasNullState)
						{
							if (logicalMin >= 1)
							{
								return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(logicalMin - 1);
							}
							ulong num2 = (ulong)((1L << reportSizeInBits) - 1);
							if ((ulong)logicalMax < num2)
							{
								return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(logicalMax + 1);
							}
						}
						break;
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 64:
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
						if (!isSigned)
						{
							int num = logicalMin + (logicalMax - logicalMin) / 2;
							if (num != 0)
							{
								return new global::UnityEngine.InputSystem.Utilities.PrimitiveValue(num);
							}
						}
						break;
					}
				}
				return default(global::UnityEngine.InputSystem.Utilities.PrimitiveValue);
			}

			internal void AddChildControls(ref global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor element, string controlName, ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder builder)
			{
				if (usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop && usage == 57)
				{
					global::UnityEngine.InputSystem.Utilities.PrimitiveValue primitiveValue = DetermineDefaultState();
					if (!primitiveValue.isEmpty)
					{
						builder.AddControl(controlName + "/up").WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit).WithLayout("DiscreteButton")
							.WithParameters(string.Format(global::System.Globalization.CultureInfo.InvariantCulture, "minValue={0},maxValue={1},nullValue={2},wrapAtValue={3}", logicalMax, logicalMin + 1, primitiveValue.ToString(), logicalMax))
							.WithBitOffset((uint)element.reportOffsetInBits % 8u)
							.WithSizeInBits((uint)reportSizeInBits);
						builder.AddControl(controlName + "/right").WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit).WithLayout("DiscreteButton")
							.WithParameters(string.Format(global::System.Globalization.CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", logicalMin + 1, logicalMin + 3))
							.WithBitOffset((uint)element.reportOffsetInBits % 8u)
							.WithSizeInBits((uint)reportSizeInBits);
						builder.AddControl(controlName + "/down").WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit).WithLayout("DiscreteButton")
							.WithParameters(string.Format(global::System.Globalization.CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", logicalMin + 3, logicalMin + 5))
							.WithBitOffset((uint)element.reportOffsetInBits % 8u)
							.WithSizeInBits((uint)reportSizeInBits);
						builder.AddControl(controlName + "/left").WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit).WithLayout("DiscreteButton")
							.WithParameters(string.Format(global::System.Globalization.CultureInfo.InvariantCulture, "minValue={0},maxValue={1}", logicalMin + 5, logicalMin + 7))
							.WithBitOffset((uint)element.reportOffsetInBits % 8u)
							.WithSizeInBits((uint)reportSizeInBits);
					}
				}
			}
		}

		[global::System.Serializable]
		public struct HIDCollectionDescriptor
		{
			public global::UnityEngine.InputSystem.HID.HID.HIDCollectionType type;

			public int usage;

			public global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage;

			public int parent;

			public int childCount;

			public int firstChild;
		}

		[global::System.Serializable]
		public struct HIDDeviceDescriptor
		{
			public int vendorId;

			public int productId;

			public int usage;

			public global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage;

			public int inputReportSize;

			public int outputReportSize;

			public int featureReportSize;

			public global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor[] elements;

			public global::UnityEngine.InputSystem.HID.HID.HIDCollectionDescriptor[] collections;

			public string ToJson()
			{
				return global::UnityEngine.JsonUtility.ToJson(this, prettyPrint: true);
			}

			public static global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor FromJson(string json)
			{
				try
				{
					global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor result = default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor);
					global::System.ReadOnlySpan<char> readOnlySpan = global::System.MemoryExtensions.AsSpan(json);
					global::UnityEngine.InputSystem.Utilities.PredictiveParser predictiveParser = default(global::UnityEngine.InputSystem.Utilities.PredictiveParser);
					predictiveParser.ExpectSingleChar(readOnlySpan, '{');
					predictiveParser.AcceptString(readOnlySpan, out var output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.vendorId = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.productId = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.usage = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.usagePage = (global::UnityEngine.InputSystem.HID.HID.UsagePage)predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.inputReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.outputReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out output);
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					result.featureReportSize = predictiveParser.ExpectInt(readOnlySpan);
					predictiveParser.AcceptSingleChar(readOnlySpan, ',');
					predictiveParser.AcceptString(readOnlySpan, out var output2);
					if (output2.ToString() != "elements")
					{
						return result;
					}
					predictiveParser.ExpectSingleChar(readOnlySpan, ':');
					predictiveParser.ExpectSingleChar(readOnlySpan, '[');
					global::System.Collections.Generic.List<global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor> value;
					using (global::UnityEngine.Pool.CollectionPool<global::System.Collections.Generic.List<global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor>, global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor>.Get(out value))
					{
						while (!predictiveParser.AcceptSingleChar(readOnlySpan, ']'))
						{
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectSingleChar(readOnlySpan, '{');
							global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor item = default(global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor);
							predictiveParser.AcceptSingleChar(readOnlySpan, '}');
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.usage = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.usagePage = (global::UnityEngine.InputSystem.HID.HID.UsagePage)predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.unit = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.unitExponent = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.logicalMin = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.logicalMax = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.physicalMin = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.physicalMax = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.collectionIndex = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.reportType = (global::UnityEngine.InputSystem.HID.HID.HIDReportType)predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.reportId = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							predictiveParser.AcceptInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.reportSizeInBits = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.reportOffsetInBits = predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.AcceptSingleChar(readOnlySpan, ',');
							predictiveParser.ExpectString(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, ':');
							item.flags = (global::UnityEngine.InputSystem.HID.HID.HIDElementFlags)predictiveParser.ExpectInt(readOnlySpan);
							predictiveParser.ExpectSingleChar(readOnlySpan, '}');
							value.Add(item);
						}
						result.elements = value.ToArray();
						return result;
					}
				}
				catch (global::System.Exception)
				{
					return global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor>(json);
				}
			}
		}

		public struct HIDDeviceDescriptorBuilder
		{
			public global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage;

			public int usage;

			private int m_CurrentReportId;

			private global::UnityEngine.InputSystem.HID.HID.HIDReportType m_CurrentReportType;

			private int m_CurrentReportOffsetInBits;

			private global::System.Collections.Generic.List<global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor> m_Elements;

			private global::System.Collections.Generic.List<global::UnityEngine.InputSystem.HID.HID.HIDCollectionDescriptor> m_Collections;

			private int m_InputReportSize;

			private int m_OutputReportSize;

			private int m_FeatureReportSize;

			public HIDDeviceDescriptorBuilder(global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage, int usage)
			{
				this = default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder);
				this.usagePage = usagePage;
				this.usage = usage;
			}

			public HIDDeviceDescriptorBuilder(global::UnityEngine.InputSystem.HID.HID.GenericDesktop usage)
				: this(global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop, (int)usage)
			{
			}

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder StartReport(global::UnityEngine.InputSystem.HID.HID.HIDReportType reportType, int reportId = 1)
			{
				m_CurrentReportId = reportId;
				m_CurrentReportType = reportType;
				m_CurrentReportOffsetInBits = 8;
				return this;
			}

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder AddElement(global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage, int usage, int sizeInBits)
			{
				if (m_Elements == null)
				{
					m_Elements = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor>();
				}
				else
				{
					foreach (global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor element in m_Elements)
					{
						if (element.reportId == m_CurrentReportId && element.reportType == m_CurrentReportType && element.usagePage == usagePage && element.usage == usage)
						{
							throw new global::System.InvalidOperationException($"Cannot add two elements with the same usage page '{usagePage}' and usage '0x{usage:X} the to same device");
						}
					}
				}
				m_Elements.Add(new global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor
				{
					usage = usage,
					usagePage = usagePage,
					reportOffsetInBits = m_CurrentReportOffsetInBits,
					reportSizeInBits = sizeInBits,
					reportType = m_CurrentReportType,
					reportId = m_CurrentReportId
				});
				m_CurrentReportOffsetInBits += sizeInBits;
				return this;
			}

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder AddElement(global::UnityEngine.InputSystem.HID.HID.GenericDesktop usage, int sizeInBits)
			{
				return AddElement(global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop, (int)usage, sizeInBits);
			}

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder WithPhysicalMinMax(int min, int max)
			{
				int num = m_Elements.Count - 1;
				if (num < 0)
				{
					throw new global::System.InvalidOperationException("No element has been added to the descriptor yet");
				}
				global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor value = m_Elements[num];
				value.physicalMin = min;
				value.physicalMax = max;
				m_Elements[num] = value;
				return this;
			}

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptorBuilder WithLogicalMinMax(int min, int max)
			{
				int num = m_Elements.Count - 1;
				if (num < 0)
				{
					throw new global::System.InvalidOperationException("No element has been added to the descriptor yet");
				}
				global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor value = m_Elements[num];
				value.logicalMin = min;
				value.logicalMax = max;
				m_Elements[num] = value;
				return this;
			}

			public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor Finish()
			{
				return new global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor
				{
					usage = usage,
					usagePage = usagePage,
					elements = m_Elements?.ToArray(),
					collections = m_Collections?.ToArray()
				};
			}
		}

		public enum UsagePage
		{
			Undefined = 0,
			GenericDesktop = 1,
			Simulation = 2,
			VRControls = 3,
			SportControls = 4,
			GameControls = 5,
			GenericDeviceControls = 6,
			Keyboard = 7,
			LEDs = 8,
			Button = 9,
			Ordinal = 10,
			Telephony = 11,
			Consumer = 12,
			Digitizer = 13,
			PID = 15,
			Unicode = 16,
			AlphanumericDisplay = 20,
			MedicalInstruments = 64,
			Monitor = 128,
			Power = 132,
			BarCodeScanner = 140,
			MagneticStripeReader = 142,
			Camera = 144,
			Arcade = 145,
			VendorDefined = 65280
		}

		public enum GenericDesktop
		{
			Undefined = 0,
			Pointer = 1,
			Mouse = 2,
			Joystick = 4,
			Gamepad = 5,
			Keyboard = 6,
			Keypad = 7,
			MultiAxisController = 8,
			TabletPCControls = 9,
			AssistiveControl = 10,
			X = 48,
			Y = 49,
			Z = 50,
			Rx = 51,
			Ry = 52,
			Rz = 53,
			Slider = 54,
			Dial = 55,
			Wheel = 56,
			HatSwitch = 57,
			CountedBuffer = 58,
			ByteCount = 59,
			MotionWakeup = 60,
			Start = 61,
			Select = 62,
			Vx = 64,
			Vy = 65,
			Vz = 66,
			Vbrx = 67,
			Vbry = 68,
			Vbrz = 69,
			Vno = 70,
			FeatureNotification = 71,
			ResolutionMultiplier = 72,
			SystemControl = 128,
			SystemPowerDown = 129,
			SystemSleep = 130,
			SystemWakeUp = 131,
			SystemContextMenu = 132,
			SystemMainMenu = 133,
			SystemAppMenu = 134,
			SystemMenuHelp = 135,
			SystemMenuExit = 136,
			SystemMenuSelect = 137,
			SystemMenuRight = 138,
			SystemMenuLeft = 139,
			SystemMenuUp = 140,
			SystemMenuDown = 141,
			SystemColdRestart = 142,
			SystemWarmRestart = 143,
			DpadUp = 144,
			DpadDown = 145,
			DpadRight = 146,
			DpadLeft = 147,
			SystemDock = 160,
			SystemUndock = 161,
			SystemSetup = 162,
			SystemBreak = 163,
			SystemDebuggerBreak = 164,
			ApplicationBreak = 165,
			ApplicationDebuggerBreak = 166,
			SystemSpeakerMute = 167,
			SystemHibernate = 168,
			SystemDisplayInvert = 176,
			SystemDisplayInternal = 177,
			SystemDisplayExternal = 178,
			SystemDisplayBoth = 179,
			SystemDisplayDual = 180,
			SystemDisplayToggleIntExt = 181,
			SystemDisplaySwapPrimarySecondary = 182,
			SystemDisplayLCDAutoScale = 183
		}

		public enum Simulation
		{
			Undefined = 0,
			FlightSimulationDevice = 1,
			AutomobileSimulationDevice = 2,
			TankSimulationDevice = 3,
			SpaceshipSimulationDevice = 4,
			SubmarineSimulationDevice = 5,
			SailingSimulationDevice = 6,
			MotorcycleSimulationDevice = 7,
			SportsSimulationDevice = 8,
			AirplaneSimulationDevice = 9,
			HelicopterSimulationDevice = 10,
			MagicCarpetSimulationDevice = 11,
			BicylcleSimulationDevice = 12,
			FlightControlStick = 32,
			FlightStick = 33,
			CyclicControl = 34,
			CyclicTrim = 35,
			FlightYoke = 36,
			TrackControl = 37,
			Aileron = 176,
			AileronTrim = 177,
			AntiTorqueControl = 178,
			AutopilotEnable = 179,
			ChaffRelease = 180,
			CollectiveControl = 181,
			DiveBreak = 182,
			ElectronicCountermeasures = 183,
			Elevator = 184,
			ElevatorTrim = 185,
			Rudder = 186,
			Throttle = 187,
			FlightCommunications = 188,
			FlareRelease = 189,
			LandingGear = 190,
			ToeBreak = 191,
			Trigger = 192,
			WeaponsArm = 193,
			WeaponsSelect = 194,
			WingFlaps = 195,
			Accelerator = 196,
			Brake = 197,
			Clutch = 198,
			Shifter = 199,
			Steering = 200,
			TurretDirection = 201,
			BarrelElevation = 202,
			DivePlane = 203,
			Ballast = 204,
			BicycleCrank = 205,
			HandleBars = 206,
			FrontBrake = 207,
			RearBrake = 208
		}

		public enum Button
		{
			Undefined = 0,
			Primary = 1,
			Secondary = 2,
			Tertiary = 3
		}

		internal const string kHIDInterface = "HID";

		internal const string kHIDNamespace = "HID";

		private bool m_HaveParsedHIDDescriptor;

		private global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor m_HIDDescriptor;

		private static readonly global::Unity.Profiling.ProfilerMarker k_HIDParseDescriptorFallback = new global::Unity.Profiling.ProfilerMarker("HIDParseDescriptorFallback");

		public static global::UnityEngine.InputSystem.Utilities.FourCC QueryHIDReportDescriptorDeviceCommandType => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'D');

		public static global::UnityEngine.InputSystem.Utilities.FourCC QueryHIDReportDescriptorSizeDeviceCommandType => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'S');

		public static global::UnityEngine.InputSystem.Utilities.FourCC QueryHIDParsedReportDescriptorDeviceCommandType => new global::UnityEngine.InputSystem.Utilities.FourCC('H', 'I', 'D', 'P');

		public global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor hidDescriptor
		{
			get
			{
				if (!m_HaveParsedHIDDescriptor)
				{
					if (!string.IsNullOrEmpty(base.description.capabilities))
					{
						m_HIDDescriptor = global::UnityEngine.JsonUtility.FromJson<global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor>(base.description.capabilities);
					}
					m_HaveParsedHIDDescriptor = true;
				}
				return m_HIDDescriptor;
			}
		}

		internal static string OnFindLayoutForDevice(ref global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description, string matchedLayout, global::UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeDeviceCommand)
		{
			if (!string.IsNullOrEmpty(matchedLayout))
			{
				return null;
			}
			if (description.interfaceName != "HID")
			{
				return null;
			}
			global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor hIDDeviceDescriptor = ReadHIDDeviceDescriptor(ref description, executeDeviceCommand);
			if (!global::System.Linq.Enumerable.Contains(global::UnityEngine.InputSystem.HID.HIDSupport.supportedHIDUsages, new global::UnityEngine.InputSystem.HID.HIDSupport.HIDPageUsage(hIDDeviceDescriptor.usagePage, hIDDeviceDescriptor.usage)))
			{
				return null;
			}
			bool flag = false;
			if (hIDDeviceDescriptor.elements != null)
			{
				global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor[] elements = hIDDeviceDescriptor.elements;
				foreach (global::UnityEngine.InputSystem.HID.HID.HIDElementDescriptor hIDElementDescriptor in elements)
				{
					if (hIDElementDescriptor.IsUsableElement())
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			global::System.Type typeFromHandle = typeof(global::UnityEngine.InputSystem.HID.HID);
			string text = "HID";
			if (hIDDeviceDescriptor.usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop && (hIDDeviceDescriptor.usage == 4 || hIDDeviceDescriptor.usage == 5))
			{
				text = "Joystick";
				typeFromHandle = typeof(global::UnityEngine.InputSystem.Joystick);
			}
			string text2 = "";
			if (text != "Joystick")
			{
				text2 = ((hIDDeviceDescriptor.usagePage == global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop) ? $" {(global::UnityEngine.InputSystem.HID.HID.GenericDesktop)hIDDeviceDescriptor.usage}" : $" {hIDDeviceDescriptor.usagePage}-{hIDDeviceDescriptor.usage}");
			}
			global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher inputDeviceMatcher = global::UnityEngine.InputSystem.Layouts.InputDeviceMatcher.FromDeviceDescription(description);
			string result;
			if (!string.IsNullOrEmpty(description.product) && !string.IsNullOrEmpty(description.manufacturer))
			{
				result = "HID::" + description.manufacturer + " " + description.product + text2;
			}
			else if (!string.IsNullOrEmpty(description.product))
			{
				result = "HID::" + description.product + text2;
			}
			else
			{
				if (hIDDeviceDescriptor.vendorId == 0)
				{
					return null;
				}
				result = string.Format("{0}::{1:X}-{2:X}{3}", "HID", hIDDeviceDescriptor.vendorId, hIDDeviceDescriptor.productId, text2);
				inputDeviceMatcher = inputDeviceMatcher.WithCapability("productId", hIDDeviceDescriptor.productId).WithCapability("vendorId", hIDDeviceDescriptor.vendorId);
			}
			inputDeviceMatcher = inputDeviceMatcher.WithCapability("usage", hIDDeviceDescriptor.usage).WithCapability("usagePage", hIDDeviceDescriptor.usagePage);
			global::UnityEngine.InputSystem.HID.HID.HIDLayoutBuilder layout = new global::UnityEngine.InputSystem.HID.HID.HIDLayoutBuilder
			{
				displayName = description.product,
				hidDescriptor = hIDDeviceDescriptor,
				parentLayout = text,
				deviceType = (typeFromHandle ?? typeof(global::UnityEngine.InputSystem.HID.HID))
			};
			global::UnityEngine.InputSystem.InputSystem.RegisterLayoutBuilder(() => layout.Build(), result, text, inputDeviceMatcher);
			return result;
		}

		internal unsafe static global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor ReadHIDDeviceDescriptor(ref global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription, global::UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeCommandDelegate)
		{
			if (deviceDescription.interfaceName != "HID")
			{
				throw new global::System.ArgumentException($"Device '{deviceDescription}' is not a HID");
			}
			bool flag = true;
			global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor deviceDescriptor = default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor);
			if (!string.IsNullOrEmpty(deviceDescription.capabilities))
			{
				try
				{
					deviceDescriptor = global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor.FromJson(deviceDescription.capabilities);
					if (deviceDescriptor.elements != null && deviceDescriptor.elements.Length != 0)
					{
						flag = false;
					}
				}
				catch (global::System.Exception exception)
				{
					global::UnityEngine.Debug.LogError($"Could not parse HID descriptor of device '{deviceDescription}'");
					global::UnityEngine.Debug.LogException(exception);
				}
			}
			if (flag)
			{
				global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand command = new global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand(QueryHIDReportDescriptorSizeDeviceCommandType);
				long num = executeCommandDelegate(ref command);
				if (num > 0)
				{
					using (global::Unity.Collections.NativeArray<byte> nativeArray = global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand.AllocateNative(QueryHIDReportDescriptorDeviceCommandType, (int)num))
					{
						global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* unsafePtr = (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray);
						if (executeCommandDelegate(ref *unsafePtr) != num)
						{
							return default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor);
						}
						if (!global::UnityEngine.InputSystem.HID.HIDParser.ParseReportDescriptor((byte*)unsafePtr->payloadPtr, (int)num, ref deviceDescriptor))
						{
							return default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor);
						}
					}
					deviceDescription.capabilities = deviceDescriptor.ToJson();
				}
				else
				{
					using global::Unity.Collections.NativeArray<byte> nativeArray2 = global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand.AllocateNative(QueryHIDParsedReportDescriptorDeviceCommandType, 2097152);
					global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand* unsafePtr2 = (global::UnityEngine.InputSystem.LowLevel.InputDeviceCommand*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(nativeArray2);
					long num2 = executeCommandDelegate(ref *unsafePtr2);
					if (num2 < 0)
					{
						return default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor);
					}
					byte[] array = new byte[num2];
					fixed (byte* destination = array)
					{
						global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(destination, unsafePtr2->payloadPtr, num2);
					}
					string text = global::System.Text.Encoding.UTF8.GetString(array, 0, (int)num2);
					try
					{
						deviceDescriptor = global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor.FromJson(text);
					}
					catch (global::System.Exception exception2)
					{
						global::UnityEngine.Debug.LogError($"Could not parse HID descriptor of device '{deviceDescription}'");
						global::UnityEngine.Debug.LogException(exception2);
						return default(global::UnityEngine.InputSystem.HID.HID.HIDDeviceDescriptor);
					}
					deviceDescription.capabilities = text;
				}
			}
			return deviceDescriptor;
		}

		public static string UsagePageToString(global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage)
		{
			if (usagePage < global::UnityEngine.InputSystem.HID.HID.UsagePage.VendorDefined)
			{
				return usagePage.ToString();
			}
			return "Vendor-Defined";
		}

		public static string UsageToString(global::UnityEngine.InputSystem.HID.HID.UsagePage usagePage, int usage)
		{
			switch (usagePage)
			{
			case global::UnityEngine.InputSystem.HID.HID.UsagePage.GenericDesktop:
			{
				global::UnityEngine.InputSystem.HID.HID.GenericDesktop genericDesktop = (global::UnityEngine.InputSystem.HID.HID.GenericDesktop)usage;
				return genericDesktop.ToString();
			}
			case global::UnityEngine.InputSystem.HID.HID.UsagePage.Simulation:
			{
				global::UnityEngine.InputSystem.HID.HID.Simulation simulation = (global::UnityEngine.InputSystem.HID.HID.Simulation)usage;
				return simulation.ToString();
			}
			default:
				return null;
			}
		}
	}
}
