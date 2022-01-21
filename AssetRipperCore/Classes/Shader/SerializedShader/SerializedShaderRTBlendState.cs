using AssetRipper.Core.Classes.Shader.SerializedShader.Enum;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;

namespace AssetRipper.Core.Classes.Shader.SerializedShader
{
	public struct SerializedShaderRTBlendState : IAssetReadable,IYAMLExportable
	{
		public void Read(AssetReader reader)
		{
			SrcBlend.Read(reader);
			DestBlend.Read(reader);
			SrcBlendAlpha.Read(reader);
			DestBlendAlpha.Read(reader);
			BlendOp.Read(reader);
			BlendOpAlpha.Read(reader);
			ColMask.Read(reader);
		}

		public YAMLNode ExportYAML(IExportContainer container)
		{
			YAMLMappingNode node = new YAMLMappingNode();
			node.Add("srcBlend", SrcBlend.ExportYAML(container));
			node.Add("destBlend", DestBlend.ExportYAML(container));
			node.Add("srcBlendAlpha", SrcBlendAlpha.ExportYAML(container));
			node.Add("destBlendAlpha", DestBlendAlpha.ExportYAML(container));
			node.Add("blendOp", BlendOp.ExportYAML(container));
			node.Add("blendOpAlpha", BlendOpAlpha.ExportYAML(container));
			node.Add("colMask", ColMask.ExportYAML(container));
			return node;
		}

		public SerializedShaderFloatValue SrcBlend;
		public SerializedShaderFloatValue DestBlend;
		public SerializedShaderFloatValue SrcBlendAlpha;
		public SerializedShaderFloatValue DestBlendAlpha;
		public SerializedShaderFloatValue BlendOp;
		public SerializedShaderFloatValue BlendOpAlpha;
		public SerializedShaderFloatValue ColMask;

		public BlendMode SrcBlendValue => (BlendMode)SrcBlend.Val;
		public BlendMode DestBlendValue => (BlendMode)DestBlend.Val;
		public BlendMode SrcBlendAlphaValue => (BlendMode)SrcBlendAlpha.Val;
		public BlendMode DestBlendAlphaValue => (BlendMode)DestBlendAlpha.Val;
		public BlendOp BlendOpValue => (BlendOp)BlendOp.Val;
		public BlendOp BlendOpAlphaValue => (BlendOp)BlendOpAlpha.Val;
		public ColorWriteMask ColMaskValue => (ColorWriteMask)ColMask.Val;
	}
}