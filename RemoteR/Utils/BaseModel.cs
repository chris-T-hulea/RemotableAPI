using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteR.Utils
{
	[INotifyPropertyChanged]
	public partial class BaseModel
	{
		[ObservableProperty]
		private string title;
	}
}
