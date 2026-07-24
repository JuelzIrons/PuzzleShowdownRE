namespace Unity.Networking.Transport
{
	public struct OperationResult : global::System.IDisposable
	{
		private global::Unity.Collections.FixedString64Bytes m_Label;

		private global::Unity.Collections.NativeReference<int> m_ErrorCode;

		public int ErrorCode
		{
			get
			{
				return m_ErrorCode.Value;
			}
			set
			{
				if (value != 0)
				{
					global::UnityEngine.Debug.LogError($"Error on {m_Label}, errorCode = {value}");
				}
				m_ErrorCode.Value = value;
			}
		}

		internal OperationResult(global::Unity.Collections.FixedString64Bytes label, global::Unity.Collections.Allocator allocator)
		{
			m_Label = label;
			m_ErrorCode = new global::Unity.Collections.NativeReference<int>(allocator);
		}

		public void Dispose()
		{
			m_ErrorCode.Dispose();
		}
	}
}
