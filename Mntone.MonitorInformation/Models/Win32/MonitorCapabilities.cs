
namespace Mntone.MonitorInformation.Models.Win32
{
	enum MonitorCapabilities: uint
	{
		None = 0x0000,
		MonitorTechnologyType = 0x0001,
		Brightness = 0x0002,
		Contrast = 0x0004,
		ColorTemperature = 0x0008,
		RedGreenBlueGain = 0x0010,
		RedGreenBlueDrive = 0x0020,
		Degauss = 0x0040,

		DisplayAreaPosition = 0x0080,
		DisplayAreaSize = 0x0100,

		RestoreFactoryDefaults = 0x0400,
		RestoreFactoryColorDefaults = 0x0800,
		RestoreFactoryDefaultsEnablesMonitorSettings = 0x1000,
	}
}
