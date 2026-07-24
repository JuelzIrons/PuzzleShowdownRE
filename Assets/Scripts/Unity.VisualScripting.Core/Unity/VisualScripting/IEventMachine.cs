namespace Unity.VisualScripting
{
	public interface IEventMachine : global::Unity.VisualScripting.IMachine, global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.IGraphNester, global::Unity.VisualScripting.IAotStubbable
	{
		void TriggerAnimationEvent(global::UnityEngine.AnimationEvent animationEvent);

		void TriggerUnityEvent(string name);
	}
}
