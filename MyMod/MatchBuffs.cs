using System;
using System.Linq;
using HarmonyLib;
using BepInEx.Logging;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using GameMain.BattleSystem;
using GameMain.UnitSystem;
using Utility.ValueStruct;
using LogicFramework;
using UnityEngine;
using Utility.GameSystem.LogicFrameworkX;
using BattleMainUI;
using Utility.PoolSystem;
using LogicFramework.EffectorScript;
using static UnityEngine.GraphicsBuffer;
using Utility;
using GameMain.UnitSystem.Equipment;

namespace MatchBuffs
{
	[HarmonyPatch(typeof(Battle), "GetUnits", new[] { typeof(Predicate<Unit>) })]
	public class Patch_GetUnits
	{
		//private static bool hasBuffsApplied = false;

		private static ManualLogSource Logger;
		public static void InitializeLogger(ManualLogSource logger)
		{
			Logger = logger;
		}

		static void Postfix(Battle __instance, Predicate<Unit> predicate, ref IEnumerable<Unit> __result)
		{
			//if (hasBuffsApplied == true)
			//{
			//	Logger?.LogInfo($"Buff logic already executed. Skipping further application {hasBuffsApplied}");
			//	return;
			//}

			if (__instance == null || __result == null)
			{
				Logger?.LogWarning("Battle instance or result units collection is null. Skipping buff application.");
				return;
			}
			// Skip execution if buffs have already been applied

			Logger?.LogInfo("GetUnits triggered. Iterating over units for potential buff application.");

			// Convert the enumerable to a list and filter for heroes
			var unitsList = __result.Where(unit => unit.IsHero).ToList();

			// Log the number of units found
			Logger?.LogInfo($"Total units found: {unitsList.Count}");

			// Apply buffs if there are enough units
			if (unitsList.Count < 2)
			{
				Logger?.LogInfo($"Not enough hero units available for buff/nerf application {unitsList}.");
				return;
			}

			try
			{
				var random = new System.Random();

				// Randomly select two hero units from the list
				var selectedUnits = unitsList.OrderBy(_ => random.Next()).Take(2).ToArray();

				// Apply buffs and nerfs
				Logger?.LogInfo($"\n[BuffApply] BEFORE BUFF  {selectedUnits[0].Name}:\n " +
								$"GoldRatio: {selectedUnits[0].BattleGainGoldRatio},\n " +
									$"Gold: {selectedUnits[0].Gold},\n " +
								$"Health: {selectedUnits[0].HPMax},\n " +
								$"Armor: {selectedUnits[0].Armor},\n " +
								$"Attack: {selectedUnits[0].Attack},\n " +
								$"Crit: {selectedUnits[0].CritChance}\n" +
								$"EcoE:{selectedUnits[0].Athlete.AthleteEcoEfficiency}\n");

				Logger?.LogInfo($"\n[BuffApply] BEFORE BUFF  {selectedUnits[1].Name}:\n " +
								$"GoldRatio: {selectedUnits[1].BattleGainGoldRatio},\n " +
									$"Gold: {selectedUnits[1].Gold},\n " +
								$"Health: {selectedUnits[1].HPMax},\n " +
								$"Armor: {selectedUnits[1].Armor},\n " +
								$"Attack: {selectedUnits[1].Attack},\n " +
								$"Crit: {selectedUnits[1].CritChance}\n" +
								$"EcoE:{selectedUnits[1].Athlete.AthleteEcoEfficiency}\n");

				ApplyBuff(selectedUnits[0]);
				ApplyBuff(selectedUnits[1]);

				//hasBuffsApplied = true;
			}
			catch (Exception ex)
			{
				Logger?.LogError($"Error during buff/nerf application: {ex.Message}\n{ex.StackTrace}");
			}


			Logger?.LogInfo($"PropertyOps.GainGoldRatio_Base: {LogicEntity_Unit.PropertyOps.GainGoldRatio_Base}");
			if (LogicEntity_Unit.PropertyOps.GainGoldRatio_Base == null)
			{
				Logger?.LogError("GainGoldRatio_Base is null.");
			}

		}


		private static void ApplyBuff(Unit unit)
		{

			try
			{
				// Create Value object for the unit to be used with EffectorScript
				Value value = Value.NewObj(unit);

				// Define the property to be modified (e.g., Armor and Gold Ratio)
				var armorProp = LogicEntityCenter.GetProperty(LogicEntity_Unit.PropertyOpIds.Armor, true);
				var goldRatioProp = LogicEntityCenter.GetProperty(LogicEntity_Unit.PropertyOpIds.GainGoldRatio_Multiplier, true);

				if (armorProp == null || goldRatioProp == null)
				{
					Logger?.LogError("Failed to retrieve property operation settings for Armor or Gold Ratio.");
					return;
				}

				// Apply buffs using EffectorScriptBase
				// Here, we're applying a flat increase to Armor (e.g., +150) and Gold Ratio (e.g., *2)
				double currentArmor = EffectorScriptBase.ForProperty(value, armorProp).GetResult(true).AsDouble();
				double newArmor = currentArmor + 150.0; // Buff Armor by 150

				double currentGoldRatio = EffectorScriptBase.ForProperty(value, goldRatioProp).GetResult(true).AsDouble();
				double newGoldRatio = currentGoldRatio * 2.0; // Double the Gold Ratio

				// Set the new values using EffectorScript
				// Here we use Apply() instead of GetResult(true) to apply the new values directly
				// Apply buffs using EffectorScriptBase
				EffectorScriptBase.ForProperty(value, armorProp).SetParams(newArmor).OnInvoke(true); // Directly apply new Armor value
				EffectorScriptBase.ForProperty(value, goldRatioProp).SetParams(newGoldRatio).OnInvoke(true); // Directly apply new Gold Ratio

				// Log the results after buffing
				Logger?.LogInfo($"Applied buffs to {unit.Name}: " +
								 $"New Armor: {newArmor}, " +
								 $"New Gold Gain Ratio: {newGoldRatio}");

				// Bind the properties after modification
				LogicEntity_Unit.PropertyOps.BindOps();
			}
			catch (Exception ex)
			{
				Logger?.LogError($"Error applying buffs to {unit.Name}: {ex.Message}\n{ex.StackTrace}");
			}

			// Log the final stats after buff application
			Logger?.LogInfo($"[BuffApply] BUFFED {unit.Name}:\n" +
							 $"GoldRatio: {unit.BattleGainGoldRatio}, " +
							 $"Gold: {unit.Gold}, " +
							 $"Health: {unit.HPMax}, " +
							 $"Armor: {unit.Armor}, " +
							 $"Attack: {unit.Attack}, " +
							 $"Crit: {unit.CritChance}, " +
							 $"EcoE: {unit.Athlete.AthleteEcoEfficiency}");


			//public static void ResetBuffState()
			//{
			//	hasBuffsApplied = false;
			//	Logger?.LogInfo($"Buff state has been reset. {hasBuffsApplied}");
			//}
		}
	}
}




