using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using WindowsInput;
using Microsoft.Extensions.Options;
using Model.Entities;
using ServiceLayer.Interfaces;
using Model;

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

			UsableApp app = this.FindApp(command.TargetAppId);
			if (app != null)
			{
				this.Focus(app);
			}

			simulator.Keyboard.KeyPress(command.KeyCode);
		}

		public IEnumerable<UsableApp> GetApps()
		{
			return Process.GetProcesses().Where(proc => proc.MainWindowHandle != IntPtr.Zero
																												&& proc.MainWindowTitle != string.Empty)
																	 .Select(proc => new UsableApp(proc));
		}

		public UsableApp FindApp(int pid)
		{
			return new UsableApp(Process.GetProcesses().FirstOrDefault(process => process.Id == pid));
		}

		public void GetApps(double volume)
		{
		}

		private bool Focus(UsableApp app)
		{
			return SetForegroundWindow(app.Process.MainWindowHandle) != 0;
		}
	}
}