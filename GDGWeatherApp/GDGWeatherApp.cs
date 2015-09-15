using System;
using GDGWeatherApp.Pages;
using GDGWeatherApp.ViewModel;

using Xamarin.Forms;

namespace GDGWeatherApp
{
	public class App : Application
	{
		public App ()
		{
			MainPage = new WeatherPage ();
			MainPage.BindingContext = new WeatherViewModel ();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

