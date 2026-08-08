using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RenPy.Python;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// Builds uGUI from the evaluated screen-language widget tree.
    ///
    /// Widgets are rebuilt whenever the engine reports a change rather than being
    /// diffed: screens are small, and rebuilding keeps this honest about state.
    /// </summary>
    public class RenPyScreenView
    {
        readonly RectTransform root;
        readonly Func<string, Texture2D> textureLoader;
        readonly Action<object> onAction;
        readonly Action<string> playSound;
        readonly Func<string, string, bool, Texture> movieResolver;

        readonly Dictionary<string, GameObject> screens = new Dictionary<string, GameObject>();

        public RenPyScreenView(Transform parent,
                               Func<string, Texture2D> textureLoader,
                               Action<object> onAction,
                               Action<string> playSound,
                               Func<string, string, bool, Texture> movieResolver)
        {
            this.textureLoader = textureLoader;
            this.onAction = onAction;
            this.playSound = playSound;
            this.movieResolver = movieResolver;

            var go = new GameObject("RenPy Screens", typeof(RectTransform));
            go.transform.SetParent(parent, false);

            root = (RectTransform)go.transform;
            Stretch(root);
        }

        public RectTransform Root { get { return root; } }

        /// <summary>Rebuilds the displayed screens.</summary>
        public void Rebuild(List<ShownScreen> shown)
        {
            foreach (var kv in screens) UnityEngine.Object.Destroy(kv.Value);
            screens.Clear();

            // Screen movies are keyed by build order, so restart the numbering. The
            // movie pool reuses a key when the path is unchanged, which keeps a menu
            // backdrop playing across rebuilds instead of restarting every frame.
            movieSerial = 0;

            if (shown == null) return;

            foreach (var screen in shown)
            {
                if (screen.Root == null) continue;

                var holder = new GameObject("screen " + screen.Name, typeof(RectTransform));
                holder.transform.SetParent(root, false);
                Stretch((RectTransform)holder.transform);

                screens[screen.Name] = holder;

                foreach (var child in screen.Root.Children)
                    Build(child, (RectTransform)holder.transform);
            }
        }

        public bool IsShowing(string name) { return screens.ContainsKey(name); }

        // ---------------------------------------------------------------- building

        void Build(ScreenWidget widget, RectTransform parent)
        {
            switch (widget.Kind)
            {
                // Behaviours with no visual representation.
                case "on":
                case "key":
                case "timer":
                case "null":
                case "dismiss":
                    return;

                case "vbox":
                case "hbox":
                    BuildBox(widget, parent, widget.Kind == "vbox");
                    return;

                case "imagebutton":
                    BuildImageButton(widget, parent);
                    return;

                case "textbutton":
                case "button":
                    BuildTextButton(widget, parent);
                    return;

                case "text":
                case "label":
                    BuildText(widget, parent);
                    return;

                case "add":
                case "image":
                    BuildImage(widget, parent);
                    return;

                case "frame":
                case "window":
                case "viewport":
                case "vpgrid":
                case "grid":
                case "side":
                case "fixed":
                default:
                    BuildContainer(widget, parent);
                    return;
            }
        }

        RectTransform BuildContainer(ScreenWidget widget, RectTransform parent)
        {
            var rect = Create(widget.Kind, parent);
            Stretch(rect);
            Position(widget, rect, 0, 0);

            foreach (var child in widget.Children) Build(child, rect);
            return rect;
        }

        void BuildBox(ScreenWidget widget, RectTransform parent, bool vertical)
        {
            var rect = Create(widget.Kind, parent);

            // A box sizes to its content and sits where the transform puts it.
            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);

            var layout = vertical
                ? (HorizontalOrVerticalLayoutGroup)rect.gameObject.AddComponent<VerticalLayoutGroup>()
                : rect.gameObject.AddComponent<HorizontalLayoutGroup>();

            layout.spacing = widget.Float("spacing", 0f);
            layout.childForceExpandWidth = false;
            layout.childForceExpandHeight = false;
            layout.childControlWidth = true;
            layout.childControlHeight = true;
            layout.childAlignment = TextAnchor.UpperLeft;

            var fitter = rect.gameObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            Position(widget, rect, 0, 0);

            foreach (var child in widget.Children) Build(child, rect);
        }

        int movieSerial;

        void BuildImage(ScreenWidget widget, RectTransform parent)
        {
            // A screen can `add` a Movie, which is how animated menu backdrops work.
            if (widget.Displayable != null && widget.Displayable.Kind == "movie")
            {
                Texture movie = movieResolver("screen/" + (movieSerial++), widget.Displayable.Path, true);
                if (movie != null)
                {
                    var movieRect = Create("movie", parent);
                    var movieImage = movieRect.gameObject.AddComponent<RawImage>();
                    movieImage.texture = movie;
                    movieImage.raycastTarget = false;
                    Stretch(movieRect);
                    return;
                }
                // Fall through to start_image when the video cannot play.
            }

            if (widget.Displayable != null && widget.Displayable.Kind == "solid")
            {
                var solidRect = Create("solid", parent);
                var solid = solidRect.gameObject.AddComponent<Image>();
                solid.color = RenPyUI.ParseColor(widget.Displayable.Color, Color.black);
                solid.raycastTarget = false;
                Stretch(solidRect);
                return;
            }

            Texture2D texture = widget.Idle != null ? textureLoader(widget.Idle) : null;
            if (texture == null)
            {
                // Still build children so a bare `add` container is not lost.
                foreach (var child in widget.Children) Build(child, parent);
                return;
            }

            var rect = Create("image", parent);
            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;
            image.raycastTarget = false;

            Position(widget, rect, texture.width, texture.height);

            foreach (var child in widget.Children) Build(child, rect);
        }

        void BuildImageButton(ScreenWidget widget, RectTransform parent)
        {
            Texture2D idle = widget.Idle != null ? textureLoader(widget.Idle) : null;
            Texture2D hover = widget.Hover != null ? textureLoader(widget.Hover) : null;

            if (idle == null && hover == null) return;
            if (idle == null) idle = hover;

            var rect = Create("imagebutton", parent);

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = idle;

            Position(widget, rect, idle.width, idle.height);

            Wire(rect.gameObject, widget, image, idle, hover);
        }

        void BuildTextButton(ScreenWidget widget, RectTransform parent)
        {
            var rect = Create("textbutton", parent);

            var background = rect.gameObject.AddComponent<Image>();
            background.color = new Color(0f, 0f, 0f, 0.35f);

            var element = rect.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = widget.Float("ysize", 56f);
            element.preferredWidth = widget.Float("xsize", 420f);

            Position(widget, rect, 0, 0);

            if (widget.Text != null)
            {
                var textHolder = Create("text", rect);
                Stretch(textHolder);

                var text = textHolder.gameObject.AddComponent<TextMeshProUGUI>();
                text.text = widget.Text;
                text.fontSize = widget.Float("size", 32f);
                text.alignment = TextAlignmentOptions.Center;
                text.color = RenPyUI.ParseColor(widget.Str("color"), Color.white);
                text.raycastTarget = false;
            }

            Wire(rect.gameObject, widget, null, null, null);
        }

        void BuildText(ScreenWidget widget, RectTransform parent)
        {
            if (widget.Text == null) return;

            var rect = Create("text", parent);

            var text = rect.gameObject.AddComponent<TextMeshProUGUI>();
            text.text = widget.Text;
            text.fontSize = widget.Float("size", 32f);
            text.color = RenPyUI.ParseColor(widget.Str("color"), Color.white);
            text.raycastTarget = false;
            text.alignment = TextAlignmentOptions.TopLeft;

            var fitter = rect.gameObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);

            Position(widget, rect, 0, 0);
        }

        /// <summary>Attaches click and hover behaviour to a widget.</summary>
        void Wire(GameObject go, ScreenWidget widget, RawImage image, Texture2D idle, Texture2D hover)
        {
            var button = go.AddComponent<Button>();
            button.transition = Selectable.Transition.None;

            var graphic = go.GetComponent<Graphic>();
            if (graphic != null) { graphic.raycastTarget = true; button.targetGraphic = graphic; }

            if (widget.Action != null)
            {
                object action = widget.Action;
                string sound = widget.ActivateSound;

                button.onClick.AddListener(() =>
                {
                    if (!string.IsNullOrEmpty(sound)) playSound(sound);
                    onAction(action);
                });
            }

            // Hover swaps the texture and fires hovered/unhovered actions.
            var trigger = go.AddComponent<EventTrigger>();

            AddTrigger(trigger, EventTriggerType.PointerEnter, () =>
            {
                if (image != null && hover != null) image.texture = hover;
                if (!string.IsNullOrEmpty(widget.HoverSound)) playSound(widget.HoverSound);
                if (widget.Hovered != null) onAction(widget.Hovered);
            });

            AddTrigger(trigger, EventTriggerType.PointerExit, () =>
            {
                if (image != null && idle != null) image.texture = idle;
                if (widget.Unhovered != null) onAction(widget.Unhovered);
            });
        }

        static void AddTrigger(EventTrigger trigger, EventTriggerType type, Action handler)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener(_ => handler());
            trigger.triggers.Add(entry);
        }

        // ---------------------------------------------------------------- layout

        static RectTransform Create(string name, Transform parent)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            return (RectTransform)go.transform;
        }

        static void Stretch(RectTransform rect)
        {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        /// <summary>Places a widget using Ren'Py's top-left, y-down convention.</summary>
        static void Position(ScreenWidget widget, RectTransform rect, int width, int height)
        {
            var state = widget.Transform;

            bool sized = width > 0 && height > 0;
            if (sized)
                rect.sizeDelta = new Vector2(width * state.Zoom * state.XZoom,
                                             height * state.Zoom * state.YZoom);

            // Layout groups own the position of their children.
            var parent = rect.parent as RectTransform;
            if (parent != null && parent.GetComponent<LayoutGroup>() != null) return;

            float anchorX = state.HasAlign && state.XAlign >= 0f ? state.XAlign : state.XAnchor;
            float anchorY = state.HasAlign && state.YAlign >= 0f ? state.YAlign : state.YAnchor;

            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(anchorX, 1f - anchorY);

            float x = state.HasAlign && state.XAlign >= 0f
                ? state.XAlign * RenPyUI.DesignWidth
                : Resolve(state.XPos, RenPyUI.DesignWidth);

            float y = state.HasAlign && state.YAlign >= 0f
                ? state.YAlign * RenPyUI.DesignHeight
                : Resolve(state.YPos, RenPyUI.DesignHeight);

            rect.anchoredPosition = new Vector2(x, -y);
            rect.localRotation = Quaternion.Euler(0f, 0f, -state.Rotate);

            if (!sized && rect.GetComponent<ContentSizeFitter>() == null && rect.GetComponent<LayoutGroup>() == null)
            {
                // Containers with no intrinsic size fill their parent.
                Stretch(rect);
            }
        }

        /// <summary>0..1 is a fraction of the screen; larger values are pixels.</summary>
        static float Resolve(float value, float extent)
        {
            if (value > -1.0001f && value < 1.0001f && value != 0f) return value * extent;
            return value;
        }
    }
}
