# IMPORTANT

- This does nothing on without other mods. Please don't download this on its own and complain to me unless you really want to.

- Join the BadAssEmotes/EmotesAPI discord https://discord.gg/3kXkVkNDxs

- If you are unsure how to do animations, go to Settings->ModSettings->CustomEmotesAPI. From here you can customize your emote wheels and see keybinds.

## WIKI

- I have set up a wiki which should be more organized and in-depth than this readme has been previously

- [Wiki Link](https://github.com/ToastedOven/CustomEmotesAPI/wiki)

- [Code Documentation](https://github.com/ToastedOven/CustomEmotesAPI/wiki/Terrible-Documentation)

- [Old Readme if you just wanna see it](https://github.com/ToastedOven/CustomEmotesAPI/wiki/Old-Readme-if-you-just-wanna-look-at-it)

### Changelog

- Version 1.9.4: Probably fixed a scaling issue with anything other than 1080p, don't know how I missed this for so long

- Version 1.9.3: Removed HAND support since it's getting an update soon with official support and this would cause conflicts.

- Version 1.9.2: Fixed settings for the previous survivors not actually being used

- Version 1.9.1: Removed my debug calls, oops

- Version 1.9.0: Added support for Starstorm, Amp, Pathfinder, TF2Medic, and HAND. Fixed an issue where syncing emotes might not work if the host was using a character that doesn't support emotes

- Version 1.8.3 & 1.8.4: Added more verbose debugging in hopes I can fix the long-standing issue where audio sometimes doesn't stop properly.

- Version 1.8.2: Support for calling your own wwise PostEvents for playing sounds, check the wiki for more info. Goku and friends can emote now, it's a bit weird (they have very strong lower legs) but otherwise works fine.

- Version 1.8.1: Added support for the PlayableScavenger mod

- Version 1.8.0: Added UnlockBones and LockBones as public BoneMapper functions, if for some reason you ever find you want to stop an emote without actually stopping it, this is prob how. Added the animJoined event which you can subscribe to in code, this is mostly extra however it made stuff I'm working on a morbillion times easier to handle. On that note, if anyone ever wants something added to the API to help ease of emote creating, just reach out to me with a concrete idea/use case and I'll probably do it as long as I am capable.

- Version 1.7.1: Bonemappers can now see what EmoteLocations they have with BoneMapper.emoteLocations. This is reset automatically whenever a new animation plays.

- Version 1.7.0: Removed constant log spamming with unhelpful error whenever a BoneMapper get's destroyed. Fixed issue where on some floors the camera would get stuck in the floor during certain emotes. Changed the new default setting for emotes that have a parent GameObject setup, now teleports you slightly upwards, which is much more seemless of an experience, if you want to switch back to the node based system, simply switch the useSafePositionReset in your CustomAnimationClip to be true. Added a new class AnimationClipParams which contains the same info you have been passing into AddCustomAnimation. I recommend using this for any new emotes since it will be the only method to implement new functionality in the future. You can pass said class into AddCustomAnimation.

- Version 1.6.3: Fixed issue with Heretic skins breaking, emotes will not play however normal animations will be fine, proper fix soonTM. Added more verbose errors when adding emote skeletons.

- Version 1.6.2: I meant to add this earlier but w/e. Added an option under MISC to hide all join spots while you are animating. This is turned off by default because it removes visual clarity, but offers a more cinematic experience if you want that.

- Version 1.6.1: Added a few helper functions such as SetAnimationSpeed. Updated readme to be less uhh, bad, I have a wiki now, basically everything you need to know that used to be here is on the wiki, check it out!

- Version 1.6.0: New audio setup, you no longer get earblasted when multiple people do the same emote next to eachother. This however might come with a few audio bugs. I believe I got all of them but if anyone has any issues please reach out to me with a log/r2modman code.

- Version 1.5.4: Fixed joinspots not always getting deleted, probably fixed audio passing between stages randomly.

- Version 1.5.3: Actually did it, OOOOOPS

- Version 1.5.2: Added a BlackListEmote function for mod devs. This is a temporary fix until Rune finishes the UI overhaul, but it prevents an emote from being in random.

- Version 1.5.1: Updated visuals for JoinSpots

- Version 1.5.0: Added ability to have emotes be sequential (player 1 gets emoteA, player 2 gets emoteB, player 3 gets emoteC, etc...) You can implement this by having your join/start pref be -2. Added world props as an option. Added join spots as an option when setting up an emote or world props. Added emote rigs for Jinx and Soldier.

- Version 1.4.0: Fixed errors with upcoming mods. Removed Brass Contraption emote skeleton temporarily, will be readded when I suck less. Added support for auto-walking during animations (BoneMapper.SetAutoWalk()) and scaling of props based of bandit, aka scale your props with bandit for best results. (BoneMapper.ScaleProps()) not perfect but works most of the time. Added support for multiple audio events for an animation. This is unneeded if you don't sync your animation since you can just do the randomiztion in wwise. Probably fixed emote syncing.

- Version 1.3.6: Fixed incompatibility with my own mod because I am a dumbass.

- Version 1.3.5: Fixed issue with props being immediately deleted.

- Version 1.3.4: Fixed some of the underlying code, now properly allowing enemy injections without any issue (probably). As part of this, you can now call PlayAnimation on a specific BoneMapper.

- Version 1.3.3: Phoenix Wright and Scout (he already tposes around, I know he jank so dont @ me). Removed Dancer's experimental rig as it is now official supported by the mod creator. Added toggles for all experimental rigs under Mod Settings/BepinExConfig.

- Version 1.3.2: Added a toggle for Aurelion Sol in Mod Settings. He is very bug but still works if you really want that.

- Version 1.3.1: Added Dancer support. Fixed issue where a handfull of modded survivors wouldn't animate if they were a lobby player. Fixed conflict when Paladin has his cape turned on. Also added a fun toy for you animation creators, each BoneMapper now has a list of gameobjects called "props" which you can access, these are automatically cleared whenever an animation ends, so feel free to do with that as you will. I should really fix my documentation..........

- Version 1.3.0: Fixed Artificer and Void Fiend not animating normally. Added the following emote skeletons: The House, Henry, Aurelion Sol (damn boi he ugly), Katarina, Miner

- Version 1.2.3: The "Fine, I'll do it myself" update. Added emote skeletons for the following mods: Paladin, Enforcer/NemEnforcer, Chef, Holomancer, Sett, and Tracer. If any of these mod creators want to do it on their side, lemme know and I can remove it from here.

- Version 1.2.2: Fixed animations with an intro and loop, not playing the loop. Ooooops

- Version 1.2.1: Fixed default binds for the new buttons which I totally forgot about, please understand. Please rebind these if you downloaded 1.2.0

- Version 1.2.0: Made a change to how animations are imported, you WILL need to update your mods. If you are a mod dev, you should only need to recompile and you'll be good.

- Version 1.1.10: Changed the bone loading mode to accomidate certain models that add bones in ways not previously accounted for, this "should" be the last time this needs to be done.

- Version 1.1.9: Fixed footIK for emotes, everything will now be 10% less floaty.

- Version 1.1.8: Fixed a incompatibility with other mods that add bones inside of existing rigs.

- Version 1.1.7: Fixed issue where audio syncs didn't get deleted if multiple people are performing the same emote when someone dies.

- Version 1.1.6: Touched up commando's import model, it was the first one I ever did so his feet were kinda wack. Removed the auto rigging feature as I have already noticed serious potential problems it could cause in some survivors. It only worked on a couple of mods anyway.

- Version 1.1.5: Fixed issue with resolution scaling causing you to be stuck in the emote picker wheel. Added a sfx slider which I encourage everyone importing emotes to use if you have sound. The RTPC value is Volume_Emotes and it ranges from 0 to 100, starting at 50

- Version 1.1.4: Added advanced, experimental, rocket science, galaxy brain technology that will usually not work. But in some cases like with Sett, it will auto generate an animation rig if one isn't present. Fixed an issue with emotes getting desynced upon a player dying.

- Version 1.1.3: Fixed crippling performance from last patch. Hopefully everything is sorted for real this time. Admittidly the method is a bit jank. My apolocheese

- Version 1.1.2: Fixed the fix because I suck at coding :)

- Version 1.1.1: Fixed incompatibility issue with AutoSprintMod.

- Version 1.1.0: Fixed issue with less than perfect connections causing the emotewheel to lock up. Added the ability to sync audio and animation position of emotes. This changes how you import anim files. If anyone is currently working on an animation pack, you need to download the latest version and use it as a reference for your project.

- Version 1.0.1: Fixed error with armature importing. You shouldn't have to change anything on your end.

- Version 1.0.0: Initial Release
