//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using HarmonyLib;
//using BepInEx.Logging;
//using GameMain.BattleSystem;
//using GameMain.ClubSystem;
//using GameMain;
//using Utility.SettingSystem;
//using Mod;

//namespace Mod
//{
//	[HarmonyPatch(typeof(Battle), "EnterBattle")]
//	internal class Patch_EnterBattle
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}

//		// Postfix method to trigger mods after the battle starts
//		static void Postfix(Battle __instance)
//		{
//			if (__instance == null)
//			{
//				Logger?.LogWarning("Battle instance is null. Cannot trigger mod scripts.");
//				return;
//			}

//			try
//			{
//				Logger?.LogInfo("Battle started. Triggering OnBattleEntered for all registered mod scripts.");

//				foreach (var modScrpt in ModCenter.ModScripts)
//				{
//					try
//					{
//						modScrpt.OnBattleEntered(__instance);
//						Logger?.LogInfo($"Triggered OnBattleEntered for mod: {modScrpt.Mod?.Name ?? "Unnamed Mod"}");
//					}
//					catch (Exception ex)
//					{
//						Logger?.LogError($"Error in mod script {modScrpt.Mod?.Name}: {ex.Message}\n{ex.StackTrace}");
//					}
//				}
//			}
//			catch (Exception ex)
//			{
//				Logger?.LogError($"An unexpected error occurred in Patch_EnterBattle: {ex.Message}\n{ex.StackTrace}");
//			}
//		}
//	}
//	public class ExampleModScript : IModScrpt
//	{
//		public ModCenter.Mod Mod { get; set; }

//		public void OnModInit(ModInitContext context)
//		{
//			// Initialize the mod
//		}

//		public void OnBattleEntered(Battle battle)
//		{
//			// Custom logic for when a battle starts
//			Console.WriteLine($"Battle started: {battle.Name}");
//		}

//		public void OnSettingObjectLoaded(SettingObjectLoadedContext context) { }
//		public void OnGameEntered(Game game) { }
//		public void OnClubAffairEntered(ClubAffair affair) { }
//		public void OnBattleExited(Battle battle) { }
//	}

//}
