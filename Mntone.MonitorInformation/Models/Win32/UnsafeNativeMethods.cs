using System;
using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	internal static class UnsafeNativeMethods
	{
		[DllImport( "user32.dll" )]
		internal static extern IntPtr GetDC( IntPtr hwnd );

		[DllImport( "user32.dll" )]
		internal static extern bool ReleaseDC( IntPtr hwnd, IntPtr hdc );

		[DllImport( "dxva2.dll", CharSet = CharSet.Unicode )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool GetPhysicalMonitorsFromHMONITOR( IntPtr hmonitor, uint physicalMonitorArraySize, [Out] PhysicalMonitor[] physicalMonitorArray );

		[DllImport( "dxva2.dll", CharSet = CharSet.Unicode )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool DestroyPhysicalMonitors( uint physicalMonitorArraySize, ref PhysicalMonitor[] physicalMonitorArray );
	}
}
