using System;

namespace Mntone.MonitorInformation.Models.Win32
{
	public sealed class DeviceContextHandle: IDisposable
	{
		private IntPtr _hwnd = IntPtr.Zero;
		private IntPtr _hdc = IntPtr.Zero;

		public DeviceContextHandle()
		{
			Attach( IntPtr.Zero );
		}

		public DeviceContextHandle( IntPtr hwnd )
		{
			Attach( hwnd );
		}

		public void Attach( IntPtr hwnd )
		{
			Dispose();

			_hwnd = hwnd;
			_hdc = UnsafeNativeMethods.GetDC( hwnd );
		}

		public void Detach()
		{
			Dispose();
		}

		public void Dispose()
		{
			if( _hdc != IntPtr.Zero )
			{
				UnsafeNativeMethods.ReleaseDC( _hwnd, _hdc );
				_hdc = IntPtr.Zero;
				_hwnd = IntPtr.Zero;
			}
			GC.SuppressFinalize( this );
		}

		public IntPtr HDC { get { return _hdc; } }

		public int GetDeviceCaps( DeviceCap index )
		{
			return SafeNativeMethods.GetDeviceCaps( _hdc, index );
		}
	}
}
