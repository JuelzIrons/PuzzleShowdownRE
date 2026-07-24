namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerVector2 : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.Rendering.UI.UIFoldout valueToggle;

		public global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldX;

		public global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField fieldY;

		private global::UnityEngine.Rendering.DebugUI.Vector2Field m_Field;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerContainer m_Container;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			m_Field = CastWidget<global::UnityEngine.Rendering.DebugUI.Vector2Field>();
			m_Container = GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerContainer>();
			nameLabel.text = m_Field.displayName;
			fieldX.getter = () => m_Field.GetValue().x;
			fieldX.setter = delegate(float x)
			{
				SetValue(x, x: true);
			};
			fieldX.nextUIHandler = fieldY;
			SetupSettings(fieldX);
			fieldY.getter = () => m_Field.GetValue().y;
			fieldY.setter = delegate(float x)
			{
				SetValue(x, x: false, y: true);
			};
			fieldY.previousUIHandler = fieldX;
			SetupSettings(fieldY);
		}

		private void SetValue(float v, bool x = false, bool y = false)
		{
			global::UnityEngine.Vector2 value = m_Field.GetValue();
			if (x)
			{
				value.x = v;
			}
			if (y)
			{
				value.y = v;
			}
			m_Field.SetValue(value);
		}

		private void SetupSettings(global::UnityEngine.Rendering.UI.DebugUIHandlerIndirectFloatField field)
		{
			field.parentUIHandler = this;
			field.incStepGetter = () => m_Field.incStep;
			field.incStepMultGetter = () => m_Field.incStepMult;
			field.decimalsGetter = () => m_Field.decimals;
			field.Init();
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			if (fromNext || !valueToggle.isOn)
			{
				nameLabel.color = colorSelected;
			}
			else if (valueToggle.isOn)
			{
				if (m_Container.IsDirectChild(previous))
				{
					nameLabel.color = colorSelected;
				}
				else
				{
					global::UnityEngine.Rendering.UI.DebugUIHandlerWidget lastItem = m_Container.GetLastItem();
					global::UnityEngine.Rendering.DebugManager.instance.ChangeSelection(lastItem, fromNext: false);
				}
			}
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
		}

		public override void OnIncrement(bool fast)
		{
			valueToggle.isOn = true;
		}

		public override void OnDecrement(bool fast)
		{
			valueToggle.isOn = false;
		}

		public override void OnAction()
		{
			valueToggle.isOn = !valueToggle.isOn;
		}

		public override global::UnityEngine.Rendering.UI.DebugUIHandlerWidget Next()
		{
			if (!valueToggle.isOn || m_Container == null)
			{
				return base.Next();
			}
			global::UnityEngine.Rendering.UI.DebugUIHandlerWidget firstItem = m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}
	}
}
