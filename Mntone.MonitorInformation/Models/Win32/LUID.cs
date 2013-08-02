using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct LUID
	{
		public uint LowPart;
		public uint HighPart;
	}
}
