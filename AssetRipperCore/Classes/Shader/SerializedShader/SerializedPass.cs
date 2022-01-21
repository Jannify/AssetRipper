using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.Classes.Shader.SerializedShader.Enum;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;
using AssetRipper.Core.YAML.Extensions;
using System.Collections.Generic;
using UnityVersion = AssetRipper.Core.Parser.Files.UnityVersion;

namespace AssetRipper.Core.Classes.Shader.SerializedShader
{
	public struct SerializedPass : IAssetReadable, IYAMLExportable
	{
		/// <summary>
		/// 2020.2 and greater
		/// </summary>
		public static bool HasHash(UnityVersion version) => version.IsGreaterEqual(2020, 2);
		/// <summary>
		/// 2019.3 and greater
		/// </summary>
		public static bool HasProgRayTracing(UnityVersion version) => version.IsGreaterEqual(2019, 3);

		/// <summary>
		/// 2021.2 and greater
		/// </summary>
		public static bool HasKeywordStateMaskInsteadOfKeywordMasks(UnityVersion version) => version.IsGreaterEqual(2021, 2);

		public void Read(AssetReader reader)
		{
			if (HasHash(reader.Version))
			{
				EditorDataHash = reader.ReadAssetArray<Hash128>();
				reader.AlignStream();
				Platforms = reader.ReadUInt8Array();
				reader.AlignStream();
				if (!HasKeywordStateMaskInsteadOfKeywordMasks(reader.Version))
				{
					LocalKeywordMask = reader.ReadUInt16Array();
					reader.AlignStream();
					GlobalKeywordMask = reader.ReadUInt16Array();
					reader.AlignStream();
				}
			}

			m_nameIndices = new Dictionary<string, int>();
			m_nameIndices.Read(reader);

			Type = (SerializedPassType)reader.ReadInt32();
			State.Read(reader);
			ProgramMask = reader.ReadUInt32();
			ProgVertex.Read(reader);
			ProgFragment.Read(reader);
			ProgGeometry.Read(reader);
			ProgHull.Read(reader);
			ProgDomain.Read(reader);
			if (HasProgRayTracing(reader.Version))
			{
				ProgRayTracing.Read(reader);
			}
			HasInstancingVariant = reader.ReadBoolean();
			HasProceduralInstancingVariant = reader.ReadBoolean();
			reader.AlignStream();

			UseName = reader.ReadString();
			Name = reader.ReadString();
			TextureName = reader.ReadString();
			Tags.Read(reader);

			if (HasKeywordStateMaskInsteadOfKeywordMasks(reader.Version))
			{
				reader.ReadUInt16Array(); //m_SerializedKeywordStateMask
			}
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			if (HasHash(container.Version))
			{
				node.Add("m_EditorDataHash", EditorDataHash.ExportYAML(container));
				node.Add("m_Platforms", Platforms.ExportYAML());
				if (!HasKeywordStateMaskInsteadOfKeywordMasks(container.Version))
				{
					node.Add("m_LocalKeywordMask", LocalKeywordMask.ExportYAML(false));
					node.Add("m_GlobalKeywordMask", GlobalKeywordMask.ExportYAML(false));
				}
			}
			node.Add("m_NameIndices", NameIndices.ExportYAML());
			node.Add("m_Type", (int)Type);
			node.Add("m_State", State.ExportYAML(container));
			node.Add("m_ProgramMask", ProgramMask);
			node.Add("progVertex", ProgVertex.ExportYAML(container));
			node.Add("progFragment", ProgFragment.ExportYAML(container));
			node.Add("progGeometry", ProgGeometry.ExportYAML(container));
			node.Add("progHull", ProgHull.ExportYAML(container));
			node.Add("progDomain", ProgDomain.ExportYAML(container));
			if (HasProgRayTracing(container.Version))
			{
				node.Add("progRayTracing", ProgRayTracing.ExportYAML(container));
			}
			node.Add("m_HasInstancingVariant", HasInstancingVariant);
			node.Add("m_HasProceduralInstancingVariant", HasProceduralInstancingVariant);
			node.Add("m_UseName", UseName);
			node.Add("m_Name", Name);
			node.Add("m_TextureName", TextureName);
			node.Add("m_Tags", Tags.ExportYAML(container));
			if (HasKeywordStateMaskInsteadOfKeywordMasks(container.Version))
			{
				//TODO: Implement SerializedKeywordStateMask
				//node.Add("m_SerializedKeywordStateMask", SerializedKeywordStateMask.ExportYAML(container));
			}
			return node;
		}

		public Hash128[] EditorDataHash { get; set; }
		public byte[] Platforms { get; set; }
		public ushort[] LocalKeywordMask { get; set; }
		public ushort[] GlobalKeywordMask { get; set; }
		public IReadOnlyDictionary<string, int> NameIndices => m_nameIndices;
		public SerializedPassType Type { get; set; }
		public uint ProgramMask { get; set; }
		public bool HasInstancingVariant { get; set; }
		public bool HasProceduralInstancingVariant { get; set; }
		public string UseName { get; set; }
		public string Name { get; set; }
		public string TextureName { get; set; }

		public SerializedShaderState State;
		public SerializedProgram ProgVertex;
		public SerializedProgram ProgFragment;
		public SerializedProgram ProgGeometry;
		public SerializedProgram ProgHull;
		public SerializedProgram ProgDomain;
		public SerializedProgram ProgRayTracing;
		public SerializedTagMap Tags;

		private Dictionary<string, int> m_nameIndices;
	}
}