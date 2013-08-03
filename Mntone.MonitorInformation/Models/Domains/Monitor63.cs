using System.Windows;

namespace Mntone.MonitorInformation.Models.Domains
{
	// Windows 8.1 or later
	public class Monitor63: Monitor61
	{
		public Point EffectiveDpi { get; internal set; }
		public Point AngularDpi { get; internal set; }
		public Point RawDpi { get; internal set; }

		public new string Description
		{
			get
			{
				return base.Description + "\n"
					+ "\nEffective dpi: " + EffectiveDpi.ToString()
					+ "\nAngular dpi: " + AngularDpi.ToString()
					+ "\nRaw dpi: " + RawDpi.ToString();
			}
		}
	}
}
