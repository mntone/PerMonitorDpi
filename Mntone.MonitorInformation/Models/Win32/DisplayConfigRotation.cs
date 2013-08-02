
namespace Mntone.MonitorInformation.Models.Win32
{
	public enum DisplayConfigRotation: uint
	{
		Zero = 0x0,

		Identity = 1,
		Rotate90 = 2,
		Rotate180 = 3,
		Rotate270 = 4,
		ForceUint32 = 0xffffffff,
	}
}
