public class CPUSearch : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	private int m_width;

	private int m_height;

	private global::System.Threading.Tasks.Task<(int[] swaps, int score, CPUGameState bestState)> _currentSearch;

	public void OnGameStarted()
	{
		if (!MyPodManager.IsControlledByCPU)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
		m_width = 6;
		m_height = 11;
	}

	public bool TryGetSearchResult(out (int[] swap, int score, CPUGameState bestState) result)
	{
		if (_currentSearch == null || !_currentSearch.IsCompleted)
		{
			result = default((int[], int, CPUGameState));
			return false;
		}
		result = _currentSearch.Result;
		_currentSearch = null;
		return true;
	}

	public void StartSearch(CPUGameState state, int maxDepth)
	{
		if (_currentSearch == null)
		{
			_currentSearch = global::System.Threading.Tasks.Task.Run(delegate
			{
				int startScore = EvaluateState(state);
				return RunSearch(state, maxDepth, startScore);
			});
		}
	}

	public (int[], int, CPUGameState) RunSearch(CPUGameState state, int maxDepth, int startScore)
	{
		CPUGameState bestState = state.Clone();
		bool foundBetter = false;
		int[] bestSwaps = new int[maxDepth];
		for (int i = 1; i < maxDepth; i++)
		{
			bestSwaps[i] = -1;
		}
		int bestScore = startScore;
		for (int j = 1; j <= maxDepth; j++)
		{
			Recurse(state, 1, (int[])bestSwaps.Clone(), j);
		}
		if (EvaluateState(state) == bestScore)
		{
			bestScore = -1;
		}
		if (!foundBetter)
		{
			bestScore = -1;
		}
		return (bestSwaps, bestScore, bestState);
		void Recurse(CPUGameState s, int d, int[] swaps, int toDepth)
		{
			int[] legalSwaps = GetLegalSwaps(s);
			for (int k = 0; k < legalSwaps.Length; k++)
			{
				if (legalSwaps[k] != -1 && (d <= 1 || swaps[d - 2] != k) && !state.garbageMap[k] && !state.revealMap[k])
				{
					CPUGameState cPUGameState = MyPodManager.CPUInput.UpdateState(MyPodManager.CPUInput.SwapState(s, k));
					swaps[d - 1] = k;
					if (d == toDepth)
					{
						int num = EvaluateState(cPUGameState);
						if (num > bestScore)
						{
							bestScore = num;
							foundBetter = true;
							global::System.Array.Copy(swaps, bestSwaps, swaps.Length);
							bestState = cPUGameState;
						}
					}
					else
					{
						Recurse(cPUGameState, d + 1, (int[])swaps.Clone(), toDepth);
					}
				}
			}
		}
	}

	public int[] GetLegalSwaps(CPUGameState state)
	{
		bool[] garbageMap = state.garbageMap;
		bool[] revealMap = state.revealMap;
		bool[] matchingMap = state.matchingMap;
		int[] tileColorMap = state.tileColorMap;
		int[] array = new int[tileColorMap.Length];
		array[tileColorMap.Length - 1] = -1;
		for (int i = 0; i < tileColorMap.Length - 1; i++)
		{
			if (i % m_width != m_width - 1 && (tileColorMap[i] > 0 || tileColorMap[i + 1] > 0) && !garbageMap[i] && !garbageMap[i + 1] && !matchingMap[i] && !matchingMap[i + 1] && !revealMap[i] && !revealMap[i + 1] && tileColorMap[i] != tileColorMap[i + 1])
			{
				array[i] = 1;
			}
			else
			{
				array[i] = -1;
			}
		}
		return array;
	}

	public int EvaluateState(CPUGameState state)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 1; i < global::System.Linq.Enumerable.Count(state.slottedChainCounter); i++)
		{
			int num4 = state.slottedChainCounter[i] * state.slottedChainCounter[i];
			if (num4 > 1)
			{
				num += num4 - 1;
			}
			if (num3 < state.slottedChainCounter[i])
			{
				num3 = state.slottedChainCounter[i];
			}
		}
		num2 += global::System.Linq.Enumerable.Sum(state.slottedComboCounter, (ListWrapper<int> x) => x.list.Count);
		_ = state.GetMaxOccupiedIndex() / m_width;
		_ = m_height / 2;
		bool flag = state.GetMaxOccupiedIndex() / m_width > 3 * m_height / 4;
		int num5 = global::System.Linq.Enumerable.Count(state.garbageMap, (bool x) => x);
		int num6 = global::System.Linq.Enumerable.Count(state.tileColorMap, (int x) => x == 0) + num5;
		int num7 = m_height * m_width - num6;
		return ((!(state.GetMaxOccupiedIndex(garbage: false) / m_width < 4 && num7 < 3 * m_width && flag)) ? 1000 : 0) - (flag ? (50 * (state.GetMaxOccupiedIndex(garbage: false) / m_width)) : 0) + ((MyPodManager.CPUAct.MAX_CHAIN_PRIO >= num3) ? (2 * num * num) : 0) + ((MyPodManager.CPUAct.CPULevel < CPUAct.CPU_LEVEL_COMBO_CUTOFF) ? num2 : 0) - ((MyPodManager.CPUAct.MAX_CHAIN_PRIO > num3) ? 1000 : 0) - 10 * global::System.Linq.Enumerable.Count(state.garbageMap, (bool t) => t) - (flag ? (50 * global::System.Linq.Enumerable.Count(state.garbageMap, (bool t) => t)) : 0);
	}
}
