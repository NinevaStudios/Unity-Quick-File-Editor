#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public sealed class NoteTextArea
	{
		readonly Action<string> _onTextUpdated;

		Vector2 _scroll = Vector2.zero;
		readonly bool _isTooMuchText;
		string _text;
		
		public string Text
		{
			set { _text = value; }
		}

		public static NoteTextArea CreateTooMuchText()
		{
			return new NoteTextArea(true, "This file is too large. Unfortunately Unity allows only 65K characters in the editor text area", null);
		}
		
		public static NoteTextArea Create(string initialText, Action<string> onTextUpdated)
		{
			return new NoteTextArea(false, initialText, onTextUpdated);
		}

		NoteTextArea(bool isTooMuchText, string initialText, Action<string> onTextUpdated)
		{
			_isTooMuchText = isTooMuchText;
			_text = initialText;
			_onTextUpdated = onTextUpdated;
		}

		public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
		{
			DrawNoteBackground(rect, colors.main);

			if (_isTooMuchText)
			{
				GUI.enabled = false;
			}

			GUILayout.BeginArea(GetTextAreaRect(rect));
			EditorGUILayout.BeginVertical();
			GUI.skin = Assets.Styles.Skin;

			_scroll = EditorGUILayout.BeginScrollView(_scroll);
			EditorGUI.BeginChangeCheck();
			var textAreaTextStyle = _isTooMuchText ? Assets.Styles.TooBigMessageText : Assets.Styles.TextArea;
			textAreaTextStyle.fontSize = QuickEditorEditorSettings.FontSize;
			_text = EditorGUILayout.TextArea(_text, textAreaTextStyle);

			if (EditorGUI.EndChangeCheck())
			{
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