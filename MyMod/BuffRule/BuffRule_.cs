//using System;
//using BepInEx.Logging;
//using GameMain.UnitSystem;
//using Utility;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.ValueStruct;
//using LogicFramework.EffectorScript;

//namespace LogicFramework.EffectorScript
//{
//	// Token: 0x020008A8 RID: 2216
//	public static class BuffRule
//	{

//		private static ManualLogSource Logger;
//		public static void InitializeLogger(ManualLogSource logger)
//		{
//			Logger = logger;
//			Logger?.LogInfo($"BuffRule Starting");
//		}
		

//		// Token: 0x02002FCD RID: 12237
//		[EffectorScript("85be6145-b015-44a2-1111-a660b63195d4")]
//		public class P0 : EffectorScriptBase
//		{
//			// Token: 0x1700389A RID: 14490
//			// (get) Token: 0x0601072A RID: 67370 RVA: 0x003FDD4C File Offset: 0x003FBF4C
//			public override Guid Id
//			{
//				get
//				{
//					return Guid.Parse("85be6145-b015-44a2-1111-a660b63195d4");
//				}
//			}

//			// Token: 0x1700389B RID: 14491
//			// (get) Token: 0x0601072B RID: 67371 RVA: 0x003FDD58 File Offset: 0x003FBF58
//			public override OpSetting OpSetting
//			{
//				get
//				{
//					return LogicEntity_Unit.PropertyOps.CriticalChance_Base;
//					//return LogicEntity_Battle.BehaviourOps.OnBattleStart;
//				}
//			}

//			// Token: 0x0601072C RID: 67372 RVA: 0x003FDD60 File Offset: 0x003FBF60
//			protected override Value OnInvoke(OpInvokeContext context)
//			{
//				Value entity = Value.NewObj(context.InvokerEntity);
//				if (EffectorScriptBase.ForProperty(entity, LogicEntity_Unit.PropertyOps.IsUnitType).SetParams(UnitTypes.Hero).GetResult(true).ToBool())
//				{
//					return Value.New(100.0);

//				}
//				return default(Value);
//			}
//		}

//		//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

//		// Token: 0x02002FCE RID: 12238
//		[EffectorScript("cf54463b-ed2d-4f39-2222-c3a68ad92bd7")]
//		public class P1 : EffectorScriptBase
//		{
//			// Token: 0x1700389C RID: 14492
//			// (get) Token: 0x0601072E RID: 67374 RVA: 0x003FDE71 File Offset: 0x003FC071
//			public override Guid Id
//			{
//				get
//				{
//					return Guid.Parse("cf54463b-ed2d-4f39-2222-c3a68ad92bd7");
//				}
//			}

//			// Token: 0x1700389D RID: 14493
//			// (get) Token: 0x0601072F RID: 67375 RVA: 0x003FDE7D File Offset: 0x003FC07D
//			public override OpSetting OpSetting
//			{
//				get
//				{
//					return LogicEntity_Unit.PropertyOps.Attack_Base;
//					//return LogicEntity_Battle.BehaviourOps.OnBattleStart;
//				}
//			}

//			// Token: 0x06010730 RID: 67376 RVA: 0x003FDE84 File Offset: 0x003FC084
//			protected override Value OnInvoke(OpInvokeContext context)
//			{
//				Logger?.LogInfo($"[P1] context: {context}");
//				Value entity = Value.NewObj(context.InvokerEntity);
//				if (EffectorScriptBase.ForProperty(entity, LogicEntity_Unit.PropertyOps.IsUnitType).SetParams(UnitTypes.Hero).GetResult(true).ToBool())
//				{
//					Logger?.LogInfo($"[P1] entity: {entity}");

//					return Value.New(1000.0);
			

//				}
//				return default(Value);
//			}
//		}

//		//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

//		[EffectorScript("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee")]
//		public class BuffRule_OnStart : EffectorScriptBase
//		{
//			public override Guid Id => Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");

//			// Fires once at battle start
//			public override OpSetting OpSetting => LogicEntity_Battle.BehaviourOps.OnBattleStart;

//			protected override Value OnInvoke(OpInvokeContext context)
//			{
//				Logger?.LogInfo($"[BuffRule] context: {context}");
//				// context.CurrentEntity is the battle entity
//				var battle = Value.NewObj(context.CurrentEntity);
//				Logger?.LogInfo($"[BuffRule] battle: {battle}");

//				// Get all hero units in all teams
//				var teams = EffectorScriptBase
//					.ForProperty(battle, LogicEntity_Battle.PropertyOps.TeamAll)
//					.GetResultList(new List<Value>(), true);

//				foreach (var team in teams)
//				{
//					Logger?.LogInfo($"[BuffRule] teams: {teams}");
//					var heroes = EffectorScriptBase
//						.ForProperty(team, LogicEntity_BattleTeam.PropertyOps.Units)
//						.GetResultList(new List<Value>(), true);

//					foreach (var hero in heroes)
//					{
//						Logger?.LogInfo($"[BuffRule] Hero: {hero}");
//						// **Force** the engine to re‑read CriticalChance_Base,
//						// which will invoke P0 and thus apply your 100.0 buffer.
//						EffectorScriptBase
//							.ForProperty(hero, LogicEntity_Unit.PropertyOps.CriticalChance_Base)
//							.GetResult(true);
//						EffectorScriptBase
//							.ForProperty(hero, LogicEntity_Unit.PropertyOps.Attack_Base)
//							.GetResult(true);
//					}
//				}
			

//				// We don’t produce a new mission here—just side‑effects
//				return Value.Null;
//			}
//		}
//	}
//}
