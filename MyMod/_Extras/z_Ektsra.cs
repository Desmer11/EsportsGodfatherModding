
//namespace MyMod
//{
//	internal class z_Ektsra
//	{

//private static void ApplyBuffLogicToUnits(Battle battle, Predicate<Unit> predicate)
//{
//	var units = battle.GetUnits(predicate).ToList(); // Collect all units matching the predicate

//	if (units.Count >= 2)
//	{
//		var random = new System.Random();

//		// Randomly select two units from the list
//		var selectedUnits = units.OrderBy(_ => random.Next()).Take(2).ToArray();

//		// Apply buffs and nerfs
//		Logger?.LogInfo($"[BuffApply] BEFORE {selectedUnits[0].Name}: {selectedUnits[0].Attack}, {selectedUnits[0].BattleGainGoldRatio}, {selectedUnits[0].HPMax}, {selectedUnits[0].AccuracyBattle}");
//		Logger?.LogInfo($"[BuffApply] BEFORE {selectedUnits[1].Name}: {selectedUnits[1].Attack}, {selectedUnits[1].BattleGainGoldRatio}, {selectedUnits[1].HPMax}, {selectedUnits[1].AccuracyBattle}");
//		ApplyBuff(selectedUnits[0]);
//		Logger?.LogInfo($"Buff applied to unit: {selectedUnits[0].Name}");
//		ApplyNerf(selectedUnits[1]);
//		Logger?.LogInfo($"Buff applied to unit: {selectedUnits[1].Name}");

//	}
//	else
//	{
//		Logger?.LogInfo("Not enough units available for buff/nerf application.");
//	}
//}
//	}
//}
//using GameMain.UnitSystem;
//using LogicFramework;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.ValueStruct;

//private static void ApplyNerf(Unit unit)
//{
//	EffectorScriptBase.ForProperty(Value.NewObj(unit), LogicEntity_Unit.PropertyOps.MaxHp_Multiplier)
//		.SetParams(0.5f)
//		.GetResult();
//	Logger?.LogInfo($"[BuffApply] Nerfed {unit.Name}: HP: {unit.HPMax}");
//}



