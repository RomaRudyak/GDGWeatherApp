using System;

using Xamarin.Forms;

namespace GDGWeatherApp.Pages
{
	public class WeatherPage : ContentPage
	{
		public WeatherPage ()
		{

			var input = new Entry () {
				VerticalOptions = LayoutOptions.Start,
				Placeholder = "Search"
			};

			var button = new Button () {
				Text = "Refresh"
			};

			var template = new DataTemplate (() => new TextCell(){
				
			});

			var list = new ListView {
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Fill,
				ItemTemplate = template
			};

			template.SetBinding (TextCell.TextProperty, new Binding ("."));

			input.SetBinding (Entry.TextProperty, new Binding("Search"));
			list.SetBinding (ListView.ItemsSourceProperty, new Binding ("Source"));
			button.SetBinding (Button.CommandProperty, new Binding ("RefreshCommand"));

			Content = new StackLayout {
				Padding = Device.OnPlatform(20, 0, 0),
				Children = {
					input,
					button,
					list
				}
			};
		}
	}

}

