using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using NineSolsAPI;
using NineSolsAPI.Utils;
using TPoVMod.Soundwork;
using TPoVMod.System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TPoVMod;

[BepInDependency(NineSolsAPICore.PluginGUID)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {

    private Harmony harmony = null!;
    internal static Action<Scene, LoadSceneMode>? OnSceneLoaded;

    private void Awake() {
        Log.Init(Logger);
        RCGLifeCycle.DontDestroyForever(gameObject);

        var banks = SoundsLoader.LoadSounds();
        foreach (var bank in banks) {
            DontDestroyOnLoad(bank);
        }

        SceneManager.sceneLoaded += OnSceneChanged;

        SoundsPatcher.Subscribe();
        harmony = Harmony.CreateAndPatchAll(typeof(Plugin).Assembly);
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode) {
        OnSceneLoaded?.Invoke(scene, mode);
    }

    private void OnDestroy() {
        harmony.UnpatchSelf();
    }
}