namespace MobileGame.Snake
{
    using System;
    using UnityEngine;

	public static class ColorExtensions
    {
		public static String ColorToHexadecimal(Color color)
		{
			return $"#{((int)(color.r * 255)).ToString("X2")}" +
					$"{((int)(color.g * 255)).ToString("X2")}" +
					$"{((int)(color.b * 255)).ToString("X2")}";
		}
	}
}
