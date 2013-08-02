
namespace Mntone.MonitorInformation.Models.Win32
{
	public enum DisplayConfigScanLineOrdering: uint
	{
		Unspecified = 0,
		Progressive = 1,
		Interlaced = 2,
		InterlacedUpperFieldFirst = Interlaced,
		InterlacedLowerFieldFirst = 3,
		ForceUint32 = 0xffffffff,
	}
}
