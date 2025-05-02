//using GameMain.UnitSystem;
//using LogicFramework;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Utility.GameSystem.LogicFrameworkX;

//namespace MatchBuffManager
//{
//	public class BuffManager
//	{
//		public static void InitializeLogicItems(LogicItemList items)
//		{
//			// Add custom buffs dynamically
//			items.AddProperty(LogicEntity_Unit.PropertyOpIds.HeroGoldAddPropertiesRatio_Base)
//				 .Func(new Action<PropertyOpInvokeContext>(ApplyCustomGoldBuff));

//			items.AddProperty(LogicEntity_Unit.PropertyOpIds.Armor_ExtraAdded)
//				 .Func(new Action<PropertyOpInvokeContext>(ApplyCustomArmorBuff));
//		}

//		private static void ApplyCustomGoldBuff(PropertyOpInvokeContext context)
//		{
//			var unit = context.Target as Unit;
//			if (unit == null) return;

//			float goldBuff = 100.0f; // Example calculation
//			context.AddModifier(goldBuff); // Dynamically apply the buff
//		}

//		private static void ApplyCustomArmorBuff(PropertyOpInvokeContext context)
//		{
//			var unit = context.Target as Unit;
//			if (unit == null) return;

//			float armorBuff = 50.0f; // Example calculation
//			context.AddModifier(armorBuff); // Dynamically apply the buff
//		}
//	}
//}
