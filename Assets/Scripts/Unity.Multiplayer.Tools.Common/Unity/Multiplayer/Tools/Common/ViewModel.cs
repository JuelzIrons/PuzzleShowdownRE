namespace Unity.Multiplayer.Tools.Common
{
	internal class ViewModel<TViewModel> : global::Unity.Multiplayer.Tools.Common.IViewModel<TViewModel> where TViewModel : global::Unity.Multiplayer.Tools.Common.ViewModel<TViewModel>
	{
		public event global::Unity.Multiplayer.Tools.Common.IViewModel<TViewModel>.ViewModelChangedEventHandler ViewModelChanged;

		public event global::Unity.Multiplayer.Tools.Common.IViewModel<TViewModel>.ViewModelChangedPropertyEventHandler PropertyChanged;

		protected bool SetField<TProperty>(ref TProperty field, TProperty value, [global::System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
		{
			if (global::System.Collections.Generic.EqualityComparer<TProperty>.Default.Equals(field, value))
			{
				return false;
			}
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		protected virtual void OnViewModelChanged()
		{
			this.ViewModelChanged?.Invoke((TViewModel)this);
		}

		[global::JetBrains.Annotations.NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([global::System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
		{
			OnViewModelChanged();
			this.PropertyChanged?.Invoke((TViewModel)this, new global::System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
	}
}
