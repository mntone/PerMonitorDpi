using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Mntone.MonitorInformation.ViewModels.Infrastructure
{
	public static class RectHelper
	{
		public static Rect Cover( Rect r1, Rect r2 )
		{
			var left = Math.Min( r1.Left, r2.Left );
			var top = Math.Min( r1.Top, r2.Top );
			var right = Math.Max( r1.Right, r2.Right );
			var bottom = Math.Max( r1.Bottom, r2.Bottom );
			return new Rect( left, top, right - left, bottom - top );
		}

		public static Rect Scalar( this Rect r, double constant )
		{
			r.Y *= constant;
			r.X *= constant;
			r.Height *= constant;
			r.Width *= constant;
			return r;
		}
	}
}
