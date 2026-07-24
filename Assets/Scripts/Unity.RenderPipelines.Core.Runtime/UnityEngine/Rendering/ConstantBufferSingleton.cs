namespace UnityEngine.Rendering
{
	internal class ConstantBufferSingleton<CBType> : global::UnityEngine.Rendering.ConstantBuffer<CBType> where CBType : struct
	{
		private static global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> s_Instance;

		internal static global::UnityEngine.Rendering.ConstantBufferSingleton<CBType> instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = new global::UnityEngine.Rendering.ConstantBufferSingleton<CBType>();
					global::UnityEngine.Rendering.ConstantBuffer.Register(s_Instance);
				}
				return s_Instance;
			}
			set
			{
				s_Instance = value;
			}
		}

		public override void Release()
		{
			base.Release();
			s_Instance = null;
		}
	}
}
