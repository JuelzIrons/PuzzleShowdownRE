namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class DebugMessageHandler : global::UnityEngine.ScriptableObject
	{
		public enum MessageType : byte
		{
			Activate = 0,
			DebugData = 1,
			AnalyticsData = 2
		}

		public abstract class IPayload
		{
			public int version;

			public bool isCompatible => version == 1;
		}

		public class DebugDataPayload : global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload
		{
			public string graphName;

			public global::UnityEngine.EntityId executionId;

			public global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugData debugData;
		}

		public class AnalyticsPayload : global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload
		{
			public global::UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType;

			public global::UnityEngine.DeviceType deviceType;

			public string deviceModel;

			public string gpuVendor;

			public string gpuName;

			public AnalyticsPayload()
			{
				deviceModel = global::UnityEngine.SystemInfo.deviceModel;
				deviceType = global::UnityEngine.SystemInfo.deviceType;
				graphicsDeviceType = global::UnityEngine.SystemInfo.graphicsDeviceType;
				gpuVendor = global::UnityEngine.SystemInfo.graphicsDeviceVendor;
				gpuName = global::UnityEngine.SystemInfo.graphicsDeviceName;
			}
		}

		internal const int k_Version = 1;

		private static readonly global::System.Guid s_EditorToPlayerGuid = new global::System.Guid("df519969-f421-4397-b2a1-1740abc989a0");

		private static readonly global::System.Guid s_PlayerToEditorGuid = new global::System.Guid("98d0787d-3917-4c48-8393-e313498046e6");

		private global::System.Action<global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType, global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload> m_UserCallback;

		private void InternalCallback(global::UnityEngine.Networking.PlayerConnection.MessageEventArgs msg)
		{
			var (arg, arg2) = DeserializeMessage(msg.data);
			m_UserCallback(arg, arg2);
		}

		public void Register(global::System.Action<global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType, global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload> callback)
		{
			m_UserCallback = callback;
			global::UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.Register(s_EditorToPlayerGuid, InternalCallback);
		}

		public void UnregisterAll()
		{
			global::UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.Unregister(s_EditorToPlayerGuid, InternalCallback);
		}

		public void Send(global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType messageType, global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload payload = null)
		{
			global::UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.Send(s_PlayerToEditorGuid, SerializeMessage(messageType, payload));
		}

		internal static byte[] SerializeMessage(global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType type, global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload payload = null)
		{
			using global::System.IO.MemoryStream memoryStream = new global::System.IO.MemoryStream();
			using global::System.IO.BinaryWriter binaryWriter = new global::System.IO.BinaryWriter(memoryStream);
			binaryWriter.Write((byte)type);
			switch (type)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType.DebugData:
				binaryWriter.Write(1);
				if (!(payload is global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.DebugDataPayload debugDataPayload))
				{
					throw new global::System.InvalidOperationException("No valid payload provided");
				}
				binaryWriter.Write(debugDataPayload.graphName);
				binaryWriter.Write(debugDataPayload.executionId);
				binaryWriter.Write(global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugDataSerialization.ToJson(debugDataPayload.debugData));
				break;
			case global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType.AnalyticsData:
				binaryWriter.Write(1);
				if (!(payload is global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.AnalyticsPayload analyticsPayload))
				{
					throw new global::System.InvalidOperationException("No valid payload provided");
				}
				binaryWriter.Write((int)analyticsPayload.graphicsDeviceType);
				binaryWriter.Write((int)analyticsPayload.deviceType);
				binaryWriter.Write(analyticsPayload.deviceModel);
				binaryWriter.Write(analyticsPayload.gpuVendor);
				binaryWriter.Write(analyticsPayload.gpuName);
				break;
			}
			return memoryStream.ToArray();
		}

		internal static (global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType, global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.IPayload) DeserializeMessage(byte[] data)
		{
			using global::System.IO.MemoryStream input = new global::System.IO.MemoryStream(data);
			using global::System.IO.BinaryReader binaryReader = new global::System.IO.BinaryReader(input);
			global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType messageType = (global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType)binaryReader.ReadByte();
			switch (messageType)
			{
			case global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType.DebugData:
			{
				global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.DebugDataPayload debugDataPayload = new global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.DebugDataPayload();
				debugDataPayload.version = binaryReader.ReadInt32();
				if (!debugDataPayload.isCompatible)
				{
					global::UnityEngine.Debug.LogWarning($"Render Graph Viewer message version mismatch (expected {1}, received {debugDataPayload.version})");
					return (messageType, debugDataPayload);
				}
				debugDataPayload.graphName = binaryReader.ReadString();
				debugDataPayload.executionId = binaryReader.ReadInt32();
				debugDataPayload.debugData = global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugDataSerialization.FromJson(binaryReader.ReadString());
				return (messageType, debugDataPayload);
			}
			case global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.MessageType.AnalyticsData:
			{
				global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.AnalyticsPayload analyticsPayload = new global::UnityEngine.Rendering.RenderGraphModule.DebugMessageHandler.AnalyticsPayload();
				analyticsPayload.version = binaryReader.ReadInt32();
				if (!analyticsPayload.isCompatible)
				{
					return (messageType, analyticsPayload);
				}
				analyticsPayload.graphicsDeviceType = (global::UnityEngine.Rendering.GraphicsDeviceType)binaryReader.ReadInt32();
				analyticsPayload.deviceType = (global::UnityEngine.DeviceType)binaryReader.ReadInt32();
				analyticsPayload.deviceModel = binaryReader.ReadString();
				analyticsPayload.gpuVendor = binaryReader.ReadString();
				analyticsPayload.gpuName = binaryReader.ReadString();
				return (messageType, analyticsPayload);
			}
			default:
				return (messageType, null);
			}
		}
	}
}
