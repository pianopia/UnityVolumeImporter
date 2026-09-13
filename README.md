# Unity Volume Importer

Standalone Unity 6 package for **volumetric import and in-scene preview**. Import dense 3D volume textures today; native OpenVDB™ (`.vdb`) support is on the roadmap.

Free distribution via [Booth](https://booth.pm) is planned — this repository is the source of truth for the UPM package and release artifacts.

OpenVDB is a trademark of LF Projects, LLC.

## Features (0.1.0)

- Import **Volume Texture v1** files (`.uvi`, legacy `.evol`)
- Editor menu: **Unity Volume Importer → Import Volume…**
- `VolumePlayer` component with multi-frame playback
- Built-in raymarch preview shader (`Louddin/VolumeRaymarch`)
- `IVdbImporter` plug-in surface for future `.vdb` back end

## Requirements

- **Unity 6** (6000.x)
- HDRP, URP, or Built-in projects (preview shader targets Built-in; sample `_VolumeTex` in your SRP shaders for production)

## Install

### Git URL (UPM)

```
https://github.com/pianopia/UnityVolumeImporter.git?path=Packages/com.louddin.unity-volume-importer
```

**Window → Package Manager → + → Add package from git URL…**

### Local / disk

1. Clone this repository or copy `Packages/com.louddin.unity-volume-importer` into your project `Packages/` folder.
2. **Package Manager → + → Add package from disk…** → select `package.json`.

### Booth zip (when published)

Extract the downloaded zip into `Packages/` or import the bundled `.unitypackage`. See [docs/booth-packaging.md](docs/booth-packaging.md).

## Quick usage

1. Obtain a `.uvi` or `.evol` volume file (see [tools/README.md](tools/README.md) for conversion notes).
2. **Unity Volume Importer → Import Volume…**
3. Select the file. Assets land in `Assets/UnityVolumeImporter/Volumes/<name>/` and a `VolumePlayer` is added to the scene.
4. Press Play to preview animated volumes.

[QUICKSTART.md](QUICKSTART.md) has a step-by-step walkthrough.

## Import paths

| Source | Menu | Result |
|--------|------|--------|
| `.uvi` Volume Texture v1 | Import Volume… | `Texture3D` assets + `VolumePlayer` |
| `.evol` legacy volume texture | Import Volume… | Same (reads `EFVT` magic) |
| `.vdb` OpenVDB grid | Import Volume… | Dialog with conversion guidance (native import planned) |

## Runtime

Attach or use the spawned **`VolumePlayer`** component:

| Field | Description |
|-------|-------------|
| `volumeTexture` | Primary 3D density texture |
| `frameTextures` | Optional per-frame textures for animation |
| `boundsMin` / `boundsMax` | World-space axis-aligned bounds (meters, Y-up) |
| `playbackFps` | Frame rate (default 12) |
| `densityScale`, `stepSize`, `maxSteps` | Raymarch tuning |

## Architecture

```
IVdbImporter
├── VolumeTextureImporterBackend   (.uvi, .evol)
└── NativeVdbImporterStub          (.vdb — future)

VolumeTextureReader  → parses Volume Texture v1 binary
VolumePlayer         → scene playback + raymarch material
```

Format specification: [docs/volume-texture-v1.md](docs/volume-texture-v1.md)

## Known limitations

Phase 1 is dense-volume import and preview only. See [docs/known-limitations.md](docs/known-limitations.md).

## Documentation

| Doc | Purpose |
|-----|---------|
| [QUICKSTART.md](QUICKSTART.md) | Five-minute setup |
| [CHANGELOG.md](CHANGELOG.md) | Release history |
| [docs/known-limitations.md](docs/known-limitations.md) | Scope and caps |
| [docs/booth-packaging.md](docs/booth-packaging.md) | Booth zip / `.unitypackage` layout |
| [docs/volume-texture-v1.md](docs/volume-texture-v1.md) | Binary format reference |
| [tools/README.md](tools/README.md) | External conversion pointers |

## License

MIT — see [LICENSE](LICENSE).
