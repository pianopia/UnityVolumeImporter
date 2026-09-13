# Quickstart

Get a volume into Unity in under five minutes.

## 1. Install the package

**Git URL (UPM):**

1. Open **Window → Package Manager**.
2. Click **+ → Add package from git URL…**
3. Enter:
   ```
   https://github.com/pianopia/UnityVolumeImporter.git?path=Packages/com.louddin.unity-volume-importer
   ```

**Local folder:**

Copy `Packages/com.louddin.unity-volume-importer` into your project's `Packages/` folder, or use **Add package from disk…** and select its `package.json`.

## 2. Obtain a volume file

Phase 1 imports **Volume Texture v1** files:

| Extension | Notes |
|-----------|-------|
| `.uvi` | Preferred neutral extension |
| `.evol` | Legacy extension (same binary layout, `EFVT` magic) |

If you have smoke or fluid simulation grids in another format, convert them first — see [tools/README.md](tools/README.md).

## 3. Import in the Editor

1. **Unity Volume Importer → Import Volume…**
2. Select your `.uvi` or `.evol` file.
3. The importer creates `Texture3D` assets under `Assets/UnityVolumeImporter/Volumes/<name>/` and spawns a `VolumePlayer` in the scene.

## 4. Preview playback

The spawned `VolumePlayer` raymarches the density volume inside its bounds. Tune these fields in the Inspector:

- **Bounds Min / Max** — world-space AABB (meters, Y-up)
- **Playback Fps** — frame rate for multi-frame volumes (default 12)
- **Density Scale**, **Step Size**, **Max Steps** — raymarch quality

Press Play to animate multi-frame volumes.

## 5. Render pipelines

`Louddin/VolumeRaymarch` is a Built-in pipeline preview shader. For HDRP or URP production work, sample `_VolumeTex` in your own shader or Shader Graph with the same bounds and density uniforms.

## Next steps

- [README.md](README.md) — full install options and Booth distribution notes
- [docs/known-limitations.md](docs/known-limitations.md) — Phase 1 scope
- [docs/booth-packaging.md](docs/booth-packaging.md) — zip layout for Booth listing
