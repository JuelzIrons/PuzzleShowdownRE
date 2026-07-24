namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("Layout/Grid Layout Group", 152)]
	public class GridLayoutGroup : global::UnityEngine.UI.LayoutGroup
	{
		public enum Corner
		{
			UpperLeft = 0,
			UpperRight = 1,
			LowerLeft = 2,
			LowerRight = 3
		}

		public enum Axis
		{
			Horizontal = 0,
			Vertical = 1
		}

		public enum Constraint
		{
			Flexible = 0,
			FixedColumnCount = 1,
			FixedRowCount = 2
		}

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.GridLayoutGroup.Corner m_StartCorner;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.GridLayoutGroup.Axis m_StartAxis;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.Vector2 m_CellSize = new global::UnityEngine.Vector2(100f, 100f);

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.Vector2 m_Spacing = global::UnityEngine.Vector2.zero;

		[global::UnityEngine.SerializeField]
		protected global::UnityEngine.UI.GridLayoutGroup.Constraint m_Constraint;

		[global::UnityEngine.SerializeField]
		protected int m_ConstraintCount = 2;

		public global::UnityEngine.UI.GridLayoutGroup.Corner startCorner
		{
			get
			{
				return m_StartCorner;
			}
			set
			{
				SetProperty(ref m_StartCorner, value);
			}
		}

		public global::UnityEngine.UI.GridLayoutGroup.Axis startAxis
		{
			get
			{
				return m_StartAxis;
			}
			set
			{
				SetProperty(ref m_StartAxis, value);
			}
		}

		public global::UnityEngine.Vector2 cellSize
		{
			get
			{
				return m_CellSize;
			}
			set
			{
				SetProperty(ref m_CellSize, value);
			}
		}

		public global::UnityEngine.Vector2 spacing
		{
			get
			{
				return m_Spacing;
			}
			set
			{
				SetProperty(ref m_Spacing, value);
			}
		}

		public global::UnityEngine.UI.GridLayoutGroup.Constraint constraint
		{
			get
			{
				return m_Constraint;
			}
			set
			{
				SetProperty(ref m_Constraint, value);
			}
		}

		public int constraintCount
		{
			get
			{
				return m_ConstraintCount;
			}
			set
			{
				SetProperty(ref m_ConstraintCount, global::UnityEngine.Mathf.Max(1, value));
			}
		}

		protected GridLayoutGroup()
		{
		}

		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int num = 0;
			int num2 = 0;
			if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = (num2 = m_ConstraintCount);
			}
			else if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = (num2 = global::UnityEngine.Mathf.CeilToInt((float)base.rectChildren.Count / (float)m_ConstraintCount - 0.001f));
			}
			else
			{
				num = 1;
				num2 = global::UnityEngine.Mathf.CeilToInt(global::UnityEngine.Mathf.Sqrt(base.rectChildren.Count));
			}
			SetLayoutInputForAxis((float)base.padding.horizontal + (cellSize.x + spacing.x) * (float)num - spacing.x, (float)base.padding.horizontal + (cellSize.x + spacing.x) * (float)num2 - spacing.x, -1f, 0);
		}

		public override void CalculateLayoutInputVertical()
		{
			int num = 0;
			if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = global::UnityEngine.Mathf.CeilToInt((float)base.rectChildren.Count / (float)m_ConstraintCount - 0.001f);
			}
			else if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = m_ConstraintCount;
			}
			else
			{
				float width = base.rectTransform.rect.width;
				int num2 = global::UnityEngine.Mathf.Max(1, global::UnityEngine.Mathf.FloorToInt((width - (float)base.padding.horizontal + spacing.x + 0.001f) / (cellSize.x + spacing.x)));
				num = global::UnityEngine.Mathf.CeilToInt((float)base.rectChildren.Count / (float)num2);
			}
			float num3 = (float)base.padding.vertical + (cellSize.y + spacing.y) * (float)num - spacing.y;
			SetLayoutInputForAxis(num3, num3, -1f, 1);
		}

		public override void SetLayoutHorizontal()
		{
			SetCellsAlongAxis(0);
		}

		public override void SetLayoutVertical()
		{
			SetCellsAlongAxis(1);
		}

		private void SetCellsAlongAxis(int axis)
		{
			int count = base.rectChildren.Count;
			if (axis == 0)
			{
				for (int i = 0; i < count; i++)
				{
					global::UnityEngine.RectTransform rectTransform = base.rectChildren[i];
					m_Tracker.Add(this, rectTransform, global::UnityEngine.DrivenTransformProperties.Anchors | global::UnityEngine.DrivenTransformProperties.AnchoredPosition | global::UnityEngine.DrivenTransformProperties.SizeDelta);
					rectTransform.anchorMin = global::UnityEngine.Vector2.up;
					rectTransform.anchorMax = global::UnityEngine.Vector2.up;
					rectTransform.sizeDelta = cellSize;
				}
				return;
			}
			float x = base.rectTransform.rect.size.x;
			float y = base.rectTransform.rect.size.y;
			int num = 1;
			int num2 = 1;
			if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount)
			{
				num = m_ConstraintCount;
				if (count > num)
				{
					num2 = count / num + ((count % num > 0) ? 1 : 0);
				}
			}
			else if (m_Constraint != global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount)
			{
				num = ((!(cellSize.x + spacing.x <= 0f)) ? global::UnityEngine.Mathf.Max(1, global::UnityEngine.Mathf.FloorToInt((x - (float)base.padding.horizontal + spacing.x + 0.001f) / (cellSize.x + spacing.x))) : int.MaxValue);
				num2 = ((!(cellSize.y + spacing.y <= 0f)) ? global::UnityEngine.Mathf.Max(1, global::UnityEngine.Mathf.FloorToInt((y - (float)base.padding.vertical + spacing.y + 0.001f) / (cellSize.y + spacing.y))) : int.MaxValue);
			}
			else
			{
				num2 = m_ConstraintCount;
				if (count > num2)
				{
					num = count / num2 + ((count % num2 > 0) ? 1 : 0);
				}
			}
			int num3 = (int)startCorner % 2;
			int num4 = (int)startCorner / 2;
			int num5;
			int num6;
			int num7;
			if (startAxis == global::UnityEngine.UI.GridLayoutGroup.Axis.Horizontal)
			{
				num5 = num;
				num6 = global::UnityEngine.Mathf.Clamp(num, 1, count);
				num7 = ((m_Constraint != global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount) ? global::UnityEngine.Mathf.Clamp(num2, 1, global::UnityEngine.Mathf.CeilToInt((float)count / (float)num5)) : global::UnityEngine.Mathf.Min(num2, count));
			}
			else
			{
				num5 = num2;
				num7 = global::UnityEngine.Mathf.Clamp(num2, 1, count);
				num6 = ((m_Constraint != global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount) ? global::UnityEngine.Mathf.Clamp(num, 1, global::UnityEngine.Mathf.CeilToInt((float)count / (float)num5)) : global::UnityEngine.Mathf.Min(num, count));
			}
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2((float)num6 * cellSize.x + (float)(num6 - 1) * spacing.x, (float)num7 * cellSize.y + (float)(num7 - 1) * spacing.y);
			global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(GetStartOffset(0, vector.x), GetStartOffset(1, vector.y));
			int num8 = 0;
			if (count > m_ConstraintCount && global::UnityEngine.Mathf.CeilToInt((float)count / (float)num5) < m_ConstraintCount)
			{
				num8 = m_ConstraintCount - global::UnityEngine.Mathf.CeilToInt((float)count / (float)num5);
				num8 += global::UnityEngine.Mathf.FloorToInt((float)num8 / ((float)num5 - 1f));
				if (count % num5 == 1)
				{
					num8++;
				}
			}
			for (int j = 0; j < count; j++)
			{
				int num9;
				int num10;
				if (startAxis == global::UnityEngine.UI.GridLayoutGroup.Axis.Horizontal)
				{
					if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount && count - j <= num8)
					{
						num9 = 0;
						num10 = m_ConstraintCount - (count - j);
					}
					else
					{
						num9 = j % num5;
						num10 = j / num5;
					}
				}
				else if (m_Constraint == global::UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount && count - j <= num8)
				{
					num9 = m_ConstraintCount - (count - j);
					num10 = 0;
				}
				else
				{
					num9 = j / num5;
					num10 = j % num5;
				}
				if (num3 == 1)
				{
					num9 = num6 - 1 - num9;
				}
				if (num4 == 1)
				{
					num10 = num7 - 1 - num10;
				}
				SetChildAlongAxis(base.rectChildren[j], 0, vector2.x + (cellSize[0] + spacing[0]) * (float)num9, cellSize[0]);
				SetChildAlongAxis(base.rectChildren[j], 1, vector2.y + (cellSize[1] + spacing[1]) * (float)num10, cellSize[1]);
			}
		}
	}
}
