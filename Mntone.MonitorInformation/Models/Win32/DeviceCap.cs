
namespace Mntone.MonitorInformation.Models.Win32
{
	public enum DeviceCap: int
	{
		DriverVersion = 0,
		Technology = 2,

		HorzSize = 4,
		VertSize = 6,
		HorzRes = 8,
		VertRes = 10,
		BitsPixel = 12,
		Planes = 14,

		NumBrushes = 16,
		NumPens = 18,
		NumMarkers = 20,
		NumFonts = 22,
		NumColors = 24,

		PDeviceSize = 26,

		CurveCaps = 28,
		LineCaps = 30,
		PolygonalCaps = 32,
		TextCaps = 34,
		ClipCaps = 36,
		RasterCaps = 38,

		AspectX = 40,
		AspectY = 42,
		AspectXY = 44,

		ShadeBlendCaps = 45,

		LogPixelsX = 88,
		LogPixelsY = 90,

		SizePalette = 104,
		NumReserved = 106,
		ColorRes = 108,

		PhysicalWidth = 110,
		PhysicalHeight = 111,
		PhysicalOffsetX = 112,
		PhysicalOffsetY = 113,

		ScalingFactorX = 114,
		ScalingFactorY = 115,

		VRefresh = 116,

		DesktopVertres = 117,
		DesktopHorzres = 118,

		BltAlignment = 119,
	}
}
