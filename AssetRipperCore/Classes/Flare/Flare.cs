using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;
using System.Collections.Generic;

namespace AssetRipper.Core.Classes.Flare
{
	public sealed class Flare : NamedObject
	{
		public Flare(AssetInfo assetInfo) : base(assetInfo) { }

		public override void Read(AssetReader reader)
		{
			base.Read(reader);
			FlareTexture.Read(reader);
			TextureLayout = (TextureLayout)reader.ReadInt32();
			Elements = reader.ReadAssetArray<FlareElement>();
			UseFog = reader.ReadBoolean();
		}

		public override IEnumerable<PPtr<IUnityObjectBase>> FetchDependencies(DependencyContext context)
		{
			foreach (PPtr<IUnityObjectBase> asset in base.FetchDependencies(context))
			{
				yield return asset;
			}

			yield return context.FetchDependency(FlareTexture, "m_FlareTexture");
		}

		protected override YAMLMappingNode ExportYAMLRoot(IExportContainer container)
		{
			YAMLMappingNode node = base.ExportYAMLRoot(container);
			node.AddSerializedVersion(1);
			node.Add("m_FlareTexture", FlareTexture.ExportYAML(container));
			node.Add("m_TextureLayout", (int)TextureLayout);
			node.Add("m_Elements", Elements.ExportYAML(container));
			node.Add("m_UseFog", UseFog);
			return node;
		}

		public TextureLayout TextureLayout { get; set; }
		public FlareElement[] Elements { get; set; }
		public bool UseFog { get; set; }
		public PPtr<Texture> FlareTexture = new();
	}
}
