#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System;
	using UnityEditor;
	using UnityEngine;

	public sealed class NoteHeader : INoteGUIElement
	{
		public const float Height = 32f;

		readonly Action _onDeleteBtnClick;

		public NoteHeader(Action onDelete)
		{
			_onDeleteBtnClick = onDelete;
		}

		public void OnGUI(Rect rect, Colors.NoteColorCollection colors)
		{
			var headerRect = GetHeaderRect(rect);
			QuickEditorGUI.ColorRect(headerRect, colors.header, Color.clear);

			DrawSaveButton(headerRect);
			DrawColorPickerButton(headerRect);
		}

		void DrawSaveButton(Rect headerRect)
		{
			if (DeleteButton(headerRect))
			{

			}
		}

		void DrawColorPickerButton(Rect headerRect)
		{
			if (ColorPickerButton(headerRect))
			{
			}
		}

		static bool DeleteButton(Rect headerRect)
		{
			return QuickEditorGUI.TextureButton(GetDeleteBtnRect(headerRect), Assets.Textures.DeleteTexture);
		}

		static bool ColorPickerButton(Rect headerRect)
		{
			return QuickEditorGUI.TextureButton(GetPickColorBtnRect(headerRect), Assets.Textures.MoreOptionsTexture);
		}

		#region rects

		static Rect GetHeaderRect(Rect noteRect)
		{
			var headerRect = new Rect(noteRect.x, noteRect.y, noteRect.width, Height);
			return headerRect;
		}

		static Rect GetDeleteBtnRect(Rect headerRect)
		{
			return new Rect(headerRect.width - headerRect.height, headerRect.y, headerRect.height, headerRect.height);
		}

		static Rect GetPickColorBtnRect(Rect headerRect)
		{
			return new Rect(headerRect.width - headerRect.height * 2, headerRect.y, headerRect.height, headerRect.height);
		}

		#endregion
	}
}

#endif