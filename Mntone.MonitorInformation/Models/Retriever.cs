using Mntone.MonitorInformation.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.MonitorInformation.Models
{
	public sealed class Retriever
	{
		public List<Monitor> Monitors { get { return _Monitors; } }
		private List<Monitor> _Monitors;

		public void Retrieve()
		{
			var os = Environment.OSVersion.Version;
			if( os >= new Version( 6, 3 ) )
			{
				// Windows 8.1 or later
				Retrieve63<Monitor63>();
			}
			else if( os >= new Version( 6, 1 ) )
			{
				// Windows 7 or 8
				Retrieve61<Monitor61>();
			}
			else if( os >= new Version( 6, 0 ) )
			{
				// Windows Vista
				Retrieve60<Monitor60>();
			}
			else if( os >= new Version( 5, 1 ) )
			{
				// Windows XP
				Retrieve51<Monitor>();
			}
			else
			{
				// Windows XP before and exception
				throw new Exception();
			}

#if false&&DEBUG
			Monitors.Add( new Monitor() { No = 1, Area = new System.Windows.Rect() { Y = 0, X = -1024, Height = 768, Width = 1024 } } );
			Monitors.Add( new Monitor() { No = 2, Area = new System.Windows.Rect() { Y = 768, X = 0, Height = 1440, Width = 2560 } } );
#endif
		}

		private void Retrieve63<T>()
			where T: Monitor63, new()
		{
			Retrieve61<T>();
			RetrievingHelper.UpdateWithPerMonitorApi( _Monitors );
		}

		private void Retrieve61<T>()
			where T: Monitor61, new()
		{
			Retrieve60<T>();
			RetrievingHelper.UpdateWithUserModeDisplayDriverApi( _Monitors );
		}

		private void Retrieve60<T>()
			where T: Monitor60, new()
		{
			Retrieve51<T>();
			RetrievingHelper.UpdateWithLowLevelMonitorConfigurationApi( _Monitors );
		}

		private void Retrieve51<T>()
			where T: Monitor, new()
		{
			_Monitors = RetrievingHelper.GetMonitorList<T>();
		}
	}
}
