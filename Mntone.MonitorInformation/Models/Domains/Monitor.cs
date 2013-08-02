using Mntone.MonitorInformation.Models.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				return "No: " + No + "\n"
					+ "\nArea: " + Area.ToString()
					+ "\nSystem dpi: " + SystemDpi.ToString();
			}
		}
	}

	// Windows Vista or later
	public class Monitor60: Monitor
	{
		public string Name { get; internal set; }

		//public bool HasTechnologyType { get; internal set; }
		//public DisplayTechnologyType DisplayTechnologyType { get; internal set; }

		//public bool HasBrightness { get; internal set; }
		//public uint MinimumBrightness { get; internal set; }
		//public uint CurrentBrightness { get; internal set; }
		//public uint MaximumBrightness { get; internal set; }

		//public bool HasColorTemperature { get; internal set; }
		//public bool Is4000K { get; internal set; }
		//public bool Is5000K { get; internal set; }
		//public bool Is6500K { get; internal set; }
		//public bool Is7500K { get; internal set; }
		//public bool Is8200K { get; internal set; }
		//public bool Is9300K { get; internal set; }
		//public bool Is10000K { get; internal set; }
		//public bool Is11500K { get; internal set; }

		public new string Description
		{
			get
			{
				var str = base.Description + "\n\nName: " + Name;
				//if( HasBrightness ) str += "\nMin. brightness:" + MinimumBrightness + "\n" + "Current brightness:" + CurrentBrightness + "\n" + "Max. brightness:" + MaximumBrightness;
				return str;
			}
		}
	}

	// Windows 7 or later
	public class Monitor61: Monitor60
	{
		public DisplayConfigPixelFormat PixelFormat { get; internal set; }
		public D3DmdtVideoSignalStandard SignalStandard { get; internal set; }
		public DisplayConfigVideoOutputTechnology OutputTechnology { get; internal set; }
		public DisplayConfigRotation Rotation { get; internal set; }
		public DisplayConfigScaling Scaling { get; internal set; }
		public DisplayConfigScanLineOrdering ScanLineOrdering { get; internal set; }
		public Point SyncFreq { get; internal set; }
		public double RefreshRate { get; internal set; }

		public new string Description
		{
			get
			{
				return base.Description + "\n"
					+ "\nPixelFormat: " + PixelFormat.ToString()
					+ "\nSyncFreq: " + SyncFreq.ToString()
					+ "\nSignalStandard: " + SignalStandard.ToString()
					+ "\nOutputTechnology: " + OutputTechnology.ToString()
					+ "\nRotation: " + Rotation.ToString()
					+ "\nScaling: " + Scaling.ToString()
					+ "\nRefreshRate: " + RefreshRate.ToString()
					+ "\nScanLineOrdering: " + ScanLineOrdering.ToString();
			}
		}
	}

	// Windows 8.1 or later
	public class Monitor63: Monitor61
	{
		public Point EffectiveDpi { get; internal set; }
		public Point AngularDpi { get; internal set; }
		public Point RawDpi { get; internal set; }

		public new string Description
		{
			get
			{
				return base.Description + "\n"
					+ "\nEffective dpi: " + EffectiveDpi.ToString()
					+ "\nAngular dpi: " + AngularDpi.ToString()
					+ "\nRaw dpi: " + RawDpi.ToString();
			}
		}
	}
}
