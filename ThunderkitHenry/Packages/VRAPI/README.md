# Add VR compatibility to your mod!
This API allows for simple interactions with the VR mod and its features.

## For mod developpers:
You can create custom hands for your character or support custom skins by importing the [RoR2VR Devkit unity package](https://github.com/DrBibop/RoR2VRMod/releases) into a ThunderKit project.

The documentation for the API and the devkit can be found in the [GitHub wiki](https://github.com/DrBibop/RoR2VRMod/wiki).

You can also watch the [video tutorials](https://www.youtube.com/playlist?list=PLAvCVBah7RrLYmnEVApu4PqIdi3tXI4YT) once they are available.

# Changelog

### 1.0.0
- Initial release.

### 1.0.1
- Fixed for Survivor of the Void update.

### 1.1.0
- Added custom skin support.
- Added the onHandPairSet event that triggers when hands for the player character are loaded.
- AddSkillRemap has been deprecated and replaced by AddSkillBindingOverride where you can directly assign skills to buttons.