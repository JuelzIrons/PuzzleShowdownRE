namespace Unity.Hierarchy
{
	[global::System.Serializable]
	internal sealed class HierarchyViewState
	{
		[global::System.Flags]
		public enum Content
		{
			Invalid = 0,
			ViewModelState = 2,
			SearchText = 4,
			Columns = 8,
			ScrollPosition = 0x10,
			All = 0x1E,
			Layout = 8,
			Settings = 8,
			DomainReload = 0x1E,
			EnterPlayMode = 0x16,
			ExitPlayMode = 0x16,
			Stage = 0x12
		}

		private const int SerialVersion = 1;

		private const uint FileIdentifierToken = 1751737714u;

		private const uint EndOfFileToken = 1919117433u;

		public global::Unity.Hierarchy.HierarchyViewState.Content ValidContent;

		public byte[] ViewModelState;

		public string SearchText;

		public global::Unity.Hierarchy.HierarchyViewColumnState[] Columns = global::System.Array.Empty<global::Unity.Hierarchy.HierarchyViewColumnState>();

		public float ScrollPositionX = -1f;

		public float ScrollPositionY = -1f;

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal static void BinarySerialization(global::System.IO.BinaryWriter writer, global::Unity.Hierarchy.HierarchyViewState value)
		{
			writer.Write(1751737714u);
			writer.Write(0);
			writer.Write(1);
			writer.Write(0);
			int num = (int)writer.BaseStream.Position;
			writer.Write((int)value.ValidContent);
			writer.Write(value.ViewModelState.Length);
			writer.Write(value.ViewModelState);
			writer.Write(value.SearchText ?? string.Empty);
			global::Unity.Hierarchy.HierarchyViewColumnState[] columns = value.Columns;
			writer.Write(columns.Length);
			global::Unity.Hierarchy.HierarchyViewColumnState[] array = columns;
			foreach (global::Unity.Hierarchy.HierarchyViewColumnState hierarchyViewColumnState in array)
			{
				writer.Write(hierarchyViewColumnState.ColumnId ?? string.Empty);
				writer.Write(hierarchyViewColumnState.Visible);
				writer.Write(hierarchyViewColumnState.Width);
				writer.Write(hierarchyViewColumnState.Index);
			}
			writer.Write(value.ScrollPositionX);
			writer.Write(value.ScrollPositionY);
			int num2 = (int)writer.BaseStream.Position;
			int value2 = num2 - num;
			writer.Write(1919117433u);
			writer.Seek(num - 4, global::System.IO.SeekOrigin.Begin);
			writer.Write(value2);
		}

		[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
		internal static global::Unity.Hierarchy.HierarchyViewState BinaryDeserialization(global::System.IO.BinaryReader reader)
		{
			global::Unity.Hierarchy.HierarchyViewState hierarchyViewState = new global::Unity.Hierarchy.HierarchyViewState();
			if (reader.ReadUInt32() != 1751737714 || reader.ReadInt32() != 0)
			{
				return null;
			}
			int num = reader.ReadInt32();
			if (num != 1)
			{
				return null;
			}
			int num2 = reader.ReadInt32();
			int num3 = (int)reader.BaseStream.Position;
			hierarchyViewState.ValidContent = (global::Unity.Hierarchy.HierarchyViewState.Content)reader.ReadInt32();
			hierarchyViewState.ViewModelState = reader.ReadBytes(reader.ReadInt32());
			hierarchyViewState.SearchText = reader.ReadString();
			int num4 = reader.ReadInt32();
			for (int i = 0; i < num4; i++)
			{
				global::Unity.Hierarchy.HierarchyViewColumnState hierarchyViewColumnState = new global::Unity.Hierarchy.HierarchyViewColumnState();
				hierarchyViewColumnState.ColumnId = reader.ReadString();
				hierarchyViewColumnState.Visible = reader.ReadBoolean();
				hierarchyViewColumnState.Width = reader.ReadSingle();
				hierarchyViewColumnState.Index = reader.ReadInt32();
			}
			hierarchyViewState.ScrollPositionX = reader.ReadSingle();
			hierarchyViewState.ScrollPositionY = reader.ReadSingle();
			int num5 = (int)reader.BaseStream.Position;
			int num6 = num5 - num3;
			if (num6 != num2)
			{
				return null;
			}
			if (reader.ReadUInt32() != 1919117433)
			{
				return null;
			}
			return hierarchyViewState;
		}

		public HierarchyViewState()
		{
			ValidContent = global::Unity.Hierarchy.HierarchyViewState.Content.Invalid;
		}

		public HierarchyViewState(global::Unity.Hierarchy.HierarchyViewState.Content content)
		{
			ValidContent = content;
		}

		public override string ToString()
		{
			string text = $"Content: {ValidContent}";
			if ((ValidContent & global::Unity.Hierarchy.HierarchyViewState.Content.SearchText) != global::Unity.Hierarchy.HierarchyViewState.Content.Invalid)
			{
				text = text + "Text:" + SearchText + " ";
			}
			if ((ValidContent & global::Unity.Hierarchy.HierarchyViewState.Content.ViewModelState) != global::Unity.Hierarchy.HierarchyViewState.Content.Invalid)
			{
				text += $"{ViewModelState.Length}";
			}
			if ((ValidContent & global::Unity.Hierarchy.HierarchyViewState.Content.Columns) != global::Unity.Hierarchy.HierarchyViewState.Content.Invalid)
			{
				text += $"ColsCount:{Columns.Length} ";
			}
			if ((ValidContent & global::Unity.Hierarchy.HierarchyViewState.Content.ScrollPosition) != global::Unity.Hierarchy.HierarchyViewState.Content.Invalid)
			{
				text += $"Scroll:({ScrollPositionX},{ScrollPositionY})";
			}
			return text;
		}
	}
}
