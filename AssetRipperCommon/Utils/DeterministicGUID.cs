using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AssetRipper.Core.Utils
{
	public static class DeterministicGUID
	{
		private static readonly HashSet<Guid> allGuids = new();
		private static bool isDeterministic = true;

		public static Guid NewGuid(ClassIDType classID, string name, long pathID)
		{
			byte[] guidData = new byte[16];
			Array.Copy(BitConverter.GetBytes((int)classID), guidData, 4);
			Array.Copy(MD5.HashData(Encoding.ASCII.GetBytes(name)), 0, guidData, 4, 4);
			Array.Copy(BitConverter.GetBytes(pathID), 0, guidData, 8, 8);
			Guid guid = new(guidData);

			return guid.Validate($"GUID was already existing. ClassNumber:{classID} Name:{name} PathID:{pathID}. Using random one.");
		}

		public static Guid NewGuid(ClassIDType classID, string seed)
		{
			byte[] guidData = new byte[16];
			Array.Copy(BitConverter.GetBytes((int)classID), guidData, 4);
			Array.Copy(MD5.HashData(Encoding.ASCII.GetBytes(seed)), 0, guidData, 4, 12);
			Guid guid = new(guidData);

			return guid.Validate($"GUID was already existing. ClassID: {classID} Seed:{seed}. Using random one.");
		}

		public static Guid NewGuid(string seed)
		{
			byte[] guidData = new byte[16];
			Array.Copy(MD5.HashData(Encoding.ASCII.GetBytes(seed)), guidData, 16);
			Guid guid = new(guidData);

			return guid.Validate($"GUID was already existing. Seed:{seed}. Using random one.");
		}

		private static Guid Validate(this Guid guid, string errorMessage)
		{
			if (allGuids.Contains(guid))
			{
				Logging.Logger.Warning(errorMessage);
				guid = Guid.NewGuid();
				isDeterministic = false;
			}

			allGuids.Add(guid);
			return guid;
		}

		public static void Reset(out bool wasDeterministic)
		{
			allGuids.Clear();
			wasDeterministic = isDeterministic;
			isDeterministic = true;
		}
	}
}
