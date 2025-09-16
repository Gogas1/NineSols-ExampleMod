using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace TPoVMod.Patches {
    [HarmonyPatch(typeof(SoundManager))]
    internal static class SoundManagerPatches {

        [HarmonyPatch(nameof(SoundManager.PlaySound))]
        [HarmonyPostfix]
        private static void PlaySoundPostfix(ref uint __result, string soundName) {
            Log.Warning($"{__result}: {soundName}");
        }
    }
}
