using System;
using System.Runtime.InteropServices;

namespace Mntone.PerMonitorDpi.Win32
{
	internal static class NativeMethods
	{
		[DllImport( "user32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
		public static extern IntPtr MonitorFromWindow( IntPtr hwnd, MonitorDefaultTo dwFlags );

		[DllImport( "SHCore.dll", CharSet = CharSet.Unicode, PreserveSig = false )]
		public static extern void GetDpiForMonitor( IntPtr hmonitor, MonitorDpiType dpiType, ref uint dpiX, ref uint dpiY );
	}
}
