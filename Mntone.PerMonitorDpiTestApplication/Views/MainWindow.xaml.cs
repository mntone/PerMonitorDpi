using Mntone.MonitorInformation.Models.Win32;
using Mntone.PerMonitorDpi;
using Mntone.PerMonitorDpi.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Mntone.PerMonitorDpiTestApplication.Views
{
	public partial class MainWindow: PmWindow
	{
		private bool _isPerMonitorEnabled = false;

		public MainWindow()
		{
			InitializeComponent();

			SizeChanged += MainWindow_SizeChanged;
		}

		protected override void OnContentRendered( EventArgs e )
		{
			base.OnContentRendered( e );

			var os = Environment.OSVersion.Version;
			_isPerMonitorEnabled = os >= new Version( 6, 3 );

			if( _isPerMonitorEnabled )
			{

				//SetProcessDpiAwareness( PDA.PER_MONITOR_DPI_AWARE );
				var p = PDA.DPI_UNAWARE;
				GetProcessDpiAwareness( Process.GetCurrentProcess().Handle, ref p );

				switch( p )
				{
				case PDA.DPI_UNAWARE:
					pda.Text = "PROCESS_DPI_UNAWARE";
					break;
				case PDA.SYSTEM_DPI_AWARE:
					pda.Text = "PROCESS_DPI_AWARE";
					pda.Foreground = Brushes.Blue;
					break;
				case PDA.PER_MONITOR_DPI_AWARE:
					pda.Text = "PROCESS_MONITOR_DPI_AWARE";
					pda.Foreground = Brushes.Red;
					break;
				}
			}
			else
			{
				if( IsProcessDPIAware() )
				{
					pda.Text = "PROCESS_DPI_AWARE";
					pda.Foreground = Brushes.Blue;
				}
				else
					pda.Text = "PROCESS_DPI_UNAWARE";
			}

			_loaded = true;
			Update();
		}

		private bool _loaded = false;

		protected override void OnLocationChanged( EventArgs e )
		{
			base.OnLocationChanged( e );
			if( _loaded )
				Update();
		}

		private void MainWindow_SizeChanged( object sender, SizeChangedEventArgs e )
		{
			if( _loaded )
			{
				Update();
				e.Handled = true;
			}
		}

		private void Update()
		{
			y.Text = Top.ToString();
			x.Text = Left.ToString();
			h.Text = ActualHeight.ToString();
			w.Text = ActualWidth.ToString();

			var handle = ( ( HwndSource )PresentationSource.FromVisual( this ) ).Handle;
			if( _isPerMonitorEnabled )
			{
				var hmonitor = NativeMethods.MonitorFromWindow( handle, MonitorDefaultTo.Nearest );

				uint yDpi = 0, xDpi = 0;

				NativeMethods.GetDpiForMonitor( hmonitor, MonitorDpiType.EffectiveDpi, ref xDpi, ref yDpi );
				syd.Text = yDpi.ToString();
				sxd.Text = xDpi.ToString();

				NativeMethods.GetDpiForMonitor( hmonitor, MonitorDpiType.AngularDpi, ref xDpi, ref yDpi );
				ayd.Text = yDpi.ToString();
				axd.Text = xDpi.ToString();

				NativeMethods.GetDpiForMonitor( hmonitor, MonitorDpiType.RawDpi, ref xDpi, ref yDpi );
				ryd.Text = yDpi.ToString();
				rxd.Text = xDpi.ToString();

				var mie = new MonitorInfo();
				mie.Init();
				GetMonitorInfo( hmonitor, ref mie );

				double yi, xi, i;
				yi = ( double )( mie.Monitor.Bottom - mie.Monitor.Top ) / ( double )yDpi;
				xi = ( double )( mie.Monitor.Right - mie.Monitor.Left ) / ( double )xDpi;
				i = Math.Sqrt( Math.Pow( yi, 2 ) + Math.Pow( xi, 2 ) );

				yinch.Text = yi.ToString( "0.000" );
				xinch.Text = xi.ToString( "0.000" );
				inch.Text = i.ToString( "0.000" );
			}
			else
			{
				var hdc = GetDC( handle );
				syd.Text = GetDeviceCaps( hdc, DeviceCap.LogPixelsY ).ToString();
				sxd.Text = GetDeviceCaps( hdc, DeviceCap.LogPixelsX ).ToString();
				ReleaseDC( handle, hdc );

				ayd.Text = axd.Text = ryd.Text = rxd.Text = yinch.Text = xinch.Text = inch.Text = "---";
			}
		}

		[DllImport( "user32.dll" )]
		private static extern bool IsProcessDPIAware();

		public enum PDA
		{
			DPI_UNAWARE = 0,
			SYSTEM_DPI_AWARE = 1,
			PER_MONITOR_DPI_AWARE = 2,
		}

		[DllImport( "SHCore.dll", CharSet = CharSet.Unicode, PreserveSig = false )]
		private static extern void SetProcessDpiAwareness( PDA value );

		[DllImport( "SHCore.dll", CharSet = CharSet.Unicode, PreserveSig = false )]
		private static extern void GetProcessDpiAwareness( IntPtr hprocess, ref PDA value );

		[StructLayout( LayoutKind.Sequential )]
		internal struct MonitorInfo
		{
			public int Size;
			public NativeRect Monitor;
			public NativeRect WorkArea;
			public uint Flags;

			public void Init()
			{
				Size = 40;
			}
		}

		[DllImport( "gdi32.dll" )]
		public static extern int GetDeviceCaps( IntPtr hdc, DeviceCap index );

		[DllImport( "user32.dll" )]
		internal static extern IntPtr GetDC( IntPtr hwnd );

		[DllImport( "user32.dll" )]
		internal static extern bool ReleaseDC( IntPtr hwnd, IntPtr hdc );

		[DllImport( "user32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
		private static extern bool GetMonitorInfo( IntPtr hMonitor, ref MonitorInfo lpmi );
	}
}
