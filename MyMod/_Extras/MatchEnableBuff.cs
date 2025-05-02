//using System;
//using System.Collections.Generic;
//using BepInEx.Logging;
//using GameMain.UnitSystem.Equipment;
//using GameMain.UnitSystem;
//using HarmonyLib;
//using LogicFramework;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.ValueStruct;
//using BattleMainUI;
//using Utility.MissionSystem;

//namespace Co_EnableEquipment
//{
//	[HarmonyPatch(typeof(HeroEquipment), "Co_EnableEquipment", new[] { typeof(Predicate<Unit>) })]
//	public class Patch_Co_EnableEquipment
//	{
//		private static ManualLogSource Logger;

//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//		}

//		static void Postfix(HeroEquipment __instance, Predicate<Unit> predicate, ref IEnumerable<Unit> __result)
//		{
//			public IEnumerator<YieldCmd> Co_ApplyBuffOnEnable(Unit targetUnit, float goldRatioBonus, float armorBonus)
//			{
//				if (this.HeroCurrent != targetUnit)
//				{
//					this.SetHero(targetUnit);
//				}

//				if (!this.Enable)
//				{
//					if (this.HeroCurrent == null)
//					{
//						yield break;
//					}

//					// Connect equipment to the hero
//					this.HeroCurrent.Entity.Connect(base.Entity);
//					this.Enable = true;

//					// Apply Gold Ratio Buff
//					var goldRatioEffector = EffectorScriptBase
//						.ForProperty(Value.NewObj(this.HeroCurrent), LogicEntity_Unit.PropertyOps.GainGoldRatio_Multiplier)
//						.SetParams(goldRatioBonus);
//					this.HeroCurrent.BattleGainGoldRatio = goldRatioEffector.GetResult(true).AsFloat();

//					// Apply Armor Buff
//					var armorEffector = EffectorScriptBase
//						.ForProperty(Value.NewObj(this.HeroCurrent), LogicEntity_Unit.PropertyOps.Armor)
//						.SetParams(armorBonus);
//					this.HeroCurrent.Armor = armorEffector.GetResult(true).AsFloat();

//					// Log Buffs
//					Logger?.LogInfo($"Equipment Buffs Applied: GainGoldRatio = {this.HeroCurrent.BattleGainGoldRatio}, Armor = {this.HeroCurrent.Armor}");

//					// Notify system
//					yield return Yield.Mission(base.Entity.GetBehaviour_OnEnable(this.HeroCurrent.Entity));
//					UI_Battle_MainUI_HeroMessage_Utility.UpdateEquip(this.HeroCurrent, null);
//					this.HeroCurrent.Battle.C_Data.BattleRecorder.Log_HeroActiveEquipment(this.HeroCurrent, this);
//				}
//			}
//		}
//}
