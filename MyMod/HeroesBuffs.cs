using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using GameMain.UnitSystem;
using Utility;
using LogicFramework;
using Utility.GameSystem.LogicFrameworkX;
using Utility.ValueStruct;
using BepInEx;
using BepInEx.Logging;

namespace HeroesBuffs
{
	[HarmonyPatch]
	public static class ChangeHeroVariantPatch
	{
		private static ManualLogSource Logger;
		public static void InitializeLogger(ManualLogSource logger)
		{
			Logger = logger;
		}


		private static readonly Random Rng = new Random();

		[HarmonyPatch(typeof(GameMain.Game.RuleComponent), "ChangeHeroVariant")]
		[HarmonyPostfix]
		public static void Postfix(



			int abilityCount,
			int cardCount,
			Dictionary<UnitSetting, (int OldIndex, int NewIndex)> abilityChange,
			Dictionary<UnitSetting, (int OldIndex, int NewIndex)> cardChange,
			GameMain.Game.RuleComponent __instance)
		{
			// Combine all affected heroes
			var affectedHeroes = abilityChange.Keys.Union(cardChange.Keys).ToList();

			// Ensure there are at least 10 heroes to work with
			if (affectedHeroes.Count < 10)
				return;

			// Shuffle and divide the list into buffs and nerfs
			var shuffledHeroes = affectedHeroes.OrderBy(_ => Rng.Next()).ToList();
			var buffs = shuffledHeroes.Take(5).ToList();
			var nerfs = shuffledHeroes.Skip(5).Take(5).ToList();

			//foreach (var hero in buffs)
			//{
			//	var unit = GetUnitFromSetting(hero);
			//	if (unit != null)
			//		ApplyBuff(unit);
			//}


			//foreach (var hero in nerfs)
			//{
			//	var unit = GetUnitFromSetting(hero);
			//	if (unit != null)
			//		ApplyNerf(unit);
			//}
		}

		// Converts UnitSetting to Unit using the game's UnitManager
		//private static Unit GetUnitFromSetting(UnitSetting setting)
		//{

		//	return GameMain.UnitSystem.UnitCollection.FirstOrDefault(unit => unit.Setting == setting);
		//}


		// Apply buffs to a hero
		private static void ApplyBuff(Unit unit)
		{
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.MaxHp_Multiplier)
				.SetParams(2.0f)  // Apply a 200% increase
				.GetResult();     // Trigger the effect

			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.HeroGoldAddPropertiesRatio_Multiplier)
				.SetParams(2.0f)  // Apply a 100% increase
				.GetResult();     // Trigger the effect

			UnityEngine.Debug.Log($"Buffed {unit.Name}: +100% Max HP, +100% Gold");
		}

		// Apply nerfs to a hero
		private static void ApplyNerf(Unit unit)
		{
			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.MaxHp_Multiplier)
				.SetParams(0.5f)  // Apply a 50% decrease
				.GetResult();     // Trigger the effect

			EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.HeroGoldAddPropertiesRatio_Multiplier)
				.SetParams(0.5f)  // Apply a 50% decrease
				.GetResult();     // Trigger the effect

			UnityEngine.Debug.Log($"Nerfed {unit.Name}: -50% Max HP, -50% Gold");
		}

	}
}
