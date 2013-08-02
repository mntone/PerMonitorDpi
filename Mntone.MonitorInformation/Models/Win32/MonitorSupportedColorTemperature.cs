
namespace Mntone.MonitorInformation.Models.Win32
{
	enum MonitorSupportedColorTemperature: uint
	{
		None = 0x00,
		t4000K = 0x01,
		t5000K = 0x02,
		t6500K = 0x04,
		t7500K = 0x08,
		t8200K = 0x10,
		t9300K = 0x20,
		t10000K = 0x40,
		t11500K = 0x80,
	}
}
