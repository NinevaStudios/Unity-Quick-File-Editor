#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.Stickies
{
    [InitializeOnLoad]
    public static class Stickies
    {
        public enum ViewType
        {
            Project
        }

        static Stickies()
        {
            EditorApplication.projectWindowItemOnGUI += AddRevealerIconToProjectView;
            EditorApplication.hierarchyWindowChanged += RefreshHierarchy;
            RefreshHierarchy();
        }

        static void AddRevealerIconToProjectView(string guid, Rect selectionRect)
        {
            AddEditButton(guid, selectionRect, ViewType.Project);
            EditorApplication.RepaintProjectWindow();
        }

        static void AddEditButton(string guid, Rect rect, ViewType viewType)
        {
            var iconRect = StickiesGUI.GetProjectViewIconRect(rect, viewType);

            var isInFocus = rect.HasMouseInside();
            if (isInFocus)
            {
                DrawEditButton(iconRect, guid);
            }
        }

        static void DrawEditButton(Rect iconRect, string guid)
        {
            if (GUI.Button(iconRect, string.Empty, GUI.skin.button))
            {
                ShowNote(iconRect, guid);
            }
            GUI.Label(iconRect, "E", Assets.Styles.PlusLabel);
        }

        static void ShowNote(Rect iconRect, string guid)
        {
            PopupWindow.Show(iconRect, new StickyNoteContent(guid));
        }

        #region hierarchy

        static void RefreshHierarchy()
        {
            if (!StickiesEditorSettings.EnableHierarchyStickies)
            {
                return;
            }

            HierarchyObjectIdTools.Refresh();
        }


        #endregion
    }
}
#endif