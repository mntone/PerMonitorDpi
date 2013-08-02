using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct NativeRect
	{
		public NativeRect( int left, int top, int right, int bottom )
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public NativeRect( NativePoint p1, NativePoint p2 ) :
			this( p1.X, p1.Y, p2.X, p2.Y )
		{

		}

		public NativeRect( Point p1, Point p2 ) :
			this( ( int )p1.X, ( int )p1.Y, ( int )p2.X, ( int )p2.Y )
		{
		}

		public NativeRect( Rect r ) :
			this( ( int )r.Left, ( int )r.Top, ( int )r.Right, ( int )r.Bottom )
		{
		}

		public int Left;
		public int Top;
		public int Right;
		public int Bottom;

		public int X
		{
			get { return Left; }
			set { Left = X; }
		}
		public int Y
		{
			get { return Top; }
			set { Top = Y; }
		}
		public int Width
		{
			get { return Right - Left; }
			set { Right = Left + Width; }
		}
		public int Height
		{
			get { return Bottom - Top; }
			set { Bottom = Top + Height; }
		}

		public NativePoint TopLeft
		{
			get { return new NativePoint( Left, Top ); }
			set { Left = value.X; Top = value.Y; }
		}
		public NativePoint TopRight
		{
			get { return new NativePoint( Right, Top ); }
			set { Right = value.X; Top = value.Y; }
		}
		public NativePoint BottomLeft
		{
			get { return new NativePoint( Left, Bottom ); }
			set { Left = value.X; Bottom = value.Y; }
		}
		public NativePoint BottomRight
		{
			get { return new NativePoint( Right, Bottom ); }
			set { Right = value.X; Bottom = value.Y; }
		}

		public Tuple<Point, Point> ToPoints()
		{
			return Tuple.Create( new Point( Left, Top ), new Point( Right, Bottom ) );
		}
		public Rect ToRect()
		{
			return new Rect( Left, Top, Width, Height );
		}

		public static implicit operator NativeRect( Rect r )
		{
			return new NativeRect( r );
		}
	}
}
