using System;
using System.Runtime.CompilerServices;

namespace Overengineering.Utilities
{
	public static partial class Utils
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EaseInQuint(float x) => x * x * x * x * x;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EaseInQuint(float a, float b, float x) => a + (b - a) * EaseInQuint(x);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EaseOutQuint(float x) => 1 - (float)Math.Pow(1 - x, 5);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EaseOutQuint(float a, float b, float x) => a + (b - a) * EaseOutQuint(x);
	}
}
