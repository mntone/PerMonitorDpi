using Livet;
using Mntone.MonitorInformation.Models.Domains;
using System.Windows;

namespace Mntone.MonitorInformation.ViewModels
{
	public sealed class MonitorViewModel: NotificationObject
	{
		public Monitor Domain { get { return _Domain; } }
		private readonly Monitor _Domain;

		public MonitorViewModel( Monitor domain )
		{
			_Domain = domain;
		}
	}
}
