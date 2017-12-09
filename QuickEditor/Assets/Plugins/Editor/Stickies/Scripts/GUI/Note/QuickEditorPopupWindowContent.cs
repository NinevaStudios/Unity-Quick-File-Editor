#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using UnityEditor;
	using UnityEngine;

	public class QuickEditorPopupWindowContent : PopupWindowContent
	{
		const float DefaultSize = 320f;

		public override Vector2 GetWindowSize()
		{
			return new Vector2(DefaultSize, DefaultSize);
		}

		public override void OnGUI(Rect rect)
		{
			var c = Colors.ColorById(_noteData.color);
			_textArea.OnGUI(rect, c);

			_headerGui.OnGUI(rect, c);

			editorWindow.Repaint();
		}

		void OnTextUpdated(string text)
		{
			_noteData.text = text;
		}

		void OnDelete()
		{
			editorWindow.Close();
		}

		#region gui_elements

		INoteGUIElement _headerGui;
		INoteGUIElement _textArea;

		#endregion

		#region note_persisted_properties

		readonly string _guid;
		NoteData _noteData;

		#endregion

		#region init

		public QuickEditorPopupWindowContent(string guid)
		{
			_guid = guid;
			Init();
			InitGui();
		}

		void InitGui()
		{
			_headerGui = new NoteHeader(OnDelete);
			_textArea = new NoteTextArea(_noteData.text, OnTextUpdated);
		}


		void Init()
		{
			_noteData = new NoteData(_guid);
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