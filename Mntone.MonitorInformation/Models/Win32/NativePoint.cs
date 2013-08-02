using System.Runtime.InteropServices;
using System.Windows;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct NativePoint
	{
		public NativePoint( int x, int y )
		{
			X = x;
			Y = y;
		}

		public NativePoint( Point p ) :
			this( ( int )p.X, ( int )p.Y )
		{
		}

		public int X;
		public int Y;

		public Point ToPoint()
		{
			return new Point( X, Y );
		}

		public static implicit operator NativePoint( Point p )
		{
			return new NativePoint( p );
		}
	}
}
