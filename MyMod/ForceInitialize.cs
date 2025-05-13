// Force initialization of the EffectorScriptCenter class
using System;
using System.Reflection;

namespace ForceESCInitialization
{
		public static class ForceInitialize
	{
		public static void InitializeEffectorScriptCenter()
		{
			var type = typeof(Utility.GameSystem.LogicFrameworkX.EffectorScriptCenter);
			var dummy = type.GetField("s_dict", BindingFlags.Static | BindingFlags.NonPublic);
			if (dummy != null)
			{
				Console.WriteLine("EffectorScriptCenter initialized by accessing a static field.");
			}
			else
			{
				Console.WriteLine("Failed to access s_dict. Ensure class is loaded.");
			}
		}
	}
}