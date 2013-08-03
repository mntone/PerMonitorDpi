using Livet;
using Mntone.MonitorInformation.Models.Domains;
using System.Collections.Generic;
using System.Windows.Media;

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

		public SolidColorBrush PanelBorderColor
		{
			get { return _PanelBorderColor ?? ( _PanelBorderColor = new SolidColorBrush( Colors[( int )Domain.No] ) ); }
		}
		private SolidColorBrush _PanelBorderColor;

		public SolidColorBrush PanelBackgroundColor
		{
			get
			{
				if( _PanelBackgroundColor == null )
				{
					var c = Colors[( int )Domain.No];
					c.A = 0x66;
					_PanelBackgroundColor = new SolidColorBrush( c );
				}
				return _PanelBackgroundColor;
			}
		}
		private SolidColorBrush _PanelBackgroundColor;


		#region Color

		private static readonly List<Color> Colors = new List<Color>()
		{
			Color.FromRgb( 255, 0, 0 ),
			Color.FromRgb( 0, 0, 255 ),
			Color.FromRgb( 0, 255, 0 ),
			Color.FromRgb( 255, 0, 255 ),
			Color.FromRgb( 226, 235, 0 ),
			Color.FromRgb( 255, 166, 0 ),
			Color.FromRgb( 0, 255, 252 ),
			Color.FromRgb( 221, 69, 142 ),
			Color.FromRgb( 173, 115, 49 ),
			Color.FromRgb( 151, 173, 49 ),
			Color.FromRgb( 81, 130, 157 ),
			Color.FromRgb( 112, 56, 127 ),
			Color.FromRgb( 116, 0, 0 ),
			Color.FromRgb( 116, 92, 0 ),
			Color.FromRgb( 65, 148, 17 ),
			Color.FromRgb( 55, 190, 140 ),
			Color.FromRgb( 108, 91, 189 ),
			Color.FromRgb( 243, 0, 170 ),
			Color.FromRgb( 88, 109, 65 ),
			Color.FromRgb( 59, 78, 49 ),
		};

		#endregion
	}
}
