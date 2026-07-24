namespace UnityEngine.Rendering
{
	public class DebugUI
	{
		public class Container : global::UnityEngine.Rendering.DebugUI.Widget, global::UnityEngine.Rendering.DebugUI.IContainer
		{
			private const string k_IDToken = "#";

			internal bool hideDisplayName
			{
				get
				{
					if (!string.IsNullOrEmpty(base.displayName))
					{
						return base.displayName.StartsWith("#");
					}
					return true;
				}
			}

			public global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children { get; private set; }

			public override global::UnityEngine.Rendering.DebugUI.Panel panel
			{
				get
				{
					return m_Panel;
				}
				internal set
				{
					if (value == null || !value.flags.HasFlag(global::UnityEngine.Rendering.DebugUI.Flags.FrequentlyUsed))
					{
						m_Panel = value;
						int count = children.Count;
						for (int i = 0; i < count; i++)
						{
							children[i].panel = value;
						}
					}
				}
			}

			public Container()
				: this(string.Empty, new global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget>())
			{
			}

			public Container(string id)
				: this("#" + id, new global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget>())
			{
			}

			public Container(string displayName, global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children)
			{
				base.displayName = displayName;
				this.children = children;
				children.ItemAdded += OnItemAdded;
				children.ItemRemoved += OnItemRemoved;
				for (int i = 0; i < this.children.Count; i++)
				{
					OnItemAdded(this.children, new global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget>(i, this.children[i]));
				}
			}

			internal override void GenerateQueryPath()
			{
				base.GenerateQueryPath();
				int count = children.Count;
				for (int i = 0; i < count; i++)
				{
					children[i].GenerateQueryPath();
				}
			}

			protected virtual void OnItemAdded(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = m_Panel;
					e.item.parent = this;
				}
				if (m_Panel != null)
				{
					m_Panel.SetDirty();
				}
			}

			protected virtual void OnItemRemoved(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				if (m_Panel != null)
				{
					m_Panel.SetDirty();
				}
			}

			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + base.queryPath.GetHashCode();
				num = num * 23 + base.isHidden.GetHashCode();
				int count = children.Count;
				for (int i = 0; i < count; i++)
				{
					num = num * 23 + children[i].GetHashCode();
				}
				return num;
			}
		}

		public class Foldout : global::UnityEngine.Rendering.DebugUI.Container, global::UnityEngine.Rendering.DebugUI.IValueField
		{
			public struct ContextMenuItem
			{
				public string displayName;

				public global::System.Action action;
			}

			public bool isHeader;

			public global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Foldout.ContextMenuItem> contextMenuItems;

			private bool m_Dirty;

			private string[] m_ColumnLabels;

			private string[] m_ColumnTooltips;

			private global::System.Collections.Generic.List<global::UnityEngine.GUIContent> m_RowContents = new global::System.Collections.Generic.List<global::UnityEngine.GUIContent>();

			public bool isReadOnly => false;

			public bool opened { get; set; }

			public string documentationUrl { get; set; }

			public string[] columnLabels
			{
				get
				{
					return m_ColumnLabels;
				}
				set
				{
					m_ColumnLabels = value;
					m_Dirty = true;
				}
			}

			public string[] columnTooltips
			{
				get
				{
					return m_ColumnTooltips;
				}
				set
				{
					m_ColumnTooltips = value;
					m_Dirty = true;
				}
			}

			internal global::System.Collections.Generic.List<global::UnityEngine.GUIContent> rowContents
			{
				get
				{
					if (m_Dirty)
					{
						if (m_ColumnTooltips == null)
						{
							m_ColumnTooltips = new string[m_ColumnLabels.Length];
							global::System.Array.Fill(columnTooltips, string.Empty);
						}
						else if (m_ColumnTooltips.Length != m_ColumnLabels.Length)
						{
							throw new global::System.Exception("Dimension for labels and tooltips on Foldout - " + base.displayName + ", do not match");
						}
						m_RowContents.Clear();
						for (int i = 0; i < m_ColumnLabels.Length; i++)
						{
							string text = columnLabels[i] ?? string.Empty;
							string text2 = m_ColumnTooltips[i] ?? string.Empty;
							m_RowContents.Add(new global::UnityEngine.GUIContent(text, text2));
						}
						m_Dirty = false;
					}
					return m_RowContents;
				}
			}

			public Foldout()
			{
			}

			public Foldout(string displayName, global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children, string[] columnLabels = null, string[] columnTooltips = null)
				: base(displayName, children)
			{
				this.columnLabels = columnLabels;
				this.columnTooltips = columnTooltips;
			}

			public bool GetValue()
			{
				return opened;
			}

			object global::UnityEngine.Rendering.DebugUI.IValueField.GetValue()
			{
				return GetValue();
			}

			public void SetValue(object value)
			{
				SetValue((bool)value);
			}

			public object ValidateValue(object value)
			{
				return value;
			}

			public void SetValue(bool value)
			{
				opened = value;
			}
		}

		public class HBox : global::UnityEngine.Rendering.DebugUI.Container
		{
			public HBox()
			{
				base.displayName = "HBox";
			}
		}

		public class VBox : global::UnityEngine.Rendering.DebugUI.Container
		{
			public VBox()
			{
				base.displayName = "VBox";
			}
		}

		public class Table : global::UnityEngine.Rendering.DebugUI.Container
		{
			public class Row : global::UnityEngine.Rendering.DebugUI.Foldout
			{
				public Row()
				{
					base.displayName = "Row";
				}
			}

			private static global::UnityEngine.GUIStyle columnHeaderStyle = new global::UnityEngine.GUIStyle
			{
				alignment = global::UnityEngine.TextAnchor.MiddleCenter
			};

			public bool isReadOnly;

			private bool[] m_Header;

			public bool[] VisibleColumns
			{
				get
				{
					if (m_Header != null)
					{
						return m_Header;
					}
					int num = 0;
					if (base.children.Count != 0)
					{
						num = ((global::UnityEngine.Rendering.DebugUI.Container)base.children[0]).children.Count;
						for (int i = 1; i < base.children.Count; i++)
						{
							if (((global::UnityEngine.Rendering.DebugUI.Container)base.children[i]).children.Count != num)
							{
								global::UnityEngine.Debug.LogError("All rows must have the same number of children.");
								return null;
							}
						}
					}
					m_Header = new bool[num];
					for (int j = 0; j < num; j++)
					{
						m_Header[j] = true;
					}
					return m_Header;
				}
			}

			public Table()
			{
				base.displayName = "Array";
			}

			public void SetColumnVisibility(int index, bool visible)
			{
				bool[] visibleColumns = VisibleColumns;
				if (index >= 0 && index <= visibleColumns.Length)
				{
					visibleColumns[index] = visible;
				}
			}

			public bool GetColumnVisibility(int index)
			{
				bool[] visibleColumns = VisibleColumns;
				if (index < 0 || index > visibleColumns.Length)
				{
					return false;
				}
				return visibleColumns[index];
			}

			protected override void OnItemAdded(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget> e)
			{
				base.OnItemAdded(sender, e);
				m_Header = null;
			}

			protected override void OnItemRemoved(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget> e)
			{
				base.OnItemRemoved(sender, e);
				m_Header = null;
			}
		}

		[global::System.Flags]
		public enum Flags
		{
			None = 0,
			EditorOnly = 2,
			RuntimeOnly = 4,
			EditorForceUpdate = 8,
			FrequentlyUsed = 0x10
		}

		public abstract class Widget
		{
			public struct NameAndTooltip
			{
				public string name;

				public string tooltip;
			}

			protected global::UnityEngine.Rendering.DebugUI.Panel m_Panel;

			protected global::UnityEngine.Rendering.DebugUI.IContainer m_Parent;

			public global::System.Func<bool> isHiddenCallback;

			public int order { get; set; }

			public virtual global::UnityEngine.Rendering.DebugUI.Panel panel
			{
				get
				{
					return m_Panel;
				}
				internal set
				{
					m_Panel = value;
				}
			}

			public virtual global::UnityEngine.Rendering.DebugUI.IContainer parent
			{
				get
				{
					return m_Parent;
				}
				internal set
				{
					m_Parent = value;
				}
			}

			public global::UnityEngine.Rendering.DebugUI.Flags flags { get; set; }

			public string displayName { get; set; }

			public string tooltip { get; set; }

			public string queryPath { get; private set; }

			public bool isEditorOnly => flags.HasFlag(global::UnityEngine.Rendering.DebugUI.Flags.EditorOnly);

			public bool isRuntimeOnly => flags.HasFlag(global::UnityEngine.Rendering.DebugUI.Flags.RuntimeOnly);

			public bool isInactiveInEditor
			{
				get
				{
					if (isRuntimeOnly)
					{
						return !global::UnityEngine.Application.isPlaying;
					}
					return false;
				}
			}

			public bool isHidden => isHiddenCallback?.Invoke() ?? false;

			public global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip nameAndTooltip
			{
				set
				{
					displayName = value.name;
					tooltip = value.tooltip;
				}
			}

			internal virtual void GenerateQueryPath()
			{
				queryPath = displayName.Trim();
				if (m_Parent != null)
				{
					queryPath = m_Parent.queryPath + " -> " + queryPath;
				}
			}

			public override int GetHashCode()
			{
				return queryPath.GetHashCode() ^ isHidden.GetHashCode();
			}
		}

		public interface IContainer
		{
			global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children { get; }

			string displayName { get; set; }

			string queryPath { get; }
		}

		public interface IValueField
		{
			object GetValue();

			void SetValue(object value);

			object ValidateValue(object value);
		}

		public class Button : global::UnityEngine.Rendering.DebugUI.Widget
		{
			public global::System.Action action { get; set; }
		}

		public class Value : global::UnityEngine.Rendering.DebugUI.Widget
		{
			public float refreshRate = 0.1f;

			public string formatString;

			public global::System.Func<object> getter { get; set; }

			public Value()
			{
				base.displayName = "";
			}

			public virtual object GetValue()
			{
				return getter();
			}

			public virtual string FormatString(object value)
			{
				if (!string.IsNullOrEmpty(formatString))
				{
					return string.Format(formatString, value);
				}
				return $"{value}";
			}
		}

		public class ProgressBarValue : global::UnityEngine.Rendering.DebugUI.Value
		{
			public float min;

			public float max = 1f;

			public override string FormatString(object value)
			{
				float num = Remap(global::UnityEngine.Mathf.Clamp((float)value, min, max), min, max);
				return $"{num:P1}";
				static float Remap(float v, float x0, float y0)
				{
					return (v - x0) / (y0 - x0);
				}
			}
		}

		public class ValueTuple : global::UnityEngine.Rendering.DebugUI.Widget
		{
			public global::UnityEngine.Rendering.DebugUI.Value[] values;

			public int pinnedElementIndex = -1;

			public int numElements => values.Length;

			public float refreshRate => global::System.Linq.Enumerable.FirstOrDefault(values)?.refreshRate ?? 0.1f;
		}

		public abstract class Field<T> : global::UnityEngine.Rendering.DebugUI.Widget, global::UnityEngine.Rendering.DebugUI.IValueField
		{
			public global::System.Action<global::UnityEngine.Rendering.DebugUI.Field<T>, T> onValueChanged;

			public global::System.Func<T> getter { get; set; }

			public global::System.Action<T> setter { get; set; }

			object global::UnityEngine.Rendering.DebugUI.IValueField.ValidateValue(object value)
			{
				return ValidateValue((T)value);
			}

			public virtual T ValidateValue(T value)
			{
				return value;
			}

			object global::UnityEngine.Rendering.DebugUI.IValueField.GetValue()
			{
				return GetValue();
			}

			public T GetValue()
			{
				return getter();
			}

			public void SetValue(object value)
			{
				SetValue((T)value);
			}

			public virtual void SetValue(T value)
			{
				if (setter != null)
				{
					T val = ValidateValue(value);
					if (val == null || !val.Equals(getter()))
					{
						setter(val);
						onValueChanged?.Invoke(this, val);
					}
				}
			}
		}

		public class BoolField : global::UnityEngine.Rendering.DebugUI.Field<bool>
		{
		}

		public class HistoryBoolField : global::UnityEngine.Rendering.DebugUI.BoolField
		{
			public global::System.Func<bool>[] historyGetter { get; set; }

			public int historyDepth
			{
				get
				{
					global::System.Func<bool>[] array = historyGetter;
					if (array == null)
					{
						return 0;
					}
					return array.Length;
				}
			}

			public bool GetHistoryValue(int historyIndex)
			{
				return historyGetter[historyIndex]();
			}
		}

		public class IntField : global::UnityEngine.Rendering.DebugUI.Field<int>
		{
			public global::System.Func<int> min;

			public global::System.Func<int> max;

			public int incStep = 1;

			public int intStepMult = 10;

			public override int ValidateValue(int value)
			{
				if (min != null)
				{
					value = global::UnityEngine.Mathf.Max(value, min());
				}
				if (max != null)
				{
					value = global::UnityEngine.Mathf.Min(value, max());
				}
				return value;
			}
		}

		public class UIntField : global::UnityEngine.Rendering.DebugUI.Field<uint>
		{
			public global::System.Func<uint> min;

			public global::System.Func<uint> max;

			public uint incStep = 1u;

			public uint intStepMult = 10u;

			public override uint ValidateValue(uint value)
			{
				if (min != null)
				{
					value = (uint)global::UnityEngine.Mathf.Max((int)value, (int)min());
				}
				if (max != null)
				{
					value = (uint)global::UnityEngine.Mathf.Min((int)value, (int)max());
				}
				return value;
			}
		}

		public class FloatField : global::UnityEngine.Rendering.DebugUI.Field<float>
		{
			public global::System.Func<float> min;

			public global::System.Func<float> max;

			public float incStep = 0.1f;

			public float incStepMult = 10f;

			public int decimals = 3;

			public override float ValidateValue(float value)
			{
				if (min != null)
				{
					value = global::UnityEngine.Mathf.Max(value, min());
				}
				if (max != null)
				{
					value = global::UnityEngine.Mathf.Min(value, max());
				}
				return value;
			}
		}

		public class RenderingLayerField : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.RenderingLayerMask>, global::UnityEngine.Rendering.DebugUI.IContainer
		{
			private static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip s_RenderingLayerColors = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Layers Color",
				tooltip = "Select the display color for each Rendering Layer"
			};

			private string[] m_RenderingLayersNames = global::System.Array.Empty<string>();

			private int m_DefinedRenderingLayersCount = -1;

			private global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> m_RenderingLayersColors = new global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget>();

			private int maxRenderingLayerCount => global::UnityEngine.RenderingLayerMask.GetRenderingLayerCount();

			public string[] renderingLayersNames
			{
				get
				{
					if (m_DefinedRenderingLayersCount != global::UnityEngine.RenderingLayerMask.GetDefinedRenderingLayerCount())
					{
						Resize();
					}
					return m_RenderingLayersNames;
				}
			}

			public global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children
			{
				get
				{
					if (m_DefinedRenderingLayersCount != global::UnityEngine.RenderingLayerMask.GetDefinedRenderingLayerCount())
					{
						Resize();
					}
					return m_RenderingLayersColors;
				}
			}

			public global::System.Func<int, global::UnityEngine.Vector4> getRenderingLayerColor { get; set; }

			public global::System.Action<global::UnityEngine.Vector4, int> setRenderingLayerColor { get; set; }

			private void Resize()
			{
				m_DefinedRenderingLayersCount = global::UnityEngine.RenderingLayerMask.GetDefinedRenderingLayerCount();
				m_RenderingLayersNames = new string[maxRenderingLayerCount];
				for (int i = 0; i < maxRenderingLayerCount; i++)
				{
					string text = global::UnityEngine.RenderingLayerMask.RenderingLayerToName(i);
					if (string.IsNullOrEmpty(text))
					{
						text = $"Unused Rendering Layer {i}";
					}
					m_RenderingLayersNames[i] = text;
				}
				m_RenderingLayersColors.Clear();
				global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					nameAndTooltip = s_RenderingLayerColors,
					flags = global::UnityEngine.Rendering.DebugUI.Flags.EditorOnly,
					parent = this
				};
				m_RenderingLayersColors.Add(foldout);
				for (int j = 0; j < m_RenderingLayersNames.Length; j++)
				{
					int index = j;
					foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.ColorField
					{
						displayName = m_RenderingLayersNames[index],
						getter = () => getRenderingLayerColor(index),
						setter = delegate(global::UnityEngine.Color value)
						{
							setRenderingLayerColor(value, index);
						}
					});
				}
				GenerateQueryPath();
			}

			internal override void GenerateQueryPath()
			{
				base.GenerateQueryPath();
				int count = children.Count;
				for (int i = 0; i < count; i++)
				{
					children[i].GenerateQueryPath();
				}
			}
		}

		public abstract class EnumField<T> : global::UnityEngine.Rendering.DebugUI.Field<T>
		{
			public global::UnityEngine.GUIContent[] enumNames;

			private int[] m_EnumValues;

			private static global::System.Text.RegularExpressions.Regex s_NicifyRegEx = new global::System.Text.RegularExpressions.Regex("([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", global::System.Text.RegularExpressions.RegexOptions.Compiled);

			public int[] enumValues
			{
				get
				{
					return m_EnumValues;
				}
				set
				{
					if (global::System.Linq.Enumerable.Count(global::System.Linq.Enumerable.Distinct(value?)) != global::System.Linq.Enumerable.Count(value?))
					{
						global::UnityEngine.Debug.LogWarning(base.displayName + " - The values of the enum are duplicated, this might lead to a errors displaying the enum");
					}
					m_EnumValues = value;
				}
			}

			protected void AutoFillFromType(global::System.Type enumType)
			{
				if (enumType == null || !enumType.IsEnum)
				{
					throw new global::System.ArgumentException("enumType must not be null and it must be an Enum type");
				}
				global::System.Collections.Generic.List<global::UnityEngine.GUIContent> value;
				using (global::UnityEngine.Rendering.ListPool<global::UnityEngine.GUIContent>.Get(out value))
				{
					global::System.Collections.Generic.List<int> value2;
					using (global::UnityEngine.Rendering.ListPool<int>.Get(out value2))
					{
						foreach (global::System.Reflection.FieldInfo item2 in global::System.Linq.Enumerable.Where(enumType.GetFields(global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public), (global::System.Reflection.FieldInfo fieldInfo) => !global::System.Reflection.CustomAttributeExtensions.IsDefined(fieldInfo, typeof(global::System.ObsoleteAttribute)) && !global::System.Reflection.CustomAttributeExtensions.IsDefined(fieldInfo, typeof(global::UnityEngine.HideInInspector))))
						{
							global::UnityEngine.InspectorNameAttribute customAttribute = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.InspectorNameAttribute>(item2);
							global::UnityEngine.GUIContent item = new global::UnityEngine.GUIContent((customAttribute == null) ? s_NicifyRegEx.Replace(item2.Name, "$1 ") : customAttribute.displayName);
							value.Add(item);
							value2.Add((int)global::System.Enum.Parse(enumType, item2.Name));
						}
						enumNames = value.ToArray();
						enumValues = value2.ToArray();
					}
				}
			}
		}

		public class EnumField : global::UnityEngine.Rendering.DebugUI.EnumField<int>
		{
			internal int[] quickSeparators;

			private int[] m_Indexes;

			internal int[] indexes
			{
				get
				{
					int[] array = m_Indexes;
					if (array == null)
					{
						global::UnityEngine.GUIContent[] array2 = enumNames;
						array = (m_Indexes = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Range(0, (array2 != null) ? array2.Length : 0)));
					}
					return array;
				}
			}

			public global::System.Func<int> getIndex { get; set; }

			public global::System.Action<int> setIndex { get; set; }

			public int currentIndex
			{
				get
				{
					return getIndex();
				}
				set
				{
					setIndex(value);
				}
			}

			public global::System.Type autoEnum
			{
				set
				{
					AutoFillFromType(value);
					InitQuickSeparators();
				}
			}

			internal void InitQuickSeparators()
			{
				global::System.Collections.Generic.IEnumerable<string> source = global::System.Linq.Enumerable.Select(enumNames, delegate(global::UnityEngine.GUIContent x)
				{
					string[] array = x.text.Split('/');
					return (array.Length == 1) ? "" : array[0];
				});
				quickSeparators = new int[global::System.Linq.Enumerable.Count(global::System.Linq.Enumerable.Distinct(source))];
				string text = null;
				int num = 0;
				int num2 = 0;
				for (; num < quickSeparators.Length; num++)
				{
					string text2 = global::System.Linq.Enumerable.ElementAt(source, num2);
					while (text == text2)
					{
						text2 = global::System.Linq.Enumerable.ElementAt(source, ++num2);
					}
					text = text2;
					quickSeparators[num] = num2++;
				}
			}

			public override void SetValue(int value)
			{
				int num = ValidateValue(value);
				int num2 = global::System.Array.IndexOf(base.enumValues, num);
				if (currentIndex != num2 && !num.Equals(base.getter()))
				{
					base.setter(num);
					onValueChanged?.Invoke(this, num);
					if (num2 > -1)
					{
						currentIndex = num2;
					}
				}
			}
		}

		public class ObjectPopupField : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Object>
		{
			public global::System.Func<global::System.Collections.Generic.IEnumerable<global::UnityEngine.Object>> getObjects { get; set; }
		}

		public class CameraSelector : global::UnityEngine.Rendering.DebugUI.ObjectPopupField
		{
			private global::UnityEngine.Camera[] m_CamerasArray;

			private global::System.Collections.Generic.List<global::UnityEngine.Camera> m_Cameras = new global::System.Collections.Generic.List<global::UnityEngine.Camera>();

			private global::System.Collections.Generic.IEnumerable<global::UnityEngine.Camera> cameras
			{
				get
				{
					m_Cameras.Clear();
					if (m_CamerasArray == null || m_CamerasArray.Length != global::UnityEngine.Camera.allCamerasCount)
					{
						m_CamerasArray = new global::UnityEngine.Camera[global::UnityEngine.Camera.allCamerasCount];
					}
					global::UnityEngine.Camera.GetAllCameras(m_CamerasArray);
					global::UnityEngine.Camera[] camerasArray = m_CamerasArray;
					foreach (global::UnityEngine.Camera camera in camerasArray)
					{
						if (!(camera == null) && camera.cameraType != global::UnityEngine.CameraType.Preview && camera.cameraType != global::UnityEngine.CameraType.Reflection && camera.TryGetComponent<global::UnityEngine.Rendering.IAdditionalData>(out var _))
						{
							m_Cameras.Add(camera);
						}
					}
					return m_Cameras;
				}
			}

			public CameraSelector()
			{
				base.displayName = "Camera";
				base.getObjects = () => cameras;
			}
		}

		public class HistoryEnumField : global::UnityEngine.Rendering.DebugUI.EnumField
		{
			public global::System.Func<int>[] historyIndexGetter { get; set; }

			public int historyDepth
			{
				get
				{
					global::System.Func<int>[] array = historyIndexGetter;
					if (array == null)
					{
						return 0;
					}
					return array.Length;
				}
			}

			public int GetHistoryValue(int historyIndex)
			{
				return historyIndexGetter[historyIndex]();
			}
		}

		public class BitField : global::UnityEngine.Rendering.DebugUI.EnumField<global::System.Enum>
		{
			private global::System.Type m_EnumType;

			public global::System.Type enumType
			{
				get
				{
					return m_EnumType;
				}
				set
				{
					m_EnumType = value;
					AutoFillFromType(value);
				}
			}
		}

		public class ColorField : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Color>
		{
			public bool hdr;

			public bool showAlpha = true;

			public bool showPicker = true;

			public float incStep = 0.025f;

			public float incStepMult = 5f;

			public int decimals = 3;

			public override global::UnityEngine.Color ValidateValue(global::UnityEngine.Color value)
			{
				if (!hdr)
				{
					value.r = global::UnityEngine.Mathf.Clamp01(value.r);
					value.g = global::UnityEngine.Mathf.Clamp01(value.g);
					value.b = global::UnityEngine.Mathf.Clamp01(value.b);
					value.a = global::UnityEngine.Mathf.Clamp01(value.a);
				}
				return value;
			}
		}

		public class Vector2Field : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Vector2>
		{
			public float incStep = 0.025f;

			public float incStepMult = 10f;

			public int decimals = 3;
		}

		public class Vector3Field : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Vector3>
		{
			public float incStep = 0.025f;

			public float incStepMult = 10f;

			public int decimals = 3;
		}

		public class Vector4Field : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Vector4>
		{
			public float incStep = 0.025f;

			public float incStepMult = 10f;

			public int decimals = 3;
		}

		public class ObjectField : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Object>
		{
			public global::System.Type type = typeof(global::UnityEngine.Object);
		}

		public class ObjectListField : global::UnityEngine.Rendering.DebugUI.Field<global::UnityEngine.Object[]>
		{
			public global::System.Type type = typeof(global::UnityEngine.Object);
		}

		public class MessageBox : global::UnityEngine.Rendering.DebugUI.Widget
		{
			public enum Style
			{
				Info = 0,
				Warning = 1,
				Error = 2
			}

			public global::UnityEngine.Rendering.DebugUI.MessageBox.Style style;

			public global::System.Func<string> messageCallback;

			public string message
			{
				get
				{
					if (messageCallback != null)
					{
						return messageCallback();
					}
					return base.displayName;
				}
			}
		}

		public class RuntimeDebugShadersMessageBox : global::UnityEngine.Rendering.DebugUI.MessageBox
		{
			public RuntimeDebugShadersMessageBox()
			{
				base.displayName = "Warning: the debug shader variants are missing. Ensure that the \"Strip Runtime Debug Shaders\" option is disabled in the SRP Graphics Settings.";
				style = global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning;
				isHiddenCallback = () => !global::UnityEngine.Rendering.GraphicsSettings.TryGetRenderPipelineSettings<global::UnityEngine.Rendering.ShaderStrippingSetting>(out var settings) || !settings.stripRuntimeDebugShaders;
			}
		}

		public class Panel : global::UnityEngine.Rendering.DebugUI.IContainer, global::System.IComparable<global::UnityEngine.Rendering.DebugUI.Panel>
		{
			public global::UnityEngine.Rendering.DebugUI.Flags flags { get; set; }

			public string displayName { get; set; }

			public int groupIndex { get; set; }

			public string queryPath => displayName;

			public bool isEditorOnly => (flags & global::UnityEngine.Rendering.DebugUI.Flags.EditorOnly) != 0;

			public bool isRuntimeOnly => (flags & global::UnityEngine.Rendering.DebugUI.Flags.RuntimeOnly) != 0;

			public bool isInactiveInEditor
			{
				get
				{
					if (isRuntimeOnly)
					{
						return !global::UnityEngine.Application.isPlaying;
					}
					return false;
				}
			}

			public bool editorForceUpdate => (flags & global::UnityEngine.Rendering.DebugUI.Flags.EditorForceUpdate) != 0;

			public global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> children { get; private set; }

			public event global::System.Action<global::UnityEngine.Rendering.DebugUI.Panel> onSetDirty = delegate
			{
			};

			public Panel()
			{
				children = new global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget>(0, (global::UnityEngine.Rendering.DebugUI.Widget widget, global::UnityEngine.Rendering.DebugUI.Widget widget1) => widget.order.CompareTo(widget1.order));
				children.ItemAdded += OnItemAdded;
				children.ItemRemoved += OnItemRemoved;
			}

			protected virtual void OnItemAdded(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this;
					e.item.parent = this;
				}
				SetDirty();
			}

			protected virtual void OnItemRemoved(global::UnityEngine.Rendering.ObservableList<global::UnityEngine.Rendering.DebugUI.Widget> sender, global::UnityEngine.Rendering.ListChangedEventArgs<global::UnityEngine.Rendering.DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				SetDirty();
			}

			public void SetDirty()
			{
				int count = children.Count;
				for (int i = 0; i < count; i++)
				{
					children[i].GenerateQueryPath();
				}
				this.onSetDirty(this);
			}

			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + displayName.GetHashCode();
				int count = children.Count;
				for (int i = 0; i < count; i++)
				{
					num = num * 23 + children[i].GetHashCode();
				}
				return num;
			}

			int global::System.IComparable<global::UnityEngine.Rendering.DebugUI.Panel>.CompareTo(global::UnityEngine.Rendering.DebugUI.Panel other)
			{
				if (other != null)
				{
					return groupIndex.CompareTo(other.groupIndex);
				}
				return 1;
			}
		}

		[global::System.Obsolete("Mask field is not longer supported. Please use a BitField or implement your own Widget. #from(6000.2)")]
		public class MaskField : global::UnityEngine.Rendering.DebugUI.EnumField<uint>
		{
			public void Fill(string[] names)
			{
				global::System.Collections.Generic.List<global::UnityEngine.GUIContent> value;
				using (global::UnityEngine.Rendering.ListPool<global::UnityEngine.GUIContent>.Get(out value))
				{
					global::System.Collections.Generic.List<int> value2;
					using (global::UnityEngine.Rendering.ListPool<int>.Get(out value2))
					{
						for (int i = 0; i < names.Length; i++)
						{
							value.Add(new global::UnityEngine.GUIContent(names[i]));
							value2.Add(i);
						}
						enumNames = value.ToArray();
						base.enumValues = value2.ToArray();
					}
				}
			}

			public override void SetValue(uint value)
			{
				uint num = ValidateValue(value);
				if (!num.Equals(base.getter()))
				{
					base.setter(num);
					onValueChanged?.Invoke(this, num);
				}
			}
		}
	}
}
