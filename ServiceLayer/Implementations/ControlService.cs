using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using WindowsInput;
using Microsoft.Extensions.Options;
using Model.Entities;
using ServiceLayer.Interfaces;
using Utils;

namespace ServiceLayer
{
	internal class ControlService : IControlService
	{
		private readonly InputSimulator simulator = new InputSimulator();

		private readonly Settings _settings;

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern int SetForegroundWindow(IntPtr hwnd);

		public ControlService(IOptions<Settings> settings)
		{
			this._settings = settings.Value;
		}

		public void SendGlobalKey(KeyPressCommand command)
		{
			if (_settings.DevMode)
			{
				Console.WriteLine($"Pressed {command}");
				return;
			}

			if (command.KeyCode == default)
			{
				return;
			}

			App app = this.FindApp(command.TargetAppId);
			if (app != null)
			{
				this.Focus(app);
			}

			simulator.Keyboard.KeyPress(command.KeyCode);
		}

		public IEnumerable<App> GetApps()
		{
			return Process.GetProcesses().Where(proc => proc.MainWindowHandle != IntPtr.Zero
																												&& proc.MainWindowTitle != string.Empty)
																	 .Select(proc => new App(proc));
		}

		public App FindApp(int pid)
		{
			return new App(Process.GetProcesses().FirstOrDefault(process => process.Id == pid));
		}

		public void GetApps(double volume)
		{
		}

		private bool Focus(App app)
		{
			return SetForegroundWindow(app.Process.MainWindowHandle) != 0;
		}
	}
}