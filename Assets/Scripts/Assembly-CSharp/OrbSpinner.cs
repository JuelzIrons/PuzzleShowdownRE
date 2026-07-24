public class OrbSpinner : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.MeshRenderer m_mr;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Material m_matTemplate;

	[global::UnityEngine.SerializeField]
	private float m_spinSpeed = 0.5f;

	private void Start()
	{
		global::UnityEngine.Material material = new global::UnityEngine.Material(m_matTemplate);
		global::System.Collections.Generic.List<global::UnityEngine.Material> materials = new global::System.Collections.Generic.List<global::UnityEngine.Material> { material };
		m_mr.SetMaterials(materials);
		global::DG.Tweening.ShortcutExtensions.DOFloat(material, 1f, "TRANS", 2f);
	}

	private void Update()
	{
		base.transform.Rotate(global::UnityEngine.Vector3.up * m_spinSpeed * global::UnityEngine.Time.deltaTime, global::UnityEngine.Space.Self);
	}
}
