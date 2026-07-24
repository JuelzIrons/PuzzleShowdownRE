namespace UnityEngine.Timeline
{
	public interface IPropertyCollector
	{
		void PushActiveGameObject(global::UnityEngine.GameObject gameObject);

		void PopActiveGameObject();

		void AddFromClip(global::UnityEngine.AnimationClip clip);

		void AddFromClips(global::System.Collections.Generic.IEnumerable<global::UnityEngine.AnimationClip> clips);

		void AddFromName<T>(string name) where T : global::UnityEngine.Component;

		void AddFromName(string name);

		void AddFromClip(global::UnityEngine.GameObject obj, global::UnityEngine.AnimationClip clip);

		void AddFromClips(global::UnityEngine.GameObject obj, global::System.Collections.Generic.IEnumerable<global::UnityEngine.AnimationClip> clips);

		void AddFromName<T>(global::UnityEngine.GameObject obj, string name) where T : global::UnityEngine.Component;

		void AddFromName(global::UnityEngine.GameObject obj, string name);

		void AddFromName(global::UnityEngine.Component component, string name);

		void AddFromComponent(global::UnityEngine.GameObject obj, global::UnityEngine.Component component);

		void AddObjectProperties(global::UnityEngine.Object obj, global::UnityEngine.AnimationClip clip);
	}
}
