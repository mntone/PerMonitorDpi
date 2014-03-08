
namespace Mntone.PerMonitorDpiTestApplication.Views.Infrastructure.Win32
{
	public static class NativeHelper
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
