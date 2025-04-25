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

namespace MatchBuffs
{
	[HarmonyPatch(typeof(Battle), "GetUnits", new[] { typeof(Predicate<Unit>) })]
	public class Patch_GetUnits
	{
		private static bool hasBuffsApplied = false;
		static void Postfix(Battle __instance, Predicate<Unit> predicate, ref IEnumerable<Unit> __result)
		{
			if (hasBuffsApplied == true)
			{
				Logger?.LogInfo($"Buff logic already executed. Skipping further application {hasBuffsApplied}");
				return;
			}
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

			// Apply buffs and nerfs if there are enough units
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
				Logger?.LogInfo($"[BuffApply] BEFORE BUFF  {selectedUnits[0].Name}:\n GoldRatio: {selectedUnits[0].BattleGainGoldRatio},\n Health: {selectedUnits[0].HPMax},\n Armor: {selectedUnits[0].Armor},\n Attack: {selectedUnits[0].Attack},\n Crit: {selectedUnits[0].CritChance}");
				Logger?.LogInfo($"[BuffApply] BEFORE BUFF  {selectedUnits[1].Name}:\n GoldRatio: {selectedUnits[1].BattleGainGoldRatio},\n Health: {selectedUnits[1].HPMax},\n Armor: {selectedUnits[1].Armor},\n Attack: {selectedUnits[1].Attack},\n Crit: {selectedUnits[1].CritChance}");

				ApplyBuff(selectedUnits[0]);
				ApplyBuff(selectedUnits[1]);

				Logger?.LogInfo($"Buff applied to unit: {selectedUnits[0].Name}");
				Logger?.LogInfo($"Buff applied to unit: {selectedUnits[1].Name}");

				hasBuffsApplied = true;
			}
			catch (Exception ex)
			{
				Logger?.LogError($"Error during buff/nerf application: {ex.Message}\n{ex.StackTrace}");
			}


		}

		private static ManualLogSource Logger;
		public static void InitializeLogger(ManualLogSource logger)
		{
			Logger = logger;
		}


		private static void ApplyBuffLogicToUnits(Battle battle, Predicate<Unit> predicate)
		{
			var units = battle.GetUnits(predicate).ToList(); // Collect all units matching the predicate

			if (units.Count >= 2)
			{
				var random = new System.Random();

				// Randomly select two units from the list
				var selectedUnits = units.OrderBy(_ => random.Next()).Take(2).ToArray();

				// Apply buffs and nerfs
				Logger?.LogInfo($"[BuffApply] BEFORE {selectedUnits[0].Name}: {selectedUnits[0].Attack}, {selectedUnits[0].BattleGainGoldRatio}, {selectedUnits[0].HPMax}, {selectedUnits[0].AccuracyBattle}");
				Logger?.LogInfo($"[BuffApply] BEFORE {selectedUnits[1].Name}: {selectedUnits[1].Attack}, {selectedUnits[1].BattleGainGoldRatio}, {selectedUnits[1].HPMax}, {selectedUnits[1].AccuracyBattle}");
				ApplyBuff(selectedUnits[0]);
				Logger?.LogInfo($"Buff applied to unit: {selectedUnits[0].Name}");
				ApplyNerf(selectedUnits[1]);
				Logger?.LogInfo($"Buff applied to unit: {selectedUnits[1].Name}");

			}
			else
			{
				Logger?.LogInfo("Not enough units available for buff/nerf application.");
			}
		}

		private static void ApplyBuff(Unit unit)
		{

			//Debug;

			Logger?.LogInfo($"Armor Guid: {LogicEntity_Unit.PropertyOps.Armor}");
			Logger?.LogInfo($"Armor_Base Guid: {LogicEntity_Unit.PropertyOps.Armor_Base}");
			var directResult = EffectorScriptBase.ForProperty(unit, LogicEntity_Unit.PropertyOps.Armor)
			.SetParams(100.0f)
			.GetResult();
			Logger?.LogInfo($"Direct result for Armor: {directResult}");

			var parsedArmorGuid = Guid.Parse("6c0ee3f8-ee5f-4794-a79c-326eafed4684");
			Logger?.LogInfo($"Parsed Armor Guid: {parsedArmorGuid}");

			var testArmorProperty = LogicEntity_Unit.PropertyOps.Armor;
			Logger?.LogInfo($"Attempting direct assignment using Armor Guid: {testArmorProperty}");
			EffectorScriptBase.ForProperty(unit, testArmorProperty)
				.SetParams(100.0f)
				.GetResult();

			Logger?.LogInfo("Starting property effect application...");

			var armorGuid = LogicEntity_Unit.PropertyOps.Armor;
			Logger?.LogInfo($"Using Guid for Armor: {armorGuid}");

			var result = EffectorScriptBase.ForProperty(Value.NewObj(unit), armorGuid)
				.SetParams(100.0f)
				.GetResult();
			Logger?.LogInfo($"Result for Armor effect application: {result}");


			//Debug;
			Logger?.LogInfo($"Armor before: {unit.Armor}");
			EffectorScriptBase.ForProperty(unit, LogicEntity_Unit.PropertyOps.Armor)
				.SetParams(100.0f)
				.GetResult();
			Logger?.LogInfo($"Armor after: {unit.Armor}");

			Logger?.LogInfo($"GetResult output: {result}");

			Logger?.LogInfo($"Simplified buff applied. New Armor: {unit.Armor}");
			Debug.Assert(unit.Armor > 40, "Armor buff not applied correctly!");







			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Armor)
			.SetParams(100.0f)
			.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Armor_Base)
			.SetParams(100.0f)
			.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Armor_ExtraAdded)
			.SetParams(100.0f)
			.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Armor_ExtraRatio)
			.SetParams(100.0f)
			.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Armor_Multiplier)
			.SetParams(100.0f)
			.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Armor_Override)
			.SetParams(100.0f)
			.GetResult();

			//____________________________________________________________________________________________________________

			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.MaxHp)
		.SetParams(100.0f)
		.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.GainGoldRatio_Multiplier)
				.SetParams(100.0f)
				.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.Attack)
			.SetParams(100.0f)
			.GetResult();
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.CriticalChance)
				.SetParams(100.0f)
				.GetResult();




			Logger?.LogInfo($"[BuffApply] BUFFED {unit.Name}:\n GoldRatio: {unit.BattleGainGoldRatio},\n Health: {unit.HPMax},\n Armor: {unit.Armor},\n Attack: {unit.Attack},\n Crit: {unit.CritChance}\n");







		}

		private static void ApplyNerf(Unit unit)
		{
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.MaxHp_Multiplier)
				.SetParams(0.5f)
				.GetResult();
			Logger?.LogInfo($"[BuffApply] Nerfed {unit.Name}: HP: {unit.HPMax}");
		}


		public static void ResetBuffState()
		{
			hasBuffsApplied = false;
			Logger?.LogInfo($"Buff state has been reset. {hasBuffsApplied}");
		}
	}
}
