namespace UnityEngine.InputSystem.Layouts
{
	internal struct InputDeviceBuilder : global::System.IDisposable
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct RefInstance : global::System.IDisposable
		{
			public void Dispose()
			{
				s_InstanceRef--;
				if (s_InstanceRef <= 0)
				{
					s_Instance.Dispose();
					s_Instance = default(global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder);
					s_InstanceRef = 0;
				}
				else
				{
					s_Instance.Reset();
				}
			}
		}

		private global::UnityEngine.InputSystem.InputDevice m_Device;

		private global::UnityEngine.InputSystem.Layouts.InputControlLayout.CacheRefInstance m_LayoutCacheRef;

		private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem> m_ChildControlOverrides;

		private global::System.Collections.Generic.List<uint> m_StateOffsetToControlMap;

		private global::System.Text.StringBuilder m_StringBuilder;

		private const uint kSizeForControlUsingStateFromOtherControl = uint.MaxValue;

		private static global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder s_Instance;

		private static int s_InstanceRef;

		internal static ref global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder instance => ref s_Instance;

		public void Setup(global::UnityEngine.InputSystem.Utilities.InternedString layout, global::UnityEngine.InputSystem.Utilities.InternedString variants, global::UnityEngine.InputSystem.Layouts.InputDeviceDescription deviceDescription = default(global::UnityEngine.InputSystem.Layouts.InputDeviceDescription))
		{
			m_LayoutCacheRef = global::UnityEngine.InputSystem.Layouts.InputControlLayout.CacheRef();
			InstantiateLayout(layout, variants, default(global::UnityEngine.InputSystem.Utilities.InternedString), null);
			FinalizeControlHierarchy();
			m_StateOffsetToControlMap.Sort();
			m_Device.m_Description = deviceDescription;
			m_Device.m_StateOffsetToControlMap = m_StateOffsetToControlMap.ToArray();
			m_Device.CallFinishSetupRecursive();
		}

		public global::UnityEngine.InputSystem.InputDevice Finish()
		{
			global::UnityEngine.InputSystem.InputDevice device = m_Device;
			int num = 0;
			foreach (global::UnityEngine.InputSystem.InputControl allControl in device.allControls)
			{
				if (allControl.isButton)
				{
					num++;
				}
			}
			device.m_ButtonControlsCheckingPressState = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Controls.ButtonControl>(num);
			device.m_UpdatedButtons = new global::System.Collections.Generic.HashSet<int>(num);
			Reset();
			return device;
		}

		public void Dispose()
		{
			m_LayoutCacheRef.Dispose();
		}

		private void Reset()
		{
			m_Device = null;
			m_ChildControlOverrides?.Clear();
			m_StateOffsetToControlMap?.Clear();
		}

		private global::UnityEngine.InputSystem.InputControl InstantiateLayout(global::UnityEngine.InputSystem.Utilities.InternedString layout, global::UnityEngine.InputSystem.Utilities.InternedString variants, global::UnityEngine.InputSystem.Utilities.InternedString name, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Layouts.InputControlLayout layout2 = FindOrLoadLayout(layout);
			return InstantiateLayout(layout2, variants, name, parent);
		}

