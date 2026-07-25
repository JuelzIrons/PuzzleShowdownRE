public class VisualGarbageQueue : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	[global::UnityEngine.SerializeField]
	private float m_offsetLen;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_ChainGarabgeIcon;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_ComboGarabgeIcon;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<GarbageVisualObject> m_visualObjects;

	public void PurgeAllQueues()
	{
		foreach (GarbageVisualObject visualObject in m_visualObjects)
		{
			visualObject.Kill();
		}
	}

	private void CorrectlyShuffleQueue()
	{
		for (int i = 0; i < m_visualObjects.Count; i++)
		{
			if (m_visualObjects[i].HasArrived)
			{
				m_visualObjects[i].Visual.transform.position = base.transform.position + new global::UnityEngine.Vector3(m_offsetLen * (float)i, 0f);
			}
		}
	}

	public void IncrementGarbageVisualChain(global::UnityEngine.Vector3 clearPosition, int chainIndex)
	{
		CorrectlyShuffleQueue();
		int indexToIncrement = 0;
		for (int i = 0; i < m_visualObjects.Count; i++)
		{
			if (m_visualObjects[i].ChainIndexID == chainIndex)
			{
				indexToIncrement = i;
				break;
			}
		}
		global::UnityEngine.GameObject chainEffect = global::UnityEngine.Object.Instantiate(m_ChainGarabgeIcon);
		chainEffect.transform.position = clearPosition;
		chainEffect.GetComponentInChildren<global::TMPro.TextMeshPro>().text = (MyPodManager.OpponentManager.GameLoop.SlottedChainCounter[chainIndex] + 1).ToString() ?? "";
		
		{
			m_visualObjects[indexToIncrement].Visual.GetComponentInChildren<global::TMPro.TextMeshPro>().text = (MyPodManager.OpponentManager.GameLoop.SlottedChainCounter[chainIndex] + 1).ToString() ?? "";
			global::UnityEngine.Object.Destroy(chainEffect);
		});
	}

	public void AddChainItemToQueue(global::UnityEngine.Vector3 clearPosition, int chainIndex)
	{
		CorrectlyShuffleQueue();
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(m_ChainGarabgeIcon);
		gameObject.transform.position = clearPosition;
		gameObject.GetComponentInChildren<global::TMPro.TextMeshPro>().text = (MyPodManager.OpponentManager.GameLoop.SlottedChainCounter[chainIndex] + 1).ToString() ?? "";
		
		GarbageVisualObject newVis = new GarbageVisualObject(chainIndex, -1, gameObject);
		m_visualObjects.Add(newVis);
		
		{
			newVis.HasArrived = true;
		});
	}

	public void AddComboItemToQueue(global::UnityEngine.Vector3 clearPosition, int comboSize)
	{
		CorrectlyShuffleQueue();
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(m_ComboGarabgeIcon);
		gameObject.transform.position = clearPosition;
		gameObject.GetComponentInChildren<global::TMPro.TextMeshPro>().text = comboSize.ToString() ?? "";
		
		GarbageVisualObject newVis = new GarbageVisualObject(-1, comboSize, gameObject);
		m_visualObjects.Add(newVis);
		
		{
			newVis.HasArrived = true;
		});
	}

	public void RemoveItemFromQueue(int chainIndex, int comboSize)
	{
		int num = -1;
		if (chainIndex == -1)
		{
			for (int i = 0; i < m_visualObjects.Count; i++)
			{
				if (m_visualObjects[i].ComboSize == comboSize)
				{
					num = i;
					break;
				}
			}
		}
		else
		{
			for (int j = 0; j < m_visualObjects.Count; j++)
			{
				if (m_visualObjects[j].ChainIndexID == chainIndex)
				{
					num = j;
					break;
				}
			}
		}
		if (m_visualObjects.Count > num && num != -1)
		{
			m_visualObjects[num].Kill();
			m_visualObjects.RemoveAt(num);
		}
	}
}
