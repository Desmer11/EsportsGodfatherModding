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
using CustomBattleRules;
using System.Reflection.Emit;
using System.Collections.Generic;
using Utility.LogSystem;
using ForceESCInitialization;

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

				Logger.LogInfo("AWAKE: System Patch initialized successfully.");
				Logger.LogInfo("AWAKE: LogEffectorScripts.\n");

				try
				{
					//CustomBattleRules.CollectScriptsPatch.EffectorScriptCenter.CollectScripts();
					ForceInitialize.InitializeEffectorScriptCenter();
					LogEffectorScripts();
				

					//var methodInfo = AccessTools.DeclaredMethod(typeof(Utility.GameSystem.LogicFrameworkX.EffectorScriptCenter), "CollectScripts");
					//if (methodInfo == null)
					//{
					//	Logger.LogInfo("Failed to locate CollectScripts(). Check method visibility.");
					//}
					//else
					//{
					//	Logger.LogInfo("CollectScripts() method found.");
					//}

					EffectorScriptCenterPatch.ApplyPatch();

					//foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
					//{
					//	Logger.LogInfo($"Loaded Assembly: {assembly.FullName}");
					//}


				}
				catch (Exception ex)
				{
					Logger.LogError($"Error collecting or logging Effector Scripts: {ex.Message}");
				}


			}
			catch (Exception ex)
			{
				Logger.LogError($"AWAKE: Initialization failed: {ex.Message}");
			}
			Logger.LogInfo("\n");
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



		private void InitializePatches(Harmony harmony)
		{
			try
			{
				AthleteDataComponentPatch.InitializeLogger(Logger);
				Logger.LogInfo("LOGGER HeropoolPatch Initialized LOGGER");

				//HeroBuffs.ChangeHeroVariantPatch.InitializeLogger(Logger);
				//Logger.LogInfo("LOGGER ChangeHeroVariantPatch initialized LOGGER.");

				//OnBattleEnteredPatch.InitializeLogger(Logger);
				//Logger.LogInfo("LOGGER OnBattleEnteredPatch initialized LOGGER.");

				EffectorScriptCenterPatch.InitializeLogger(Logger);
				Logger.LogInfo("LOGGER CollectScriptsPatch initialized LOGGER.");

				//BattleRule_RuleBonusPatch.C1.InitializeLogger(Logger);
				//Logger.LogInfo("LOGGER BattleRule_RuleBonus C1 initialized LOGGER.");

				//BattleRule_RuleBonusPatch.C2.InitializeLogger(Logger);
				//Logger.LogInfo("LOGGER BattleRule_RuleBonus C2 initialized LOGGER.");




				//BattleRuleOnAddPatch.InitializeLogger(Logger);
				//Logger.LogInfo("BuffRule.BattleRuleOnAddPatch initialized.");


				//Buffs.Patch_GetUnits.InitializeLogger(Logger);
				//Logger.LogInfo("MatchBuffs.Patch initialized.");


				//CustomRule.Patch_GetUnits2.InitializeLogger(Logger);
				//Logger.LogInfo("MatchBuffs.Patch initialized.");


				//MatchDespawn.Patch_SetBattleRunState.InitializeLogger(Logger);
				//Logger.LogInfo("Patch_OnReset.Patch initialized.");



				// Apply all Harmony patches
			
				try
				{
					harmony.PatchAll();
					Logger.LogInfo("Harmony patches applied.");
				}
				catch (Exception ex)
				{
					Logger.LogError($"Error applying Harmony patches: {ex}");
				}

			}
			catch (Exception ex)
			{
				Logger.LogError($"Error during patch initialization: {ex.Message}");
			}
		}
	}
}
