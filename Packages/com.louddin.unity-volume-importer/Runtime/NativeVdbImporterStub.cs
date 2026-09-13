using System.IO;
using System.Threading.Tasks;

namespace Louddin.UnityVolumeImporter
{
    /// <summary>
    /// Placeholder for native OpenVDB .vdb import (planned for a later release).
    /// </summary>
    public sealed class NativeVdbImporterStub : IVdbImporter
    {
        public string[] SupportedExtensions => new[] { "vdb" };

        public bool CanImport(string absolutePath)
        {
            var ext = Path.GetExtension(absolutePath)?.TrimStart('.').ToLowerInvariant();
            return ext == "vdb";
        }

        public Task<VolumeImportResult> ImportAsync(string absolutePath, string assetFolder)
        {
            throw new FileLoadException(
                "Native .vdb import is not available in this release. " +
                "Convert grids to Volume Texture v1 (.uvi) using an external tool — see tools/README.md — " +
                "then import with Unity Volume Importer → Import Volume…");
        }
    }
}