		private global::UnityEngine.InputSystem.InputControl InstantiateLayout(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout, global::UnityEngine.InputSystem.Utilities.InternedString variants, global::UnityEngine.InputSystem.Utilities.InternedString name, global::UnityEngine.InputSystem.InputControl parent)
		{
			if (!(global::System.Activator.CreateInstance(layout.type) is global::UnityEngine.InputSystem.InputControl inputControl))
			{
				throw new global::System.InvalidOperationException($"Type '{layout.type.Name}' referenced by layout '{layout.name}' is not an InputControl");
			}
			if (inputControl is global::UnityEngine.InputSystem.InputDevice device)
			{
				if (parent != null)
				{
					throw new global::System.InvalidOperationException($"Cannot instantiate device layout '{layout.name}' as child of '{parent.path}'; devices must be added at root");
				}
				m_Device = device;
				m_Device.m_StateBlock.byteOffset = 0u;
				m_Device.m_StateBlock.bitOffset = 0u;
				m_Device.m_StateBlock.format = layout.stateFormat;
				m_Device.m_AliasesForEachControl = null;
				m_Device.m_ChildrenForEachControl = null;
				m_Device.m_UpdatedButtons = null;
				m_Device.m_UsagesForEachControl = null;
				m_Device.m_UsageToControl = null;
				if (layout.m_UpdateBeforeRender == true)
				{
					m_Device.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.UpdateBeforeRender;
				}
				if (layout.canRunInBackground.HasValue)
				{
					m_Device.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackgroundHasBeenQueried;
					if (layout.canRunInBackground == true)
					{
						m_Device.m_DeviceFlags |= global::UnityEngine.InputSystem.InputDevice.DeviceFlags.CanRunInBackground;
					}
				}
			}
			else if (parent == null)
			{
				throw new global::System.InvalidOperationException($"Toplevel layout used with InputDeviceBuilder must be a device layout; '{layout.name}' is a control layout");
			}
			if (name.IsEmpty())
			{
				name = layout.name;
				int num = name.ToString().LastIndexOf(':');
				if (num != -1)
				{
					name = new global::UnityEngine.InputSystem.Utilities.InternedString(name.ToString().Substring(num + 1));
				}
			}
			if (name.ToString().IndexOf('/') != -1)
			{
				name = new global::UnityEngine.InputSystem.Utilities.InternedString(name.ToString().CleanSlashes());
			}
			if (variants.IsEmpty())
			{
				variants = layout.variants;
				if (variants.IsEmpty())
				{
					variants = global::UnityEngine.InputSystem.Layouts.InputControlLayout.DefaultVariant;
				}
			}
			inputControl.m_Name = name;
			inputControl.m_DisplayNameFromLayout = layout.m_DisplayName;
			inputControl.m_Layout = layout.name;
			inputControl.m_Variants = variants;
			inputControl.m_Parent = parent;
			inputControl.m_Device = m_Device;
			if (inputControl is global::UnityEngine.InputSystem.InputDevice)
			{
				inputControl.noisy = layout.isNoisy;
			}
			bool haveChildrenUsingStateFromOtherControls = false;
			try
			{
				AddChildControls(layout, variants, inputControl, ref haveChildrenUsingStateFromOtherControls);
			}
			catch
			{
				throw;
			}
			ComputeStateLayout(inputControl);
			if (haveChildrenUsingStateFromOtherControls)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] controls = layout.m_Controls;
				for (int i = 0; i < controls.Length; i++)
				{
					ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem reference = ref controls[i];
					if (!string.IsNullOrEmpty(reference.useStateFrom))
					{
						ApplyUseStateFrom(inputControl, ref reference, layout);
					}
				}
			}
			return inputControl;
		}

		private void AddChildControls(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout, global::UnityEngine.InputSystem.Utilities.InternedString variants, global::UnityEngine.InputSystem.InputControl parent, ref bool haveChildrenUsingStateFromOtherControls)
		{
			global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem[] controls = layout.m_Controls;
			if (controls == null)
			{
				return;
			}
			int num = 0;
			bool flag = false;
			for (int i = 0; i < controls.Length; i++)
			{
				if (!controls[i].variants.IsEmpty() && !global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(controls[i].variants, variants, ";"[0]))
				{
					continue;
				}
				if (controls[i].isModifyingExistingControl)
				{
					if (controls[i].isArray)
					{
						throw new global::System.NotSupportedException($"Control '{controls[i].name}' in layout '{layout.name}' is modifying the child of another control but is marked as an array");
					}
					flag = true;
					InsertChildControlOverride(parent, ref controls[i]);
				}
				else
				{
					num = ((!controls[i].isArray) ? (num + 1) : (num + controls[i].arraySize));
				}
			}
			if (num == 0)
			{
				parent.m_ChildCount = 0;
				parent.m_ChildStartIndex = 0;
				haveChildrenUsingStateFromOtherControls = false;
				return;
			}
			int num2 = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.GrowBy(ref m_Device.m_ChildrenForEachControl, num);
			int num3 = num2;
			for (int j = 0; j < controls.Length; j++)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem = controls[j];
				if (controlItem.isModifyingExistingControl || (!controlItem.variants.IsEmpty() && !global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(controlItem.variants, variants, ";"[0])))
				{
					continue;
				}
				if (controlItem.isArray)
				{
					for (int k = 0; k < controlItem.arraySize; k++)
					{
						string nameOverride = string.Concat(controlItem.name, k.ToString());
						global::UnityEngine.InputSystem.InputControl inputControl = AddChildControl(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, controlItem, num3, nameOverride);
						num3++;
						if (inputControl.m_StateBlock.byteOffset != uint.MaxValue)
						{
							inputControl.m_StateBlock.byteOffset += (uint)(k * (int)inputControl.m_StateBlock.alignedSizeInBytes);
						}
					}
				}
				else
				{
					AddChildControl(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, controlItem, num3);
					num3++;
				}
			}
			parent.m_ChildCount = num;
			parent.m_ChildStartIndex = num2;
			if (!flag)
			{
				return;
			}
			for (int l = 0; l < controls.Length; l++)
			{
				global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem2 = controls[l];
				if (controlItem2.isModifyingExistingControl && (controlItem2.variants.IsEmpty() || global::UnityEngine.InputSystem.Utilities.StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(controls[l].variants, variants, ";"[0])))
				{
					AddChildControlIfMissing(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, ref controlItem2);
				}
			}
		}

		private global::UnityEngine.InputSystem.InputControl AddChildControl(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout, global::UnityEngine.InputSystem.Utilities.InternedString variants, global::UnityEngine.InputSystem.InputControl parent, ref bool haveChildrenUsingStateFromOtherControls, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem, int childIndex, string nameOverride = null)
		{
			global::UnityEngine.InputSystem.Utilities.InternedString internedString = ((nameOverride != null) ? new global::UnityEngine.InputSystem.Utilities.InternedString(nameOverride) : controlItem.name);
			if (string.IsNullOrEmpty(controlItem.layout))
			{
				throw new global::System.InvalidOperationException($"Layout has not been set on control '{controlItem.name}' in '{layout.name}'");
			}
			if (m_ChildControlOverrides != null)
			{
				string key = ChildControlOverridePath(parent, internedString);
				if (m_ChildControlOverrides.TryGetValue(key, out var value))
				{
					controlItem = value.Merge(controlItem);
				}
			}
			global::UnityEngine.InputSystem.Utilities.InternedString layout2 = controlItem.layout;
			global::UnityEngine.InputSystem.InputControl inputControl;
			try
			{
				inputControl = InstantiateLayout(layout2, variants, internedString, parent);
			}
			catch (global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutNotFoundException ex)
			{
				throw new global::UnityEngine.InputSystem.Layouts.InputControlLayout.LayoutNotFoundException($"Cannot find layout '{ex.layout}' used in control '{internedString}' of layout '{layout.name}'", ex);
			}
			m_Device.m_ChildrenForEachControl[childIndex] = inputControl;
			inputControl.noisy = controlItem.isNoisy;
			inputControl.synthetic = controlItem.isSynthetic;
			inputControl.usesStateFromOtherControl = !string.IsNullOrEmpty(controlItem.useStateFrom);
			inputControl.dontReset = (inputControl.noisy || controlItem.dontReset) && !inputControl.usesStateFromOtherControl;
			if (inputControl.noisy)
			{
				m_Device.noisy = true;
			}
			inputControl.isButton = inputControl is global::UnityEngine.InputSystem.Controls.ButtonControl;
			if (inputControl.dontReset)
			{
				m_Device.hasDontResetControls = true;
			}
			inputControl.m_DisplayNameFromLayout = controlItem.displayName;
			inputControl.m_ShortDisplayNameFromLayout = controlItem.shortDisplayName;
			inputControl.m_DefaultState = controlItem.defaultState;
			if (!inputControl.m_DefaultState.isEmpty)
			{
				m_Device.hasControlsWithDefaultState = true;
			}
			if (!controlItem.minValue.isEmpty)
			{
				inputControl.m_MinValue = controlItem.minValue;
			}
			if (!controlItem.maxValue.isEmpty)
			{
				inputControl.m_MaxValue = controlItem.maxValue;
			}
			if (!inputControl.usesStateFromOtherControl)
			{
				inputControl.m_StateBlock.byteOffset = controlItem.offset;
				inputControl.m_StateBlock.bitOffset = controlItem.bit;
				if (controlItem.sizeInBits != 0)
				{
					inputControl.m_StateBlock.sizeInBits = controlItem.sizeInBits;
				}
				if (controlItem.format != 0)
				{
					SetFormat(inputControl, controlItem);
				}
			}
			else
			{
				inputControl.m_StateBlock.sizeInBits = uint.MaxValue;
				haveChildrenUsingStateFromOtherControls = true;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.InternedString> usages = controlItem.usages;
			if (usages.Count > 0)
			{
				int count = usages.Count;
				int num = (inputControl.m_UsageStartIndex = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendToImmutable(ref m_Device.m_UsagesForEachControl, usages.m_Array));
				inputControl.m_UsageCount = count;
				global::UnityEngine.InputSystem.Utilities.ArrayHelpers.GrowBy(ref m_Device.m_UsageToControl, count);
				for (int i = 0; i < count; i++)
				{
					m_Device.m_UsageToControl[num + i] = inputControl;
				}
			}
			if (controlItem.aliases.Count > 0)
			{
				int count2 = controlItem.aliases.Count;
				int aliasStartIndex = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.AppendToImmutable(ref m_Device.m_AliasesForEachControl, controlItem.aliases.m_Array);
				inputControl.m_AliasStartIndex = aliasStartIndex;
				inputControl.m_AliasCount = count2;
			}
			if (controlItem.parameters.Count > 0)
			{
				global::UnityEngine.InputSystem.Utilities.NamedValue.ApplyAllToObject(inputControl, controlItem.parameters);
			}
			if (controlItem.processors.Count > 0)
			{
				AddProcessors(inputControl, ref controlItem, layout.name);
			}
			return inputControl;
		}

		private void InsertChildControlOverride(global::UnityEngine.InputSystem.InputControl parent, ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem)
		{
			if (m_ChildControlOverrides == null)
			{
				m_ChildControlOverrides = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem>();
			}
			string key = ChildControlOverridePath(parent, controlItem.name);
			if (!m_ChildControlOverrides.TryGetValue(key, out var value))
			{
				m_ChildControlOverrides[key] = controlItem;
				return;
			}
			value = value.Merge(controlItem);
			m_ChildControlOverrides[key] = value;
		}

		private string ChildControlOverridePath(global::UnityEngine.InputSystem.InputControl parent, global::UnityEngine.InputSystem.Utilities.InternedString controlName)
		{
			string text = controlName.ToLower();
			for (global::UnityEngine.InputSystem.InputControl inputControl = parent; inputControl != m_Device; inputControl = inputControl.m_Parent)
			{
				text = inputControl.m_Name.ToLower() + "/" + text;
			}
			return text;
		}

		private void AddChildControlIfMissing(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout, global::UnityEngine.InputSystem.Utilities.InternedString variants, global::UnityEngine.InputSystem.InputControl parent, ref bool haveChildrenUsingStateFromOtherControls, ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem)
		{
			global::UnityEngine.InputSystem.InputControl inputControl = global::UnityEngine.InputSystem.InputControlPath.TryFindChild(parent, controlItem.name);
			if (inputControl == null)
			{
				inputControl = InsertChildControl(layout, variants, parent, ref haveChildrenUsingStateFromOtherControls, ref controlItem);
				if (inputControl.parent != parent)
				{
					ComputeStateLayout(inputControl.parent);
				}
			}
		}

		private global::UnityEngine.InputSystem.InputControl InsertChildControl(global::UnityEngine.InputSystem.Layouts.InputControlLayout layout, global::UnityEngine.InputSystem.Utilities.InternedString variant, global::UnityEngine.InputSystem.InputControl parent, ref bool haveChildrenUsingStateFromOtherControls, ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem)
		{
			string text = controlItem.name.ToString();
			int num = text.LastIndexOf('/');
			if (num == -1)
			{
				throw new global::System.InvalidOperationException("InsertChildControl has to be called with a slash-separated path");
			}
			string text2 = text.Substring(0, num);
			global::UnityEngine.InputSystem.InputControl inputControl = global::UnityEngine.InputSystem.InputControlPath.TryFindChild(parent, text2);
			if (inputControl == null)
			{
				throw new global::System.InvalidOperationException($"Cannot find parent '{text2}' of control '{controlItem.name}' in layout '{layout.name}'");
			}
			string text3 = text.Substring(num + 1);
			if (text3.Length == 0)
			{
				throw new global::System.InvalidOperationException($"Path cannot end in '/' (control '{controlItem.name}' in layout '{layout.name}')");
			}
			int num2 = inputControl.m_ChildStartIndex;
			if (num2 == 0)
			{
				num2 = (inputControl.m_ChildStartIndex = global::UnityEngine.InputSystem.Utilities.ArrayHelpers.LengthSafe(m_Device.m_ChildrenForEachControl));
			}
			int num3 = num2 + inputControl.m_ChildCount;
			ShiftChildIndicesInHierarchyOneUp(m_Device, num3, inputControl);
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.InsertAt(ref m_Device.m_ChildrenForEachControl, num3, null);
			inputControl.m_ChildCount++;
			return AddChildControl(layout, variant, inputControl, ref haveChildrenUsingStateFromOtherControls, controlItem, num3, text3);
		}

		private static void ApplyUseStateFrom(global::UnityEngine.InputSystem.InputControl parent, ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem, global::UnityEngine.InputSystem.Layouts.InputControlLayout layout)
		{
			global::UnityEngine.InputSystem.InputControl inputControl = global::UnityEngine.InputSystem.InputControlPath.TryFindChild(parent, controlItem.name);
			global::UnityEngine.InputSystem.InputControl inputControl2 = global::UnityEngine.InputSystem.InputControlPath.TryFindChild(parent, controlItem.useStateFrom);
			if (inputControl2 == null)
			{
				throw new global::System.InvalidOperationException($"Cannot find control '{controlItem.useStateFrom}' referenced in 'useStateFrom' of control '{controlItem.name}' in layout '{layout.name}'");
			}
			inputControl.m_StateBlock = inputControl2.m_StateBlock;
			inputControl.usesStateFromOtherControl = true;
			inputControl.dontReset = inputControl2.dontReset;
			if (inputControl.parent != inputControl2.parent)
			{
				for (global::UnityEngine.InputSystem.InputControl parent2 = inputControl2.parent; parent2 != parent; parent2 = parent2.parent)
				{
					inputControl.m_StateBlock.byteOffset += parent2.m_StateBlock.byteOffset;
				}
			}
		}

		private static void ShiftChildIndicesInHierarchyOneUp(global::UnityEngine.InputSystem.InputDevice device, int startIndex, global::UnityEngine.InputSystem.InputControl exceptControl)
		{
			global::UnityEngine.InputSystem.InputControl[] childrenForEachControl = device.m_ChildrenForEachControl;
			int num = childrenForEachControl.Length;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.InputSystem.InputControl inputControl = childrenForEachControl[i];
				if (inputControl != null && inputControl != exceptControl && inputControl.m_ChildStartIndex >= startIndex)
				{
					inputControl.m_ChildStartIndex++;
				}
			}
		}

		private void SetDisplayName(global::UnityEngine.InputSystem.InputControl control, string longDisplayNameFromLayout, string shortDisplayNameFromLayout, bool shortName)
		{
			string text = (shortName ? shortDisplayNameFromLayout : longDisplayNameFromLayout);
			if (string.IsNullOrEmpty(text))
			{
				if (shortName)
				{
					if (control.parent != null && control.parent != control.device)
					{
						if (m_StringBuilder == null)
						{
							m_StringBuilder = new global::System.Text.StringBuilder();
						}
						m_StringBuilder.Length = 0;
						AddParentDisplayNameRecursive(control.parent, m_StringBuilder, shortName: true);
						if (m_StringBuilder.Length == 0)
						{
							control.m_ShortDisplayNameFromLayout = null;
							return;
						}
						if (!string.IsNullOrEmpty(longDisplayNameFromLayout))
						{
							m_StringBuilder.Append(longDisplayNameFromLayout);
						}
						else
						{
							m_StringBuilder.Append(control.name);
						}
						control.m_ShortDisplayNameFromLayout = m_StringBuilder.ToString();
					}
					else
					{
						control.m_ShortDisplayNameFromLayout = null;
					}
					return;
				}
				text = control.name;
			}
			if (control.parent != null && control.parent != control.device)
			{
				if (m_StringBuilder == null)
				{
					m_StringBuilder = new global::System.Text.StringBuilder();
				}
				m_StringBuilder.Length = 0;
				AddParentDisplayNameRecursive(control.parent, m_StringBuilder, shortName);
				m_StringBuilder.Append(text);
				text = m_StringBuilder.ToString();
			}
			if (shortName)
			{
				control.m_ShortDisplayNameFromLayout = text;
			}
			else
			{
				control.m_DisplayNameFromLayout = text;
			}
		}

		private static void AddParentDisplayNameRecursive(global::UnityEngine.InputSystem.InputControl control, global::System.Text.StringBuilder stringBuilder, bool shortName)
		{
			if (control.parent != null && control.parent != control.device)
			{
				AddParentDisplayNameRecursive(control.parent, stringBuilder, shortName);
			}
			if (shortName)
			{
				string value = control.shortDisplayName;
				if (string.IsNullOrEmpty(value))
				{
					value = control.displayName;
				}
				stringBuilder.Append(value);
			}
			else
			{
				stringBuilder.Append(control.displayName);
			}
			stringBuilder.Append(' ');
		}

		private static void AddProcessors(global::UnityEngine.InputSystem.InputControl control, ref global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem, string layoutName)
		{
			int count = controlItem.processors.Count;
			for (int i = 0; i < count; i++)
			{
				string name = controlItem.processors[i].name;
				global::System.Type type = global::UnityEngine.InputSystem.InputProcessor.s_Processors.LookupTypeRegistration(name);
				if (type == null)
				{
					throw new global::System.InvalidOperationException($"Cannot find processor '{name}' referenced by control '{controlItem.name}' in layout '{layoutName}'");
				}
				object first = global::System.Activator.CreateInstance(type);
				global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue> parameters = controlItem.processors[i].parameters;
				if (parameters.Count > 0)
				{
					global::UnityEngine.InputSystem.Utilities.NamedValue.ApplyAllToObject(first, parameters);
				}
				control.AddProcessor(first);
			}
		}

		private static void SetFormat(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.Layouts.InputControlLayout.ControlItem controlItem)
		{
			control.m_StateBlock.format = controlItem.format;
			if (controlItem.sizeInBits == 0)
			{
				int sizeOfPrimitiveFormatInBits = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.GetSizeOfPrimitiveFormatInBits(controlItem.format);
				if (sizeOfPrimitiveFormatInBits != -1)
				{
					control.m_StateBlock.sizeInBits = (uint)sizeOfPrimitiveFormatInBits;
				}
			}
		}

		private static global::UnityEngine.InputSystem.Layouts.InputControlLayout FindOrLoadLayout(string name)
		{
			return global::UnityEngine.InputSystem.Layouts.InputControlLayout.cache.FindOrLoadLayout(name);
		}

		private static void ComputeStateLayout(global::UnityEngine.InputSystem.InputControl control)
		{
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.InputControl> children = control.children;
			if (control.m_StateBlock.sizeInBits == 0 && control.m_StateBlock.format != 0)
			{
				int sizeOfPrimitiveFormatInBits = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.GetSizeOfPrimitiveFormatInBits(control.m_StateBlock.format);
				if (sizeOfPrimitiveFormatInBits != -1)
				{
					control.m_StateBlock.sizeInBits = (uint)sizeOfPrimitiveFormatInBits;
				}
			}
			if (control.m_StateBlock.sizeInBits == 0 && children.Count == 0)
			{
				throw new global::System.InvalidOperationException("Control '" + control.path + "' with layout '" + control.layout + "' has no size set and has no children to compute size from");
			}
			if (children.Count == 0)
			{
				return;
			}
			uint num = 0u;
			foreach (global::UnityEngine.InputSystem.InputControl item in children)
			{
				if (item.m_StateBlock.sizeInBits == uint.MaxValue)
				{
					continue;
				}
				uint sizeInBits = item.m_StateBlock.sizeInBits;
				if (sizeInBits == 0 || sizeInBits == uint.MaxValue)
				{
					throw new global::System.InvalidOperationException("Child '" + item.name + "' of '" + control.name + "' has no size set!");
				}
				if (item.m_StateBlock.byteOffset != uint.MaxValue && item.m_StateBlock.byteOffset != 4294967294u)
				{
					if (item.m_StateBlock.bitOffset == uint.MaxValue)
					{
						item.m_StateBlock.bitOffset = 0u;
					}
					uint num2 = global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ComputeFollowingByteOffset(item.m_StateBlock.byteOffset, item.m_StateBlock.bitOffset + sizeInBits);
					if (num2 > num)
					{
						num = num2;
					}
				}
			}
			uint num3 = num;
			global::UnityEngine.InputSystem.InputControl inputControl = null;
			uint num4 = 0u;
			foreach (global::UnityEngine.InputSystem.InputControl item2 in children)
			{
				if ((item2.m_StateBlock.byteOffset != uint.MaxValue && item2.m_StateBlock.byteOffset != 4294967294u) || item2.m_StateBlock.sizeInBits == uint.MaxValue)
				{
					continue;
				}
				bool num5 = item2.m_StateBlock.sizeInBits % 8 != 0;
				if (num5)
				{
					if (inputControl == null)
					{
						inputControl = item2;
					}
					if (item2.m_StateBlock.bitOffset == uint.MaxValue || item2.m_StateBlock.bitOffset == 4294967294u)
					{
						item2.m_StateBlock.bitOffset = num4;
						num4 += item2.m_StateBlock.sizeInBits;
					}
					else
					{
						uint num6 = item2.m_StateBlock.bitOffset + item2.m_StateBlock.sizeInBits;
						if (num6 > num4)
						{
							num4 = num6;
						}
					}
				}
				else
				{
					if (inputControl != null)
					{
						num3 = global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ComputeFollowingByteOffset(num3, num4);
						inputControl = null;
					}
					if (item2.m_StateBlock.bitOffset == uint.MaxValue)
					{
						item2.m_StateBlock.bitOffset = 0u;
					}
					num3 = global::UnityEngine.InputSystem.Utilities.MemoryHelpers.AlignNatural(num3, item2.m_StateBlock.alignedSizeInBytes);
				}
				item2.m_StateBlock.byteOffset = num3;
				if (!num5)
				{
					num3 = global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ComputeFollowingByteOffset(num3, item2.m_StateBlock.sizeInBits);
				}
			}
			if (inputControl != null)
			{
				num3 = global::UnityEngine.InputSystem.Utilities.MemoryHelpers.ComputeFollowingByteOffset(num3, num4);
			}
			uint num7 = num3;
			control.m_StateBlock.sizeInBits = num7 * 8;
		}

		private void FinalizeControlHierarchy()
		{
			if (m_StateOffsetToControlMap == null)
			{
				m_StateOffsetToControlMap = new global::System.Collections.Generic.List<uint>();
			}
			if ((long)m_Device.allControls.Count > 1024L)
			{
				throw new global::System.NotSupportedException($"Device '{m_Device}' exceeds maximum supported control count of {1024u} (has {m_Device.allControls.Count} controls)");
			}
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode controlBitRangeNode = new global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode((ushort)(m_Device.m_StateBlock.sizeInBits - 1));
			m_Device.m_ControlTreeNodes = new global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode[1];
			m_Device.m_ControlTreeNodes[0] = controlBitRangeNode;
			int controlIndiciesNextFreeIndex = 0;
			FinalizeControlHierarchyRecursive(m_Device, -1, m_Device.m_ChildrenForEachControl, noisy: false, dontReset: false, ref controlIndiciesNextFreeIndex);
		}

		private void FinalizeControlHierarchyRecursive(global::UnityEngine.InputSystem.InputControl control, int controlIndex, global::UnityEngine.InputSystem.InputControl[] allControls, bool noisy, bool dontReset, ref int controlIndiciesNextFreeIndex)
		{
			if (control.m_ChildCount == 0)
			{
				if (control.m_StateBlock.effectiveBitOffset >= 8192)
				{
					throw new global::System.NotSupportedException($"Control '{control}' exceeds maximum supported state bit offset of {8191u} (bit offset {control.stateBlock.effectiveBitOffset})");
				}
				if (control.m_StateBlock.sizeInBits >= 512)
				{
					throw new global::System.NotSupportedException($"Control '{control}' exceeds maximum supported state bit size of {511u} (bit offset {control.stateBlock.sizeInBits})");
				}
			}
			if (control != m_Device)
			{
				InsertControlBitRangeNode(ref m_Device.m_ControlTreeNodes[0], control, ref controlIndiciesNextFreeIndex, 0);
			}
			if (control.m_ChildCount == 0)
			{
				m_StateOffsetToControlMap.Add(global::UnityEngine.InputSystem.InputDevice.EncodeStateOffsetToControlMapEntry((uint)controlIndex, control.m_StateBlock.effectiveBitOffset, control.m_StateBlock.sizeInBits));
			}
			string displayNameFromLayout = control.m_DisplayNameFromLayout;
			string shortDisplayNameFromLayout = control.m_ShortDisplayNameFromLayout;
			SetDisplayName(control, displayNameFromLayout, shortDisplayNameFromLayout, shortName: false);
			SetDisplayName(control, displayNameFromLayout, shortDisplayNameFromLayout, shortName: true);
			if (control != control.device)
			{
				if (noisy)
				{
					control.noisy = true;
				}
				else
				{
					noisy = control.noisy;
				}
				if (dontReset)
				{
					control.dontReset = true;
				}
				else
				{
					dontReset = control.dontReset;
				}
			}
			uint byteOffset = control.m_StateBlock.byteOffset;
			int childCount = control.m_ChildCount;
			int childStartIndex = control.m_ChildStartIndex;
			for (int i = 0; i < childCount; i++)
			{
				int num = childStartIndex + i;
				global::UnityEngine.InputSystem.InputControl inputControl = allControls[num];
				inputControl.m_StateBlock.byteOffset += byteOffset;
				FinalizeControlHierarchyRecursive(inputControl, num, allControls, noisy, dontReset, ref controlIndiciesNextFreeIndex);
			}
			control.isSetupFinished = true;
		}

		private void InsertControlBitRangeNode(ref global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parent, global::UnityEngine.InputSystem.InputControl control, ref int controlIndiciesNextFreeIndex, ushort startOffset)
		{
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode left;
			global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode right;
			if (parent.leftChildIndex == -1)
			{
				ushort bestMidPoint = GetBestMidPoint(parent, startOffset);
				left = new global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode(bestMidPoint);
				right = new global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode(parent.endBitOffset);
				AddChildren(ref parent, left, right);
			}
			else
			{
				left = m_Device.m_ControlTreeNodes[parent.leftChildIndex];
				right = m_Device.m_ControlTreeNodes[parent.leftChildIndex + 1];
			}
			if (control.m_StateBlock.effectiveBitOffset < left.endBitOffset && control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits > left.endBitOffset)
			{
				AddControlToNode(control, ref controlIndiciesNextFreeIndex, parent.leftChildIndex);
				AddControlToNode(control, ref controlIndiciesNextFreeIndex, parent.leftChildIndex + 1);
			}
			else if (control.m_StateBlock.effectiveBitOffset == startOffset && control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits == left.endBitOffset)
			{
				AddControlToNode(control, ref controlIndiciesNextFreeIndex, parent.leftChildIndex);
			}
			else if (control.m_StateBlock.effectiveBitOffset == left.endBitOffset && control.m_StateBlock.effectiveBitOffset + control.m_StateBlock.sizeInBits == right.endBitOffset)
			{
				AddControlToNode(control, ref controlIndiciesNextFreeIndex, parent.leftChildIndex + 1);
			}
			else if (control.m_StateBlock.effectiveBitOffset < left.endBitOffset)
			{
				InsertControlBitRangeNode(ref m_Device.m_ControlTreeNodes[parent.leftChildIndex], control, ref controlIndiciesNextFreeIndex, startOffset);
			}
			else
			{
				InsertControlBitRangeNode(ref m_Device.m_ControlTreeNodes[parent.leftChildIndex + 1], control, ref controlIndiciesNextFreeIndex, left.endBitOffset);
			}
		}

		private ushort GetBestMidPoint(global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parent, ushort startOffset)
		{
			ushort num = (ushort)(startOffset + ((parent.endBitOffset - startOffset - 1) / 2 + 1));
			ushort num2 = ushort.MaxValue;
			ushort num3 = ushort.MaxValue;
			global::UnityEngine.InputSystem.InputControl[] childrenForEachControl = m_Device.m_ChildrenForEachControl;
			for (int i = 0; i < childrenForEachControl.Length; i++)
			{
				global::UnityEngine.InputSystem.LowLevel.InputStateBlock stateBlock = childrenForEachControl[i].m_StateBlock;
				if (stateBlock.effectiveBitOffset + stateBlock.sizeInBits - 1 >= startOffset && stateBlock.effectiveBitOffset < parent.endBitOffset && stateBlock.sizeInBits <= parent.endBitOffset - startOffset && stateBlock.effectiveBitOffset != startOffset && stateBlock.effectiveBitOffset + stateBlock.sizeInBits != parent.endBitOffset)
				{
					if (global::System.Math.Abs(stateBlock.effectiveBitOffset + stateBlock.sizeInBits - (int)num) < global::System.Math.Abs(num2 - num) && stateBlock.effectiveBitOffset + stateBlock.sizeInBits < parent.endBitOffset)
					{
						num2 = (ushort)(stateBlock.effectiveBitOffset + stateBlock.sizeInBits);
					}
					if (global::System.Math.Abs(stateBlock.effectiveBitOffset - (int)num) < global::System.Math.Abs(num3 - num) && stateBlock.effectiveBitOffset >= startOffset)
					{
						num3 = (ushort)stateBlock.effectiveBitOffset;
					}
				}
			}
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			childrenForEachControl = m_Device.m_ChildrenForEachControl;
			foreach (global::UnityEngine.InputSystem.InputControl inputControl in childrenForEachControl)
			{
				if (num3 != ushort.MaxValue && num3 > inputControl.m_StateBlock.effectiveBitOffset && num3 < inputControl.m_StateBlock.effectiveBitOffset + inputControl.m_StateBlock.sizeInBits)
				{
					num5++;
				}
				if (num2 != ushort.MaxValue && num2 > inputControl.m_StateBlock.effectiveBitOffset && num2 < inputControl.m_StateBlock.effectiveBitOffset + inputControl.m_StateBlock.sizeInBits)
				{
					num6++;
				}
				if (num > inputControl.m_StateBlock.effectiveBitOffset && num < inputControl.m_StateBlock.effectiveBitOffset + inputControl.m_StateBlock.sizeInBits)
				{
					num4++;
				}
			}
			if (num2 != ushort.MaxValue && num6 <= num5 && num6 <= num4)
			{
				return num2;
			}
			if (num3 != ushort.MaxValue && num5 <= num6 && num5 <= num4)
			{
				return num3;
			}
			return num;
		}

		private void AddControlToNode(global::UnityEngine.InputSystem.InputControl control, ref int controlIndiciesNextFreeIndex, int nodeIndex)
		{
			ref global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode reference = ref m_Device.m_ControlTreeNodes[nodeIndex];
			ushort controlStartIndex = reference.controlStartIndex;
			if (reference.controlCount == 0)
			{
				reference.controlStartIndex = (ushort)controlIndiciesNextFreeIndex;
				controlStartIndex = reference.controlStartIndex;
			}
			global::UnityEngine.InputSystem.Utilities.ArrayHelpers.InsertAt(ref m_Device.m_ControlTreeIndices, reference.controlStartIndex + reference.controlCount, GetControlIndex(control));
			reference.controlCount++;
			controlIndiciesNextFreeIndex++;
			for (int i = 0; i < m_Device.m_ControlTreeNodes.Length; i++)
			{
				if (m_Device.m_ControlTreeNodes[i].controlCount != 0 && m_Device.m_ControlTreeNodes[i].controlStartIndex > controlStartIndex)
				{
					m_Device.m_ControlTreeNodes[i].controlStartIndex++;
				}
			}
		}

		private void AddChildren(ref global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode parent, global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode left, global::UnityEngine.InputSystem.InputDevice.ControlBitRangeNode right)
		{
			if (parent.leftChildIndex == -1)
			{
				int num = m_Device.m_ControlTreeNodes.Length;
				parent.leftChildIndex = (short)num;
				global::System.Array.Resize(ref m_Device.m_ControlTreeNodes, num + 2);
				m_Device.m_ControlTreeNodes[num] = left;
				m_Device.m_ControlTreeNodes[num + 1] = right;
			}
		}

		private ushort GetControlIndex(global::UnityEngine.InputSystem.InputControl control)
		{
			for (int i = 0; i < m_Device.m_ChildrenForEachControl.Length; i++)
			{
				if (control == m_Device.m_ChildrenForEachControl[i])
				{
					return (ushort)i;
				}
			}
			throw new global::System.InvalidOperationException($"InputDeviceBuilder error. Couldn't find control {control}.");
		}

		internal static global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.RefInstance Ref()
		{
			s_InstanceRef++;
			return default(global::UnityEngine.InputSystem.Layouts.InputDeviceBuilder.RefInstance);
		}
	}
}
