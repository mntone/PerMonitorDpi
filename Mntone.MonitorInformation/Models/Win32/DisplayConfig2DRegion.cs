using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfig2DRegion
	{
		public uint cx;
		public uint cy;
	}
}
