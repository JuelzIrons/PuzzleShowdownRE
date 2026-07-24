namespace DG.Tweening.Core
{
	public class DOTweenSettings : global::UnityEngine.ScriptableObject
	{
		public enum SettingsLocation
		{
			AssetsDirectory = 0,
			DOTweenDirectory = 1,
			DemigiantDirectory = 2
		}

		[global::System.Serializable]
		public class SafeModeOptions
		{
			public global::DG.Tweening.Core.Enums.SafeModeLogBehaviour logBehaviour = global::DG.Tweening.Core.Enums.SafeModeLogBehaviour.Warning;

			public global::DG.Tweening.Core.Enums.NestedTweenFailureBehaviour nestedTweenFailureBehaviour;
		}

		[global::System.Serializable]
		public class ModulesSetup
		{
			public bool showPanel;

			public bool audioEnabled = true;

			public bool physicsEnabled = true;

			public bool physics2DEnabled = true;

			public bool spriteEnabled = true;

			public bool uiEnabled = true;

			public bool textMeshProEnabled;

			public bool tk2DEnabled;

			public bool deAudioEnabled;

			public bool deUnityExtendedEnabled;

			public bool epoOutlineEnabled;
		}

		public const string AssetName = "DOTweenSettings";

		public const string AssetFullFilename = "DOTweenSettings.asset";

		public bool useSafeMode = true;

		public global::DG.Tweening.Core.DOTweenSettings.SafeModeOptions safeModeOptions = new global::DG.Tweening.Core.DOTweenSettings.SafeModeOptions();

		public float timeScale = 1f;

		public float unscaledTimeScale = 1f;

		public bool useSmoothDeltaTime;

		public float maxSmoothUnscaledTime = 0.15f;

		public global::DG.Tweening.Core.Enums.RewindCallbackMode rewindCallbackMode;

		public bool showUnityEditorReport;

		public global::DG.Tweening.LogBehaviour logBehaviour;

		public bool drawGizmos = true;

		public bool defaultRecyclable;

		public global::DG.Tweening.AutoPlay defaultAutoPlay = global::DG.Tweening.AutoPlay.All;

		public global::DG.Tweening.UpdateType defaultUpdateType;

		public bool defaultTimeScaleIndependent;

		public global::DG.Tweening.Ease defaultEaseType = global::DG.Tweening.Ease.OutQuad;

		public float defaultEaseOvershootOrAmplitude = 1.70158f;

		public float defaultEasePeriod;

		public bool defaultAutoKill = true;

		public global::DG.Tweening.LoopType defaultLoopType;

		public bool debugMode;

		public bool debugStoreTargetId = true;

		public bool showPreviewPanel = true;

		public global::DG.Tweening.Core.DOTweenSettings.SettingsLocation storeSettingsLocation;

		public global::DG.Tweening.Core.DOTweenSettings.ModulesSetup modules = new global::DG.Tweening.Core.DOTweenSettings.ModulesSetup();

		public bool createASMDEF;

		public bool showPlayingTweens;

		public bool showPausedTweens;
	}
}
