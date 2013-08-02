using System.Runtime.InteropServices;

namespace Mntone.MonitorInformation.Models.Win32
{
	[StructLayout( LayoutKind.Sequential )]
	public struct DisplayConfigRational
	{
		public uint Numerator;
		public uint Denominator;

		public double Value { get { return ( double )Numerator / ( double )Denominator; } }
	}
}
