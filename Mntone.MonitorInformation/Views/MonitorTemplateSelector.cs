using System.Windows;
using System.Windows.Controls;

namespace Mntone.MonitorInformation.Views
{
	public class MonitorTemplateSelector: DataTemplateSelector
	{
		public DataTemplate MonitorDataTemplate { get; set; }
		public DataTemplate Monitor60DataTemplate { get; set; }
		public DataTemplate Monitor61DataTemplate { get; set; }
		public DataTemplate Monitor63DataTemplate { get; set; }

		public override DataTemplate SelectTemplate( object item, DependencyObject container )
		{
			var m63 = item as Models.Domains.Monitor63;
			if( m63 != null ) return Monitor63DataTemplate;

			var m61 = item as Models.Domains.Monitor61;
			if( m61 != null ) return Monitor61DataTemplate;

			var m60 = item as Models.Domains.Monitor60;
			if( m60 != null ) return Monitor60DataTemplate;

			var m = item as Models.Domains.Monitor;
			if( m != null ) return MonitorDataTemplate;

			return base.SelectTemplate( item, container );
		}
	}
}
