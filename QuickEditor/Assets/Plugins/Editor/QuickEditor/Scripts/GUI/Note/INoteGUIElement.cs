#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using UnityEngine;

	public interface INoteGUIElement
	{
		void OnGUI(Rect rect, Colors.NoteColorCollection colors);
	}
}
#endif