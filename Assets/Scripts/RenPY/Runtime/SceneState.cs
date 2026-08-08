using System;
using System.Collections.Generic;

namespace RenPy.Runtime
{
    /// <summary>
    /// What is currently shown, per layer. Ren'Py keys displayables by tag within a
    /// layer, so showing "eileen happy" replaces whatever "eileen" was showing.
    /// </summary>
    public class SceneState
    {
        public readonly List<string> LayerOrder = new List<string> { "master", "transient", "screens", "overlay" };

        readonly Dictionary<string, Dictionary<string, ShownImage>> layers =
            new Dictionary<string, Dictionary<string, ShownImage>>();

        /// <summary>Remembers which layer a tag was last shown on.</summary>
        readonly Dictionary<string, string> tagLayers = new Dictionary<string, string>();

        public Dictionary<string, ShownImage> Layer(string name)
        {
            Dictionary<string, ShownImage> layer;
            if (!layers.TryGetValue(name, out layer))
            {
                layer = new Dictionary<string, ShownImage>();
                layers[name] = layer;
                if (!LayerOrder.Contains(name)) LayerOrder.Add(name);
            }
            return layer;
        }

        public void Show(ShownImage image)
        {
            if (string.IsNullOrEmpty(image.Tag)) return;

            // Showing a tag on a new layer removes it from the old one.
            string previous;
            if (tagLayers.TryGetValue(image.Tag, out previous) && previous != image.Layer)
                Layer(previous).Remove(image.Tag);

            Layer(image.Layer)[image.Tag] = image;
            tagLayers[image.Tag] = image.Layer;
        }

        public void Hide(string layerName, string tag)
        {
            if (string.IsNullOrEmpty(tag)) return;
            tag = RenPyScript.Normalize(tag);
            Layer(layerName).Remove(tag);
            tagLayers.Remove(tag);
        }

        public void ClearLayer(string layerName)
        {
            var layer = Layer(layerName);
            foreach (var tag in new List<string>(layer.Keys)) tagLayers.Remove(tag);
            layer.Clear();
        }

        public string LayerForTag(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return null;
            string layer;
            return tagLayers.TryGetValue(RenPyScript.Normalize(tag), out layer) ? layer : null;
        }

        public ShownImage Get(string layerName, string tag)
        {
            ShownImage image;
            return Layer(layerName).TryGetValue(RenPyScript.Normalize(tag), out image) ? image : null;
        }

        /// <summary>Images on a layer, sorted by z-order for drawing.</summary>
        public List<ShownImage> Sorted(string layerName)
        {
            var rv = new List<ShownImage>(Layer(layerName).Values);
            rv.Sort((a, b) => a.ZOrder.CompareTo(b.ZOrder));
            return rv;
        }

        public void Clear()
        {
            layers.Clear();
            tagLayers.Clear();
        }
    }
}
