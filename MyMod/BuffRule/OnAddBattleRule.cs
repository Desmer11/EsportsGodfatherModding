//using HarmonyLib;
//using System;
//using UnityEngine;
//using Utility.GameSystem.LogicFrameworkX;
//using GameMain.BattleSystem;
//using LogicFramework.EffectorScript;
//using Utility;
//using BepInEx.Logging;
//using System.Collections.Generic;
//using Utility.MissionSystem;

//namespace LogicFramework.Patches
//{
//	[HarmonyPatch(typeof(BattleRule))]
//	[HarmonyPatch("OnAddBattleRule")]
//	public class BattleRuleOnAddPatch
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//			Logger?.LogInfo("OnAddBattleRule Starting");
//		}

//		[HarmonyPrefix]
//		public static void OnAddBattleRulePrefix(BattleRule __instance, BattleRule rule)
//		{
//			try
//			{
//				const string effectorGuidStr = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";
//				var effectorGuid = Guid.Parse(effectorGuidStr);

//				// 1. Get effector script
//				if (!EffectorScriptCenter.TryGetScript(effectorGuid, out EffectorScriptBase script))
//				{
//					Logger.LogError($"Effector script {effectorGuidStr} not found");
//					return;
//				}

//				// 2. Get battle entity (adjust based on your actual battle structure)
//				var battleEntity =rule.Battle.Entity;
//				if (battleEntity == null)
//				{
//					Logger.LogError("Failed to get battle LogicEntity");
//					return;
//				}

//				// 3. Create appropriate context
//				if (script.OpSetting is BehaviourOpSetting behaviourOp)
//				{
//					var context = EffectorScriptBase.ForBehaviour(battleEntity, behaviourOp);

//					// 4. Execute based on operation type
//					if (behaviourOp.IsMission)
//					{
//						var mission = script.InvokeMission(context);
//						//HandleMission(mission);
//					}
//					else
//					{
//						script.InvokeFunc(context);
//					}
//				}
//				else if (script.OpSetting is PropertyOpSetting propertyOp)
//				{
//					var context = EffectorScriptBase.ForProperty(battleEntity, propertyOp);
//					script.InvokeProperty(context);
//				}
//			}
//			catch (Exception ex)
//			{
//				Logger.LogError($"Error applying effector: {ex}");
//			}
//		}

//		//private static void HandleMission(Mission mission)
//		//{
//		//	if (script.OpSetting is BehaviourOpSetting behaviourOp && behaviourOp.IsMission)
//		//	{
//		//		var context = EffectorScriptBase.ForBehaviour(battleEntity, behaviourOp);
//		//		Mission mission1 = script.InvokeMission(context);

//		//		mission.OnCompleted += (success) =>
//		//		{
//		//			Logger.LogInfo($"Mission {mission.GetType().Name} completed: {success}");
//		//			// Add cleanup/result handling here
//		//		};

//		//		mission1.Start(); // Don't forget to start!

//		//		// Optional: Track ongoing missions
//		//		MissionTracker.Add(mission1);
//		//	}
//		//}
//	}
//}