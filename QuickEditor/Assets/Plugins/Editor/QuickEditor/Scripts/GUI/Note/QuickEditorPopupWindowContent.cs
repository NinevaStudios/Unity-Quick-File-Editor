#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	public class QuickEditorPopupWindowContent : PopupWindowContent
	{
		const float DefaultSize = 320f;

		public override Vector2 GetWindowSize()
		{
			return new Vector2(720, 400);
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
		}

		void OnDelete()
		{
			editorWindow.Close();
		}

		#region gui_elements

		readonly INoteGUIElement _headerGui;
		readonly INoteGUIElement _textArea;

		#endregion

		#region note_persisted_properties

		readonly string _guid;
		const int CharacterLimit = 65000;

		#endregion

		#region init

		public QuickEditorPopupWindowContent(string guid)
		{
			_guid = guid;

			var text = File.ReadAllText(AssetDatabase.GUIDToAssetPath(guid));
			if (text.Length > CharacterLimit)
			{
				text = "This file is too large. Unfortunately Unity allows only 65K characters in the editor text area";
			}

			_headerGui = new NoteHeader(OnDelete);
			_textArea = new NoteTextArea(text, OnTextUpdated);
		}

		#endregion

		#region callbacks

		public override void OnOpen()
		{
		}

		public override void OnClose()
		{
		}

		#endregion
	}
}

#endif