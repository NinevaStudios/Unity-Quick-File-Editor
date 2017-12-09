#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.QuickEditor
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
            EditorApplication.projectWindowItemOnGUI += AddEditIcon;
        }

        static void AddEditIcon(string guid, Rect selectionRect)
        {
            AddEditButton(guid, selectionRect, ViewType.Project);
            EditorApplication.RepaintProjectWindow();
        }

        static void AddEditButton(string guid, Rect rect, ViewType viewType)
        {
            var iconRect = StickiesGUI.GetProjectViewIconRect(rect, viewType);

            var isInFocus = rect.HasMouseInside();
            var isFile = FileUtils.IsFile(AssetDatabase.GUIDToAssetPath(guid));
            
            if (isInFocus && isFile)
            {
                DrawEditButton(iconRect, guid);
            }
        }

        static void DrawEditButton(Rect iconRect, string guid)
        {
            if (GUI.Button(iconRect, string.Empty, GUI.skin.button))
            {
                var guidToAssetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (FileUtils.IsFile(guidToAssetPath))
                {
                    Debug.Log(guidToAssetPath);
                }
                ShowEditor(iconRect, guid);
            }
            GUI.Label(iconRect, "E", Assets.Styles.PlusLabel);
        }

        static void ShowEditor(Rect iconRect, string guid)
        {
            PopupWindow.Show(iconRect, new StickyNoteContent(guid));
        }

        #region hierarchy

        #endregion
    }
}
#endif