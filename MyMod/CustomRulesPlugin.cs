using System;
using System.Reflection;
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
			var harmony = new Harmony("com.example.patch");

			// Check if the method exists
			var cctorMethod = AccessTools.DeclaredMethod(typeof(Utility.GameSystem.LogicFrameworkX.EffectorScriptCenter), ".cctor");
			if (cctorMethod == null)
			{
				Logger.LogInfo("Failed to locate .cctor method.");
				return;
			}
			else
			{
				Logger.LogInfo("Successfully located .cctor method.");
			}

			// Apply patch
			harmony.Patch(cctorMethod, postfix: new HarmonyMethod(typeof(EffectorScriptCenterPatch), nameof(Postfix_EffectorScriptCenter_Cctor)));
		}

		public static void Postfix_EffectorScriptCenter_Cctor()
		{
			Logger.LogInfo("EffectorScriptCenter static constructor patched successfully.");
		}
	}
}
