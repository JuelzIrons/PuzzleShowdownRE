public class DebugInputReader : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private InputDebuggerTemplateSO m_template;

	[global::UnityEngine.SerializeField]
	private PodManager m_myPodManager;

	public void ReadDebugInput(int frameCount)
	{
		for (int i = 0; i < m_template.Actions.Count; i++)
		{
			if (m_template.Actions[i].startFrame == frameCount)
			{
				switch (m_template.Actions[i].type)
				{
				case InputType.right:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.right, performed: true);
					break;
				case InputType.left:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.left, performed: true);
					break;
				case InputType.up:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.up, performed: true);
					break;
				case InputType.down:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.down, performed: true);
					break;
				case InputType.swap:
					m_myPodManager.CursorController.DebugSwapButton(performed: true);
					break;
				}
			}
			if (m_template.Actions[i].endFrame == frameCount)
			{
				switch (m_template.Actions[i].type)
				{
				case InputType.right:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.right, performed: false);
					break;
				case InputType.left:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.left, performed: false);
					break;
				case InputType.up:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.up, performed: false);
					break;
				case InputType.down:
					m_myPodManager.CursorController.DebugTryMove(global::UnityEngine.Vector2.down, performed: false);
					break;
				case InputType.swap:
					m_myPodManager.CursorController.DebugSwapButton(performed: false);
					break;
				}
			}
		}
	}
}
