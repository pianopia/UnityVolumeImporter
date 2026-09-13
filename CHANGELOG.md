# Changelog

All notable changes to **Unity Volume Importer** are documented here.

## [0.1.0] - 2026-09-13

### Added

- Initial public release as a standalone Unity 6 package (`com.louddin.unity-volume-importer`).
- **Volume Texture v1** import for `.uvi` and legacy `.evol` dense volume files.
- Editor menu: **Unity Volume Importer → Import Volume…**
- `VolumePlayer` runtime component with frame playback and raymarch preview shader (`Louddin/VolumeRaymarch`).
- `IVdbImporter` interface with `VolumeTextureImporterBackend` (dense volumes) and `NativeVdbImporterStub` (`.vdb` placeholder).
- Documentation: README, QUICKSTART, known limitations, Booth packaging notes, and conversion tool pointers.

### Notes

- Native OpenVDB™ (`.vdb`) decode is planned for a later release.
- Phase 1 focuses on dense `Texture3D` import and in-scene preview playback.
