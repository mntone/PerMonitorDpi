using Mntone.MonitorInformation.Models.Domains;
using Mntone.MonitorInformation.Models.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Mntone.MonitorInformation.Models
{
	internal static class RetrievingHelper
	{
		public static List<Monitor> GetMonitorList<T>()
			where T: Monitor, new()
		{
			var monitorList = new List<Monitor>();

			var i = 0u;
			SafeNativeMethods.EnumDisplayMonitors( IntPtr.Zero, IntPtr.Zero,
				delegate( IntPtr hMonitor, IntPtr hdcMonitor, ref NativeRect lprcMonitor, IntPtr dwData )
				{
					var m = new T();
					m.No = i++;
					m.Hmonitor = hMonitor;
					m.Area = lprcMonitor.ToRect();
					
					monitorList.Add( m );
					return true;
				}, IntPtr.Zero );

			var hdc = new DeviceContextHandle();
			var y = SafeNativeMethods.GetDeviceCaps( hdc.HDC, DeviceCap.LogPixelsY );
			var x = SafeNativeMethods.GetDeviceCaps( hdc.HDC, DeviceCap.LogPixelsX );
			Monitor.SystemDpi = new Point( x, y );

			return monitorList;
		}

		public static void UpdateWithLowLevelMonitorConfigurationApi( List<Monitor> list )
		{
			foreach( var obj in list )
			{
				var m = ( Monitor60 )obj;

				var physicalMonitorSize = 0u;
				if( SafeNativeMethods.GetNumberOfPhysicalMonitorsFromHMONITOR( m.Hmonitor, ref physicalMonitorSize ) )
				{
					var physicalMonitors = new PhysicalMonitor[physicalMonitorSize];
					if( UnsafeNativeMethods.GetPhysicalMonitorsFromHMONITOR( m.Hmonitor, physicalMonitorSize, physicalMonitors ) )
					{
						var pm = physicalMonitors[0];
						m.Name = pm.physicalMonitorDescription;

						//uint c = 0u, t = 0u;
						//SafeNativeMethods.GetMonitorCapabilities( pm.hPhysicalMonitor, c, t );

						//m.HasTechnologyType = ( c & ( uint )MonitorCapabilities.MonitorTechnologyType ) != 0;
						//m.HasBrightness = ( c & ( uint )MonitorCapabilities.Brightness ) != 0;
						//m.HasColorTemperature = ( c & ( uint )MonitorCapabilities.ColorTemperature ) != 0;

						//if( m.HasTechnologyType )
						//	SafeNativeMethods.GetMonitorTechnologyType( pm.hPhysicalMonitor, m.DisplayTechnologyType );

						//if( m.HasBrightness )
						//	SafeNativeMethods.GetMonitorBrightness( pm.hPhysicalMonitor, m.MinimumBrightness, m.CurrentBrightness, m.MaximumBrightness );

						//if( m.HasColorTemperature )
						//{
						//	m.Is4000K = ( t & ( uint )MonitorSupportedColorTemperature.t4000K ) != 0;
						//	m.Is5000K = ( t & ( uint )MonitorSupportedColorTemperature.t5000K ) != 0;
						//	m.Is6500K = ( t & ( uint )MonitorSupportedColorTemperature.t6500K ) != 0;
						//	m.Is7500K = ( t & ( uint )MonitorSupportedColorTemperature.t7500K ) != 0;
						//	m.Is8200K = ( t & ( uint )MonitorSupportedColorTemperature.t8200K ) != 0;
						//	m.Is9300K = ( t & ( uint )MonitorSupportedColorTemperature.t9300K ) != 0;
						//	m.Is10000K = ( t & ( uint )MonitorSupportedColorTemperature.t10000K ) != 0;
						//	m.Is11500K = ( t & ( uint )MonitorSupportedColorTemperature.t11500K ) != 0;
						//}

						UnsafeNativeMethods.DestroyPhysicalMonitors( physicalMonitorSize, ref physicalMonitors );
					}
				}
			}
		}

		public static void UpdateWithUserModeDisplayDriverApi(List<Monitor> list)
		{
			uint pathSize = 0u, modeSize = 0u;
			if( SafeNativeMethods.GetDisplayConfigBufferSizes( QueryDisplayFlags.OnlyActivePaths, out pathSize, out modeSize ) == 0 )
			{
				var pathInfo = new DisplayConfigPathInfo[pathSize];
				var modeInfo = new DisplayConfigModeInfo[modeSize];
				SafeNativeMethods.QueryDisplayConfig( QueryDisplayFlags.OnlyActivePaths, ref pathSize, pathInfo, ref modeSize, modeInfo, IntPtr.Zero );
				foreach( var p in pathInfo )
				{
					var idx = p.sourceInfo.Id;

					uint i = 0u;
					DisplayConfigSourceMode sourceMode = default( DisplayConfigSourceMode );
					DisplayConfigTargetMode targetMode = default( DisplayConfigTargetMode );
					foreach( var m in modeInfo )
					{
						if( m.infoType == DisplayConfigModeInfoType.Source && m.id == p.sourceInfo.Id )
						{
							sourceMode = m.sourceMode;
							++i;
						}
						else if( m.infoType == DisplayConfigModeInfoType.Target && m.id == p.targetInfo.Id )
						{
							targetMode = m.targetMode;
							++i;
						}
						if( i == 2u )
							break;
					}

					var m61 = ( Monitor61 )list[( int )idx];
					m61.PixelFormat = sourceMode.PixelFormat;
					m61.SignalStandard = targetMode.targetVideoSignalInfo.videoStandard;
					m61.OutputTechnology = p.targetInfo.OutputTechnology;
					m61.Rotation = p.targetInfo.Rotation;
					m61.Scaling = p.targetInfo.Scaling;
					m61.ScanLineOrdering = p.targetInfo.ScanLineOrdering;
					m61.SyncFreq = new Point( targetMode.targetVideoSignalInfo.vSyncFreq.Value, targetMode.targetVideoSignalInfo.hSyncFreq.Value );
					m61.RefreshRate = p.targetInfo.RefreshRate.Value;
				}
			}
		}

		public static void UpdateWithPerMonitorApi( List<Monitor> list )
		{
			foreach( var obj in list )
			{
				var m = ( Monitor63 )obj;

				uint yDpi = 0, xDpi = 0;
				SafeNativeMethods.GetDpiForMonitor( m.Hmonitor, MonitorDpiType.EffectiveDpi, ref xDpi, ref yDpi );
				m.EffectiveDpi = new Point( xDpi, yDpi );
				SafeNativeMethods.GetDpiForMonitor( m.Hmonitor, MonitorDpiType.AngularDpi, ref xDpi, ref yDpi );
				m.AngularDpi = new Point( xDpi, yDpi );
				SafeNativeMethods.GetDpiForMonitor( m.Hmonitor, MonitorDpiType.RawDpi, ref xDpi, ref yDpi );
				m.RawDpi = new Point( xDpi, yDpi );
			}
		}
	}
}
