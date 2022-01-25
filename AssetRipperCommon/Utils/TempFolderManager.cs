using System;
using System.IO;
using System.Text;

namespace AssetRipper.Core.Utils
{
	public static class TempFolderManager
	{
		private const int NUMBER_OF_RANDOM_CHARACTERS = 10;
		public static string TempFolderPath { get; }

		private static readonly Random random = new Random();

		static TempFolderManager()
		{
			TempFolderPath = ExecutingDirectory.Combine("temp");
			DeleteTempFolder();
			Directory.CreateDirectory(TempFolderPath);
		}

		private static void DeleteTempFolder()
		{
			if (Directory.Exists(TempFolderPath))
				Directory.Delete(TempFolderPath, true);
		}

		private static string GetNewRandomTempFolder() => Path.Combine(TempFolderPath, $"AssetRipper-{GenerateRandomString(NUMBER_OF_RANDOM_CHARACTERS)}");

		public static string GenerateRandomString(int size)
		{
			StringBuilder builder = new StringBuilder();

			for (int i = 0; i < size; i++)
			{
				char ch = Convert.ToChar(Convert.ToInt32(26 * random.NextDouble() + 65));
				builder.Append(ch);
			}

			return builder.ToString();
		}

		public static string CreateNewRandomTempFolder()
		{
			string path = GetNewRandomTempFolder();
			Directory.CreateDirectory(path);
			return path;
		}

		/// <summary>
		/// Make a temporary file
		/// </summary>
		/// <param name="data"></param>
		/// <param name="fileExtension">The file extension with the dot</param>
		/// <returns>The path to the file</returns>
		public static string WriteToTempFile(byte[] data, string fileExtension)
		{
			if (data == null)
				throw new ArgumentNullException(nameof(data));

			string fileName = GenerateRandomString(NUMBER_OF_RANDOM_CHARACTERS) + (fileExtension ?? "");
			string filePath = Path.Combine(TempFolderPath, fileName);
			File.WriteAllBytes(filePath, data);
			return filePath;
		}
	}
}
