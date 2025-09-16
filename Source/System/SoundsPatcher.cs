using NineSolsAPI.Utils;
using RCGFSM.Audios;
using RCGFSM.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TPoVMod.System {
    internal static class SoundsPatcher {

        private static SoundPlayer? _menuPausePlayer;
        private static SoundPlayer? _menuResumePlayer;
        private static bool _hasReplacedUIEvents = false;

        public static void Subscribe() {
            Plugin.OnSceneLoaded += HandleSceneChange;
        }

        private static void HandleSceneChange(Scene scene, LoadSceneMode mode) {
            if(string.IsNullOrEmpty(scene.name)) {
                return;
            }

            if (scene.name.Contains(Config.ModConfig.TARGET_SCEN_NAME_PART)) {
                HandleUIEventsReplacement();
                HandleBGMEventsReplacement();
            }
            else {
                HandleUIEventsReset();
            }
        }

        private static void HandleUIEventsReplacement() {
            try {
                GetUISoundPlayers();

                _menuPausePlayer!.soundName = Config.ModConfig.MENU_PAUSE_SOUND_PLAYER_REPLACEMENT_EVENT;
                _menuResumePlayer!.soundName = Config.ModConfig.MENU_RESUME_SOUND_PLAYER_REPLACEMENT_EVENT;

                _hasReplacedUIEvents = true;
            } catch (Exception e) {
                Log.Error($"{e.Message}, {e.StackTrace}");
            }
        }

        private static void HandleUIEventsReset() {
            if (!_hasReplacedUIEvents) return;

            try {
                GetUISoundPlayers();

                _menuPausePlayer!.soundName = Config.ModConfig.MENU_PAUSE_SOUND_PLAYER_ORIGINAL_EVENT;
                _menuResumePlayer!.soundName = Config.ModConfig.MENU_RESUME_SOUND_PLAYER_ORIGINAL_EVENT;

                _hasReplacedUIEvents = false;
            } catch (Exception e) {
                Log.Error($"{e.Message}, {e.StackTrace}");
            }
        }

        private static void GetUISoundPlayers() {
            try {
                if (_menuPausePlayer == null) {
                    var playerObject = ObjectUtils.LookupPath(Config.ModConfig.MENU_PAUSE_SOUND_PLAYER_PATH);
                    _menuPausePlayer = playerObject!.GetComponent<SoundPlayer>();
                }

                if (_menuResumePlayer == null) {
                    var playerObject = ObjectUtils.LookupPath(Config.ModConfig.MENU_RESUME_SOUND_PLAYER_PATH);
                    _menuResumePlayer = playerObject!.GetComponent<SoundPlayer>();
                }
            } catch (Exception e) {
                Log.Error($"{e.Message}, {e.StackTrace}");
            }
        }

        private static void HandleBGMEventsReplacement() {
            try {
                var changeBGMObjectMyWill = ObjectUtils.LookupPath(Config.ModConfig.MY_WILL_SOUND_SOURCE_PATH);
                var changeBGMObjectThePath = ObjectUtils.LookupPath(Config.ModConfig.THE_PATH_SOUND_SOURCE_PATH);
                var stateBGMActivatorObject = ObjectUtils.LookupPath(Config.ModConfig.STATE_BGM_ACTIVATOR_PATH);

                var changeBGMAction = changeBGMObjectMyWill!.GetComponent<ChangeBGMAction>();
                var ambSource = changeBGMObjectThePath!.GetComponent<AmbienceSource>();

                if(changeBGMAction.dataList.Count > 0) {
                    var changeBGMData = changeBGMAction.dataList[0];
                    changeBGMData.ambienceTask.ambience = Config.ModConfig.REPLACEMENT_EVENT;
                    changeBGMData.bgmEntry._bgmName = Config.ModConfig.REPLACEMENT_EVENT;
                }

                ambSource.ambPair = new AmbPair() { sound = Config.ModConfig.REPLACEMENT_EVENT, type = AmbienceManager.AmbienceType.BGM };

                var gameOjectActivator = stateBGMActivatorObject!.GetComponent<GameObjectActivateAction>();
                gameOjectActivator.enableObj.Clear();
            } catch (Exception e) {
                Log.Error($"{e.Message}, {e.StackTrace}");
            }
        }
    }
}
