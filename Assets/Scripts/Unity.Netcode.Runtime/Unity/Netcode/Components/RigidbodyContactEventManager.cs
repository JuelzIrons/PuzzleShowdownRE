namespace Unity.Netcode.Components
{
	[global::UnityEngine.AddComponentMenu("Netcode/Rigidbody Contact Event Manager")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/api/Unity.Netcode.Components.RigidbodyContactEventManager.html")]
	public class RigidbodyContactEventManager : global::UnityEngine.MonoBehaviour
	{
		private struct JobResultStruct
		{
			public bool HasCollisionStay;

			public global::UnityEngine.EntityId ThisInstanceID;

			public global::UnityEngine.EntityId OtherInstanceID;

			public global::UnityEngine.Vector3 AverageNormal;

			public global::UnityEngine.Vector3 AverageCollisionStayNormal;

			public global::UnityEngine.Vector3 ContactPoint;
		}

		private struct GetCollisionsJob : global::Unity.Jobs.IJobParallelFor
		{
			[global::Unity.Collections.ReadOnly]
			public global::Unity.Collections.NativeArray<global::UnityEngine.ContactPairHeader>.ReadOnly PairedHeaders;

			public global::Unity.Collections.NativeArray<global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct> ResultsArray;

			public void Execute(int index)
			{
				global::UnityEngine.Vector3 zero = global::UnityEngine.Vector3.zero;
				global::UnityEngine.Vector3 zero2 = global::UnityEngine.Vector3.zero;
				global::UnityEngine.Vector3 zero3 = global::UnityEngine.Vector3.zero;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				global::UnityEngine.ContactPairHeader contactPairHeader;
				while (true)
				{
					int num5 = num4;
					contactPairHeader = PairedHeaders[index];
					if (num5 >= contactPairHeader.pairCount)
					{
						break;
					}
					contactPairHeader = PairedHeaders[index];
					ref readonly global::UnityEngine.ContactPair contactPair = ref contactPairHeader.GetContactPair(num4);
					if (!contactPair.isCollisionExit)
					{
						for (int i = 0; i < contactPair.contactCount; i++)
						{
							ref readonly global::UnityEngine.ContactPairPoint contactPoint = ref contactPair.GetContactPoint(i);
							zero2 += contactPoint.position;
							num3++;
							if (!contactPair.isCollisionStay)
							{
								zero += contactPoint.normal;
								num++;
							}
							else
							{
								zero3 += contactPoint.normal;
								num2++;
							}
						}
					}
					num4++;
				}
				if (num != 0)
				{
					zero /= (float)num;
				}
				if (num2 != 0)
				{
					zero3 /= (float)num2;
				}
				if (num3 != 0)
				{
					zero2 /= (float)num3;
				}
				global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct jobResultStruct = default(global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct);
				contactPairHeader = PairedHeaders[index];
				jobResultStruct.ThisInstanceID = contactPairHeader.bodyEntityId;
				contactPairHeader = PairedHeaders[index];
				jobResultStruct.OtherInstanceID = contactPairHeader.otherBodyEntityId;
				jobResultStruct.AverageNormal = zero;
				jobResultStruct.HasCollisionStay = num2 != 0;
				jobResultStruct.AverageCollisionStayNormal = zero3;
				jobResultStruct.ContactPoint = zero2;
				global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct value = jobResultStruct;
				ResultsArray[index] = value;
			}
		}

		private global::Unity.Collections.NativeArray<global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct> m_ResultsArray;

		private int m_Count;

		private global::Unity.Jobs.JobHandle m_JobHandle;

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::UnityEngine.Rigidbody> m_RigidbodyMapping = new global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::UnityEngine.Rigidbody>();

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::Unity.Netcode.Components.IContactEventHandler> m_HandlerMapping = new global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::Unity.Netcode.Components.IContactEventHandler>();

		private readonly global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::Unity.Netcode.Components.ContactEventHandlerInfo> m_HandlerInfo = new global::System.Collections.Generic.Dictionary<global::UnityEngine.EntityId, global::Unity.Netcode.Components.ContactEventHandlerInfo>();

		private bool m_HasCollisions;

		private int m_CurrentCount;

		private ulong m_EventId;

		public static global::Unity.Netcode.Components.RigidbodyContactEventManager Instance { get; private set; }

		private void OnEnable()
		{
			m_ResultsArray = new global::Unity.Collections.NativeArray<global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct>(16, global::Unity.Collections.Allocator.Persistent);
			global::UnityEngine.Physics.ContactEvent += Physics_ContactEvent;
			if (Instance != null)
			{
				global::Unity.Netcode.NetworkLog.LogError("[Invalid][Multiple Instances] Found more than one instance of RigidbodyContactEventManager: " + base.name + " and " + Instance.name);
				global::Unity.Netcode.NetworkLog.LogError("[Disable][Additional Instance] Disabling " + base.name + " instance!");
				base.gameObject.SetActive(value: false);
			}
			else
			{
				Instance = this;
			}
		}

		public void RegisterHandler(global::Unity.Netcode.Components.IContactEventHandler contactEventHandler, bool register = true)
		{
			global::UnityEngine.Rigidbody rigidbody = contactEventHandler.GetRigidbody();
			if (rigidbody == null)
			{
				return;
			}
			global::UnityEngine.EntityId entityId = rigidbody.GetEntityId();
			if (register)
			{
				if (!m_RigidbodyMapping.ContainsKey(entityId))
				{
					m_RigidbodyMapping.Add(entityId, rigidbody);
				}
				if (!m_HandlerMapping.ContainsKey(entityId))
				{
					m_HandlerMapping.Add(entityId, contactEventHandler);
				}
				if (!m_HandlerInfo.ContainsKey(entityId))
				{
					global::Unity.Netcode.Components.ContactEventHandlerInfo value = new global::Unity.Netcode.Components.ContactEventHandlerInfo
					{
						HasContactEventPriority = true,
						ProvideNonRigidBodyContactEvents = false
					};
					if (contactEventHandler is global::Unity.Netcode.Components.IContactEventHandlerWithInfo contactEventHandlerWithInfo)
					{
						value = contactEventHandlerWithInfo.GetContactEventHandlerInfo();
					}
					m_HandlerInfo.Add(entityId, value);
				}
			}
			else
			{
				m_RigidbodyMapping.Remove(entityId);
				m_HandlerMapping.Remove(entityId);
			}
		}

		private void OnDisable()
		{
			m_JobHandle.Complete();
			m_ResultsArray.Dispose();
			global::UnityEngine.Physics.ContactEvent -= Physics_ContactEvent;
			m_RigidbodyMapping.Clear();
			Instance = null;
		}

		private void ProcessCollisions()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.EntityId, global::Unity.Netcode.Components.IContactEventHandler> item in m_HandlerMapping)
			{
				if (item.Value is global::Unity.Netcode.Components.IContactEventHandlerWithInfo contactEventHandlerWithInfo)
				{
					m_HandlerInfo[item.Key] = contactEventHandlerWithInfo.GetContactEventHandlerInfo();
					continue;
				}
				global::Unity.Netcode.Components.ContactEventHandlerInfo value = m_HandlerInfo[item.Key];
				value.HasContactEventPriority = !m_RigidbodyMapping[item.Key].isKinematic;
				m_HandlerInfo[item.Key] = value;
			}
			for (int i = 0; i < m_Count; i++)
			{
				global::UnityEngine.EntityId thisInstanceID = m_ResultsArray[i].ThisInstanceID;
				global::UnityEngine.EntityId otherInstanceID = m_ResultsArray[i].OtherInstanceID;
				global::Unity.Netcode.Components.IContactEventHandler contactEventHandler = null;
				global::Unity.Netcode.Components.IContactEventHandler contactEventHandler2 = null;
				global::Unity.Netcode.Components.IContactEventHandler contactEventHandler3 = null;
				bool flag = false;
				global::Unity.Netcode.Components.IContactEventHandler contactEventHandler4 = null;
				global::UnityEngine.Rigidbody collidingBody = null;
				bool flag2 = false;
				if (m_RigidbodyMapping.ContainsKey(thisInstanceID))
				{
					contactEventHandler = m_HandlerMapping[thisInstanceID];
					global::Unity.Netcode.Components.ContactEventHandlerInfo contactEventHandlerInfo = m_HandlerInfo[thisInstanceID];
					if (contactEventHandlerInfo.HasContactEventPriority)
					{
						contactEventHandler3 = contactEventHandler;
						flag = contactEventHandlerInfo.ProvideNonRigidBodyContactEvents;
						_ = m_RigidbodyMapping[thisInstanceID];
					}
					else
					{
						contactEventHandler4 = contactEventHandler;
						flag2 = contactEventHandlerInfo.ProvideNonRigidBodyContactEvents;
						collidingBody = m_RigidbodyMapping[thisInstanceID];
					}
				}
				if (m_RigidbodyMapping.ContainsKey(otherInstanceID))
				{
					contactEventHandler2 = m_HandlerMapping[otherInstanceID];
					global::Unity.Netcode.Components.ContactEventHandlerInfo contactEventHandlerInfo2 = m_HandlerInfo[otherInstanceID];
					if (contactEventHandlerInfo2.HasContactEventPriority && contactEventHandler3 == null)
					{
						contactEventHandler3 = contactEventHandler2;
						flag = contactEventHandlerInfo2.ProvideNonRigidBodyContactEvents;
						_ = m_RigidbodyMapping[otherInstanceID];
					}
					else
					{
						contactEventHandler4 = contactEventHandler2;
						flag2 = contactEventHandlerInfo2.ProvideNonRigidBodyContactEvents;
						collidingBody = m_RigidbodyMapping[otherInstanceID];
					}
				}
				if (contactEventHandler3 == null && contactEventHandler4 != null)
				{
					contactEventHandler3 = contactEventHandler4;
					flag = flag2;
					contactEventHandler4 = null;
					flag2 = false;
					collidingBody = null;
				}
				if (contactEventHandler3 != null && (contactEventHandler3 == null || contactEventHandler4 != null || flag))
				{
					if (m_ResultsArray[i].HasCollisionStay)
					{
						contactEventHandler3.ContactEvent(m_EventId, m_ResultsArray[i].AverageNormal, collidingBody, m_ResultsArray[i].ContactPoint, m_ResultsArray[i].HasCollisionStay, m_ResultsArray[i].AverageCollisionStayNormal);
					}
					else
					{
						contactEventHandler3.ContactEvent(m_EventId, m_ResultsArray[i].AverageNormal, collidingBody, m_ResultsArray[i].ContactPoint);
					}
				}
			}
		}

		private void FixedUpdate()
		{
			if (m_HasCollisions || m_CurrentCount != 0)
			{
				if (m_HasCollisions)
				{
					m_CurrentCount = m_Count;
					m_HasCollisions = false;
					m_JobHandle.Complete();
				}
				ProcessCollisions();
			}
		}

		private void LateUpdate()
		{
			m_CurrentCount = 0;
		}

		private void Physics_ContactEvent(global::UnityEngine.PhysicsScene scene, global::Unity.Collections.NativeArray<global::UnityEngine.ContactPairHeader>.ReadOnly pairHeaders)
		{
			m_EventId++;
			m_HasCollisions = true;
			int length = pairHeaders.Length;
			if (m_ResultsArray.Length < length)
			{
				m_ResultsArray.Dispose();
				m_ResultsArray = new global::Unity.Collections.NativeArray<global::Unity.Netcode.Components.RigidbodyContactEventManager.JobResultStruct>(global::UnityEngine.Mathf.NextPowerOfTwo(length), global::Unity.Collections.Allocator.Persistent);
			}
			m_Count = length;
			global::Unity.Netcode.Components.RigidbodyContactEventManager.GetCollisionsJob jobData = new global::Unity.Netcode.Components.RigidbodyContactEventManager.GetCollisionsJob
			{
				PairedHeaders = pairHeaders,
				ResultsArray = m_ResultsArray
			};
			m_JobHandle = global::Unity.Jobs.IJobParallelForExtensions.Schedule(jobData, length, 256);
		}
	}
}
