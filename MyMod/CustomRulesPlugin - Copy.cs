//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Reflection.Emit;
//using BepInEx.Logging;
//using GameMain.BattleSystem;
//using HarmonyLib;
//using LogicFramework.EffectorScript;
//using Mod;
//using Utility;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.LogSystem;


//namespace CustomBattleRules
//{
//	public static class CollectScriptsPatch
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}
//		// Postfix method
//		public static class EffectorScriptCenter
//		{
//			// Token: 0x06000584 RID: 1412 RVA: 0x00010E20 File Offset: 0x0000F020
//			static EffectorScriptCenter()
//			{
//				try
//				{
//					Logger?.LogInfo("EffectorScriptCenter static constructor starting...");
//					CollectScripts();
//					Logger?.LogInfo("EffectorScriptCenter static constructor completed successfully.");
//				}
//				catch (Exception ex)
//				{
//					Logger?.LogError($"EffectorScriptCenter initialization failed: {ex}");
//				}
//			}

//			// Token: 0x06000585 RID: 1413 RVA: 0x00010E34 File Offset: 0x0000F034
//			public static void CollectScripts()
//			{
//				if (AllTypes.Classes == null || !AllTypes.Classes.Any())
//				{
//					Logger?.LogError("AllTypes.Classes is null or empty. Initialization cannot proceed.");
//					return;
//				}

//				Logger?.LogInfo("Starting to collect EffectorScriptBase instances...");

//				foreach (var type in AllTypes.Classes)
//				{
//					try
//					{
//						var instance = Activator.CreateInstance(type) as EffectorScriptBase;
//						if (instance == null)
//						{
//							Logger?.LogWarning($"Failed to instantiate type: {type.FullName}");
//							continue;
//						}
//					}
//					catch (Exception ex)
//					{
//						Logger?.LogError($"Error instantiating type {type.FullName}: {ex}");
//					}
//				}

//				IEnumerable<EffectorScriptBase> enumerable = ((IEnumerable<Type>)AllTypes.Classes)
//					.WithBase<EffectorScriptBase>(includeBaseClass: false, includeAbstract: false)
//					.WithAttribute<EffectorScriptAttribute>(true)
//					.CreateInstances(true)
//					.OfType<EffectorScriptBase>();

//				EffectorScriptCenter.s_dict.Clear();


//				foreach (EffectorScriptBase effectorScriptBase in enumerable)
//				{
//					EffectorScriptCenter.s_dict.Add(effectorScriptBase.Id, effectorScriptBase);
//				}

//				AddCustomScripts();


//				LogBuilder logBuilder = LogRecorders.Setting.Log();
//				if (logBuilder == null)
//				{
//					return;
//				}
//				logBuilder.AppendBrief(string.Format("收集效果器脚本{0}个", EffectorScriptCenter.s_dict.Count)).Record(false);
//			}

//			private static void AddCustomScripts()
//			{
//				try
//				{
//					Logger?.LogInfo("Adding custom scripts...");
//					var customScripts = new List<EffectorScriptBase>
//					{
//						new BattleRule_RuleBonusPatch.C1(),
//						new BattleRule_RuleBonusPatch.C2()
//					};

//					foreach (var script in customScripts)
//					{
//						AddScript(script);
//					}
//				}
//				catch (Exception ex)
//				{
//					Logger?.LogError($"Error adding custom scripts: {ex}");
//				}
//			}

//			private static void AddScript(EffectorScriptBase script)
//			{
//				try
//				{
//					if (!s_dict.ContainsKey(script.Id))
//					{
//						s_dict.Add(script.Id, script);
//						Logger?.LogInfo($"Registered script: {script.Id}");
//					}
//					else
//					{
//						Logger?.LogWarning($"Script with ID {script.Id} already exists. Skipping.");
//					}
//				}
//				catch (Exception ex)
//				{
//					Logger?.LogError($"Error adding script {script.Id}: {ex.Message}");
//				}
//			}

//			// Token: 0x06000586 RID: 1414 RVA: 0x00010EDC File Offset: 0x0000F0DC
//			public static bool TryGetScript(Guid id, out EffectorScriptBase script)
//			{
//				return s_dict.TryGetValue(id, out script);
//			}

//			// Token: 0x04000233 RID: 563
//			private static readonly Dictionary<Guid, EffectorScriptBase> s_dict = new Dictionary<Guid, EffectorScriptBase>();
//		}
//	}
//}

