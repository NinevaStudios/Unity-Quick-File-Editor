#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace DeadMosquito.QuickEditor
{
    public class StickyNoteContent : PopupWindowContent
    {
        enum Mode
        {
            Default = 0,
            ColorPicker = 1
        }

        const float DefaultSize = 320f;

        #region gui_elements
        INoteGUIElement _headerGui;
        INoteGUIElement _colorPicker;
        INoteGUIElement _textArea;
        #endregion

        #region note_persisted_properties
        readonly string _guid;
        NoteData _noteData;
        #endregion

        bool _deleted;
        Mode _mode = Mode.Default;

        #region init
        public StickyNoteContent(string guid)
        {
            _guid = guid;
            Init();
            InitGui();
        }

        void InitGui()
        {
            _headerGui = new NoteHeader(OnPickColor, OnDelete);
            _colorPicker = new NoteColorPicker(OnColorSelected);
            _textArea = new NoteTextArea(_noteData.text, OnTextUpdated, IsInDefaultMode);
        }

        bool IsInDefaultMode()
        {
           return _mode == Mode.Default;
        }

        void Init()
        {
            _noteData = new NoteData(_guid);
        }

        #endregion

        public override Vector2 GetWindowSize()
        {
            return new Vector2(DefaultSize, DefaultSize);
        }

        public override void OnGUI(Rect rect)
        {
            var c = Colors.ColorById(_noteData.color);
            _textArea.OnGUI(rect, c);

            if (IsInDefaultMode())
            {
                _headerGui.OnGUI(rect, c);
            }
            if (_mode == Mode.ColorPicker)
            {
                _colorPicker.OnGUI(rect, c);
            }

            editorWindow.Repaint();
        }

        void OnTextUpdated(string text)
        {
            _noteData.text = text;
        }

        void OnPickColor()
        {
             _mode = Mode.ColorPicker;
        }

        void OnDelete()
        {
            _deleted = true;
            editorWindow.Close();
        }

        void OnColorSelected(NoteColor color)
        {
            _noteData.color = color;
            _mode = Mode.Default;
        }

        #region callbacks
        public override void OnOpen()
        {

        }

        public override void OnClose()
        {

        }

        void Persist()
        {
        }
        #endregion
    }
}

#endif