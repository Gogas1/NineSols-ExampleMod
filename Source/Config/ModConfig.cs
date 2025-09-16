using System;
using System.Collections.Generic;
using System.Text;

namespace TPoVMod.Config {
    internal static class ModConfig {
        internal const string BANKS_LOCATION = "StreamingAssets\\Audio\\GeneratedSoundBanks";
        internal const string MOD_BANK_RESOURCES_LOCATION = "TPoVMod.Resources";
        internal const string MOD_BANK_NAME = "TPoV_Bank";
        internal const uint MOD_BANK_ID = 2903215819;

        internal const string TARGET_SCEN_NAME_PART = "YiGung";

        internal const string MENU_RESUME_SOUND_PLAYER_PATH = "GameCore(Clone)/RCG LifeCycle/UIManager/GameplayUICamera/UI-Canvas/[Tab] MenuTab/MenuResumeSFX";
        internal const string MENU_RESUME_SOUND_PLAYER_ORIGINAL_EVENT = "MenuResumeSFX";
        internal const string MENU_RESUME_SOUND_PLAYER_REPLACEMENT_EVENT = "TPoV_MenuResume";

        internal const string MENU_PAUSE_SOUND_PLAYER_PATH = "GameCore(Clone)/RCG LifeCycle/UIManager/GameplayUICamera/UI-Canvas/[Tab] MenuTab/MenuPauseSFX";
        internal const string MENU_PAUSE_SOUND_PLAYER_ORIGINAL_EVENT = "MenuPauseSFX";
        internal const string MENU_PAUSE_SOUND_PLAYER_REPLACEMENT_EVENT = "TPoV_MenuPause";

        internal const string STATE_BGM_ACTIVATOR_PATH = "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/--[States]/FSM/[State] BossAngryToPhase3/[Action] 三階BGM";

        internal const string REPLACEMENT_EVENT = "TPoV_Replacement1";

        internal const string MY_WILL_SOUND_SOURCE_PATH = "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/--[States]/FSM/[State] BossFighting_Phase1/[Action] BGM->BattleBGM";
        internal const string THE_PATH_SOUND_SOURCE_PATH = "GameLevel/Room/Prefab/EventBinder/General Boss Fight FSM Object Variant/FSM Animator/LogicRoot/Boss三階BGM/BGM_Boss_A11_P3";
    }
}
