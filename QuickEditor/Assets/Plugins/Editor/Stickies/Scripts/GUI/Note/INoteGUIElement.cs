#if UNITY_EDITOR
using UnityEngine;

namespace DeadMosquito.QuickEditor
{
    public interface INoteGUIElement
    {
        void OnGUI(Rect rect, Colors.NoteColorCollection colors);
    }
}
#endif