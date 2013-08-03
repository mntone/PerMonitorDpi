
namespace Mntone.MonitorInformation.Models.Domains
{
	// Windows Vista or later
	public class Monitor60: Monitor
	{
		public string Name { get; internal set; }

		//public bool HasTechnologyType { get; internal set; }
		//public DisplayTechnologyType DisplayTechnologyType { get; internal set; }

		//public bool HasBrightness { get; internal set; }
		//public uint MinimumBrightness { get; internal set; }
		//public uint CurrentBrightness { get; internal set; }
		//public uint MaximumBrightness { get; internal set; }

		//public bool HasColorTemperature { get; internal set; }
		//public bool Is4000K { get; internal set; }
		//public bool Is5000K { get; internal set; }
		//public bool Is6500K { get; internal set; }
		//public bool Is7500K { get; internal set; }
		//public bool Is8200K { get; internal set; }
		//public bool Is9300K { get; internal set; }
		//public bool Is10000K { get; internal set; }
		//public bool Is11500K { get; internal set; }

		public new string Description
		{
			get
			{
				var str = base.Description + "\n\nName: " + Name;
				//if( HasBrightness ) str += "\nMin. brightness:" + MinimumBrightness + "\n" + "Current brightness:" + CurrentBrightness + "\n" + "Max. brightness:" + MaximumBrightness;
				return str;
			}
		}
	}
}
