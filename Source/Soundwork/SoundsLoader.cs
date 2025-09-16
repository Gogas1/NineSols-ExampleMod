using HarmonyLib;
using NineSolsAPI.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using TPoVMod.Config;

namespace TPoVMod.Soundwork {
    internal static class SoundsLoader {
        internal static FieldInfo wwiseBankIdField = AccessTools.Field(typeof(WwiseObjectReference), "id");
        internal static FieldInfo wwiseBankObjectNameField = AccessTools.Field(typeof(WwiseObjectReference), "objectName");

        internal static GameObject[] LoadSounds() {
            CreateBankFile();
            return CreateBankObj();
        }

        private static GameObject[] CreateBankObj() {
            var bankObj = new GameObject(ModConfig.MOD_BANK_NAME);
            var akBank = bankObj.AddComponent<AkBank>();
            var data = akBank.data = new AK.Wwise.Bank();
            
            var bankRef = data.WwiseObjectReference = new WwiseBankReference();
            wwiseBankIdField.SetValue(bankRef, ModConfig.MOD_BANK_ID);
            wwiseBankObjectNameField.SetValue(bankRef, ModConfig.MOD_BANK_NAME);
            akBank.HandleEvent(null);

            return [bankObj];
        }

        private static void CreateBankFile() {
            var dataPath = Application.dataPath;

            string os = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) os = "Windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) os = "Mac";

            var bankFilesFolder = Path.Combine(dataPath, ModConfig.BANKS_LOCATION, os);

            var bankFile = AssemblyUtils.GetEmbeddedResource($"{ModConfig.MOD_BANK_RESOURCES_LOCATION}.{ModConfig.MOD_BANK_NAME}.bnk");

            var bankFilePath = Path.Combine(bankFilesFolder, $"{ModConfig.MOD_BANK_NAME}.bnk");

            if (!File.Exists(bankFilePath)) {
                File.WriteAllBytes(bankFilePath, bankFile);
            } else {
                var fileBytes = File.ReadAllBytes(bankFilePath);

                if (bankFile != null && fileBytes.Length != bankFile.Length && !ComputeHash(bankFile).SequenceEqual(ComputeHash(fileBytes))) {
                    File.WriteAllBytes(bankFilePath, bankFile);
                }
            }
        }

        private static byte[] ComputeHash(byte[] data) {
            using (var sha = SHA256.Create())
                return sha.ComputeHash(data);
        }
    }
}
