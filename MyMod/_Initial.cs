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
using LogicFramework.EffectorScript;
using BepInEx.Logging;
using HeroBuffs;
using Heropool;

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
				var harmony = new Harmony("com.system.patch");
				Logger.LogInfo("Initializing patches...");
				InitializePatches(harmony);

				//AssemblyRegister.ManualRegisterEffectorScripts();

				//EffectorScriptBase.RegisterAssembly(typeof(BuffRule).Assembly);

				//>>>>>>>>>>>>>>>>>>>> LOGGING BATTLE METHODS

				//var battleType = typeof(GameMain.BattleSystem.Battle);
				//foreach (var method in battleType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
				//{
				//	Logger?.LogInfo($"[Debug] Found method: {method.Name}");
				//}



				//foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
				//{
				//	Logger.LogDebug($"Loaded Assembly: {assembly.GetName().Name}");
				//}

			


				Logger.LogInfo("AWAKE: System Patch initialized successfully.");
				LogEffectorScripts();
				Logger.LogInfo("AWAKE: LogEffectorScripts.");

			}
			catch (Exception ex)
			{
				Logger.LogError($"Initialization failed: {ex.Message}");
			}
		}
		private void LogEffectorScripts()
		{
			Logger.LogInfo("Searching for EffectorScriptBase-derived scripts...");

			try
			{
				var effectorScriptType = typeof(EffectorScriptBase);
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();

				foreach (var assembly in assemblies)
				{
					try
					{
						foreach (var type in assembly.GetTypes())
						{
							if (effectorScriptType.IsAssignableFrom(type) && !type.IsAbstract)
							{
								Logger.LogInfo($"Found EffectorScript: {type.FullName}");
							}
						}
					}
					catch (ReflectionTypeLoadException ex)
					{
						Logger.LogWarning($"Failed to load some types in assembly {assembly.GetName().Name}: {ex.Message}");
						foreach (var loaderException in ex.LoaderExceptions)
						{
							Logger.LogWarning($"LoaderException: {loaderException.Message}");
						}
					}
					catch (Exception ex)
					{
						Logger.LogError($"Error processing assembly {assembly.GetName().Name}: {ex.Message}");
					}
				}

				Logger.LogInfo("EffectorScriptBase search completed.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Failed to log EffectorScriptBase-derived scripts: {ex.Message}");
			}
		}
		//[HarmonyPatch(typeof(EffectorScriptBase), "OnInvoke")]
		//static class EffectorScriptBase_OnInvoke_Patch
		//{
		//	static void Prefix(OpInvokeContext context, EffectorScriptBase __instance)
		//	{
		//		InitialPatch.Logger.LogInfo(
		//			$"Invoking {__instance.GetType().Name} ({__instance.Id}) on entity {context.InvokerEntity}"
		//		);
		//	}
		//}

		private void InitializePatches(Harmony harmony)
		{
			try
			{
				AthleteDataComponentPatch.InitializeLogger(Logger);
				Logger.LogInfo("HeropoolPatch.AthleteDataComponentPatch Initialized");

				//HeroBuffs.ChangeHeroVariantPatch.InitializeLogger(Logger);
				//Logger.LogInfo("HeroesBuffs.ChangeHeroVariantPatch initialized.");

				//BattleRule_RuleBonus.C1.InitializeLogger(Logger);
				//Logger.LogInfo("BattleRule_RuleBonus C1 initialized.");

				//BattleRule_RuleBonus.C2.InitializeLogger(Logger);
				//Logger.LogInfo("BattleRule_RuleBonus C2 initialized.");

				//AssemblyRegister.InitializeLogger(Logger);
				//Logger.LogInfo("BuffRule.AssemblyRegister initialized.");



				//BattleRuleOnAddPatch.InitializeLogger(Logger);
				//Logger.LogInfo("BuffRule.BattleRuleOnAddPatch initialized.");


				//Buffs.Patch_GetUnits.InitializeLogger(Logger);
				//Logger.LogInfo("MatchBuffs.Patch initialized.");


				//CustomRule.Patch_GetUnits2.InitializeLogger(Logger);
				//Logger.LogInfo("MatchBuffs.Patch initialized.");


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
