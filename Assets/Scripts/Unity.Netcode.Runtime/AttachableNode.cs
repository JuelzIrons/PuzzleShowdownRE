public class AttachableNode : global::Unity.Netcode.NetworkBehaviour
{
	public bool DetachOnDespawn = true;

	protected readonly global::System.Collections.Generic.List<global::Unity.Netcode.Components.AttachableBehaviour> m_AttachedBehaviours = new global::System.Collections.Generic.List<global::Unity.Netcode.Components.AttachableBehaviour>();

	public bool HasAttachments => m_AttachedBehaviours.Count > 0;

	protected override void OnNetworkPreSpawn(ref global::Unity.Netcode.NetworkManager networkManager)
	{
		m_AttachedBehaviours.Clear();
		base.OnNetworkPreSpawn(ref networkManager);
	}

	protected override void OnOwnershipChanged(ulong previous, ulong current)
	{
		m_AttachedBehaviours.Clear();
		if (current == base.NetworkManager.LocalClientId)
		{
			global::Unity.Netcode.Components.AttachableBehaviour[] componentsInChildren = base.NetworkObject.transform.GetComponentsInChildren<global::Unity.Netcode.Components.AttachableBehaviour>();
			foreach (global::Unity.Netcode.Components.AttachableBehaviour attachableBehaviour in componentsInChildren)
			{
				if (attachableBehaviour.InternalAttachableNode == this)
				{
					m_AttachedBehaviours.Add(attachableBehaviour);
				}
			}
		}
		base.OnOwnershipChanged(previous, current);
	}

	public override void OnNetworkPreDespawn()
	{
		if (base.IsSpawned && DetachOnDespawn)
		{
			for (int num = m_AttachedBehaviours.Count - 1; num >= 0; num--)
			{
				if ((bool)m_AttachedBehaviours[num])
				{
					if (!m_AttachedBehaviours[num].HasAuthority)
					{
						m_AttachedBehaviours[num].ForceDetach();
					}
					else
					{
						m_AttachedBehaviours[num].Detach();
					}
				}
			}
		}
		base.OnNetworkPreDespawn();
	}

	internal override void InternalOnDestroy()
	{
		for (int num = m_AttachedBehaviours.Count - 1; num >= 0; num--)
		{
			m_AttachedBehaviours[num]?.OnAttachNodeDestroy();
		}
		m_AttachedBehaviours.Clear();
		base.InternalOnDestroy();
	}

	protected virtual void OnAttached(global::Unity.Netcode.Components.AttachableBehaviour attachableBehaviour)
	{
	}

	internal void Attach(global::Unity.Netcode.Components.AttachableBehaviour attachableBehaviour)
	{
		if (m_AttachedBehaviours.Contains(attachableBehaviour))
		{
			global::Unity.Netcode.NetworkLog.LogError("[AttachableNode][" + base.name + "][Attach] AttachableBehaviour " + attachableBehaviour.name + " is already attached!");
		}
		else
		{
			m_AttachedBehaviours.Add(attachableBehaviour);
			OnAttached(attachableBehaviour);
		}
	}

	protected virtual void OnDetached(global::Unity.Netcode.Components.AttachableBehaviour attachableBehaviour)
	{
	}

	internal void Detach(global::Unity.Netcode.Components.AttachableBehaviour attachableBehaviour)
	{
		if (!m_AttachedBehaviours.Contains(attachableBehaviour))
		{
			global::Unity.Netcode.NetworkLog.LogError("[AttachableNode][" + base.name + "][Detach] AttachableBehaviour " + attachableBehaviour.name + " is not attached!");
		}
		else
		{
			m_AttachedBehaviours.Remove(attachableBehaviour);
			OnDetached(attachableBehaviour);
		}
	}

	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	protected internal override string __getTypeName()
	{
		return "AttachableNode";
	}
}
