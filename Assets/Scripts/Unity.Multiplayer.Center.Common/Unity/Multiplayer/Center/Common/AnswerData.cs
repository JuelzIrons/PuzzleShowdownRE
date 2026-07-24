namespace Unity.Multiplayer.Center.Common
{
	[global::System.Serializable]
	public class AnswerData
	{
		public global::System.Collections.Generic.List<global::Unity.Multiplayer.Center.Common.AnsweredQuestion> Answers = new global::System.Collections.Generic.List<global::Unity.Multiplayer.Center.Common.AnsweredQuestion>();

		public global::Unity.Multiplayer.Center.Common.AnswerData Clone()
		{
			return global::UnityEngine.JsonUtility.FromJson(global::UnityEngine.JsonUtility.ToJson(this), typeof(global::Unity.Multiplayer.Center.Common.AnswerData)) as global::Unity.Multiplayer.Center.Common.AnswerData;
		}
	}
}
