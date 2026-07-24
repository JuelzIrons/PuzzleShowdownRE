namespace UnityEngine.UI
{
	[global::UnityEngine.ExecuteAlways]
	public abstract class BaseMeshEffect : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.UI.IMeshModifier
	{
		[global::System.NonSerialized]
		private global::UnityEngine.UI.Graphic m_Graphic;

		protected global::UnityEngine.UI.Graphic graphic
		{
			get
			{
				if (m_Graphic == null)
				{
					m_Graphic = GetComponent<global::UnityEngine.UI.Graphic>();
				}
				return m_Graphic;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (graphic != null)
			{
				graphic.SetVerticesDirty();
			}
		}

		protected override void OnDisable()
		{
			if (graphic != null)
			{
				graphic.SetVerticesDirty();
			}
			base.OnDisable();
		}

		protected override void OnDidApplyAnimationProperties()
		{
			if (graphic != null)
			{
				graphic.SetVerticesDirty();
			}
			base.OnDidApplyAnimationProperties();
		}

		public virtual void ModifyMesh(global::UnityEngine.Mesh mesh)
		{
			using global::UnityEngine.UI.VertexHelper vertexHelper = new global::UnityEngine.UI.VertexHelper(mesh);
			ModifyMesh(vertexHelper);
			vertexHelper.FillMesh(mesh);
		}

		public abstract void ModifyMesh(global::UnityEngine.UI.VertexHelper vh);
	}
}
