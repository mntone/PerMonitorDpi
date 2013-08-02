
namespace Mntone.MonitorInformation.Models.Win32
{
	public enum DisplayConfigScaling: uint
	{
		Zero = 0x0,

		Identity = 1,
		Centered = 2,
		Stretched = 3,
		AspectRatioCenteredMax = 4,
		Custom = 5,
		Preferred = 128,
		ForceUint32 = 0xffffffff,
	}
}
