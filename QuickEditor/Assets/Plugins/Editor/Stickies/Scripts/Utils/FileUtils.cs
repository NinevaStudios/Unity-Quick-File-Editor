namespace DeadMosquito.QuickEditor
{
	using System.IO;

	public static class FileUtils
	{
		public static bool IsFile(string filePath)
		{
			var attr = File.GetAttributes(filePath);
			return (attr & FileAttributes.Directory) != FileAttributes.Directory;
		}
	}
}