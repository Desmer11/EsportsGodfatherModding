//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace MyMod.Logger
//{
//	public static class Logger
//	{
//		private static readonly object _lock = new object(); // To ensure thread safety

//		public static void LogInfo(string message) => Log("INFO", message);
//		public static void LogDebug(string message) => Log("DEBUG", message);
//		public static void LogWarning(string message) => Log("WARN", message);
//		public static void LogError(string message, Exception ex = null)
//		{
//			string errorDetails = ex != null ? $"{message}\n{ex}" : message;
//			Log("ERROR", errorDetails);
//		}

//		private static void Log(string level, string message)
//		{
//			lock (_lock)
//			{
//				string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
//				Console.WriteLine($"[{timestamp}] {level}: {message}");
//			}
//		}
//	}
//}
