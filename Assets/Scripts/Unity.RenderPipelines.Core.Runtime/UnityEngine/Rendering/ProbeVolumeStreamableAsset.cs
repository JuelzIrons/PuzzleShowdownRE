namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::UnityEngine.Scripting.APIUpdating.MovedFrom(false, "UnityEngine.Rendering", "Unity.RenderPipelines.Core.Runtime", "ProbeVolumeBakingSet.StreamableAsset")]
	internal class ProbeVolumeStreamableAsset
	{
		[global::System.Serializable]
		[global::UnityEngine.Scripting.APIUpdating.MovedFrom(false, "UnityEngine.Rendering", "Unity.RenderPipelines.Core.Runtime", "ProbeVolumeBakingSet.StreamableAsset.StreamableCellDesc")]
		public struct StreamableCellDesc
		{
			public int offset;

			public int elementCount;
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("assetGUID")]
		private string m_AssetGUID = "";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("streamableAssetPath")]
		private string m_StreamableAssetPath = "";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("elementSize")]
		private int m_ElementSize;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("streamableCellDescs")]
		private global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.ProbeVolumeStreamableAsset.StreamableCellDesc> m_StreamableCellDescs = new global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.ProbeVolumeStreamableAsset.StreamableCellDesc>();

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.TextAsset m_Asset;

		private string m_FinalAssetPath;

		private global::Unity.IO.LowLevel.Unsafe.FileHandle m_AssetFileHandle;

		public string assetGUID => m_AssetGUID;

		public global::UnityEngine.TextAsset asset => m_Asset;

		public int elementSize => m_ElementSize;

		public global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.ProbeVolumeStreamableAsset.StreamableCellDesc> streamableCellDescs => m_StreamableCellDescs;

		public ProbeVolumeStreamableAsset(string apvStreamingAssetsPath, global::UnityEngine.Rendering.SerializedDictionary<int, global::UnityEngine.Rendering.ProbeVolumeStreamableAsset.StreamableCellDesc> cellDescs, int elementSize, string bakingSetGUID, string assetGUID)
		{
			m_AssetGUID = assetGUID;
			m_StreamableCellDescs = cellDescs;
			m_ElementSize = elementSize;
			m_StreamableAssetPath = global::System.IO.Path.Combine(global::System.IO.Path.Combine(apvStreamingAssetsPath, bakingSetGUID), m_AssetGUID + ".bytes");
		}

		internal void RefreshAssetPath()
		{
			m_FinalAssetPath = global::System.IO.Path.Combine(global::UnityEngine.Application.streamingAssetsPath, m_StreamableAssetPath);
		}

		public string GetAssetPath()
		{
			if (string.IsNullOrEmpty(m_FinalAssetPath))
			{
				RefreshAssetPath();
			}
			return m_FinalAssetPath;
		}

		internal bool HasValidAssetReference()
		{
			if (m_Asset != null)
			{
				return m_Asset.bytes != null;
			}
			return false;
		}

		public unsafe bool FileExists()
		{
			if (m_Asset != null)
			{
				return true;
			}
			global::Unity.IO.LowLevel.Unsafe.FileInfoResult fileInfoResult = default(global::Unity.IO.LowLevel.Unsafe.FileInfoResult);
			global::Unity.IO.LowLevel.Unsafe.AsyncReadManager.GetFileInfo(GetAssetPath(), &fileInfoResult).JobHandle.Complete();
			return fileInfoResult.FileState == global::Unity.IO.LowLevel.Unsafe.FileState.Exists;
		}

		public long GetFileSize()
		{
			return new global::System.IO.FileInfo(GetAssetPath()).Length;
		}

		public bool IsOpen()
		{
			return m_AssetFileHandle.IsValid();
		}

		public global::Unity.IO.LowLevel.Unsafe.FileHandle OpenFile()
		{
			if (m_AssetFileHandle.IsValid())
			{
				return m_AssetFileHandle;
			}
			m_AssetFileHandle = global::Unity.IO.LowLevel.Unsafe.AsyncReadManager.OpenFileAsync(GetAssetPath());
			return m_AssetFileHandle;
		}

		public void CloseFile()
		{
			if (m_AssetFileHandle.IsValid() && m_AssetFileHandle.JobHandle.IsCompleted)
			{
				m_AssetFileHandle.Close();
			}
			m_AssetFileHandle = default(global::Unity.IO.LowLevel.Unsafe.FileHandle);
		}

		public bool IsValid()
		{
			return !string.IsNullOrEmpty(m_AssetGUID);
		}

		public void Dispose()
		{
			if (m_AssetFileHandle.IsValid())
			{
				m_AssetFileHandle.Close().Complete();
				m_AssetFileHandle = default(global::Unity.IO.LowLevel.Unsafe.FileHandle);
			}
		}
	}
}
