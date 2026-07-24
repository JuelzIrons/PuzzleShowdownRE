internal class VariableExamples
{
	public class PlayerController : global::UnityEngine.MonoBehaviour
	{
		private global::Unity.VisualScripting.VariableDeclaration m_Velocity;

		private void Start()
		{
			global::Unity.VisualScripting.Variables component = GetComponent<global::Unity.VisualScripting.Variables>();
			m_Velocity = component.declarations.GetDeclaration("velocity");
		}

		private void Update()
		{
			if (global::UnityEngine.Input.GetKeyDown("space"))
			{
				float num = (float)m_Velocity.value;
				m_Velocity.value = num * 2f;
			}
		}
	}
}
