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
using HeroBuffs;
using BepInEx.Logging;

namespace Heropool
{
	//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> HeropoolPatch
	[HarmonyPatch(typeof(Athlete.DataComponent))]
	public static class AthleteDataComponentPatch
	{
		private static ManualLogSource Logger;
		public static void InitializeLogger(ManualLogSource logger)
		{
			Logger = logger;
		}

		[HarmonyPatch("SetHeroPoolMaxRandom")]
		[HarmonyPostfix]
		static void PostfixSetHeroPoolMaxRandom(Athlete.DataComponent __instance)
		{


			// Access the hero pool max value that was set
			int currentMax = __instance.GetHeroPoolMax();

			// Ensure it doesn't exceed 4
			if (currentMax > 4)
			{
				__instance.SetHeroPoolMax(4);
				Logger?.LogInfo("SetHeroPoolMaxRandom value limited to 4");
			}
		}

		[HarmonyPatch(typeof(Athlete.DataComponent), "GetHeroDataCount")]
		public static class Patch_GetHeroDataCount
		{
			// Postfix to modify the returned count
			[HarmonyPostfix]
			public static void Postfix(ref int __result)
			{
				if (__result > 4)
				{
					__result = 4; // Limit the count to 4
					Logger?.LogInfo("GetHeroDataCount limited to 4.");
				}
			}
		}

		//------------------------------------------------------------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>-----SAVEDATA

		[HarmonyPatch(typeof(Athlete.SaveData), "OnSaveData")]
		public static class AthleteSaveDataOnSavePatch
		{
			[HarmonyPostfix]
			public static void Postfix(Athlete.SaveData __instance)
			{
				// Limit heroPoolCount to 4 during saving
				if (__instance.heroPoolCount > 4)
				{
					__instance.heroPoolCount = 4;
					Logger?.LogInfo("Hero pool count limited to 4 during saving.");
				}
			}
		}

		[HarmonyPatch(typeof(Athlete.SaveData), "OnLoadObj")]
		public static class AthleteSaveDataOnLoadPatch
		{
			[HarmonyPostfix]
			public static void Postfix(Athlete.SaveData __instance)
			{
				// Limit heroPoolCount to 4 during loading
				if (__instance.heroPoolCount > 4)
				{
					__instance.heroPoolCount = 4;
					Logger?.LogInfo("Hero pool count limited to 4 during loading.");
				}
			}
		}
	}
}
