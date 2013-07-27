using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mntone.PerMonitorDpiTestApplication
{
	public partial class MainWindow: Window
	{
		public MainWindow()
		{
			InitializeComponent();

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

		protected override void OnLocationChanged( EventArgs e )
		{
			base.OnLocationChanged( e );
			Update();
		}

		private void MainWindow_SizeChanged( object sender, SizeChangedEventArgs e )
		{
			Update();
		}

		private void Update()
		{
			y.Text = Top.ToString();
			x.Text = Left.ToString();
			h.Text = ActualHeight.ToString();
			w.Text = ActualWidth.ToString();

			var hmonitor = MonitorFromWindow( ( ( HwndSource )PresentationSource.FromVisual( this ) ).Handle, MO.DEFAULTTONEAREST );
			
			UIntPtr yDpi = UIntPtr.Zero, xDpi = UIntPtr.Zero;

			GetDpiForMonitor( hmonitor, MDT.EFFECTIVE_DPI, ref xDpi, ref yDpi );
			syd.Text = yDpi.ToString();
			sxd.Text = xDpi.ToString();

			GetDpiForMonitor( hmonitor, MDT.ANGULAR_DPI, ref xDpi, ref yDpi );
			ayd.Text = yDpi.ToString();
			axd.Text = xDpi.ToString();

			GetDpiForMonitor( hmonitor, MDT.RAW_DPI, ref xDpi, ref yDpi );
			ryd.Text = yDpi.ToString();
			rxd.Text = xDpi.ToString();
		}

		public enum MDT
		{
			EFFECTIVE_DPI = 0,
			ANGULAR_DPI = 1,
			RAW_DPI = 2,
			DEFAULT = EFFECTIVE_DPI,
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
		
		[DllImport( "SHCore.dll", CharSet = CharSet.Unicode, PreserveSig = false )]
		public static extern void GetDpiForMonitor( IntPtr hmonitor, MDT dpiType, ref UIntPtr dpiX, ref UIntPtr dpiY );

		[DllImport( "user32.dll", SetLastError = true )]
		public static extern IntPtr MonitorFromWindow( IntPtr hwnd, MO dwFlags );

		public enum MO: int
		{
			DEFAULTTONULL = 0,
			DEFAULTTOPRIMARY = 1,
			DEFAULTTONEAREST = 2,
		}
	}
}
