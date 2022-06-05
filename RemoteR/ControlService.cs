using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Model;
using Model.DTO;
using RemoteR.Utils;

namespace RemoteR
{
	public class ControlService
	{
		private const string Endpoint = "Command";
		private Configuration configuration;
		private HttpClient client;

		public ControlService(Configuration configuration)
		{
			this.configuration = configuration;
			this.client = new HttpClient();
		}

		public async Task PressKeyAsync(KeyCode key, int? appId)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			var dto = new KeyPressDto
			{
				KeyCode = key,
				TargetAppId = appId ?? default
			};
			var payload = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
			var response = await this.client.PostAsync($"{this.configuration.ServerIp}/{Endpoint}/KeyPress/", payload);
			stopwatch.Stop();

			await Shell.Current.DisplayAlert("Time!", $"{stopwatch.Elapsed.TotalSeconds}s", "OK");
		}

		public async Task<IEnumerable<UsableAppDto>> GetAppsAsync()
		{
			try
			{
				var response = await this.client.GetAsync($"{this.configuration.ServerIp}/{Endpoint}/");
				return await JsonSerializer.DeserializeAsync<IEnumerable<UsableAppDto>>(response.Content.ReadAsStream());

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
				return new List<UsableAppDto>();
			}
		}

		public async Task<double> GetVolumeAsync(int appId)
		{
			try
			{
				var response = await this.client.GetAsync($"{this.configuration.ServerIp}/{Endpoint}/Volume/{appId}/");
				return await JsonSerializer.DeserializeAsync<double>(response.Content.ReadAsStream());

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
				return default;
			}
		}

		public async Task SetVolumeAsync(int appId, double value)
		{
			try
			{
				var payload = new StringContent("", Encoding.UTF8, "application/json");
				var response = await this.client.PostAsync($"{this.configuration.ServerIp}/{Endpoint}/Volume/{appId}?volume={Math.Round(value,2)}",payload);

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
				return;
			}
		}
	}
}
