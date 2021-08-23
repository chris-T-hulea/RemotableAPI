using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web.WebSockets;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using QRCoder;
using ReactiveUI;
using Remotable.UiUtils;
using ServerLayer;
using ServiceLayer;
using Utils;

namespace Remotable
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly IServer _server;
		private readonly LinkageService _linkageService;
		private readonly Constants _constants;
		private string _title;
		private BitmapImage _image;

		public MainWindowViewModel(IServer server, LinkageService linkageService, Constants constants)
		{
			_server = server;
			_linkageService = linkageService;
			_constants = constants;
			this.Button = ReactiveCommand.Create(TimerOnTick);

			string line = _constants.Url + "_" + _linkageService.GetGuid();

			PrintQrCode(line);
		}

		private void PrintQrCode(string line)
		{
			QRCodeGenerator gen = new QRCodeGenerator();
			QRCodeData data = gen.CreateQrCode(line, QRCodeGenerator.ECCLevel.Q);

			QRCode cod = new QRCode(data);
			Image = BitmapToImageSource(cod.GetGraphic(20));
		}

		private BitmapImage BitmapToImageSource(Bitmap bitmap)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
				memory.Position = 0;
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.StreamSource = memory;
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.EndInit();

				return bitmapimage;
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			Title = DateTime.Now.ToString("HH:mm:ss");
		}

		private void TimerOnTick()
		{
		}

		public BitmapImage Image
		{
			get => this._image;
			set => this.SetProperty(ref this._image, value);
		}

		public string Title
		{
			get => this._title;
			set => this.SetProperty(ref this._title, value);
		}

		public ICommand Button { get; set; }
	}
}