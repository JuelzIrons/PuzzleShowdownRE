namespace Unity.Multiplayer.Center.Common
{
	public interface ISectionDependingOnUserChoices : global::Unity.Multiplayer.Center.Common.IOnboardingSection
	{
		void HandleAnswerData(global::Unity.Multiplayer.Center.Common.AnswerData answerData)
		{
		}

		void HandleUserSelectionData(global::Unity.Multiplayer.Center.Common.SelectedSolutionsData selectedSolutionsData)
		{
		}

		void HandlePreset(global::Unity.Multiplayer.Center.Common.Preset preset)
		{
		}
	}
}
