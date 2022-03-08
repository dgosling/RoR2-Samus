using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.Data;
using Rewired.Data.Mapping;
using RoR2;

namespace SamusMod.Modules
{
    internal static class ExtraInputs
    {
        internal static void AddActionsToInputCatalog()
        {
            InputCatalog.actionToToken[RewiredAction.autoFire] = RewiredAction.autoFire.DisplayToken;
        }

        internal static void AddCustomActions(Action<UserData> orig, UserData self)
        {
            self.actions?.Add(RewiredAction.autoFire);

            var joystickMap = self.joystickMaps?.FirstOrDefault();
            var keyboardMap = self.keyboardMaps?.FirstOrDefault();

            FillActionMaps(RewiredAction.autoFire, keyboardMap, joystickMap);

            orig(self);
        }

        internal static void OnLoadUserProfiles(On.RoR2.SaveSystem.orig_LoadUserProfiles orig, SaveSystem self)
        {
            orig(self);

            foreach (var (name, userProfile) in self.loadedUserProfiles)
            {
                try
                {
                    AddMissingBindings(userProfile);
                    userProfile.RequestEventualSave();
                }
                catch(Exception e)
                {
                    SamusPlugin.logger.LogWarning($"Failed to add default bindings to '{name}' profile");
                    SamusPlugin.logger.LogError(e);
                }
            }
        }
internal static void OnLoadDefaultProfile(On.RoR2.UserProfile.orig_LoadDefaultProfile orig)
        {
            orig();

            try
            {
                AddMissingBindings(UserProfile.defaultProfile);
            }
            catch(Exception e)
            {
                SamusPlugin.logger.LogWarning($"Failed to add default bindings to default profile");
                SamusPlugin.logger.LogError(e);
            }
        }

        private static void AddMissingBindings(UserProfile userProfile)
        {
            AddActionMaps(RewiredAction.autoFire, userProfile);
        }
        private static void FillActionMaps(RewiredAction action, ControllerMap_Editor keyboardMap, ControllerMap_Editor joystickMap)
        {
            if (joystickMap != null && joystickMap.actionElementMaps.All(map => map.actionId != action.ActionId))
            {
                joystickMap.actionElementMaps.Add(action.DefaultJoystickMap);
            }
            if (keyboardMap != null && keyboardMap.actionElementMaps.All(map => map.actionId != action.ActionId))
            {
                keyboardMap.actionElementMaps.Add(action.DefaultKeyboardMap);
            }
        }
        private static void AddActionMaps(RewiredAction action,UserProfile userProfile)
        {
            if (userProfile.joystickMap.AllMaps.All(map => map.actionId != action.ActionId))
            {
                userProfile.joystickMap.AMpxarTReIdQulZiNSMMkbJtMRz(action.DefaultJoystickMap);
                action.DefaultJoystickMap.cNRtEejHCWUdzrhmpthsuslcVcs(userProfile.joystickMap);
            }

            if (userProfile.keyboardMap.AllMaps.All(map => map.actionId != action.ActionId))
            {
                userProfile.keyboardMap.AMpxarTReIdQulZiNSMMkbJtMRz(action.DefaultKeyboardMap);
                action.DefaultKeyboardMap.cNRtEejHCWUdzrhmpthsuslcVcs(userProfile.keyboardMap);
            }
        }

        private static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
        {
            key = pair.Key;
            value = pair.Value;
        }
    }

    public class RewiredAction
    {
        public static RewiredAction autoFire { get; }

        public int ActionId{ get; private set; }
        public string Name { get; private set; }
        public string DisplayToken { get; private set; }
        public KeyboardKeyCode DefaultKeyboardKey { get; private set; }
        public int DefaultJoystickKey { get; private set; }

        private InputAction inputAction;

        private ActionElementMap _defaultJoystickMap;
        public ActionElementMap DefaultJoystickMap
        {
            get
            {
               return _defaultJoystickMap ?? new ActionElementMap(ActionId, ControllerElementType.Button, DefaultJoystickKey, Pole.Positive, AxisRange.Full);
            }
        }

        private ActionElementMap _defaultKeyboardMap;
        public ActionElementMap DefaultKeyboardMap 
        {
            get
            {
                return _defaultKeyboardMap ?? new ActionElementMap(ActionId, ControllerElementType.Button, (int)DefaultKeyboardKey-21) { _keyboardKeyCode = DefaultKeyboardKey }; ;
            }
        
        
        }

        static RewiredAction()
        {
            autoFire = new RewiredAction
            {
                ActionId = 388,
                Name = "autoFireToggle",
                DisplayToken = "SAMUS_AUTOFIRE_TOGGLE",
                DefaultKeyboardKey = KeyboardKeyCode.G,
                DefaultJoystickKey = 16
            };
        }
        public static implicit operator InputCatalog.ActionAxisPair(RewiredAction action)
        {
            return new InputCatalog.ActionAxisPair(action.Name, AxisRange.Full);
        }
        public static implicit operator InputAction(RewiredAction action)
        {
            return action.inputAction ?? new InputAction
            {
                id = action.ActionId,
                name = action.Name,
                type = InputActionType.Button,
                descriptiveName = action.Name,
                behaviorId = 0,
                userAssignable = true,
                categoryId = 0
            };
        } 
    }
}
