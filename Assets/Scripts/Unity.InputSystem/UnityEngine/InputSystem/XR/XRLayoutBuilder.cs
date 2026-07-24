namespace UnityEngine.InputSystem.XR
{
	internal class XRLayoutBuilder
	{
		private string parentLayout;

		private string interfaceName;

		private global::UnityEngine.InputSystem.XR.XRDeviceDescriptor descriptor;

		private static readonly string[] poseSubControlNames = new string[6] { "/isTracked", "/trackingState", "/position", "/rotation", "/velocity", "/angularVelocity" };

		private static readonly global::UnityEngine.InputSystem.XR.FeatureType[] poseSubControlTypes = new global::UnityEngine.InputSystem.XR.FeatureType[6]
		{
			global::UnityEngine.InputSystem.XR.FeatureType.Binary,
			global::UnityEngine.InputSystem.XR.FeatureType.DiscreteStates,
			global::UnityEngine.InputSystem.XR.FeatureType.Axis3D,
			global::UnityEngine.InputSystem.XR.FeatureType.Rotation,
			global::UnityEngine.InputSystem.XR.FeatureType.Axis3D,
			global::UnityEngine.InputSystem.XR.FeatureType.Axis3D
		};

		private static uint GetSizeOfFeature(global::UnityEngine.InputSystem.XR.XRFeatureDescriptor featureDescriptor)
		{
			return featureDescriptor.featureType switch
			{
				global::UnityEngine.InputSystem.XR.FeatureType.Binary => 1u, 
				global::UnityEngine.InputSystem.XR.FeatureType.DiscreteStates => 4u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Axis1D => 4u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Axis2D => 8u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Axis3D => 12u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Rotation => 16u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Hand => 104u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Bone => 32u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Eyes => 76u, 
				global::UnityEngine.InputSystem.XR.FeatureType.Custom => featureDescriptor.customSize, 
				_ => 0u, 
			};
		}

		private static string SanitizeString(string original, bool allowPaths = false)
		{
			int length = original.Length;
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = original[i];
				if (char.IsUpper(c) || char.IsLower(c) || char.IsDigit(c) || c == '_' || (allowPaths && c == '/'))
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		internal static string OnFindLayoutForDevice(ref global::UnityEngine.InputSystem.Layouts.InputDeviceDescription description, string matchedLayout, global::UnityEngine.InputSystem.LowLevel.InputDeviceExecuteCommandDelegate executeCommandDelegate)
		{
			if (description.interfaceName != "XRInputV1" && description.interfaceName != "XRInput")
			{
				return null;
			}
			if (string.IsNullOrEmpty(description.capabilities))
			{
				return null;
			}
			global::UnityEngine.InputSystem.XR.XRDeviceDescriptor xRDeviceDescriptor;
			try
			{
				xRDeviceDescriptor = global::UnityEngine.InputSystem.XR.XRDeviceDescriptor.FromJson(description.capabilities);
			}
			catch (global::System.Exception)
			{
				return null;
			}
			if (xRDeviceDescriptor == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(matchedLayout))
			{
				if ((xRDeviceDescriptor.characteristics & global::UnityEngine.XR.InputDeviceCharacteristics.HeadMounted) != global::UnityEngine.XR.InputDeviceCharacteristics.None)
				{
					matchedLayout = "XRHMD";
				}
				else if ((xRDeviceDescriptor.characteristics & (global::UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | global::UnityEngine.XR.InputDeviceCharacteristics.Controller)) == (global::UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | global::UnityEngine.XR.InputDeviceCharacteristics.Controller))
				{
					matchedLayout = "XRController";
				}
			}
			string text = ((!string.IsNullOrEmpty(description.manufacturer)) ? (SanitizeString(description.interfaceName) + "::" + SanitizeString(description.manufacturer) + "::" + SanitizeString(description.product)) : (SanitizeString(description.interfaceName) + "::" + SanitizeString(description.product)));
			global::UnityEngine.InputSystem.XR.XRLayoutBuilder layout = new global::UnityEngine.InputSystem.XR.XRLayoutBuilder
			{
				descriptor = xRDeviceDescriptor,
				parentLayout = matchedLayout,
				interfaceName = description.interfaceName
			};
			global::UnityEngine.InputSystem.InputSystem.RegisterLayoutBuilder(() => layout.Build(), text, matchedLayout);
			return text;
		}

		private static string ConvertPotentialAliasToName(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout, string nameOrAlias)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = new global::UnityEngine.InputSystem.Utilities.InternedString(nameOrAlias);
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> controls = layout.controls;
			for (int i = 0; i < controls.Count; i++)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem = controls[i];
				if (controlItem.name == internedString)
				{
					return nameOrAlias;
				}
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> aliases = controlItem.aliases;
				for (int j = 0; j < aliases.Count; j++)
				{
					if (aliases[j] == nameOrAlias)
					{
						return controlItem.name.ToString();
					}
				}
			}
			return nameOrAlias;
		}

		private bool IsSubControl(string name)
		{
			return name.Contains('/');
		}

		private string GetParentControlName(string name)
		{
			return name[..name.IndexOf('/')];
		}

		private bool IsPoseControl(global::System.Collections.Generic.List<global::UnityEngine.InputSystem.XR.XRFeatureDescriptor> features, int startIndex)
		{
			for (int i = 0; i < 6; i++)
			{
				if (!features[startIndex + i].name.EndsWith(poseSubControlNames[i]) || features[startIndex + i].featureType != poseSubControlTypes[i])
				{
					return false;
				}
			}
			return true;
		}

		private global::UnityEngine.InputSystem.Layouts.InputControlLayout Build()
		{
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder builder = new global::UnityEngine.InputSystem.Layouts.InputControlLayout.Builder
			{
				stateFormat = new global::UnityEngine.InputSystem.Utilities.FourCC('X', 'R', 'S', '0'),
				extendsLayout = parentLayout,
				updateBeforeRender = true
			};
			global::UnityEngine.InputSystem.Layouts.InputControlLayout inputControlLayout = ((!string.IsNullOrEmpty(parentLayout)) ? global::UnityEngine.InputSystem.InputSystem.LoadLayout(parentLayout) : null);
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			global::System.Collections.Generic.List<string> list2 = new global::System.Collections.Generic.List<string>();
			uint num = 0u;
			for (int i = 0; i < descriptor.inputFeatures.Count; i++)
			{
				global::UnityEngine.InputSystem.XR.XRFeatureDescriptor featureDescriptor = descriptor.inputFeatures[i];
				list2.Clear();
				if (featureDescriptor.usageHints != null)
				{
					foreach (global::UnityEngine.InputSystem.XR.UsageHint usageHint in featureDescriptor.usageHints)
					{
						if (!string.IsNullOrEmpty(usageHint.content))
						{
							list2.Add(usageHint.content);
						}
					}
				}
				string name = featureDescriptor.name;
				name = SanitizeString(name, allowPaths: true);
				if (inputControlLayout != null)
				{
					name = ConvertPotentialAliasToName(inputControlLayout, name);
				}
				name = name.ToLowerInvariant();
				if (IsSubControl(name))
				{
					string parentControlName = GetParentControlName(name);
					if (!list.Contains(parentControlName) && IsPoseControl(descriptor.inputFeatures, i))
					{
						builder.AddControl(parentControlName).WithLayout("Pose").WithByteOffset(0u);
						list.Add(parentControlName);
					}
				}
				uint sizeOfFeature = GetSizeOfFeature(featureDescriptor);
				if (!(interfaceName == "XRInput") && sizeOfFeature >= 4 && num % 4 != 0)
				{
					num += 4 - num % 4;
				}
				switch (featureDescriptor.featureType)
				{
				case global::UnityEngine.InputSystem.XR.FeatureType.Binary:
					builder.AddControl(name).WithLayout("Button").WithByteOffset(num)
						.WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit)
						.WithUsages(list2);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.DiscreteStates:
					builder.AddControl(name).WithLayout("Integer").WithByteOffset(num)
						.WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInt)
						.WithUsages(list2);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.Axis1D:
					builder.AddControl(name).WithLayout("Analog").WithRange(-1f, 1f)
						.WithByteOffset(num)
						.WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat)
						.WithUsages(list2);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.Axis2D:
					builder.AddControl(name).WithLayout("Stick").WithByteOffset(num)
						.WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatVector2)
						.WithUsages(list2);
					builder.AddControl(name + "/x").WithLayout("Analog").WithRange(-1f, 1f);
					builder.AddControl(name + "/y").WithLayout("Analog").WithRange(-1f, 1f);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.Axis3D:
					builder.AddControl(name).WithLayout("Vector3").WithByteOffset(num)
						.WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatVector3)
						.WithUsages(list2);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.Rotation:
					builder.AddControl(name).WithLayout("Quaternion").WithByteOffset(num)
						.WithFormat(global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatQuaternion)
						.WithUsages(list2);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.Bone:
					builder.AddControl(name).WithLayout("Bone").WithByteOffset(num)
						.WithUsages(list2);
					break;
				case global::UnityEngine.InputSystem.XR.FeatureType.Eyes:
					builder.AddControl(name).WithLayout("Eyes").WithByteOffset(num)
						.WithUsages(list2);
					break;
				}
				num += sizeOfFeature;
			}
			return builder.Build();
		}
	}
}
