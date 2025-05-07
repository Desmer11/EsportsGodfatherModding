//using System;
//using System.Linq;
//using HarmonyLib;
//using BepInEx.Logging;
//using System.Reflection;
//using System.Collections;
//using System.Collections.Generic;
//using GameMain.BattleSystem;
//using GameMain.UnitSystem;
//using Utility.ValueStruct;
//using LogicFramework;
//using UnityEngine;
//using Utility.GameSystem.LogicFrameworkX;
//using BattleMainUI;
//using Utility.PoolSystem;
//using MatchBuffs;

//namespace MatchDespawn
//{
//[HarmonyPatch(typeof(Battle), "SetBattleRunState")]
//[HarmonyPatch(new Type[] { typeof(BattleRunStates) })]
//internal class Patch_SetBattleRunState
//	{
//    static ManualLogSource Logger;

//    public static void InitializeLogger(ManualLogSource logger)
//    {
//        Logger = logger;
//    }

//    static void Postfix(Battle __instance)
//    {
//        if (__instance == null)
//        {
//            Logger?.LogWarning("Battle instance was null on reset—skipping buff reset.");
//            return;
//        }

//        try
//        {
//            Logger?.LogInfo($"Resetting buff state. Current RunState: {__instance.RunState}");

//                Patch_GetUnits.ResetBuffState();

//                Logger?.LogInfo("Buff state reset complete.");
//        }
//        catch (Exception ex)
//        {
//            Logger?.LogError($"An error occurred during the buff state reset: {ex.Message}\n{ex.StackTrace}");
//        }
//    }
//}
//}
