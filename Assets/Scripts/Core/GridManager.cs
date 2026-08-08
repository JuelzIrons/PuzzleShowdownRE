public class GridManager : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	public const int WIDTH = 6;

	public const int HEIGHT = 11;

	public const int GARBAGE_BONUS_HEIGHT = 150;

	[global::UnityEngine.SerializeField]
	private global::Unity.Mathematics.int2 m_gridOffset;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_garbageGroup;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_gridVisual;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.GameObject> m_squareThings = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

	public global::UnityEngine.Transform m_blockHolder;

	public global::System.Collections.Generic.List<global::UnityEngine.Vector2> GridPositions = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();

	public static global::System.Collections.Generic.List<SquareComponent> CURRENTMATCHES = new global::System.Collections.Generic.List<SquareComponent>();

	public global::System.Collections.Generic.List<SquareComponent> AliveSquares;

	private global::System.Collections.Generic.List<SquareComponent> AliveSquaresTemp;

	public global::UnityEngine.GameObject GhostPrefab;

	private global::System.Collections.Generic.List<GarbageInstruction> GarbageQueue = new global::System.Collections.Generic.List<GarbageInstruction>();

	public SquareComponent GetTopSquareInRow(int rowCoord)
	{
		global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(rowCoord, 0f);
		SquareComponent result = null;
		for (int i = 0; i < 11; i++)
		{
			vector = new global::UnityEngine.Vector2(rowCoord, i);
			if (TryGetSquareAtTile(vector, out var sqr))
			{
				result = sqr;
			}
		}
		return result;
	}

	private void Awake()
	{
		foreach (global::UnityEngine.Transform item in m_blockHolder)
		{
			if ((bool)item.GetComponent<SquareComponent>())
			{
				AliveSquares.Add(item.GetComponent<SquareComponent>());
				item.GetComponent<SquareComponent>().IsMatchable = true;
				item.GetComponent<SquareComponent>().ShouldCheckForMatches = true;
				item.GetComponent<SquareComponent>().FallTS = -1;
			}
		}
	}

	public bool GetAllSquaresInCollumn(int rowCoord, out global::System.Collections.Generic.List<SquareComponent> list)
	{
		list = new global::System.Collections.Generic.List<SquareComponent>();
		global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(rowCoord, 0f);
		for (int i = -1; i < 11; i++)
		{
			vector = new global::UnityEngine.Vector2(rowCoord, i);
			if (TryGetSquareAtTile(vector, out var sqr))
			{
				list.Add(sqr);
			}
		}
		if (list.Count > 0)
		{
			return true;
		}
		return false;
	}

	public void CheckSendGarbageQueue()
	{
		int num = 0;
		for (int i = 0; i < GarbageQueue.Count; i++)
		{
			if (MyPodManager.IsOnlineMp && GarbageQueue[i].Ts <= (float)NetworkServerReciever.Instance.LocalTS)
			{
				int xOffset = 6 - GarbageQueue[i].Width;
				SpawnGarbageGroup(GarbageQueue[i].Width, GarbageQueue[i].Height, xOffset);
				num++;
			}
			else if (!MyPodManager.IsOnlineMp && GarbageQueue[i].Ts <= (float)MyPodManager.GameLoop.GameLoopFrameCounter)
			{
				int xOffset2 = 6 - GarbageQueue[i].Width;
				SpawnGarbageGroup(GarbageQueue[i].Width, GarbageQueue[i].Height, xOffset2);
				num++;
			}
		}
		GarbageQueue.RemoveRange(0, num);
	}

	public bool TryGetSquareSurronding(global::UnityEngine.Vector2 coords, out global::System.Collections.Generic.List<SquareComponent> sqrs)
	{
		sqrs = new global::System.Collections.Generic.List<SquareComponent>();
		SquareComponent sqr = null;
		if (TryGetSquareAtTile(coords + global::UnityEngine.Vector2.up, out sqr))
		{
			sqrs.Add(sqr);
		}
		if (TryGetSquareAtTile(coords + global::UnityEngine.Vector2.down, out sqr))
		{
			sqrs.Add(sqr);
		}
		if (TryGetSquareAtTile(coords + global::UnityEngine.Vector2.right, out sqr))
		{
			sqrs.Add(sqr);
		}
		if (TryGetSquareAtTile(coords + global::UnityEngine.Vector2.left, out sqr))
		{
			sqrs.Add(sqr);
		}
		if (sqrs.Count > 0)
		{
			return true;
		}
		return false;
	}

	public bool TryGetSquareAtTile(global::UnityEngine.Vector2 coords, out SquareComponent sqr, bool falseOnFalling = false, bool falseOnSwappingAndFalling = false, bool falseOnSwapping = false)
	{
		sqr = null;
		for (int i = 0; i < AliveSquares.Count; i++)
		{
			if (AliveSquares[i].Coords == coords)
			{
				sqr = AliveSquares[i];
				if (falseOnFalling && sqr.IsFalling)
				{
					return false;
				}
				if (falseOnSwappingAndFalling && sqr.IsSwapping && !TryGetSquareAtTile(sqr.Coords + new global::UnityEngine.Vector2(0f, -1f), out var _) && GridPositions.Contains(sqr.Coords + new global::UnityEngine.Vector2(0f, -1f)))
				{
					return false;
				}
				if (falseOnSwapping && sqr.IsSwapping)
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	public bool TilesOnRow(int rowNum)
	{
		for (int i = (int)base.transform.position.x; i < 6; i++)
		{
			if (TryGetSquareAtTile(new global::UnityEngine.Vector2(i, rowNum), out var _))
			{
				return true;
			}
		}
		return false;
	}

	public void TimeTickAllBlocks()
	{
		AliveSquaresTemp = new global::System.Collections.Generic.List<SquareComponent>(AliveSquares);
		for (int i = 0; i < AliveSquaresTemp.Count; i++)
		{
			AliveSquaresTemp[i].FixedTimerManager();
		}
	}

	public void FallTickAllBlocks()
	{
		for (int i = 0; i < AliveSquares.Count; i++)
		{
			AliveSquares[i].UpdateWillFall();
		}
		for (int j = 0; j < AliveSquares.Count; j++)
		{
			AliveSquares[j].GarbyCheck2();
		}
		for (int k = 0; k < AliveSquares.Count; k++)
		{
			AliveSquares[k].GarbyCheck3();
		}
		for (int l = 0; l < AliveSquares.Count; l++)
		{
			AliveSquares[l].Fall();
		}
		for (int m = 0; m < AliveSquares.Count; m++)
		{
			AliveSquares[m].SetLateFallTsAbove();
		}
		for (int n = 0; n < AliveSquares.Count; n++)
		{
			AliveSquares[n].SetLateLand();
		}
	}

	public void LandTicks()
	{
		foreach (global::UnityEngine.Vector2 shouldLandAllPosition in MyPodManager.GameLoop.ShouldLandAllPositions)
		{
			LandAllBlocksAbove(shouldLandAllPosition);
		}
	}

	public void ClearChainTicks()
	{
		foreach (SquareComponent shouldClearChainId in MyPodManager.GameLoop.ShouldClearChainIds)
		{
			if (shouldClearChainId.TempChainIDFrames <= 0 || shouldClearChainId.IsBeingDestroyed)
			{
				if (shouldClearChainId.ChainID != 0)
				{
					MyPodManager.GameLoop.SubtractChainBlockCount(shouldClearChainId.ChainID);
				}
				shouldClearChainId.ChainID = 0;
			}
		}
	}

	public void LandAllBlocksAbove(global::UnityEngine.Vector2 position)
	{
		float y = position.y;
		SquareComponent sqr;
		for (int i = (int)y; i < 161 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(position.x, i), out sqr) && (sqr.Type != BlockType.Ghost || i == (int)y); i++)
		{
			if (sqr.IsGarbage)
			{
				if (sqr.GarbageGroup.Blocks.Count > 6)
				{
					for (int j = 0; j < sqr.GarbageGroup.Blocks.Count; j++)
					{
						if (MyPodManager.GridManager.TryGetSquareAtTile(sqr.GarbageGroup.Blocks[j].Coords + global::UnityEngine.Vector2.up, out var sqr2) && sqr2.FallTS != -1)
						{
							sqr2.FallTS = -1;
							LandAllBlocksAbove(sqr2.Coords);
						}
					}
				}
				MyPodManager.GameLoop.StartBlockShake();
			}
			sqr.FallTS = -1;
			sqr.StartCoroutine(sqr.SquareLandRoutine());
			sqr.ShouldCheckForMatches = true;
			MyPodManager.GameLoop.ShouldClearChainIds.Add(sqr);
		}
	}

	public void TsAllConnectedBlocksAboveTick()
	{
		foreach (global::UnityEngine.Vector2 shouldTsConnectedBlocksPosition in MyPodManager.GameLoop.ShouldTsConnectedBlocksPositions)
		{
			TimestampAllConnectedBlocksAbove(shouldTsConnectedBlocksPosition);
		}
	}

	public void TimestampAllConnectedBlocksAbove(global::UnityEngine.Vector2 position, int bonusFrames = 0)
	{
		GarbageGroup garbageGroup = null;
		float y = position.y;
		for (int i = (int)y; i < 161; i++)
		{
			if (MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(position.x, i), out var sqr))
			{
				if (sqr.Type != BlockType.Ghost || i != (int)y)
				{
					if (sqr.Type == BlockType.Ghost || garbageGroup != null)
					{
						break;
					}
					if (sqr.IsGarbage && garbageGroup == null)
					{
						garbageGroup = sqr.GarbageGroup;
					}
					if (sqr.IsBeingDestroyed)
					{
						break;
					}
					if (sqr.IsSwapping)
					{
						sqr.AlteredFallTS = MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.HangingFrames;
						continue;
					}
					sqr.FallTS = MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.HangingFrames;
					sqr.LateSameTsInclusive = -100;
				}
			}
			else if (i != (int)y || bonusFrames != 0)
			{
				break;
			}
		}
	}

	public bool ShouldAnythingFall()
	{
		for (int i = 0; i < AliveSquares.Count; i++)
		{
			SquareComponent squareComponent = AliveSquares[i];
			if (!TryGetSquareAtTile(squareComponent.Coords + new global::UnityEngine.Vector2(0f, -1f), out var _) && GridPositions.Contains(squareComponent.Coords + new global::UnityEngine.Vector2(0f, -1f)) && (!squareComponent.IsGarbage || (squareComponent.IsGarbage && squareComponent.GarbageGroup.GetCanFall())))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsAnythingFalling()
	{
		for (int i = 0; i < AliveSquares.Count; i++)
		{
			SquareComponent squareComponent = AliveSquares[i];
			if (squareComponent.IsGarbage && squareComponent.GarbageGroup.GetCanFall())
			{
				return true;
			}
			if (squareComponent.IsFalling && !squareComponent.IsGarbage)
			{
				return true;
			}
		}
		return false;
	}

	public void SpawnPrePlacedBlocks(int rows = 5, int ditherAmount = 8)
	{
		int[] array = ComputeDitherDepths(ditherAmount, 6, 3);
		for (int i = 1; i < rows; i++)
		{
			int num = rows - 1 - i;
			global::System.Collections.Generic.List<int> list = null;
			for (int j = 0; j < 6; j++)
			{
				if (array[j] > num)
				{
					if (list == null)
					{
						list = new global::System.Collections.Generic.List<int>();
					}
					list.Add(i * 6 + j);
				}
			}
			SpawnNewRow(i * 6, active: true, list);
		}
	}

	private int[] ComputeDitherDepths(int ditherAmount, int width, int maxDepthPerColumn)
	{
		int[] array = new int[width];
		int max = width * maxDepthPerColumn;
		ditherAmount = global::UnityEngine.Mathf.Clamp(ditherAmount, 0, max);
		global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>(width);
		for (int i = 0; i < width; i++)
		{
			list.Add(i);
		}
		int num = ditherAmount;
		while (num > 0 && list.Count > 0)
		{
			int index = MyPodManager.RandomManager.Next(0, list.Count);
			int num2 = list[index];
			array[num2]++;
			num--;
			if (array[num2] >= maxDepthPerColumn)
			{
				list.RemoveAt(index);
			}
		}
		return array;
	}

	public void ReceiveGarbageInstruction(int width, int height, float ts)
	{
		GarbageQueue.Add(new GarbageInstruction(width, height, ts));
	}

	public void SpawnGarbageGroup(int width, int height, int xOffset = 0)
	{
		global::System.Collections.Generic.List<SquareComponent> list = new global::System.Collections.Generic.List<SquareComponent>();
		int num = -1;
		int num2 = 0;
		GarbageGroup component = global::UnityEngine.Object.Instantiate(m_garbageGroup).GetComponent<GarbageGroup>();
		GarbageGroup.currentMaxID++;
		component.ID = GarbageGroup.currentMaxID;
		component.LocalPodManager = MyPodManager;
		int num3 = 16;
		bool flag;
		do
		{
			flag = true;
			if (num3 >= 161)
			{
				global::UnityEngine.Debug.LogError("Reached top layer for gbg!");
				break;
			}
			for (int i = num3 + 1; i <= height + num3 + 1; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					if (TryGetSquareAtTile(new global::UnityEngine.Vector2(j, i), out var _))
					{
						flag = false;
						num3 = i;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
		}
		while (!flag);
		for (int k = num3 + 2; k < height + num3 + 2; k++)
		{
			for (int num4 = (k + 1) * 6 - 1 - xOffset; num4 >= (k + 1) * 6 - xOffset - width; num4--)
			{
				global::System.Collections.Generic.List<global::UnityEngine.GameObject> list2 = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();
				list2.AddRange(m_squareThings);
				global::System.Collections.Generic.List<int> list3 = new global::System.Collections.Generic.List<int>();
				if (num2 >= 1 && num != -1)
				{
					list3.Add(num);
				}
				for (int l = 0; l < list3.Count; l++)
				{
					for (int m = 0; m < list2.Count; m++)
					{
						if (list2[m].GetComponent<SquareComponent>().Type == (BlockType)list3[l])
						{
							list2.Remove(list2[m]);
						}
					}
				}
				int index = MyPodManager.RandomManager.Next(0, list2.Count);
				if (num == (int)list2[index].GetComponent<SquareComponent>().Type)
				{
					num2++;
				}
				num = (int)list2[index].GetComponent<SquareComponent>().Type;
				SquareComponent component2 = global::UnityEngine.Object.Instantiate(list2[index], new global::UnityEngine.Vector3(0f, -100f, 0f), global::UnityEngine.Quaternion.identity, m_blockHolder).transform.GetComponent<SquareComponent>();
				component2.MyPodManager = MyPodManager;
				AliveSquares.Add(component2);
				list.Add(component2);
				component2.Coords = GridPositions[num4];
				component2.IsGarbage = true;
				component2.FallTS = MyPodManager.GameLoop.GameLoopFrameCounter;
				component2.WillFall = true;
				component2.GarbageGroup = component;
				component.width = width;
				component2.IsMatchable = true;
				component.Blocks.Add(component2);
			}
			component.CanFall = true;
		}
		component.GenerateGarbageShapeSprites(0, effectiveImmediately: true);
	}

	public void SpawnNewRow(int heightOffset = 0, bool active = false, global::System.Collections.Generic.List<int> excludedCols = null)
	{
		global::System.Collections.Generic.List<SquareComponent> list = new global::System.Collections.Generic.List<SquareComponent>();
		int num = -1;
		int num2 = 0;
		for (int i = heightOffset; i < 6 + heightOffset; i++)
		{
			if (excludedCols != null && excludedCols.Contains(i))
			{
				continue;
			}
			global::System.Collections.Generic.List<global::UnityEngine.GameObject> list2 = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();
			list2.AddRange(m_squareThings);
			global::System.Collections.Generic.List<int> list3 = new global::System.Collections.Generic.List<int>();
			if (num2 >= 1 && num != -1)
			{
				list3.Add(num);
			}
			SquareComponent sqr2;
			SquareComponent sqr3;
			if (active)
			{
				if (TryGetSquareAtTile(GridPositions[i] + global::UnityEngine.Vector2.down + global::UnityEngine.Vector2.down, out var sqr))
				{
					list3.Add((int)sqr.Type);
				}
			}
			else if (TryGetSquareAtTile(GridPositions[i], out sqr2) && TryGetSquareAtTile(GridPositions[i] + new global::UnityEngine.Vector2(0f, 1f), out sqr3) && sqr2.Type == sqr3.Type)
			{
				list3.Add((int)sqr2.Type);
			}
			for (int j = 0; j < list3.Count; j++)
			{
				for (int k = 0; k < list2.Count; k++)
				{
					if (list2[k].GetComponent<SquareComponent>().Type == (BlockType)list3[j])
					{
						list2.Remove(list2[k]);
					}
				}
			}
			int index = MyPodManager.RandomManager.Next(0, list2.Count);
			if (num == (int)list2[index].GetComponent<SquareComponent>().Type)
			{
				num2++;
			}
			num = (int)list2[index].GetComponent<SquareComponent>().Type;
			SquareComponent component = global::UnityEngine.Object.Instantiate(list2[index], new global::UnityEngine.Vector3(0f, -100f, 0f), global::UnityEngine.Quaternion.identity, m_blockHolder).transform.GetComponent<SquareComponent>();
			component.MyPodManager = MyPodManager;
			AliveSquares.Add(component);
			list.Add(component);
			if (active)
			{
				component.Coords = GridPositions[i] - new global::UnityEngine.Vector2(0f, 1f);
			}
			else
			{
				component.Coords = GridPositions[i] - new global::UnityEngine.Vector2(0f, 1f);
			}
			if (active)
			{
				component.IsMatchable = true;
			}
		}
	}

	public bool IsMoveValid(global::UnityEngine.Vector2 first, global::UnityEngine.Vector2 second)
	{
		if (GridPositions.Contains(first) && GridPositions.Contains(second))
		{
			return true;
		}
		return false;
	}

	public void BakeGrid()
	{
		for (int i = 0; i < 161; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3(j, i, 0f);
				GridPositions.Add(vector);
				global::UnityEngine.Object.Instantiate(m_gridVisual, base.transform).transform.position = vector;
			}
		}
	}

	private void OnDrawGizmos()
	{
		global::UnityEngine.Gizmos.color = new global::UnityEngine.Color(255f, 255f, 255f, 0.2f);
		for (int i = 0; i < 161; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				global::UnityEngine.Vector3 vector = new global::UnityEngine.Vector3((float)j + base.transform.position.x, (float)i + base.transform.position.y, 0f);
				global::UnityEngine.Gizmos.DrawCube(new global::UnityEngine.Vector3(vector.x, vector.y, 0f), new global::UnityEngine.Vector3(0.8f, 0.8f));
			}
		}
	}
}
