//using BepInEx.Logging;
//using GameMain.BattleSystem;
//using GameMain.UnitSystem;
//using HarmonyLib;
//using LogicFramework.EffectorScript;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.ValueStruct;

//namespace BuffRule.CustomRule
//{
//	[HarmonyPatch(typeof(Battle), "GetUnits", new[] { typeof(Predicate<Unit>) })]
//	public class Patch_GetUnits2
//	{
//			private static ManualLogSource Logger;
//			public static void InitializeLogger(ManualLogSource logger)
//			{
//				Logger = logger;
//			}
//		static void Postfix(List<Unit> __result, Predicate<Unit> predicate)
//		{
//			if (__result == null)
//			{
//				Logger?.LogWarning("GetUnits returned a null result.");
//				return;
//			}

//			try
//			{
//				var unitsList = __result.Where(unit => unit.IsHero).ToList();
//				foreach (var unit in unitsList)
//				{
//					ApplyBuffs(unit);
//				}
//			}
//			catch (Exception ex)
//			{
//				Logger?.LogError($"Error in Patch_GetUnits2.Postfix: {ex}");
//			}
//		}

//		private static void ApplyBuffs(Unit unit)
//		{
//			if (unit == null) return;

//			try
//			{
//				// --- P0: CriticalChance_Base ---
//				var rule0 = new BattleRule_ADR1031.P0();
//				var settingP0 = rule0.OpSetting;                   
//				// The OpSetting comes from the script itself
//				var valueP0 = InvokeEffectorScript(rule0, unit, settingP0);
//				Logger?.LogInfo($"[BuffRule.P0] Unit {unit.Id} → {valueP0}");

//				// --- P1: Attack_Base ---
//				var rule1 = new BattleRule_ADR1031.P1();
//				var settingP1 = rule1.OpSetting;
//				var valueP1 = InvokeEffectorScript(rule1, unit, settingP1);
//				Logger?.LogInfo($"[BuffRule.P1] Unit {unit.Id} → {valueP1}");
//			}
//			catch (Exception ex)
//			{
//				Logger?.LogError($"Error applying buffs to {unit?.Id}: {ex}");
//			}
//		}

//		/// <summary>
//		/// Spawns an OpInvokeContext from the pool, initializes it,
//		/// invokes the script, then returns the context to the pool.
//		/// </summary>
//		private static Value InvokeEffectorScript<TScript>(
//			TScript script,
//			Unit unit,
//			OpSetting setting
//		) where TScript : EffectorScriptBase, new()
//		{
//			// 1) Spawn a context from your pool:
//			var ctx = PoolManager.Instance.Spawn<OpInvokeContext>();
//			try
//			{
//				// 2) Initialize it with the LogicEntity (backed by the Unit) and its OpSetting:
//				ctx.Init(unit.ToLogicEntity(), setting);

//				// 3) Call the public Invoke method (which calls your protected OnInvoke under the hood):
//				return script.InvokeFunc(ctx);
//			}
//			finally
//			{
//				// 4) Always despawn it so it can be reused:
//				ctx.Despawn();
//			}
//		}
//	}

//	// --- Helpers / Extensions (you may already have these somewhere) ---
//	public static class UnitExtensions
//	{
//		/// <summary>
//		/// Converts a game‑unit into the LogicEntity wrapper your scripts expect.
//		/// </summary>
//		public static LogicEntity ToLogicEntity(this Unit u)
//		{
//			// You might have a real conversion; this is placeholder:
//			return new LogicEntity(u.Id);
//		}
//	}
//}

//}
