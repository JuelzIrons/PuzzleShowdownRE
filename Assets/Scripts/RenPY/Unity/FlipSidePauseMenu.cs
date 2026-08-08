using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>A pause menu the player can open, save from, and leave.</summary>
    public interface IRenPyPauseMenu
    {
        bool IsOpen { get; }
        void Open();
        void Close();
        void Tick();
        void SetOpenButtonVisible(bool visible);
    }

    /// <summary>
    /// The in-game menu for Class of '09: Flip Side, built from the game's own art.
    ///
    /// Mirrors what the game's `pause_menu` screen does — `gui/nvl.png` behind the
    /// logo, a centred stack of buttons, and the menu press/rollover sounds — using
    /// the real button and slot sprites rather than drawn rectangles.
    ///
    /// It also reproduces `game_pause()`: the voice and ambient channels are paused
    /// while the menu is up, which is what stops a line talking over the menu.
    /// </summary>
    public class FlipSidePauseMenu : IRenPyPauseMenu
    {
        const string Backdrop = "gui/nvl.png";
        const string SaveBackdrop = "gui/overlay/game_menu.png";
        const string LogoArt = "gui/ClassOf09logo.png";

        const string ButtonIdle = "gui/button/choice_idle_background.png";
        const string ButtonHover = "gui/button/choice_hover_background.png";
        const string SlotIdle = "gui/button/slot_idle_background.png";
        const string SlotHover = "gui/button/slot_hover_background.png";

        const string PauseButtonArt = "gui/pausebutton.png";
        const string BackArt = "gui/backbutton.png";

        const string HoverSound = "audio/MainMenuRollover.mp3";
        const string PressSound = "audio/MainMenuPress.mp3";

        /// <summary>Channels the game silences while paused, from its own game_pause().</summary>
        static readonly string[] PausedChannels = { "vo", "ambient" };

        public static bool Matches(RenPyFileSystem files)
        {
            if (files == null || !files.Ready) return false;
            return files.Exists(LogoArt) && files.Exists(ButtonIdle);
        }

        readonly RenPyPlayer player;
        readonly RectTransform root;

        GameObject panel;
        GameObject slotPanel;
        RectTransform openButton;

        public bool IsOpen { get { return panel != null; } }

        public FlipSidePauseMenu(RenPyPlayer player, RectTransform parent)
        {
            this.player = player;

            var go = new GameObject("Flip Side Pause Menu", typeof(RectTransform));
            go.transform.SetParent(parent, false);
            root = (RectTransform)go.transform;
            Stretch(root);

            BuildOpenButton();
        }

        void Raise() { root.SetAsLastSibling(); }

        // ---------------------------------------------------------------- opener

        void BuildOpenButton()
        {
            var texture = player.LoadTextureFor(PauseButtonArt);

            openButton = Child("pause button", root);
            openButton.anchorMin = openButton.anchorMax = new Vector2(1f, 1f);
            openButton.pivot = new Vector2(1f, 1f);
            openButton.anchoredPosition = new Vector2(-28f, -28f);

            if (texture != null)
            {
                openButton.sizeDelta = new Vector2(texture.width * 0.6f, texture.height * 0.6f);

                var image = openButton.gameObject.AddComponent<RawImage>();
                image.texture = texture;

                var button = openButton.gameObject.AddComponent<Button>();
                button.transition = Selectable.Transition.None;
                button.targetGraphic = image;
                button.onClick.AddListener(Open);

                Hover(openButton.gameObject, image, texture, null);
                return;
            }

            // Fall back to a plain glyph if the art is missing.
            openButton.sizeDelta = new Vector2(64f, 64f);
            var background = openButton.gameObject.AddComponent<Image>();
            background.color = new Color(0f, 0f, 0f, 0.45f);

            var plain = openButton.gameObject.AddComponent<Button>();
            plain.targetGraphic = background;
            plain.onClick.AddListener(Open);

            var labelRect = Child("text", openButton);
            Stretch(labelRect);
            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = "≡";
            label.fontSize = 40f;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        public void SetOpenButtonVisible(bool visible)
        {
            if (openButton != null) openButton.gameObject.SetActive(visible);
        }

        public void Tick()
        {
            if (!RenPyInput.EscapePressed()) return;

            if (slotPanel != null) { CloseSlots(); return; }
            if (IsOpen) Close(); else Open();
        }

        // ---------------------------------------------------------------- menu

        public void Open()
        {
            if (IsOpen) return;

            Raise();
            SetGameAudioPaused(true);

            panel = new GameObject("pause", typeof(RectTransform));
            panel.transform.SetParent(root, false);
            Stretch((RectTransform)panel.transform);

            AddBackdrop(panel.transform, Backdrop, 0.72f);
            AddLogo(panel.transform, 0.075f, 0.2f);

            var list = Child("buttons", panel.transform);
            list.anchorMin = list.anchorMax = new Vector2(0.5f, 0.5f);
            list.pivot = new Vector2(0.5f, 0.5f);
            list.sizeDelta = new Vector2(800f, 470f);
            list.anchoredPosition = new Vector2(0f, -60f);

            var layout = list.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10f;
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = false;
            layout.childControlWidth = false;

            AddButton(list, "Resume", Close);
            AddButton(list, "Save", () => ShowSlots(true));
            AddButton(list, "Load", () => ShowSlots(false));
            AddButton(list, "Main Menu", () => { Close(); player.AbortToTitle(); });
            AddButton(list, "Quit", () => player.QuitGame());
        }

        public void Close()
        {
            CloseSlots();

            if (panel != null)
            {
                UnityEngine.Object.Destroy(panel);
                panel = null;
            }

            SetGameAudioPaused(false);
        }

        /// <summary>Matches the game's own game_pause(): silence voice and ambience.</summary>
        void SetGameAudioPaused(bool paused)
        {
            if (player.Audio == null) return;
            foreach (var channel in PausedChannels) player.Audio.SetPaused(channel, paused);
        }

        // ---------------------------------------------------------------- slots

        void ShowSlots(bool saving)
        {
            CloseSlots();
            Raise();

            slotPanel = new GameObject("slots", typeof(RectTransform));
            slotPanel.transform.SetParent(root, false);
            Stretch((RectTransform)slotPanel.transform);

            AddBackdrop(slotPanel.transform, SaveBackdrop, 0.88f);
            AddHeading(slotPanel.transform, saving ? "SAVE" : "LOAD", new Vector2(0.5f, 0.11f));

            // The game's slot art is 414x309, which lays out as a 3x3 page.
            var grid = Child("grid", slotPanel.transform);
            grid.anchorMin = grid.anchorMax = new Vector2(0.5f, 0.5f);
            grid.pivot = new Vector2(0.5f, 0.5f);
            grid.sizeDelta = new Vector2(1330f, 830f);
            grid.anchoredPosition = new Vector2(0f, -10f);

            var layout = grid.gameObject.AddComponent<GridLayoutGroup>();
            layout.cellSize = new Vector2(414f * 0.72f, 309f * 0.72f);
            layout.spacing = new Vector2(22f, 20f);
            layout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layout.constraintCount = 3;
            layout.childAlignment = TextAnchor.MiddleCenter;

            foreach (var info in RenPySaveSystem.List(player.GamePath))
                AddSlot(grid, info, saving);

            AddBackButton(slotPanel.transform, new Vector2(0.5f, 0.94f), CloseSlots);
        }

        void AddSlot(RectTransform parent, SaveSlotInfo info, bool saving)
        {
            var idle = player.LoadTextureFor(SlotIdle);
            var hover = player.LoadTextureFor(SlotHover);

            var cell = Child("slot" + info.Slot, parent);

            RawImage image = null;
            if (idle != null)
            {
                image = cell.gameObject.AddComponent<RawImage>();
                image.texture = idle;
            }
            else
            {
                var background = cell.gameObject.AddComponent<Image>();
                background.color = new Color(0.15f, 0.15f, 0.2f, 0.95f);
            }

            var textRect = Child("text", cell);
            Stretch(textRect);

            var label = textRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = info.Occupied
                ? "<b>" + info.Slot + "</b>\n" + info.SavedAt +
                  (string.IsNullOrEmpty(info.LabelHint) ? "" : "\n" + info.LabelHint) +
                  (string.IsNullOrEmpty(info.Caption) ? "" : "\n\n<size=18>" + info.Caption + "</size>")
                : "<b>" + info.Slot + "</b>\n\nempty";
            label.fontSize = 22f;
            label.color = info.Occupied ? Color.white : new Color(0.6f, 0.6f, 0.65f);
            label.alignment = TextAlignmentOptions.Center;
            label.margin = new Vector4(16f, 14f, 16f, 14f);
            label.raycastTarget = false;

            if (!saving && !info.Occupied) return;

            var button = cell.gameObject.AddComponent<Button>();
            button.transition = Selectable.Transition.None;
            if (image != null) button.targetGraphic = image;
            else button.targetGraphic = cell.GetComponent<Image>();

            int slot = info.Slot;
            button.onClick.AddListener(() =>
            {
                player.PlayInterfaceSound(PressSound);

                if (saving)
                {
                    // Rebuild so the slot shows its new contents straight away.
                    if (player.SaveToSlot(slot)) ShowSlots(true);
                    return;
                }

                CloseSlots();
                Close();
                player.LoadFromSlotInGame(slot);
            });

            if (image != null) Hover(cell.gameObject, image, idle, hover);
        }

        void CloseSlots()
        {
            if (slotPanel == null) return;
            UnityEngine.Object.Destroy(slotPanel);
            slotPanel = null;
        }

        // ---------------------------------------------------------------- pieces

        void AddBackdrop(Transform parent, string art, float dimAlpha)
        {
            var dim = Child("dim", parent);
            Stretch(dim);
            var dimImage = dim.gameObject.AddComponent<Image>();
            dimImage.color = new Color(0f, 0f, 0f, dimAlpha);

            var texture = player.LoadTextureFor(art);
            if (texture == null) return;

            var rect = Child("backdrop", parent);
            Stretch(rect);

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;
            image.raycastTarget = false;
        }

        void AddLogo(Transform parent, float scale, float yAnchor)
        {
            var texture = player.LoadTextureFor(LogoArt);
            if (texture == null) return;

            var rect = Child("logo", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 1f - yAnchor);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(texture.width * scale, texture.height * scale);

            var image = rect.gameObject.AddComponent<RawImage>();
            image.texture = texture;
            image.raycastTarget = false;
        }

        void AddHeading(Transform parent, string text, Vector2 anchor)
        {
            var rect = Child("heading", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(900f, 90f);

            var label = rect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 52f;
            label.fontStyle = FontStyles.Bold;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        void AddButton(RectTransform parent, string text, Action onClick)
        {
            var idle = player.LoadTextureFor(ButtonIdle);
            var hover = player.LoadTextureFor(ButtonHover);

            var rect = Child("button " + text, parent);

            RawImage image = null;
            if (idle != null)
            {
                rect.sizeDelta = new Vector2(idle.width, idle.height);
                image = rect.gameObject.AddComponent<RawImage>();
                image.texture = idle;
            }
            else
            {
                rect.sizeDelta = new Vector2(640f, 70f);
                var background = rect.gameObject.AddComponent<Image>();
                background.color = new Color(0.19f, 0.19f, 0.25f, 0.96f);
            }

            var element = rect.gameObject.AddComponent<LayoutElement>();
            element.preferredWidth = rect.sizeDelta.x;
            element.preferredHeight = rect.sizeDelta.y;

            var button = rect.gameObject.AddComponent<Button>();
            button.transition = Selectable.Transition.None;
            button.targetGraphic = image != null ? (Graphic)image : rect.GetComponent<Image>();
            button.onClick.AddListener(() =>
            {
                player.PlayInterfaceSound(PressSound);
                onClick();
            });

            var labelRect = Child("text", rect);
            Stretch(labelRect);

            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 34f;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;

            if (image != null) Hover(rect.gameObject, image, idle, hover);
        }

        void AddBackButton(Transform parent, Vector2 anchor, Action onClick)
        {
            var texture = player.LoadTextureFor(BackArt);

            var rect = Child("back", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);

            if (texture != null)
            {
                rect.sizeDelta = new Vector2(texture.width, texture.height);

                var image = rect.gameObject.AddComponent<RawImage>();
                image.texture = texture;

                var button = rect.gameObject.AddComponent<Button>();
                button.transition = Selectable.Transition.None;
                button.targetGraphic = image;
                button.onClick.AddListener(() =>
                {
                    player.PlayInterfaceSound(PressSound);
                    onClick();
                });

                Hover(rect.gameObject, image, texture, null);
                return;
            }

            AddButton((RectTransform)rect, "Back", onClick);
        }

        /// <summary>Swaps art and plays the rollover sound while the pointer is over.</summary>
        void Hover(GameObject go, RawImage image, Texture2D idle, Texture2D hover)
        {
            var trigger = go.AddComponent<EventTrigger>();

            var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            enter.callback.AddListener(_ =>
            {
                if (hover != null) image.texture = hover;
                player.PlayInterfaceSound(HoverSound);
            });
            trigger.triggers.Add(enter);

            var exit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            exit.callback.AddListener(_ =>
            {
                if (idle != null) image.texture = idle;
            });
            trigger.triggers.Add(exit);
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

    /// <summary>
    /// Escape detection that works with whichever input backend the project uses.
    /// Kept in one place so the menus do not each need the conditional compilation.
    /// </summary>
    public static class RenPyInput
    {
        public static bool EscapePressed()
        {
#if ENABLE_INPUT_SYSTEM
            var keyboard = UnityEngine.InputSystem.Keyboard.current;
            return keyboard != null && keyboard.escapeKey.wasPressedThisFrame;
#elif ENABLE_LEGACY_INPUT_MANAGER
            return Input.GetKeyDown(KeyCode.Escape);
#else
            return false;
#endif
        }
    }
}
