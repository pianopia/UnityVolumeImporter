# Known limitations (Phase 1)

Unity Volume Importer 0.1.0 is a **dense volume texture** import and preview package. The following limits apply.

## Import

| Topic | Limit |
|-------|-------|
| OpenVDB `.vdb` | Not supported in-editor yet. `IVdbImporter` stub logs guidance; convert to Volume Texture v1 first. |
| Sparse grids | Only dense `float32` voxel arrays are supported. |
| Channels | Single density channel. No velocity, temperature, or color channels. |
| Compression | No compression; files are raw `float32` payloads after a 52-byte header. |

## Playback

| Topic | Limit |
|-------|-------|
| Shader | `Louddin/VolumeRaymarch` is a preview-quality unlit raymarch, not production volumetric lighting or fog. |
| HDRP / URP | No first-class SRP shader graphs included. Duplicate or reimplement sampling in your pipeline. |
| Sorting | Transparent queue; no depth-aware compositing with other transparents. |

## Platform

| Topic | Limit |
|-------|-------|
| Texture3D size | Subject to Unity and GPU caps (often 2048³ max per dimension). Large volumes consume significant VRAM. |
| Mobile | Large `Texture3D` assets may exceed mobile memory budgets. |

## Format

| Topic | Limit |
|-------|-------|
| Volume Texture v1 | 52-byte header + little-endian `float32` voxels, x-fastest flattening. |
| Magic bytes | `UVVT` (neutral) or `EFVT` (legacy `.evol`). |
| Version | Only version `1` is accepted. |

## Roadmap (not in 0.1.0)

- Native OpenVDB™ `.vdb` import via `IVdbImporter` implementation.
- Additional channels (velocity, temperature).
- HDRP/URP preview shaders.

OpenVDB is a trademark of LF Projects, LLC.
