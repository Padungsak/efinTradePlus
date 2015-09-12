using STIControl;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace i2TradePlus
{
	public class MyColor
	{
		public static Color UpColor = Color.FromArgb(0, 255, 124);

		public static Color DownColor = Color.FromArgb(255, 80, 75);

		public static Color BuyColor = Color.FromArgb(255, 175, 0);

		public static Color SellColor = Color.FromArgb(174, 126, 213);

		public static Color UnChgColor = Color.FromArgb(255, 244, 80);

		public static Color CeilingColor = Color.FromArgb(78, 232, 230);

		public static Color FloorColor = Color.FromArgb(187, 44, 189);

		public static Color OpenColor = Color.White;

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void SetStyle(bool isSoftStyle)
		{
			if (isSoftStyle)
			{
				MyColor.UpColor = Color.FromArgb(0, 240, 50);
				MyColor.DownColor = Color.FromArgb(255, 55, 46);
				MyColor.BuyColor = Color.FromArgb(255, 175, 0);
				MyColor.SellColor = Color.FromArgb(216, 100, 216);
				MyColor.UnChgColor = Color.FromArgb(255, 255, 80);
				MyColor.OpenColor = Color.White;
				MyColor.CeilingColor = Color.FromArgb(78, 232, 230);
				MyColor.FloorColor = Color.FromArgb(187, 44, 189);
			}
			else
			{
				MyColor.UpColor = Color.Lime;
				MyColor.DownColor = Color.Red;
				MyColor.BuyColor = Color.Lime;
				MyColor.SellColor = Color.Red;
				MyColor.UnChgColor = Color.Yellow;
				MyColor.OpenColor = Color.Yellow;
				MyColor.CeilingColor = Color.Cyan;
				MyColor.FloorColor = Color.Magenta;
			}
			MySetting.UpColor = MyColor.UpColor;
			MySetting.DownColor = MyColor.DownColor;
			MySetting.BuyColor = MyColor.BuyColor;
			MySetting.SellColor = MyColor.SellColor;
			MySetting.UnchgColor = MyColor.UnChgColor;
			MySetting.OpenColor = MyColor.OpenColor;
			MySetting.CeilingColor = MyColor.CeilingColor;
			MySetting.FloorColor = MyColor.FloorColor;
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public MyColor()
		{
		}
	}
}
