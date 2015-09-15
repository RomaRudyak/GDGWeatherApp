using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using GDGWeatherApp.Pages;

using Xamarin.Forms;

namespace GDGWeatherApp.ViewModel
{
	public class WeatherViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<String> Source {
			get{ return _source;}
			set{ 
				_source = value;
				OnPropertyChanged ();
			}
		}

		public String Search {
			get {
				return search;
			}
			set {
				search = value;
				OnPropertyChanged ();
			}
		}

		public ICommand RefreshCommand {
			get{
				return _refreshCommand;
			}
		}


		public WeatherViewModel ()
		{
			Source = new ObservableCollection<string> ();
			_refreshCommand = new Command (RefreshWeather);
		}

		private async void RefreshWeather(){
			var list = await new WeaherProvider ().GetWeather (Search);
			Source.Clear ();
			foreach (var l in list) {
				Source.Add (l);
			}
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName]String propName = ""){
			var handlers = PropertyChanged;
			if (handlers != null) {
				handlers(this, new PropertyChangedEventArgs(propName));
			}
		}
		#endregion

		private ObservableCollection<String> _source;
		private String search;
		private ICommand _refreshCommand;
	}

}

