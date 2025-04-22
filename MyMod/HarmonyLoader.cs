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



namespace GameMain.AthleteSystem
{
	[BepInPlugin("com.example.athletepatch", "Athlete System Patch", "1.0.0")]
	public class AthletePatchPlugin : BaseUnityPlugin
	{
		private void Awake()
		{
			// Initialize Harmony and apply all patches
			var harmony = new Harmony("com.example.athletepatch");
			harmony.PatchAll();
			Logger.LogInfo("Athlete System Patch Loaded");
		}
	}

	[HarmonyPatch(typeof(Athlete.DataComponent))]
	public static class AthleteDataComponentPatch
	{
		// Patch SetHeroPoolMaxRandom to ensure the random value doesn't exceed 4
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
				Console.WriteLine("SetHeroPoolMaxRandom value limited to 4");
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
					Console.WriteLine("GetHeroDataCount limited to 4.");
				}
			}
		}

		//------------------------------------------------------------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>-----SAVEDATA

		[HarmonyPatch(typeof(GameMain.AthleteSystem.Athlete.SaveData), "OnSaveData")]
		public static class AthleteSaveDataOnSavePatch
		{
			[HarmonyPostfix]
			public static void Postfix(GameMain.AthleteSystem.Athlete.SaveData __instance)
			{
				// Limit heroPoolCount to 4 during saving
				if (__instance.heroPoolCount > 4)
				{
					__instance.heroPoolCount = 4;
					Console.WriteLine("Hero pool count limited to 4 during saving.");
				}
			}
		}

		[HarmonyPatch(typeof(GameMain.AthleteSystem.Athlete.SaveData), "OnLoadObj")]
		public static class AthleteSaveDataOnLoadPatch
		{
			[HarmonyPostfix]
			public static void Postfix(GameMain.AthleteSystem.Athlete.SaveData __instance)
			{
				// Limit heroPoolCount to 4 during loading
				if (__instance.heroPoolCount > 4)
				{
					__instance.heroPoolCount = 4;
					Console.WriteLine("Hero pool count limited to 4 during loading.");
				}
			}
		}

		//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> MESSAGEPACKPATCH








	}
}
