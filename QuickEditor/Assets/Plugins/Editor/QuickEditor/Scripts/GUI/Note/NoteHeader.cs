#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public sealed class NoteHeader
	{
		public const float Height = 32f;

		readonly Action _onCloseBtnClick;
		readonly Action _onSaveBtnClick;
		readonly Action _onRestore;
		readonly bool _isFileTooBig;

		public NoteHeader(bool isFileTooBig, Action onClose, Action onSave, Action onRestore)
		{
			_onCloseBtnClick = onClose;
			_onSaveBtnClick = onSave;
			_onRestore = onRestore;
			_isFileTooBig = isFileTooBig;
		}

		public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
		{
			var headerRect = GetHeaderRect(rect);
			QuickEditorGUI.ColorRect(headerRect, colors.header, Color.clear);

			if (!_isFileTooBig)
			{
			DrawSaveButton(headerRect);
			DrawRestoreButton(headerRect);
			}
			DrawColorPickerButton(headerRect);
		}

		void DrawRestoreButton(Rect headerRect)
		{
			if (RestoreButton(headerRect))
			{
				_onRestore();
			}
		}

		void DrawSaveButton(Rect headerRect)
		{
			if (SaveButton(headerRect))
			{
				_onSaveBtnClick();
			}
		}

		void DrawColorPickerButton(Rect headerRect)
		{
			if (CloseButton(headerRect))
			{
				_onCloseBtnClick();
			}
		}

		static bool SaveButton(Rect headerRect)
		{
			return QuickEditorGUI.TextureButton(GetSaveBtnRect(headerRect), Assets.Textures.SaveTexture, "Save changes and overwrite file");
		}

		static bool CloseButton(Rect headerRect)
		{
			return QuickEditorGUI.TextureButton(GetCloseBtnRect(headerRect), Assets.Textures.CloseTexture, "Close editor");
		}
		
		static bool RestoreButton(Rect headerRect)
		{
			return QuickEditorGUI.TextureButton(GetRestoreBtnRect(headerRect), Assets.Textures.RestoreTexture, "Restore original text");
		}

		#region rects

		static Rect GetHeaderRect(Rect noteRect)
		{
			var headerRect = new Rect(noteRect.x, noteRect.y, noteRect.width, Height);
			return headerRect;
		}

		static Rect GetSaveBtnRect(Rect headerRect)
		{
			return new Rect(headerRect.width - headerRect.height, headerRect.y, headerRect.height, headerRect.height);
		}

		static Rect GetCloseBtnRect(Rect headerRect)
		{
			return new Rect(0, headerRect.y, headerRect.height, headerRect.height);
		}
		
		static Rect GetRestoreBtnRect(Rect headerRect)
		{
			return new Rect(headerRect.width - 2 * headerRect.height, headerRect.y, headerRect.height, headerRect.height);
		}

		#endregion
	}
}

#endif