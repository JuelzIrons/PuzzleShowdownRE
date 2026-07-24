namespace UnityEngine.InputSystem
{
	internal class FastMouse : global::UnityEngine.InputSystem.Mouse, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver, global::UnityEngine.InputSystem.LowLevel.IEventMerger
	{
		public const string metadata = "AutoWindowSpace;Vector2;Delta;Button;Axis;Digital;Integer;Mouse;Pointer";

		public FastMouse()
		{
			global::UnityEngine.InputSystem.InputControlExtensions.DeviceBuilder deviceBuilder = this.Setup(30, 10, 2).WithName("Mouse").WithDisplayName("Mouse")
				.WithChildren(0, 14)
				.WithLayout(new global::UnityEngine.InputSystem.Utilities.InternedString("Mouse"))
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1297044819),
					sizeInBits = 392u
				});
			global::UnityEngine.InputSystem.Utilities.InternedString kVector2Layout = new global::UnityEngine.InputSystem.Utilities.InternedString("Vector2");
			global::UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout = new global::UnityEngine.InputSystem.Utilities.InternedString("Delta");
			global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout = new global::UnityEngine.InputSystem.Utilities.InternedString("Button");
			global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout = new global::UnityEngine.InputSystem.Utilities.InternedString("Axis");
			global::UnityEngine.InputSystem.Utilities.InternedString kDigitalLayout = new global::UnityEngine.InputSystem.Utilities.InternedString("Digital");
			global::UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout = new global::UnityEngine.InputSystem.Utilities.InternedString("Integer");
			global::UnityEngine.InputSystem.Controls.Vector2Control vector2Control = Initialize_ctrlMouseposition(kVector2Layout, this);
			global::UnityEngine.InputSystem.Controls.DeltaControl deltaControl = Initialize_ctrlMousedelta(kDeltaLayout, this);
			global::UnityEngine.InputSystem.Controls.DeltaControl deltaControl2 = Initialize_ctrlMousescroll(kDeltaLayout, this);
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = Initialize_ctrlMousepress(kButtonLayout, this);
			global::UnityEngine.InputSystem.Controls.ButtonControl control = Initialize_ctrlMouseleftButton(kButtonLayout, this);
			global::UnityEngine.InputSystem.Controls.ButtonControl control2 = Initialize_ctrlMouserightButton(kButtonLayout, this);
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl2 = Initialize_ctrlMousemiddleButton(kButtonLayout, this);
			global::UnityEngine.InputSystem.Controls.ButtonControl control3 = Initialize_ctrlMouseforwardButton(kButtonLayout, this);
			global::UnityEngine.InputSystem.Controls.ButtonControl control4 = Initialize_ctrlMousebackButton(kButtonLayout, this);
			global::UnityEngine.InputSystem.Controls.AxisControl control5 = Initialize_ctrlMousepressure(kAxisLayout, this);
			global::UnityEngine.InputSystem.Controls.Vector2Control vector2Control2 = Initialize_ctrlMouseradius(kVector2Layout, this);
			Initialize_ctrlMousepointerId(kDigitalLayout, this);
			global::UnityEngine.InputSystem.Controls.IntegerControl integerControl = Initialize_ctrlMousedisplayIndex(kIntegerLayout, this);
			global::UnityEngine.InputSystem.Controls.IntegerControl integerControl2 = Initialize_ctrlMouseclickCount(kIntegerLayout, this);
			global::UnityEngine.InputSystem.Controls.AxisControl x = Initialize_ctrlMousepositionx(kAxisLayout, vector2Control);
			global::UnityEngine.InputSystem.Controls.AxisControl y = Initialize_ctrlMousepositiony(kAxisLayout, vector2Control);
			global::UnityEngine.InputSystem.Controls.AxisControl up = Initialize_ctrlMousedeltaup(kAxisLayout, deltaControl);
			global::UnityEngine.InputSystem.Controls.AxisControl down = Initialize_ctrlMousedeltadown(kAxisLayout, deltaControl);
			global::UnityEngine.InputSystem.Controls.AxisControl left = Initialize_ctrlMousedeltaleft(kAxisLayout, deltaControl);
			global::UnityEngine.InputSystem.Controls.AxisControl right = Initialize_ctrlMousedeltaright(kAxisLayout, deltaControl);
			global::UnityEngine.InputSystem.Controls.AxisControl x2 = Initialize_ctrlMousedeltax(kAxisLayout, deltaControl);
			global::UnityEngine.InputSystem.Controls.AxisControl y2 = Initialize_ctrlMousedeltay(kAxisLayout, deltaControl);
			global::UnityEngine.InputSystem.Controls.AxisControl up2 = Initialize_ctrlMousescrollup(kAxisLayout, deltaControl2);
			global::UnityEngine.InputSystem.Controls.AxisControl down2 = Initialize_ctrlMousescrolldown(kAxisLayout, deltaControl2);
			global::UnityEngine.InputSystem.Controls.AxisControl left2 = Initialize_ctrlMousescrollleft(kAxisLayout, deltaControl2);
			global::UnityEngine.InputSystem.Controls.AxisControl right2 = Initialize_ctrlMousescrollright(kAxisLayout, deltaControl2);
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = Initialize_ctrlMousescrollx(kAxisLayout, deltaControl2);
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl2 = Initialize_ctrlMousescrolly(kAxisLayout, deltaControl2);
			global::UnityEngine.InputSystem.Controls.AxisControl x3 = Initialize_ctrlMouseradiusx(kAxisLayout, vector2Control2);
			global::UnityEngine.InputSystem.Controls.AxisControl y3 = Initialize_ctrlMouseradiusy(kAxisLayout, vector2Control2);
			deviceBuilder.WithControlUsage(0, new global::UnityEngine.InputSystem.Utilities.InternedString("Point"), vector2Control);
			deviceBuilder.WithControlUsage(1, new global::UnityEngine.InputSystem.Utilities.InternedString("Secondary2DMotion"), deltaControl);
			deviceBuilder.WithControlUsage(2, new global::UnityEngine.InputSystem.Utilities.InternedString("ScrollHorizontal"), axisControl);
			deviceBuilder.WithControlUsage(3, new global::UnityEngine.InputSystem.Utilities.InternedString("ScrollVertical"), axisControl2);
			deviceBuilder.WithControlUsage(4, new global::UnityEngine.InputSystem.Utilities.InternedString("PrimaryAction"), control);
			deviceBuilder.WithControlUsage(5, new global::UnityEngine.InputSystem.Utilities.InternedString("SecondaryAction"), control2);
			deviceBuilder.WithControlUsage(6, new global::UnityEngine.InputSystem.Utilities.InternedString("Forward"), control3);
			deviceBuilder.WithControlUsage(7, new global::UnityEngine.InputSystem.Utilities.InternedString("Back"), control4);
			deviceBuilder.WithControlUsage(8, new global::UnityEngine.InputSystem.Utilities.InternedString("Pressure"), control5);
			deviceBuilder.WithControlUsage(9, new global::UnityEngine.InputSystem.Utilities.InternedString("Radius"), vector2Control2);
			deviceBuilder.WithControlAlias(0, new global::UnityEngine.InputSystem.Utilities.InternedString("horizontal"));
			deviceBuilder.WithControlAlias(1, new global::UnityEngine.InputSystem.Utilities.InternedString("vertical"));
			base.scroll = deltaControl2;
			base.leftButton = control;
			base.middleButton = buttonControl2;
			base.rightButton = control2;
			base.backButton = control4;
			base.forwardButton = control3;
			base.clickCount = integerControl2;
			base.position = vector2Control;
			base.delta = deltaControl;
			base.radius = vector2Control2;
			base.pressure = control5;
			base.press = buttonControl;
			base.displayIndex = integerControl;
			vector2Control.x = x;
			vector2Control.y = y;
			deltaControl.up = up;
			deltaControl.down = down;
			deltaControl.left = left;
			deltaControl.right = right;
			deltaControl.x = x2;
			deltaControl.y = y2;
			deltaControl2.up = up2;
			deltaControl2.down = down2;
			deltaControl2.left = left2;
			deltaControl2.right = right2;
			deltaControl2.x = axisControl;
			deltaControl2.y = axisControl2;
			vector2Control2.x = x3;
			vector2Control2.y = y3;
			deviceBuilder.WithStateOffsetToControlIndexMap(new uint[26]
			{
				32782u, 16809999u, 33587218u, 33587219u, 33587220u, 50364432u, 50364433u, 50364437u, 67141656u, 67141657u,
				67141658u, 83918870u, 83918871u, 83918875u, 100664323u, 100664324u, 101188613u, 101712902u, 102237191u, 102761480u,
				109068300u, 117456909u, 134250505u, 167804956u, 184582173u, 201327627u
			});
			deviceBuilder.WithControlTree(new byte[371]
			{
				135, 1, 1, 0, 0, 0, 0, 196, 0, 3,
				0, 0, 0, 0, 135, 1, 23, 0, 0, 0,
				0, 128, 0, 5, 0, 0, 0, 0, 196, 0,
				11, 0, 0, 0, 0, 64, 0, 7, 0, 0,
				0, 1, 128, 0, 9, 0, 3, 0, 1, 32,
				0, 255, 255, 1, 0, 1, 64, 0, 255, 255,
				2, 0, 1, 96, 0, 255, 255, 7, 0, 3,
				128, 0, 255, 255, 4, 0, 3, 193, 0, 13,
				0, 0, 0, 0, 196, 0, 19, 0, 0, 0,
				0, 161, 0, 15, 0, 10, 0, 4, 193, 0,
				17, 0, 14, 0, 4, 145, 0, 255, 255, 18,
				0, 3, 161, 0, 255, 255, 21, 0, 3, 192,
				0, 255, 255, 0, 0, 0, 193, 0, 255, 255,
				24, 0, 2, 195, 0, 21, 0, 0, 0, 0,
				196, 0, 255, 255, 28, 0, 1, 194, 0, 255,
				255, 26, 0, 1, 195, 0, 255, 255, 27, 0,
				1, 32, 1, 25, 0, 0, 0, 0, 135, 1,
				41, 0, 0, 0, 0, 240, 0, 27, 0, 0,
				0, 0, 32, 1, 39, 0, 0, 0, 0, 224,
				0, 29, 0, 0, 0, 0, 240, 0, 255, 255,
				41, 0, 1, 210, 0, 31, 0, 39, 0, 1,
				224, 0, 255, 255, 40, 0, 1, 203, 0, 33,
				0, 0, 0, 0, 210, 0, 255, 255, 0, 0,
				0, 200, 0, 35, 0, 0, 0, 0, 203, 0,
				255, 255, 0, 0, 0, 198, 0, 37, 0, 0,
				0, 0, 200, 0, 255, 255, 0, 0, 0, 197,
				0, 255, 255, 29, 0, 1, 198, 0, 255, 255,
				0, 0, 0, 8, 1, 255, 255, 30, 0, 1,
				32, 1, 255, 255, 31, 0, 1, 128, 1, 43,
				0, 0, 0, 0, 135, 1, 47, 0, 0, 0,
				0, 80, 1, 255, 255, 32, 0, 2, 128, 1,
				45, 0, 34, 0, 2, 104, 1, 255, 255, 36,
				0, 1, 128, 1, 255, 255, 37, 0, 1, 132,
				1, 49, 0, 0, 0, 0, 135, 1, 255, 255,
				0, 0, 0, 130, 1, 51, 0, 0, 0, 0,
				132, 1, 255, 255, 0, 0, 0, 129, 1, 255,
				255, 38, 0, 1, 130, 1, 255, 255, 0, 0,
				0
			}, new ushort[42]
			{
				0, 14, 15, 1, 16, 17, 21, 18, 19, 20,
				2, 22, 23, 27, 2, 22, 23, 27, 24, 25,
				26, 24, 25, 26, 3, 4, 5, 6, 7, 8,
				9, 9, 10, 28, 10, 28, 29, 29, 11, 12,
				12, 13
			});
			deviceBuilder.Finish();
		}

		private global::UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlMouseposition(global::UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.Vector2Control vector2Control = new global::UnityEngine.InputSystem.Controls.Vector2Control();
			vector2Control.Setup().At(this, 0).WithParent(parent)
				.WithChildren(14, 2)
				.WithName("position")
				.WithDisplayName("Position")
				.WithLayout(kVector2Layout)
				.WithUsages(0, 1)
				.DontReset(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1447379762),
					byteOffset = 0u,
					bitOffset = 0u,
					sizeInBits = 64u
				})
				.Finish();
			return vector2Control;
		}

		private global::UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlMousedelta(global::UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.DeltaControl deltaControl = new global::UnityEngine.InputSystem.Controls.DeltaControl();
			deltaControl.Setup().At(this, 1).WithParent(parent)
				.WithChildren(16, 6)
				.WithName("delta")
				.WithDisplayName("Delta")
				.WithLayout(kDeltaLayout)
				.WithUsages(1, 1)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1447379762),
					byteOffset = 8u,
					bitOffset = 0u,
					sizeInBits = 64u
				})
				.Finish();
			return deltaControl;
		}

		private global::UnityEngine.InputSystem.Controls.DeltaControl Initialize_ctrlMousescroll(global::UnityEngine.InputSystem.Utilities.InternedString kDeltaLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.DeltaControl deltaControl = new global::UnityEngine.InputSystem.Controls.DeltaControl();
			deltaControl.Setup().At(this, 2).WithParent(parent)
				.WithChildren(22, 6)
				.WithName("scroll")
				.WithDisplayName("Scroll")
				.WithLayout(kDeltaLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1447379762),
					byteOffset = 16u,
					bitOffset = 0u,
					sizeInBits = 64u
				})
				.Finish();
			return deltaControl;
		}

		private global::UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMousepress(global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = new global::UnityEngine.InputSystem.Controls.ButtonControl();
			buttonControl.Setup().At(this, 3).WithParent(parent)
				.WithName("press")
				.WithDisplayName("Press")
				.WithLayout(kButtonLayout)
				.IsSynthetic(value: true)
				.IsButton(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 24u,
					bitOffset = 0u,
					sizeInBits = 1u
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		private global::UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMouseleftButton(global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = new global::UnityEngine.InputSystem.Controls.ButtonControl();
			buttonControl.Setup().At(this, 4).WithParent(parent)
				.WithName("leftButton")
				.WithDisplayName("Left Button")
				.WithShortDisplayName("LMB")
				.WithLayout(kButtonLayout)
				.WithUsages(4, 1)
				.IsButton(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 24u,
					bitOffset = 0u,
					sizeInBits = 1u
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		private global::UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMouserightButton(global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = new global::UnityEngine.InputSystem.Controls.ButtonControl();
			buttonControl.Setup().At(this, 5).WithParent(parent)
				.WithName("rightButton")
				.WithDisplayName("Right Button")
				.WithShortDisplayName("RMB")
				.WithLayout(kButtonLayout)
				.WithUsages(5, 1)
				.IsButton(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 24u,
					bitOffset = 1u,
					sizeInBits = 1u
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		private global::UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMousemiddleButton(global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = new global::UnityEngine.InputSystem.Controls.ButtonControl();
			buttonControl.Setup().At(this, 6).WithParent(parent)
				.WithName("middleButton")
				.WithDisplayName("Middle Button")
				.WithShortDisplayName("MMB")
				.WithLayout(kButtonLayout)
				.IsButton(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 24u,
					bitOffset = 2u,
					sizeInBits = 1u
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		private global::UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMouseforwardButton(global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = new global::UnityEngine.InputSystem.Controls.ButtonControl();
			buttonControl.Setup().At(this, 7).WithParent(parent)
				.WithName("forwardButton")
				.WithDisplayName("Forward")
				.WithLayout(kButtonLayout)
				.WithUsages(6, 1)
				.IsButton(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 24u,
					bitOffset = 3u,
					sizeInBits = 1u
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		private global::UnityEngine.InputSystem.Controls.ButtonControl Initialize_ctrlMousebackButton(global::UnityEngine.InputSystem.Utilities.InternedString kButtonLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.ButtonControl buttonControl = new global::UnityEngine.InputSystem.Controls.ButtonControl();
			buttonControl.Setup().At(this, 8).WithParent(parent)
				.WithName("backButton")
				.WithDisplayName("Back")
				.WithLayout(kButtonLayout)
				.WithUsages(7, 1)
				.IsButton(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 24u,
					bitOffset = 4u,
					sizeInBits = 1u
				})
				.WithMinAndMax(0, 1)
				.Finish();
			return buttonControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousepressure(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 9).WithParent(parent)
				.WithName("pressure")
				.WithDisplayName("Pressure")
				.WithLayout(kAxisLayout)
				.WithUsages(8, 1)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 32u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.WithDefaultState(1)
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.Vector2Control Initialize_ctrlMouseradius(global::UnityEngine.InputSystem.Utilities.InternedString kVector2Layout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.Vector2Control vector2Control = new global::UnityEngine.InputSystem.Controls.Vector2Control();
			vector2Control.Setup().At(this, 10).WithParent(parent)
				.WithChildren(28, 2)
				.WithName("radius")
				.WithDisplayName("Radius")
				.WithLayout(kVector2Layout)
				.WithUsages(9, 1)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1447379762),
					byteOffset = 40u,
					bitOffset = 0u,
					sizeInBits = 64u
				})
				.Finish();
			return vector2Control;
		}

		private global::UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlMousepointerId(global::UnityEngine.InputSystem.Utilities.InternedString kDigitalLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.IntegerControl integerControl = new global::UnityEngine.InputSystem.Controls.IntegerControl();
			integerControl.Setup().At(this, 11).WithParent(parent)
				.WithName("pointerId")
				.WithDisplayName("pointerId")
				.WithLayout(kDigitalLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1112101920),
					byteOffset = 48u,
					bitOffset = 0u,
					sizeInBits = 1u
				})
				.Finish();
			return integerControl;
		}

		private global::UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlMousedisplayIndex(global::UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.IntegerControl integerControl = new global::UnityEngine.InputSystem.Controls.IntegerControl();
			integerControl.Setup().At(this, 12).WithParent(parent)
				.WithName("displayIndex")
				.WithDisplayName("Display Index")
				.WithLayout(kIntegerLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1431521364),
					byteOffset = 26u,
					bitOffset = 0u,
					sizeInBits = 16u
				})
				.Finish();
			return integerControl;
		}

		private global::UnityEngine.InputSystem.Controls.IntegerControl Initialize_ctrlMouseclickCount(global::UnityEngine.InputSystem.Utilities.InternedString kIntegerLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.IntegerControl integerControl = new global::UnityEngine.InputSystem.Controls.IntegerControl();
			integerControl.Setup().At(this, 13).WithParent(parent)
				.WithName("clickCount")
				.WithDisplayName("Click Count")
				.WithLayout(kIntegerLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1431521364),
					byteOffset = 28u,
					bitOffset = 0u,
					sizeInBits = 16u
				})
				.Finish();
			return integerControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousepositionx(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 14).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Position X")
				.WithShortDisplayName("Position X")
				.WithLayout(kAxisLayout)
				.DontReset(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 0u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousepositiony(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 15).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Position Y")
				.WithShortDisplayName("Position Y")
				.WithLayout(kAxisLayout)
				.DontReset(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 4u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltaup(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMax = 3.402823E+38f
			};
			obj.Setup().At(this, 16).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Delta Up")
				.WithShortDisplayName("Delta Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 12u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltadown(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMin = -3.402823E+38f,
				invert = true
			};
			obj.Setup().At(this, 17).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Delta Down")
				.WithShortDisplayName("Delta Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 12u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltaleft(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMin = -3.402823E+38f,
				invert = true
			};
			obj.Setup().At(this, 18).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Delta Left")
				.WithShortDisplayName("Delta Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 8u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltaright(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMax = 3.402823E+38f
			};
			obj.Setup().At(this, 19).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Delta Right")
				.WithShortDisplayName("Delta Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 8u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltax(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 20).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Delta X")
				.WithShortDisplayName("Delta X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 8u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousedeltay(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 21).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Delta Y")
				.WithShortDisplayName("Delta Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 12u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollup(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMax = 3.402823E+38f
			};
			obj.Setup().At(this, 22).WithParent(parent)
				.WithName("up")
				.WithDisplayName("Scroll Up")
				.WithShortDisplayName("Scroll Up")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 20u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrolldown(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMin = -3.402823E+38f,
				invert = true
			};
			obj.Setup().At(this, 23).WithParent(parent)
				.WithName("down")
				.WithDisplayName("Scroll Down")
				.WithShortDisplayName("Scroll Down")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 20u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollleft(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMin = -3.402823E+38f,
				invert = true
			};
			obj.Setup().At(this, 24).WithParent(parent)
				.WithName("left")
				.WithDisplayName("Scroll Left")
				.WithShortDisplayName("Scroll Left")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 16u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollright(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl obj = new global::UnityEngine.InputSystem.Controls.AxisControl
			{
				clamp = global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize,
				clampMax = 3.402823E+38f
			};
			obj.Setup().At(this, 25).WithParent(parent)
				.WithName("right")
				.WithDisplayName("Scroll Right")
				.WithShortDisplayName("Scroll Right")
				.WithLayout(kAxisLayout)
				.IsSynthetic(value: true)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 16u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return obj;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrollx(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 26).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Scroll Left/Right")
				.WithShortDisplayName("Scroll Left/Right")
				.WithLayout(kAxisLayout)
				.WithUsages(2, 1)
				.WithAliases(0, 1)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 16u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMousescrolly(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 27).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Scroll Up/Down")
				.WithShortDisplayName("Scroll Wheel")
				.WithLayout(kAxisLayout)
				.WithUsages(3, 1)
				.WithAliases(1, 1)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 20u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMouseradiusx(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 28).WithParent(parent)
				.WithName("x")
				.WithDisplayName("Radius X")
				.WithShortDisplayName("Radius X")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 40u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		private global::UnityEngine.InputSystem.Controls.AxisControl Initialize_ctrlMouseradiusy(global::UnityEngine.InputSystem.Utilities.InternedString kAxisLayout, global::UnityEngine.InputSystem.InputControl parent)
		{
			global::UnityEngine.InputSystem.Controls.AxisControl axisControl = new global::UnityEngine.InputSystem.Controls.AxisControl();
			axisControl.Setup().At(this, 29).WithParent(parent)
				.WithName("y")
				.WithDisplayName("Radius Y")
				.WithShortDisplayName("Radius Y")
				.WithLayout(kAxisLayout)
				.WithStateBlock(new global::UnityEngine.InputSystem.LowLevel.InputStateBlock
				{
					format = new global::UnityEngine.InputSystem.Utilities.FourCC(1179407392),
					byteOffset = 44u,
					bitOffset = 0u,
					sizeInBits = 32u
				})
				.Finish();
			return axisControl;
		}

		protected new void OnNextUpdate()
		{
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(base.delta, global::UnityEngine.Vector2.zero, global::UnityEngine.InputSystem.LowLevel.InputState.currentUpdateType);
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(base.scroll, global::UnityEngine.Vector2.zero, global::UnityEngine.InputSystem.LowLevel.InputState.currentUpdateType);
		}

		protected new unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			if (eventPtr.type != 1398030676)
			{
				base.OnStateEvent(eventPtr);
				return;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(eventPtr);
			if (ptr->stateFormat != global::UnityEngine.InputSystem.LowLevel.MouseState.Format)
			{
				base.OnStateEvent(eventPtr);
				return;
			}
			global::UnityEngine.InputSystem.LowLevel.MouseState state = *(global::UnityEngine.InputSystem.LowLevel.MouseState*)ptr->state;
			global::UnityEngine.InputSystem.LowLevel.MouseState* ptr2 = (global::UnityEngine.InputSystem.LowLevel.MouseState*)((byte*)base.currentStatePtr + m_StateBlock.byteOffset);
			state.delta += ptr2->delta;
			state.scroll += ptr2->scroll;
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(this, ref state, global::UnityEngine.InputSystem.LowLevel.InputState.currentUpdateType, eventPtr);
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
		{
			OnNextUpdate();
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			OnStateEvent(eventPtr);
		}

		internal unsafe static bool MergeForward(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
		{
			if (currentEventPtr.type != 1398030676 || nextEventPtr.type != 1398030676)
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(currentEventPtr);
			global::UnityEngine.InputSystem.LowLevel.StateEvent* ptr2 = global::UnityEngine.InputSystem.LowLevel.StateEvent.FromUnchecked(nextEventPtr);
			if (ptr->stateFormat != global::UnityEngine.InputSystem.LowLevel.MouseState.Format || ptr2->stateFormat != global::UnityEngine.InputSystem.LowLevel.MouseState.Format)
			{
				return false;
			}
			global::UnityEngine.InputSystem.LowLevel.MouseState* state = (global::UnityEngine.InputSystem.LowLevel.MouseState*)ptr->state;
			global::UnityEngine.InputSystem.LowLevel.MouseState* state2 = (global::UnityEngine.InputSystem.LowLevel.MouseState*)ptr2->state;
			if (state->buttons != state2->buttons || state->clickCount != state2->clickCount)
			{
				return false;
			}
			state2->delta += state->delta;
			state2->scroll += state->scroll;
			return true;
		}

		bool global::UnityEngine.InputSystem.LowLevel.IEventMerger.MergeForward(global::UnityEngine.InputSystem.LowLevel.InputEventPtr currentEventPtr, global::UnityEngine.InputSystem.LowLevel.InputEventPtr nextEventPtr)
		{
			return MergeForward(currentEventPtr, nextEventPtr);
		}
	}
}
