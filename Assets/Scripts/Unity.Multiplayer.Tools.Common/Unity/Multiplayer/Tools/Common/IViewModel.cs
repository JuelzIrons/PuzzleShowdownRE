namespace Unity.Multiplayer.Tools.Common
{
	internal interface IViewModel<TViewModel>
	{
		public delegate void ViewModelChangedEventHandler(TViewModel viewModel);

		public delegate void ViewModelChangedPropertyEventHandler(TViewModel viewModel, global::System.ComponentModel.PropertyChangedEventArgs eventArgs);

		event global::Unity.Multiplayer.Tools.Common.IViewModel<TViewModel>.ViewModelChangedEventHandler ViewModelChanged;

		event global::Unity.Multiplayer.Tools.Common.IViewModel<TViewModel>.ViewModelChangedPropertyEventHandler PropertyChanged;
	}
}
