using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfigPathSourceInfo
	{
		public LUID AdapterId;
		public uint Id;
		public uint ModeInfoIdx;

		public DisplayConfigSourceStatus StatusFlags;
	}
}
