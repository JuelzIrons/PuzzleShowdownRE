public class NetworkServerReciever : global::Unity.Netcode.NetworkBehaviour
{
	public readonly uint MOVE_LATENCY = 72u;

	public global::System.Collections.Generic.List<OpponentMove> m_opponentMoves = new global::System.Collections.Generic.List<OpponentMove>();

	public global::System.Collections.Generic.List<OpponentMove> m_opponentSwaps = new global::System.Collections.Generic.List<OpponentMove>();

	public global::System.Collections.Generic.List<OpponentMove> m_opponentForceRaises = new global::System.Collections.Generic.List<OpponentMove>();

	public global::System.Collections.Generic.List<OpponentMove> m_opponentLostMoves = new global::System.Collections.Generic.List<OpponentMove>();

	public static NetworkServerReciever Instance;

	public global::Unity.Netcode.NetworkVariable<int> HostDifficultyLevel = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<bool> HostHasLaunchedScene = new global::Unity.Netcode.NetworkVariable<bool>(value: false);

	public global::Unity.Netcode.NetworkVariable<int> NetworkedRandomSeed = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<int> NetworkReadyCounter = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<int> HostCharacter = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<int> ClientCharacter = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<bool> LetClientSelectChar = new global::Unity.Netcode.NetworkVariable<bool>(value: false);

	public global::Unity.Netcode.NetworkVariable<bool> DRAW = new global::Unity.Netcode.NetworkVariable<bool>(value: false);

	public PodManager LocalPlayerPodManager;

	public PodManager LocalOpponentPodManager;

	public uint LocalTS;

	public bool HasLoadedInLocally;

	public bool LocalOpponentHasDied;

	public bool ForceTrueThisFrame;

	private int HostsDeathTS = -1;

	private int ClientsDeathTS = -1;

	private int ClientScoreAtDeath = -1;

	private int HostScoreAtDeath = -1;

	public bool HasEnded;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_hostWins;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_clientWins;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_localTSDebug;

	private bool m_gameEndBroadcasted;

	public void ClearAllQueues()
	{
		HasEnded = false;
		if (base.IsHost)
		{
			DRAW.Value = false;
		}
		HostsDeathTS = -1;
		ClientsDeathTS = -1;
		ClientScoreAtDeath = -1;
		HostScoreAtDeath = -1;
		m_gameEndBroadcasted = false;
		m_opponentMoves.Clear();
		m_opponentForceRaises.Clear();
		m_opponentLostMoves.Clear();
		m_opponentSwaps.Clear();
	}

	private void Awake()
	{
		Instance = this;
		LocalTS = 0u;
		if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
		{
			ClientCharacter.Value = -1;
			HostCharacter.Value = -1;
		}
		global::Unity.Netcode.NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
		global::Unity.Netcode.NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectCallback;
		HostSetDifficultyLevel(1);
	}

	private void OnClientDisconnectCallback(ulong clientId)
	{
		RelayManager.Instance.ReturnToMainMenu();
	}

	private void OnClientConnectCallback(ulong clientId)
	{
		RelayManager.Instance.SetupCharSelectScreenLocally();
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void MoveCursorToPosRpc(ulong id, int x, int y, global::Unity.Netcode.RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(322686856u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, id);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, x);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, y);
			__endSendRpc(ref bufferWriter, 322686856u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (base.NetworkManager.LocalClientId != id)
			{
				RelayManager.Instance.CharGrid.ForceOpponentCursorToPos(new global::UnityEngine.Vector2Int(x, y));
			}
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void SendReadyUpEventToOpponentRpc(ulong id, bool isReady, global::Unity.Netcode.RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(289878002u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, id);
			bufferWriter.WriteValueSafe(in isReady, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			__endSendRpc(ref bufferWriter, 289878002u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (base.NetworkManager.LocalClientId != id)
			{
				RelayManager.Instance.CharGrid.ForceOpponentReadyState(isReady);
			}
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void DisconnectAsClientRpc(ulong id, global::Unity.Netcode.RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(2130894510u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, id);
			__endSendRpc(ref bufferWriter, 2130894510u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			global::Unity.Netcode.NetworkManager.Singleton.DisconnectClient(id);
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void CloseConnectionScreenForAllClientRpc()
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1263289019u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
			__endSendRpc(ref bufferWriter, 1263289019u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			RelayManager.Instance.LocallyCloseLobbyCanvas(base.IsHost);
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void SendReadyToServerRpc()
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(1080371837u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			__endSendRpc(ref bufferWriter, 1080371837u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (base.IsServer)
			{
				NetworkReadyCounter.Value++;
			}
		}
	}

	public void HostSetDifficultyLevel(int Level)
	{
		HostDifficultyLevel.Value = Level;
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void SendMoveToServerRpc(uint TS, int moveType, ulong playerId, int moveOrder, global::UnityEngine.Vector2 dir = default(global::UnityEngine.Vector2), int scoreAtTimeOfDeath = -1)
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(960489248u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, TS);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, moveType);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, playerId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, moveOrder);
			bufferWriter.WriteValueSafe(in dir);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, scoreAtTimeOfDeath);
			__endSendRpc(ref bufferWriter, 960489248u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.Server, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		global::UnityEngine.Debug.Log($"Broadcasting move of type {(Moves)moveType} with TS: {TS} From player with id: {playerId}");
		BroadcastOpponentMovesRpc(TS + MOVE_LATENCY, moveType, playerId, moveOrder, dir);
		if (moveType != 3)
		{
			return;
		}
		if (global::Unity.Netcode.NetworkManager.Singleton.LocalClientId == playerId && HostsDeathTS == -1)
		{
			HostsDeathTS = (int)TS;
			HostScoreAtDeath = scoreAtTimeOfDeath;
		}
		if (global::Unity.Netcode.NetworkManager.Singleton.LocalClientId != playerId && ClientsDeathTS == -1)
		{
			ClientsDeathTS = (int)TS;
			ClientScoreAtDeath = scoreAtTimeOfDeath;
		}
		if (ClientsDeathTS == HostsDeathTS && ClientsDeathTS != -1 && HostsDeathTS != -1)
		{
			DRAW.Value = true;
			ulong hostId = global::Unity.Netcode.NetworkManager.Singleton.LocalClientId;
			ulong winnerClientId = global::System.Linq.Enumerable.First(global::Unity.Netcode.NetworkManager.Singleton.ConnectedClientsIds, (ulong id) => id != hostId);
			if (HostScoreAtDeath > ClientScoreAtDeath)
			{
				BroadcastTimeoutResultRpc(hostId);
			}
			else if (HostScoreAtDeath < ClientScoreAtDeath)
			{
				BroadcastTimeoutResultRpc(winnerClientId);
			}
			else
			{
				BroadcastTimeoutResultRpc(hostId, isDraw: true);
			}
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void BroadcastTimeoutResultRpc(ulong winnerClientId, bool isDraw = false)
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(74546731u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, winnerClientId);
			bufferWriter.WriteValueSafe(in isDraw, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			__endSendRpc(ref bufferWriter, 74546731u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage == global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
			if (!m_gameEndBroadcasted)
			{
				m_gameEndBroadcasted = true;
				bool flag = global::Unity.Netcode.NetworkManager.Singleton.LocalClientId == winnerClientId;
				LocalPlayerPodManager.GameLoop.Lost = true;
				LocalOpponentPodManager.GameLoop.Lost = true;
				LocalPlayerPodManager.GameLoop.HasLossChecked = false;
				LocalOpponentPodManager.GameLoop.HasLossChecked = true;
				LocalPlayerPodManager.GameLoop.EndGame(flag, isDraw);
				LocalPlayerPodManager.GameLoop.HasLossChecked = true;
				LocalOpponentPodManager.GameLoop.EndGame(!flag, isDraw);
				LocalOpponentPodManager.GameLoop.HasLossChecked = true;
			}
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void BroadcastOpponentMovesRpc(uint TS, int moveType, ulong playerId, int moveOrder, global::UnityEngine.Vector2 dir = default(global::UnityEngine.Vector2))
	{
		global::Unity.Netcode.NetworkManager networkManager = base.NetworkManager;
		if ((object)networkManager == null || !networkManager.IsListening)
		{
			global::UnityEngine.Debug.LogError("Rpc methods can only be invoked after starting the NetworkManager!");
			return;
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			global::Unity.Netcode.RpcAttribute.RpcAttributeParams attributeParams = new global::Unity.Netcode.RpcAttribute.RpcAttributeParams
			{
				InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone
			};
			global::Unity.Netcode.RpcParams rpcParams = default(global::Unity.Netcode.RpcParams);
			global::Unity.Netcode.FastBufferWriter bufferWriter = __beginSendRpc(692377062u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, TS);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, moveType);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, playerId);
			global::Unity.Netcode.BytePacker.WriteValueBitPacked(bufferWriter, moveOrder);
			bufferWriter.WriteValueSafe(in dir);
			__endSendRpc(ref bufferWriter, 692377062u, rpcParams, attributeParams, global::Unity.Netcode.SendTo.ClientsAndHost, global::Unity.Netcode.RpcDelivery.Reliable);
		}
		if (__rpc_exec_stage != global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute)
		{
			return;
		}
		__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		if (base.NetworkManager.LocalClientId == playerId)
		{
			return;
		}
		global::UnityEngine.Debug.Log($"MOVE ADDED WITH TS: {TS} MoveType: {(Moves)moveType} Dir = {dir}");
		switch (moveType)
		{
		case 2:
			m_opponentForceRaises.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentForceRaises.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		case 3:
			m_opponentLostMoves.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentLostMoves.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		case 1:
			m_opponentSwaps.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentSwaps.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		case 0:
			m_opponentMoves.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentMoves.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		}
		if ((float)(TS - LocalTS) <= 0f)
		{
			global::UnityEngine.Debug.LogError($"Err Recieved Move diff: {TS - LocalTS}");
			RelayManager.Instance.ReturnToMainMenu();
		}
		else if ((float)(TS - LocalTS) < 0.5f)
		{
			global::UnityEngine.Debug.LogWarning($"Warning Recieved Move diff: {TS - LocalTS}");
		}
		else
		{
			global::UnityEngine.Debug.Log($"Recieved Move diff: {TS - LocalTS}");
		}
	}

	public void ServerInit()
	{
		LocalPlayerPodManager.GameLoop.HangingFrames = 7;
		LocalOpponentPodManager.GameLoop.HangingFrames = 7;
		LocalPlayerPodManager.GameLoop.SpeedLv = HostDifficultyLevel.Value;
		LocalOpponentPodManager.GameLoop.SpeedLv = HostDifficultyLevel.Value;
		if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
		{
			LocalPlayerPodManager.SetSelectedCharacter((CharacterType)Instance.HostCharacter.Value);
			LocalOpponentPodManager.SetSelectedCharacter((CharacterType)Instance.ClientCharacter.Value);
		}
		else
		{
			LocalPlayerPodManager.SetSelectedCharacter((CharacterType)Instance.ClientCharacter.Value);
			LocalOpponentPodManager.SetSelectedCharacter((CharacterType)Instance.HostCharacter.Value);
		}
	}

	public void RecieverFixedUpdate()
	{
		ForceTrueThisFrame = false;
		m_localTSDebug.text = LocalTS.ToString() ?? "";
		if ((!HasLoadedInLocally && NetworkReadyCounter.Value >= 2) || HasEnded)
		{
			return;
		}
		LocalTS++;
		if (LocalTS == 36000)
		{
			LocalPlayerPodManager.GameLoop.CallMpLoss();
			ExtraElementsManager.Instance.TimeBuzzer?.Play();
		}
		global::System.Collections.Generic.List<OpponentMove> list = new global::System.Collections.Generic.List<OpponentMove>();
		if (m_opponentForceRaises.Count > 0 && m_opponentForceRaises[0].TS == (float)LocalTS)
		{
			list.Add(m_opponentForceRaises[0]);
			m_opponentForceRaises.RemoveAt(0);
		}
		if (m_opponentLostMoves.Count > 0 && m_opponentLostMoves[0].TS == (float)LocalTS)
		{
			list.Add(m_opponentLostMoves[0]);
			m_opponentLostMoves.RemoveAt(0);
		}
		if (m_opponentMoves.Count > 0 && m_opponentMoves[0].TS == (float)LocalTS)
		{
			global::System.Collections.Generic.List<OpponentMove> list2 = new global::System.Collections.Generic.List<OpponentMove>();
			for (int i = 0; i < m_opponentMoves.Count; i++)
			{
				if (m_opponentMoves[i].TS == (float)LocalTS)
				{
					list2.Add(m_opponentMoves[i]);
				}
			}
			foreach (OpponentMove item in list2)
			{
				list.Add(item);
				m_opponentMoves.RemoveAt(0);
			}
		}
		if (m_opponentSwaps.Count > 0 && m_opponentSwaps[0].TS == (float)LocalTS)
		{
			list.Add(m_opponentSwaps[0]);
			m_opponentSwaps.RemoveAt(0);
		}
		list.Sort((OpponentMove m1, OpponentMove m2) => m1.MoveFrameOrder.CompareTo(m2.MoveFrameOrder));
		foreach (OpponentMove item2 in list)
		{
			PerformMoveLocally(item2);
		}
	}

	public void StartTimer()
	{
		HasLoadedInLocally = true;
		SendReadyToServerRpc();
	}

	private void PerformMoveLocally(OpponentMove move)
	{
		switch (move.MoveType)
		{
		case Moves.Move:
			LocalOpponentPodManager.CursorController.ApplyNetworkMove(move.Dir);
			break;
		case Moves.Swap:
			LocalOpponentPodManager.CursorController.NetworkedSwap();
			break;
		case Moves.ForceTrue:
			ForceTrueThisFrame = true;
			break;
		case Moves.LOST:
			LocalOpponentHasDied = true;
			break;
		}
	}

	protected override void __initializeVariables()
	{
		if (HostDifficultyLevel == null)
		{
			throw new global::System.Exception("NetworkServerReciever.HostDifficultyLevel cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		HostDifficultyLevel.Initialize(this);
		__nameNetworkVariable(HostDifficultyLevel, "HostDifficultyLevel");
		NetworkVariableFields.Add(HostDifficultyLevel);
		if (HostHasLaunchedScene == null)
		{
			throw new global::System.Exception("NetworkServerReciever.HostHasLaunchedScene cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		HostHasLaunchedScene.Initialize(this);
		__nameNetworkVariable(HostHasLaunchedScene, "HostHasLaunchedScene");
		NetworkVariableFields.Add(HostHasLaunchedScene);
		if (NetworkedRandomSeed == null)
		{
			throw new global::System.Exception("NetworkServerReciever.NetworkedRandomSeed cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		NetworkedRandomSeed.Initialize(this);
		__nameNetworkVariable(NetworkedRandomSeed, "NetworkedRandomSeed");
		NetworkVariableFields.Add(NetworkedRandomSeed);
		if (NetworkReadyCounter == null)
		{
			throw new global::System.Exception("NetworkServerReciever.NetworkReadyCounter cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		NetworkReadyCounter.Initialize(this);
		__nameNetworkVariable(NetworkReadyCounter, "NetworkReadyCounter");
		NetworkVariableFields.Add(NetworkReadyCounter);
		if (HostCharacter == null)
		{
			throw new global::System.Exception("NetworkServerReciever.HostCharacter cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		HostCharacter.Initialize(this);
		__nameNetworkVariable(HostCharacter, "HostCharacter");
		NetworkVariableFields.Add(HostCharacter);
		if (ClientCharacter == null)
		{
			throw new global::System.Exception("NetworkServerReciever.ClientCharacter cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		ClientCharacter.Initialize(this);
		__nameNetworkVariable(ClientCharacter, "ClientCharacter");
		NetworkVariableFields.Add(ClientCharacter);
		if (LetClientSelectChar == null)
		{
			throw new global::System.Exception("NetworkServerReciever.LetClientSelectChar cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		LetClientSelectChar.Initialize(this);
		__nameNetworkVariable(LetClientSelectChar, "LetClientSelectChar");
		NetworkVariableFields.Add(LetClientSelectChar);
		if (DRAW == null)
		{
			throw new global::System.Exception("NetworkServerReciever.DRAW cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		DRAW.Initialize(this);
		__nameNetworkVariable(DRAW, "DRAW");
		NetworkVariableFields.Add(DRAW);
		base.__initializeVariables();
	}

	protected override void __initializeRpcs()
	{
		__registerRpc(322686856u, __rpc_handler_322686856, "MoveCursorToPosRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(289878002u, __rpc_handler_289878002, "SendReadyUpEventToOpponentRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(2130894510u, __rpc_handler_2130894510, "DisconnectAsClientRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(1263289019u, __rpc_handler_1263289019, "CloseConnectionScreenForAllClientRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(1080371837u, __rpc_handler_1080371837, "SendReadyToServerRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(960489248u, __rpc_handler_960489248, "SendMoveToServerRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(74546731u, __rpc_handler_74546731, "BroadcastTimeoutResultRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		__registerRpc(692377062u, __rpc_handler_692377062, "BroadcastOpponentMovesRpc", global::Unity.Netcode.RpcInvokePermission.Everyone);
		base.__initializeRpcs();
	}

	private static void __rpc_handler_322686856(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value2);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value3);
			global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).MoveCursorToPosRpc(value, value2, value3, ext);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_289878002(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value);
			reader.ReadValueSafe(out bool value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).SendReadyUpEventToOpponentRpc(value, value2, ext);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_2130894510(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value);
			global::Unity.Netcode.RpcParams ext = rpcParams.Ext;
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).DisconnectAsClientRpc(value, ext);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_1263289019(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).CloseConnectionScreenForAllClientRpc();
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_1080371837(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).SendReadyToServerRpc();
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_960489248(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out uint value);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value2);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value3);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value4);
			reader.ReadValueSafe(out global::UnityEngine.Vector2 value5);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value6);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).SendMoveToServerRpc(value, value2, value3, value4, value5, value6);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_74546731(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value);
			reader.ReadValueSafe(out bool value2, default(global::Unity.Netcode.FastBufferWriter.ForPrimitives));
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).BroadcastTimeoutResultRpc(value, value2);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	private static void __rpc_handler_692377062(global::Unity.Netcode.NetworkBehaviour target, global::Unity.Netcode.FastBufferReader reader, global::Unity.Netcode.__RpcParams rpcParams)
	{
		global::Unity.Netcode.NetworkManager networkManager = target.NetworkManager;
		if ((object)networkManager != null && networkManager.IsListening)
		{
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out uint value);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value2);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out ulong value3);
			global::Unity.Netcode.ByteUnpacker.ReadValueBitPacked(reader, out int value4);
			reader.ReadValueSafe(out global::UnityEngine.Vector2 value5);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Execute;
			((NetworkServerReciever)target).BroadcastOpponentMovesRpc(value, value2, value3, value4, value5);
			target.__rpc_exec_stage = global::Unity.Netcode.NetworkBehaviour.__RpcExecStage.Send;
		}
	}

	protected internal override string __getTypeName()
	{
		return "NetworkServerReciever";
	}
}
