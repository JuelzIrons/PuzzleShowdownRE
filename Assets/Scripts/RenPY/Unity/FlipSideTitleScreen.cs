using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// A hand-built title screen for Class of '09: Flip Side.
    ///
    /// The generic screen-language renderer draws this game's `main_menu`, but
    /// without Ren'Py's style system the result does not match the original. This
    /// reproduces the intended layout directly in uGUI using the game's own art, and
    /// adds working save slots on top.
    ///
    /// It is deliberately game-specific: <see cref="Matches"/> fingerprints the game
    /// and the player falls back to the generic renderer for anything else.
    /// </summary>
    public class FlipSideTitleScreen
    {
        // Positions taken from the game's own main_menu screen.
        static readonly (string Art, string Label, float X, float Y)[] Buttons =
        {
            ("gui/menu 2/newgame2.png",  "new",      520f, 140f),
            ("gui/menu 2/continue2.png", "continue", 1000f, 140f),
            ("gui/menu 2/about2.png",    "about",     300f, 480f),
            ("gui/menu 2/options2.png",  "options",  1250f, 480f),
            ("gui/menu 2/exit2.png",     "exit",      790f, 850f),
        };

        const string LogoArt = "gui/ClassOf09logo.png";
        const string TrackerArt = "gui/ending_tracker/text_access.png";
        const string BackgroundVideo = "gui/menu 2/BG living.webm";
        const string BackgroundStill = "gui/menu 2/menu_bg.png";
        const string HoverSound = "audio/MainMenuRollover.mp3";
        const string PressSound = "audio/MainMenuPress.mp3";

        // Panel dressing, also taken from the game rather than drawn by hand.
        const string PanelArt = "gui/game_menu.png";
        const string RowArt = "gui/frame.png";
        const string BackArt = "gui/backbutton.png";

        /// <summary>
        /// True when the loaded game is Flip Side. Checks for art unique to it rather
        /// than a title string, which games routinely leave at the template default.
        /// </summary>
        public static bool Matches(RenPyFileSystem files)
        {
            if (files == null || !files.Ready) return false;
            return files.Exists(LogoArt) && files.Exists(BackgroundVideo);
        }

        readonly RenPyPlayer player;
        readonly RectTransform root;

        GameObject panel;
        GameObject savePanel;

        public bool Visible { get { return panel != null && panel.activeSelf; } }

        /// <summary>Raised when the player picks New Game.</summary>
        public Action OnNewGame;

        /// <summary>Raised with the slot number when a save is chosen to load.</summary>
        public Action<int> OnLoadSlot;

        public FlipSideTitleScreen(RenPyPlayer player, RectTransform parent)
        {
            this.player = player;

            var go = new GameObject("Flip Side Title", typeof(RectTransform));
            go.transform.SetParent(parent, false);
            root = (RectTransform)go.transform;
            Stretch(root);
            root.SetAsLastSibling();
        }

        // ---------------------------------------------------------------- show/hide

        public void Show()
        {
            Hide();

            panel = new GameObject("panel", typeof(RectTransform));
            panel.transform.SetParent(root, false);
            Stretch((RectTransform)panel.transform);
            root.SetAsLastSibling();

            BuildBackground();
            BuildLogo();
            BuildButtons();
            BuildTracker();
        }

        public void Hide()
        {
            CloseSavePanel();

            if (panel != null)
            {
                UnityEngine.Object.Destroy(panel);
                panel = null;
            }

            player.Movies.Release("title/bg");
        }

        // ---------------------------------------------------------------- pieces

        void BuildBackground()
        {
            var rect = Child("background", panel.transform);
            Stretch(rect);

            var image = rect.gameObject.AddComponent<RawImage>();
            image.raycastTarget = false;

            // The original backdrop is a looping video; the still is its start_image.
            Texture video = player.Movies.Resolve("title/bg", BackgroundVideo, true);
            image.texture = video ?? (Texture)player.LoadTextureFor(BackgroundStill);

            if (image.texture == null) image.color = Color.black;
        }

        void BuildLogo()
        {
            var texture = player.LoadTextureFor(LogoArt);
            if (texture == null) return;

            var rect = Child("logo", panel.transform);
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);

            // The source art is far larger than the screen, so it is drawn scaled down.
            float scale = Mathf.Max(0.01f, player.TitleLogoScale);
            rect.sizeDelta = new Vector2(texture.width * scale, texture.height * scale);
            rect.anchoredPosition = Vector2.zero;

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;
            image.raycastTarget = false;

            // The original pulses the logo; a gentle scale keeps that character.
            rect.gameObject.AddComponent<TitlePulse>();
        }

        void BuildButtons()
        {
            foreach (var entry in Buttons)
            {
                var texture = player.LoadTextureFor(entry.Art);
                if (texture == null) continue;

                string hoverArt = entry.Art.Replace(".png", "_ro.png");
                var hoverTexture = player.LoadTextureFor(hoverArt);

                string label = entry.Label;
                MakeButton(entry.Art, texture, hoverTexture, entry.X, entry.Y, () => Activate(label));
            }
        }

        void BuildTracker()
        {
            var texture = player.LoadTextureFor(TrackerArt);
            if (texture == null) return;

            var rect = Child("tracker", panel.transform);
            rect.anchorMin = rect.anchorMax = new Vector2(0.96f, 1f - 0.925f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(texture.width, texture.height);
            rect.anchoredPosition = Vector2.zero;

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;

            Wire(rect.gameObject, image, texture, null, () => ShowEndings());
        }

        RectTransform MakeButton(string name, Texture2D idle, Texture2D hover, float x, float y, Action onClick)
        {
            var rect = Child(name, panel.transform);

            // Ren'Py positions from the top-left with y growing downwards.
            rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
            rect.pivot = new Vector2(0f, 1f);
            rect.sizeDelta = new Vector2(idle.width, idle.height);
            rect.anchoredPosition = new Vector2(x, -y);

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = idle;

            Wire(rect.gameObject, image, idle, hover, onClick);
            return rect;
        }

        void Wire(GameObject go, RawImage image, Texture2D idle, Texture2D hover, Action onClick)
        {
            var button = go.AddComponent<Button>();
            button.transition = Selectable.Transition.None;
            button.targetGraphic = image;
            button.onClick.AddListener(() =>
            {
                player.PlayInterfaceSound(PressSound);
                onClick();
            });

            var trigger = go.AddComponent<EventTrigger>();

            AddTrigger(trigger, EventTriggerType.PointerEnter, () =>
            {
                if (hover != null) image.texture = hover;
                player.PlayInterfaceSound(HoverSound);
            });

            AddTrigger(trigger, EventTriggerType.PointerExit, () =>
            {
                if (hover != null) image.texture = idle;
            });
        }

        static void AddTrigger(EventTrigger trigger, EventTriggerType type, Action handler)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener(_ => handler());
            trigger.triggers.Add(entry);
        }

        // ---------------------------------------------------------------- actions

        void Activate(string label)
        {
            switch (label)
            {
                case "new":
                    Hide();
                    if (OnNewGame != null) OnNewGame();
                    return;

                case "continue":
                    ShowSaves(false);
                    return;

                case "options":
                    ShowMessage("Options",
                        "Preferences are not implemented yet.\n\nText speed and skipping can be set on the Ren'Py Player component.");
                    return;

                case "about":
                    ShowMessage("About",
                        "Class of '09: Flip Side\n\nRunning on the Unity Ren'Py player.\n" +
                        "Original game by Wrath Club / SBN3.");
                    return;

                case "exit":
                    player.QuitGame();
                    return;
            }
        }

        void ShowEndings()
        {
            var endings = player.Engine != null
                ? player.Engine.Persistent.GetStr("endings", null) as RenPy.Python.PyList
                : null;

            int unlocked = endings != null ? endings.Length : 0;

            ShowMessage("Endings", "Unlocked " + unlocked + " of 5.\n\n" +
                (unlocked == 0
                    ? "Finish a route to record an ending here."
                    : "Progress is stored between sessions."));
        }

        // ---------------------------------------------------------------- saves

        /// <summary>Shows the slot list. `saving` writes, otherwise it loads.</summary>
        public void ShowSaves(bool saving)
        {
            CloseSavePanel();

            savePanel = new GameObject("saves", typeof(RectTransform));
            savePanel.transform.SetParent(root, false);
            Stretch((RectTransform)savePanel.transform);
            savePanel.transform.SetAsLastSibling();

            AddBackdrop(savePanel.transform, 0.82f);

            AddLabel(savePanel.transform, saving ? "Save" : "Load", 64f, new Vector2(0.5f, 0.12f));

            var list = Child("list", savePanel.transform);
            list.anchorMin = new Vector2(0.5f, 0.5f);
            list.anchorMax = new Vector2(0.5f, 0.5f);
            list.pivot = new Vector2(0.5f, 0.5f);
            list.sizeDelta = new Vector2(1100f, 640f);

            var layout = list.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 12f;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childControlWidth = true;

            foreach (var info in RenPySaveSystem.List(player.GamePath))
                AddSlotRow(list, info, saving);

            AddBackButton(savePanel.transform, new Vector2(0.5f, 0.93f), CloseSavePanel);
        }

        void AddSlotRow(RectTransform parent, SaveSlotInfo info, bool saving)
        {
            var row = Child("slot" + info.Slot, parent);

            var background = row.gameObject.AddComponent<Image>();
            var rowArt = player.LoadTextureFor(RowArt);

            if (rowArt != null)
            {
                background.sprite = Sprite.Create(rowArt, new Rect(0f, 0f, rowArt.width, rowArt.height),
                                                  new Vector2(0.5f, 0.5f));
                background.type = Image.Type.Sliced;
                background.color = info.Occupied ? Color.white : new Color(0.6f, 0.6f, 0.6f);
            }
            else
            {
                background.color = info.Occupied
                    ? new Color(0.16f, 0.16f, 0.2f, 0.95f)
                    : new Color(0.09f, 0.09f, 0.11f, 0.95f);
            }

            var element = row.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = 62f;

            string text = info.Occupied
                ? info.Slot + ".  " + info.SavedAt +
                  (string.IsNullOrEmpty(info.LabelHint) ? "" : "   [" + info.LabelHint + "]") +
                  (string.IsNullOrEmpty(info.Caption) ? "" : "   " + info.Caption)
                : info.Slot + ".  empty";

            var labelRect = Child("text", row);
            Stretch(labelRect);
            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 26f;
            label.color = info.Occupied ? Color.white : new Color(0.55f, 0.55f, 0.6f);
            label.alignment = TextAlignmentOptions.Left;
            label.margin = new Vector4(20f, 0f, 20f, 0f);
            label.raycastTarget = false;

            // Loading an empty slot does nothing; saving into one is fine.
            if (!saving && !info.Occupied) return;

            var button = row.gameObject.AddComponent<Button>();
            button.targetGraphic = background;

            int slot = info.Slot;
            button.onClick.AddListener(() =>
            {
                player.PlayInterfaceSound(PressSound);

                if (saving) { player.SaveToSlot(slot); CloseSavePanel(); return; }

                CloseSavePanel();
                Hide();
                if (OnLoadSlot != null) OnLoadSlot(slot);
            });
        }

        void ShowMessage(string title, string body)
        {
            CloseSavePanel();

            savePanel = new GameObject("message", typeof(RectTransform));
            savePanel.transform.SetParent(root, false);
            Stretch((RectTransform)savePanel.transform);
            savePanel.transform.SetAsLastSibling();

            AddBackdrop(savePanel.transform, 0.85f);

            AddLabel(savePanel.transform, title, 60f, new Vector2(0.5f, 0.28f));
            AddLabel(savePanel.transform, body, 32f, new Vector2(0.5f, 0.5f));
            AddBackButton(savePanel.transform, new Vector2(0.5f, 0.78f), CloseSavePanel);
        }

        public void CloseSavePanel()
        {
            if (savePanel == null) return;
            UnityEngine.Object.Destroy(savePanel);
            savePanel = null;
        }

        // ---------------------------------------------------------------- helpers

        /// <summary>
        /// Dims the title and lays the game's own menu panel over it, so dialogs sit
        /// in the game's art rather than a flat rectangle.
        /// </summary>
        void AddBackdrop(Transform parent, float dimAlpha)
        {
            var dim = Child("dim", parent);
            Stretch(dim);
            var dimImage = dim.gameObject.AddComponent<Image>();
            dimImage.color = new Color(0f, 0f, 0f, dimAlpha);

            var texture = player.LoadTextureFor(PanelArt);
            if (texture == null) return;

            var rect = Child("panel art", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(texture.width, texture.height);
            rect.anchoredPosition = Vector2.zero;

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;
            image.raycastTarget = false;
        }

        /// <summary>A back button drawn from the game's art when it exists.</summary>
        void AddBackButton(Transform parent, Vector2 anchor, Action onClick)
        {
            var texture = player.LoadTextureFor(BackArt);
            if (texture == null) { AddTextButton(parent, "Back", anchor, onClick); return; }

            var rect = Child("back", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(texture.width, texture.height);

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;

            Wire(rect.gameObject, image, texture, null, onClick);
        }

        void AddLabel(Transform parent, string text, float size, Vector2 anchor)
        {
            var rect = Child("label", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(1300f, 220f);

            var label = rect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = size;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        void AddTextButton(Transform parent, string text, Vector2 anchor, Action onClick)
        {
            var rect = Child("button " + text, parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(280f, 66f);

            var background = rect.gameObject.AddComponent<Image>();
            background.color = new Color(0.2f, 0.2f, 0.26f, 0.95f);

            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = background;
            button.onClick.AddListener(() =>
            {
                player.PlayInterfaceSound(PressSound);
                onClick();
            });

            var labelRect = Child("text", rect);
            Stretch(labelRect);
            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 30f;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        static RectTransform Child(string name, Transform parent)
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
    }

    /// <summary>The logo's slow breathing scale, matching the original's zoom_pulse.</summary>
    public class TitlePulse : MonoBehaviour
    {
        public float Amount = 0.02f;
        public float Speed = 1.1f;

        Vector3 baseScale;

        void Awake() { baseScale = transform.localScale; }

        void Update()
        {
            float t = 1f + Mathf.Sin(Time.unscaledTime * Speed) * Amount;
            transform.localScale = baseScale * t;
        }
    }
}
