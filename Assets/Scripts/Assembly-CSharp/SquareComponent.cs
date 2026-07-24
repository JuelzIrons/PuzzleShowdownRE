public class SquareComponent : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	public BlockType Type;

	public bool IsMatchable;

	public bool ShouldCheckForMatches;

	public bool ShouldBeDestroyed;

	public bool IsBeingDestroyed;

	public bool IsGarbage;

	public bool HideGbgOverlay;

	public GarbageGroup GarbageGroup;

	public global::UnityEngine.SpriteRenderer GarbageOverlaySR;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Sprite[] m_garbageOverlaySprites;

	public int ChainID;

	public bool ChainIDValidated;

	public bool ShouldDance;

	public global::UnityEngine.Sprite CachedGarbageShape;

	private global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> m_tweenX;

	private global::DG.Tweening.Core.TweenerCore<global::UnityEngine.Vector3, global::UnityEngine.Vector3, global::DG.Tweening.Plugins.Options.VectorOptions> m_tweenY;

	public bool IsSideMoving;

	public int FallTS = -1;

	public int AlteredFallTS = -1;

	public int IndividualRevealTs = -1;

	public int TotalGBGRevealTs = -1;

	public bool FakeReveal;

	public int GarbageRevealIndex;

	public int TotalGBGChainID = -1;

	public int TempChainIDFrames = -1;

	private bool m_tempWasFalling;

	public global::System.Collections.Generic.List<GarbageGroup> ToBeDestroyedGbgs = new global::System.Collections.Generic.List<GarbageGroup>();

	public global::UnityEngine.AudioClip PopClip;

	public global::UnityEngine.AudioClip DropClip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color m_unmatchableSpriteClr;

	public SpriteDataSO SpriteData;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.SpriteRenderer m_sr;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animator m_anim;

	public global::UnityEngine.Vector2 Coords;

	private global::UnityEngine.Vector2 PrevCoords;

	public bool IsLanding;

	public bool IsSwapping;

	private int swapTs;

	private int otherBlockChainID;

	private global::UnityEngine.Vector2 coordSwap;

	private SquareComponent otherBlock;

	private SquareComponent ghost;

	public int GhostDissapearTs = -1;

	private bool m_hasRisenWhileSwapping;

	private int blockDestroyTs = -1;

	private int tempChainID = -1;

	private global::DG.Tweening.Tweener m_swapTween;

	public bool WillFall;

	public bool WillLand;

	public int LateSameTsInclusive = -100;

	private bool hangingOnSwap;

	public bool CanBeSwapped
	{
		get
		{
			if (Type != BlockType.Ghost)
			{
				return MyPodManager.GameLoop.GameLoopFrameCounter >= FallTS + 1;
			}
			return false;
		}
	}

	public bool IsFalling => FallTS > 0;

	public global::UnityEngine.Vector2 CoordSwap => coordSwap;

	public SquareComponent OtherBlock => otherBlock;

	private void Start()
	{
		blockDestroyTs = -1;
		IndividualRevealTs = -1;
		TotalGBGRevealTs = -1;
		FakeReveal = false;
		TempChainIDFrames = -1;
		CachedGarbageShape = SpriteData.GetSprite(SpriteType.None, this);
		MyPodManager.GameLoop.Rise.AddListener(Rise);
		if (!MyPodManager.GridManager.AliveSquares.Contains(this))
		{
			MyPodManager.GridManager.AliveSquares.Add(this);
		}
	}

	public void ClearGarbageStatus()
	{
		IsGarbage = false;
		SetBlockSprite(SpriteData.GetSprite(SpriteType.Default, this));
	}

	public void RevealSprite(bool shouldReveal)
	{
		if (!(m_sr == null))
		{
			if (!shouldReveal)
			{
				HideGbgOverlay = false;
			}
			SetBlockSprite(SpriteData.GetSprite(SpriteType.Default, this, 0, shouldReveal));
		}
	}

	public void FlashSingleGarbage(int flashCount)
	{
		HideGbgOverlay = true;
		StartCoroutine(FlashSingleGarbageAnim(flashCount));
	}

	private global::System.Collections.IEnumerator FlashSingleGarbageAnim(int flashes)
	{
		for (int i = 0; i < flashes; i++)
		{
			SetBlockSprite(SpriteData.GetSprite(SpriteType.SingleGarbage, this));
			yield return new global::UnityEngine.WaitForSeconds(0.03f);
			SetBlockSprite(SpriteData.GetSprite(SpriteType.FlashingGarbage, this));
			yield return new global::UnityEngine.WaitForSeconds(0.03f);
		}
		SetBlockSprite(SpriteData.GetSprite(SpriteType.SingleGarbage, this));
	}

	private void OnDisable()
	{
		MyPodManager.GameLoop.Rise.RemoveListener(Rise);
	}

	public void CheckGarbageGroupFall()
	{
		if (GarbageGroup != null)
		{
			GarbageGroup.CanAndUpdateFalling();
		}
	}

	public void GoToCoord(global::UnityEngine.Vector2 coord, bool SwapTweened = false, int otherBlockChainID = -1, SquareComponent otherBlock = null)
	{
		if (IsSwapping && SwapTweened)
		{
			if (!(coordSwap == MyPodManager.CursorController.coords[0]) && !(coordSwap == MyPodManager.CursorController.coords[1]))
			{
				if (!(coordSwap != MyPodManager.CursorController.coords[0]) || !(coordSwap != MyPodManager.CursorController.coords[1]))
				{
					return;
				}
				if (m_swapTween != null)
				{
					global::DG.Tweening.TweenExtensions.Kill(m_swapTween);
				}
				FinishSwap();
				if (!(this.otherBlock == null) && (this.otherBlock.coordSwap == MyPodManager.CursorController.coords[0] || this.otherBlock.coordSwap == MyPodManager.CursorController.coords[1]))
				{
					this.otherBlock.FinishSwap();
					if (otherBlock != null)
					{
						otherBlock.otherBlock = this.otherBlock;
					}
					this.otherBlock.IsSwapping = false;
					this.otherBlock.GoToCoord(coord, SwapTweened: true, otherBlockChainID, otherBlock);
				}
				return;
			}
			coord = Coords;
		}
		m_hasRisenWhileSwapping = false;
		PrevCoords = Coords;
		if (FallTS >= 0)
		{
			m_tempWasFalling = true;
		}
		else
		{
			m_tempWasFalling = false;
		}
		FallTS = -1;
		AlteredFallTS = -1;
		GiveAllBlocksAboveSameTsInclusive(-1, Coords);
		if (SwapTweened)
		{
			if (otherBlockChainID != ChainID && ChainID != 0)
			{
				TempChainIDFrames = 5;
			}
			if (otherBlockChainID != -1)
			{
				ghost = null;
			}
			else
			{
				ghost = global::UnityEngine.Object.Instantiate(MyPodManager.GridManager.GhostPrefab).GetComponent<SquareComponent>();
				ghost.MyPodManager = MyPodManager;
				ghost.Coords = coord;
				ghost.IsSwapping = true;
				ghost.IsMatchable = false;
				ghost.IsBeingDestroyed = true;
				ghost.ShouldCheckForMatches = false;
				ghost.GhostDissapearTs = MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.SwappingFrames + 1;
				ghost.ShouldDance = false;
				ghost.FallTS = MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.HangingFrames + MyPodManager.GameLoop.SwappingFrames;
				ghost.transform.SetParent(MyPodManager.GridManager.m_blockHolder);
				MyPodManager.GridManager.AliveSquares.Add(ghost);
			}
			IsSwapping = true;
			m_swapTween = global::DG.Tweening.ShortcutExtensions.DOLocalMoveX(base.transform, coord.x, 0.066f);
			global::DG.Tweening.TweenSettingsExtensions.SetEase(m_swapTween, global::DG.Tweening.Ease.Linear);
			global::DG.Tweening.TweenSettingsExtensions.SetUpdate(m_swapTween, global::DG.Tweening.UpdateType.Fixed);
			swapTs = MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.SwappingFrames;
			this.otherBlockChainID = otherBlockChainID;
			coordSwap = coord;
			this.otherBlock = otherBlock;
		}
		else
		{
			Coords = coord;
			ShouldCheckForMatches = true;
		}
	}

	public void FinishSwap()
	{
		if (m_swapTween != null)
		{
			global::DG.Tweening.TweenExtensions.Complete(m_swapTween);
		}
		if (otherBlockChainID == -1)
		{
			MyPodManager.GridManager.AliveSquares.Remove(ghost);
			global::UnityEngine.Object.Destroy(ghost.gameObject);
		}
		ShouldCheckForMatches = true;
		Coords = (m_hasRisenWhileSwapping ? (coordSwap + new global::UnityEngine.Vector2(0f, 1f)) : coordSwap);
		PrevCoords = (m_hasRisenWhileSwapping ? (PrevCoords + new global::UnityEngine.Vector2(0f, 1f)) : PrevCoords);
		if (otherBlock != null && otherBlock.AlteredFallTS >= 0)
		{
			FallTS = otherBlock.AlteredFallTS;
		}
		IsSwapping = false;
		TryApplyFallTimestamp();
		if (otherBlockChainID == -1 && otherBlock == null)
		{
			MyPodManager.GridManager.TimestampAllConnectedBlocksAbove(PrevCoords + new global::UnityEngine.Vector2(0f, 1f), 243754);
		}
		if (MyPodManager.GameLoop.shouldKill)
		{
			SetDownSqueeze(isSqueeze: true);
		}
	}

	public void SwapFixedUpdate()
	{
		if (IsSwapping && Type != BlockType.Ghost && MyPodManager.GameLoop.GameLoopFrameCounter >= swapTs)
		{
			FinishSwap();
		}
	}

	public bool TryApplyFallTimestamp()
	{
		GiveAllBlocksAboveSameTsInclusive(FallTS, Coords);
		if (MyPodManager.GridManager.TryGetSquareAtTile(Coords - new global::UnityEngine.Vector2(0f, 1f), out var sqr) && (sqr.FallTS <= 0 || sqr.FallTS != FallTS))
		{
			if ((sqr.FallTS != FallTS && sqr.FallTS >= 0 && !sqr.IsGarbage) || (sqr.IsGarbage && sqr.GarbageGroup.GetCanFall()))
			{
				GiveAllBlocksAboveSameTsInclusive(sqr.FallTS, sqr.Coords);
			}
			else if (FallTS > 0 || m_tempWasFalling)
			{
				StartCoroutine(SquareLandRoutine());
				ShouldCheckForMatches = true;
				MyPodManager.GameLoop.ShouldClearChainIds.Add(this);
				FallTS = -1;
				GiveAllBlocksAboveSameTsInclusive(FallTS, Coords);
			}
			return false;
		}
		MyPodManager.GridManager.TimestampAllConnectedBlocksAbove(Coords);
		return true;
	}

	public void UpdateWillFall()
	{
		WillFall = false;
		WillLand = false;
		hangingOnSwap = false;
		LateSameTsInclusive = -100;
		if (FallTS == -1 && !MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, Coords.y - 1f), out var _) && IsGarbage)
		{
			LateSameTsInclusive = MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.HangingFrames;
		}
		else if (FallTS > 0 && !IsBeingDestroyed && !IsSwapping && MyPodManager.GameLoop.GameLoopFrameCounter >= FallTS)
		{
			SquareComponent sqr3;
			if (MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, Coords.y - 1f), out var sqr2) && sqr2.IsSwapping)
			{
				hangingOnSwap = true;
				sqr2.LateSameTsInclusive = sqr2.FallTS;
			}
			else if (MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, Coords.y - 1f), out sqr3) && sqr3.hangingOnSwap)
			{
				hangingOnSwap = true;
			}
			else
			{
				WillFall = true;
			}
		}
	}

	public bool CanClosestGBGUnderMeFall()
	{
		int num = (int)Coords.y - 1;
		while (num > 0)
		{
			if (MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, num), out var sqr))
			{
				if (sqr.IsGarbage)
				{
					return sqr.GarbageGroup.CanFall;
				}
				num--;
				continue;
			}
			return true;
		}
		return true;
	}

	public void GarbyCheck2()
	{
		if (!IsGarbage || !(GarbageGroup != null))
		{
			return;
		}
		if (GarbageGroup.IsRevealing)
		{
			WillFall = false;
			return;
		}
		bool flag = true;
		for (int i = 0; i < global::UnityEngine.Mathf.Min(GarbageGroup.Blocks.Count, 6); i++)
		{
			if (!GarbageGroup.Blocks[i].WillFall)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			WillFall = false;
			GarbageGroup.CanFall = false;
			return;
		}
		GarbageGroup.CanFall = true;
		SquareComponent sqr;
		for (int j = (int)Coords.y + 1; j < 161 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, j), out sqr); j++)
		{
			if (sqr.IsSwapping)
			{
				break;
			}
			if (sqr.IsBeingDestroyed)
			{
				break;
			}
			sqr.WillFall = true;
			if (sqr.IsGarbage)
			{
				break;
			}
		}
	}

	public void GarbyCheck3()
	{
		if ((!WillFall && !IsGarbage) || CanClosestGBGUnderMeFall())
		{
			return;
		}
		if (IsGarbage)
		{
			for (int i = 0; i < global::UnityEngine.Mathf.Min(GarbageGroup.Blocks.Count, 6); i++)
			{
				if (GarbageGroup.Blocks[i].WillFall)
				{
					GarbageGroup.Blocks[i].WillLand = true;
				}
				GarbageGroup.Blocks[i].WillFall = false;
			}
			GarbageGroup.CanFall = false;
		}
		else
		{
			WillFall = false;
			WillLand = true;
		}
	}

	public void Fall()
	{
		if (!WillFall || WillLand)
		{
			return;
		}
		MoveBlockToCoord(Coords - new global::UnityEngine.Vector2(0f, 1f));
		if (Coords.y < -4f)
		{
			ShouldBeDestroyed = true;
			blockDestroyTs = MyPodManager.GameLoop.GameLoopFrameCounter + 1;
			if (ChainID != 0)
			{
				MyPodManager.GameLoop.SubtractChainBlockCount(ChainID);
			}
			global::UnityEngine.Debug.LogError("Zombie block detected, removing it!");
		}
		ChainIDValidated = true;
		SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this, 1));
		if (!MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, Coords.y - 1f), out var sqr) || (sqr.FallTS > 0 && sqr.FallTS == FallTS))
		{
			return;
		}
		if (sqr.Type == BlockType.Ghost && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(sqr.Coords.x, sqr.Coords.y - 1f), out var sqr2) && (sqr2.FallTS == -1 || sqr2.IsGarbage))
		{
			global::UnityEngine.Debug.Log("LANDED ON GHOST!");
			TempChainIDFrames = sqr.GhostDissapearTs - MyPodManager.GameLoop.GameLoopFrameCounter + 1;
			WillLand = true;
		}
		else if ((sqr.FallTS != FallTS || IsGarbage) && sqr.FallTS > 0)
		{
			if (sqr.IsGarbage && !sqr.GarbageGroup.CanFall)
			{
				WillLand = true;
			}
			else
			{
				sqr.LateSameTsInclusive = sqr.FallTS;
			}
		}
		else
		{
			WillLand = true;
		}
	}

	public void SetLateFallTsAbove()
	{
		if (LateSameTsInclusive != -100)
		{
			GiveAllBlocksAboveSameTsInclusive(LateSameTsInclusive, Coords);
		}
	}

	public void SetLateLand()
	{
		if (WillLand)
		{
			MyPodManager.GameLoop.ShouldLandAllPositions.Add(Coords);
		}
	}

	public void SetDownSqueeze(bool isSqueeze = false)
	{
		if (!isSqueeze)
		{
			SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this));
		}
		else
		{
			SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this, 2));
		}
	}

	public void GiveAllBlocksAboveSameTsInclusive(int ts, global::UnityEngine.Vector2 coords)
	{
		float y = coords.y;
		bool flag = false;
		SquareComponent sqr;
		for (int i = (int)y; i < 161 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(coords.x, i), out sqr); i++)
		{
			if (sqr.IsBeingDestroyed && i != (int)y)
			{
				flag = true;
			}
			if (sqr.IsGarbage && sqr.FallTS != -1 && ts == -1)
			{
				MyPodManager.GameLoop.ShouldClearChainIds.Add(sqr);
			}
			if (!flag)
			{
				sqr.FallTS = ts;
				sqr.LateSameTsInclusive = -100;
			}
			if (sqr.IsGarbage)
			{
				break;
			}
		}
	}

	public void MoveBlockToCoord(global::UnityEngine.Vector2 goToCoord)
	{
		PrevCoords = Coords;
		Coords = goToCoord;
	}

	public void Rise()
	{
		m_hasRisenWhileSwapping = true;
		MoveBlockToCoord(Coords + new global::UnityEngine.Vector2(0f, 1f));
		IsMatchable = true;
		ShouldCheckForMatches = true;
		if (Coords.y >= 10f)
		{
			MyPodManager.GameLoop.m_isForcingUp = false;
		}
		else
		{
			MyPodManager.GameLoop.CheckTopLayer();
		}
	}

	public global::System.Collections.IEnumerator GiveTempChainID(int tempChainID)
	{
		int chainID = ChainID;
		if (chainID != tempChainID)
		{
			if (chainID != 0)
			{
				MyPodManager.GameLoop.SlottedChainBlockCounter[chainID]--;
			}
			ChainID = tempChainID;
			MyPodManager.GameLoop.SlottedChainBlockCounter[tempChainID]++;
			yield return new global::UnityEngine.WaitForFixedUpdate();
		}
	}

	public void ClearTempChainID()
	{
		if (ChainID == tempChainID)
		{
			MyPodManager.GameLoop.SlottedChainBlockCounter[tempChainID]--;
			ChainID = 0;
		}
	}

	private void UseOffset()
	{
		if (IsSwapping)
		{
			base.transform.localPosition = new global::UnityEngine.Vector2(base.transform.localPosition.x, Coords.y) + new global::UnityEngine.Vector2(0f, MyPodManager.GameLoop.OffsetValue);
		}
		else
		{
			base.transform.localPosition = Coords + new global::UnityEngine.Vector2(0f, MyPodManager.GameLoop.OffsetValue);
		}
	}

	public void PopBlock()
	{
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroyedSprite, this));
	}

	public void LoseBlock()
	{
		SetBlockSprite(SpriteData.GetSprite(SpriteType.FrozenSprite, this));
	}

	public global::System.Collections.IEnumerator SquareLandRoutine()
	{
		IsLanding = true;
		MyPodManager.AudioManager.PlaySfx(DropClip, 0.2f, antiOverlap: true, 1f, 0.15f);
		yield return new global::UnityEngine.WaitForSeconds(0.033f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this, 2));
		yield return new global::UnityEngine.WaitForSeconds(0.066f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this));
		yield return new global::UnityEngine.WaitForSeconds(0.033f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this, 1));
		yield return new global::UnityEngine.WaitForSeconds(0.033f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.LandSprites, this, 3));
		IsLanding = false;
		if (MyPodManager.GameLoop.shouldKill)
		{
			SetDownSqueeze(isSqueeze: true);
		}
	}

	public bool CheckForHorizontalMatches(out global::System.Collections.Generic.List<SquareComponent> horizontalMatchingBlocks)
	{
		horizontalMatchingBlocks = new global::System.Collections.Generic.List<SquareComponent> { this };
		SquareComponent sqr;
		for (int i = (int)Coords.x + 1; i < (int)Coords.x + 3 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(i, Coords.y), out sqr, falseOnFalling: false, falseOnSwappingAndFalling: false, falseOnSwapping: true); i++)
		{
			if (sqr.IsGarbage)
			{
				break;
			}
			if (sqr.Type != Type)
			{
				break;
			}
			if (sqr.ShouldBeDestroyed)
			{
				break;
			}
			if (sqr.IsFalling)
			{
				break;
			}
			if (!sqr.IsMatchable)
			{
				break;
			}
			if (sqr.IsSwapping)
			{
				break;
			}
			horizontalMatchingBlocks.Add(sqr);
		}
		int num = (int)Coords.x - 1;
		SquareComponent sqr2;
		while (num > (int)Coords.x - 3 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(num, Coords.y), out sqr2, falseOnFalling: false, falseOnSwappingAndFalling: false, falseOnSwapping: true) && !sqr2.IsGarbage && sqr2.Type == Type && !sqr2.ShouldBeDestroyed && !sqr2.IsFalling && sqr2.IsMatchable && !sqr2.IsSwapping)
		{
			horizontalMatchingBlocks.Add(sqr2);
			num--;
		}
		horizontalMatchingBlocks.Sort((SquareComponent a, SquareComponent b) => a.Coords.x.CompareTo(b.Coords.x));
		if (horizontalMatchingBlocks.Count < 3)
		{
			horizontalMatchingBlocks.Clear();
			return false;
		}
		return true;
	}

	public bool CheckForVerticalMatches(out global::System.Collections.Generic.List<SquareComponent> verticalMatchingBlocks)
	{
		verticalMatchingBlocks = new global::System.Collections.Generic.List<SquareComponent> { this };
		SquareComponent sqr;
		for (int i = (int)Coords.y + 1; i < 12 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, i), out sqr, falseOnFalling: false, falseOnSwappingAndFalling: false, falseOnSwapping: true); i++)
		{
			if (sqr.IsGarbage)
			{
				break;
			}
			if (sqr.Type != Type)
			{
				break;
			}
			if (sqr.ShouldBeDestroyed)
			{
				break;
			}
			if (sqr.IsFalling)
			{
				break;
			}
			if (!sqr.IsMatchable)
			{
				break;
			}
			verticalMatchingBlocks.Add(sqr);
		}
		int num = (int)Coords.y - 1;
		SquareComponent sqr2;
		while (num > -12 && MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, num), out sqr2, falseOnFalling: false, falseOnSwappingAndFalling: false, falseOnSwapping: true) && !sqr2.IsGarbage && sqr2.Type == Type && !sqr2.ShouldBeDestroyed && !sqr2.IsFalling && sqr2.IsMatchable)
		{
			verticalMatchingBlocks.Add(sqr2);
			num--;
		}
		verticalMatchingBlocks.Sort((SquareComponent a, SquareComponent b) => a.Coords.y.CompareTo(b.Coords.y));
		if (verticalMatchingBlocks.Count < 3)
		{
			verticalMatchingBlocks.Clear();
			return false;
		}
		return true;
	}

	public void SetDance(bool shouldDance)
	{
		if (!shouldDance && ShouldDance && !IsGarbage)
		{
			SetBlockSprite(SpriteData.GetSprite(SpriteType.DanceSprites, this, 1));
		}
		ShouldDance = shouldDance;
	}

	public void FixedTimerManager()
	{
		FixedLoc();
		SwapFixedUpdate();
		if (blockDestroyTs != -1 && MyPodManager.GameLoop.GameLoopFrameCounter == blockDestroyTs)
		{
			DestroyBlock();
		}
		if (IndividualRevealTs != -1 && MyPodManager.GameLoop.GameLoopFrameCounter == IndividualRevealTs)
		{
			HideGbgOverlay = true;
			GBGRevealInvocation();
			IndividualRevealTs = -1;
		}
		if (TotalGBGRevealTs != -1 && MyPodManager.GameLoop.GameLoopFrameCounter == TotalGBGRevealTs)
		{
			LastGbgInvocation();
			TotalGBGRevealTs = -1;
		}
		if (TempChainIDFrames > 0)
		{
			TempChainIDFrames--;
			if (TempChainIDFrames == 0)
			{
				TempChainIDFrames = -1;
				MyPodManager.GameLoop.ShouldClearChainIds.Add(this);
			}
		}
	}

	private void GBGRevealInvocation()
	{
		if (FakeReveal)
		{
			GarbageGroup.revealChainID = 0;
			RevealSprite(shouldReveal: false);
		}
		else
		{
			RevealSprite(shouldReveal: true);
		}
		if (Coords.y < 11f)
		{
			global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(MyPodManager.GameLoop.m_starSparkle[1]);
			obj.transform.SetParent(MyPodManager.GridManager.transform);
			global::UnityEngine.ParticleSystem.MainModule main = obj.GetComponent<global::UnityEngine.ParticleSystem>().main;
			main.startDelay = 0f;
			obj.transform.localPosition = Coords + new global::UnityEngine.Vector2(0f, MyPodManager.GameLoop.OffsetValue);
			MyPodManager.AudioManager.PlaySfx(PopClip, 1f, antiOverlap: true, global::UnityEngine.Mathf.Clamp(1f + (float)GarbageRevealIndex * 0.25f, 1f, 3f));
		}
	}

	private void LastGbgInvocation()
	{
		if (FakeReveal)
		{
			HideGbgOverlay = false;
			GarbageGroup.revealChainID = 0;
			return;
		}
		HideGbgOverlay = true;
		GarbageGroup.revealChainID = 0;
		GarbageGroup.IsRevealing = false;
		GarbageGroup.Blocks.Remove(this);
		GarbageGroup = null;
		ClearGarbageStatus();
		ShouldCheckForMatches = true;
		if (FallTS > 0)
		{
			global::UnityEngine.Debug.Log($"Assigned chain ID {TotalGBGChainID} to falling block");
			GiveAllBlocksAboveSameTsInclusive(MyPodManager.GameLoop.GameLoopFrameCounter + MyPodManager.GameLoop.HangingFrames, Coords);
			ChainID = TotalGBGChainID;
			MyPodManager.GameLoop.SlottedChainBlockCounter[TotalGBGChainID]++;
		}
		if (ChainID == 0)
		{
			ChainID = TotalGBGChainID;
			MyPodManager.GameLoop.SlottedChainBlockCounter[TotalGBGChainID]++;
			TempChainIDFrames = 1;
		}
		MyPodManager.GameLoop.SubtractChainBlockCount(ChainID);
	}

	public void TryDestroyBlock(int combolifetime, int index, int chainNum = 0)
	{
		if (ShouldBeDestroyed && !IsBeingDestroyed && Type != BlockType.Ghost)
		{
			FallTS = -1;
			IsBeingDestroyed = true;
			StartCoroutine(MatchSpriteRoutine(index, chainNum));
			m_sr.color = global::UnityEngine.Color.white;
			blockDestroyTs = MyPodManager.GameLoop.GameLoopFrameCounter + combolifetime;
		}
	}

	private void DestroyBlock()
	{
		MyPodManager.GameLoop.ShouldTsConnectedBlocksPositions.Add(Coords);
		MyPodManager.GridManager.AliveSquares.Remove(this);
		SetBlockRowAboveChainID();
		if (ChainID != 0)
		{
			MyPodManager.GameLoop.SubtractChainBlockCount(ChainID);
		}
		MyPodManager.GameLoop.Score += 10;
		blockDestroyTs = -1;
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private global::System.Collections.IEnumerator MatchSpriteRoutine(int index, int chainNum)
	{
		float totalOffsetTime = 0.2f * (float)index;
		for (int i = 0; i < 5; i++)
		{
			SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this));
			yield return new global::UnityEngine.WaitForSeconds(0.033f);
			SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 1));
			yield return new global::UnityEngine.WaitForSeconds(0.033f);
			SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 3));
			yield return new global::UnityEngine.WaitForSeconds(0.066f);
		}
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 4));
		yield return new global::UnityEngine.WaitForSeconds(0.2f);
		yield return new global::UnityEngine.WaitForSeconds(totalOffsetTime);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 3));
		yield return new global::UnityEngine.WaitForSeconds(0.06f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 2));
		yield return new global::UnityEngine.WaitForSeconds(0.06f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 2));
		yield return new global::UnityEngine.WaitForSeconds(0.03f);
		MyPodManager.AudioManager.PlaySfx(PopClip, 1f, antiOverlap: true, global::UnityEngine.Mathf.Clamp(1f + 0.25f * (float)(index + chainNum), 1f, 3f));
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 3));
		yield return new global::UnityEngine.WaitForSeconds(0.03f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 4));
		yield return new global::UnityEngine.WaitForSeconds(0.03f);
		SetBlockSprite(SpriteData.GetSprite(SpriteType.DestroySprites, this, 5));
		m_sr.enabled = false;
	}

	public void SetBlockRowAboveChainID()
	{
		for (int i = (int)Coords.y + 1; i < 11; i++)
		{
			if (!MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(Coords.x, i), out var sqr))
			{
				continue;
			}
			if (sqr.IsGarbage)
			{
				break;
			}
			if (sqr.Type != BlockType.Ghost && !sqr.IsSwapping)
			{
				if (sqr.ChainID != 0)
				{
					MyPodManager.GameLoop.SubtractChainBlockCount(sqr.ChainID);
				}
				sqr.ChainID = ChainID;
				if (ChainID != 0)
				{
					MyPodManager.GameLoop.SlottedChainBlockCounter[ChainID]++;
				}
				sqr.ChainIDValidated = false;
			}
		}
	}

	private void FixedLoc()
	{
		if (!ShouldBeDestroyed)
		{
			m_sr.color = (IsMatchable ? global::UnityEngine.Color.white : m_unmatchableSpriteClr);
		}
		UseOffset();
		IsSideMoving = m_tweenX != null && m_tweenX.active;
		if (IsGarbage && !HideGbgOverlay)
		{
			if (!GarbageOverlaySR.enabled)
			{
				GarbageOverlaySR.enabled = true;
			}
			GarbageOverlaySR.sprite = m_garbageOverlaySprites[MyPodManager.GameLoop.GarbageOverlaySyncIndex];
		}
		else if (GarbageOverlaySR.enabled)
		{
			GarbageOverlaySR.enabled = false;
		}
		if (ShouldDance && !IsGarbage && !MyPodManager.GameLoop.shouldKill)
		{
			SetBlockSprite(SpriteData.GetSprite(SpriteType.DanceSprites, this, MyPodManager.GameLoop.DanceSyncIndex));
		}
	}

	public void SetBlockSprite(global::UnityEngine.Sprite spr)
	{
		if (m_sr != null)
		{
			m_sr.sprite = spr;
		}
	}
}
