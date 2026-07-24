namespace Unity.VectorGraphics
{
	internal class PathDistanceForwardIterator
	{
		private class BezierLoop : global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment>, global::System.Collections.Generic.ICollection<global::Unity.VectorGraphics.BezierPathSegment>, global::System.Collections.Generic.IEnumerable<global::Unity.VectorGraphics.BezierPathSegment>, global::System.Collections.IEnumerable
		{
			private global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment> OpenPath;

			public global::Unity.VectorGraphics.BezierPathSegment this[int index]
			{
				get
				{
					if (index == OpenPath.Count)
					{
						return OpenPath[0];
					}
					return OpenPath[index];
				}
				set
				{
					throw new global::System.NotSupportedException();
				}
			}

			public int Count => OpenPath.Count + 1;

			public bool IsReadOnly => true;

			public BezierLoop(global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment> openPath)
			{
				OpenPath = openPath;
			}

			public void Add(global::Unity.VectorGraphics.BezierPathSegment item)
			{
				throw new global::System.NotSupportedException();
			}

			public void Clear()
			{
			}

			public bool Contains(global::Unity.VectorGraphics.BezierPathSegment item)
			{
				throw new global::System.NotImplementedException();
			}

			public void CopyTo(global::Unity.VectorGraphics.BezierPathSegment[] array, int arrayIndex)
			{
				throw new global::System.NotImplementedException();
			}

			public global::System.Collections.Generic.IEnumerator<global::Unity.VectorGraphics.BezierPathSegment> GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}

			public int IndexOf(global::Unity.VectorGraphics.BezierPathSegment item)
			{
				throw new global::System.NotImplementedException();
			}

			public void Insert(int index, global::Unity.VectorGraphics.BezierPathSegment item)
			{
				throw new global::System.NotSupportedException();
			}

			public bool Remove(global::Unity.VectorGraphics.BezierPathSegment item)
			{
				throw new global::System.NotSupportedException();
			}

			public void RemoveAt(int index)
			{
				throw new global::System.NotSupportedException();
			}

			global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
			{
				throw new global::System.NotImplementedException();
			}
		}

		public enum Result
		{
			Stepped = 0,
			NewSegment = 1,
			Ended = 2
		}

		private readonly bool closed;

		private readonly bool needTangentsDuringEval;

		private readonly float maxCordDeviationSq;

		private readonly float maxTanAngleDevCosine;

		private readonly float stepSizeT;

		private int currentSegment;

		private float currentT;

		private float segmentLengthSoFar;

		private float lengthSoFar;

		private global::UnityEngine.Vector2 lastPointEval;

		private global::UnityEngine.Vector2 currentTTangent;

		private global::Unity.VectorGraphics.BezierSegment currentBezSeg;

		public global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment> Segments { get; }

		public bool Closed => closed;

		public int CurrentSegment => currentSegment;

		public float CurrentT => currentT;

		public float LengthSoFar => lengthSoFar;

		public float SegmentLengthSoFar => segmentLengthSoFar;

		public bool Ended => currentT == 1f && currentSegment + 1 == Segments.Count - 1;

		public PathDistanceForwardIterator(global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment> pathSegments, bool closed, float maxCordDeviationSq, float maxTanAngleDevCosine, float stepSizeT)
		{
			if (pathSegments.Count < 2)
			{
				throw new global::System.Exception("Cannot iterate a path with no segments in it");
			}
			global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment> list;
			if (!closed || global::Unity.VectorGraphics.VectorUtils.PathEndsPerfectlyMatch(pathSegments))
			{
				list = pathSegments;
			}
			else
			{
				global::System.Collections.Generic.IList<global::Unity.VectorGraphics.BezierPathSegment> list2 = new global::Unity.VectorGraphics.PathDistanceForwardIterator.BezierLoop(pathSegments);
				list = list2;
			}
			Segments = list;
			this.closed = closed;
			needTangentsDuringEval = maxTanAngleDevCosine < 1f;
			this.maxCordDeviationSq = maxCordDeviationSq;
			this.maxTanAngleDevCosine = maxTanAngleDevCosine;
			this.stepSizeT = stepSizeT;
			currentBezSeg = new global::Unity.VectorGraphics.BezierSegment
			{
				P0 = pathSegments[0].P0,
				P1 = pathSegments[0].P1,
				P2 = pathSegments[0].P2,
				P3 = pathSegments[1].P0
			};
			lastPointEval = pathSegments[0].P0;
			currentTTangent = (needTangentsDuringEval ? global::Unity.VectorGraphics.VectorUtils.EvalTangent(currentBezSeg, 0f) : global::UnityEngine.Vector2.zero);
		}

		private float PointToLineDistanceSq(global::UnityEngine.Vector2 point, global::UnityEngine.Vector2 lineStart, global::UnityEngine.Vector2 lineEnd)
		{
			float sqrMagnitude = (lineEnd - lineStart).sqrMagnitude;
			if (sqrMagnitude < global::Unity.VectorGraphics.VectorUtils.Epsilon)
			{
				return (point - lineStart).sqrMagnitude;
			}
			float num = (lineEnd.y - lineStart.y) * point.x - (lineEnd.x - lineStart.x) * point.y + lineEnd.x * lineStart.y - lineEnd.y * lineStart.x;
			return num * num / sqrMagnitude;
		}

		public global::Unity.VectorGraphics.PathDistanceForwardIterator.Result AdvanceBy(float units, out float unitsRemaining)
		{
			unitsRemaining = units;
			if (Ended)
			{
				return global::Unity.VectorGraphics.PathDistanceForwardIterator.Result.Ended;
			}
			float num = currentT;
			global::UnityEngine.Vector2 vector = lastPointEval;
			global::UnityEngine.Vector2 tangent;
			while (true)
			{
				float num2 = global::UnityEngine.Mathf.Min(num + stepSizeT, 1f);
				tangent = global::UnityEngine.Vector2.zero;
				global::UnityEngine.Vector2 vector2 = (needTangentsDuringEval ? global::Unity.VectorGraphics.VectorUtils.EvalFull(currentBezSeg, num2, out tangent) : global::Unity.VectorGraphics.VectorUtils.Eval(currentBezSeg, num2));
				bool flag = false;
				if (needTangentsDuringEval)
				{
					float num3 = global::UnityEngine.Vector2.Dot(tangent, currentTTangent);
					flag = num3 < maxTanAngleDevCosine;
				}
				if (!flag && maxCordDeviationSq != float.MaxValue)
				{
					global::UnityEngine.Vector2 vector3 = vector;
					float sqrMagnitude = (vector2 - vector3).sqrMagnitude;
					if (sqrMagnitude > global::Unity.VectorGraphics.VectorUtils.Epsilon)
					{
						global::UnityEngine.Vector2 lineEnd = global::Unity.VectorGraphics.VectorUtils.Eval(currentBezSeg, global::UnityEngine.Mathf.Min((num2 - currentT) * 2f + currentT, 1f));
						float num4 = PointToLineDistanceSq(vector2, vector3, lineEnd);
						flag = num4 >= maxCordDeviationSq;
					}
				}
				float num5 = (vector2 - lastPointEval).magnitude;
				if (num5 > unitsRemaining)
				{
					num2 = num + stepSizeT * (unitsRemaining / num5);
					num5 = unitsRemaining;
					vector2 = global::Unity.VectorGraphics.VectorUtils.Eval(currentBezSeg, num2);
				}
				segmentLengthSoFar += num5;
				lengthSoFar += num5;
				unitsRemaining -= num5;
				lastPointEval = vector2;
				num = num2;
				if (!(num2 < 1f))
				{
					break;
				}
				if (unitsRemaining > 0f && !flag)
				{
					continue;
				}
				currentT = num2;
				currentTTangent = tangent;
				return global::Unity.VectorGraphics.PathDistanceForwardIterator.Result.Stepped;
			}
			if (currentSegment + 1 == Segments.Count - 1)
			{
				currentT = 1f;
				return global::Unity.VectorGraphics.PathDistanceForwardIterator.Result.Ended;
			}
			currentSegment++;
			currentBezSeg = new global::Unity.VectorGraphics.BezierSegment
			{
				P0 = Segments[currentSegment].P0,
				P1 = Segments[currentSegment].P1,
				P2 = Segments[currentSegment].P2,
				P3 = Segments[currentSegment + 1].P0
			};
			segmentLengthSoFar = 0f;
			currentT = 0f;
			currentTTangent = tangent;
			lastPointEval = currentBezSeg.P0;
			return global::Unity.VectorGraphics.PathDistanceForwardIterator.Result.NewSegment;
		}

		public global::UnityEngine.Vector2 EvalCurrent()
		{
			return global::Unity.VectorGraphics.VectorUtils.Eval(currentBezSeg, currentT);
		}
	}
}
