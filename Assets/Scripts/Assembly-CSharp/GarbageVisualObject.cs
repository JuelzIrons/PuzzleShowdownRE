[global::System.Serializable]
public class GarbageVisualObject
{
	public int ChainIndexID;

	public int ComboSize;

	public global::UnityEngine.GameObject Visual;

	public bool HasArrived;

	public GarbageVisualObject(int chainIndex, int comboSize, global::UnityEngine.GameObject visual)
	{
		ChainIndexID = chainIndex;
		ComboSize = comboSize;
		Visual = visual;
	}

	public void Kill()
	{
		global::UnityEngine.Object.Destroy(Visual);
	}
}
