public class CPUAct : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	public int RaisesSinceSwapPlan;

	public int LastRaiseTS = -10;

	private int m_lastSwap;

	private int[] m_swapPlan = new int[1] { 1 };

	private int m_swapPlanStep;

	private int m_swapPlanDepth = 1;

	private int m_maxDepth = 3;

	private int m_swapPlanScore = -1;

	private CPUGameState m_swapPlanBestState;

	private CPUGameState m_swapPlanState;

	private CPUGameState m_gameState;

	private bool m_firstTime = true;

	private int m_width;

	private int m_height;

	private int[] m_imaginarySwaps = new int[3];

	private float CPU_action_delay = 0.0333f;

	private int m_waitTurnsAfterSwap = 3;

	private int m_waitTurnsAfterMove = 2;

	private int m_waitTurnsLeft;

	private global::UnityEngine.Vector2 m_moveDir = global::UnityEngine.Vector2.zero;

	public int CPULevel;

	public int MAX_CHAIN_PRIO;

	private bool m_forceUpLogic = true;

	private float randMovePercent;

	private float saveRandMovePercent;

	public int RAISE_LEVEL_BOUND;

	public bool isInDanger;

	public static readonly int CPU_LEVEL_COMBO_CUTOFF = 52;

	[global::UnityEngine.SerializeField]
	private CampaignDifficultyScalingSO m_difficultyScalingObj;

	public void ImprovedActionLoop()
	{
		MyPodManager.GameLoop.HoldingForceButton = false;
		if (MyPodManager.GameLoop.GameLoopFrameCounter % 2 == 1)
		{
			return;
		}
		m_imaginarySwaps[MyPodManager.GameLoop.GameLoopFrameCounter % 3] = -1;
		if (m_waitTurnsLeft > 0)
		{
			m_waitTurnsLeft--;
			return;
		}
		if (m_firstTime)
		{
			m_maxDepth = 3;
			MAX_CHAIN_PRIO = (int)m_difficultyScalingObj.MaxChainToAimForCurve.Evaluate(CPULevel);
			RAISE_LEVEL_BOUND = (int)m_difficultyScalingObj.RaiseLevelBoundCurve.Evaluate(CPULevel);
			m_waitTurnsAfterMove = (int)m_difficultyScalingObj.WaitTurnsAfterMoveCurve.Evaluate(CPULevel);
			m_waitTurnsAfterSwap = (int)m_difficultyScalingObj.WaitTurnsAfterSwapCurve.Evaluate(CPULevel);
			randMovePercent = m_difficultyScalingObj.RandomMovePercentCurve.Evaluate(CPULevel);
			saveRandMovePercent = m_difficultyScalingObj.RandomMovePercentCurve.Evaluate(CPULevel);
			global::UnityEngine.Debug.LogWarning($"SETTING AI DIFF TO {CPULevel}");
			for (int i = 0; i < m_imaginarySwaps.Length; i++)
			{
				m_imaginarySwaps[i] = -1;
			}
			m_width = 6;
			m_height = 11;
			m_gameState = MyPodManager.CPUInput.GetCurrentState();
			m_swapPlanState = m_gameState;
			m_swapPlan = null;
			MyPodManager.CPUSearch.StartSearch(m_gameState, m_swapPlanDepth);
			m_firstTime = false;
			return;
		}
		m_gameState = MyPodManager.CPUInput.GetCurrentState();
		ApplyImaginarySwaps();
		if (MyPodManager.CPUSearch.TryGetSearchResult(out (int[], int, CPUGameState) result))
		{
			(int[], int, CPUGameState) tuple = result;
			m_swapPlan = tuple.Item1;
			m_swapPlanScore = tuple.Item2;
			m_swapPlanBestState = tuple.Item3;
			m_swapPlanStep = 0;
		}
		else if (m_swapPlan == null)
		{
			return;
		}
		isInDanger = m_gameState.GetMaxOccupiedIndex() / m_width > m_height - 3;
		if (global::UnityEngine.Random.Range(0f, 100f) < (isInDanger ? saveRandMovePercent : randMovePercent))
		{
			m_swapPlan = RandomMoves(m_swapPlanDepth);
			m_swapPlanStep = 0;
		}
		if (!SameState(m_gameState))
		{
			m_swapPlan = null;
			RaisesSinceSwapPlan = 0;
			m_swapPlanState = m_gameState;
			m_swapPlanDepth = 1;
			MyPodManager.CPUSearch.StartSearch(m_gameState, m_swapPlanDepth);
			if (MyPodManager.WillLogCpuMoves)
			{
				global::UnityEngine.Debug.Log("State changed; starting new search");
			}
			return;
		}
		if (m_swapPlanScore == -1 || (m_swapPlanStep == m_swapPlan.Length - 1 && m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width == m_lastSwap))
		{
			if (m_swapPlanDepth >= m_maxDepth)
			{
				if (m_gameState.maxRevealFramesLeft > 100 && m_swapPlanDepth < m_maxDepth + 2)
				{
					m_swapPlanDepth++;
					m_swapPlan = null;
					m_swapPlanState = m_gameState;
					RaisesSinceSwapPlan = 0;
					MyPodManager.CPUSearch.StartSearch(m_gameState, m_swapPlanDepth);
					if (MyPodManager.WillLogCpuMoves)
					{
						global::UnityEngine.Debug.Log($"Started search with higher depth = {m_swapPlanDepth}");
					}
					if (MyPodManager.WillLogCpuMoves)
					{
						global::UnityEngine.Debug.Log("No result at max depth but reveal time left, forcing higher depth.");
					}
				}
				else if (m_gameState.maxRevealFramesLeft > 0 || global::System.Linq.Enumerable.Count(m_gameState.matchingMap, (bool x) => x) > 0 || global::System.Linq.Enumerable.Count(m_gameState.fallTSMap, (int x) => x > MyPodManager.GameLoop.GameLoopFrameCounter) > 0)
				{
					m_swapPlanStep = 0;
					m_swapPlan = RandomMoves(1);
					m_swapPlanScore = -1000;
					if (MyPodManager.WillLogCpuMoves)
					{
						global::UnityEngine.Debug.Log("No improved state but can't raise. Getting a random move.");
					}
				}
				else if (m_forceUpLogic && m_gameState.GetMaxOccupiedIndex() / m_width < m_height - RAISE_LEVEL_BOUND)
				{
					MyPodManager.GameLoop.HoldingForceButton = true;
					m_swapPlan = null;
					RaisesSinceSwapPlan = 0;
					m_swapPlanState = m_gameState;
					m_swapPlanDepth = 1;
					MyPodManager.CPUSearch.StartSearch(m_gameState, m_swapPlanDepth);
					if (MyPodManager.WillLogCpuMoves)
					{
						global::UnityEngine.Debug.Log("Couldn't find improved state with max depth; forcing and starting search. State: \n" + MyPodManager.CPUInput.printData(m_gameState.tileColorMap));
					}
				}
				else
				{
					m_swapPlanStep = 0;
					m_swapPlan = RandomMoves(3);
					m_swapPlanScore = -1000;
				}
			}
			else
			{
				m_swapPlanDepth++;
				m_swapPlan = null;
				m_swapPlanState = m_gameState;
				RaisesSinceSwapPlan = 0;
				MyPodManager.CPUSearch.StartSearch(m_gameState, m_swapPlanDepth);
				if (MyPodManager.WillLogCpuMoves)
				{
					global::UnityEngine.Debug.Log($"Started search with higher depth = {m_swapPlanDepth}");
				}
			}
			return;
		}
		if (m_swapPlanStep < m_swapPlan.Length - 1 && m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width == m_lastSwap)
		{
			m_swapPlanStep++;
			return;
		}
		string text = $"Executing step {m_swapPlanStep} of plan: ";
		for (int num = 0; num < m_swapPlan.Length; num++)
		{
			text = text + (m_swapPlan[num] + RaisesSinceSwapPlan * m_width) + ", ";
		}
		text = text + "with score: " + m_swapPlanScore;
		text = text + "\nBetter than current state, with score: " + MyPodManager.CPUSearch.EvaluateState(m_swapPlanState);
		text += "\n";
		int[] array = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Concat(m_swapPlanState.tileColorMap, MyPodManager.CPUInput.UpdateState(m_swapPlanBestState).tileColorMap));
		for (int num2 = m_width * m_height * 2 - 1; num2 >= 0; num2--)
		{
			if (num2 % (m_width * m_height) == m_width * m_height - 1)
			{
				text += "\n";
			}
			if (num2 % m_width == m_width - 1)
			{
				text += "\n ";
			}
			text += array[num2];
		}
		if (MyPodManager.WillLogCpuMoves)
		{
			global::UnityEngine.MonoBehaviour.print(text);
		}
		if (m_swapPlanStep == m_swapPlan.Length || m_swapPlan[m_swapPlanStep] == -1)
		{
			m_swapPlanDepth = 1;
			m_swapPlan = null;
			RaisesSinceSwapPlan = 0;
			m_swapPlanState = m_gameState;
			MyPodManager.CPUSearch.StartSearch(m_gameState, m_swapPlanDepth);
			if (MyPodManager.WillLogCpuMoves)
			{
				global::UnityEngine.Debug.Log("Done with plan; starting new search on tileMap:\n" + MyPodManager.CPUInput.printData(m_gameState.tileColorMap));
			}
		}
		else if (MyPodManager.CPUInput.CoordinateToIndex(MyPodManager.CursorController.coords[0]) != m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width)
		{
			StepTo(m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width);
			m_waitTurnsLeft = m_waitTurnsAfterMove;
		}
		else if (MyPodManager.CursorController.TrySwap())
		{
			if (MyPodManager.WillLogCpuMoves)
			{
				global::UnityEngine.Debug.Log("Swap worked");
			}
			m_swapPlanState = MyPodManager.CPUInput.SwapState(m_swapPlanState, m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width);
			m_imaginarySwaps[MyPodManager.GameLoop.GameLoopFrameCounter % 3] = m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width;
			m_lastSwap = m_swapPlan[m_swapPlanStep] + RaisesSinceSwapPlan * m_width;
			m_swapPlanStep++;
			m_waitTurnsLeft = m_waitTurnsAfterSwap;
		}
		else if (MyPodManager.WillLogCpuMoves)
		{
			global::UnityEngine.Debug.Log("Swap failed");
		}
	}

	private int[] RandomMoves(int count)
	{
		int[] legalSwaps = MyPodManager.CPUSearch.GetLegalSwaps(m_gameState);
		global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
		for (int i = 0; i < legalSwaps.Length; i++)
		{
			if (legalSwaps[i] == 1)
			{
				list.Add(i);
			}
		}
		int[] array = new int[count];
		if (list.Count == 0)
		{
			if (MyPodManager.WillLogCpuMoves)
			{
				global::UnityEngine.Debug.LogWarning("RandomMoves: no legal swaps available, returning fallback plan.");
			}
			for (int j = 0; j < count; j++)
			{
				array[j] = -1;
			}
			return array;
		}
		for (int k = 0; k < count; k++)
		{
			array[k] = list[global::UnityEngine.Random.Range(0, list.Count)];
		}
		return array;
	}

	private void ApplyImaginarySwaps()
	{
		for (int i = 0; i < m_imaginarySwaps.Length; i++)
		{
			if (m_imaginarySwaps[i] != -1)
			{
				if (RaisesSinceSwapPlan < (i - MyPodManager.GameLoop.GameLoopFrameCounter % 3) % 3 * 2)
				{
					m_gameState = MyPodManager.CPUInput.SwapState(m_gameState, m_imaginarySwaps[i] + m_width);
				}
				else
				{
					m_gameState = MyPodManager.CPUInput.SwapState(m_gameState, m_imaginarySwaps[i]);
				}
			}
		}
	}

	private bool SameState(CPUGameState state)
	{
		int[] tileColorMap = state.tileColorMap;
		for (int i = 0; i < m_height * m_width - (RaisesSinceSwapPlan + 2) * m_width; i++)
		{
			if (tileColorMap[i + RaisesSinceSwapPlan * m_width] == m_swapPlanState.tileColorMap[i])
			{
				continue;
			}
			bool flag = false;
			for (int j = 0; j < m_swapPlan.Length; j++)
			{
				if (m_swapPlan[j] == i || m_swapPlan[j] + 1 == i)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	private void StepTo(int to)
	{
		global::UnityEngine.Vector2 vector = MyPodManager.CPUInput.IndexToCoordinate(to);
		global::UnityEngine.Vector2 vector2 = MyPodManager.CursorController.coords[0];
		global::UnityEngine.Vector2 vector3 = new global::UnityEngine.Vector2(0f, 0f);
		vector3 = ((vector.x > vector2.x) ? global::UnityEngine.Vector2.right : ((vector.x < vector2.x) ? global::UnityEngine.Vector2.left : ((!(vector.y > vector2.y)) ? global::UnityEngine.Vector2.down : global::UnityEngine.Vector2.up)));
		if (m_moveDir != vector3)
		{
			m_moveDir = vector3;
		}
		else
		{
			MyPodManager.CursorController.TryApplyMove(vector3);
		}
	}
}
