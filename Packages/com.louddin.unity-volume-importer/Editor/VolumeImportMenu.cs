using System.IO;
using UnityEditor;
using UnityEngine;

namespace Louddin.UnityVolumeImporter.Editor
{
    public static class VolumeImportMenu
    {
        public const string ImportVolumeMenuPath = "Unity Volume Importer/Import Volume…";

        [MenuItem(ImportVolumeMenuPath)]
        public static void ImportVolume()
        {
            var path = EditorUtility.OpenFilePanel(
                "Import Volume",
                "",
                "uvi,evol,vdb");

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            ImportVolumeAtPath(path);
        }

        public static void ImportVolumeAtPath(string absolutePath)
        {
            if (!File.Exists(absolutePath))
            {
                Debug.LogError($"[Unity Volume Importer] File not found: {absolutePath}");
                return;
            }

            var ext = Path.GetExtension(absolutePath)?.TrimStart('.').ToLowerInvariant();
            if (ext == "vdb")
            {
                EditorUtility.DisplayDialog(
                    "OpenVDB import",
                    "Native .vdb import is not available in this release.\n\n" +
                    "Convert grids to Volume Texture v1 (.uvi) using an external converter " +
                    "(see tools/README.md in this repository), then import the .uvi file with this menu.\n\n" +
                    "OpenVDB is a trademark of LF Projects, LLC.",
                    "OK");
                return;
            }

            try
            {
                var task = VolumeImportService.ImportVolumeFileAsync(absolutePath);
                task.Wait();
                if (task.Result != null)
                {
                    Selection.activeGameObject = task.Result;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[Unity Volume Importer] Import failed: {ex.Message}");
            }
        }
    }
}
