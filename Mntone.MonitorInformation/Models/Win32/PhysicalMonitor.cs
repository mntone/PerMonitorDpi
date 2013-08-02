using System;
using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
	public struct PhysicalMonitor
	{
		public IntPtr hPhysicalMonitor;

		[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 64 )]
		public string physicalMonitorDescription;
	}
}
