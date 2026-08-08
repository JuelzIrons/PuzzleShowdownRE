public struct CPUGameState
{
	public int[] tileColorMap;

	public int[] fallTSMap;

	public bool[] matchingMap;

	public bool[] garbageMap;

	public bool[] revealMap;

	public int[] chainIDMap;

	public global::System.Collections.Generic.List<int> slottedChainCounter;

	public global::System.Collections.Generic.List<ListWrapper<int>> slottedComboCounter;

	public int maxRevealFramesLeft;

	public CPUGameState(int[] tileColorMap, int[] fallTSMap, bool[] matchingMap, bool[] garbageMap, bool[] revealMap, int[] chainIDMap, global::System.Collections.Generic.List<int> slottedChainCounter, global::System.Collections.Generic.List<ListWrapper<int>> slottedComboCounter, int maxRevealFramesLeft)
	{
		this = default(CPUGameState);
		this.tileColorMap = tileColorMap;
		this.fallTSMap = fallTSMap;
		this.matchingMap = matchingMap;
		this.garbageMap = garbageMap;
		this.revealMap = revealMap;
		this.chainIDMap = chainIDMap;
		this.slottedChainCounter = slottedChainCounter;
		this.slottedComboCounter = slottedComboCounter;
		this.maxRevealFramesLeft = maxRevealFramesLeft;
	}

	public int GetMaxOccupiedIndex(bool garbage = true)
	{
		int result = 0;
		int num = 6;
		bool[] array = new bool[num];
		for (int i = 0; i < global::System.Linq.Enumerable.Count(tileColorMap); i++)
		{
			int num2 = i % num;
			if (tileColorMap[i] != 0 && (garbage || !garbageMap[i]))
			{
				if (matchingMap[i])
				{
					array[num2] = true;
				}
				if (!array[num2] || garbageMap[i])
				{
					result = i;
				}
			}
			else if (tileColorMap[i] == 0)
			{
				array[num2] = true;
			}
		}
		return result;
	}

	public CPUGameState Clone()
	{
		return new CPUGameState((int[])tileColorMap.Clone(), (int[])fallTSMap.Clone(), (bool[])matchingMap.Clone(), (bool[])garbageMap.Clone(), (bool[])revealMap.Clone(), (int[])chainIDMap.Clone(), new global::System.Collections.Generic.List<int>(slottedChainCounter), global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(slottedComboCounter, (ListWrapper<int> w) => w.Clone())), maxRevealFramesLeft);
	}
}
