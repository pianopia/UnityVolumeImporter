# Conversion tools

Unity Volume Importer imports **Volume Texture v1** (`.uvi` / `.evol`) dense files. It does not include a full grid simulation or OpenVDB™ decode pipeline.

## What you need

A tool that writes Volume Texture v1 binaries (see [docs/volume-texture-v1.md](../docs/volume-texture-v1.md)) or renames legacy `.evol` exports.

## External converters

Implement or use a CLI that:

1. Reads your source grid (simulation cache, image sequence, etc.).
2. Resamples to a uniform `nx × ny × nz` dense float32 array.
3. Writes the 52-byte header + payload with `UVVT` magic and `.uvi` extension.

### Suggested workflow (smoke / fluid grids)

If you already have a pipeline that produced legacy `.evol` files (`EFVT` magic), import them directly — no conversion required.

To produce new neutral `.uvi` files from custom tools:

```
your_converter --format uvi_volume_texture_v1 --output smoke.uvi grid_cache/
```

### OpenVDB `.vdb` grids

Phase 1 does not decode `.vdb` in Unity. Use an external tool to rasterize or resample OpenVDB grids into a dense Volume Texture v1 file, then import with **Unity Volume Importer → Import Volume…**

OpenVDB is a trademark of LF Projects, LLC.

## Minimal writer pseudocode

```python
import struct

def write_uvi(path, nx, ny, nz, frames, bounds_min, bounds_max, density_frames):
    with open(path, "wb") as f:
        f.write(b"UVVT")
        f.write(struct.pack("<I", 1))  # version
        f.write(struct.pack("<IIIII", nx, ny, nz, len(density_frames), 1))
        for v in bounds_min:
            f.write(struct.pack("<f", v))
        for v in bounds_max:
            f.write(struct.pack("<f", v))
        for frame in density_frames:
            for value in frame:  # x-fastest, len = nx*ny*nz
                f.write(struct.pack("<f", value))
```

## Bundled CLI

This repository does not ship a Rust/C++ converter. Keep conversion tools in separate repositories to avoid pulling simulation dependencies into the Unity package.
