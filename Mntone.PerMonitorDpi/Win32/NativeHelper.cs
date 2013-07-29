
namespace Mntone.PerMonitorDpi.Win32
{
	internal static class NativeHelper
	{
		public static ushort GetLoWord( uint dword )
		{
			return ( ushort )( dword & 0xffff );
		}

		public static ushort GetHiWord( uint dword )
		{
			return ( ushort )( dword >> 16 );
		}
	}
}
