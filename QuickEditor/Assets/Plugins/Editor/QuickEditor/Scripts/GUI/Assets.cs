#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System.Collections.Generic;
	using System.IO;
	using UnityEditor;
	using UnityEngine;

	public static class Assets
	{
		public static class Styles
		{
			public static readonly GUIStyle TextArea;
			public static readonly GUIStyle PlusLabel;
			public static readonly GUIStyle BlackBoldText;

			public static readonly GUISkin Skin;

			static Styles()
			{
				var fontPath = Path.Combine(QuickEditorEditorSettings.QuickEditorHomeFolder, "Assets/SourceCodePro-Regular.ttf");
				var font = AssetDatabase.LoadAssetAtPath<Font>(fontPath);
				
				TextArea = new GUIStyle(EditorStyles.textArea)
				{
					normal = {background = null, textColor = Color.black},
					active = {background = null},
					focused = {background = null},
					wordWrap = false,
					richText = false,
					font = font,
				};
				PlusLabel = new GUIStyle(EditorStyles.boldLabel)
				{
					padding = new RectOffset(0, 1, 0, 2),
					margin = new RectOffset(),
					alignment = TextAnchor.MiddleCenter,
					stretchHeight = true,
					stretchWidth = true
				};
				BlackBoldText = EditorStyles.boldLabel;
				BlackBoldText.normal.textColor = Color.black;

				var skinPath = Path.Combine(QuickEditorEditorSettings.QuickEditorHomeFolder, "Assets/ScrollGUISkin.asset");
				Skin = AssetDatabase.LoadAssetAtPath<GUISkin>(skinPath);

				if (Skin == null)
				{
					Debug.LogError(
						"Could not load GUI skin. Did you move QuickEditor folder around in your project? Go to Preferences -> QuickEditor and update the location of QuickEditor folder");
				}
			}
		}


		public static class Textures
		{
			public static readonly Texture2D DeleteTexture;
			public static readonly Texture2D CloseTexture;
			public static readonly Texture2D SaveTexture;
			public static readonly Texture2D MoreOptionsTexture;

			public static readonly Texture2D LemonNoteTexture;
			public static readonly Texture2D GrassNoteTexture;
			public static readonly Texture2D SkyBlueNoteTexture;
			public static readonly Texture2D AmethystNoteTexture;
			public static readonly Texture2D RoseNoteTexture;
			public static readonly Texture2D CleanNoteTexture;

			public static readonly Texture HasText;

			static Dictionary<NoteColor, Texture2D> _notes;

			static Textures()
			{
				DeleteTexture = GetTexture("ic_delete");
				CloseTexture = GetTexture("ic_close");
				SaveTexture = GetTexture("ic_save");
				MoreOptionsTexture = GetTexture("ic_color_picker");

				LemonNoteTexture = GetTexture("1x/lemon");
				GrassNoteTexture = GetTexture("1x/grass");
				SkyBlueNoteTexture = GetTexture("1x/sky_blue");
				AmethystNoteTexture = GetTexture("1x/amethyst");
				RoseNoteTexture = GetTexture("1x/rose");
				CleanNoteTexture = GetTexture("1x/clean");

				HasText = GetTexture("has_text");

				InitNotesDic();
			}


			static void InitNotesDic()
			{
				_notes = new Dictionary<NoteColor, Texture2D>
				{
					{NoteColor.None, LemonNoteTexture},
					{NoteColor.Lemon, LemonNoteTexture},
					{NoteColor.Grass, GrassNoteTexture},
					{NoteColor.SkyBlue, SkyBlueNoteTexture},
					{NoteColor.Amethyst, AmethystNoteTexture},
					{NoteColor.Rose, RoseNoteTexture},
					{NoteColor.Clean, CleanNoteTexture}
				};
			}

			public static Texture2D NoteByColor(NoteColor color)
			{
				return !_notes.ContainsKey(color) ? LemonNoteTexture : _notes[color];
			}

			static Texture2D GetTexture(string name)
			{
				return AssetDatabase.LoadAssetAtPath<Texture2D>(GetTexturePath(name));
			}

			static string GetTexturePath(string name)
			{
				var relativePath = Path.Combine("Assets/GUI", name + ".png");
				return Path.Combine(QuickEditorEditorSettings.QuickEditorHomeFolder, relativePath);
			}
		}
	}
}

#endif