
namespace Mntone.MonitorInformation.Models.Win32
{
	public enum DisplayConfigTopologyId: uint
	{
		Zero = 0,

		Internal = 1,
		Clone = 2,
		Extend = 4,
		External = 8,
		ForceUint32 = 0xffffffff,
	}
}
