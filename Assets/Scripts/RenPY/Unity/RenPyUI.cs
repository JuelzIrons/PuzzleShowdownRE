using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// Builds and drives the on-screen presentation: image layers, the dialogue box,
    /// and the choice menu. Everything here runs on the main thread.
    /// </summary>
    public class RenPyUI
    {
        public const float DesignWidth = 1920f;
        public const float DesignHeight = 1080f;

        public Canvas Canvas { get; private set; }
        public RectTransform Root { get; private set; }

        readonly Dictionary<string, RectTransform> layers = new Dictionary<string, RectTransform>();
        readonly Dictionary<string, ImageView> shown = new Dictionary<string, ImageView>();

        RectTransform dialogueBox;
        TextMeshProUGUI nameText;
        TextMeshProUGUI bodyText;
        Image dialogueBackground;

        RectTransform choicePanel;
        readonly List<Button> choiceButtons = new List<Button>();

        RectTransform fadeOverlay;
        Image fadeImage;

        /// <summary>A displayable currently parented into a layer.</summary>
        sealed class ImageView
        {
            public GameObject Root;
            public RawImage Raw;
            public Image Solid;
            public string Layer;
            public string Key;

            // Natural size of the content, needed to re-apply the transform each frame.
            public int Width = (int)DesignWidth;
            public int Height = (int)DesignHeight;

            // Running ATL animation.
            public AtlTimeline Timeline;
            public TransformState From;
            public TransformState Current;
            public int StepIndex;
            public float Elapsed;
            public int LoopsDone;
        }

        readonly Func<string, Texture2D> textureLoader;
        readonly Func<string, string, bool, Texture> movieResolver;
        readonly Action<string> movieReleaser;
        readonly int sortingOrder;

        public RenPyUI(Transform parent,
                       Func<string, Texture2D> textureLoader,
                       Func<string, string, bool, Texture> movieResolver,
                       Action<string> movieReleaser,
                       int sortingOrder)
        {
            this.textureLoader = textureLoader;
            this.movieResolver = movieResolver;
            this.movieReleaser = movieReleaser;
            this.sortingOrder = sortingOrder;
            Build(parent);
        }

        // ---------------------------------------------------------------- build

        void Build(Transform parent)
        {
            var canvasObject = new GameObject("RenPy Canvas",
                typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasObject.transform.SetParent(parent, false);

            Canvas = canvasObject.GetComponent<Canvas>();
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Canvas.sortingOrder = sortingOrder;

            var scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(DesignWidth, DesignHeight);
            // Ren'Py letterboxes to preserve the authored composition.
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;

            Root = canvasObject.GetComponent<RectTransform>();

            // A black backdrop so uncovered areas never show the Unity camera.
            var backdrop = CreateChild("Backdrop", Root);
            Stretch(backdrop);
            var backdropImage = backdrop.gameObject.AddComponent<Image>();
            backdropImage.color = Color.black;
            backdropImage.raycastTarget = false;

            foreach (var name in new[] { "master", "transient", "screens", "overlay" }) Layer(name);

            BuildAdvanceArea();
            BuildDialogue();
            BuildChoices();
            BuildFade();
        }

        /// <summary>
        /// A transparent full-screen button, so "click to advance" works through the
        /// event system rather than polling an input backend that may be disabled.
        /// </summary>
        void BuildAdvanceArea()
        {
            var area = CreateChild("Advance", Root);
            Stretch(area);

            var graphic = area.gameObject.AddComponent<Image>();
            graphic.color = new Color(0f, 0f, 0f, 0f);
            graphic.raycastTarget = true;

            advanceButton = area.gameObject.AddComponent<Button>();
            advanceButton.transition = Selectable.Transition.None;
            advanceButton.targetGraphic = graphic;
        }

        Button advanceButton;

        /// <summary>Sets the callback invoked when the player clicks to advance.</summary>
        public void SetAdvanceHandler(Action onAdvance)
        {
            if (advanceButton == null) return;
            advanceButton.onClick.RemoveAllListeners();
            advanceButton.onClick.AddListener(() => onAdvance());
        }

        RectTransform Layer(string name)
        {
            RectTransform layer;
            if (layers.TryGetValue(name, out layer)) return layer;

            layer = CreateChild("Layer " + name, Root);
            Stretch(layer);
            layers[name] = layer;

            // Keep UI above the image layers.
            if (dialogueBox != null) dialogueBox.SetAsLastSibling();
            if (choicePanel != null) choicePanel.SetAsLastSibling();
            if (fadeOverlay != null) fadeOverlay.SetAsLastSibling();

            return layer;
        }

        void BuildDialogue()
        {
            dialogueBox = CreateChild("Dialogue", Root);
            dialogueBox.anchorMin = new Vector2(0f, 0f);
            dialogueBox.anchorMax = new Vector2(1f, 0f);
            dialogueBox.pivot = new Vector2(0.5f, 0f);
            dialogueBox.sizeDelta = new Vector2(0f, 280f);
            dialogueBox.anchoredPosition = Vector2.zero;

            dialogueBackground = dialogueBox.gameObject.AddComponent<Image>();
            dialogueBackground.color = new Color(0f, 0f, 0f, 0.72f);
            dialogueBackground.raycastTarget = false;

            var nameObject = CreateChild("Name", dialogueBox);
            nameObject.anchorMin = new Vector2(0f, 1f);
            nameObject.anchorMax = new Vector2(1f, 1f);
            nameObject.pivot = new Vector2(0f, 1f);
            nameObject.offsetMin = new Vector2(60f, 0f);
            nameObject.offsetMax = new Vector2(-60f, 0f);
            nameObject.sizeDelta = new Vector2(nameObject.sizeDelta.x, 54f);
            nameObject.anchoredPosition = new Vector2(60f, -14f);

            nameText = nameObject.gameObject.AddComponent<TextMeshProUGUI>();
            nameText.fontSize = 40f;
            nameText.fontStyle = FontStyles.Bold;
            nameText.color = new Color(0.85f, 0.75f, 0.45f);
            nameText.raycastTarget = false;

            var bodyObject = CreateChild("Body", dialogueBox);
            bodyObject.anchorMin = new Vector2(0f, 0f);
            bodyObject.anchorMax = new Vector2(1f, 1f);
            bodyObject.offsetMin = new Vector2(60f, 24f);
            bodyObject.offsetMax = new Vector2(-60f, -72f);

            bodyText = bodyObject.gameObject.AddComponent<TextMeshProUGUI>();
            bodyText.fontSize = 36f;
            bodyText.color = Color.white;
            bodyText.raycastTarget = false;
            bodyText.textWrappingMode = TextWrappingModes.Normal;
            bodyText.overflowMode = TextOverflowModes.Overflow;

            dialogueBox.gameObject.SetActive(false);
        }

        void BuildChoices()
        {
            choicePanel = CreateChild("Choices", Root);
            choicePanel.anchorMin = new Vector2(0.5f, 0.5f);
            choicePanel.anchorMax = new Vector2(0.5f, 0.5f);
            choicePanel.pivot = new Vector2(0.5f, 0.5f);
            choicePanel.sizeDelta = new Vector2(1200f, 800f);

            var layout = choicePanel.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 18f;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childControlWidth = true;

            choicePanel.gameObject.SetActive(false);
        }

        void BuildFade()
        {
            fadeOverlay = CreateChild("Transition", Root);
            Stretch(fadeOverlay);

            fadeImage = fadeOverlay.gameObject.AddComponent<Image>();
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
            fadeImage.raycastTarget = false;

            fadeOverlay.gameObject.SetActive(false);
        }

        static RectTransform CreateChild(string name, Transform parent)
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

        // ---------------------------------------------------------------- images

        // The outgoing contents of a layer, retained so a transition has something to
        // fade FROM. Ren'Py transitions between the old and new state of a layer; if
        // the old widgets are destroyed on `scene`, there is nothing to dissolve.
        /// <summary>
        /// A retained layer plus the movies it owns. The video must outlive the
        /// crossfade but die with the ghost, or it keeps playing (and sounding)
        /// after the scene it belonged to is gone.
        /// </summary>
        sealed class Ghost
        {
            public CanvasGroup Group;
            public readonly List<string> MovieKeys = new List<string>();
        }

        readonly Dictionary<string, Ghost> ghosts = new Dictionary<string, Ghost>();

        /// <summary>
        /// Moves a layer's current contents aside before it is modified, so the next
        /// transition can fade them out over the new scene.
        /// </summary>
        void EnsureGhost(string layerName)
        {
            if (ghosts.ContainsKey(layerName)) return;

            var source = Layer(layerName);
            if (source.childCount == 0) return;

            var go = new GameObject("ghost " + layerName, typeof(RectTransform), typeof(CanvasGroup));
            var rect = (RectTransform)go.transform;
            rect.SetParent(Root, false);
            Stretch(rect);
            // Directly above its layer, so new content builds underneath it.
            rect.SetSiblingIndex(source.GetSiblingIndex() + 1);

            var group = go.GetComponent<CanvasGroup>();
            group.blocksRaycasts = false;
            group.interactable = false;

            while (source.childCount > 0) source.GetChild(0).SetParent(rect, true);

            var ghost = new Ghost { Group = group };

            // The live views are gone; new shows must build fresh ones. Their movie
            // keys transfer to the ghost so nothing is left playing untracked.
            var orphaned = new List<string>();
            foreach (var kv in shown)
                if (kv.Value.Layer == layerName) orphaned.Add(kv.Key);

            foreach (var key in orphaned)
            {
                ghost.MovieKeys.Add(key);
                shown.Remove(key);
            }

            ghosts[layerName] = ghost;
        }

        /// <summary>Drops the retained scene; called once a transition ends or is moot.</summary>
        public void DiscardGhosts()
        {
            foreach (var kv in ghosts)
            {
                var ghost = kv.Value;
                if (ghost == null) continue;

                // Stop the videos before the widgets that displayed them go away.
                foreach (var key in ghost.MovieKeys) movieReleaser(key);

                if (ghost.Group != null) UnityEngine.Object.Destroy(ghost.Group.gameObject);
            }

            ghosts.Clear();
        }

        /// <summary>Scale applied to images whose game authored a different resolution.</summary>
        public float ImageScale = 1f;

        readonly HashSet<string> suppressed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Sets the "layer/tag" entries that are never drawn. Purely an override for
        /// artwork that misbehaves; the engine still tracks the image as shown.
        /// </summary>
        public void SetSuppressed(string csv)
        {
            suppressed.Clear();
            if (string.IsNullOrEmpty(csv)) return;

            foreach (var entry in csv.Split(','))
            {
                string trimmed = entry.Trim();
                if (trimmed.Length > 0) suppressed.Add(trimmed);
            }
        }

        public void Show(ShownImage image)
        {
            EnsureGhost(image.Layer);

            string key = image.Layer + "/" + image.Tag;

            if (suppressed.Contains(key))
            {
                // Drop any existing view so a previously drawn frame does not linger.
                ImageView existing;
                if (shown.TryGetValue(key, out existing))
                {
                    movieReleaser(key);
                    UnityEngine.Object.Destroy(existing.Root);
                    shown.Remove(key);
                }
                return;
            }

            ImageView view;
            if (!shown.TryGetValue(key, out view))
            {
                view = new ImageView { Layer = image.Layer, Key = key };
                view.Root = new GameObject(key, typeof(RectTransform));
                view.Root.transform.SetParent(Layer(image.Layer), false);
                shown[key] = view;
            }

            var rect = (RectTransform)view.Root.transform;

            BeginAnimation(view, image);

            if (image.AssetPath != null)
            {
                Texture2D texture = textureLoader(image.AssetPath);
                if (texture != null)
                {
                    if (view.Raw == null)
                    {
                        if (view.Solid != null) { UnityEngine.Object.Destroy(view.Solid); view.Solid = null; }
                        view.Raw = view.Root.AddComponent<RawImage>();
                        view.Raw.raycastTarget = false;
                    }
                    view.Raw.texture = texture;
                    view.Width = Mathf.RoundToInt(texture.width * ImageScale);
                    view.Height = Mathf.RoundToInt(texture.height * ImageScale);
                    ApplyTransform(rect, image.Transform, view.Width, view.Height);
                    SetAlpha(view, image.Transform.Alpha);
                    OrderByZ(image);
                    return;
                }
            }

            if (image.Displayable != null && image.Displayable.Kind == "movie")
            {
                Texture movie = movieResolver(key, image.Displayable.Path, true);
                if (movie != null)
                {
                    if (view.Raw == null)
                    {
                        if (view.Solid != null) { UnityEngine.Object.Destroy(view.Solid); view.Solid = null; }
                        view.Raw = view.Root.AddComponent<RawImage>();
                        view.Raw.raycastTarget = false;
                    }
                    view.Raw.texture = movie;
                    // A movie has no intrinsic size until it loads, so fill the screen.
                    Stretch(rect);
                    SetAlpha(view, image.Transform.Alpha);
                    OrderByZ(image);
                    return;
                }
            }

            if (image.Displayable != null && image.Displayable.Kind == "solid")
            {
                if (view.Solid == null)
                {
                    if (view.Raw != null) { UnityEngine.Object.Destroy(view.Raw); view.Raw = null; }
                    view.Solid = view.Root.AddComponent<Image>();
                    view.Solid.raycastTarget = false;
                }
                view.Solid.color = ParseColor(image.Displayable.Color, Color.black);
                Stretch(rect);
                SetAlpha(view, image.Transform.Alpha);
                OrderByZ(image);
                return;
            }

            // Nothing renderable; hide rather than leaving a stale frame.
            view.Root.SetActive(false);
        }

        /// <summary>Arms (or clears) the ATL animation attached to a shown image.</summary>
        static void BeginAnimation(ImageView view, ShownImage image)
        {
            view.Current = image.Transform.Clone();
            view.From = image.Transform.Clone();
            view.StepIndex = 0;
            view.Elapsed = 0f;
            view.LoopsDone = 0;
            view.Timeline = (image.Timeline != null && image.Timeline.HasAnimation) ? image.Timeline : null;
        }

        /// <summary>
        /// Advances every running ATL animation. Called once per frame by the player.
        /// </summary>
        public void Tick(float deltaTime)
        {
            foreach (var view in shown.Values)
            {
                if (view.Timeline == null || view.Root == null) continue;

                var steps = view.Timeline.Steps;
                if (view.StepIndex >= steps.Count) { view.Timeline = null; continue; }

                var step = steps[view.StepIndex];
                view.Elapsed += deltaTime;

                float t = step.Duration <= 0f ? 1f : Mathf.Clamp01(view.Elapsed / step.Duration);
                float warped = Warpers.Apply(step.Warper, t);

                view.Current = TransformBlend.Lerp(view.From, step.Target, warped);
                ApplyState(view);

                if (t < 1f) continue;

                // Step finished; move on, looping if the block asked to repeat.
                view.From = step.Target.Clone();
                view.Elapsed = 0f;
                view.StepIndex++;

                if (view.StepIndex < steps.Count) continue;

                int repeat = view.Timeline.Repeat;
                view.LoopsDone++;

                if (repeat == 0 || view.LoopsDone < repeat)
                {
                    view.StepIndex = 0;
                    view.From = view.Timeline.Initial.Clone();
                }
                else
                {
                    view.Timeline = null;
                }
            }
        }

        void ApplyState(ImageView view)
        {
            var rect = (RectTransform)view.Root.transform;

            if (view.Solid != null) Stretch(rect);
            else ApplyTransform(rect, view.Current, view.Width, view.Height);

            SetAlpha(view, view.Current.Alpha);
        }

        void OrderByZ(ShownImage image)
        {
            var layer = Layer(image.Layer);
            var ordered = new List<Transform>();
            for (int i = 0; i < layer.childCount; i++) ordered.Add(layer.GetChild(i));

            // Higher z-order draws later; uGUI paints in sibling order.
            ordered.Sort((a, b) =>
            {
                int za = ZOf(a.name), zb = ZOf(b.name);
                return za.CompareTo(zb);
            });

            for (int i = 0; i < ordered.Count; i++) ordered[i].SetSiblingIndex(i);
        }

        readonly Dictionary<string, int> zOrders = new Dictionary<string, int>();

        int ZOf(string key)
        {
            int z;
            return zOrders.TryGetValue(key, out z) ? z : 0;
        }

        public void RecordZ(ShownImage image) { zOrders[image.Layer + "/" + image.Tag] = image.ZOrder; }

        void SetAlpha(ImageView view, float alpha)
        {
            view.Root.SetActive(alpha > 0.001f);
            if (view.Raw != null)
            {
                var c = view.Raw.color;
                view.Raw.color = new Color(c.r, c.g, c.b, alpha);
            }
            if (view.Solid != null)
            {
                var c = view.Solid.color;
                view.Solid.color = new Color(c.r, c.g, c.b, alpha);
            }
        }

        /// <summary>Maps Ren'Py transform values onto a RectTransform.</summary>
        static void ApplyTransform(RectTransform rect, TransformState state, int width, int height)
        {
            float zoomX = state.Zoom * state.XZoom;
            float zoomY = state.Zoom * state.YZoom;

            // A negative zoom mirrors the image. Feeding that into sizeDelta yields a
            // degenerate rect, so the sign becomes a scale flip and the size stays
            // positive -- this is how `xzoom -1` faces a sprite the other way.
            rect.sizeDelta = new Vector2(width * Mathf.Abs(zoomX), height * Mathf.Abs(zoomY));
            rect.localScale = new Vector3(zoomX < 0f ? -1f : 1f, zoomY < 0f ? -1f : 1f, 1f);

            // Ren'Py measures from the top-left with y growing downwards.
            float anchorX = state.XAnchor;
            float anchorY = state.YAnchor;

            float xalign = state.XAlign >= 0f ? state.XAlign : 0f;
            float yalign = state.YAlign >= 0f ? state.YAlign : 0f;

            if (state.HasAlign)
            {
                anchorX = xalign;
                anchorY = yalign;
            }

            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(anchorX, 1f - anchorY);

            float x = state.HasAlign ? xalign * DesignWidth : Resolve(state.XPos, DesignWidth);
            float y = state.HasAlign ? yalign * DesignHeight : Resolve(state.YPos, DesignHeight);

            rect.anchoredPosition = new Vector2(x, -y);
            rect.localRotation = Quaternion.Euler(0f, 0f, -state.Rotate);
        }

        /// <summary>Ren'Py treats 0..1 as a fraction of the screen and larger values as pixels.</summary>
        static float Resolve(float value, float extent)
        {
            if (value > -1.0001f && value < 1.0001f && value != 0f) return value * extent;
            return value;
        }

        public void Hide(string layer, string tag)
        {
            string key = layer + "/" + RenPyScript.Normalize(tag);
            ImageView view;
            if (!shown.TryGetValue(key, out view)) return;

            movieReleaser(key);
            UnityEngine.Object.Destroy(view.Root);
            shown.Remove(key);
            zOrders.Remove(key);
        }

        public void ClearLayer(string layer)
        {
            EnsureGhost(layer);

            var toRemove = new List<string>();
            foreach (var kv in shown)
                if (kv.Value.Layer == layer) toRemove.Add(kv.Key);

            foreach (var key in toRemove)
            {
                movieReleaser(key);
                UnityEngine.Object.Destroy(shown[key].Root);
                shown.Remove(key);
                zOrders.Remove(key);
            }
        }

        // ---------------------------------------------------------------- dialogue

        public void ShowDialogue(SpeakerStyle who, string what)
        {
            dialogueBox.gameObject.SetActive(true);

            bool hasName = who != null && !string.IsNullOrEmpty(who.Name);
            nameText.gameObject.SetActive(hasName);

            if (hasName)
            {
                nameText.text = who.Name;
                nameText.color = ParseColor(who.NameColor, new Color(0.85f, 0.75f, 0.45f));
            }

            bodyText.text = what ?? "";
            bodyText.color = who != null ? ParseColor(who.WhatColor, Color.white) : Color.white;
        }

        public void HideDialogue()
        {
            if (dialogueBox != null) dialogueBox.gameObject.SetActive(false);
        }

        /// <summary>Drives the typewriter reveal. Pass int.MaxValue to show everything.</summary>
        public void SetVisibleCharacters(int count)
        {
            if (bodyText == null) return;

            if (count == int.MaxValue)
            {
                bodyText.maxVisibleCharacters = int.MaxValue;
                return;
            }

            bodyText.maxVisibleCharacters = Mathf.Max(0, count);
        }

        /// <summary>True when a texture is still parented into a visible layer.</summary>
        public bool IsTextureInUse(Texture2D texture)
        {
            if (texture == null) return false;

            foreach (var view in shown.Values)
                if (view.Raw != null && ReferenceEquals(view.Raw.texture, texture)) return true;

            return false;
        }

        // ---------------------------------------------------------------- choices

        public void ShowChoices(string caption, List<string> choices, Action<int> onPick)
        {
            HideDialogue();
            ClearChoices();

            choicePanel.gameObject.SetActive(true);

            if (!string.IsNullOrEmpty(caption)) AddCaption(caption);

            for (int i = 0; i < choices.Count; i++)
            {
                int index = i;
                choiceButtons.Add(CreateChoiceButton(choices[i], () => onPick(index)));
            }
        }

        void AddCaption(string caption)
        {
            var holder = CreateChild("Caption", choicePanel);
            var text = holder.gameObject.AddComponent<TextMeshProUGUI>();
            text.text = caption;
            text.fontSize = 38f;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;
            text.raycastTarget = false;

            var element = holder.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = 70f;
        }

        Button CreateChoiceButton(string label, Action onClick)
        {
            var holder = CreateChild("Choice", choicePanel);

            var background = holder.gameObject.AddComponent<Image>();
            background.color = new Color(0.12f, 0.12f, 0.16f, 0.92f);

            var button = holder.gameObject.AddComponent<Button>();
            button.targetGraphic = background;

            var colors = button.colors;
            colors.highlightedColor = new Color(0.28f, 0.28f, 0.36f, 1f);
            colors.pressedColor = new Color(0.4f, 0.4f, 0.5f, 1f);
            button.colors = colors;

            button.onClick.AddListener(() => onClick());

            var element = holder.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = 84f;

            var textHolder = CreateChild("Text", holder);
            Stretch(textHolder);

            var text = textHolder.gameObject.AddComponent<TextMeshProUGUI>();
            text.text = label;
            text.fontSize = 34f;
            text.alignment = TextAlignmentOptions.Center;
            text.color = Color.white;
            text.raycastTarget = false;
            text.margin = new Vector4(24f, 0f, 24f, 0f);

            return button;
        }

        public void ClearChoices()
        {
            choiceButtons.Clear();
            for (int i = choicePanel.childCount - 1; i >= 0; i--)
                UnityEngine.Object.Destroy(choicePanel.GetChild(i).gameObject);
            choicePanel.gameObject.SetActive(false);
        }

        // ---------------------------------------------------------------- transitions

        /// <summary>
        /// Drives a transition; the player advances `t` from 0 to 1.
        ///
        /// The outgoing scene is retained as a ghost layer above the new one, so a
        /// dissolve is a genuine cross-fade rather than a flash of black. A `fade`
        /// still goes out to black and back, and `Pause` simply holds the old scene.
        /// </summary>
        public void SetTransitionProgress(string name, float t)
        {
            t = Mathf.Clamp01(t);

            float ghostAlpha;
            float blackAlpha = 0f;

            switch (name)
            {
                case "pause":
                    // Ren'Py's Pause is NoTransition, which "only displays the NEW
                    // screen for `delay` seconds" — its render draws new_widget. So
                    // the outgoing scene goes at once and the new one is held.
                    ghostAlpha = 0f;
                    break;

                case "fade":
                case "irisin":
                case "irisout":
                    // Out to black by the midpoint, back in by the end.
                    blackAlpha = 1f - Mathf.Abs(t - 0.5f) * 2f;
                    ghostAlpha = t < 0.5f ? 1f : 0f;
                    break;

                default:
                    // Dissolve, moves and wipes all read as a cross-fade.
                    ghostAlpha = 1f - t;
                    break;
            }

            foreach (var kv in ghosts)
                if (kv.Value != null && kv.Value.Group != null) kv.Value.Group.alpha = ghostAlpha;

            if (fadeOverlay != null)
            {
                bool visible = blackAlpha > 0.001f;
                fadeOverlay.gameObject.SetActive(visible);
                if (visible)
                {
                    fadeOverlay.SetAsLastSibling();
                    fadeImage.color = new Color(0f, 0f, 0f, blackAlpha);
                }
            }

            if (t >= 1f)
            {
                DiscardGhosts();
                if (fadeOverlay != null) fadeOverlay.gameObject.SetActive(false);
            }
        }

        // ---------------------------------------------------------------- colours

        public static Color ParseColor(string value) { return ParseColor(value, Color.white); }

        public static Color ParseColor(string value, Color fallback)
        {
            if (string.IsNullOrEmpty(value)) return fallback;

            string text = value.Trim();
            if (!text.StartsWith("#", StringComparison.Ordinal)) text = "#" + text;

            // Expand #rgb / #rgba shorthand.
            string digits = text.Substring(1);
            if (digits.Length == 3 || digits.Length == 4)
            {
                var sb = new System.Text.StringBuilder("#");
                foreach (char c in digits) sb.Append(c).Append(c);
                text = sb.ToString();
            }

            Color parsed;
            return ColorUtility.TryParseHtmlString(text, out parsed) ? parsed : fallback;
        }
    }
}
