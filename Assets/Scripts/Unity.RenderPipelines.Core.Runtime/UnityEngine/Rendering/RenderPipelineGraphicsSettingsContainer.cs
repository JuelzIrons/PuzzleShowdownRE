namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class RenderPipelineGraphicsSettingsContainer : global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.Rendering.RenderPipelineGraphicsSettingsCollection m_RuntimeSettings = new global::UnityEngine.Rendering.RenderPipelineGraphicsSettingsCollection();

		public global::System.Collections.Generic.List<global::UnityEngine.Rendering.IRenderPipelineGraphicsSettings> settingsList => m_RuntimeSettings.settingsList;

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}
	}
}
