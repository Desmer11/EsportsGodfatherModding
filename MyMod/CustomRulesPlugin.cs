using System;
using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using Utility.GameSystem.LogicFrameworkX;

namespace CustomBattleRules
{
	[HarmonyPatch]
	public static class EffectorScriptCenterPatch
	{
		private static ManualLogSource Logger;

		public static void InitializeLogger(ManualLogSource logger)
		{
			Logger = logger;
		}

		public static void ApplyPatch()
		{
			TestStaticInitializer();
			InvokeCollectScripts();
			AccessSDictField();
		}
		private static void TestStaticInitializer()
		{
			try
			{
				// Use reflection to get fields or methods without triggering static constructor
				var field = AccessTools.Field(typeof(EffectorScriptCenter), "s_dict");
				var method = AccessTools.Method(typeof(EffectorScriptCenter), "CollectScripts");
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error bypassing static initializer: {ex.Message}");
			}
			try
			{
				var type = typeof(EffectorScriptCenter);
				System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(type.TypeHandle);
				Logger.LogInfo("EffectorScriptCenter static constructor executed successfully.");
			}
			catch (Exception ex)
			{
				Logger.LogError($"EffectorScriptCenter static constructor threw an exception: {ex.Message}");
				if (ex.InnerException != null)
				{
					Logger.LogError($"Inner exception: {ex.InnerException.Message}");
					Logger.LogError($"Stack trace: {ex.InnerException.StackTrace}");
				}
			}
		}

		private static void InvokeCollectScripts()
		{
			try
			{
				var collectScriptsMethod = AccessTools.Method(typeof(EffectorScriptCenter), "CollectScripts");
				if (collectScriptsMethod != null)
				{
					collectScriptsMethod.Invoke(null, null);
					Logger.LogInfo("Successfully invoked CollectScripts method.");
				}
				else
				{
					Logger.LogError("Failed to locate CollectScripts method.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error invoking CollectScripts: {ex.Message}");
				if (ex.InnerException != null)
				{
					Logger.LogError($"Inner exception: {ex.InnerException.Message}");
					Logger.LogError($"Stack trace: {ex.InnerException.StackTrace}");
				}
			}
		}

		private static void AccessSDictField()
		{
			try
			{
				var sDictField = AccessTools.Field(typeof(EffectorScriptCenter), "s_dict");
				if (sDictField != null)
				{
					var sDict = sDictField.GetValue(null) as Dictionary<Guid, EffectorScriptBase>;
					if (sDict != null)
					{
						Logger.LogInfo($"Successfully accessed s_dict with {sDict.Count} entries.");
						foreach (var entry in sDict)
						{
							Logger.LogInfo($"Entry: ID = {entry.Key}, Script = {entry.Value?.GetType().FullName}");
						}
					}
					else
					{
						Logger.LogError("s_dict is null or could not be cast to the expected type.");
					}
				}
				else
				{
					Logger.LogError("Failed to locate s_dict field.");
				}
			}
			catch (Exception ex)
			{
				Logger.LogError($"Error accessing s_dict field: {ex.Message}");
			}
		}
	}
}
