#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System;
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	public class QuickEditorPopupWindowContent : PopupWindowContent
	{
		const int CharacterLimit = 65000;
		
		readonly INoteGUIElement _headerGui;
		readonly INoteGUIElement _textArea;
		readonly string _filePath;

		readonly string _originalText;
		string _currentText = string.Empty;

		public QuickEditorPopupWindowContent(string guid)
		{
			_filePath = AssetDatabase.GUIDToAssetPath(guid);
			_originalText = File.ReadAllText(_filePath);
			var isTooLarge = _originalText.Length > CharacterLimit;
			if (isTooLarge)
			{
				_originalText = "This file is too large. Unfortunately Unity allows only 65K characters in the editor text area";
			}

			_headerGui = new NoteHeader(OnCloseButtonClick, OnSaveButtonClick);
			_textArea = isTooLarge ? NoteTextArea.CreateTooMuchText() : NoteTextArea.Create(_originalText, OnTextUpdated);
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(QuickEditorEditorSettings.WindowWidth, QuickEditorEditorSettings.WindowHeight);
		}

		public override void OnGUI(Rect rect)
		{
			var c = Colors.ColorById(NoteColor.Clean);
			_textArea.OnGUI(rect, c);
			_headerGui.OnGUI(rect, c);

			editorWindow.Repaint();
		}

		void OnTextUpdated(string text)
		{
			_currentText = text;
		}

		void OnCloseButtonClick()
		{
			editorWindow.Close();
		}

		void OnSaveButtonClick()
		{
			editorWindow.Close();
			File.WriteAllText(_filePath, _currentText);
			AssetDatabase.Refresh();
		}

		public override void OnOpen()
		{
		}

		public override void OnClose()
		{
		}
	}
}

#endif