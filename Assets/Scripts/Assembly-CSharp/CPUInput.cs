public class CPUInput : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	public int width;

	private int m_height;

	private int m_height_with_garbage;

	private void Start()
	{
		width = 6;
		m_height = 11;
		m_height_with_garbage = 163;
	}

	public CPUGameState GetCurrentState()
	{
		int[] array = new int[width * m_height];
		int[] array2 = new int[width * m_height];
		bool[] array3 = new bool[width * m_height];
		bool[] array4 = new bool[width * m_height];
		bool[] array5 = new bool[width * m_height];
		int[] array6 = new int[width * m_height];
		int num = 0;
		global::System.Collections.Generic.List<int> slottedChainCounter = new global::System.Collections.Generic.List<int>(MyPodManager.GameLoop.SlottedChainCounter);
		global::System.Collections.Generic.List<ListWrapper<int>> slottedComboCounter = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(MyPodManager.GameLoop.SlottedComboCounter, (ListWrapper<int> w) => w.Clone()));
		for (int num2 = 0; num2 < width * m_height; num2++)
		{
			global::UnityEngine.Vector2 coords = IndexToCoordinate(num2);
			if (MyPodManager.GridManager.TryGetSquareAtTile(coords, out var sqr))
			{
				array[num2] = (int)(sqr.Type + 1);
				array2[num2] = sqr.FallTS;
				array3[num2] = sqr.IsBeingDestroyed;
				array4[num2] = sqr.IsGarbage && !sqr.GarbageGroup.IsRevealing;
				array5[num2] = sqr.TotalGBGRevealTs != -1;
				num = global::System.Math.Max(num, sqr.TotalGBGRevealTs - MyPodManager.GameLoop.GameLoopFrameCounter);
				if (sqr.IsGarbage)
				{
					array6[num2] = sqr.GarbageGroup.revealChainID;
				}
				else
				{
					array6[num2] = sqr.ChainID;
				}
			}
			else
			{
				array[num2] = 0;
				array4[num2] = false;
				array5[num2] = false;
				array6[num2] = 0;
			}
		}
		return new CPUGameState(array, array2, array3, array4, array5, array6, slottedChainCounter, slottedComboCounter, num);
	}

	public int CoordinateToIndex(global::UnityEngine.Vector2 coord)
	{
		return (int)coord.x + (int)coord.y * width;
	}

	public global::UnityEngine.Vector2 IndexToCoordinate(int i)
	{
		return new global::UnityEngine.Vector2(i % width, i / width);
	}

	public CPUGameState UpdateState(CPUGameState state)
	{
		state = state.Clone();
		state.chainIDMap = UpdateChainIDs(state.matchingMap, state.revealMap, state.chainIDMap);
		state = RemoveMatching(state);
		int[] tileColorMap = state.tileColorMap;
		bool[] matchingMap = state.matchingMap;
		bool[] garbageMap = state.garbageMap;
		bool[] revealMap = state.revealMap;
		int[] chainIDMap = state.chainIDMap;
		global::System.Collections.Generic.List<int> slottedChainCounter = state.slottedChainCounter;
		global::System.Collections.Generic.List<ListWrapper<int>> slottedComboCounter = state.slottedComboCounter;
		bool flag = true;
		bool firstTime = true;
		while (flag)
		{
			(int[], bool[], bool[], int[], bool[]) tuple = UpdateFall(tileColorMap, garbageMap, revealMap, chainIDMap, firstTime);
			tileColorMap = tuple.Item1;
			garbageMap = tuple.Item2;
			revealMap = tuple.Item3;
			chainIDMap = tuple.Item4;
			bool[] item = tuple.Item5;
			firstTime = false;
			flag = false;
			bool flag2 = true;
			while (flag2)
			{
				(state, flag2) = UpdateMatch(new CPUGameState(tileColorMap, state.fallTSMap, matchingMap, garbageMap, revealMap, chainIDMap, slottedChainCounter, slottedComboCounter, state.maxRevealFramesLeft), item);
				if (flag2)
				{
					flag = true;
				}
			}
		}
		return state;
	}

	private CPUGameState RemoveMatching(CPUGameState state)
	{
		for (int i = 0; i < state.tileColorMap.Length; i++)
		{
			if (state.matchingMap[i])
			{
				state.tileColorMap[i] = 0;
				state.matchingMap[i] = false;
				state.chainIDMap[i] = 0;
			}
		}
		return state;
	}

	private int[] UpdateChainIDs(bool[] matchingMap, bool[] revealMap, int[] chainIDMap)
	{
		int[] array = new int[width];
		int num = 0;
		for (int i = 0; i < global::System.Linq.Enumerable.Count(matchingMap); i++)
		{
			int num2 = i % width;
			if (matchingMap[i])
			{
				array[num2] = chainIDMap[i];
				num = chainIDMap[i];
			}
			else if (revealMap[i])
			{
				chainIDMap[i] = num;
			}
			else
			{
				chainIDMap[i] = array[num2];
			}
		}
		return chainIDMap;
	}

	public (int[], bool[], bool[], int[], bool[]) UpdateFall(int[] tileColorMap, bool[] garbageMap, bool[] revealMap, int[] chainIDMap, bool firstTime)
	{
		bool[] array = new bool[width * m_height];
		bool flag = true;
		bool flag2 = true;
		bool flag3 = false;
		while (flag)
		{
			flag = false;
			for (int i = 0; i < tileColorMap.Length - width; i++)
			{
				if (i % width == 0)
				{
					flag3 = flag2;
					flag2 = true;
				}
				if (tileColorMap[i] == 0 && tileColorMap[i + width] != 0 && (!garbageMap[i + width] || flag3) && (!firstTime || !revealMap[i + width]))
				{
					flag = true;
					array[i] = true;
					array[i + width] = false;
					tileColorMap[i] = tileColorMap[i + width];
					tileColorMap[i + width] = 0;
					revealMap[i + width] = revealMap[i];
					revealMap[i] = false;
					chainIDMap[i] = chainIDMap[i + width];
					chainIDMap[i + width] = 0;
					garbageMap[i] = garbageMap[i + width];
					garbageMap[i + width] = false;
				}
				else if (tileColorMap[i + width] != 0)
				{
					flag2 = false;
				}
			}
		}
		return (tileColorMap, garbageMap, revealMap, chainIDMap, array);
	}

	public (CPUGameState, bool) UpdateMatch(CPUGameState state, bool[] fallMap)
	{
		int[] tileColorMap = state.tileColorMap;
		bool[] matchingMap = state.matchingMap;
		bool[] garbageMap = state.garbageMap;
		bool[] revealMap = state.revealMap;
		int[] chainIDMap = state.chainIDMap;
		global::System.Collections.Generic.List<int> slottedChainCounter = state.slottedChainCounter;
		global::System.Collections.Generic.List<ListWrapper<int>> slottedComboCounter = state.slottedComboCounter;
		for (int i = 0; i < tileColorMap.Length; i++)
		{
			if (width - i % width > 2 && tileColorMap[i] != 0 && !garbageMap[i] && !matchingMap[i])
			{
				int num = tileColorMap[i];
				int num2 = 1;
				int num3 = i;
				int num4 = chainIDMap[i];
				bool flag = fallMap[i];
				while (num3 < (i / width + 1) * width)
				{
					num3++;
					if (num3 >= tileColorMap.Length || tileColorMap[num3] != num || garbageMap[num3] || matchingMap[num3])
					{
						break;
					}
					if (fallMap[num3] || revealMap[num3])
					{
						flag = true;
					}
					num2++;
					if (chainIDMap[num3] != 0 && fallMap[num3])
					{
						num4 = chainIDMap[num3];
					}
				}
				if (num2 >= 3)
				{
					int num5 = num4;
					if (num4 == 0 || !flag)
					{
						for (int j = 1; j < slottedChainCounter.Count; j++)
						{
							if (slottedChainCounter[j] == 0)
							{
								num5 = j;
							}
						}
					}
					if (num4 == 0)
					{
						num4 = num5;
					}
					if (num2 > 3)
					{
						slottedComboCounter[flag ? num4 : num5].list.Add(num2);
					}
					slottedChainCounter[flag ? num4 : num5]++;
					for (int k = i; k < i + num2; k++)
					{
						for (int l = k + width; l < garbageMap.Length; l += width)
						{
							fallMap[l] = false;
							chainIDMap[l] = num4;
						}
						tileColorMap[k] = 0;
						chainIDMap[k] = 0;
						revealMap[k] = false;
						if (k + width >= garbageMap.Length || !garbageMap[k + width])
						{
							continue;
						}
						for (int m = 0; m < garbageMap.Length; m++)
						{
							if (garbageMap[m])
							{
								chainIDMap[m] = num4;
								garbageMap[m] = false;
							}
						}
					}
					return (new CPUGameState(tileColorMap, state.fallTSMap, matchingMap, garbageMap, revealMap, chainIDMap, slottedChainCounter, slottedComboCounter, state.maxRevealFramesLeft), true);
				}
			}
			if (i >= tileColorMap.Length - 2 * width || tileColorMap[i] == 0 || garbageMap[i] || matchingMap[i])
			{
				continue;
			}
			int num6 = tileColorMap[i];
			int num7 = 1;
			int num8 = i;
			int num9 = chainIDMap[i];
			bool flag2 = fallMap[i];
			while (num8 + width < tileColorMap.Length)
			{
				num8 += width;
				if (tileColorMap[num8] != num6 || garbageMap[num8] || matchingMap[num8])
				{
					break;
				}
				if (fallMap[num8] || revealMap[num8])
				{
					flag2 = true;
				}
				num7++;
				if (chainIDMap[num8] != 0 && fallMap[num8])
				{
					num9 = chainIDMap[num8];
				}
			}
			if (num7 < 3)
			{
				continue;
			}
			int num10 = num9;
			if (num9 == 0 || !flag2)
			{
				for (int n = 1; n < slottedChainCounter.Count; n++)
				{
					if (slottedChainCounter[n] == 0)
					{
						num10 = n;
					}
				}
			}
			if (num9 == 0)
			{
				num9 = num10;
			}
			if (num7 > 3)
			{
				slottedComboCounter[flag2 ? num9 : num10].list.Add(num7);
			}
			slottedChainCounter[flag2 ? num9 : num10]++;
			for (int num11 = i; num11 < i + num7 * width; num11 += width)
			{
				for (int num12 = num11 + width; num12 < garbageMap.Length; num12 += width)
				{
					fallMap[num12] = false;
					chainIDMap[num12] = num9;
				}
				tileColorMap[num11] = 0;
				chainIDMap[num11] = 0;
				revealMap[num11] = false;
			}
			if (i + num7 * width < garbageMap.Length && garbageMap[i + num7 * width])
			{
				for (int num13 = 0; num13 < garbageMap.Length; num13++)
				{
					if (garbageMap[num13])
					{
						chainIDMap[num13] = num9;
						garbageMap[num13] = false;
					}
				}
			}
			return (new CPUGameState(tileColorMap, state.fallTSMap, matchingMap, garbageMap, revealMap, chainIDMap, slottedChainCounter, slottedComboCounter, state.maxRevealFramesLeft), true);
		}
		return (state, false);
	}

	public CPUGameState SwapState(CPUGameState state, int swap)
	{
		if (swap < 0 || swap + 1 >= state.tileColorMap.Length)
		{
			global::UnityEngine.Debug.LogWarning($"SwapState: swap index {swap} out of bounds, ignoring swap.");
			return state.Clone();
		}
		if (swap % width == width - 1)
		{
			global::UnityEngine.Debug.LogWarning($"SwapState: swap index {swap} is at right edge of row, ignoring swap.");
			return state.Clone();
		}
		CPUGameState result = state.Clone();
		int[] tileColorMap = result.tileColorMap;
		int[] chainIDMap = result.chainIDMap;
		int num = tileColorMap[swap];
		_ = chainIDMap[swap];
		tileColorMap[swap] = tileColorMap[swap + 1];
		tileColorMap[swap + 1] = num;
		if (chainIDMap[swap] != chainIDMap[swap + 1])
		{
			chainIDMap[swap] = 0;
			chainIDMap[swap + 1] = 0;
		}
		return result;
	}

	public string printData(int[] data)
	{
		string text = "";
		int num = 0;
		for (int i = 0; i < m_height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				text += data[CoordinateToIndex(new global::UnityEngine.Vector2(j, m_height - i - 1))];
				num++;
				if (num >= width)
				{
					num = 0;
					text += "\n";
				}
			}
		}
		return text;
	}
}
