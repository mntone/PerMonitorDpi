
namespace Mntone.PerMonitorDpi
{
	/// <summary>
	/// Resize Window according to dpimode.
	/// </summary>
	public enum DpiMode: uint
	{
		/// <summary>
		/// Resize with system default dpi
		/// </summary>
		System = 1,

		/// <summary>
		/// Resize with limited real dpi.
		/// This value is suggested by the system.
		/// </summary>
		LimitedReal,

		/// <summary>
		/// Resize with real dpi.
		/// </summary>
		Real,
	}
}
