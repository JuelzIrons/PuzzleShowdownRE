namespace Unity.Multiplayer.Center.Common
{
	public interface IOnboardingSection
	{
		global::UnityEngine.UIElements.VisualElement Root { get; }

		void Load();

		void Unload();
	}
}
