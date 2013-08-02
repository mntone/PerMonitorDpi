using Livet;
using Mntone.MonitorInformation.Models;
using Mntone.MonitorInformation.ViewModels.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mntone.MonitorInformation.ViewModels
{
	public sealed class MainWindowViewModel: ViewModel
	{
		private readonly Retriever _retriver;
		public ObservableCollection<MonitorViewModel> Monitors { get { return _Monitors; } }
		private readonly ObservableCollection<MonitorViewModel> _Monitors;

		public MainWindowViewModel()
		{
			_retriver = new Retriever();
			_Monitors = new ObservableCollection<MonitorViewModel>();
		}

		public void Initilize()
		{
			_retriver.Retrieve();
			Update();
		}

		public void Update()
		{
			Monitors.Clear();
			foreach( var m in _retriver.Monitors )
			{
				var mvm = new MonitorViewModel( m );
				Monitors.Add( mvm );
			}
			Calculate();
		}

		private const double HEIGHT = 342;
		private const double WIDTH = 608;
		private void Calculate()
		{
			var ca = new Rect();
			foreach( var m in Monitors )
			{
				ca = RectHelper.Cover( ca, m.Domain.Area );
			}
			var ph = HEIGHT / ca.Height;
			var pw = WIDTH / ca.Width;
			ScaleFactor = ph > pw ? pw : ph;
			OffsetY = -ca.Y * ScaleFactor;
			OffsetX = -ca.X * ScaleFactor;
		}

		public double OffsetY
		{
			get { return _OffsetY; }
			set
			{
				if( _OffsetY == value )
					return;

				_OffsetY = value;
				RaisePropertyChanged( "OffsetY" );
			}
		}
		private double _OffsetY = 0.0;

		public double OffsetX
		{
			get { return _OffsetX; }
			set
			{
				if( _OffsetX == value )
					return;

				_OffsetX = value;
				RaisePropertyChanged( "OffsetX" );
			}
		}
		private double _OffsetX = 0.0;

		public double ScaleFactor
		{
			get { return _ScaleFactor; }
			set
			{
				if( _ScaleFactor == value )
					return;

				_ScaleFactor = value;
				RaisePropertyChanged( "ScaleFactor" );
			}
		}
		private double _ScaleFactor = 1.0;
	}
}
