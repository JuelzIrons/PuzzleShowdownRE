namespace Unity.VectorGraphics
{
	internal class SVGDocument
	{
		private enum ViewBoxAlign
		{
			Min = 0,
			Mid = 1,
			Max = 2
		}

		private enum ViewBoxAspectRatio
		{
			DontPreserve = 0,
			FitLargestDim = 1,
			FitSmallestDim = 2
		}

		private struct ViewBoxInfo
		{
			public global::UnityEngine.Rect ViewBox;

			public global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio AspectRatio;

			public global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign AlignX;

			public global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign AlignY;

			public bool IsEmpty;
		}

		private struct HierarchyUpdate
		{
			public global::Unity.VectorGraphics.SceneNode Parent;

			public global::Unity.VectorGraphics.SceneNode NewNode;

			public global::Unity.VectorGraphics.SceneNode ReplaceNode;
		}

		private delegate void ElemHandler();

		private class Handlers : global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SVGDocument.ElemHandler>
		{
			public Handlers(int capacity)
				: base(capacity)
			{
			}
		}

		private enum DimType
		{
			Width = 0,
			Height = 1,
			Length = 2
		}

		private struct NodeGlobalSceneState
		{
			public global::UnityEngine.Vector2 ContainerSize;
		}

		private class GradientExData
		{
			public bool WorldRelative;

			public global::Unity.VectorGraphics.Matrix2D FillTransform;
		}

		private class LinearGradientExData : global::Unity.VectorGraphics.SVGDocument.GradientExData
		{
			public string X1;

			public string Y1;

			public string X2;

			public string Y2;
		}

		private class RadialGradientExData : global::Unity.VectorGraphics.SVGDocument.GradientExData
		{
			public bool Parsed;

			public string Cx;

			public string Cy;

			public string Fx;

			public string Fy;

			public string R;
		}

		private struct ClipData
		{
			public bool WorldRelative;
		}

		private struct PatternData
		{
			public bool WorldRelative;

			public bool ContentWorldRelative;

			public global::Unity.VectorGraphics.Matrix2D PatternTransform;
		}

		private struct MaskData
		{
			public bool WorldRelative;

			public bool ContentWorldRelative;
		}

		private struct NodeWithParent
		{
			public global::Unity.VectorGraphics.SceneNode node;

			public global::Unity.VectorGraphics.SceneNode parent;
		}

		private struct NodeReferenceData
		{
			public global::Unity.VectorGraphics.SceneNode node;

			public global::UnityEngine.Rect viewport;

			public string id;
		}

		private struct PostponedStopData
		{
			public global::Unity.VectorGraphics.GradientFill fill;
		}

		private struct PostponedClip
		{
			public global::Unity.VectorGraphics.SceneNode node;
		}

		internal const float SVGLengthFactor = 1.4142135f;

		private static char[] whiteSpaceNumberChars = " \r\n\t,".ToCharArray();

		private global::Unity.VectorGraphics.XmlReaderIterator docReader;

		private global::Unity.VectorGraphics.Scene scene;

		private float dpiScale;

		private int windowWidth;

		private int windowHeight;

		private global::UnityEngine.Vector2 scenePos;

		private global::UnityEngine.Vector2 sceneSize;

		private global::Unity.VectorGraphics.SVGDictionary svgObjects = new global::Unity.VectorGraphics.SVGDictionary();

		private global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SVGDocument.Handlers> subTags = new global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SVGDocument.Handlers>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.GradientFill, global::Unity.VectorGraphics.SVGDocument.GradientExData> gradientExInfo = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.GradientFill, global::Unity.VectorGraphics.SVGDocument.GradientExData>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo> symbolViewBoxes = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.NodeGlobalSceneState> nodeGlobalSceneState = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.NodeGlobalSceneState>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, float> nodeOpacity = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, float>();

		private global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SceneNode> nodeIDs = new global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SceneNode>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer> nodeStyleLayers = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.ClipData> clipData = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.ClipData>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.PatternData> patternData = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.PatternData>();

		private global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.MaskData> maskData = new global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, global::Unity.VectorGraphics.SVGDocument.MaskData>();

		private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.NodeReferenceData>> postponedSymbolData = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.NodeReferenceData>>();

		private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedStopData>> postponedStopData = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedStopData>>();

		private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedClip>> postponedClip = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedClip>>();

		private global::Unity.VectorGraphics.SVGPostponedFills postponedFills = new global::Unity.VectorGraphics.SVGPostponedFills();

		private global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.NodeWithParent> invisibleNodes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.NodeWithParent>();

		private global::System.Collections.Generic.Stack<global::UnityEngine.Vector2> currentContainerSize = new global::System.Collections.Generic.Stack<global::UnityEngine.Vector2>();

		private global::System.Collections.Generic.Stack<global::UnityEngine.Vector2> currentViewBoxSize = new global::System.Collections.Generic.Stack<global::UnityEngine.Vector2>();

		private global::System.Collections.Generic.Stack<global::Unity.VectorGraphics.SceneNode> currentSceneNode = new global::System.Collections.Generic.Stack<global::Unity.VectorGraphics.SceneNode>();

		private global::Unity.VectorGraphics.GradientFill currentGradientFill;

		private string currentGradientId;

		private string currentGradientLink;

		private global::Unity.VectorGraphics.SVGDocument.ElemHandler[] allElems;

		private global::System.Collections.Generic.HashSet<global::Unity.VectorGraphics.SVGDocument.ElemHandler> elemsToAddToHierarchy;

		private global::Unity.VectorGraphics.SVGStyleResolver styles = new global::Unity.VectorGraphics.SVGStyleResolver();

		private bool applyRootViewBox;

		internal global::UnityEngine.Rect sceneViewport;

		public global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, float> NodeOpacities => nodeOpacity;

		public global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SceneNode> NodeIDs => nodeIDs;

		internal static string StockBlackNonZeroFillName => "unity_internal_black_nz";

		internal static string StockBlackOddEvenFillName => "unity_internal_black_oe";

		public SVGDocument(global::System.Xml.XmlReader docReader, float dpi, global::Unity.VectorGraphics.Scene scene, int windowWidth, int windowHeight, bool applyRootViewBox)
		{
			allElems = new global::Unity.VectorGraphics.SVGDocument.ElemHandler[18]
			{
				circle, defs, ellipse, g, image, line, linearGradient, path, polygon, polyline,
				radialGradient, clipPath, pattern, mask, rect, symbol, use, style
			};
			elemsToAddToHierarchy = new global::System.Collections.Generic.HashSet<global::Unity.VectorGraphics.SVGDocument.ElemHandler>(new global::Unity.VectorGraphics.SVGDocument.ElemHandler[11]
			{
				circle, ellipse, g, image, line, path, polygon, polyline, rect, svg,
				use
			});
			this.docReader = new global::Unity.VectorGraphics.XmlReaderIterator(docReader);
			this.scene = scene;
			dpiScale = dpi / 90f;
			this.windowWidth = windowWidth;
			this.windowHeight = windowHeight;
			this.applyRootViewBox = applyRootViewBox;
			svgObjects[StockBlackNonZeroFillName] = new global::Unity.VectorGraphics.SolidFill
			{
				Color = new global::UnityEngine.Color(0f, 0f, 0f),
				Mode = global::Unity.VectorGraphics.FillMode.NonZero
			};
			svgObjects[StockBlackOddEvenFillName] = new global::Unity.VectorGraphics.SolidFill
			{
				Color = new global::UnityEngine.Color(0f, 0f, 0f),
				Mode = global::Unity.VectorGraphics.FillMode.OddEven
			};
		}

		public void Import()
		{
			if (scene == null)
			{
				throw new global::System.ArgumentNullException();
			}
			if (!docReader.GoToRoot("svg"))
			{
				throw new global::Unity.VectorGraphics.SVGFormatException("Document doesn't have 'svg' root");
			}
			currentContainerSize.Push(new global::UnityEngine.Vector2(windowWidth, windowHeight));
			svg();
			currentContainerSize.Pop();
			if (currentContainerSize.Count > 0)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
			PostProcess(scene.Root);
			RemoveInvisibleNodes();
		}

		private void ParseChildren(global::Unity.VectorGraphics.XmlReaderIterator.Node node, string nodeName)
		{
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			global::Unity.VectorGraphics.SVGDocument.Handlers handlers = subTags[nodeName];
			while (docReader.GoToNextChild(node))
			{
				global::Unity.VectorGraphics.XmlReaderIterator.Node node2 = docReader.VisitCurrent();
				if (!handlers.TryGetValue(node2.Name, out var value))
				{
					docReader.SkipCurrentChildTree(node2);
					continue;
				}
				bool flag = elemsToAddToHierarchy.Contains(value);
				global::Unity.VectorGraphics.SceneNode sceneNode2 = null;
				if (flag)
				{
					if (sceneNode.Children == null)
					{
						sceneNode.Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode>();
					}
					sceneNode2 = new global::Unity.VectorGraphics.SceneNode();
					nodeGlobalSceneState[sceneNode2] = new global::Unity.VectorGraphics.SVGDocument.NodeGlobalSceneState
					{
						ContainerSize = currentContainerSize.Peek()
					};
					sceneNode.Children.Add(sceneNode2);
					currentSceneNode.Push(sceneNode2);
				}
				styles.PushNode(node2);
				if (sceneNode2 != null)
				{
					styles.SaveLayerForSceneNode(sceneNode2);
					if (styles.Evaluate("display") == "none")
					{
						invisibleNodes.Add(new global::Unity.VectorGraphics.SVGDocument.NodeWithParent
						{
							node = sceneNode2,
							parent = sceneNode
						});
					}
				}
				value();
				ParseChildren(node2, node2.Name);
				styles.PopNode();
				if (!flag || currentSceneNode.Pop() == sceneNode2)
				{
					continue;
				}
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
		}

		private void circle()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(node, svgObjects, postponedFills, styles);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			float x = AttribLengthVal(node, "cx", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float y = AttribLengthVal(node, "cy", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			float radius = AttribLengthVal(node, "r", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
			global::Unity.VectorGraphics.VectorUtils.MakeCircleShape(shape, new global::UnityEngine.Vector2(x, y), radius);
			shape.PathProps = new global::Unity.VectorGraphics.PathProperties
			{
				Stroke = stroke,
				Head = strokeEnding,
				Tail = strokeEnding,
				Corners = strokeCorner
			};
			shape.Fill = fill;
			sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
			sceneNode.Shapes.Add(shape);
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void defs()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode();
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
			currentSceneNode.Push(sceneNode);
			ParseChildren(node, node.Name);
			if (currentSceneNode.Pop() != sceneNode)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
		}

		private void ellipse()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(node, svgObjects, postponedFills, styles);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			float x = AttribLengthVal(node, "cx", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float y = AttribLengthVal(node, "cy", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			float radiusX = AttribLengthVal(node, "rx", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			float radiusY = AttribLengthVal(node, "ry", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
			global::Unity.VectorGraphics.VectorUtils.MakeEllipseShape(shape, new global::UnityEngine.Vector2(x, y), radiusX, radiusY);
			shape.PathProps = new global::Unity.VectorGraphics.PathProperties
			{
				Stroke = stroke,
				Corners = strokeCorner,
				Head = strokeEnding,
				Tail = strokeEnding
			};
			shape.Fill = fill;
			sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
			sceneNode.Shapes.Add(shape);
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void g()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
		}

		private void image()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			string text = node["xlink:href"];
			if (text != null)
			{
				global::Unity.VectorGraphics.TextureFill textureFill = new global::Unity.VectorGraphics.TextureFill();
				textureFill.Mode = global::Unity.VectorGraphics.FillMode.NonZero;
				textureFill.Addressing = global::Unity.VectorGraphics.AddressMode.Clamp;
				string text2 = text.ToLower();
				if (text2.StartsWith("data:"))
				{
					textureFill.Texture = DecodeTextureData(text);
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("Unsupported URL scheme for <image>: " + text);
				}
				if (textureFill.Texture != null)
				{
					ParseID(node, sceneNode);
					ParseOpacity(sceneNode);
					sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
					global::UnityEngine.Rect rect = ParseViewport(node, sceneNode, currentContainerSize.Peek());
					sceneNode.Transform *= global::Unity.VectorGraphics.Matrix2D.Translate(rect.position);
					global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo viewBoxInfo = new global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo
					{
						ViewBox = new global::UnityEngine.Rect(0f, 0f, textureFill.Texture.width, textureFill.Texture.height)
					};
					ParseViewBoxAspectRatio(node, ref viewBoxInfo);
					ApplyViewBox(sceneNode, viewBoxInfo, rect);
					global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
					global::Unity.VectorGraphics.VectorUtils.MakeRectangleShape(shape, new global::UnityEngine.Rect(0f, 0f, textureFill.Texture.width, textureFill.Texture.height));
					shape.Fill = textureFill;
					sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
					sceneNode.Shapes.Add(shape);
					ParseClipAndMask(node, sceneNode);
				}
			}
			string text3 = node["id"];
			if (!string.IsNullOrEmpty(text3) && postponedSymbolData.TryGetValue(text3, out var value))
			{
				foreach (global::Unity.VectorGraphics.SVGDocument.NodeReferenceData item in value)
				{
					ResolveReferencedNode(sceneNode, item, isDeferred: true);
				}
			}
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void line()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			float x = AttribLengthVal(node, "x1", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float y = AttribLengthVal(node, "y1", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			float x2 = AttribLengthVal(node, "x2", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float y2 = AttribLengthVal(node, "y2", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
			shape.PathProps = new global::Unity.VectorGraphics.PathProperties
			{
				Stroke = stroke,
				Head = strokeEnding,
				Tail = strokeEnding
			};
			shape.Contours = new global::Unity.VectorGraphics.BezierContour[1]
			{
				new global::Unity.VectorGraphics.BezierContour
				{
					Segments = global::Unity.VectorGraphics.VectorUtils.BezierSegmentToPath(global::Unity.VectorGraphics.VectorUtils.MakeLine(new global::UnityEngine.Vector2(x, y), new global::UnityEngine.Vector2(x2, y2)))
				}
			};
			sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
			sceneNode.Shapes.Add(shape);
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void linearGradient()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			string text = node["xlink:href"];
			global::Unity.VectorGraphics.GradientFill gradientFill = global::Unity.VectorGraphics.SVGAttribParser.ParseRelativeRef(text, svgObjects) as global::Unity.VectorGraphics.GradientFill;
			bool worldRelative = ((gradientFill != null) ? (gradientExInfo[gradientFill] as global::Unity.VectorGraphics.SVGDocument.LinearGradientExData) : null)?.WorldRelative ?? false;
			switch (node["gradientUnits"])
			{
			case "objectBoundingBox":
				worldRelative = false;
				break;
			case "userSpaceOnUse":
				worldRelative = true;
				break;
			default:
				throw node.GetUnsupportedAttribValException("gradientUnits");
			case null:
				break;
			}
			global::Unity.VectorGraphics.AddressMode addressing = gradientFill?.Addressing ?? global::Unity.VectorGraphics.AddressMode.Clamp;
			switch (node["spreadMethod"])
			{
			case "pad":
				addressing = global::Unity.VectorGraphics.AddressMode.Clamp;
				break;
			case "reflect":
				addressing = global::Unity.VectorGraphics.AddressMode.Mirror;
				break;
			case "repeat":
				addressing = global::Unity.VectorGraphics.AddressMode.Wrap;
				break;
			default:
				throw node.GetUnsupportedAttribValException("spreadMethod");
			case null:
				break;
			}
			global::Unity.VectorGraphics.Matrix2D fillTransform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node, "gradientTransform");
			global::Unity.VectorGraphics.GradientFill gradientFill2 = CloneGradientFill(gradientFill);
			if (gradientFill2 == null)
			{
				gradientFill2 = new global::Unity.VectorGraphics.GradientFill
				{
					Addressing = addressing,
					Type = global::Unity.VectorGraphics.GradientFillType.Linear
				};
			}
			gradientFill2.Type = global::Unity.VectorGraphics.GradientFillType.Linear;
			global::Unity.VectorGraphics.SVGDocument.LinearGradientExData linearGradientExData = new global::Unity.VectorGraphics.SVGDocument.LinearGradientExData
			{
				WorldRelative = worldRelative,
				FillTransform = fillTransform
			};
			gradientExInfo[gradientFill2] = linearGradientExData;
			currentContainerSize.Push(global::UnityEngine.Vector2.one);
			linearGradientExData.X1 = node["x1"];
			linearGradientExData.Y1 = node["y1"];
			linearGradientExData.X2 = node["x2"];
			linearGradientExData.Y2 = node["y2"];
			AttribLengthVal(linearGradientExData.X1, node, "x1", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			AttribLengthVal(linearGradientExData.Y1, node, "y1", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			AttribLengthVal(linearGradientExData.X2, node, "x2", 1f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			AttribLengthVal(linearGradientExData.Y2, node, "y2", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			currentContainerSize.Pop();
			currentGradientFill = gradientFill2;
			currentGradientId = node["id"];
			currentGradientLink = global::Unity.VectorGraphics.SVGAttribParser.CleanIri(text);
			if (!string.IsNullOrEmpty(text) && !svgObjects.ContainsKey(text))
			{
				if (!postponedStopData.ContainsKey(currentGradientLink))
				{
					postponedStopData.Add(currentGradientLink, new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedStopData>());
				}
				postponedStopData[currentGradientLink].Add(new global::Unity.VectorGraphics.SVGDocument.PostponedStopData
				{
					fill = gradientFill2
				});
			}
			AddToSVGDictionaryIfPossible(node, gradientFill2);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, stop);
			}
		}

		private void path()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(node, svgObjects, postponedFills, styles);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			global::Unity.VectorGraphics.PathProperties pathProps = new global::Unity.VectorGraphics.PathProperties
			{
				Stroke = stroke,
				Corners = strokeCorner,
				Head = strokeEnding,
				Tail = strokeEnding
			};
			global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierContour> list = global::Unity.VectorGraphics.SVGAttribParser.ParsePath(node);
			if (list != null && list.Count > 0)
			{
				sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
				sceneNode.Shapes.Add(new global::Unity.VectorGraphics.Shape
				{
					Contours = list.ToArray(),
					Fill = fill,
					PathProps = pathProps
				});
				AddToSVGDictionaryIfPossible(node, sceneNode);
			}
			ParseClipAndMask(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void polygon()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(node, svgObjects, postponedFills, styles);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			string[] array = node["points"]?.Split(whiteSpaceNumberChars, global::System.StringSplitOptions.RemoveEmptyEntries);
			if (array != null)
			{
				if ((array.Length & 1) == 1)
				{
					throw node.GetException("polygon 'points' must specify x,y for each coordinate");
				}
				if (array.Length < 4)
				{
					throw node.GetException("polygon 'points' do not even specify one triangle");
				}
				global::Unity.VectorGraphics.PathProperties pathProps = new global::Unity.VectorGraphics.PathProperties
				{
					Stroke = stroke,
					Corners = strokeCorner,
					Head = strokeEnding,
					Tail = strokeEnding
				};
				global::Unity.VectorGraphics.BezierContour bezierContour = new global::Unity.VectorGraphics.BezierContour
				{
					Closed = true
				};
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(AttribLengthVal(array[0], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(array[1], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
				int num = array.Length / 2;
				global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierPathSegment> list = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierPathSegment>(num);
				for (int i = 1; i < num; i++)
				{
					global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(AttribLengthVal(array[i * 2], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(array[i * 2 + 1], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
					if (!(vector2 == vector))
					{
						global::Unity.VectorGraphics.BezierSegment bezierSegment = global::Unity.VectorGraphics.VectorUtils.MakeLine(vector, vector2);
						list.Add(new global::Unity.VectorGraphics.BezierPathSegment
						{
							P0 = bezierSegment.P0,
							P1 = bezierSegment.P1,
							P2 = bezierSegment.P2
						});
						vector = vector2;
					}
				}
				if (list.Count > 0)
				{
					global::Unity.VectorGraphics.BezierSegment bezierSegment2 = global::Unity.VectorGraphics.VectorUtils.MakeLine(vector, list[0].P0);
					list.Add(new global::Unity.VectorGraphics.BezierPathSegment
					{
						P0 = bezierSegment2.P0,
						P1 = bezierSegment2.P1,
						P2 = bezierSegment2.P2
					});
					bezierContour.Segments = list.ToArray();
					global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
					shape.Contours = new global::Unity.VectorGraphics.BezierContour[1] { bezierContour };
					shape.PathProps = pathProps;
					shape.Fill = fill;
					global::Unity.VectorGraphics.Shape item = shape;
					sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
					sceneNode.Shapes.Add(item);
				}
			}
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void polyline()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(node, svgObjects, postponedFills, styles);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			string[] array = node["points"]?.Split(whiteSpaceNumberChars, global::System.StringSplitOptions.RemoveEmptyEntries);
			if (array != null)
			{
				if ((array.Length & 1) == 1)
				{
					throw node.GetException("polyline 'points' must specify x,y for each coordinate");
				}
				if (array.Length < 4)
				{
					throw node.GetException("polyline 'points' do not even specify one line");
				}
				global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape
				{
					Fill = fill
				};
				shape.PathProps = new global::Unity.VectorGraphics.PathProperties
				{
					Stroke = stroke,
					Corners = strokeCorner,
					Head = strokeEnding,
					Tail = strokeEnding
				};
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(AttribLengthVal(array[0], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(array[1], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
				int num = array.Length / 2;
				global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierPathSegment> list = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.BezierPathSegment>(num);
				for (int i = 1; i < num; i++)
				{
					global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(AttribLengthVal(array[i * 2], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(array[i * 2 + 1], node, "points", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
					if (!(vector2 == vector))
					{
						global::Unity.VectorGraphics.BezierSegment bezierSegment = global::Unity.VectorGraphics.VectorUtils.MakeLine(vector, vector2);
						list.Add(new global::Unity.VectorGraphics.BezierPathSegment
						{
							P0 = bezierSegment.P0,
							P1 = bezierSegment.P1,
							P2 = bezierSegment.P2
						});
						vector = vector2;
					}
				}
				if (list.Count > 0)
				{
					global::Unity.VectorGraphics.BezierSegment bezierSegment2 = global::Unity.VectorGraphics.VectorUtils.MakeLine(vector, list[0].P0);
					list.Add(new global::Unity.VectorGraphics.BezierPathSegment
					{
						P0 = bezierSegment2.P0,
						P1 = bezierSegment2.P1,
						P2 = bezierSegment2.P2
					});
					shape.Contours = new global::Unity.VectorGraphics.BezierContour[1]
					{
						new global::Unity.VectorGraphics.BezierContour
						{
							Segments = list.ToArray()
						}
					};
					sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
					sceneNode.Shapes.Add(shape);
				}
			}
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void radialGradient()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			string text = node["xlink:href"];
			global::Unity.VectorGraphics.GradientFill gradientFill = global::Unity.VectorGraphics.SVGAttribParser.ParseRelativeRef(text, svgObjects) as global::Unity.VectorGraphics.GradientFill;
			bool worldRelative = ((gradientFill != null) ? (gradientExInfo[gradientFill] as global::Unity.VectorGraphics.SVGDocument.RadialGradientExData) : null)?.WorldRelative ?? false;
			switch (node["gradientUnits"])
			{
			case "objectBoundingBox":
				worldRelative = false;
				break;
			case "userSpaceOnUse":
				worldRelative = true;
				break;
			default:
				throw node.GetUnsupportedAttribValException("gradientUnits");
			case null:
				break;
			}
			global::Unity.VectorGraphics.AddressMode addressing = gradientFill?.Addressing ?? global::Unity.VectorGraphics.AddressMode.Clamp;
			switch (node["spreadMethod"])
			{
			case "pad":
				addressing = global::Unity.VectorGraphics.AddressMode.Clamp;
				break;
			case "reflect":
				addressing = global::Unity.VectorGraphics.AddressMode.Mirror;
				break;
			case "repeat":
				addressing = global::Unity.VectorGraphics.AddressMode.Wrap;
				break;
			default:
				throw node.GetUnsupportedAttribValException("spreadMethod");
			case null:
				break;
			}
			global::Unity.VectorGraphics.Matrix2D fillTransform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node, "gradientTransform");
			global::Unity.VectorGraphics.GradientFill gradientFill2 = CloneGradientFill(gradientFill);
			if (gradientFill2 == null)
			{
				gradientFill2 = new global::Unity.VectorGraphics.GradientFill
				{
					Addressing = addressing,
					Type = global::Unity.VectorGraphics.GradientFillType.Radial
				};
			}
			gradientFill2.Type = global::Unity.VectorGraphics.GradientFillType.Radial;
			global::Unity.VectorGraphics.SVGDocument.RadialGradientExData radialGradientExData = new global::Unity.VectorGraphics.SVGDocument.RadialGradientExData
			{
				WorldRelative = worldRelative,
				FillTransform = fillTransform
			};
			gradientExInfo[gradientFill2] = radialGradientExData;
			currentContainerSize.Push(global::UnityEngine.Vector2.one);
			radialGradientExData.Cx = node["cx"];
			radialGradientExData.Cy = node["cy"];
			radialGradientExData.Fx = node["fx"];
			radialGradientExData.Fy = node["fy"];
			radialGradientExData.R = node["r"];
			AttribLengthVal(radialGradientExData.Cx, node, "cx", 0.5f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			AttribLengthVal(radialGradientExData.Cy, node, "cy", 0.5f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			AttribLengthVal(radialGradientExData.Fx, node, "fx", 0.5f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			AttribLengthVal(radialGradientExData.Fy, node, "fy", 0.5f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			AttribLengthVal(radialGradientExData.R, node, "r", 0.5f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			currentContainerSize.Pop();
			currentGradientFill = gradientFill2;
			currentGradientId = node["id"];
			currentGradientLink = global::Unity.VectorGraphics.SVGAttribParser.CleanIri(text);
			if (!string.IsNullOrEmpty(text) && !svgObjects.ContainsKey(text))
			{
				if (!postponedStopData.ContainsKey(currentGradientLink))
				{
					postponedStopData.Add(currentGradientLink, new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedStopData>());
				}
				postponedStopData[currentGradientLink].Add(new global::Unity.VectorGraphics.SVGDocument.PostponedStopData
				{
					fill = gradientFill2
				});
			}
			AddToSVGDictionaryIfPossible(node, gradientFill2);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, stop);
			}
		}

		private void clipPath()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			string text = node["id"];
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node)
			};
			bool worldRelative;
			switch (node["clipPathUnits"])
			{
			case null:
			case "userSpaceOnUse":
				worldRelative = true;
				break;
			case "objectBoundingBox":
				worldRelative = false;
				break;
			default:
				throw node.GetUnsupportedAttribValException("clipPathUnits");
			}
			clipData[sceneNode] = new global::Unity.VectorGraphics.SVGDocument.ClipData
			{
				WorldRelative = worldRelative
			};
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
			currentSceneNode.Push(sceneNode);
			ParseChildren(node, node.Name);
			if (currentSceneNode.Pop() != sceneNode)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
			if (string.IsNullOrEmpty(text) || !postponedClip.TryGetValue(text, out var value))
			{
				return;
			}
			foreach (global::Unity.VectorGraphics.SVGDocument.PostponedClip item in value)
			{
				ApplyClipper(sceneNode, item.node, worldRelative);
			}
		}

		private void pattern()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = global::Unity.VectorGraphics.Matrix2D.identity
			};
			bool flag = false;
			switch (node["patternUnits"])
			{
			case null:
			case "objectBoundingBox":
				flag = false;
				break;
			case "userSpaceOnUse":
				flag = true;
				break;
			default:
				throw node.GetUnsupportedAttribValException("patternUnits");
			}
			bool flag2 = true;
			switch (node["patternContentUnits"])
			{
			case null:
			case "userSpaceOnUse":
				flag2 = true;
				break;
			case "objectBoundingBox":
				flag2 = false;
				break;
			default:
				throw node.GetUnsupportedAttribValException("patternContentUnits");
			}
			float x = AttribLengthVal(node["x"], node, "x", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float y = AttribLengthVal(node["y"], node, "y", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			float width = AttribLengthVal(node["width"], node, "width", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float height = AttribLengthVal(node["height"], node, "height", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			global::Unity.VectorGraphics.Matrix2D patternTransform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node, "patternTransform");
			patternData[sceneNode] = new global::Unity.VectorGraphics.SVGDocument.PatternData
			{
				WorldRelative = flag,
				ContentWorldRelative = flag2,
				PatternTransform = patternTransform
			};
			global::Unity.VectorGraphics.PatternFill vectorElement = new global::Unity.VectorGraphics.PatternFill
			{
				Pattern = sceneNode,
				Rect = new global::UnityEngine.Rect(x, y, width, height)
			};
			AddToSVGDictionaryIfPossible(node, vectorElement);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
			currentSceneNode.Push(sceneNode);
			ParseChildren(node, node.Name);
			if (currentSceneNode.Pop() != sceneNode)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
		}

		private void mask()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = global::Unity.VectorGraphics.Matrix2D.identity
			};
			bool worldRelative;
			switch (node["maskUnits"])
			{
			case null:
			case "userSpaceOnUse":
				worldRelative = true;
				break;
			case "objectBoundingBox":
				worldRelative = false;
				break;
			default:
				throw node.GetUnsupportedAttribValException("maskUnits");
			}
			bool contentWorldRelative;
			switch (node["maskContentUnits"])
			{
			case null:
			case "userSpaceOnUse":
				contentWorldRelative = true;
				break;
			case "objectBoundingBox":
				contentWorldRelative = false;
				break;
			default:
				throw node.GetUnsupportedAttribValException("maskContentUnits");
			}
			maskData[sceneNode] = new global::Unity.VectorGraphics.SVGDocument.MaskData
			{
				WorldRelative = worldRelative,
				ContentWorldRelative = contentWorldRelative
			};
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
			currentSceneNode.Push(sceneNode);
			ParseChildren(node, node.Name);
			if (currentSceneNode.Pop() != sceneNode)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
		}

		private void rect()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(node, svgObjects, postponedFills, styles);
			global::Unity.VectorGraphics.PathCorner strokeCorner;
			global::Unity.VectorGraphics.PathEnding strokeEnding;
			global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(node, out strokeCorner, out strokeEnding);
			float x = AttribLengthVal(node, "x", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			float y = AttribLengthVal(node, "y", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			float num = AttribLengthVal(node, "rx", -1f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			float num2 = AttribLengthVal(node, "ry", -1f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			float num3 = AttribLengthVal(node, "width", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			float num4 = AttribLengthVal(node, "height", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
			if (num < 0f && num2 >= 0f)
			{
				num = num2;
			}
			else if (num2 < 0f && num >= 0f)
			{
				num2 = num;
			}
			else if (num2 < 0f && num < 0f)
			{
				num = (num2 = 0f);
			}
			num = global::UnityEngine.Mathf.Min(num, num3 * 0.5f);
			num2 = global::UnityEngine.Mathf.Min(num2, num4 * 0.5f);
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(num, num2);
			global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
			global::Unity.VectorGraphics.VectorUtils.MakeRectangleShape(shape, new global::UnityEngine.Rect(x, y, num3, num4), vector, vector, vector, vector);
			shape.Fill = fill;
			shape.PathProps = new global::Unity.VectorGraphics.PathProperties
			{
				Stroke = stroke,
				Head = strokeEnding,
				Tail = strokeEnding,
				Corners = strokeCorner
			};
			sceneNode.Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(1);
			sceneNode.Shapes.Add(shape);
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void stop()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.GradientStop gradientStop = default(global::Unity.VectorGraphics.GradientStop);
			string text = styles.Evaluate("stop-color");
			global::UnityEngine.Color color = ((text != null) ? global::Unity.VectorGraphics.SVGAttribParser.ParseColor(text) : global::UnityEngine.Color.black);
			color.a = AttribFloatVal("stop-opacity", 1f);
			gradientStop.Color = color;
			string text2 = styles.Evaluate("offset");
			if (!string.IsNullOrEmpty(text2))
			{
				bool flag = text2.EndsWith("%");
				if (flag)
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				gradientStop.StopPercentage = global::Unity.VectorGraphics.SVGAttribParser.ParseFloat(text2);
				if (flag)
				{
					gradientStop.StopPercentage /= 100f;
				}
				gradientStop.StopPercentage = global::UnityEngine.Mathf.Max(0f, gradientStop.StopPercentage);
				gradientStop.StopPercentage = global::UnityEngine.Mathf.Min(1f, gradientStop.StopPercentage);
			}
			global::Unity.VectorGraphics.GradientStop[] array;
			if (currentGradientFill.Stops == null || currentGradientFill.Stops.Length == 0)
			{
				array = new global::Unity.VectorGraphics.GradientStop[1];
			}
			else
			{
				array = new global::Unity.VectorGraphics.GradientStop[currentGradientFill.Stops.Length + 1];
				currentGradientFill.Stops.CopyTo(array, 0);
			}
			array[^1] = gradientStop;
			currentGradientFill.Stops = array;
			if (!string.IsNullOrEmpty(currentGradientId) && postponedStopData.ContainsKey(currentGradientId))
			{
				foreach (global::Unity.VectorGraphics.SVGDocument.PostponedStopData item in postponedStopData[currentGradientId])
				{
					item.fill.Stops = array;
				}
			}
			if (!string.IsNullOrEmpty(currentGradientLink) && postponedStopData.ContainsKey(currentGradientLink))
			{
				global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedStopData> list = postponedStopData[currentGradientLink];
				foreach (global::Unity.VectorGraphics.SVGDocument.PostponedStopData item2 in list)
				{
					if (item2.fill == currentGradientFill)
					{
						list.Remove(item2);
						break;
					}
				}
			}
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void svg()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode();
			if (scene.Root == null)
			{
				scene.Root = sceneNode;
			}
			styles.PushNode(node);
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneViewport = ParseViewport(node, sceneNode, new global::UnityEngine.Vector2(windowWidth, windowHeight));
			global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo viewBoxInfo = ParseViewBox(node, sceneNode, sceneViewport);
			if (applyRootViewBox)
			{
				ApplyViewBox(sceneNode, viewBoxInfo, sceneViewport);
			}
			currentContainerSize.Push(sceneViewport.size);
			if (!viewBoxInfo.IsEmpty)
			{
				currentViewBoxSize.Push(viewBoxInfo.ViewBox.size);
			}
			currentSceneNode.Push(sceneNode);
			nodeGlobalSceneState[sceneNode] = new global::Unity.VectorGraphics.SVGDocument.NodeGlobalSceneState
			{
				ContainerSize = currentContainerSize.Peek()
			};
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
			ParseChildren(node, "svg");
			if (currentSceneNode.Pop() != sceneNode)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
			if (!viewBoxInfo.IsEmpty)
			{
				currentViewBoxSize.Pop();
			}
			currentContainerSize.Pop();
			styles.PopNode();
		}

		private void symbol()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode();
			string text = node["id"];
			ParseID(node, sceneNode);
			ParseOpacity(sceneNode);
			sceneNode.Transform = global::Unity.VectorGraphics.Matrix2D.identity;
			global::UnityEngine.Rect rect = new global::UnityEngine.Rect(global::UnityEngine.Vector2.zero, currentContainerSize.Peek());
			global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo value = ParseViewBox(node, sceneNode, rect);
			if (!value.IsEmpty)
			{
				currentViewBoxSize.Push(value.ViewBox.size);
			}
			symbolViewBoxes[sceneNode] = value;
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node, allElems);
			}
			currentSceneNode.Push(sceneNode);
			ParseChildren(node, node.Name);
			if (currentSceneNode.Pop() != sceneNode)
			{
				throw global::Unity.VectorGraphics.SVGFormatException.StackError;
			}
			if (!value.IsEmpty)
			{
				currentViewBoxSize.Pop();
			}
			ParseClipAndMask(node, sceneNode);
			if (string.IsNullOrEmpty(text) || !postponedSymbolData.TryGetValue(text, out var value2))
			{
				return;
			}
			foreach (global::Unity.VectorGraphics.SVGDocument.NodeReferenceData item in value2)
			{
				ResolveReferencedNode(sceneNode, item, isDeferred: true);
			}
		}

		private void use()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			global::Unity.VectorGraphics.SceneNode sceneNode = currentSceneNode.Peek();
			ParseOpacity(sceneNode);
			global::UnityEngine.Rect viewport = ParseViewport(node, sceneNode, global::UnityEngine.Vector2.zero);
			global::Unity.VectorGraphics.SVGDocument.NodeReferenceData nodeReferenceData = new global::Unity.VectorGraphics.SVGDocument.NodeReferenceData
			{
				node = sceneNode,
				viewport = viewport,
				id = node["id"]
			};
			string text = node["xlink:href"];
			global::Unity.VectorGraphics.SceneNode sceneNode2 = global::Unity.VectorGraphics.SVGAttribParser.ParseRelativeRef(text, svgObjects) as global::Unity.VectorGraphics.SceneNode;
			if (sceneNode2 == null && !string.IsNullOrEmpty(text) && text.StartsWith("#"))
			{
				text = text.Substring(1);
				if (!postponedSymbolData.TryGetValue(text, out var value))
				{
					value = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.NodeReferenceData>();
					postponedSymbolData[text] = value;
				}
				value.Add(nodeReferenceData);
			}
			sceneNode.Transform = global::Unity.VectorGraphics.SVGAttribParser.ParseTransform(node);
			sceneNode.Transform *= global::Unity.VectorGraphics.Matrix2D.Translate(viewport.position);
			if (sceneNode2 != null)
			{
				ResolveReferencedNode(sceneNode2, nodeReferenceData, isDeferred: false);
			}
			ParseClipAndMask(node, sceneNode);
			AddToSVGDictionaryIfPossible(node, sceneNode);
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void style()
		{
			global::Unity.VectorGraphics.XmlReaderIterator.Node node = docReader.VisitCurrent();
			string text = docReader.ReadTextWithinElement();
			if (text.Length > 0)
			{
				styles.SetGlobalStyleSheet(global::Unity.VectorGraphics.SVGStyleSheetUtils.Parse(text));
			}
			if (ShouldDeclareSupportedChildren(node))
			{
				SupportElems(node);
			}
		}

		private void ResolveReferencedNode(global::Unity.VectorGraphics.SceneNode referencedNode, global::Unity.VectorGraphics.SVGDocument.NodeReferenceData refData, bool isDeferred)
		{
			if (symbolViewBoxes.TryGetValue(referencedNode, out var value))
			{
				ApplyViewBox(refData.node, value, refData.viewport);
			}
			if (refData.node.Children == null)
			{
				refData.node.Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode>();
			}
			global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer styleLayer = null;
			if (isDeferred)
			{
				styleLayer = styles.GetLayerForScenNode(refData.node);
				if (styleLayer != null)
				{
					styles.PushLayer(styleLayer);
				}
			}
			global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer styleLayer2 = nodeStyleLayers[referencedNode];
			if (styleLayer2 != null)
			{
				styles.PushLayer(styleLayer2);
			}
			global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> list = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode>(10);
			foreach (global::Unity.VectorGraphics.SceneNode item in global::Unity.VectorGraphics.VectorUtils.SceneNodes(referencedNode))
			{
				list.Add(item);
			}
			global::Unity.VectorGraphics.SceneNode sceneNode = CloneSceneNode(referencedNode);
			int num = 0;
			foreach (global::Unity.VectorGraphics.SceneNode item2 in global::Unity.VectorGraphics.VectorUtils.SceneNodes(sceneNode))
			{
				int index = num++;
				if (item2.Shapes == null)
				{
					continue;
				}
				global::Unity.VectorGraphics.SceneNode node = list[index];
				global::Unity.VectorGraphics.SVGStyleResolver.StyleLayer layerForScenNode = styles.GetLayerForScenNode(node);
				if (layerForScenNode != null)
				{
					styles.PushLayer(layerForScenNode);
				}
				bool isDefaultFill;
				global::Unity.VectorGraphics.IFill fill = global::Unity.VectorGraphics.SVGAttribParser.ParseFill(null, svgObjects, postponedFills, styles, global::Unity.VectorGraphics.Inheritance.Inherited, out isDefaultFill);
				global::Unity.VectorGraphics.PathCorner strokeCorner;
				global::Unity.VectorGraphics.PathEnding strokeEnding;
				global::Unity.VectorGraphics.Stroke stroke = ParseStrokeAttributeSet(null, out strokeCorner, out strokeEnding);
				foreach (global::Unity.VectorGraphics.Shape shape in item2.Shapes)
				{
					global::Unity.VectorGraphics.PathProperties pathProps = shape.PathProps;
					pathProps.Stroke = stroke;
					pathProps.Corners = strokeCorner;
					pathProps.Head = strokeEnding;
					shape.PathProps = pathProps;
					shape.Fill = (isDefaultFill ? shape.Fill : fill);
				}
				if (layerForScenNode != null)
				{
					styles.PopLayer();
				}
			}
			if (styleLayer2 != null)
			{
				styles.PopLayer();
			}
			if (styleLayer != null)
			{
				styles.PopLayer();
			}
			if (!string.IsNullOrEmpty(refData.id))
			{
				nodeIDs[refData.id] = sceneNode;
			}
			refData.node.Children.Add(sceneNode);
		}

		private global::Unity.VectorGraphics.SceneNode CloneSceneNode(global::Unity.VectorGraphics.SceneNode node)
		{
			if (node == null)
			{
				return null;
			}
			global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> list = null;
			if (node.Children != null)
			{
				list = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode>(node.Children.Count);
				foreach (global::Unity.VectorGraphics.SceneNode child in node.Children)
				{
					list.Add(CloneSceneNode(child));
				}
			}
			global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape> list2 = null;
			if (node.Shapes != null)
			{
				list2 = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape>(node.Shapes.Count);
				foreach (global::Unity.VectorGraphics.Shape shape in node.Shapes)
				{
					list2.Add(CloneShape(shape));
				}
			}
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode
			{
				Children = list,
				Shapes = list2,
				Transform = node.Transform,
				Clipper = CloneSceneNode(node.Clipper)
			};
			if (nodeGlobalSceneState.ContainsKey(node))
			{
				nodeGlobalSceneState[sceneNode] = nodeGlobalSceneState[node];
			}
			if (nodeOpacity.ContainsKey(node))
			{
				nodeOpacity[sceneNode] = nodeOpacity[node];
			}
			return sceneNode;
		}

		private global::Unity.VectorGraphics.Shape CloneShape(global::Unity.VectorGraphics.Shape shape)
		{
			if (shape == null)
			{
				return null;
			}
			global::Unity.VectorGraphics.BezierContour[] array = null;
			if (shape.Contours != null)
			{
				array = new global::Unity.VectorGraphics.BezierContour[shape.Contours.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = CloneContour(shape.Contours[i]);
				}
			}
			return new global::Unity.VectorGraphics.Shape
			{
				Fill = CloneFill(shape.Fill),
				FillTransform = shape.FillTransform,
				PathProps = ClonePathProps(shape.PathProps),
				Contours = array,
				IsConvex = shape.IsConvex
			};
		}

		private global::Unity.VectorGraphics.BezierContour CloneContour(global::Unity.VectorGraphics.BezierContour c)
		{
			global::Unity.VectorGraphics.BezierPathSegment[] array = null;
			if (c.Segments != null)
			{
				array = new global::Unity.VectorGraphics.BezierPathSegment[c.Segments.Length];
				for (int i = 0; i < array.Length; i++)
				{
					global::Unity.VectorGraphics.BezierPathSegment bezierPathSegment = c.Segments[i];
					array[i] = new global::Unity.VectorGraphics.BezierPathSegment
					{
						P0 = bezierPathSegment.P0,
						P1 = bezierPathSegment.P1,
						P2 = bezierPathSegment.P2
					};
				}
			}
			return new global::Unity.VectorGraphics.BezierContour
			{
				Segments = array,
				Closed = c.Closed
			};
		}

		private global::Unity.VectorGraphics.IFill CloneFill(global::Unity.VectorGraphics.IFill fill)
		{
			if (fill == null)
			{
				return null;
			}
			global::Unity.VectorGraphics.IFill result = null;
			if (fill is global::Unity.VectorGraphics.SolidFill)
			{
				global::Unity.VectorGraphics.SolidFill solidFill = fill as global::Unity.VectorGraphics.SolidFill;
				result = new global::Unity.VectorGraphics.SolidFill
				{
					Color = solidFill.Color,
					Opacity = solidFill.Opacity,
					Mode = solidFill.Mode
				};
			}
			else if (fill is global::Unity.VectorGraphics.GradientFill)
			{
				global::Unity.VectorGraphics.GradientFill gradientFill = fill as global::Unity.VectorGraphics.GradientFill;
				global::Unity.VectorGraphics.GradientStop[] array = null;
				if (gradientFill.Stops != null)
				{
					array = new global::Unity.VectorGraphics.GradientStop[gradientFill.Stops.Length];
					for (int i = 0; i < array.Length; i++)
					{
						global::Unity.VectorGraphics.GradientStop gradientStop = gradientFill.Stops[i];
						array[i] = new global::Unity.VectorGraphics.GradientStop
						{
							Color = gradientStop.Color,
							StopPercentage = gradientStop.StopPercentage
						};
					}
				}
				global::Unity.VectorGraphics.GradientFill gradientFill2 = new global::Unity.VectorGraphics.GradientFill
				{
					Type = gradientFill.Type,
					Stops = array,
					Mode = gradientFill.Mode,
					Opacity = gradientFill.Opacity,
					Addressing = gradientFill.Addressing,
					RadialFocus = gradientFill.RadialFocus
				};
				gradientExInfo[gradientFill2] = gradientExInfo[gradientFill];
				result = gradientFill2;
			}
			else if (fill is global::Unity.VectorGraphics.TextureFill)
			{
				global::Unity.VectorGraphics.TextureFill textureFill = fill as global::Unity.VectorGraphics.TextureFill;
				result = new global::Unity.VectorGraphics.TextureFill
				{
					Texture = textureFill.Texture,
					Mode = textureFill.Mode,
					Opacity = textureFill.Opacity,
					Addressing = textureFill.Addressing
				};
			}
			else if (fill is global::Unity.VectorGraphics.PatternFill)
			{
				global::Unity.VectorGraphics.PatternFill patternFill = fill as global::Unity.VectorGraphics.PatternFill;
				result = new global::Unity.VectorGraphics.PatternFill
				{
					Mode = patternFill.Mode,
					Opacity = patternFill.Opacity,
					Pattern = CloneSceneNode(patternFill.Pattern),
					Rect = patternFill.Rect
				};
			}
			return result;
		}

		private global::Unity.VectorGraphics.PathProperties ClonePathProps(global::Unity.VectorGraphics.PathProperties props)
		{
			global::Unity.VectorGraphics.Stroke stroke = null;
			if (props.Stroke != null)
			{
				float[] array = null;
				if (props.Stroke.Pattern != null)
				{
					array = new float[props.Stroke.Pattern.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = props.Stroke.Pattern[i];
					}
				}
				stroke = new global::Unity.VectorGraphics.Stroke
				{
					Fill = CloneFill(props.Stroke.Fill),
					FillTransform = props.Stroke.FillTransform,
					HalfThickness = props.Stroke.HalfThickness,
					Pattern = array,
					PatternOffset = props.Stroke.PatternOffset,
					TippedCornerLimit = props.Stroke.TippedCornerLimit
				};
			}
			return new global::Unity.VectorGraphics.PathProperties
			{
				Stroke = stroke,
				Head = props.Head,
				Tail = props.Tail,
				Corners = props.Corners
			};
		}

		private global::Unity.VectorGraphics.GradientFill CloneGradientFill(global::Unity.VectorGraphics.GradientFill other)
		{
			if (other == null)
			{
				return null;
			}
			return new global::Unity.VectorGraphics.GradientFill
			{
				Type = other.Type,
				Stops = other.Stops,
				Mode = other.Mode,
				Opacity = other.Opacity,
				Addressing = other.Addressing,
				RadialFocus = other.RadialFocus
			};
		}

		private int AttribIntVal(string attribName)
		{
			return AttribIntVal(attribName, 0);
		}

		private int AttribIntVal(string attribName, int defaultVal)
		{
			string text = styles.Evaluate(attribName);
			return (text != null) ? int.Parse(text) : defaultVal;
		}

		private float AttribFloatVal(string attribName)
		{
			return AttribFloatVal(attribName, 0f);
		}

		private float AttribFloatVal(string attribName, float defaultVal)
		{
			string text = styles.Evaluate(attribName);
			return (text != null) ? global::Unity.VectorGraphics.SVGAttribParser.ParseFloat(text) : defaultVal;
		}

		private float AttribLengthVal(global::Unity.VectorGraphics.XmlReaderIterator.Node node, string attribName, global::Unity.VectorGraphics.SVGDocument.DimType dimType)
		{
			return AttribLengthVal(node, attribName, 0f, dimType);
		}

		private float AttribLengthVal(global::Unity.VectorGraphics.XmlReaderIterator.Node node, string attribName, float defaultUnitVal, global::Unity.VectorGraphics.SVGDocument.DimType dimType)
		{
			string val = styles.Evaluate(attribName);
			return AttribLengthVal(val, node, attribName, defaultUnitVal, dimType);
		}

		private float AttribLengthVal(string val, global::Unity.VectorGraphics.XmlReaderIterator.Node node, string attribName, float defaultUnitVal, global::Unity.VectorGraphics.SVGDocument.DimType dimType)
		{
			if (val == null)
			{
				return defaultUnitVal;
			}
			val = val.Trim();
			string text = "px";
			char c = val[val.Length - 1];
			if (c == '%')
			{
				float num = global::Unity.VectorGraphics.SVGAttribParser.ParseFloat(val.Substring(0, val.Length - 1));
				if (num < 0f)
				{
					throw node.GetException("Number in " + attribName + " cannot be negative");
				}
				num /= 100f;
				global::UnityEngine.Vector2 vector = ((currentViewBoxSize.Count > 0) ? currentViewBoxSize.Peek() : currentContainerSize.Peek());
				switch (dimType)
				{
				case global::Unity.VectorGraphics.SVGDocument.DimType.Width:
					return num * vector.x;
				case global::Unity.VectorGraphics.SVGDocument.DimType.Height:
					return num * vector.y;
				case global::Unity.VectorGraphics.SVGDocument.DimType.Length:
					return num * vector.magnitude / 1.4142135f;
				}
			}
			else if (val.Length >= 2)
			{
				text = val.Substring(val.Length - 2);
			}
			if (char.IsDigit(c) || c == '.')
			{
				return global::Unity.VectorGraphics.SVGAttribParser.ParseFloat(val);
			}
			float num2 = global::Unity.VectorGraphics.SVGAttribParser.ParseFloat(val.Substring(0, val.Length - 2));
			return text switch
			{
				"em" => throw new global::System.NotImplementedException(), 
				"ex" => throw new global::System.NotImplementedException(), 
				"px" => num2, 
				"in" => 90f * num2 * dpiScale, 
				"cm" => 35.43307f * num2 * dpiScale, 
				"mm" => 3.543307f * num2 * dpiScale, 
				"pt" => 1.25f * num2 * dpiScale, 
				"pc" => 15f * num2 * dpiScale, 
				_ => throw new global::System.FormatException("Unknown length unit type (" + text + ")"), 
			};
		}

		private void AddToSVGDictionaryIfPossible(global::Unity.VectorGraphics.XmlReaderIterator.Node node, object vectorElement)
		{
			string text = node["id"];
			if (!string.IsNullOrEmpty(text))
			{
				svgObjects[text] = vectorElement;
			}
		}

		private global::UnityEngine.Rect ParseViewport(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SceneNode sceneNode, global::UnityEngine.Vector2 defaultViewportSize)
		{
			scenePos.x = AttribLengthVal(node, "x", global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			scenePos.y = AttribLengthVal(node, "y", global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			sceneSize.x = AttribLengthVal(node, "width", defaultViewportSize.x, global::Unity.VectorGraphics.SVGDocument.DimType.Width);
			sceneSize.y = AttribLengthVal(node, "height", defaultViewportSize.y, global::Unity.VectorGraphics.SVGDocument.DimType.Height);
			return new global::UnityEngine.Rect(scenePos, sceneSize);
		}

		private global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo ParseViewBox(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SceneNode sceneNode, global::UnityEngine.Rect sceneViewport)
		{
			global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo viewBoxInfo = new global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo
			{
				IsEmpty = true
			};
			string text = node["viewBox"]?.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return viewBoxInfo;
			}
			string[] array = text.Split(new char[2] { ' ', ',' }, global::System.StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 4)
			{
				throw node.GetException("Invalid viewBox specification");
			}
			global::UnityEngine.Vector2 position = new global::UnityEngine.Vector2(AttribLengthVal(array[0], node, "viewBox", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(array[1], node, "viewBox", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
			global::UnityEngine.Vector2 size = new global::UnityEngine.Vector2(AttribLengthVal(array[2], node, "viewBox", sceneViewport.width, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(array[3], node, "viewBox", sceneViewport.height, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
			viewBoxInfo.ViewBox = new global::UnityEngine.Rect(position, size);
			ParseViewBoxAspectRatio(node, ref viewBoxInfo);
			viewBoxInfo.IsEmpty = false;
			return viewBoxInfo;
		}

		private void ParseViewBoxAspectRatio(global::Unity.VectorGraphics.XmlReaderIterator.Node node, ref global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo viewBoxInfo)
		{
			viewBoxInfo.AspectRatio = global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio.FitLargestDim;
			viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
			viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
			string text = node["preserveAspectRatio"]?.Trim();
			bool flag = false;
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(new char[2] { ' ', ',' }, global::System.StringSplitOptions.RemoveEmptyEntries);
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					switch (array2[i])
					{
					case "none":
						flag = true;
						break;
					case "xMinYMin":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Min;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Min;
						break;
					case "xMidYMin":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Min;
						break;
					case "xMaxYMin":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Min;
						break;
					case "xMinYMid":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Min;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
						break;
					case "xMidYMid":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
						break;
					case "xMaxYMid":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
						break;
					case "xMinYMax":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Min;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max;
						break;
					case "xMidYMax":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max;
						break;
					case "xMaxYMax":
						viewBoxInfo.AlignX = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max;
						viewBoxInfo.AlignY = global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max;
						break;
					case "meet":
						viewBoxInfo.AspectRatio = global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio.FitLargestDim;
						break;
					case "slice":
						viewBoxInfo.AspectRatio = global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio.FitSmallestDim;
						break;
					}
				}
			}
			if (flag)
			{
				viewBoxInfo.AspectRatio = global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio.DontPreserve;
			}
		}

		private void ApplyViewBox(global::Unity.VectorGraphics.SceneNode sceneNode, global::Unity.VectorGraphics.SVGDocument.ViewBoxInfo viewBoxInfo, global::UnityEngine.Rect sceneViewport)
		{
			if (viewBoxInfo.ViewBox.size == global::UnityEngine.Vector2.zero || sceneViewport.size == global::UnityEngine.Vector2.zero)
			{
				return;
			}
			global::UnityEngine.Vector2 vector = global::UnityEngine.Vector2.one;
			global::UnityEngine.Vector2 vector2 = -viewBoxInfo.ViewBox.position;
			if (viewBoxInfo.AspectRatio == global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio.DontPreserve)
			{
				vector = sceneViewport.size / viewBoxInfo.ViewBox.size;
			}
			else
			{
				vector.x = (vector.y = sceneViewport.width / viewBoxInfo.ViewBox.width);
				bool flag = ((viewBoxInfo.AspectRatio != global::Unity.VectorGraphics.SVGDocument.ViewBoxAspectRatio.FitLargestDim) ? (viewBoxInfo.ViewBox.height * vector.y > sceneViewport.height) : (viewBoxInfo.ViewBox.height * vector.y <= sceneViewport.height));
				global::UnityEngine.Vector2 zero = global::UnityEngine.Vector2.zero;
				if (flag)
				{
					if (viewBoxInfo.AlignY == global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid)
					{
						zero.y = (sceneViewport.height - viewBoxInfo.ViewBox.height * vector.y) * 0.5f;
					}
					else if (viewBoxInfo.AlignY == global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max)
					{
						zero.y = sceneViewport.height - viewBoxInfo.ViewBox.height * vector.y;
					}
				}
				else
				{
					vector.x = (vector.y = sceneViewport.height / viewBoxInfo.ViewBox.height);
					if (viewBoxInfo.AlignX == global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Mid)
					{
						zero.x = (sceneViewport.width - viewBoxInfo.ViewBox.width * vector.x) * 0.5f;
					}
					else if (viewBoxInfo.AlignX == global::Unity.VectorGraphics.SVGDocument.ViewBoxAlign.Max)
					{
						zero.x = sceneViewport.width - viewBoxInfo.ViewBox.width * vector.x;
					}
				}
				vector2 += zero / vector;
			}
			sceneNode.Transform = sceneNode.Transform * global::Unity.VectorGraphics.Matrix2D.Scale(vector) * global::Unity.VectorGraphics.Matrix2D.Translate(vector2);
		}

		private global::Unity.VectorGraphics.Stroke ParseStrokeAttributeSet(global::Unity.VectorGraphics.XmlReaderIterator.Node node, out global::Unity.VectorGraphics.PathCorner strokeCorner, out global::Unity.VectorGraphics.PathEnding strokeEnding, global::Unity.VectorGraphics.Inheritance inheritance = global::Unity.VectorGraphics.Inheritance.Inherited)
		{
			global::Unity.VectorGraphics.Stroke stroke = global::Unity.VectorGraphics.SVGAttribParser.ParseStrokeAndOpacity(node, svgObjects, styles, inheritance);
			strokeCorner = global::Unity.VectorGraphics.PathCorner.Tipped;
			strokeEnding = global::Unity.VectorGraphics.PathEnding.Chop;
			if (stroke != null)
			{
				string val = styles.Evaluate("stroke-width", inheritance);
				stroke.HalfThickness = AttribLengthVal(val, node, "stroke-width", 1f, global::Unity.VectorGraphics.SVGDocument.DimType.Length) * 0.5f;
				switch (styles.Evaluate("stroke-linecap", inheritance))
				{
				case "butt":
					strokeEnding = global::Unity.VectorGraphics.PathEnding.Chop;
					break;
				case "square":
					strokeEnding = global::Unity.VectorGraphics.PathEnding.Square;
					break;
				case "round":
					strokeEnding = global::Unity.VectorGraphics.PathEnding.Round;
					break;
				}
				switch (styles.Evaluate("stroke-linejoin", inheritance))
				{
				case "miter":
					strokeCorner = global::Unity.VectorGraphics.PathCorner.Tipped;
					break;
				case "round":
					strokeCorner = global::Unity.VectorGraphics.PathCorner.Round;
					break;
				case "bevel":
					strokeCorner = global::Unity.VectorGraphics.PathCorner.Beveled;
					break;
				}
				string text = styles.Evaluate("stroke-dasharray", inheritance);
				if (text != null && text != "none")
				{
					string[] array = text.Split(whiteSpaceNumberChars, global::System.StringSplitOptions.RemoveEmptyEntries);
					int num = (((array.Length & 1) == 1) ? (array.Length * 2) : array.Length);
					stroke.Pattern = new float[num];
					for (int i = 0; i < array.Length; i++)
					{
						stroke.Pattern[i] = AttribLengthVal(array[i], node, "stroke-dasharray", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
					}
					if (num > array.Length)
					{
						for (int j = 0; j < array.Length; j++)
						{
							stroke.Pattern[j + array.Length] = stroke.Pattern[j];
						}
					}
					string val2 = styles.Evaluate("stroke-dashoffset", inheritance);
					stroke.PatternOffset = AttribLengthVal(val2, node, "stroke-dashoffset", 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
				}
				string val3 = styles.Evaluate("stroke-miterlimit", inheritance);
				stroke.TippedCornerLimit = AttribLengthVal(val3, node, "stroke-miterlimit", 4f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
				if (stroke.TippedCornerLimit < 1f)
				{
					throw node.GetException("'stroke-miterlimit' should be greater or equal to 1");
				}
			}
			return stroke;
		}

		private void ParseID(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SceneNode sceneNode)
		{
			string text = node["id"];
			if (!string.IsNullOrEmpty(text))
			{
				nodeIDs[text] = sceneNode;
				nodeStyleLayers[sceneNode] = styles.PeekLayer();
			}
		}

		private float ParseOpacity(global::Unity.VectorGraphics.SceneNode sceneNode)
		{
			float num = AttribFloatVal("opacity", 1f);
			if (num != 1f && sceneNode != null)
			{
				nodeOpacity[sceneNode] = num;
			}
			return num;
		}

		private void ParseClipAndMask(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SceneNode sceneNode)
		{
			ParseClip(node, sceneNode);
			ParseMask(node, sceneNode);
		}

		private void ParseClip(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SceneNode sceneNode)
		{
			string text = null;
			string text2 = styles.Evaluate("clip-path");
			if (text2 != null)
			{
				text = global::Unity.VectorGraphics.SVGAttribParser.ParseURLRef(text2);
			}
			if (text == null)
			{
				return;
			}
			global::Unity.VectorGraphics.SceneNode sceneNode2 = global::Unity.VectorGraphics.SVGAttribParser.ParseRelativeRef(text, svgObjects) as global::Unity.VectorGraphics.SceneNode;
			if (sceneNode2 == null && text.Length > 1 && text.StartsWith("#"))
			{
				if (!postponedClip.TryGetValue(text, out var value))
				{
					value = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.PostponedClip>(1);
				}
				value.Add(new global::Unity.VectorGraphics.SVGDocument.PostponedClip
				{
					node = sceneNode
				});
				postponedClip[text.Substring(1)] = value;
			}
			else
			{
				global::Unity.VectorGraphics.SceneNode sceneNode3 = sceneNode2;
				bool worldRelative = true;
				if (clipData.TryGetValue(sceneNode2, out var value2))
				{
					worldRelative = value2.WorldRelative;
				}
				ApplyClipper(sceneNode2, sceneNode, worldRelative);
			}
		}

		private void ApplyClipper(global::Unity.VectorGraphics.SceneNode clipper, global::Unity.VectorGraphics.SceneNode target, bool worldRelative)
		{
			global::Unity.VectorGraphics.SceneNode clipper2 = clipper;
			if (!worldRelative)
			{
				global::UnityEngine.Rect rect = global::Unity.VectorGraphics.VectorUtils.SceneNodeBounds(target);
				global::Unity.VectorGraphics.Matrix2D transform = global::Unity.VectorGraphics.Matrix2D.Translate(rect.position) * global::Unity.VectorGraphics.Matrix2D.Scale(rect.size);
				clipper2 = new global::Unity.VectorGraphics.SceneNode
				{
					Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> { clipper },
					Transform = transform
				};
			}
			target.Clipper = clipper2;
		}

		private void ParseMask(global::Unity.VectorGraphics.XmlReaderIterator.Node node, global::Unity.VectorGraphics.SceneNode sceneNode)
		{
			string text = null;
			string text2 = node["mask"];
			if (text2 != null)
			{
				text = global::Unity.VectorGraphics.SVGAttribParser.ParseURLRef(text2);
			}
			if (text != null)
			{
				global::Unity.VectorGraphics.SceneNode sceneNode2 = global::Unity.VectorGraphics.SVGAttribParser.ParseRelativeRef(text, svgObjects) as global::Unity.VectorGraphics.SceneNode;
				global::Unity.VectorGraphics.SceneNode clipper = sceneNode2;
				if (maskData.TryGetValue(sceneNode2, out var value) && !value.ContentWorldRelative)
				{
					global::UnityEngine.Rect rect = global::Unity.VectorGraphics.VectorUtils.SceneNodeBounds(sceneNode);
					global::Unity.VectorGraphics.Matrix2D transform = global::Unity.VectorGraphics.Matrix2D.Translate(rect.position) * global::Unity.VectorGraphics.Matrix2D.Scale(rect.size);
					clipper = new global::Unity.VectorGraphics.SceneNode
					{
						Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> { sceneNode2 },
						Transform = transform
					};
				}
				sceneNode.Clipper = clipper;
			}
		}

		private global::UnityEngine.Texture2D DecodeTextureData(string dataURI)
		{
			int i = 5;
			int length = dataURI.Length;
			int num = i;
			for (; i < length && dataURI[i] != ';' && dataURI[i] != ','; i++)
			{
			}
			string text = dataURI.Substring(num, i - num).ToLower();
			if (text != "image/png" && text != "image/jpeg")
			{
				return null;
			}
			for (; i < length && dataURI[i] != ','; i++)
			{
			}
			i++;
			if (i >= length)
			{
				return null;
			}
			byte[] data = global::System.Convert.FromBase64String(dataURI.Substring(i));
			global::UnityEngine.Texture2D texture2D = new global::UnityEngine.Texture2D(1, 1);
			if (global::UnityEngine.ImageConversion.LoadImage(texture2D, data))
			{
				return texture2D;
			}
			return null;
		}

		private void PostProcess(global::Unity.VectorGraphics.SceneNode root)
		{
			AdjustFills(root);
		}

		private void AdjustFills(global::Unity.VectorGraphics.SceneNode root)
		{
			global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.HierarchyUpdate> list = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SVGDocument.HierarchyUpdate>();
			foreach (global::Unity.VectorGraphics.VectorUtils.SceneNodeWorldTransform item in global::Unity.VectorGraphics.VectorUtils.WorldTransformedSceneNodes(root, nodeOpacity))
			{
				if (item.Node.Shapes == null)
				{
					continue;
				}
				foreach (global::Unity.VectorGraphics.Shape shape in item.Node.Shapes)
				{
					if (shape.Fill != null && postponedFills.TryGetValue(shape.Fill, out var value) && global::Unity.VectorGraphics.SVGAttribParser.ParseRelativeRef(value, svgObjects) is global::Unity.VectorGraphics.IFill fill)
					{
						shape.Fill = fill;
					}
					global::Unity.VectorGraphics.Stroke stroke = shape.PathProps.Stroke;
					if (stroke != null && stroke.Fill is global::Unity.VectorGraphics.GradientFill)
					{
						global::Unity.VectorGraphics.Matrix2D computedTransform = global::Unity.VectorGraphics.Matrix2D.identity;
						AdjustGradientFill(item.Node, item.WorldTransform, stroke.Fill, shape.Contours, ref computedTransform);
						stroke.FillTransform = computedTransform;
					}
					if (shape.Fill is global::Unity.VectorGraphics.GradientFill)
					{
						global::Unity.VectorGraphics.Matrix2D computedTransform2 = global::Unity.VectorGraphics.Matrix2D.identity;
						AdjustGradientFill(item.Node, item.WorldTransform, shape.Fill, shape.Contours, ref computedTransform2);
						shape.FillTransform = computedTransform2;
					}
					else if (shape.Fill is global::Unity.VectorGraphics.PatternFill)
					{
						global::Unity.VectorGraphics.SceneNode sceneNode = AdjustPatternFill(item.Node, item.WorldTransform, shape);
						if (sceneNode != null)
						{
							list.Add(new global::Unity.VectorGraphics.SVGDocument.HierarchyUpdate
							{
								Parent = item.Parent,
								NewNode = sceneNode,
								ReplaceNode = item.Node
							});
						}
					}
				}
			}
			foreach (global::Unity.VectorGraphics.SVGDocument.HierarchyUpdate item2 in list)
			{
				int index = item2.Parent.Children.IndexOf(item2.ReplaceNode);
				item2.Parent.Children.RemoveAt(index);
				item2.Parent.Children.Insert(index, item2.NewNode);
			}
		}

		private void AdjustGradientFill(global::Unity.VectorGraphics.SceneNode node, global::Unity.VectorGraphics.Matrix2D worldTransform, global::Unity.VectorGraphics.IFill fill, global::Unity.VectorGraphics.BezierContour[] contours, ref global::Unity.VectorGraphics.Matrix2D computedTransform)
		{
			global::Unity.VectorGraphics.GradientFill gradientFill = fill as global::Unity.VectorGraphics.GradientFill;
			if (fill == null || contours == null || contours.Length == 0)
			{
				return;
			}
			global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(float.MaxValue, float.MaxValue);
			global::UnityEngine.Vector2 vector2 = new global::UnityEngine.Vector2(float.MinValue, float.MinValue);
			foreach (global::Unity.VectorGraphics.BezierContour bezierContour in contours)
			{
				global::UnityEngine.Rect rect = global::Unity.VectorGraphics.VectorUtils.Bounds(bezierContour.Segments);
				vector = global::UnityEngine.Vector2.Min(vector, rect.min);
				vector2 = global::UnityEngine.Vector2.Max(vector2, rect.max);
			}
			global::UnityEngine.Rect rect2 = new global::UnityEngine.Rect(vector, vector2 - vector);
			global::Unity.VectorGraphics.SVGDocument.GradientExData gradientExData = gradientExInfo[gradientFill];
			global::UnityEngine.Vector2 containerSize = nodeGlobalSceneState[node].ContainerSize;
			global::Unity.VectorGraphics.Matrix2D matrix2D = global::Unity.VectorGraphics.Matrix2D.identity;
			currentContainerSize.Push(gradientExData.WorldRelative ? containerSize : global::UnityEngine.Vector2.one);
			if (gradientExData is global::Unity.VectorGraphics.SVGDocument.LinearGradientExData)
			{
				global::Unity.VectorGraphics.SVGDocument.LinearGradientExData linearGradientExData = (global::Unity.VectorGraphics.SVGDocument.LinearGradientExData)gradientExData;
				global::UnityEngine.Vector2 vector3 = new global::UnityEngine.Vector2(AttribLengthVal(linearGradientExData.X1, null, null, 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(linearGradientExData.Y1, null, null, 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
				global::UnityEngine.Vector2 vector4 = new global::UnityEngine.Vector2(AttribLengthVal(linearGradientExData.X2, null, null, currentContainerSize.Peek().x, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(linearGradientExData.Y2, null, null, 0f, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
				global::UnityEngine.Vector2 vector5 = vector4 - vector3;
				float num = 1f / vector5.magnitude;
				global::Unity.VectorGraphics.Matrix2D matrix2D2 = global::Unity.VectorGraphics.Matrix2D.Scale(new global::UnityEngine.Vector2(rect2.width * num, rect2.height * num));
				global::Unity.VectorGraphics.Matrix2D matrix2D3 = global::Unity.VectorGraphics.Matrix2D.RotateLH(global::UnityEngine.Mathf.Atan2(vector5.y, vector5.x));
				global::Unity.VectorGraphics.Matrix2D matrix2D4 = global::Unity.VectorGraphics.Matrix2D.Translate(-vector3);
				matrix2D = matrix2D2 * matrix2D3 * matrix2D4;
			}
			else if (gradientExData is global::Unity.VectorGraphics.SVGDocument.RadialGradientExData)
			{
				global::Unity.VectorGraphics.SVGDocument.RadialGradientExData radialGradientExData = (global::Unity.VectorGraphics.SVGDocument.RadialGradientExData)gradientExData;
				global::UnityEngine.Vector2 vector6 = currentContainerSize.Peek() * 0.5f;
				global::UnityEngine.Vector2 vector7 = new global::UnityEngine.Vector2(AttribLengthVal(radialGradientExData.Cx, null, null, vector6.x, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(radialGradientExData.Cy, null, null, vector6.y, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
				global::UnityEngine.Vector2 vector8 = new global::UnityEngine.Vector2(AttribLengthVal(radialGradientExData.Fx, null, null, vector7.x, global::Unity.VectorGraphics.SVGDocument.DimType.Width), AttribLengthVal(radialGradientExData.Fy, null, null, vector7.y, global::Unity.VectorGraphics.SVGDocument.DimType.Height));
				float num2 = AttribLengthVal(radialGradientExData.R, null, null, vector6.magnitude / 1.4142135f, global::Unity.VectorGraphics.SVGDocument.DimType.Length);
				if (!radialGradientExData.Parsed)
				{
					gradientFill.RadialFocus = (vector8 - vector7) / num2;
					if (gradientFill.RadialFocus.sqrMagnitude > 1f - global::Unity.VectorGraphics.VectorUtils.Epsilon)
					{
						gradientFill.RadialFocus = gradientFill.RadialFocus.normalized * (1f - global::Unity.VectorGraphics.VectorUtils.Epsilon);
					}
					radialGradientExData.Parsed = true;
				}
				matrix2D = global::Unity.VectorGraphics.Matrix2D.Scale(rect2.size * 0.5f / num2) * global::Unity.VectorGraphics.Matrix2D.Translate(new global::UnityEngine.Vector2(num2, num2) - vector7);
			}
			else
			{
				global::UnityEngine.Debug.LogError("Unsupported gradient type: " + gradientExData);
			}
			currentContainerSize.Pop();
			global::Unity.VectorGraphics.Matrix2D matrix2D5 = (gradientExData.WorldRelative ? (global::Unity.VectorGraphics.Matrix2D.Translate(rect2.min) * global::Unity.VectorGraphics.Matrix2D.Scale(rect2.size)) : global::Unity.VectorGraphics.Matrix2D.identity);
			global::UnityEngine.Vector2 vector9 = new global::UnityEngine.Vector2(1f / rect2.width, 1f / rect2.height);
			computedTransform = global::Unity.VectorGraphics.Matrix2D.Scale(vector9) * matrix2D * gradientExData.FillTransform.Inverse() * matrix2D5;
		}

		private global::Unity.VectorGraphics.SceneNode AdjustPatternFill(global::Unity.VectorGraphics.SceneNode node, global::Unity.VectorGraphics.Matrix2D worldTransform, global::Unity.VectorGraphics.Shape shape)
		{
			if (!(shape.Fill is global::Unity.VectorGraphics.PatternFill { Rect: var rect } patternFill) || global::UnityEngine.Mathf.Abs(rect.width) < global::Unity.VectorGraphics.VectorUtils.Epsilon || global::UnityEngine.Mathf.Abs(patternFill.Rect.height) < global::Unity.VectorGraphics.VectorUtils.Epsilon)
			{
				return null;
			}
			global::Unity.VectorGraphics.SVGDocument.PatternData patternData = this.patternData[patternFill.Pattern];
			global::UnityEngine.Rect rect2 = global::Unity.VectorGraphics.VectorUtils.SceneNodeBounds(node);
			global::UnityEngine.Rect rect3 = patternFill.Rect;
			if (!patternData.WorldRelative)
			{
				rect3.position *= rect2.size;
				rect3.size *= rect2.size;
			}
			global::Unity.VectorGraphics.SceneNode sceneNode = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = node.Transform,
				Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode>(2)
			};
			node.Transform = global::Unity.VectorGraphics.Matrix2D.identity;
			global::Unity.VectorGraphics.SceneNode sceneNode2 = patternFill.Pattern;
			if (!patternData.ContentWorldRelative)
			{
				sceneNode2 = new global::Unity.VectorGraphics.SceneNode
				{
					Transform = global::Unity.VectorGraphics.Matrix2D.Scale(rect2.size),
					Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> { patternFill.Pattern }
				};
			}
			PostProcess(sceneNode2);
			global::Unity.VectorGraphics.SceneNode sceneNode3 = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = patternData.PatternTransform,
				Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode>(20)
			};
			global::Unity.VectorGraphics.SceneNode item = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = global::Unity.VectorGraphics.Matrix2D.identity,
				Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> { sceneNode3 },
				Clipper = node
			};
			global::Unity.VectorGraphics.Shape shape2 = new global::Unity.VectorGraphics.Shape();
			global::Unity.VectorGraphics.VectorUtils.MakeRectangleShape(shape2, new global::UnityEngine.Rect(0f, 0f, rect3.width, rect3.height));
			global::Unity.VectorGraphics.SceneNode clipper = new global::Unity.VectorGraphics.SceneNode
			{
				Transform = global::Unity.VectorGraphics.Matrix2D.identity,
				Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape> { shape2 }
			};
			global::UnityEngine.Rect rect4 = global::Unity.VectorGraphics.VectorUtils.SceneNodeBounds(node);
			global::Unity.VectorGraphics.Matrix2D matrix2D = patternData.PatternTransform.Inverse();
			global::UnityEngine.Vector2[] vertices = new global::UnityEngine.Vector2[4]
			{
				matrix2D * new global::UnityEngine.Vector2(rect4.xMin, rect4.yMin),
				matrix2D * new global::UnityEngine.Vector2(rect4.xMax, rect4.yMin),
				matrix2D * new global::UnityEngine.Vector2(rect4.xMax, rect4.yMax),
				matrix2D * new global::UnityEngine.Vector2(rect4.xMin, rect4.yMax)
			};
			rect4 = global::Unity.VectorGraphics.VectorUtils.Bounds(vertices);
			float num = rect4.xMax / rect3.width;
			float num2 = rect4.yMax / rect3.height;
			if (global::UnityEngine.Mathf.Abs(rect3.width) < global::Unity.VectorGraphics.VectorUtils.Epsilon || global::UnityEngine.Mathf.Abs(rect3.height) < global::Unity.VectorGraphics.VectorUtils.Epsilon || num * num2 > 5000f)
			{
				global::UnityEngine.Debug.LogWarning("Ignoring pattern which would result in too many repetitions");
				return null;
			}
			global::UnityEngine.Vector2 position = rect3.position;
			float num3 = (float)(int)(rect4.x / rect3.width) * rect3.width - rect3.width;
			float num4 = (float)(int)(rect4.y / rect3.height) * rect3.height - rect3.height;
			for (float num5 = num4; num5 < rect4.yMax; num5 += rect3.height)
			{
				for (float num6 = num3; num6 < rect4.xMax; num6 += rect3.width)
				{
					global::Unity.VectorGraphics.SceneNode item2 = new global::Unity.VectorGraphics.SceneNode
					{
						Transform = global::Unity.VectorGraphics.Matrix2D.Translate(new global::UnityEngine.Vector2(num6, num5) + position),
						Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> { sceneNode2 },
						Clipper = clipper
					};
					sceneNode3.Children.Add(item2);
				}
			}
			sceneNode.Children.Add(item);
			sceneNode.Children.Add(node);
			return sceneNode;
		}

		private void RemoveInvisibleNodes()
		{
			foreach (global::Unity.VectorGraphics.SVGDocument.NodeWithParent invisibleNode in invisibleNodes)
			{
				if (invisibleNode.parent.Children != null)
				{
					invisibleNode.parent.Children.Remove(invisibleNode.node);
				}
			}
		}

		private bool ShouldDeclareSupportedChildren(global::Unity.VectorGraphics.XmlReaderIterator.Node node)
		{
			return !subTags.ContainsKey(node.Name);
		}

		private void SupportElems(global::Unity.VectorGraphics.XmlReaderIterator.Node node, params global::Unity.VectorGraphics.SVGDocument.ElemHandler[] handlers)
		{
			global::Unity.VectorGraphics.SVGDocument.Handlers handlers2 = new global::Unity.VectorGraphics.SVGDocument.Handlers(handlers.Length);
			foreach (global::Unity.VectorGraphics.SVGDocument.ElemHandler elemHandler in handlers)
			{
				handlers2[elemHandler.Method.Name] = elemHandler;
			}
			subTags[node.Name] = handlers2;
		}
	}
}
