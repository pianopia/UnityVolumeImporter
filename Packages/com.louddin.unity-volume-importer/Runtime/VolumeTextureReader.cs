using System;
using System.IO;
using UnityEngine;

namespace Louddin.UnityVolumeImporter
{
    /// <summary>
    /// Binary reader for Volume Texture v1 payloads (.uvi and legacy .evol).
    /// </summary>
    public static class VolumeTextureReader
    {
        static readonly byte[] MagicLegacy = { (byte)'E', (byte)'F', (byte)'V', (byte)'T' };
        static readonly byte[] MagicNeutral = { (byte)'U', (byte)'V', (byte)'V', (byte)'T' };

        public static VolumeTextureHeader ReadHeader(string filePath)
        {
            using var stream = File.OpenRead(filePath);
            using var reader = new BinaryReader(stream);
            var magic = reader.ReadBytes(4);
            if (!MatchesMagic(magic, MagicLegacy) && !MatchesMagic(magic, MagicNeutral))
            {
                throw new InvalidDataException(
                    "Invalid volume texture magic — expected UVVT (Volume Texture v1) or EFVT (legacy).");
            }

            var version = reader.ReadUInt32();
            if (version != 1)
            {
                throw new InvalidDataException($"Unsupported volume texture version {version}");
            }

            var detectedFormat = MatchesMagic(magic, MagicNeutral)
                ? VolumeTextureHeader.FormatId
                : VolumeTextureHeader.LegacyFormatId;

            return new VolumeTextureHeader
            {
                Nx = reader.ReadInt32(),
                Ny = reader.ReadInt32(),
                Nz = reader.ReadInt32(),
                FrameCount = reader.ReadInt32(),
                Channels = reader.ReadInt32(),
                BoundsMin = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                BoundsMax = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()),
                DetectedFormat = detectedFormat,
            };
        }

        public static float[] ReadFrame(string filePath, int frameIndex)
        {
            var header = ReadHeader(filePath);
            if (frameIndex < 0 || frameIndex >= header.FrameCount)
            {
                throw new ArgumentOutOfRangeException(nameof(frameIndex));
            }

            var floatCount = header.FrameFloatCount;
            var data = new float[floatCount];
            using var stream = File.OpenRead(filePath);
            stream.Seek(VolumeTextureHeader.HeaderByteSize + frameIndex * floatCount * 4, SeekOrigin.Begin);
            using var reader = new BinaryReader(stream);
            for (var i = 0; i < floatCount; i++)
            {
                data[i] = reader.ReadSingle();
            }

            return data;
        }

        public static Texture3D CreateTexture3D(float[] density, VolumeTextureHeader header, string name)
        {
            var tex = new Texture3D(header.Nx, header.Ny, header.Nz, TextureFormat.RFloat, false)
            {
                name = name,
                wrapMode = TextureWrapMode.Clamp,
                filterMode = FilterMode.Bilinear,
            };

            var colors = new Color[density.Length];
            for (var i = 0; i < density.Length; i++)
            {
                colors[i] = new Color(density[i], 0f, 0f, 1f);
            }

            tex.SetPixels(colors);
            tex.Apply(updateMipmaps: false, makeNoLongerReadable: false);
            return tex;
        }

        public static Texture3D[] CreateFrameTextures(string filePath, VolumeTextureHeader header, string baseName)
        {
            var frames = new Texture3D[header.FrameCount];
            for (var f = 0; f < header.FrameCount; f++)
            {
                var density = ReadFrame(filePath, f);
                frames[f] = CreateTexture3D(density, header, $"{baseName}_frame{f:D3}");
            }

            return frames;
        }

        static bool MatchesMagic(byte[] actual, byte[] expected)
        {
            if (actual.Length != expected.Length)
            {
                return false;
            }

            for (var i = 0; i < expected.Length; i++)
            {
                if (actual[i] != expected[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
