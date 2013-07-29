using Mntone.PerMonitorDpi;
using Mntone.PerMonitorDpiTestApplication.Views.Infrastructure.Win32;
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
		public MainWindow()
		{
			InitializeComponent();

			Loaded += MainWindow_Loaded;
			SizeChanged += MainWindow_SizeChanged;
		}

		protected override void OnContentRendered( EventArgs e )
		{
			base.OnContentRendered( e );

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

		private bool _loaded = false;
		private void MainWindow_Loaded( object sender, RoutedEventArgs e )
		{
			_loaded = true;
			Update();
		}

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

			var hmonitor = NativeMethods.MonitorFromWindow( ( ( HwndSource )PresentationSource.FromVisual( this ) ).Handle, MonitorDefaultTo.Nearest );
			
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

			var mie = new MonitorInfoEx();
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


		// size of a device name string
		private const int CCHDEVICENAME = 32;

		/// <summary>
		/// The MONITORINFOEX structure contains information about a display monitor.
		/// The GetMonitorInfo function stores information into a MONITORINFOEX structure or a MONITORINFO structure.
		/// The MONITORINFOEX structure is a superset of the MONITORINFO structure. The MONITORINFOEX structure adds a string member to contain a name
		/// for the display monitor.
		/// </summary>
		[StructLayout( LayoutKind.Sequential, CharSet = CharSet.Unicode )]
		internal struct MonitorInfoEx
		{
			/// <summary>
			/// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFOEX) (72) before calling the GetMonitorInfo function.
			/// Doing so lets the function determine the type of structure you are passing to it.
			/// </summary>
			public int Size;

			/// <summary>
			/// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates.
			/// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
			/// </summary>
			public RectStruct Monitor;

			/// <summary>
			/// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications,
			/// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor.
			/// The rest of the area in rcMonitor contains system windows such as the task bar and side bars.
			/// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
			/// </summary>
			public RectStruct WorkArea;

			/// <summary>
			/// The attributes of the display monitor.
			///
			/// This member can be the following value:
			///   1 : MONITORINFOF_PRIMARY
			/// </summary>
			public uint Flags;

			/// <summary>
			/// A string that specifies the device name of the monitor being used. Most applications have no use for a display monitor name,
			/// and so can save some bytes by using a MONITORINFO structure.
			/// </summary>
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME )]
			public string DeviceName;

			public void Init()
			{
				this.Size = 40 + 2 * CCHDEVICENAME;
				this.DeviceName = string.Empty;
			}
		}

		/// <summary>
		/// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
		/// </summary>
		/// <see cref="http://msdn.microsoft.com/en-us/library/dd162897%28VS.85%29.aspx"/>
		/// <remarks>
		/// By convention, the right and bottom edges of the rectangle are normally considered exclusive.
		/// In other words, the pixel whose coordinates are ( right, bottom ) lies immediately outside of the the rectangle.
		/// For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including,
		/// the right column and bottom row of pixels. This structure is identical to the RECTL structure.
		/// </remarks>
		[StructLayout( LayoutKind.Sequential )]
		public struct RectStruct
		{
			/// <summary>
			/// The x-coordinate of the upper-left corner of the rectangle.
			/// </summary>
			public int Left;

			/// <summary>
			/// The y-coordinate of the upper-left corner of the rectangle.
			/// </summary>
			public int Top;

			/// <summary>
			/// The x-coordinate of the lower-right corner of the rectangle.
			/// </summary>
			public int Right;

			/// <summary>
			/// The y-coordinate of the lower-right corner of the rectangle.
			/// </summary>
			public int Bottom;
		}

		[DllImport( "user32.dll", CharSet = CharSet.Unicode, SetLastError = true )]
		private static extern bool GetMonitorInfo( IntPtr hMonitor, ref MonitorInfoEx lpmi );
	}
}
