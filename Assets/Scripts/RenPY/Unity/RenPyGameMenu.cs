using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RenPy.Runtime;

namespace RenPy.Unity
{
    /// <summary>
    /// The in-game menu: resume, save, load, return to the title, quit.
    ///
    /// Ren'Py games ship their own pause menu as a screen, but those depend on the
    /// style system to look right. This is a plain, game-agnostic menu so saving is
    /// reachable mid-game in every game the player loads.
    ///
    /// It opens on Escape or from an always-visible button, so it also works on
    /// touch and, later, from a VR ray interactor.
    /// </summary>
    public class RenPyGameMenu : IRenPyPauseMenu
    {
        readonly RenPyPlayer player;
        readonly RectTransform root;

        GameObject panel;
        GameObject slotPanel;
        RectTransform openButton;

        public bool IsOpen { get { return panel != null; } }

        public RenPyGameMenu(RenPyPlayer player, RectTransform parent)
        {
            this.player = player;

            var go = new GameObject("RenPy Game Menu", typeof(RectTransform));
            go.transform.SetParent(parent, false);
            root = (RectTransform)go.transform;
            Stretch(root);

            BuildOpenButton();
        }

        /// <summary>Keeps the menu above everything the game draws.</summary>
        void Raise() { root.SetAsLastSibling(); }

        // ---------------------------------------------------------------- entry point

        void BuildOpenButton()
        {
            openButton = Child("menu button", root);
            openButton.anchorMin = openButton.anchorMax = new Vector2(1f, 1f);
            openButton.pivot = new Vector2(1f, 1f);
            openButton.sizeDelta = new Vector2(64f, 64f);
            openButton.anchoredPosition = new Vector2(-24f, -24f);

            var background = openButton.gameObject.AddComponent<Image>();
            background.color = new Color(0f, 0f, 0f, 0.45f);

            var button = openButton.gameObject.AddComponent<Button>();
            button.targetGraphic = background;
            button.onClick.AddListener(Open);

            var labelRect = Child("text", openButton);
            Stretch(labelRect);

            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = "≡";                 // hamburger
            label.fontSize = 40f;
            label.color = new Color(1f, 1f, 1f, 0.8f);
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        /// <summary>Shows or hides the small opener; hidden while the title screen is up.</summary>
        public void SetOpenButtonVisible(bool visible)
        {
            if (openButton != null) openButton.gameObject.SetActive(visible);
        }

        /// <summary>Polls for Escape. Called each frame by the player.</summary>
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

            panel = new GameObject("menu", typeof(RectTransform));
            panel.transform.SetParent(root, false);
            Stretch((RectTransform)panel.transform);

            // Blocks clicks reaching the dialogue advance area underneath.
            var dim = Child("dim", panel.transform);
            Stretch(dim);
            var dimImage = dim.gameObject.AddComponent<Image>();
            dimImage.color = new Color(0f, 0f, 0f, 0.78f);

            AddLabel(panel.transform, "Paused", 58f, new Vector2(0.5f, 0.2f), 1200f);

            var list = Child("buttons", panel.transform);
            list.anchorMin = list.anchorMax = new Vector2(0.5f, 0.5f);
            list.pivot = new Vector2(0.5f, 0.5f);
            list.sizeDelta = new Vector2(460f, 460f);

            var layout = list.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 14f;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childControlWidth = true;

            AddMenuButton(list, "Resume", Close);
            AddMenuButton(list, "Save", () => ShowSlots(true));
            AddMenuButton(list, "Load", () => ShowSlots(false));

            if (player.TitleScreen != null)
                AddMenuButton(list, "Main Menu", () => { Close(); player.AbortToTitle(); });

            AddMenuButton(list, "Quit", () => player.QuitGame());
        }

        public void Close()
        {
            CloseSlots();

            if (panel == null) return;
            UnityEngine.Object.Destroy(panel);
            panel = null;
        }

        // ---------------------------------------------------------------- slots

