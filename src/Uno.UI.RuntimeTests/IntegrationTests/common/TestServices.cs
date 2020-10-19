﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.UI.Xaml.Controls;

namespace Private.Infrastructure
{
	public class TestServices
	{
		public class WindowHelper
		{
			public static object WindowContent
			{
				get => RootControl.Content;
				internal set
				{
					if (RootControl is ContentControl content)
					{
						content.Content = value;
					}
					else
					{
						Console.WriteLine("Failed to get test content control");
					}
				}
			}

			public static ContentControl RootControl { get; set; }

			internal static async Task WaitForIdle()
			{
				await RootControl.Dispatcher.RunIdleAsync(_ => { /* Empty to wait for the idle queue to be reached */ });
				await RootControl.Dispatcher.RunIdleAsync(_ => { /* Empty to wait for the idle queue to be reached */ });
			}

			/// <summary>
			/// Wait until a specified <paramref name="condition"/> is met. 
			/// </summary>
			/// <param name="timeoutMS">The maximum time to wait before failing the test, in milliseconds.</param>
			internal static async Task WaitFor(Func<bool> condition, int timeoutMS = 1000, string message = "")
			{
				if (condition())
				{
					return;
				}

				var stopwatch = Stopwatch.StartNew();
				while (stopwatch.ElapsedMilliseconds < timeoutMS)
				{
					await WaitForIdle();
					if (condition())
					{
						return;
					}
				}

				throw new AssertFailedException("Timed out waiting for condition to be met. " + message);
			}

#if DEBUG
			/// <summary>
			/// This will wait. Forever. Useful when debugging a runtime test if you wish to visually inspect or interact with a view added
			/// by the test. (To break out of the loop, just set 'shouldWait = false' via the Immediate Window.)
			/// </summary>
			internal static async Task WaitForever()
			{
				var shouldWait = true;
				while (shouldWait)
				{
					await Task.Delay(1000);
				}
			}
#endif

			internal static void ShutdownXaml() { }
			internal static void VerifyTestCleanup() { }

			internal static void SetWindowSizeOverride(object p) { }
		}

		public class Utilities
		{
			internal static void VerifyMockDCompOutput(MockDComp.SurfaceComparison comparison, string step) { }
			internal static void VerifyMockDCompOutput(MockDComp.SurfaceComparison comparison) { }
		}

		internal static async Task RunOnUIThread(Action action)
		{
#if __WASM__
			action();
#else
			await WindowHelper.RootControl.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => action());
#endif
		}

		internal static void EnsureInitialized() { }

		public static void VERIFY_IS_LESS_THAN(double actual, double expected)
		{
			Assert.IsTrue(actual < expected, $"{actual} is not less than {expected}");
		}

		public static void VERIFY_THROWS_WINRT(Action action, Type exceptionType)
		{
			try
			{
				action();
				Assert.Fail($"Exception of type {exceptionType} is expected");
			}
			catch (Exception e)
			{
				if (e.GetType() != exceptionType)
				{
					Assert.Fail($"Exception of type {exceptionType} is expected (was {e.GetType()}");
				}
			}
		}

		internal static void VERIFY_ARE_EQUAL<T>(T actual, T expected)
		{
			Assert.AreEqual(expected: expected, actual: actual);
		}

		internal static void VERIFY_ARE_VERY_CLOSE(double actual, double expected, double tolerance = 0.1d)
		{
			var difference = Math.Abs(actual - expected);
			Assert.IsTrue(difference <= tolerance, $"Expected <{expected}>, actual <{actual}> (tolerance = {tolerance})");
		}
	}

	public class MockDComp
	{
		public enum SurfaceComparison
		{
			NoComparison
		}
	}
}
