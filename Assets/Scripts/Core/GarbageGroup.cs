public class GarbageGroup : global::UnityEngine.MonoBehaviour
{
	public static int currentMaxID;

	public global::System.Collections.Generic.List<SquareComponent> Blocks = new global::System.Collections.Generic.List<SquareComponent>();

	public int ID;

	public int width;

	public bool IsRevealing;

	public int revealChainID;

	public bool CanFall = true;

	private float m_cachedTS;

	public PodManager LocalPodManager;

	public global::UnityEngine.Vector2 LowestRightmostCoord => GetLowestRightMostCoord();

	private global::UnityEngine.Vector2 GetLowestRightMostCoord()
	{
		return Blocks[0].Coords;
	}

	public void SetSeparatedRevealSprite()
	{
		foreach (SquareComponent block in Blocks)
		{
			block.RevealSprite(shouldReveal: false);
		}
	}

	public void FlashBlocks()
	{
		for (int i = 0; i < Blocks.Count; i++)
		{
			Blocks[i].FlashSingleGarbage(6);
		}
	}

	public void GenerateGarbageShapeSprites(int startIndex, bool effectiveImmediately = false)
	{
		if (Blocks.Count <= startIndex)
		{
			return;
		}
		GarbageTiles garbageBlocks = Blocks[0].SpriteData.GarbageBlocks;
		if (Blocks.Count - startIndex <= 6)
		{
			for (int i = startIndex; i < Blocks.Count; i++)
			{
				Blocks[i].CachedGarbageShape = garbageBlocks.Tube;
			}
			Blocks[startIndex].CachedGarbageShape = garbageBlocks.TubeRight;
			Blocks[Blocks.Count - 1].CachedGarbageShape = garbageBlocks.TubeLeft;
		}
		else
		{
			for (int j = startIndex; j < Blocks.Count; j++)
			{
				if (j == startIndex)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreBottomRight;
				}
				else if (j == startIndex + 6 - 1)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreBottomLeft;
				}
				else if (j == Blocks.Count - 1)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreTopLeft;
				}
				else if (j == Blocks.Count - 6)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreTopRight;
				}
				else if (j % 6 == 0)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreRight;
				}
				else if (j % 6 == 5)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreLeft;
				}
				else if (j > startIndex && j < startIndex + 6 - 1)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreBottom;
				}
				else if (j > Blocks.Count - 6 && j < Blocks.Count - 1)
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.CoreTop;
				}
				else
				{
					Blocks[j].CachedGarbageShape = garbageBlocks.Core;
				}
			}
		}
		if (effectiveImmediately)
		{
			for (int k = startIndex; k < Blocks.Count; k++)
			{
				Blocks[k].RevealSprite(shouldReveal: false);
			}
		}
	}

	public bool HasAnyBlocksBelow()
	{
		for (int i = 0; i < global::UnityEngine.Mathf.Min(Blocks.Count, 6); i++)
		{
			if (!Blocks[i].MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Blocks[i].Coords.x, Blocks[i].Coords.y - 1f), out var sqr))
			{
				continue;
			}
			if (sqr.GarbageGroup != null)
			{
				if (!sqr.GarbageGroup.GetCanFall())
				{
					return true;
				}
			}
			else if ((float)sqr.FallTS < 0f)
			{
				return true;
			}
		}
		return false;
	}

	public bool GetCanFall()
	{
		if (IsRevealing)
		{
			return false;
		}
		return CanFall;
	}

	public bool CanAndUpdateFalling()
	{
		return true;
	}

	private void MirrorGarbageTS(int maxTS)
	{
		if (m_cachedTS == (float)maxTS)
		{
			return;
		}
		for (int i = 0; i < Blocks.Count; i++)
		{
			Blocks[i].FallTS = maxTS;
			if (Blocks.Count - i <= 6)
			{
				Blocks[i].GiveAllBlocksAboveSameTsInclusive(maxTS, Blocks[i].Coords + global::UnityEngine.Vector2.up);
			}
		}
		m_cachedTS = maxTS;
	}

	public bool FindConnectedGbgs(out global::System.Collections.Generic.List<GarbageGroup> gbgs, global::System.Collections.Generic.List<GarbageGroup> inGbgs = null)
	{
		gbgs = new global::System.Collections.Generic.List<GarbageGroup>();
		if (inGbgs != null)
		{
			gbgs.AddRange(inGbgs);
		}
		for (int i = 0; i < Blocks.Count; i++)
		{
			if (i < 6 && Blocks[i].MyPodManager.GridManager.TryGetSquareAtTile(Blocks[i].Coords + global::UnityEngine.Vector2.down, out var sqr) && sqr.IsGarbage && !sqr.GarbageGroup.IsRevealing && !gbgs.Contains(sqr.GarbageGroup))
			{
				gbgs.Add(sqr.GarbageGroup);
				sqr.GarbageGroup.FindConnectedGbgs(out var gbgs2, gbgs);
				gbgs.AddRange(gbgs2);
			}
			if (Blocks.Count - i < 6 && Blocks[i].Coords.y + 1f <= 11f && Blocks[i].MyPodManager.GridManager.TryGetSquareAtTile(Blocks[i].Coords + global::UnityEngine.Vector2.up, out var sqr2) && sqr2.IsGarbage && !sqr2.GarbageGroup.IsRevealing && !gbgs.Contains(sqr2.GarbageGroup))
			{
				gbgs.Add(sqr2.GarbageGroup);
				sqr2.GarbageGroup.FindConnectedGbgs(out var gbgs3, gbgs);
				gbgs.AddRange(gbgs3);
			}
			if (i == 0 && Blocks.Count < 6 && Blocks[i].MyPodManager.GridManager.TryGetSquareAtTile(Blocks[i].Coords + global::UnityEngine.Vector2.right, out var sqr3) && sqr3.IsGarbage && !sqr3.GarbageGroup.IsRevealing && !gbgs.Contains(sqr3.GarbageGroup))
			{
				gbgs.Add(sqr3.GarbageGroup);
				sqr3.GarbageGroup.FindConnectedGbgs(out var gbgs4, gbgs);
				gbgs.AddRange(gbgs4);
			}
			if (i == Blocks.Count - 1 && Blocks.Count < 6 && Blocks[i].MyPodManager.GridManager.TryGetSquareAtTile(Blocks[i].Coords + global::UnityEngine.Vector2.left, out var sqr4) && sqr4.IsGarbage && !sqr4.GarbageGroup.IsRevealing && !gbgs.Contains(sqr4.GarbageGroup))
			{
				gbgs.Add(sqr4.GarbageGroup);
				sqr4.GarbageGroup.FindConnectedGbgs(out var gbgs5, gbgs);
				gbgs.AddRange(gbgs5);
			}
		}
		return gbgs.Count != 0;
	}
}
