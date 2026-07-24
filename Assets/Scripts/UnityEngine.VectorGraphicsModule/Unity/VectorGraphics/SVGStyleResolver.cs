namespace Unity.VectorGraphics
{
	internal class SVGStyleResolver
	{
		public struct NodeData
		{
			public global::Unity.VectorGraphics.XmlReaderIterator.Node node;

			public string name;

			public global::System.Collections.Generic.List<string> classes;

			public string id;
		}

		public class StyleLayer
		{
			public global::Unity.VectorGraphics.SVGStyleSheet styleSheet;

			public global::Unity.VectorGraphics.SVGPropertySheet attributeSheet;

			public global::Unity.VectorGraphics.SVGStyleResolver.NodeData nodeData;
		}

		private global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer> layers = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer>();

		private global::Unity.VectorGraphics.SVGStyleSheet globalStyleSheet = new global::Unity.VectorGraphics.SVGStyleSheet();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer> nodeLayers = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer>();

		public void PushNode(global::Unity.VectorGraphics.XmlReaderIterator.Node node)
		{
			global::Unity.VectorGraphics.SVGStyleResolver.NodeData nodeData = new global::Unity.VectorGraphics.SVGStyleResolver.NodeData
			{
				node = node,
				name = node.Name
			};
			string text = node["class"];
			if (text != null)
			{
				nodeData.classes = new global::System.Collections.Generic.List<string>();
				string[] array = text.Split(new char[2] { ' ', '\t' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				foreach (string text2 in array)
				{
					string text3 = text2.Trim();
					if (!string.IsNullOrEmpty(text3))
					{
						nodeData.classes.Add(text3);
					}
				}
			}
			else
			{
				nodeData.classes = new global::System.Collections.Generic.List<string>();
			}
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			foreach (string item in SortedClasses(nodeData.classes))
			{
				list.Add(item);
			}
			nodeData.classes = list;
			nodeData.id = node["id"];
			global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer styleLayer = new global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer();
			styleLayer.nodeData = nodeData;
			styleLayer.attributeSheet = node.GetAttributes();
			styleLayer.styleSheet = new global::Unity.VectorGraphics.SVGStyleSheet();
			string text4 = node["style"];
			if (text4 != null)
			{
				global::Unity.VectorGraphics.SVGPropertySheet value = global::Unity.VectorGraphics.SVGStyleSheetUtils.ParseInline(text4);
				styleLayer.styleSheet[node.Name] = value;
			}
			PushLayer(styleLayer);
		}

		public void PopNode()
		{
			PopLayer();
		}

		public void PushLayer(global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer layer)
		{
			layers.Add(layer);
		}

		public void PopLayer()
		{
			if (layers.Count == 0)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
			layers.RemoveAt(layers.Count - 1);
		}

		public global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer PeekLayer()
		{
			if (layers.Count == 0)
			{
				return null;
			}
			return layers[layers.Count - 1];
		}

		public void SaveLayerForSceneNode(global::Unity.VectorGraphics.SceneNode node)
		{
			nodeLayers[node] = PeekLayer();
		}

		public global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer GetLayerForScenNode(global::Unity.VectorGraphics.SceneNode node)
		{
			if (!nodeLayers.ContainsKey(node))
			{
				return null;
			}
			return nodeLayers[node];
		}

		public void SetGlobalStyleSheet(global::Unity.VectorGraphics.SVGStyleSheet sheet)
		{
			foreach (string selector in sheet.selectors)
			{
				globalStyleSheet[selector] = sheet[selector];
			}
		}

		public string Evaluate(string attribName, global::Unity.VectorGraphics.Inheritance inheritance = global::Unity.VectorGraphics.Inheritance.None)
		{
			for (int num = layers.Count - 1; num >= 0; num--)
			{
				string attrib = null;
				if (LookupStyleOrAttribute(layers[num], attribName, inheritance, out attrib))
				{
					return attrib;
				}
				if (inheritance == global::Unity.VectorGraphics.Inheritance.None)
				{
					break;
				}
			}
			return null;
		}

		private bool LookupStyleOrAttribute(global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer layer, string attribName, global::Unity.VectorGraphics.Inheritance inheritance, out string attrib)
		{
			if (LookupProperty(layer.nodeData, attribName, layer.styleSheet, out attrib))
			{
				return true;
			}
			if (LookupProperty(layer.nodeData, attribName, globalStyleSheet, out attrib))
			{
				return true;
			}
			if (layer.attributeSheet.ContainsKey(attribName))
			{
				attrib = layer.attributeSheet[attribName];
				return true;
			}
			return false;
		}

		private bool LookupProperty(global::Unity.VectorGraphics.SVGStyleResolver.NodeData nodeData, string attribName, global::Unity.VectorGraphics.SVGStyleSheet sheet, out string val)
		{
			string selector = (string.IsNullOrEmpty(nodeData.id) ? null : ("#" + nodeData.id));
			string selector2 = (string.IsNullOrEmpty(nodeData.name) ? null : nodeData.name);
			if (LookupPropertyInSheet(sheet, attribName, selector, out val))
			{
				return true;
			}
			foreach (string @class in nodeData.classes)
			{
				string selector3 = "." + @class;
				if (LookupPropertyInSheet(sheet, attribName, selector3, out val))
				{
					return true;
				}
			}
			if (LookupPropertyInSheet(sheet, attribName, selector2, out val))
			{
				return true;
			}
			if (LookupPropertyInSheet(sheet, attribName, "*", out val))
			{
				return true;
			}
			val = null;
			return false;
		}

		private bool LookupPropertyInSheet(global::Unity.VectorGraphics.SVGStyleSheet sheet, string attribName, string selector, out string val)
		{
			if (selector == null)
			{
				val = null;
				return false;
			}
			string text = "";
			foreach (string selector2 in sheet.selectors)
			{
				bool flag = false;
				string[] array = selector2.Split(new char[1] { ' ' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					if (text2 == selector)
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (array.Length == 1)
					{
						text = array[0];
						break;
					}
					if (array.Length > 1 && MatchesDescendants(array, array.Length - 1))
					{
						text = selector2;
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				global::Unity.VectorGraphics.SVGPropertySheet sVGPropertySheet = sheet[text];
				if (sVGPropertySheet.ContainsKey(attribName))
				{
					val = sVGPropertySheet[attribName];
					return true;
				}
			}
			val = null;
			return false;
		}

		private bool MatchesDescendants(string[] selectorParts, int partIndexToMatch, int layerIndex = -1)
		{
			if (selectorParts.Length == 0)
			{
				return false;
			}
			if (partIndexToMatch < 0)
			{
				return true;
			}
			if (layerIndex < 0)
			{
				layerIndex = layers.Count - 1;
			}
			string text = selectorParts[partIndexToMatch];
			for (int num = layerIndex; num >= 0; num--)
			{
				global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer styleLayer = layers[num];
				global::Unity.VectorGraphics.SVGStyleResolver.NodeData nodeData = styleLayer.nodeData;
				bool flag = text == nodeData.name;
				bool flag2 = text == "#" + nodeData.id;
				bool flag3 = nodeData.classes != null && nodeData.classes.Contains(text.StartsWith(".") ? text.Substring(1) : text);
				if (flag || flag2 || flag3)
				{
					return MatchesDescendants(selectorParts, partIndexToMatch - 1, num - 1);
				}
			}
			return false;
		}

		private global::System.Collections.Generic.IEnumerable<string> SortedClasses(global::System.Collections.Generic.List<string> classes)
		{
			int selectorCount = 0;
			foreach (string selector in globalStyleSheet.selectors)
			{
				_ = selector;
				int num = selectorCount + 1;
				selectorCount = num;
			}
			if (selectorCount == 0)
			{
				foreach (string @class in classes)
				{
					yield return @class;
				}
			}
			global::System.Collections.Generic.List<string> reversedSelectors = new global::System.Collections.Generic.List<string>(globalStyleSheet.selectors);
			reversedSelectors.Reverse();
			foreach (string sel in reversedSelectors)
			{
				string[] parts = sel.Split(new char[1] { ' ' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				string[] array = parts;
				foreach (string part in array)
				{
					if (part[0] == '.')
					{
						string klass = part.Substring(1);
						if (classes.Contains(klass))
						{
							yield return klass;
						}
					}
				}
			}
		}
	}
}
