//using System;
//using System.Linq;
//using System.Reflection;
//using BepInEx.Logging;
//using HarmonyLib;

//namespace MethodsLog
//{
//	[HarmonyPatch(typeof(GameMain.BattleSystem.Battle), "Init")] 
//	public static class BattleSystemInitPatch
//	{
//		private static ManualLogSource Logger;

//		[HarmonyPostfix]
//		public static void Postfix()
//		{
//			// Log all methods after Battle.Init
//			MethodsLogger.LogBattleMethods();
//		}

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}
//	}

//	public static class MethodsLogger
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}

//		public static void LogBattleMethods()
//		{
//			if (Logger == null)
//			{
//				Console.WriteLine("Logger is not initialized."); // Fallback log for debugging
//				return;
//			}

//			try
//			{
//				// Find the Battle type dynamically
//				Type battleType = AppDomain.CurrentDomain
//					.GetAssemblies()
//					.Select(a => a.GetType("GameMain.BattleSystem.Battle"))
//					.FirstOrDefault(t => t != null);

//				if (battleType == null)
//				{
//					Logger.LogError("Failed to load Battle type. Ensure the namespace and assembly are correct.");
//					return;
//				}

//				Logger.LogInfo($"Found Battle type: {battleType.FullName}");

//				// Retrieve and log all methods of the Battle type
//				var methods = battleType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy);

//				if (!methods.Any())
//				{
//					Logger.LogWarning("No methods found in the Battle type.");
//					return;
//				}

//				Logger.LogInfo($"Logging {methods.Length} methods for type: {battleType.FullName}");

//				foreach (var method in methods)
//				{
//					string parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
//					Logger.LogInfo($"Method: {method.Name}({parameters}) | Attributes: {method.Attributes}");
//				}
//			}
//			catch (Exception ex)
//			{
//				Logger.LogError($"Error during method logging: {ex}");
//			}
//		}
//	}
//}
