using UnityEngine;

namespace Louddin.UnityVolumeImporter
{
    /// <summary>
    /// Header for Volume Texture v1 files (.uvi or legacy .evol).
    /// </summary>
    public sealed class VolumeTextureHeader
    {
        public const string FormatId = "uvi_volume_texture_v1";
        public const string LegacyFormatId = "elfentier_volume_texture_v1";
        public const int HeaderByteSize = 52;

        public int Nx;
        public int Ny;
        public int Nz;
        public int FrameCount;
        public int Channels;
        public Vector3 BoundsMin;
        public Vector3 BoundsMax;
        public string DetectedFormat;

        public int VoxelCount => Nx * Ny * Nz;
        public int FrameFloatCount => VoxelCount * Channels;
        public int DataByteLength => FrameFloatCount * FrameCount * 4;
    }
}
