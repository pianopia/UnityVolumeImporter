# Booth packaging notes

Guidance for preparing **Unity Volume Importer** as a free Booth listing (Louddin free tools pattern).

## Package identity

| Field | Value |
|-------|-------|
| Product name | Unity Volume Importer |
| UPM id | `com.louddin.unity-volume-importer` |
| Version | See `package.json` |
| License | MIT (`LICENSE`) |

## Distribution options

### Option A — Git URL (recommended for developers)

List the UPM git URL in the Booth description:

```
https://github.com/pianopia/UnityVolumeImporter.git?path=Packages/com.louddin.unity-volume-importer
```

Users add it via **Package Manager → + → Add package from git URL…**

### Option B — UPM zip

Create a zip of the package folder only:

```bash
cd Packages
zip -r ../dist/com.louddin.unity-volume-importer-<version>.zip com.louddin.unity-volume-importer
```

Upload the zip to Booth. Users extract into `Packages/` or use **Add package from disk…** on the included `package.json`.

### Option C — `.unitypackage`

1. Open a blank Unity 6 project with the package installed under `Packages/`.
2. Select the `Packages/com.louddin.unity-volume-importer` folder in the Project window.
3. **Assets → Export Package…** — include dependencies off (package is self-contained).
4. Name: `UnityVolumeImporter-<version>.unitypackage`.

Note: `.unitypackage` exports flatten paths under `Assets/`; prefer UPM zip or git URL when possible so updates are easier.

## Booth listing checklist

- [ ] Title: **Unity Volume Importer** (no OpenVDB in product title)
- [ ] Short blurb: dense volume import + preview for Unity 6; `.vdb` on roadmap
- [ ] Link to GitHub repository
- [ ] Attach UPM zip or `.unitypackage` (optional if git URL is primary)
- [ ] Mention Unity 6000.x requirement
- [ ] Trademark line where OpenVDB is mentioned: *OpenVDB is a trademark of LF Projects, LLC.*

## Version bumps

1. Update `Packages/com.louddin.unity-volume-importer/package.json` version.
2. Add entry to `CHANGELOG.md`.
3. Rebuild zip / `.unitypackage`.
4. Update Booth files and description version string.

## Repository layout (reference)

```
UnityVolumeImporter/
├── Packages/com.louddin.unity-volume-importer/   ← UPM package root
├── README.md
├── QUICKSTART.md
├── CHANGELOG.md
├── LICENSE
├── docs/
│   ├── known-limitations.md
│   ├── booth-packaging.md
│   └── volume-texture-v1.md
└── tools/
    └── README.md
```
