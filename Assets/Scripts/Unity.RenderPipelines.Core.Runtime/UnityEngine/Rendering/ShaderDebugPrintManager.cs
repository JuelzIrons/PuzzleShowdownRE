namespace UnityEngine.Rendering
{
	public sealed class ShaderDebugPrintManager
	{
		private static class Profiling
		{
			public static readonly global::UnityEngine.Rendering.ProfilingSampler BufferReadComplete = new global::UnityEngine.Rendering.ProfilingSampler("ShaderDebugPrintManager.BufferReadComplete");
		}

		private enum DebugValueType
		{
			TypeUint = 1,
			TypeInt = 2,
			TypeFloat = 3,
			TypeUint2 = 4,
			TypeInt2 = 5,
			TypeFloat2 = 6,
			TypeUint3 = 7,
			TypeInt3 = 8,
			TypeFloat3 = 9,
			TypeUint4 = 10,
			TypeInt4 = 11,
			TypeFloat4 = 12,
			TypeBool = 13
		}

		private static readonly global::UnityEngine.Rendering.ShaderDebugPrintManager s_Instance = new global::UnityEngine.Rendering.ShaderDebugPrintManager();

		private const int k_FramesInFlight = 4;

		private const int k_MaxBufferElements = 16384;

		private global::System.Collections.Generic.List<global::UnityEngine.GraphicsBuffer> m_OutputBuffers = new global::System.Collections.Generic.List<global::UnityEngine.GraphicsBuffer>();

		private global::System.Collections.Generic.List<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> m_ReadbackRequests = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.AsyncGPUReadbackRequest>();

		private global::System.Action<global::UnityEngine.Rendering.AsyncGPUReadbackRequest> m_BufferReadCompleteAction;

		private int m_FrameCounter;

		private bool m_FrameCleared;

		private string m_OutputLine = "";

		private global::System.Action<string> m_OutputAction;

		private static readonly int m_ShaderPropertyIDInputMouse = global::UnityEngine.Shader.PropertyToID("_ShaderDebugPrintInputMouse");

		private static readonly int m_ShaderPropertyIDInputFrame = global::UnityEngine.Shader.PropertyToID("_ShaderDebugPrintInputFrame");

		private static readonly int m_shaderDebugOutputData = global::UnityEngine.Shader.PropertyToID("shaderDebugOutputData");

		private const uint k_TypeHasTag = 128u;

		public static global::UnityEngine.Rendering.ShaderDebugPrintManager instance => s_Instance;

		public string outputLine => m_OutputLine;

		public global::System.Action<string> outputAction
		{
			set
			{
				m_OutputAction = value;
			}
		}

		private int DebugValueTypeToElemSize(global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType type)
		{
			switch (type)
			{
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeBool:
				return 1;
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint2:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt2:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat2:
				return 2;
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint3:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt3:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat3:
				return 3;
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint4:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt4:
			case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat4:
				return 4;
			default:
				return 0;
			}
		}

		private ShaderDebugPrintManager()
		{
			for (int i = 0; i < 4; i++)
			{
				m_OutputBuffers.Add(new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, 16384, 4));
				m_ReadbackRequests.Add(default(global::UnityEngine.Rendering.AsyncGPUReadbackRequest));
			}
			m_BufferReadCompleteAction = BufferReadComplete;
			m_OutputAction = DefaultOutput;
		}

		public void SetShaderDebugPrintInputConstants(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ShaderDebugPrintInput input)
		{
			global::UnityEngine.Vector4 value = new global::UnityEngine.Vector4(input.pos.x, input.pos.y, input.leftDown ? 1 : 0, input.rightDown ? 1 : 0);
			cmd.SetGlobalVector(m_ShaderPropertyIDInputMouse, value);
			cmd.SetGlobalInt(m_ShaderPropertyIDInputFrame, m_FrameCounter);
		}

		public void SetShaderDebugPrintBindings(global::UnityEngine.Rendering.CommandBuffer cmd)
		{
			int index = m_FrameCounter % 4;
			if (!m_ReadbackRequests[index].done)
			{
				m_ReadbackRequests[index].WaitForCompletion();
			}
			cmd.SetGlobalBuffer(m_shaderDebugOutputData, m_OutputBuffers[index]);
			ClearShaderDebugPrintBuffer();
		}

		private void ClearShaderDebugPrintBuffer()
		{
			if (!m_FrameCleared)
			{
				int index = m_FrameCounter % 4;
				global::Unity.Collections.NativeArray<uint> data = new global::Unity.Collections.NativeArray<uint>(1, global::Unity.Collections.Allocator.Temp);
				data[0] = 0u;
				m_OutputBuffers[index].SetData(data, 0, 0, 1);
				m_FrameCleared = true;
			}
		}

		private unsafe void BufferReadComplete(global::UnityEngine.Rendering.AsyncGPUReadbackRequest request)
		{
			using (new global::UnityEngine.Rendering.ProfilingScope(global::UnityEngine.Rendering.ShaderDebugPrintManager.Profiling.BufferReadComplete))
			{
				if (!request.hasError)
				{
					global::Unity.Collections.NativeArray<uint> data = request.GetData<uint>();
					uint num = data[0];
					if (num >= 16384)
					{
						num = 16384u;
						global::UnityEngine.Debug.LogWarning("Debug Shader Print Buffer Full!");
					}
					string text = "";
					if (num != 0)
					{
						text = text + "Frame #" + m_FrameCounter + ": ";
					}
					uint* unsafePtr = (uint*)global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(data);
					int num2 = 1;
					while (num2 < num)
					{
						global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType debugValueType = (global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType)(data[num2] & 0xF);
						if ((data[num2] & 0x80) == 128 && num2 + 1 < num)
						{
							uint num3 = data[num2 + 1];
							num2++;
							for (int i = 0; i < 4; i++)
							{
								char c = (char)(num3 & 0xFF);
								if (c != 0)
								{
									text += c;
									num3 >>= 8;
								}
							}
							text += " ";
						}
						int num4 = DebugValueTypeToElemSize(debugValueType);
						if (num2 + num4 > num)
						{
							break;
						}
						num2++;
						switch (debugValueType)
						{
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint:
							text += $"{data[num2]}u";
							break;
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt:
						{
							int num5 = (int)unsafePtr[num2];
							text += num5;
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat:
						{
							float num6 = *(float*)(unsafePtr + num2);
							text += $"{num6}f";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint2:
						{
							uint* ptr9 = unsafePtr + num2;
							text += $"uint2({*ptr9}, {ptr9[1]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt2:
						{
							int* ptr8 = (int*)(unsafePtr + num2);
							text += $"int2({*ptr8}, {ptr8[1]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat2:
						{
							float* ptr7 = (float*)(unsafePtr + num2);
							text += $"float2({*ptr7}, {ptr7[1]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint3:
						{
							uint* ptr6 = unsafePtr + num2;
							text += $"uint3({*ptr6}, {ptr6[1]}, {ptr6[2]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt3:
						{
							int* ptr5 = (int*)(unsafePtr + num2);
							text += $"int3({*ptr5}, {ptr5[1]}, {ptr5[2]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat3:
						{
							float* ptr4 = (float*)(unsafePtr + num2);
							text += $"float3({*ptr4}, {ptr4[1]}, {ptr4[2]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeUint4:
						{
							uint* ptr3 = unsafePtr + num2;
							text += $"uint4({*ptr3}, {ptr3[1]}, {ptr3[2]}, {ptr3[3]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeInt4:
						{
							int* ptr2 = (int*)(unsafePtr + num2);
							text += $"int4({*ptr2}, {ptr2[1]}, {ptr2[2]}, {ptr2[3]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeFloat4:
						{
							float* ptr = (float*)(unsafePtr + num2);
							text += $"float4({*ptr}, {ptr[1]}, {ptr[2]}, {ptr[3]})";
							break;
						}
						case global::UnityEngine.Rendering.ShaderDebugPrintManager.DebugValueType.TypeBool:
							text += ((data[num2] == 0) ? "False" : "True");
							break;
						default:
							num2 = (int)num;
							break;
						}
						num2 += num4;
						text += " ";
					}
					if (num != 0)
					{
						m_OutputLine = text;
						m_OutputAction(text);
					}
				}
				else
				{
					m_OutputLine = "Error at read back!";
					m_OutputAction("Error at read back!");
				}
			}
		}

		public void EndFrame()
		{
			int index = m_FrameCounter % 4;
			m_ReadbackRequests[index] = global::UnityEngine.Rendering.AsyncGPUReadback.Request(m_OutputBuffers[index], m_BufferReadCompleteAction);
			m_FrameCounter++;
			m_FrameCleared = false;
		}

		public void PrintImmediate()
		{
			int index = m_FrameCounter % 4;
			global::UnityEngine.Rendering.AsyncGPUReadbackRequest obj = global::UnityEngine.Rendering.AsyncGPUReadback.Request(m_OutputBuffers[index]);
			obj.WaitForCompletion();
			m_BufferReadCompleteAction(obj);
			m_FrameCounter++;
			m_FrameCleared = false;
		}

		public void DefaultOutput(string line)
		{
			global::UnityEngine.Debug.Log(line);
		}
	}
}
