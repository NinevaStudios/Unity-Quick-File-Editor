#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public sealed class NoteTextArea : INoteGUIElement
	{
		readonly Action<string> _onTextUpdated;

		Vector2 _scroll = Vector2.zero;
		string _text = string.Empty;

		public NoteTextArea(string initialText, Action<string> onTextUpdated)
		{
			_text = initialText;
			_onTextUpdated = onTextUpdated;
		}

		public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
		{
			DrawNoteBackground(rect, colors.main);

						
			GUILayout.BeginArea(GetTextAreaRect(rect));
			EditorGUILayout.BeginVertical();
			GUI.skin = Assets.Styles.Skin;

			_scroll = EditorGUILayout.BeginScrollView(_scroll);
			EditorGUI.BeginChangeCheck();
			Assets.Styles.TextArea.fontSize = QuickEditorEditorSettings.FontSize;
			_text = EditorGUILayout.TextArea(_text, Assets.Styles.TextArea);

			if (EditorGUI.EndChangeCheck())
			{
				Debug.Log("CHANGED");
				Undo.CreateSnapshot();
				Undo.RegisterSnapshot();
				_onTextUpdated(_text);
			}
			EditorGUILayout.EndScrollView();

			GUI.skin = null;

			EditorGUILayout.EndVertical();
			GUILayout.EndArea();

			GUI.enabled = true;
		}

		void DrawNoteBackground(Rect rect, Color backgroundColor)
		{
			QuickEditorGUI.ColorRect(rect, backgroundColor, Color.clear);
		}

		static Rect GetTextAreaRect(Rect noteRect)
		{
			const float h = NoteHeader.Height;
			return new Rect(noteRect.x, noteRect.y + h, noteRect.width, noteRect.height - h);
		}
	}
}
#endif