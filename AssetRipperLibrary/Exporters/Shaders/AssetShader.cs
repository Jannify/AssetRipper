using AssetRipper.Core;
using AssetRipper.Core.Classes;
using AssetRipper.Core.Classes.Misc;
using AssetRipper.Core.Classes.Object;
using AssetRipper.Core.Classes.PrefabInstance;
using AssetRipper.Core.Parser.Asset;
using AssetRipper.Core.Classes.Shader;
using AssetRipper.Core.Interfaces;
using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Endian;
using AssetRipper.Core.Parser.Files;
using AssetRipper.Core.Parser.Files.SerializedFiles;
using AssetRipper.Core.Parser.Files.SerializedFiles.Parser.TypeTree;
using AssetRipper.Core.Project;
using AssetRipper.Core.YAML;
using System.Collections.Generic;

namespace AssetRipper.Library.Exporters.Shaders
{
	public sealed class AssetShader : INamedObject
	{
		private readonly IShader shader;

		public AssetShader(IShader shader)
		{
			this.shader = shader;
		}

		public string ExportExtension => UnityObjectBase.AssetExtension;

		// Pointers to base shader
		public AssetInfo AssetInfo
		{
			get => shader.AssetInfo;
			set => shader.AssetInfo = value;
		}

		public ClassIDType ClassID => shader.ClassID;

		public string ExportPath => shader.ExportPath;
		public ISerializedFile SerializedFile => shader.SerializedFile;
		public UnityGUID GUID
		{
			get => shader.GUID;
			set => shader.GUID = value;
		}

		public long PathID => shader.PathID;

		public HideFlags ObjectHideFlags
		{
			get => shader.ObjectHideFlags;
			set => shader.ObjectHideFlags = value;
		}

		public IUnityObjectBase ConvertLegacy(IExportContainer container) => shader.ConvertLegacy(container);

		public YAMLDocument ExportYAMLDocument(IExportContainer container) => shader.ExportYAMLDocument(container);

		public void Read(AssetReader reader) => shader.Read(reader);

		public void Write(AssetWriter writer) => shader.Write(writer);

		public YAMLNode ExportYAML(IExportContainer container) => shader.ExportYAML(container);

		public IEnumerable<PPtr<IUnityObjectBase>> FetchDependencies(DependencyContext context) => shader.FetchDependencies(context);

		public List<TypeTreeNode> MakeReleaseTypeTreeNodes(int depth, int startingIndex) => shader.MakeReleaseTypeTreeNodes(depth, startingIndex);

		public List<TypeTreeNode> MakeEditorTypeTreeNodes(int depth, int startingIndex) => shader.MakeReleaseTypeTreeNodes(depth, startingIndex);

		public UnityVersion AssetUnityVersion
		{
			get => shader.AssetUnityVersion;
			set => shader.AssetUnityVersion = value;
		}

		public EndianType EndianType
		{
			get => shader.EndianType;
			set => shader.EndianType = value;
		}

		public TransferInstructionFlags TransferInstructionFlags
		{
			get => shader.TransferInstructionFlags;
			set => shader.TransferInstructionFlags = value;
		}

		public void ConvertToEditor() => shader.ConvertToEditor();

		public string Name
		{
			get => shader.Name;
			set => shader.Name = value;
		}

		public PPtr<IPrefabInstance> PrefabInstance
		{
			get => shader.PrefabInstance;
			set => shader.PrefabInstance = value;
		}
	}
}
