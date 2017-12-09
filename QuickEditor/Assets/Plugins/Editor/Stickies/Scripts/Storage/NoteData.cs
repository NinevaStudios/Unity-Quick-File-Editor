#if UNITY_EDITOR
namespace DeadMosquito.QuickEditor
{
	using System;

	[Serializable]
	public class NoteData
	{
		public NoteColor color;
		public string guid;
		public string text;

		public NoteData(NoteData other)
		{
			guid = other.guid;
			color = other.color;
			text = other.text;
		}

		public NoteData(string guid)
		{
			this.guid = guid;
			color = NoteColor.Lemon;
			text = string.Empty;
		}
	}
}
#endif