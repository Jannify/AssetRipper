using AssetRipper.Core.Classes.Misc;
using NUnit.Framework;
using System;

namespace AssetRipper.Tests
{
	public class GuidTests
	{
		private const string randomGuidString = "352a5b3897136ed2702a283243520538";
		private const string sequentialGuidString = "0123456789abcdef0fedcba987654321";

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void MissingReferenceSerializedCorrectly()
		{
			Assert.AreEqual("0000000deadbeef15deadf00d0000000", UnityGUID.MissingReference.ToString());
		}

		[Test]
		public void ToByteArrayIsConsistentWithConstructorFromByteArray()
		{
			UnityGUID guid = UnityGUID.NewGuid(randomGuidString);
			byte[] bytes = guid.ToByteArray();
			UnityGUID fromBytes = new UnityGUID(bytes);
			Assert.AreEqual(guid, fromBytes);
			Assert.AreEqual(guid.ToString(), fromBytes.ToString());
		}

		[Test]
		public void ConversionFromSystemGuidToUnityGuidProducesSameString()
		{
			Guid systemGuid = Guid.NewGuid();
			UnityGUID unityGUID = (UnityGUID) systemGuid;
			Assert.AreEqual(systemGuid.ToString().Replace("-",""), unityGUID.ToString());
		}

		[Test]
		public void IsZeroReturnsTrueForTheZeroGuid()
		{
			UnityGUID unityGUID = new UnityGUID(0, 0, 0, 0);
			Assert.IsTrue(unityGUID.IsZero);
		}

		[Test]
		public void IsZeroReturnsFalseForRandomGuid()
		{
			UnityGUID unityGUID = UnityGUID.NewGuid(randomGuidString);
			Assert.IsFalse(unityGUID.IsZero);
		}

		[Test]
		public void ParsedGuidOutputsSameString()
		{
			UnityGUID parsedGUID = UnityGUID.Parse(randomGuidString);
			string outputedString = parsedGUID.ToString();
			Assert.AreEqual(randomGuidString, outputedString);
		}

		[Test]
		public void ConversionsAreInverses()
		{
			UnityGUID unityGuid = UnityGUID.NewGuid(randomGuidString);
			Guid systemGuid = (Guid)unityGuid;
			Assert.AreEqual(unityGuid, (UnityGUID)systemGuid);
		}
	}
}
