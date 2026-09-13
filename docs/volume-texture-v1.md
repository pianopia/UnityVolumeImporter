# Volume Texture v1

Binary interchange format for dense 3D density volumes used by **Unity Volume Importer**.

## Identifiers

| Item | Value |
|------|-------|
| Format name | `uvi_volume_texture_v1` |
| Preferred extension | `.uvi` |
| Legacy extension | `.evol` (same layout, `EFVT` magic) |
| Version | `1` |

## File layout

```
Offset  Size  Field
------  ----  -----
0       4     Magic: "UVVT" (neutral) or "EFVT" (legacy)
4       4     Version (uint32 LE) = 1
8       4     nx (uint32 LE)
12      4     ny (uint32 LE)
16      4     nz (uint32 LE)
20      4     frame_count (uint32 LE)
24      4     channels (uint32 LE) — Phase 1 uses 1 (density)
28      12    bounds_min (3 × float32 LE, XYZ meters)
40      12    bounds_max (3 × float32 LE, XYZ meters)
52      …     Payload: frame_count × (nx × ny × nz × channels) float32 LE values
```

**Header size:** 52 bytes.

**Voxel order:** x fastest, then y, then z (column-major within each frame). Values are little-endian `float32` density samples.

## Multi-frame files

Frames are stored sequentially in the payload. Frame `f` starts at byte offset:

```
52 + f × (nx × ny × nz × channels × 4)
```

## Writing new files

New exporters should use:

- Magic: `UVVT`
- Extension: `.uvi`
- Format string in metadata: `uvi_volume_texture_v1`

Readers in this package accept both `UVVT` and `EFVT` magics.

## Example header (conceptual)

A 64³ single-frame smoke grid from (-4, 0, -4) to (4, 8, 4):

```
magic:      UVVT
version:    1
nx,ny,nz:   64, 64, 64
frames:     1
channels:   1
bounds_min: (-4.0, 0.0, -4.0)
bounds_max: (4.0, 8.0, 4.0)
payload:    64³ floats ≈ 1 MB
```

## See also

- [tools/README.md](../tools/README.md) — external conversion pointers
- [known-limitations.md](known-limitations.md) — import scope
