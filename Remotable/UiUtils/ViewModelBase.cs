using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Remotable.Annotations;

namespace Remotable.UiUtils
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			// Check if the value and backing field are actualy different
			if (EqualityComparer<T>.Default.Equals(backingField, value))
			{
				return false;
			}

			// Setting the backing field and the RaisePropertyChanged
			backingField = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}