        void ShowSlots(bool saving)
        {
            CloseSlots();
            Raise();

            slotPanel = new GameObject("slots", typeof(RectTransform));
            slotPanel.transform.SetParent(root, false);
            Stretch((RectTransform)slotPanel.transform);

            var dim = Child("dim", slotPanel.transform);
            Stretch(dim);
            var dimImage = dim.gameObject.AddComponent<Image>();
            dimImage.color = new Color(0f, 0f, 0f, 0.9f);

            AddLabel(slotPanel.transform, saving ? "Save" : "Load", 56f, new Vector2(0.5f, 0.12f), 1200f);

            var list = Child("list", slotPanel.transform);
            list.anchorMin = list.anchorMax = new Vector2(0.5f, 0.5f);
            list.pivot = new Vector2(0.5f, 0.5f);
            list.sizeDelta = new Vector2(1100f, 640f);

            var layout = list.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.spacing = 10f;
            layout.childForceExpandHeight = false;
            layout.childControlHeight = false;
            layout.childForceExpandWidth = true;
            layout.childControlWidth = true;

            foreach (var info in RenPySaveSystem.List(player.GamePath))
                AddSlotRow(list, info, saving);

            AddMenuButtonAt(slotPanel.transform, "Back", new Vector2(0.5f, 0.93f), CloseSlots);
        }

        void AddSlotRow(RectTransform parent, SaveSlotInfo info, bool saving)
        {
            var row = Child("slot" + info.Slot, parent);

            var background = row.gameObject.AddComponent<Image>();
            background.color = info.Occupied
                ? new Color(0.17f, 0.17f, 0.22f, 0.96f)
                : new Color(0.09f, 0.09f, 0.12f, 0.96f);

            var element = row.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = 60f;

            string text = info.Occupied
                ? info.Slot + ".  " + info.SavedAt +
                  (string.IsNullOrEmpty(info.LabelHint) ? "" : "   [" + info.LabelHint + "]") +
                  (string.IsNullOrEmpty(info.Caption) ? "" : "   " + info.Caption)
                : info.Slot + ".  empty";

            var labelRect = Child("text", row);
            Stretch(labelRect);

            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 25f;
            label.color = info.Occupied ? Color.white : new Color(0.55f, 0.55f, 0.6f);
            label.alignment = TextAlignmentOptions.Left;
            label.margin = new Vector4(20f, 0f, 20f, 0f);
            label.raycastTarget = false;

            // An empty slot can be saved into but not loaded from.
            if (!saving && !info.Occupied) return;

            var button = row.gameObject.AddComponent<Button>();
            button.targetGraphic = background;

            int slot = info.Slot;
            button.onClick.AddListener(() =>
            {
                if (saving)
                {
                    bool ok = player.SaveToSlot(slot);
                    // Rebuild so the slot immediately shows its new contents.
                    if (ok) ShowSlots(true);
                    return;
                }

                CloseSlots();
                Close();
                player.LoadFromSlotInGame(slot);
            });
        }

        void CloseSlots()
        {
            if (slotPanel == null) return;
            UnityEngine.Object.Destroy(slotPanel);
            slotPanel = null;
        }

        // ---------------------------------------------------------------- helpers

        void AddMenuButton(RectTransform parent, string text, Action onClick)
        {
            var rect = Child("button " + text, parent);

            var background = rect.gameObject.AddComponent<Image>();
            background.color = new Color(0.19f, 0.19f, 0.25f, 0.96f);

            var element = rect.gameObject.AddComponent<LayoutElement>();
            element.preferredHeight = 68f;

            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = background;
            button.onClick.AddListener(() => onClick());

            var labelRect = Child("text", rect);
            Stretch(labelRect);

            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 30f;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        void AddMenuButtonAt(Transform parent, string text, Vector2 anchor, Action onClick)
        {
            var rect = Child("button " + text, parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(260f, 62f);

            var background = rect.gameObject.AddComponent<Image>();
            background.color = new Color(0.19f, 0.19f, 0.25f, 0.96f);

            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = background;
            button.onClick.AddListener(() => onClick());

            var labelRect = Child("text", rect);
            Stretch(labelRect);

            var label = labelRect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = 28f;
            label.color = Color.white;
            label.alignment = TextAlignmentOptions.Center;
            label.raycastTarget = false;
        }

        void AddLabel(Transform parent, string text, float size, Vector2 anchor, float width)
        {
            var rect = Child("label", parent);
            rect.anchorMin = rect.anchorMax = new Vector2(anchor.x, 1f - anchor.y);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(width, 120f);

            var label = rect.gameObject.AddComponent<TextMeshProUGUI>();
            label.text = text;
            label.fontSize = size;
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
}
