using Mntone.PerMonitorDpi.Win32;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Mntone.PerMonitorDpi
{
	public class PmWindow: Window
	{
		private bool _isPerMonitorEnabled = false;
		private double _dpiY = 1.0, _dpiX = 1.0;
		private HwndSource _hwndSource;

		protected override void OnInitialized( EventArgs e )
		{
			base.OnInitialized( e );

			var os = Environment.OSVersion.Version;
			_isPerMonitorEnabled = os >= new Version( 6, 3 );

			if( _isPerMonitorEnabled )
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
			if( _isPerMonitorEnabled )
				_hwndSource.RemoveHook( WndProc );

			base.OnClosing( e );
		}

		private IntPtr WndProc( IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled )
		{
			var wmMsg = ( WindowMessage )msg;
			switch( wmMsg )
			{
			case WindowMessage.DisplayChange:
				CheckDpi();
				break;
			case WindowMessage.DpiChanged:
				if( DpiMode == DpiMode.System )
				{
					var xDpi = NativeHelper.GetHiWord( ( uint )wParam );
					var yDpi = NativeHelper.GetLoWord( ( uint )wParam );
					ChangeDpi( yDpi, xDpi );
				}
				else
					CheckDpi();
				handled = true;
				break;
			}
			return IntPtr.Zero;
		}

		private void CheckDpi()
		{			
			var hmonitor = NativeMethods.MonitorFromWindow( _hwndSource.Handle, MonitorDefaultTo.Nearest );

			MonitorDpiType type;
			switch( DpiMode )
			{
			case DpiMode.System:
				type = MonitorDpiType.EffectiveDpi;
				break;
			case DpiMode.LimitedReal:
				type = MonitorDpiType.AngularDpi;
				break;
			case DpiMode.Real:
				type = MonitorDpiType.RawDpi;
				break;
			default:
				throw new ArgumentException();
			}

			uint yDpi = 0, xDpi = 0;
			NativeMethods.GetDpiForMonitor( hmonitor, type, ref xDpi, ref yDpi );
			ChangeDpi( yDpi, xDpi );
		}

		private void ChangeDpi( uint y, uint x )
		{
			var c = Content as FrameworkElement;
			if( c == null )
				return;

			if( c.LayoutTransform == Transform.Identity )
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
				bool flag = true;

				var list = ( ( TransformGroup )c.LayoutTransform ).Children;
				for( var i = 0; i < list.Count; ++i )
				{
					if( list[i].GetType() == typeof( ScaleTransform ) )
					{
						var st = ( ScaleTransform )list[i];
						CalcDpi( ref st, y, x );
						list[i] = st;
						flag = false;
						break;
					}
				}

				if( flag )
				{
					var st = new ScaleTransform();
					CalcDpi( ref st, y, x );
					list.Add( st );
				}
			}
		}

		private void CalcDpi( ref ScaleTransform st, uint y, uint x )
		{
			var yDpi = ( double )y;
			if( yDpi > MaxYDpi )
				yDpi = MaxYDpi;
			else if( yDpi < MinYDpi )
				yDpi = MinYDpi;

			var xDpi = ( double )x;
			if( xDpi > MaxXDpi )
				xDpi = MaxXDpi;
			else if( xDpi < MinXDpi )
				xDpi = MinXDpi;

			var yd = yDpi / 96.0;
			var xd = xDpi / 96.0;
			st.ScaleY = yd;
			st.ScaleX = xd;
			Height *= yd / _dpiY;
			Width *= xd / _dpiX;
			_dpiY = yd;
			_dpiX = xd;
		}


		#region 依存プロパティー

		public DpiMode DpiMode
		{
			get { return ( DpiMode )GetValue( DpiModeProperty ); }
			set { SetValue( DpiModeProperty, value ); }
		}
		public static readonly DependencyProperty DpiModeProperty
			= DependencyProperty.Register( "DpiMode", typeof( DpiMode ), typeof( PmWindow ), new PropertyMetadata( DpiMode.System ) );

		public double MinYDpi
		{
			get { return ( double )GetValue( MinYDpiProperty ); }
			set { SetValue( MinYDpiProperty, value ); }
		}
		public static readonly DependencyProperty MinYDpiProperty
			= DependencyProperty.Register( "MinYDpi", typeof( double ), typeof( PmWindow ), new PropertyMetadata( double.MinValue ) );

		public double MinXDpi
		{
			get { return ( double )GetValue( MinXDpiProperty ); }
			set { SetValue( MinXDpiProperty, value ); }
		}
		public static readonly DependencyProperty MinXDpiProperty
			= DependencyProperty.Register( "MinXDpi", typeof( double ), typeof( PmWindow ), new PropertyMetadata( double.MinValue ) );

		public double MaxYDpi
		{
			get { return ( double )GetValue( MaxYDpiProperty ); }
			set { SetValue( MaxYDpiProperty, value ); }
		}
		public static readonly DependencyProperty MaxYDpiProperty
			= DependencyProperty.Register( "MaxYDpi", typeof( double ), typeof( PmWindow ), new PropertyMetadata( double.MaxValue ) );

		public double MaxXDpi
		{
			get { return ( double )GetValue( MaxXDpiProperty ); }
			set { SetValue( MaxXDpiProperty, value ); }
		}
		public static readonly DependencyProperty MaxXDpiProperty
			= DependencyProperty.Register( "MaxXDpi", typeof( double ), typeof( PmWindow ), new PropertyMetadata( double.MaxValue ) );

		#endregion
	}
}
