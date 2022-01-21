using AssetRipper.Core;
using AssetRipper.Core.Classes.Shader;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Project;
using AssetRipper.Core.Project.Collections;
using AssetRipper.Core.Project.Exporters;
using System;

namespace AssetRipper.Library.Exporters.Shaders
{
	/// <summary>
	/// An exporter for exporting shaders as unity assets. Shader.Find will not work in the Unity Editor with this exporter.
	/// </summary>
	public class AssetShaderExporter : YamlExporterBase
	{
		public override bool IsHandle(IUnityObjectBase asset)
		{
			return asset is IShader;
		}
		public override IExportCollection CreateCollection(VirtualSerializedFile virtualFile, IUnityObjectBase asset)
		{
			return new AssetExportCollection(this, new AssetShader(asset as IShader));
		}

		public override bool Export(IExportContainer container, IUnityObjectBase asset, string path)
		{
			IShader shader = (IShader)asset;

			//Importing Hidden/Internal shaders causes the unity editor screen to turn black
		//	if (shader.Name.StartsWith("Hidden/Internal", StringComparison.Ordinal))
		//		return false;

			return base.Export(container, new AssetShader(shader), path);
		}


	}
}