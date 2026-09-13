using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Louddin.UnityVolumeImporter
{
    /// <summary>
    /// Phase 1 importer for Volume Texture v1 (.uvi and legacy .evol) files.
    /// </summary>
    public sealed class VolumeTextureImporterBackend : IVdbImporter
    {
        public string[] SupportedExtensions => new[] { "uvi", "evol" };

        public bool CanImport(string absolutePath)
        {
            var ext = Path.GetExtension(absolutePath)?.TrimStart('.').ToLowerInvariant();
            return (ext == "uvi" || ext == "evol") && File.Exists(absolutePath);
        }

        public Task<VolumeImportResult> ImportAsync(string absolutePath, string assetFolder)
        {
            var header = VolumeTextureReader.ReadHeader(absolutePath);
            var baseName = Path.GetFileNameWithoutExtension(absolutePath);
            var frames = VolumeTextureReader.CreateFrameTextures(absolutePath, header, baseName);

            Directory.CreateDirectory(assetFolder);
            var savedFrames = new Texture3D[frames.Length];
            for (var i = 0; i < frames.Length; i++)
            {
                var assetPath = Path.Combine(assetFolder, $"{baseName}_frame{i:D3}.asset");
                savedFrames[i] = SaveTextureAsset(frames[i], assetPath);
            }

            var result = new VolumeImportResult
            {
                VolumeTexture = savedFrames.Length > 0 ? savedFrames[0] : null,
                FrameTextures = savedFrames,
                BoundsMin = header.BoundsMin,
                BoundsMax = header.BoundsMax,
                PlaybackFps = 12f,
                SourceFormat = header.DetectedFormat ?? VolumeTextureHeader.FormatId,
                AssetPath = assetFolder,
            };

            return Task.FromResult(result);
        }

        static Texture3D SaveTextureAsset(Texture3D texture, string assetPath)
        {
#if UNITY_EDITOR
            var existing = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture3D>(assetPath);
            if (existing != null)
            {
                UnityEditor.AssetDatabase.DeleteAsset(assetPath);
            }

            UnityEditor.AssetDatabase.CreateAsset(texture, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            return UnityEditor.AssetDatabase.LoadAssetAtPath<Texture3D>(assetPath);
#else
            return texture;
#endif
        }
    }
}
