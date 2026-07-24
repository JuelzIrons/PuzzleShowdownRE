namespace DG.Tweening.Core
{
	internal static class TweenManager
	{
		internal enum CapacityIncreaseMode
		{
			TweenersAndSequences = 0,
			TweenersOnly = 1,
			SequencesOnly = 2
		}

		private const int _DefaultMaxTweeners = 200;

		private const int _DefaultMaxSequences = 50;

		private const string _MaxTweensReached = "Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup";

		private const float _EpsilonVsTimeCheck = 1E-06f;

		internal static bool isUnityEditor;

		internal static bool isDebugBuild;

		internal static int maxActive;

		internal static int maxTweeners;

		internal static int maxSequences;

		internal static bool hasActiveTweens;

		internal static bool hasActiveDefaultTweens;

		internal static bool hasActiveLateTweens;

		internal static bool hasActiveFixedTweens;

		internal static bool hasActiveManualTweens;

		internal static int totActiveTweens;

		internal static int totActiveDefaultTweens;

		internal static int totActiveLateTweens;

		internal static int totActiveFixedTweens;

		internal static int totActiveManualTweens;

		internal static int totActiveTweeners;

		internal static int totActiveSequences;

		internal static int totPooledTweeners;

		internal static int totPooledSequences;

		internal static int totTweeners;

		internal static int totSequences;

		internal static bool isUpdateLoop;

		internal static global::DG.Tweening.Tween[] _activeTweens;

		private static global::DG.Tweening.Tween[] _pooledTweeners;

		private static readonly global::System.Collections.Generic.Stack<global::DG.Tweening.Tween> _PooledSequences;

		private static readonly global::System.Collections.Generic.List<global::DG.Tweening.Tween> _KillList;

		private static readonly global::System.Collections.Generic.Dictionary<global::DG.Tweening.Tween, global::DG.Tweening.Core.TweenLink> _TweenLinks;

		private static int _totTweenLinks;

		private static int _maxActiveLookupId;

		private static bool _requiresActiveReorganization;

		private static int _reorganizeFromId;

		private static int _minPooledTweenerId;

		private static int _maxPooledTweenerId;

		private static bool _despawnAllCalledFromUpdateLoopCallback;

		static TweenManager()
		{
			maxActive = 250;
			maxTweeners = 200;
			maxSequences = 50;
			_activeTweens = new global::DG.Tweening.Tween[250];
			_pooledTweeners = new global::DG.Tweening.Tween[200];
			_PooledSequences = new global::System.Collections.Generic.Stack<global::DG.Tweening.Tween>();
			_KillList = new global::System.Collections.Generic.List<global::DG.Tweening.Tween>(250);
			_TweenLinks = new global::System.Collections.Generic.Dictionary<global::DG.Tweening.Tween, global::DG.Tweening.Core.TweenLink>(250);
			_maxActiveLookupId = -1;
			_reorganizeFromId = -1;
			_minPooledTweenerId = -1;
			_maxPooledTweenerId = -1;
			isUnityEditor = global::UnityEngine.Application.isEditor;
		}

		internal static global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> GetTweener<T1, T2, TPlugOptions>() where TPlugOptions : struct, global::DG.Tweening.Plugins.Options.IPlugOptions
		{
			if (totPooledTweeners > 0)
			{
				global::System.Type typeFromHandle = typeof(T1);
				global::System.Type typeFromHandle2 = typeof(T2);
				global::System.Type typeFromHandle3 = typeof(TPlugOptions);
				for (int num = _maxPooledTweenerId; num > _minPooledTweenerId - 1; num--)
				{
					global::DG.Tweening.Tween tween = _pooledTweeners[num];
					if (tween != null && (object)tween.typeofT1 == typeFromHandle && (object)tween.typeofT2 == typeFromHandle2 && (object)tween.typeofTPlugOptions == typeFromHandle3)
					{
						global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> obj = (global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions>)tween;
						AddActiveTween(obj);
						_pooledTweeners[num] = null;
						if (_maxPooledTweenerId != _minPooledTweenerId)
						{
							if (num == _maxPooledTweenerId)
							{
								_maxPooledTweenerId--;
							}
							else if (num == _minPooledTweenerId)
							{
								_minPooledTweenerId++;
							}
						}
						totPooledTweeners--;
						return obj;
					}
				}
				if (totTweeners >= maxTweeners)
				{
					_pooledTweeners[_maxPooledTweenerId] = null;
					_maxPooledTweenerId--;
					totPooledTweeners--;
					totTweeners--;
				}
			}
			else if (totTweeners >= maxTweeners - 1)
			{
				int num2 = maxTweeners;
				int num3 = maxSequences;
				IncreaseCapacities(global::DG.Tweening.Core.TweenManager.CapacityIncreaseMode.TweenersOnly);
				if (global::DG.Tweening.Core.Debugger.logPriority >= 1)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", num2 + "/" + num3).Replace("#1", maxTweeners + "/" + maxSequences));
				}
			}
			global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions> tweenerCore = new global::DG.Tweening.Core.TweenerCore<T1, T2, TPlugOptions>();
			totTweeners++;
			AddActiveTween(tweenerCore);
			return tweenerCore;
		}

		internal static global::DG.Tweening.Sequence GetSequence()
		{
			if (totPooledSequences > 0)
			{
				global::DG.Tweening.Sequence obj = (global::DG.Tweening.Sequence)_PooledSequences.Pop();
				AddActiveTween(obj);
				totPooledSequences--;
				return obj;
			}
			if (totSequences >= maxSequences - 1)
			{
				int num = maxTweeners;
				int num2 = maxSequences;
				IncreaseCapacities(global::DG.Tweening.Core.TweenManager.CapacityIncreaseMode.SequencesOnly);
				if (global::DG.Tweening.Core.Debugger.logPriority >= 1)
				{
					global::DG.Tweening.Core.Debugger.LogWarning("Max Tweens reached: capacity has automatically been increased from #0 to #1. Use DOTween.SetTweensCapacity to set it manually at startup".Replace("#0", num + "/" + num2).Replace("#1", maxTweeners + "/" + maxSequences));
				}
			}
			global::DG.Tweening.Sequence sequence = new global::DG.Tweening.Sequence();
			totSequences++;
			AddActiveTween(sequence);
			return sequence;
		}

		internal static void SetUpdateType(global::DG.Tweening.Tween t, global::DG.Tweening.UpdateType updateType, bool isIndependentUpdate)
		{
			if (!t.active || t.updateType == updateType)
			{
				t.updateType = updateType;
				t.isIndependentUpdate = isIndependentUpdate;
				return;
			}
			if (t.updateType == global::DG.Tweening.UpdateType.Normal)
			{
				totActiveDefaultTweens--;
				hasActiveDefaultTweens = totActiveDefaultTweens > 0;
			}
			else
			{
				switch (t.updateType)
				{
				case global::DG.Tweening.UpdateType.Fixed:
					totActiveFixedTweens--;
					hasActiveFixedTweens = totActiveFixedTweens > 0;
					break;
				case global::DG.Tweening.UpdateType.Late:
					totActiveLateTweens--;
					hasActiveLateTweens = totActiveLateTweens > 0;
					break;
				default:
					totActiveManualTweens--;
					hasActiveManualTweens = totActiveManualTweens > 0;
					break;
				}
			}
			t.updateType = updateType;
			t.isIndependentUpdate = isIndependentUpdate;
			switch (updateType)
			{
			case global::DG.Tweening.UpdateType.Normal:
				totActiveDefaultTweens++;
				hasActiveDefaultTweens = true;
				break;
			case global::DG.Tweening.UpdateType.Fixed:
				totActiveFixedTweens++;
				hasActiveFixedTweens = true;
				break;
			case global::DG.Tweening.UpdateType.Late:
				totActiveLateTweens++;
				hasActiveLateTweens = true;
				break;
			default:
				totActiveManualTweens++;
				hasActiveManualTweens = true;
				break;
			}
		}

		internal static void AddActiveTweenToSequence(global::DG.Tweening.Tween t)
		{
			RemoveActiveTween(t);
		}

		internal static int DespawnAll()
		{
			int result = totActiveTweens;
			for (int i = 0; i < _maxActiveLookupId + 1; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween != null)
				{
					Despawn(tween, modifyActiveLists: false);
				}
			}
			ClearTweenArray(_activeTweens);
			hasActiveTweens = (hasActiveDefaultTweens = (hasActiveLateTweens = (hasActiveFixedTweens = (hasActiveManualTweens = false))));
			totActiveTweens = (totActiveDefaultTweens = (totActiveLateTweens = (totActiveFixedTweens = (totActiveManualTweens = 0))));
			totActiveTweeners = (totActiveSequences = 0);
			_maxActiveLookupId = (_reorganizeFromId = -1);
			_requiresActiveReorganization = false;
			_TweenLinks.Clear();
			_totTweenLinks = 0;
			if (isUpdateLoop)
			{
				_despawnAllCalledFromUpdateLoopCallback = true;
			}
			return result;
		}

		internal static void Despawn(global::DG.Tweening.Tween t, bool modifyActiveLists = true)
		{
			if (t.onKill != null)
			{
				global::DG.Tweening.Tween.OnTweenCallback(t.onKill, t);
			}
			if (modifyActiveLists)
			{
				RemoveActiveTween(t);
			}
			if (t.isRecyclable)
			{
				switch (t.tweenType)
				{
				case global::DG.Tweening.TweenType.Sequence:
				{
					_PooledSequences.Push(t);
					totPooledSequences++;
					global::DG.Tweening.Sequence sequence = (global::DG.Tweening.Sequence)t;
					int count = sequence.sequencedTweens.Count;
					for (int i = 0; i < count; i++)
					{
						Despawn(sequence.sequencedTweens[i], modifyActiveLists: false);
					}
					break;
				}
				case global::DG.Tweening.TweenType.Tweener:
					if (_maxPooledTweenerId == -1)
					{
						_maxPooledTweenerId = maxTweeners - 1;
						_minPooledTweenerId = maxTweeners - 1;
					}
					if (_maxPooledTweenerId < maxTweeners - 1)
					{
						_pooledTweeners[_maxPooledTweenerId + 1] = t;
						_maxPooledTweenerId++;
						if (_minPooledTweenerId > _maxPooledTweenerId)
						{
							_minPooledTweenerId = _maxPooledTweenerId;
						}
					}
					else
					{
						for (int num = _maxPooledTweenerId; num > -1; num--)
						{
							if (_pooledTweeners[num] == null)
							{
								_pooledTweeners[num] = t;
								if (num < _minPooledTweenerId)
								{
									_minPooledTweenerId = num;
								}
								if (_maxPooledTweenerId < _minPooledTweenerId)
								{
									_maxPooledTweenerId = _minPooledTweenerId;
								}
								break;
							}
						}
					}
					totPooledTweeners++;
					break;
				}
			}
			else
			{
				switch (t.tweenType)
				{
				case global::DG.Tweening.TweenType.Sequence:
				{
					totSequences--;
					global::DG.Tweening.Sequence sequence2 = (global::DG.Tweening.Sequence)t;
					int count2 = sequence2.sequencedTweens.Count;
					for (int j = 0; j < count2; j++)
					{
						Despawn(sequence2.sequencedTweens[j], modifyActiveLists: false);
					}
					break;
				}
				case global::DG.Tweening.TweenType.Tweener:
					totTweeners--;
					break;
				}
			}
			t.active = false;
			t.Reset();
		}

		internal static void PurgeAll(bool isApplicationQuitting)
		{
			if (!isApplicationQuitting)
			{
				for (int i = 0; i < maxActive; i++)
				{
					global::DG.Tweening.Tween tween = _activeTweens[i];
					if (tween != null && tween.active)
					{
						tween.active = false;
						if (tween.onKill != null)
						{
							global::DG.Tweening.Tween.OnTweenCallback(tween.onKill, tween);
						}
					}
				}
			}
			ClearTweenArray(_activeTweens);
			hasActiveTweens = (hasActiveDefaultTweens = (hasActiveLateTweens = (hasActiveFixedTweens = (hasActiveManualTweens = false))));
			totActiveTweens = (totActiveDefaultTweens = (totActiveLateTweens = (totActiveFixedTweens = (totActiveManualTweens = 0))));
			totActiveTweeners = (totActiveSequences = 0);
			_maxActiveLookupId = (_reorganizeFromId = -1);
			_requiresActiveReorganization = false;
			PurgePools();
			ResetCapacities();
			totTweeners = (totSequences = 0);
		}

		internal static void PurgePools()
		{
			totTweeners -= totPooledTweeners;
			totSequences -= totPooledSequences;
			ClearTweenArray(_pooledTweeners);
			_PooledSequences.Clear();
			totPooledTweeners = (totPooledSequences = 0);
			_minPooledTweenerId = (_maxPooledTweenerId = -1);
		}

		internal static void AddTweenLink(global::DG.Tweening.Tween t, global::DG.Tweening.Core.TweenLink tweenLink)
		{
			_totTweenLinks++;
			if (_TweenLinks.ContainsKey(t))
			{
				_TweenLinks[t] = tweenLink;
			}
			else
			{
				_TweenLinks.Add(t, tweenLink);
			}
			if (tweenLink.lastSeenActive)
			{
				global::DG.Tweening.LinkBehaviour behaviour = tweenLink.behaviour;
				if ((uint)(behaviour - 1) <= 3u)
				{
					Play(t);
				}
			}
			else
			{
				global::DG.Tweening.LinkBehaviour behaviour = tweenLink.behaviour;
				if ((uint)behaviour <= 2u)
				{
					Pause(t);
				}
			}
		}

		private static void RemoveTweenLink(global::DG.Tweening.Tween t)
		{
			if (_TweenLinks.ContainsKey(t))
			{
				_TweenLinks.Remove(t);
				_totTweenLinks--;
			}
		}

		internal static void ResetCapacities()
		{
			SetCapacities(200, 50);
		}

		internal static void SetCapacities(int tweenersCapacity, int sequencesCapacity)
		{
			if (tweenersCapacity < sequencesCapacity)
			{
				tweenersCapacity = sequencesCapacity;
			}
			maxActive = tweenersCapacity + sequencesCapacity;
			maxTweeners = tweenersCapacity;
			maxSequences = sequencesCapacity;
			global::System.Array.Resize(ref _activeTweens, maxActive);
			global::System.Array.Resize(ref _pooledTweeners, tweenersCapacity);
			_KillList.Capacity = maxActive;
		}

		internal static int Validate()
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < _maxActiveLookupId + 1; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (!tween.Validate())
				{
					num++;
					MarkForKilling(tween);
				}
			}
			if (num > 0)
			{
				DespawnActiveTweens(_KillList);
				_KillList.Clear();
			}
			return num;
		}

		internal static void Update(global::DG.Tweening.UpdateType updateType, float deltaTime, float independentTime)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			isUpdateLoop = true;
			bool flag = false;
			int num = _maxActiveLookupId + 1;
			for (int i = 0; i < num; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween != null && tween.updateType == updateType && Update(tween, deltaTime, independentTime, isSingleTweenManualUpdate: false))
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (_despawnAllCalledFromUpdateLoopCallback)
				{
					_despawnAllCalledFromUpdateLoopCallback = false;
				}
				else
				{
					DespawnActiveTweens(_KillList);
				}
				_KillList.Clear();
			}
			isUpdateLoop = false;
		}

		internal static bool Update(global::DG.Tweening.Tween t, float deltaTime, float independentTime, bool isSingleTweenManualUpdate)
		{
			if (_totTweenLinks > 0)
			{
				EvaluateTweenLink(t);
			}
			if (!t.active)
			{
				MarkForKilling(t, isSingleTweenManualUpdate);
				return true;
			}
			if (!t.isPlaying)
			{
				return false;
			}
			t.creationLocked = true;
			float num = (t.isIndependentUpdate ? independentTime : deltaTime) * t.timeScale;
			if (num < 1E-06f && num > -1E-06f)
			{
				return false;
			}
			if (!t.delayComplete)
			{
				num = t.UpdateDelay(t.elapsedDelay + num);
				if (num <= -1f)
				{
					MarkForKilling(t, isSingleTweenManualUpdate);
					return true;
				}
				if (num <= 0f)
				{
					return false;
				}
				if (t.playedOnce && t.onPlay != null)
				{
					global::DG.Tweening.Tween.OnTweenCallback(t.onPlay, t);
				}
			}
			if (!t.startupDone && !t.Startup())
			{
				MarkForKilling(t, isSingleTweenManualUpdate);
				return true;
			}
			float position = t.position;
			bool flag = position >= t.duration;
			int num2 = t.completedLoops;
			if (t.duration <= 0f)
			{
				position = 0f;
				num2 = ((t.loops == -1) ? (t.completedLoops + 1) : t.loops);
			}
			else
			{
				if (t.isBackwards)
				{
					position -= num;
					while (position < 0f && num2 > -1)
					{
						position += t.duration;
						num2--;
					}
					if (num2 < 0 || (flag && num2 < 1))
					{
						position = 0f;
						num2 = (flag ? 1 : 0);
					}
				}
				else
				{
					position += num;
					while (position >= t.duration && (t.loops == -1 || num2 < t.loops))
					{
						position -= t.duration;
						num2++;
					}
				}
				if (flag)
				{
					num2--;
				}
				if (t.loops != -1 && num2 >= t.loops)
				{
					position = t.duration;
				}
			}
			if (global::DG.Tweening.Tween.DoGoto(t, position, num2, global::DG.Tweening.Core.Enums.UpdateMode.Update))
			{
				MarkForKilling(t, isSingleTweenManualUpdate);
				return true;
			}
			return false;
		}

		internal static int FilteredOperation(global::DG.Tweening.Core.Enums.OperationType operationType, global::DG.Tweening.Core.Enums.FilterType filterType, object id, bool optionalBool, float optionalFloat, object optionalObj = null, object[] optionalArray = null)
		{
			int num = 0;
			bool flag = false;
			int num2 = ((optionalArray != null) ? optionalArray.Length : 0);
			bool flag2 = false;
			string text = null;
			bool flag3 = false;
			int num3 = 0;
			if ((uint)(filterType - 1) <= 1u)
			{
				if (id is string)
				{
					flag2 = true;
					text = (string)id;
				}
				else if (id is int)
				{
					flag3 = true;
					num3 = (int)id;
				}
			}
			for (int num4 = _maxActiveLookupId; num4 > -1; num4--)
			{
				global::DG.Tweening.Tween tween = _activeTweens[num4];
				if (tween != null && tween.active)
				{
					bool flag4 = false;
					switch (filterType)
					{
					case global::DG.Tweening.Core.Enums.FilterType.All:
						flag4 = true;
						break;
					case global::DG.Tweening.Core.Enums.FilterType.TargetOrId:
						flag4 = ((!flag2) ? ((!flag3) ? ((tween.id != null && id.Equals(tween.id)) || (tween.target != null && id.Equals(tween.target))) : (tween.intId == num3)) : (tween.stringId != null && tween.stringId == text));
						break;
					case global::DG.Tweening.Core.Enums.FilterType.TargetAndId:
						flag4 = ((!flag2) ? ((!flag3) ? (tween.id != null && tween.target != null && optionalObj != null && id.Equals(tween.id) && optionalObj.Equals(tween.target)) : (tween.target != null && tween.intId == num3 && optionalObj != null && optionalObj.Equals(tween.target))) : (tween.target != null && tween.stringId == text && optionalObj != null && optionalObj.Equals(tween.target)));
						break;
					case global::DG.Tweening.Core.Enums.FilterType.AllExceptTargetsOrIds:
					{
						flag4 = true;
						for (int i = 0; i < num2; i++)
						{
							object obj = optionalArray[i];
							if (obj is string)
							{
								flag2 = true;
								text = (string)obj;
							}
							else if (obj is int)
							{
								flag3 = true;
								num3 = (int)obj;
							}
							if (flag2 && tween.stringId == text)
							{
								flag4 = false;
								break;
							}
							if (flag3 && tween.intId == num3)
							{
								flag4 = false;
								break;
							}
							if ((tween.id != null && obj.Equals(tween.id)) || (tween.target != null && obj.Equals(tween.target)))
							{
								flag4 = false;
								break;
							}
						}
						break;
					}
					}
					if (flag4)
					{
						switch (operationType)
						{
						case global::DG.Tweening.Core.Enums.OperationType.Despawn:
							num++;
							tween.active = false;
							if (!isUpdateLoop)
							{
								Despawn(tween, modifyActiveLists: false);
								flag = true;
								_KillList.Add(tween);
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.Complete:
						{
							bool autoKill = tween.autoKill;
							if (!tween.startupDone)
							{
								ForceInit(tween);
							}
							if (!Complete(tween, modifyActiveLists: false, (!(optionalFloat > 0f)) ? global::DG.Tweening.Core.Enums.UpdateMode.Goto : global::DG.Tweening.Core.Enums.UpdateMode.Update))
							{
								break;
							}
							num += ((!optionalBool || autoKill) ? 1 : 0);
							if (autoKill)
							{
								if (isUpdateLoop)
								{
									tween.active = false;
									break;
								}
								flag = true;
								_KillList.Add(tween);
							}
							break;
						}
						case global::DG.Tweening.Core.Enums.OperationType.Flip:
							if (Flip(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.Goto:
							if (!tween.startupDone)
							{
								ForceInit(tween);
							}
							Goto(tween, optionalFloat, optionalBool);
							num++;
							break;
						case global::DG.Tweening.Core.Enums.OperationType.Pause:
							if (Pause(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.Play:
							if (Play(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.PlayBackwards:
							if (PlayBackwards(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.PlayForward:
							if (PlayForward(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.Restart:
							if (Restart(tween, optionalBool, optionalFloat))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.Rewind:
							if (Rewind(tween, optionalBool))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.SmoothRewind:
							if (SmoothRewind(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.TogglePause:
							if (TogglePause(tween))
							{
								num++;
							}
							break;
						case global::DG.Tweening.Core.Enums.OperationType.IsTweening:
							if ((!tween.isComplete || !tween.autoKill) && (!optionalBool || tween.isPlaying))
							{
								num++;
							}
							break;
						}
					}
				}
			}
			if (flag)
			{
				for (int num5 = _KillList.Count - 1; num5 > -1; num5--)
				{
					global::DG.Tweening.Tween tween2 = _KillList[num5];
					if (tween2.activeId != -1)
					{
						RemoveActiveTween(tween2);
					}
				}
				_KillList.Clear();
			}
			return num;
		}

		internal static bool Complete(global::DG.Tweening.Tween t, bool modifyActiveLists = true, global::DG.Tweening.Core.Enums.UpdateMode updateMode = global::DG.Tweening.Core.Enums.UpdateMode.Goto)
		{
			if (t.loops == -1)
			{
				return false;
			}
			if (!t.isComplete)
			{
				global::DG.Tweening.Tween.DoGoto(t, t.duration, t.loops, updateMode);
				t.isPlaying = false;
				if (t.autoKill && t.active)
				{
					if (isUpdateLoop)
					{
						t.active = false;
					}
					else
					{
						Despawn(t, modifyActiveLists);
					}
				}
				return true;
			}
			return false;
		}

		internal static bool Flip(global::DG.Tweening.Tween t)
		{
			t.isBackwards = !t.isBackwards;
			return true;
		}

		internal static void ForceInit(global::DG.Tweening.Tween t, bool isSequenced = false)
		{
			if (!t.startupDone && !t.Startup() && !isSequenced)
			{
				if (isUpdateLoop)
				{
					t.active = false;
				}
				else
				{
					RemoveActiveTween(t);
				}
			}
		}

		internal static bool Goto(global::DG.Tweening.Tween t, float to, bool andPlay = false, global::DG.Tweening.Core.Enums.UpdateMode updateMode = global::DG.Tweening.Core.Enums.UpdateMode.Goto)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = andPlay;
			t.delayComplete = true;
			t.elapsedDelay = t.delay;
			int num = ((t.duration <= 0f) ? 1 : global::UnityEngine.Mathf.FloorToInt(to / t.duration));
			float num2 = to % t.duration;
			if (t.loops != -1 && num >= t.loops)
			{
				num = t.loops;
				num2 = t.duration;
			}
			else if (num2 >= t.duration)
			{
				num2 = 0f;
			}
			bool flag = global::DG.Tweening.Tween.DoGoto(t, num2, num, updateMode);
			if (!andPlay && isPlaying && !flag && t.onPause != null)
			{
				global::DG.Tweening.Tween.OnTweenCallback(t.onPause, t);
			}
			return flag;
		}

		internal static bool Pause(global::DG.Tweening.Tween t)
		{
			if (t.isPlaying)
			{
				t.isPlaying = false;
				if (t.onPause != null)
				{
					global::DG.Tweening.Tween.OnTweenCallback(t.onPause, t);
				}
				return true;
			}
			return false;
		}

		internal static bool Play(global::DG.Tweening.Tween t)
		{
			if (!t.isPlaying && ((!t.isBackwards && !t.isComplete) || (t.isBackwards && (t.completedLoops > 0 || t.position > 0f))))
			{
				t.isPlaying = true;
				if (t.playedOnce && t.delayComplete && t.onPlay != null)
				{
					global::DG.Tweening.Tween.OnTweenCallback(t.onPlay, t);
				}
				return true;
			}
			return false;
		}

		internal static bool PlayBackwards(global::DG.Tweening.Tween t)
		{
			if (t.completedLoops == 0 && t.position <= 0f)
			{
				ManageOnRewindCallbackWhenAlreadyRewinded(t, isPlayBackwardsOrSmoothRewind: true);
				t.isBackwards = true;
				t.isPlaying = false;
				return false;
			}
			if (!t.isBackwards)
			{
				t.isBackwards = true;
				Play(t);
				return true;
			}
			return Play(t);
		}

		internal static bool PlayForward(global::DG.Tweening.Tween t)
		{
			if (t.isComplete)
			{
				t.isBackwards = false;
				t.isPlaying = false;
				return false;
			}
			if (t.isBackwards)
			{
				t.isBackwards = false;
				Play(t);
				return true;
			}
			return Play(t);
		}

		internal static bool Restart(global::DG.Tweening.Tween t, bool includeDelay = true, float changeDelayTo = -1f)
		{
			bool num = !t.isPlaying;
			t.isBackwards = false;
			if (changeDelayTo >= 0f && t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				t.delay = changeDelayTo;
			}
			Rewind(t, includeDelay);
			t.isPlaying = true;
			if (num && t.playedOnce && t.delayComplete && t.onPlay != null)
			{
				global::DG.Tweening.Tween.OnTweenCallback(t.onPlay, t);
			}
			return true;
		}

		internal static bool Rewind(global::DG.Tweening.Tween t, bool includeDelay = true)
		{
			bool isPlaying = t.isPlaying;
			t.isPlaying = false;
			bool result = false;
			if (t.delay > 0f)
			{
				if (includeDelay)
				{
					result = t.delay > 0f && t.elapsedDelay > 0f;
					t.elapsedDelay = 0f;
					t.delayComplete = false;
				}
				else
				{
					result = t.elapsedDelay < t.delay;
					t.elapsedDelay = t.delay;
					t.delayComplete = true;
				}
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (!global::DG.Tweening.Tween.DoGoto(t, 0f, 0, global::DG.Tweening.Core.Enums.UpdateMode.Goto) && isPlaying && t.onPause != null)
				{
					global::DG.Tweening.Tween.OnTweenCallback(t.onPause, t);
				}
			}
			else
			{
				ManageOnRewindCallbackWhenAlreadyRewinded(t, isPlayBackwardsOrSmoothRewind: false);
			}
			return result;
		}

		internal static bool SmoothRewind(global::DG.Tweening.Tween t)
		{
			bool result = false;
			if (t.delay > 0f)
			{
				result = t.elapsedDelay < t.delay;
				t.elapsedDelay = t.delay;
				t.delayComplete = true;
			}
			if (t.position > 0f || t.completedLoops > 0 || !t.startupDone)
			{
				result = true;
				if (t.loopType == global::DG.Tweening.LoopType.Incremental)
				{
					t.PlayBackwards();
				}
				else
				{
					t.Goto(t.ElapsedDirectionalPercentage() * t.duration);
					t.PlayBackwards();
				}
			}
			else
			{
				t.isPlaying = false;
				ManageOnRewindCallbackWhenAlreadyRewinded(t, isPlayBackwardsOrSmoothRewind: true);
			}
			return result;
		}

		internal static bool TogglePause(global::DG.Tweening.Tween t)
		{
			if (t.isPlaying)
			{
				return Pause(t);
			}
			return Play(t);
		}

		internal static int TotalPooledTweens()
		{
			return totPooledTweeners + totPooledSequences;
		}

		internal static int TotalPlayingTweens()
		{
			if (!hasActiveTweens)
			{
				return 0;
			}
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			int num = 0;
			for (int i = 0; i < _maxActiveLookupId + 1; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween != null && tween.isPlaying)
				{
					num++;
				}
			}
			return num;
		}

		internal static int TotalTweensById(object id, bool playingOnly)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return 0;
			}
			return DoGetTweensById(id, playingOnly, addToList: false, null);
		}

		internal static global::System.Collections.Generic.List<global::DG.Tweening.Tween> GetActiveTweens(bool playing, global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return null;
			}
			int num = totActiveTweens;
			if (fillableList == null)
			{
				fillableList = new global::System.Collections.Generic.List<global::DG.Tweening.Tween>(num);
			}
			for (int i = 0; i < num; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween.isPlaying == playing)
				{
					fillableList.Add(tween);
				}
			}
			if (fillableList.Count > 0)
			{
				return fillableList;
			}
			return null;
		}

		internal static global::System.Collections.Generic.List<global::DG.Tweening.Tween> GetTweensById(object id, bool playingOnly, global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return null;
			}
			if (fillableList == null)
			{
				fillableList = new global::System.Collections.Generic.List<global::DG.Tweening.Tween>(totActiveTweens);
			}
			DoGetTweensById(id, playingOnly, addToList: true, fillableList);
			if (fillableList.Count <= 0)
			{
				return null;
			}
			return fillableList;
		}

		private static int DoGetTweensById(object id, bool playingOnly, bool addToList, global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList)
		{
			int num = 0;
			bool flag = false;
			string text = null;
			bool flag2 = false;
			int num2 = 0;
			if (id is string)
			{
				flag = true;
				text = (string)id;
			}
			else if (id is int)
			{
				flag2 = true;
				num2 = (int)id;
			}
			int num3 = totActiveTweens;
			for (int i = 0; i < num3; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween == null)
				{
					continue;
				}
				if (flag)
				{
					if (tween.stringId == null || tween.stringId != text)
					{
						continue;
					}
				}
				else if (flag2)
				{
					if (tween.intId != num2)
					{
						continue;
					}
				}
				else if (tween.id == null || !object.Equals(id, tween.id))
				{
					continue;
				}
				if (!playingOnly || tween.isPlaying)
				{
					num++;
					if (addToList)
					{
						fillableList.Add(tween);
					}
				}
			}
			return num;
		}

		internal static global::System.Collections.Generic.List<global::DG.Tweening.Tween> GetTweensByTarget(object target, bool playingOnly, global::System.Collections.Generic.List<global::DG.Tweening.Tween> fillableList = null)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens <= 0)
			{
				return null;
			}
			int num = totActiveTweens;
			if (fillableList == null)
			{
				fillableList = new global::System.Collections.Generic.List<global::DG.Tweening.Tween>(num);
			}
			for (int i = 0; i < num; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween.target == target && (!playingOnly || tween.isPlaying))
				{
					fillableList.Add(tween);
				}
			}
			if (fillableList.Count > 0)
			{
				return fillableList;
			}
			return null;
		}

		private static void MarkForKilling(global::DG.Tweening.Tween t, bool isSingleTweenManualUpdate = false)
		{
			if (isSingleTweenManualUpdate && !isUpdateLoop)
			{
				Despawn(t);
				return;
			}
			t.active = false;
			_KillList.Add(t);
		}

		private static void EvaluateTweenLink(global::DG.Tweening.Tween t)
		{
			if (!_TweenLinks.TryGetValue(t, out var value))
			{
				return;
			}
			if (value.target == null)
			{
				t.active = false;
				return;
			}
			bool activeInHierarchy = value.target.activeInHierarchy;
			bool flag = !value.lastSeenActive && activeInHierarchy;
			bool flag2 = value.lastSeenActive && !activeInHierarchy;
			value.lastSeenActive = activeInHierarchy;
			switch (value.behaviour)
			{
			case global::DG.Tweening.LinkBehaviour.KillOnDisable:
				if (!activeInHierarchy)
				{
					t.active = false;
				}
				break;
			case global::DG.Tweening.LinkBehaviour.CompleteAndKillOnDisable:
				if (!activeInHierarchy)
				{
					if (!t.isComplete)
					{
						t.Complete();
					}
					t.active = false;
				}
				break;
			case global::DG.Tweening.LinkBehaviour.RewindAndKillOnDisable:
				if (!activeInHierarchy)
				{
					t.Rewind(includeDelay: false);
					t.active = false;
				}
				break;
			case global::DG.Tweening.LinkBehaviour.CompleteOnDisable:
				if (flag2 && !t.isComplete)
				{
					t.Complete();
				}
				break;
			case global::DG.Tweening.LinkBehaviour.RewindOnDisable:
				if (flag2)
				{
					t.Rewind(includeDelay: false);
				}
				break;
			case global::DG.Tweening.LinkBehaviour.PauseOnDisable:
				if (flag2 && t.isPlaying)
				{
					Pause(t);
				}
				break;
			case global::DG.Tweening.LinkBehaviour.PauseOnDisablePlayOnEnable:
				if (flag2)
				{
					Pause(t);
				}
				else if (flag)
				{
					Play(t);
				}
				break;
			case global::DG.Tweening.LinkBehaviour.PauseOnDisableRestartOnEnable:
				if (flag2)
				{
					Pause(t);
				}
				else if (flag)
				{
					Restart(t);
				}
				break;
			case global::DG.Tweening.LinkBehaviour.PlayOnEnable:
				if (flag)
				{
					Play(t);
				}
				break;
			case global::DG.Tweening.LinkBehaviour.RestartOnEnable:
				if (flag)
				{
					Restart(t);
				}
				break;
			case global::DG.Tweening.LinkBehaviour.KillOnDestroy:
				break;
			}
		}

		private static void AddActiveTween(global::DG.Tweening.Tween t)
		{
			if (_requiresActiveReorganization)
			{
				ReorganizeActiveTweens();
			}
			if (totActiveTweens < 0)
			{
				global::DG.Tweening.Core.Debugger.LogAddActiveTweenError("totActiveTweens < 0", t);
				totActiveTweens = 0;
			}
			t.active = true;
			t.updateType = global::DG.Tweening.DOTween.defaultUpdateType;
			t.isIndependentUpdate = global::DG.Tweening.DOTween.defaultTimeScaleIndependent;
			t.activeId = (_maxActiveLookupId = totActiveTweens);
			_activeTweens[totActiveTweens] = t;
			if (t.updateType == global::DG.Tweening.UpdateType.Normal)
			{
				totActiveDefaultTweens++;
				hasActiveDefaultTweens = true;
			}
			else
			{
				switch (t.updateType)
				{
				case global::DG.Tweening.UpdateType.Fixed:
					totActiveFixedTweens++;
					hasActiveFixedTweens = true;
					break;
				case global::DG.Tweening.UpdateType.Late:
					totActiveLateTweens++;
					hasActiveLateTweens = true;
					break;
				default:
					totActiveManualTweens++;
					hasActiveManualTweens = true;
					break;
				}
			}
			totActiveTweens++;
			if (t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				totActiveTweeners++;
			}
			else
			{
				totActiveSequences++;
			}
			hasActiveTweens = true;
		}

		private static void ReorganizeActiveTweens()
		{
			if (totActiveTweens <= 0)
			{
				_maxActiveLookupId = -1;
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
				return;
			}
			if (_reorganizeFromId == _maxActiveLookupId)
			{
				_maxActiveLookupId--;
				_requiresActiveReorganization = false;
				_reorganizeFromId = -1;
				return;
			}
			int num = 1;
			int num2 = _maxActiveLookupId + 1;
			_maxActiveLookupId = _reorganizeFromId - 1;
			for (int i = _reorganizeFromId + 1; i < num2; i++)
			{
				global::DG.Tweening.Tween tween = _activeTweens[i];
				if (tween == null)
				{
					num++;
					continue;
				}
				tween.activeId = (_maxActiveLookupId = i - num);
				_activeTweens[i - num] = tween;
				_activeTweens[i] = null;
			}
			_requiresActiveReorganization = false;
			_reorganizeFromId = -1;
		}

		private static void DespawnActiveTweens(global::System.Collections.Generic.List<global::DG.Tweening.Tween> tweens)
		{
			for (int num = tweens.Count - 1; num > -1; num--)
			{
				Despawn(tweens[num]);
			}
		}

		private static void RemoveActiveTween(global::DG.Tweening.Tween t)
		{
			int activeId = t.activeId;
			if (_totTweenLinks > 0)
			{
				RemoveTweenLink(t);
			}
			t.activeId = -1;
			_requiresActiveReorganization = true;
			if (_reorganizeFromId == -1 || _reorganizeFromId > activeId)
			{
				_reorganizeFromId = activeId;
			}
			_activeTweens[activeId] = null;
			if (t.updateType == global::DG.Tweening.UpdateType.Normal)
			{
				if (totActiveDefaultTweens > 0)
				{
					totActiveDefaultTweens--;
					hasActiveDefaultTweens = totActiveDefaultTweens > 0;
				}
				else
				{
					global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveDefaultTweens < 0", t);
				}
			}
			else
			{
				switch (t.updateType)
				{
				case global::DG.Tweening.UpdateType.Fixed:
					if (totActiveFixedTweens > 0)
					{
						totActiveFixedTweens--;
						hasActiveFixedTweens = totActiveFixedTweens > 0;
					}
					else
					{
						global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveFixedTweens < 0", t);
					}
					break;
				case global::DG.Tweening.UpdateType.Late:
					if (totActiveLateTweens > 0)
					{
						totActiveLateTweens--;
						hasActiveLateTweens = totActiveLateTweens > 0;
					}
					else
					{
						global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveLateTweens < 0", t);
					}
					break;
				default:
					if (totActiveManualTweens > 0)
					{
						totActiveManualTweens--;
						hasActiveManualTweens = totActiveManualTweens > 0;
					}
					else
					{
						global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveManualTweens < 0", t);
					}
					break;
				}
			}
			totActiveTweens--;
			hasActiveTweens = totActiveTweens > 0;
			if (t.tweenType == global::DG.Tweening.TweenType.Tweener)
			{
				totActiveTweeners--;
			}
			else
			{
				totActiveSequences--;
			}
			if (totActiveTweens < 0)
			{
				totActiveTweens = 0;
				global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveTweens < 0", t);
			}
			if (totActiveTweeners < 0)
			{
				totActiveTweeners = 0;
				global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveTweeners < 0", t);
			}
			if (totActiveSequences < 0)
			{
				totActiveSequences = 0;
				global::DG.Tweening.Core.Debugger.LogRemoveActiveTweenError("totActiveSequences < 0", t);
			}
		}

		private static void ClearTweenArray(global::DG.Tweening.Tween[] tweens)
		{
			int num = tweens.Length;
			for (int i = 0; i < num; i++)
			{
				tweens[i] = null;
			}
		}

		private static void IncreaseCapacities(global::DG.Tweening.Core.TweenManager.CapacityIncreaseMode increaseMode)
		{
			int num = 0;
			int num2 = global::UnityEngine.Mathf.Max((int)((float)maxTweeners * 1.5f), 200);
			int num3 = global::UnityEngine.Mathf.Max((int)((float)maxSequences * 1.5f), 50);
			switch (increaseMode)
			{
			case global::DG.Tweening.Core.TweenManager.CapacityIncreaseMode.TweenersOnly:
				num += num2;
				maxTweeners += num2;
				global::System.Array.Resize(ref _pooledTweeners, maxTweeners);
				break;
			case global::DG.Tweening.Core.TweenManager.CapacityIncreaseMode.SequencesOnly:
				num += num3;
				maxSequences += num3;
				break;
			default:
				num += num2 + num3;
				maxTweeners += num2;
				maxSequences += num3;
				global::System.Array.Resize(ref _pooledTweeners, maxTweeners);
				break;
			}
			maxActive = maxTweeners + maxSequences;
			global::System.Array.Resize(ref _activeTweens, maxActive);
			if (num > 0)
			{
				_KillList.Capacity += num;
			}
		}

		private static void ManageOnRewindCallbackWhenAlreadyRewinded(global::DG.Tweening.Tween t, bool isPlayBackwardsOrSmoothRewind)
		{
			if (t.onRewind == null)
			{
				return;
			}
			if (isPlayBackwardsOrSmoothRewind)
			{
				if (global::DG.Tweening.DOTween.rewindCallbackMode == global::DG.Tweening.Core.Enums.RewindCallbackMode.FireAlways)
				{
					t.onRewind();
				}
			}
			else if (global::DG.Tweening.DOTween.rewindCallbackMode != global::DG.Tweening.Core.Enums.RewindCallbackMode.FireIfPositionChanged)
			{
				t.onRewind();
			}
		}
	}
}
