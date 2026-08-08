public class CursorController : global::UnityEngine.MonoBehaviour
{
	public PodManager MyPodManager;

	public global::UnityEngine.Vector2[] coords;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.ContactFilter2D m_contactFilter;

	private global::UnityEngine.Collider2D[] m_results1 = new global::UnityEngine.Collider2D[2];

	private global::UnityEngine.Collider2D[] m_results2 = new global::UnityEngine.Collider2D[2];

	private float m_lastSwappedTimeStamp;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_swapClip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_moveClip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_pauseMenu;

	private global::UnityEngine.Vector2 prevDir;

	public bool CanPause = true;

	public bool hasSwappedThisFrame;

	public int moveOrderNumberThisFrame;

	private int m_moveTimer;

	private int m_initialTimer;

	private global::UnityEngine.Vector2 m_moveDir;

	private int heldDirectionCount;

	public void Setup()
	{
		coords[0] = base.transform.GetChild(0).localPosition + new global::UnityEngine.Vector3(-0.5f, 0f, 0f) + new global::UnityEngine.Vector3(3f, 5f);
		coords[1] = base.transform.GetChild(1).localPosition + new global::UnityEngine.Vector3(-0.5f, 0f, 0f) + new global::UnityEngine.Vector3(3f, 5f);
		MyPodManager.GameLoop.Rise?.AddListener(Rise);
	}

	private void OnDisable()
	{
		MyPodManager?.GameLoop.Rise?.RemoveListener(Rise);
	}

	private void Rise()
	{
		TryApplyMove(new global::UnityEngine.Vector2(0f, 1f), fromRise: true);
		if (MyPodManager.IsControlledByCPU)
		{
			MyPodManager.CPUAct.RaisesSinceSwapPlan++;
			MyPodManager.CPUAct.LastRaiseTS = MyPodManager.GameLoop.GameLoopFrameCounter;
		}
	}

	public void UseOffset()
	{
		base.transform.localPosition = coords[0] / 2f + coords[1] / 2f + new global::UnityEngine.Vector2(0f, (coords[0].y >= 10f) ? 0f : MyPodManager.GameLoop.OffsetValue);
	}

	public bool TrySwap()
	{
		if (MyPodManager == null)
		{
			return false;
		}
		if (!MyPodManager.HasStarted)
		{
			return false;
		}
		if (MyPodManager.GameLoop.GameOverTS != -1 && MyPodManager.GameLoop.GameOverTS - MyPodManager.GameLoop.GameLoopFrameCounter < 5)
		{
			return false;
		}
		MyPodManager.GridManager.TryGetSquareAtTile(coords[0], out var sqr);
		MyPodManager.GridManager.TryGetSquareAtTile(coords[1], out var sqr2);
		if (sqr != null && (sqr.ShouldBeDestroyed || !sqr.CanBeSwapped || sqr.IsGarbage))
		{
			return false;
		}
		if (sqr2 != null && (sqr2.ShouldBeDestroyed || !sqr2.CanBeSwapped || sqr2.IsGarbage))
		{
			return false;
		}
		if (sqr != null && sqr2 != null && (!sqr.IsSwapping || !sqr2.IsSwapping || !(sqr.OtherBlock == sqr2) || !(sqr2.OtherBlock == sqr)))
		{
			if (sqr.IsSwapping)
			{
				sqr = null;
			}
			if (sqr2.IsSwapping)
			{
				sqr2 = null;
			}
		}
		if (sqr != null && sqr2 != null)
		{
			sqr.GoToCoord(coords[1], SwapTweened: true, sqr2.ChainID, sqr2);
			sqr2.GoToCoord(coords[0], SwapTweened: true, sqr.ChainID, sqr);
			MyPodManager.AudioManager.PlaySfx(m_swapClip);
			return true;
		}
		bool flag = false;
		if (sqr != null)
		{
			if (MyPodManager.GridManager.TryGetSquareAtTile(coords[1] + new global::UnityEngine.Vector2(0f, 1f), out var sqr3) && !sqr3.IsGarbage)
			{
				return false;
			}
			sqr?.GoToCoord(coords[1], SwapTweened: true);
			flag = true;
		}
		if (sqr2 != null)
		{
			if (MyPodManager.GridManager.TryGetSquareAtTile(coords[0] + new global::UnityEngine.Vector2(0f, 1f), out var sqr4) && !sqr4.IsGarbage)
			{
				return false;
			}
			sqr2?.GoToCoord(coords[0], SwapTweened: true);
			flag = true;
		}
		if (flag)
		{
			MyPodManager.AudioManager.PlaySfx(m_swapClip);
		}
		return flag;
	}

	public void MoveContinously()
	{
		if ((!MyPodManager.IsOnlineMp || !(NetworkServerReciever.Instance.LocalPlayerPodManager != MyPodManager)) && !(m_moveDir == global::UnityEngine.Vector2.zero))
		{
			if (m_initialTimer < 9)
			{
				m_initialTimer++;
			}
			else
			{
				TryApplyMove(m_moveDir);
			}
		}
	}

	public void DebugSwapButton(bool performed)
	{
		if (MyPodManager.GameLoop.Lost || MyPodManager.GameLoop.PostDeathEvaluation || !performed || hasSwappedThisFrame || !((float)MyPodManager.GameLoop.GameLoopFrameCounter > m_lastSwappedTimeStamp))
		{
			return;
		}
		if (TrySwap())
		{
			hasSwappedThisFrame = true;
			if (MyPodManager.IsOnlineMp)
			{
				global::UnityEngine.Debug.Log($"SENT SWAP AT: {NetworkServerReciever.Instance.LocalTS}");
				NetworkServerReciever.Instance.SendMoveToServerRpc(NetworkServerReciever.Instance.LocalTS, 1, global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, moveOrderNumberThisFrame);
				moveOrderNumberThisFrame++;
			}
		}
		m_lastSwappedTimeStamp = MyPodManager.GameLoop.GameLoopFrameCounter;
	}

	public void DebugTryMove(global::UnityEngine.Vector2 debugInput, bool performed)
	{
		if (performed)
		{
			m_moveTimer = 0;
			m_initialTimer = 0;
			m_moveDir = debugInput;
			TryApplyMove(m_moveDir);
		}
		if (!performed)
		{
			prevDir = global::UnityEngine.Vector2.zero;
			m_moveDir = global::UnityEngine.Vector2.zero;
		}
	}

	public void TryMove(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (MyPodManager == null || !MyPodManager.HasStarted)
		{
			return;
		}
		if (context.performed)
		{
			heldDirectionCount++;
			m_moveTimer = 0;
			m_initialTimer = 0;
			m_moveDir = context.ReadValue<global::UnityEngine.Vector2>();
			TryApplyMove(m_moveDir);
		}
		if (context.canceled)
		{
			heldDirectionCount--;
			if (heldDirectionCount == 0)
			{
				m_moveDir = global::UnityEngine.Vector2.zero;
			}
			prevDir = global::UnityEngine.Vector2.zero;
		}
	}

	public void ApplyNetworkMove(global::UnityEngine.Vector2 dir)
	{
		m_moveTimer = 0;
		m_initialTimer = 0;
		m_moveDir = dir;
		TryApplyMove(m_moveDir, fromRise: false, fromMP: true);
	}

	public void SwapButton(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (MyPodManager == null || !MyPodManager.HasStarted || MyPodManager.GameLoop.Lost || MyPodManager.GameLoop.PostDeathEvaluation || !context.performed || hasSwappedThisFrame || !((float)MyPodManager.GameLoop.GameLoopFrameCounter > m_lastSwappedTimeStamp))
		{
			return;
		}
		if (TrySwap())
		{
			hasSwappedThisFrame = true;
			if (MyPodManager.IsOnlineMp)
			{
				global::UnityEngine.Debug.Log($"SENT SWAP AT: {NetworkServerReciever.Instance.LocalTS}");
				NetworkServerReciever.Instance.SendMoveToServerRpc(NetworkServerReciever.Instance.LocalTS, 1, global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, moveOrderNumberThisFrame);
				moveOrderNumberThisFrame++;
			}
		}
		m_lastSwappedTimeStamp = MyPodManager.GameLoop.GameLoopFrameCounter;
	}

	public void NetworkedSwap()
	{
		TrySwap();
		hasSwappedThisFrame = true;
		m_lastSwappedTimeStamp = MyPodManager.GameLoop.GameLoopFrameCounter;
	}

	public void TryForceSpeedup(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (!(MyPodManager == null) && MyPodManager.HasStarted && !MyPodManager.GameLoop.Lost && !MyPodManager.GameLoop.PostDeathEvaluation)
		{
			if (context.started)
			{
				MyPodManager.GameLoop.HoldingForceButton = true;
			}
			else if (context.canceled)
			{
				MyPodManager.GameLoop.HoldingForceButton = false;
			}
		}
	}

	public void TryApplyMove(global::UnityEngine.Vector2 dir, bool fromRise = false, bool fromMP = false)
	{
		if ((!fromMP && MyPodManager.IsOnlineMp && !MyPodManager.ISPLAYER1 && !fromRise) || MyPodManager.GameLoop.Lost || MyPodManager.GameLoop.PostDeathEvaluation)
		{
			return;
		}
		global::UnityEngine.Vector2 vector = dir;
		if (dir != global::UnityEngine.Vector2.zero)
		{
			m_lastSwappedTimeStamp = -1f;
		}
		if (coords[0].y + vector.y >= 11f)
		{
			return;
		}
		if (MyPodManager.GridManager.IsMoveValid(coords[0] + vector, coords[1] + vector))
		{
			base.transform.position += (global::UnityEngine.Vector3)vector;
			coords[0] = coords[0] + vector;
			coords[1] = coords[1] + vector;
			UseOffset();
			if (!fromRise && prevDir != vector)
			{
				MyPodManager.AudioManager.PlaySfx(m_moveClip, 0.4f);
			}
			if (MyPodManager.IsOnlineMp && !fromMP && !fromRise)
			{
				NetworkServerReciever.Instance.SendMoveToServerRpc(NetworkServerReciever.Instance.LocalTS, 0, global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, moveOrderNumberThisFrame, dir);
				moveOrderNumberThisFrame++;
			}
		}
		if (!fromRise)
		{
			prevDir = vector;
		}
	}

	public void SetInvis()
	{
		base.transform.GetChild(2).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
	}
}
