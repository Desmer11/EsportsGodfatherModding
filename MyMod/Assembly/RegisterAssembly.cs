//using BepInEx.Logging;
//using LogicFramework.EffectorScript;
//using System.Reflection;
//using Utility.GameSystem.LogicFrameworkX;

//public static class AssemblyRegister
//{
//	private static ManualLogSource Logger;
//	public static void InitializeLogger(ManualLogSource logger)
//	{
//		Logger = logger;
//	}
//	public static void ManualRegisterEffectorScripts()
//	{

//		try
//		{
//			var regType = typeof(EffectorScriptBase)
//				.Assembly
//				.GetType("Utility.GameSystem.LogicFrameworkX.EffectorScriptRegistry", true);
//			var regMethod = regType.GetMethod(
//				"RegisterScript",
//				BindingFlags.NonPublic | BindingFlags.Static
//			);

//			var myAsm = typeof(BuffRule).Assembly;
//			foreach (var type in myAsm.GetTypes())
//			{
//				if (!type.IsAbstract
//				 && type.IsSubclassOf(typeof(EffectorScriptBase))
//				 && type.GetCustomAttribute<EffectorScriptAttribute>() != null)
//				{
//					regMethod.Invoke(null, new object[] { type });
//				}
//			}

//			Logger.LogInfo("EffectorScript manual registration succeeded.");
//		}
//		catch (Exception ex)
//		{
//			Logger.LogError($"EffectorScript manual registration failed: {ex}");
//		}
//	}
//}
