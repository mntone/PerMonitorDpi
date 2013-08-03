using Mntone.MonitorInformation.Models.Win32;
using System;
using System.Windows;

namespace Mntone.MonitorInformation.Models.Domains
{
	// All platform (Windows XP or later)
	public class Monitor
	{
		public static Point SystemDpi { get; internal set; }

		public uint No { get; internal set; }
		public IntPtr Hmonitor { get; internal set; }
		public Rect Area { get; internal set; }

		public string Description
		{
			get
			{
				return "No: " + No
					+ "\nArea: " + Area.ToString()
					+ "\nSystem dpi: " + SystemDpi.ToString();
			}
		}
	}
}
