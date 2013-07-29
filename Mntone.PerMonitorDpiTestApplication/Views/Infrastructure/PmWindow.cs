using Mntone.PerMonitorDpiTestApplication.Views.Infrastructure.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Mntone.PerMonitorDpiTestApplication.Views.Infrastructure
{
	public class PmWindow: Window
	{
		private double _dpiY = 1.0, _dpiX = 1.0;
		private HwndSource _hwndSource;

		protected override void OnInitialized( EventArgs e )
		{
			base.OnInitialized( e );
			Loaded += PmWindow_Loaded;
		}

		private void PmWindow_Loaded( object sender, RoutedEventArgs e )
		{
			Loaded -= PmWindow_Loaded;

			_hwndSource = ( HwndSource )HwndSource.FromVisual( this );
			_hwndSource.AddHook( WndProc );

			CheckDpi();
		}

		protected override void OnClosing( CancelEventArgs e )
		{
			_hwndSource.RemoveHook( WndProc );
			base.OnClosing( e );
		}

		private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
		{
			var wmMsg = ( WindowMessage )msg;
			switch( wmMsg )
			{
			case WindowMessage.DISPLAYCHANGE:
				CheckDpi();
				break;
			case WindowMessage.DPICHANGED:
				var xDpi = NativeHelper.GetHiWord( ( uint )wParam );
				var yDpi = NativeHelper.GetLoWord( ( uint )wParam );
				ChangeDpi( yDpi, xDpi );
				handled = true;
				break;
			}
			return IntPtr.Zero;
		}

		private void CheckDpi()
		{			
			var hmonitor = NativeMethods.MonitorFromWindow( _hwndSource.Handle, MonitorDefaultTo.Nearest );

			uint yDpi = 0, xDpi = 0;
			NativeMethods.GetDpiForMonitor( hmonitor, MonitorDpiType.EffectiveDpi, ref xDpi, ref yDpi );
			ChangeDpi( yDpi, xDpi );
		}

		private void ChangeDpi( uint y, uint x )
		{
			var c = Content as FrameworkElement;
			if( c == null )
				return;

			if( c.LayoutTransform == null )
			{
				var st = new ScaleTransform();
				CalcDpi( ref st, y, x );
				c.LayoutTransform = st;
			}
			else if( c.LayoutTransform.GetType() == typeof( ScaleTransform ) )
			{
				var st = ( ScaleTransform )c.LayoutTransform;
				CalcDpi( ref st, y, x );
				c.LayoutTransform = st;
			}
			else if( c.LayoutTransform.GetType() == typeof( TransformGroup ) )
			{
				var list = ( ( TransformGroup )c.LayoutTransform ).Children;
				for( var i = 0; i < list.Count; ++i )
				{
					if( list[i].GetType() == typeof( ScaleTransform ) )
					{
						var st = ( ScaleTransform )list[i];
						CalcDpi( ref st, y, x );
						list[i] = st;
						break;
					}
				}
			}
		}

		private void CalcDpi( ref ScaleTransform st, uint y, uint x )
		{
			var yd = st.ScaleY = ( double )y / 96.0;
			var xd = st.ScaleX = ( double )x / 96.0;
			Height *= yd / _dpiY;
			Width *= xd / _dpiX;
			_dpiY = yd;
			_dpiX = xd;
		}


		#region 依存プロパティー

		public bool IsPerMonitorDpi
		{
			get { return ( bool )GetValue( IsPerMonitorDpiProperty ); }
			set { SetValue( IsPerMonitorDpiProperty, value ); }
		}
		public static readonly DependencyProperty IsPerMonitorDpiProperty
			= DependencyProperty.Register( "IsPerMonitorDpi", typeof( bool ), typeof( PmWindow ), new PropertyMetadata( true ) );

		public double MaxYDpi
		{
			get { return ( double )GetValue( MaxYDpiProperty ); }
			set { SetValue( MaxYDpiProperty, value ); }
		}
		public static readonly DependencyProperty MaxYDpiProperty
			= DependencyProperty.Register( "MaxYDpi", typeof( double ), typeof( PmWindow ), new PropertyMetadata( 0.0 ) );

		public double MaxXDpi
		{
			get { return ( double )GetValue( MaxXDpiProperty ); }
			set { SetValue( MaxXDpiProperty, value ); }
		}
		public static readonly DependencyProperty MaxXDpiProperty
			= DependencyProperty.Register( "MaxXDpi", typeof( double ), typeof( PmWindow ), new PropertyMetadata( 0.0 ) );

		#endregion
	}
}
