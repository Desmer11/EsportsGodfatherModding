//using System;
//using System.Collections.Generic;
//using BepInEx.Logging;
//using HarmonyLib;
//using LogicFramework.EffectorScript;
//using Utility.GameSystem.LogicFrameworkX;

//namespace CustomBattleRules
//{
//	[HarmonyPatch(typeof(EffectorScriptCenter), "CollectScripts")]
//	public static class EffectorScriptCenter_CollectScripts_Patch
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}

//		// Postfix patch to add custom scripts
//		public static void Postfix(Dictionary<Guid, EffectorScriptBase> ___s_dict)
//		{
//			Logger?.LogInfo("Adding Custom EffectorScriptBase instances...");

//			var customScripts = new List<EffectorScriptBase>
//			{
//				new BattleRule_RuleBonusPatch.C1(),
//				new BattleRule_RuleBonusPatch.C2()
//			};

//			foreach (var script in customScripts)
//			{
//				try
//				{
//					if (!___s_dict.ContainsKey(script.Id))
//					{
//						___s_dict.Add(script.Id, script);
//						Logger?.LogInfo($"Registered custom script: {script.Id}");
//					}
//					else
//					{
//						Logger?.LogWarning($"Script with ID {script.Id} already exists. Skipping.");
//					}
//				}
//				catch (Exception ex)
//				{
//					Logger?.LogError($"Error adding custom script {script.Id}: {ex.Message}");
//				}
//			}

//			Logger?.LogInfo($"Custom scripts added. Total scripts: {___s_dict.Count}");
//		}
//	}
//}
