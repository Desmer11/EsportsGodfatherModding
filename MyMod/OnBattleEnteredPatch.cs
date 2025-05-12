//using System;
//using BepInEx.Logging;
//using HarmonyLib;
//using GameMain.BattleSystem;
//using Mod;
//using System.Reflection;

//namespace ScriptPatch
//{
//	[HarmonyPatch]
//	public static class OnBattleEnteredPatch
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}

//		// Specify the target method explicitly
//		static MethodBase TargetMethod()
//		{
//			// Use AccessTools to locate the explicit interface implementation
//			return AccessTools.Method("Mod.ModScrptBase:Mod.IModScrpt.OnBattleEntered");
//		}

//		// Postfix method
//		public static void Postfix(Battle battle, Mod.ModScrptBase __instance)
//		{
//			if (battle == null)
//			{
//				Logger?.LogWarning("OnBattleEntered called, but battle is null.");
//				return;
//			}

//			// Log information about the battle
//			Logger?.LogInfo($"Battle entered: {battle.GetType().Name}.");
//			Logger?.LogInfo($"Battle ID: {battle.Id}.");

//			try
//			{
//				// Custom logic for handling the battle
//				CustomLogicForBattle(battle, __instance);
//			}
//			catch (Exception ex)
//			{
//				Logger?.LogError($"An error occurred during custom logic: {ex.Message}");
//			}
//		}

//		private static void CustomLogicForBattle(Battle battle, Mod.ModScrptBase instance)
//		{
//			Logger?.LogInfo("Executing custom battle logic...");
//			// Example: Log Mod ID (safe to access via __instance)
//			Logger?.LogInfo($"Executing custom logic for Mod ID: {instance?.Mod?.Id ?? "Unknown"}");

//			// Invoke any additional methods or logic safely
//			CustomBattleRules.CollectScriptsPatch.EffectorScriptCenter.CollectScripts();
//		}
//	}
//}
