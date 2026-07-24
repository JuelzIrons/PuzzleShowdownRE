namespace DG.Tweening
{
	public static class DOTweenCYInstruction
	{
		public class WaitForCompletion : global::UnityEngine.CustomYieldInstruction
		{
			private readonly global::DG.Tweening.Tween t;

			public override bool keepWaiting
			{
				get
				{
					if (t.active)
					{
						return !t.IsComplete();
					}
					return false;
				}
			}

			public WaitForCompletion(global::DG.Tweening.Tween tween)
			{
				t = tween;
			}
		}

		public class WaitForRewind : global::UnityEngine.CustomYieldInstruction
		{
			private readonly global::DG.Tweening.Tween t;

			public override bool keepWaiting
			{
				get
				{
					if (t.active)
					{
						if (t.playedOnce)
						{
							return t.position * (float)(t.CompletedLoops() + 1) > 0f;
						}
						return true;
					}
					return false;
				}
			}

			public WaitForRewind(global::DG.Tweening.Tween tween)
			{
				t = tween;
			}
		}

		public class WaitForKill : global::UnityEngine.CustomYieldInstruction
		{
			private readonly global::DG.Tweening.Tween t;

			public override bool keepWaiting => t.active;

			public WaitForKill(global::DG.Tweening.Tween tween)
			{
				t = tween;
			}
		}

		public class WaitForElapsedLoops : global::UnityEngine.CustomYieldInstruction
		{
			private readonly global::DG.Tweening.Tween t;

			private readonly int elapsedLoops;

			public override bool keepWaiting
			{
				get
				{
					if (t.active)
					{
						return t.CompletedLoops() < elapsedLoops;
					}
					return false;
				}
			}

			public WaitForElapsedLoops(global::DG.Tweening.Tween tween, int elapsedLoops)
			{
				t = tween;
				this.elapsedLoops = elapsedLoops;
			}
		}

		public class WaitForPosition : global::UnityEngine.CustomYieldInstruction
		{
			private readonly global::DG.Tweening.Tween t;

			private readonly float position;

			public override bool keepWaiting
			{
				get
				{
					if (t.active)
					{
						return t.position * (float)(t.CompletedLoops() + 1) < position;
					}
					return false;
				}
			}

			public WaitForPosition(global::DG.Tweening.Tween tween, float position)
			{
				t = tween;
				this.position = position;
			}
		}

		public class WaitForStart : global::UnityEngine.CustomYieldInstruction
		{
			private readonly global::DG.Tweening.Tween t;

			public override bool keepWaiting
			{
				get
				{
					if (t.active)
					{
						return !t.playedOnce;
					}
					return false;
				}
			}

			public WaitForStart(global::DG.Tweening.Tween tween)
			{
				t = tween;
			}
		}
	}
}
