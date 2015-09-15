using System;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GDGWeatherApp
{
	public class WeaherProvider
	{
		public Task<String[]> GetWeather(String cityName){
			var source = new TaskCompletionSource<String[]> ();

			var httpWebRequest = WebRequest.CreateHttp(String.Format (UrlFormat, cityName));

			Task<WebResponse> webResponseTask = Task<WebResponse>.Factory.FromAsync(httpWebRequest.BeginGetResponse, httpWebRequest.EndGetResponse, httpWebRequest);

			webResponseTask.ContinueWith ((t, result) => {
				var webResponse = t.Result;
				String weatherJSON = null;
				using (Stream stream = webResponse.GetResponseStream ()) {
					using (var reader = new StreamReader (stream, Encoding.UTF8)) {
						weatherJSON = reader.ReadToEnd ();
					}
				}

				var jToken = JObject.Parse (weatherJSON);
				var res = jToken ["list"]
					.Select (jt => String.Format ("day: {0} C night: {1} C", jt["temp"]["day"].Value<String>(), jt["temp"]["night"].Value<String>()))
					.ToArray ();
				source.SetResult (res);
			}, TaskContinuationOptions.OnlyOnRanToCompletion);


			webResponseTask.ContinueWith (t=> source.SetException(t.Exception), TaskContinuationOptions.OnlyOnFaulted);

			return source.Task;
		}

		public WeaherProvider ()
		{
		}

		private const String UrlFormat = "http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&mode=json&units=metric&cnt=7";
	}
}

