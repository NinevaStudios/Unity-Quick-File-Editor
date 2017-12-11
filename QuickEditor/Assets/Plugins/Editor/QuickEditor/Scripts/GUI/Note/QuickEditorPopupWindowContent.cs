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

		readonly NoteHeader _header;
		readonly NoteTextArea _textArea;
		readonly string _filePath;

		readonly string _originalText;
		string _currentText;

		public QuickEditorPopupWindowContent(string guid)
		{
			_filePath = AssetDatabase.GUIDToAssetPath(guid);
			_originalText = File.ReadAllText(_filePath);
			_currentText = _originalText;

			var isTooLarge = _originalText.Length > CharacterLimit;
			if (isTooLarge)
			{
				_originalText = "This file is too large. Unfortunately Unity allows only 65K characters in the editor text area";
			}

			_header = new NoteHeader(isTooLarge, OnCloseButtonClick, OnSaveButtonClick, OnRestoreButtonClick);
			_textArea = isTooLarge ? NoteTextArea.CreateTooMuchText() : NoteTextArea.Create(_originalText, OnTextUpdated);
		}

		public override Vector2 GetWindowSize()
		{
			return new Vector2(QuickEditorEditorSettings.WindowWidth, QuickEditorEditorSettings.WindowHeight);
		}

		public override void OnGUI(Rect rect)
		{
			var c = Colors.ColorById(QuickEditorEditorSettings.EditorColor);
			_textArea.OnGUI(rect, c);
			_header.OnGUI(rect, c);

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

		void OnRestoreButtonClick()
		{
			_currentText = _originalText;
			_textArea.Text = _currentText;
		}

		void OnSaveButtonClick()
		{
			editorWindow.Close();

			if (_currentText == _originalText)
			{
				return;
			}

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