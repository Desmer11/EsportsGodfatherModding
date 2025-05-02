using HarmonyLib;
using BepInEx;
using System;
using Utility;
using Utility.GameSystem.SaveSystem;
using GameMain.AthleteSystem;
using System.Reflection;
using LogicFramework;
using MessagePack;
using Utility.GameSystem.LogicFrameworkX;
using GameMain.FightSystem;
using MatchBuffs;
using HeroesBuffs;
using BepInEx.Logging;

namespace Initial
{
	[BepInPlugin("com.System.patch", "System Patch", "1.0.0")]
	public class InitialPatch : BaseUnityPlugin
	{
		private static ManualLogSource Logger;

		private void Awake()
		{
			Logger = BepInEx.Logging.Logger.CreateLogSource("System Patch");
			Logger.LogInfo("Initializing System Patch...");
			try
			{
				Harmony.DEBUG = true;
				// Create and register Harmony instance
				var harmony = new Harmony("com.system.patch");
				Logger.LogInfo("Initializing patches...");

				// Initialize and apply all patches
				InitializePatches(harmony);

				//>>>>>>>>>>>>>>>>>>>> LOGGING BATTLE METHODS

				//var battleType = typeof(GameMain.BattleSystem.Battle);
				//foreach (var method in battleType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
				//{
				//	Logger?.LogInfo($"[Debug] Found method: {method.Name}");
				//}






				Logger.LogInfo("System Patch initialized successfully.");

				foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					Logger.LogDebug($"Loaded Assembly: {assembly.GetName().Name}");
				}

			}
			catch (Exception ex)
			{
				Logger.LogError($"Initialization failed: {ex.Message}");
			}
		}

		private void InitializePatches(Harmony harmony)
		{
			try
			{
				HeroPoolPatch.AthleteDataComponentPatch.InitializeLogger(Logger);
				Logger.LogInfo("HeropoolPatch.AthleteDataComponentPatch Initialized");

				HeroesBuffs.ChangeHeroVariantPatch.InitializeLogger(Logger);
				Logger.LogInfo("HeroesBuffs.ChangeHeroVariantPatch initialized.");

				MatchBuffs.Patch_GetUnits.InitializeLogger(Logger);
				Logger.LogInfo("MatchBuffs.Patch initialized.");

				//MatchDespawn.Patch_SetBattleRunState.InitializeLogger(Logger);
				//Logger.LogInfo("Patch_OnReset.Patch initialized.");



				// Apply all Harmony patches
				harmony.PatchAll();
				Logger.LogInfo("Harmony patches applied.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error during patch initialization: {ex.Message}");
			}
		}
	}
}
