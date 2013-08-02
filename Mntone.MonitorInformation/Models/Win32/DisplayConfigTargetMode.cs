using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfigTargetMode
	{
		public DisplayConfigVideoSignalInfo targetVideoSignalInfo;
	}
}
