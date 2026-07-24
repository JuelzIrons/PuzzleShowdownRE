namespace Unity.VectorGraphics
{
	public class SVGParser
	{
		public struct SceneInfo
		{
			public global::Unity.VectorGraphics.Scene Scene { get; }

			public global::UnityEngine.Rect SceneViewport { get; }

			public global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, float> NodeOpacity { get; }

			public global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SceneNode> NodeIDs { get; }

			internal SceneInfo(global::Unity.VectorGraphics.Scene scene, global::UnityEngine.Rect sceneViewport, global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, float> nodeOpacities, global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SceneNode> nodeIDs)
			{
				Scene = scene;
				SceneViewport = sceneViewport;
				NodeOpacity = nodeOpacities;
				NodeIDs = nodeIDs;
			}
		}

		public static global::Unity.VectorGraphics.SVGParser.SceneInfo ImportSVG(global::System.IO.TextReader textReader, float dpi = 0f, float pixelsPerUnit = 1f, int windowWidth = 0, int windowHeight = 0, bool clipViewport = false)
		{
			global::Unity.VectorGraphics.ViewportOptions viewportOptions = (clipViewport ? global::Unity.VectorGraphics.ViewportOptions.PreserveViewport : global::Unity.VectorGraphics.ViewportOptions.DontPreserve);
			return ImportSVG(textReader, viewportOptions, dpi, pixelsPerUnit, windowWidth, windowHeight);
		}

		public static global::Unity.VectorGraphics.SVGParser.SceneInfo ImportSVG(global::System.IO.TextReader textReader, global::Unity.VectorGraphics.ViewportOptions viewportOptions, float dpi = 0f, float pixelsPerUnit = 1f, int windowWidth = 0, int windowHeight = 0)
		{
			global::Unity.VectorGraphics.Scene scene = new global::Unity.VectorGraphics.Scene();
			global::System.Xml.XmlReaderSettings xmlReaderSettings = new global::System.Xml.XmlReaderSettings();
			xmlReaderSettings.IgnoreComments = true;
			xmlReaderSettings.IgnoreProcessingInstructions = true;
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.DtdProcessing = global::System.Xml.DtdProcessing.Ignore;
			xmlReaderSettings.ValidationFlags = global::System.Xml.Schema.XmlSchemaValidationFlags.None;
			xmlReaderSettings.ValidationType = global::System.Xml.ValidationType.None;
			xmlReaderSettings.XmlResolver = null;
			if (dpi == 0f)
			{
				dpi = global::UnityEngine.Screen.dpi;
			}
			global::Unity.VectorGraphics.SVGDocument sVGDocument;
			global::System.Collections.Generic.Dictionary<global::Unity.VectorGraphics.SceneNode, float> nodeOpacities;
			global::System.Collections.Generic.Dictionary<string, global::Unity.VectorGraphics.SceneNode> nodeIDs;
			using (global::System.Xml.XmlReader docReader = global::System.Xml.XmlReader.Create(textReader, xmlReaderSettings))
			{
				bool applyRootViewBox = viewportOptions == global::Unity.VectorGraphics.ViewportOptions.PreserveViewport || viewportOptions == global::Unity.VectorGraphics.ViewportOptions.OnlyApplyRootViewBox;
				sVGDocument = new global::Unity.VectorGraphics.SVGDocument(docReader, dpi, scene, windowWidth, windowHeight, applyRootViewBox);
				sVGDocument.Import();
				nodeOpacities = sVGDocument.NodeOpacities;
				nodeIDs = sVGDocument.NodeIDs;
			}
			float num = 1f / pixelsPerUnit;
			if (num != 1f && scene != null && scene.Root != null)
			{
				scene.Root.Transform = scene.Root.Transform * global::Unity.VectorGraphics.Matrix2D.Scale(new global::UnityEngine.Vector2(num, num));
			}
			if (viewportOptions == global::Unity.VectorGraphics.ViewportOptions.PreserveViewport && scene != null && scene.Root != null)
			{
				global::UnityEngine.Rect rect = global::Unity.VectorGraphics.VectorUtils.SceneNodeBounds(scene.Root);
				if (!sVGDocument.sceneViewport.Contains(rect.min) || !sVGDocument.sceneViewport.Contains(rect.max))
				{
					global::Unity.VectorGraphics.Shape shape = new global::Unity.VectorGraphics.Shape();
					global::Unity.VectorGraphics.VectorUtils.MakeRectangleShape(shape, sVGDocument.sceneViewport);
					scene.Root = new global::Unity.VectorGraphics.SceneNode
					{
						Children = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.SceneNode> { scene.Root },
						Clipper = new global::Unity.VectorGraphics.SceneNode
						{
							Shapes = new global::System.Collections.Generic.List<global::Unity.VectorGraphics.Shape> { shape }
						}
					};
				}
			}
			return new global::Unity.VectorGraphics.SVGParser.SceneInfo(scene, sVGDocument.sceneViewport, nodeOpacities, nodeIDs);
		}
	}
}
