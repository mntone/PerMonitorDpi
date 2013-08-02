using System;
using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	public static class SafeNativeMethods
	{
		[DllImport( "gdi32.dll" )]
		public static extern int GetDeviceCaps( IntPtr hdc, DeviceCap index );

		public delegate bool EnumMonitorsDelegate( IntPtr hMonitor, IntPtr hdcMonitor, ref NativeRect lprcMonitor, IntPtr dwData );

		[DllImport( "user32.dll" )]
		public static extern bool EnumDisplayMonitors( IntPtr hdc, IntPtr lprcClip, EnumMonitorsDelegate lpfnEnum, IntPtr dwData );

		[DllImport( "dxva2.dll", SetLastError = true )]
		[return: MarshalAs( UnmanagedType.Bool )]
		internal static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR( IntPtr hmonitor, ref uint pdwNumberOfPhysicalMonitors );

		[DllImport( "dxva2.dll" )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool GetMonitorCapabilities( IntPtr hpm, [Out] uint monitorCapabilities, [Out] uint supportedColorTemperatures );
	
		[DllImport( "dxva2.dll" )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool GetMonitorTechnologyType( IntPtr hpm, [Out] DisplayTechnologyType displayTechnologyType );

		[DllImport( "dxva2.dll" )]
		[return: MarshalAs( UnmanagedType.Bool )]
		public static extern bool GetMonitorBrightness( IntPtr hpm, [Out] uint minimumBrightness, [Out] uint currentBrightness, [Out] uint maximumBrightness );

		[DllImport( "User32.dll" )]
		public static extern long GetDisplayConfigBufferSizes( QueryDisplayFlags flags, out uint numPathArrayElements, out uint numModeInfoArrayElements );

		[DllImport( "User32.dll" )]
		public static extern long QueryDisplayConfig(
			QueryDisplayFlags flags,
			ref uint numPathArrayElements, [Out] DisplayConfigPathInfo[] pathInfoArray,
			ref uint modeInfoArrayElements, [Out] DisplayConfigModeInfo[] modeInfoArray,
			IntPtr z );

		[DllImport( "SHCore.dll", CharSet = CharSet.Unicode, PreserveSig = false )]
		public static extern void GetDpiForMonitor( IntPtr hmonitor, MonitorDpiType dpiType, ref uint dpiX, ref uint dpiY );
	}
}
