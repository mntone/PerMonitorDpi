using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfigPathTargetInfo
	{
		public LUID AdapterId;
		public uint Id;
		public uint ModeInfoIdx;
		public DisplayConfigVideoOutputTechnology OutputTechnology;
		public DisplayConfigRotation Rotation;
		public DisplayConfigScaling Scaling;
		public DisplayConfigRational RefreshRate;
		public DisplayConfigScanLineOrdering ScanLineOrdering;

		public bool TargetAvailable;
		public DisplayConfigTargetStatus StatusFlags;
	}
}
