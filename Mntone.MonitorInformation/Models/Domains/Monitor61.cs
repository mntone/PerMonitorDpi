using Mntone.MonitorInformation.Models.Win32;
using System.Windows;

namespace Mntone.MonitorInformation.Models.Domains
{
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
}
