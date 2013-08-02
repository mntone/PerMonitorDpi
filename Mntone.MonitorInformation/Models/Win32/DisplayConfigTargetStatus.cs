
namespace Mntone.MonitorInformation.Models.Win32
{
	public enum DisplayConfigTargetStatus: uint
	{
		Zero = 0x00,

		InUse = 0x01,
		Forcible = 0x02,
		ForcedAvailabilityBoot = 0x04,
		ForcedAvailabilityPath = 0x08,
		ForcedAvailabilitySystem = 0x10,
	}
}
