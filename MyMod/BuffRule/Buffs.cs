//using System;
//using System.Linq;
//using HarmonyLib;
//using BepInEx.Logging;
//using System.Reflection;
//using System.Collections;
//using System.Collections.Generic;
//using GameMain.BattleSystem;
//using GameMain.UnitSystem;
//using Utility.ValueStruct;
//using LogicFramework;
//using UnityEngine;
//using Utility.GameSystem.LogicFrameworkX;
//using BattleMainUI;
//using Utility.PoolSystem;
//using LogicFramework.EffectorScript;
//using static UnityEngine.GraphicsBuffer;
//using Utility;
//using GameMain.UnitSystem.Equipment;

//namespace Buffs
//{
//	[HarmonyPatch(typeof(Battle), "GetUnits", new[] { typeof(Predicate<Unit>) })]
//	public class Patch_GetUnits
//	{
//		private static ManualLogSource Logger;
//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}

//		static void Postfix(Battle __instance, Predicate<Unit> predicate, ref IEnumerable<Unit> __result)
//		{
//			if (__instance == null || __result == null)
//			{
//				Logger?.LogWarning("Battle instance or result units collection is null. Skipping buff application.");
//				return;
//			}

//			Logger?.LogInfo("GetUnits triggered. Iterating over units for potential buff application.");

//			var unitsList = __result.Where(unit => unit.IsHero).ToList();

//			Logger?.LogInfo($"Total units found: {unitsList.Count}");

//			if (unitsList.Count < 2)
//			{
//				Logger?.LogInfo($"Not enough hero units available for buff/nerf application {unitsList}.");
//				return;
//			}
//			try
//			{
//				var random = new System.Random();
//				var selectedUnits = unitsList.OrderBy(_ => random.Next()).Take(2).ToArray();
//				Logger?.LogInfo($"\n[BuffApply] BEFORE BUFF  {selectedUnits[0].Name}:\n " +
//								$"GoldRatio: {selectedUnits[0].BattleGainGoldRatio},\n " +
//									$"Gold: {selectedUnits[0].Gold},\n " +
//								$"Health: {selectedUnits[0].HPMax},\n " +
//								$"Armor: {selectedUnits[0].Armor},\n " +
//								$"Attack: {selectedUnits[0].Attack},\n " +
//								$"Crit: {selectedUnits[0].CritChance}\n" +
//								$"EcoE:{selectedUnits[0].Athlete.AthleteEcoEfficiency}\n");

//				Logger?.LogInfo($"\n[BuffApply] BEFORE BUFF  {selectedUnits[1].Name}:\n " +
//								$"GoldRatio: {selectedUnits[1].BattleGainGoldRatio},\n " +
//									$"Gold: {selectedUnits[1].Gold},\n " +
//								$"Health: {selectedUnits[1].HPMax},\n " +
//								$"Armor: {selectedUnits[1].Armor},\n " +
//								$"Attack: {selectedUnits[1].Attack},\n " +
//								$"Crit: {selectedUnits[1].CritChance}\n" +
//								$"EcoE:{selectedUnits[1].Athlete.AthleteEcoEfficiency}\n");

//				ApplyBuff(selectedUnits[0]);
//				ApplyBuff(selectedUnits[1]);
//			}
//			catch (Exception ex)
//			{
//				Logger?.LogError($"Error during buff/nerf application: {ex.Message}\n{ex.StackTrace}");
//			}
//			Logger?.LogInfo($"PropertyOps.GainGoldRatio_Base: {LogicEntity_Unit.PropertyOps.GainGoldRatio_Base}");
//			if (LogicEntity_Unit.PropertyOps.GainGoldRatio_Base == null)
//			{
//				Logger?.LogError("GainGoldRatio_Base is null.");
//			}
//		}


//		private static void ApplyBuff(Unit unit)
//		{


//			Logger?.LogInfo($"Buffs applied to {unit.Name}: " +
//								$"CritChance = {unit.CritChance}, " +
//								$"Attack = {unit.Attack}");

//			// Log the final stats after buff application
//			Logger?.LogInfo($"[BuffApply] BUFFED {unit.Name}:\n" +
//							 $"GoldRatio: {unit.BattleGainGoldRatio}, " +
//							 $"Gold: {unit.Gold}, " +
//							 $"Health: {unit.HPMax}, " +
//							 $"Armor: {unit.Armor}, " +
//							 $"Attack: {unit.Attack}, " +
//							 $"Crit: {unit.CritChance}, " +
//							 $"EcoE: {unit.Athlete.AthleteEcoEfficiency}");
//		}
//	}
//}




