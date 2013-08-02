using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfigPathInfo
	{
		public DisplayConfigPathSourceInfo sourceInfo;
		public DisplayConfigPathTargetInfo targetInfo;
		public uint flags;
	}
}
