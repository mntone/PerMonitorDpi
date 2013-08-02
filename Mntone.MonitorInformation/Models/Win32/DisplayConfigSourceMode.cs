using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfigSourceMode
	{
		public uint Width;
		public uint Height;
		public DisplayConfigPixelFormat PixelFormat;
		public NativePoint Position;
	}
}
