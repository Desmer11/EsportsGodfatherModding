//using System;
//using System.Collections.Generic;
//using System.Linq;
//using BepInEx.Logging;
//using GameMain.FightSystem;
//using GameMain.UnitSystem;
//using HarmonyLib;
//using LogicFramework;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.ValueStruct;

//[HarmonyPatch(typeof(Fight), "Units", MethodType.Getter)]
//public class Fight_Units_Getter_Patch
//{
//	private static ManualLogSource Logger;

//	// Set the logger from the plugin
//	public static void InitializeLogger(ManualLogSource logger)
//	{
//		Logger = logger;
//	}

//	// Static flag to ensure the code is executed only once
//	//private static bool HasExecuted = false;

//	static void Postfix(Fight __instance, ref IEnumerable<Unit> __result)
//	{
//		//if (HasExecuted)
//		//{
//		//	return;
//		//}

//		// Convert the result to a list for safe manipulation
//		var unitsList = __result.ToList();

//		// Ensure there are at least two units
//		if (unitsList.Count >= 2)
//		{
//			// Randomly select two units from the list
//			var random = new Random();
//			var selectedUnits = unitsList.OrderBy(x => random.Next()).Take(2).ToArray();

//			// Apply buffs and nerfs
//			ApplyBuff(selectedUnits[0]);
//			ApplyNerf(selectedUnits[1]);

//			// Set the flag to true to prevent further execution
//			HasExecuted = true;
//		}

//		// Assign the modified list back to the result
//		__result = unitsList.AsEnumerable();
//	}

//	private static void ApplyBuff(Unit unit)
//	{
//EffectorScriptBase.ForProperty(Value.NewObj(u), LogicEntity_Unit.PropertyOps.MaxHp_Multiplier)
//				  .SetParams(12.0f)
//				  .GetResult();
//EffectorScriptBase.ForProperty(Value.NewObj(u), LogicEntity_Unit.PropertyOps.AttackRatioTryPoke_Multiplier)
//			  .SetParams(12.0f)
//			  .GetResult();
//EffectorScriptBase.ForProperty(Value.NewObj(u), LogicEntity_Unit.PropertyOps.CriticalChance_ExtraAdded)
//			  .SetParams(100.0f)
//			  .GetResult();
//EffectorScriptBase.ForProperty(Value.NewObj(u), LogicEntity_Unit.PropertyOps.HeroGoldAddPropertiesRatio_Multiplier)
//				  .SetParams(12.0f)
//				  .GetResult();
//Logger?.LogInfo($"[BuffApply] Buffed {u.Name}: +100% Max HP, +100% Gold, +100% Poke, 100% Crit");

//		Logger?.LogInfo($"Buffed {unit.Name}: +200% Max HP, +100% Gold");
//	}

//	private static void ApplyNerf(Unit unit)
//	{
//		EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.MaxHp_Multiplier)
//			.SetParams(0.1f)
//			.GetResult();

//		EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.HeroGoldAddPropertiesRatio_Multiplier)
//			.SetParams(0.1f)
//			.GetResult();

//		Logger?.LogInfo($"Nerfed {unit.Name}: -50% Max HP, -50% Gold");
//	}
//}



