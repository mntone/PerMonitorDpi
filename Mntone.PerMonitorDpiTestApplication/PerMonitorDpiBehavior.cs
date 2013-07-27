using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Interop;
using System.Windows.Media;

namespace Mntone.PerMonitorDpiTestApplication
{
	internal sealed class PerMonitorDpiBehavior: Behavior<Window>
	{
		private const uint WM_DISPLAYCHANGE = 0x7e;
		private const uint WM_DPICHANGED = 0x2e0;
		private double oldYDpi = 1.0;
		private double oldXDpi = 1.0;

		private HwndSource _hwndSource;

		protected override void OnAttached()
		{
			base.OnAttached();
			AssociatedObject.Loaded += Loaded;
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
			Unloaded();
		}

		private void Loaded( object sender, RoutedEventArgs e )
		{
			AssociatedObject.Loaded -= Loaded;
			_hwndSource = ( HwndSource )HwndSource.FromVisual( AssociatedObject );
			_hwndSource.AddHook( WndProc );

			var hmonitor = MainWindow.MonitorFromWindow( _hwndSource.Handle, MainWindow.MO.DEFAULTTONEAREST );

			UIntPtr yDpi = UIntPtr.Zero, xDpi = UIntPtr.Zero;
			MainWindow.GetDpiForMonitor( hmonitor, MainWindow.MDT.EFFECTIVE_DPI, ref xDpi, ref yDpi );
			ChangeDpi( ( double )yDpi / 96.0, ( double )xDpi / 96.0 );
		}

		private void Unloaded()
		{
			_hwndSource.RemoveHook( WndProc );
		}

		private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
		{
			if( msg == WM_DISPLAYCHANGE || msg == WM_DPICHANGED )
			{
				// Maybe.
				var xDpi = GetHiWord( ( uint )wParam );
				var yDpi = GetLoWord( ( uint )wParam );

				var newYDpi = ( double )yDpi / 96.0;
				var newXDpi = ( double )xDpi / 96.0;

				if( newYDpi != oldYDpi || newXDpi != oldXDpi )
					ChangeDpi( newYDpi, newXDpi );
			}
			return IntPtr.Zero;
		}

		private void ChangeDpi( double newYDpi, double newXDpi )
		{
			var st = ( ScaleTransform )( ( FrameworkElement )AssociatedObject.Content ).LayoutTransform;
			st.ScaleY = newYDpi;
			st.ScaleX = newXDpi;
			( ( FrameworkElement )AssociatedObject.Content ).LayoutTransform = st;

			AssociatedObject.Height *= newYDpi / oldYDpi;
			AssociatedObject.Width *= newXDpi / oldXDpi;

			oldYDpi = newYDpi;
			oldXDpi = newXDpi;
		}

		private ushort GetLoWord( uint dword )
		{
			return ( ushort )( dword & 0xffff );
		}

		private ushort GetHiWord( uint dword )
		{
			return ( ushort )( dword >> 16 );
		}
	}
}
