//using BepInEx.Logging;
//using GameMain.UnitSystem;
//using Utility.GameSystem.LogicFrameworkX;
//using Utility.ValueStruct;

//namespace LogicFramework.EffectorScript
//{
//	public static class BattleRule_RuleBonusPatch
//	{
//		[EffectorScript("e57d5b99-f312-4c5f-cccc-8d3f9c2d8e72")]
//		public class C1 : EffectorScriptBase
//		{
//			private static ManualLogSource Logger;
//			public static void InitializeLogger(ManualLogSource logger)
//			{
//				Logger = logger;
//			}

//			public override Guid Id => Guid.Parse("e57d5b99-f312-4c5f-cccc-8d3f9c2d8e72");

//			public override OpSetting OpSetting => LogicEntity_Unit.PropertyOps.CriticalChance_Base;

//			protected override Value OnInvoke(OpInvokeContext context)
//			{
//				Logger?.LogInfo("C1.OnInvoke called");
//				Value entity = Value.NewObj(context.InvokerEntity);
//				bool isHero = EffectorScriptBase.ForProperty(entity, LogicEntity_Unit.PropertyOps.IsUnitType)
//					.SetParams(UnitTypes.Hero).GetResult(true).ToBool();

//				if (isHero)
//				{
//					Logger?.LogInfo($"Entity {context.InvokerEntity} is a Hero. Returning critical chance: 100.0");
//					return Value.New(100.0); // Custom logic for heroes
//				}

//				Logger?.LogInfo($"Entity {context.InvokerEntity} is not a Hero. Returning default value.");
//				return default(Value);
//			}
//		}

//		[EffectorScript("9b3d0ab4-a3b5-4f7e-cccc-579d48c6e1d6")]
//		public class C2 : EffectorScriptBase
//		{
//			private static ManualLogSource Logger;
//			public static void InitializeLogger(ManualLogSource logger)
//			{
//				Logger = logger;
//			}

//			public override Guid Id => Guid.Parse("9b3d0ab4-a3b5-4f7e-cccc-579d48c6e1d6");

//			public override OpSetting OpSetting => LogicEntity_Unit.PropertyOps.Attack_Base;

//			protected override Value OnInvoke(OpInvokeContext context)
//			{
//				Logger?.LogInfo("C2.OnInvoke called");
//				Value entity = Value.NewObj(context.InvokerEntity);
//				bool isMonster = EffectorScriptBase.ForProperty(entity, LogicEntity_Unit.PropertyOps.UnitType)
//					.SetParams(UnitTypes.Monster).GetResult(true).ToBool();

//				if (isMonster)
//				{
//					Logger?.LogInfo($"Entity {context.InvokerEntity} is a Monster. Returning attack: 1000.0");
//					return Value.New(1000.0); // Custom logic for monsters
//				}

//				Logger?.LogInfo($"Entity {context.InvokerEntity} is not a Monster. Returning default value.");
//				return default(Value);
//			}



//		}
//	}
//}
