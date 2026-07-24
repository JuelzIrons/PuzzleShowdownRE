namespace LibTessDotNet
{
	internal class Tess
	{
		internal class ActiveRegion
		{
			internal global::LibTessDotNet.MeshUtils.Edge _eUp;

			internal global::LibTessDotNet.Dict<global::LibTessDotNet.Tess.ActiveRegion>.Node _nodeUp;

			internal int _windingNumber;

			internal bool _inside;

			internal bool _sentinel;

			internal bool _dirty;

			internal bool _fixUpperEdge;
		}

		private global::LibTessDotNet.Mesh _mesh;

		private global::LibTessDotNet.Vec3 _normal;

		private global::LibTessDotNet.Vec3 _sUnit;

		private global::LibTessDotNet.Vec3 _tUnit;

		private float _bminX;

		private float _bminY;

		private float _bmaxX;

		private float _bmaxY;

		private global::LibTessDotNet.WindingRule _windingRule;

		private global::LibTessDotNet.Dict<global::LibTessDotNet.Tess.ActiveRegion> _dict;

		private global::LibTessDotNet.PriorityQueue<global::LibTessDotNet.MeshUtils.Vertex> _pq;

		private global::LibTessDotNet.MeshUtils.Vertex _event;

		private global::LibTessDotNet.CombineCallback _combineCallback;

		private global::LibTessDotNet.ContourVertex[] _vertices;

		private int _vertexCount;

		private int[] _elements;

		private int _elementCount;

		public float SUnitX = 1f;

		public float SUnitY = 0f;

		public float SentinelCoord = 4E+30f;

		public bool NoEmptyPolygons = false;

		public bool UsePooling = false;

		public global::LibTessDotNet.Vec3 Normal
		{
			get
			{
				return _normal;
			}
			set
			{
				_normal = value;
			}
		}

		public global::LibTessDotNet.ContourVertex[] Vertices => _vertices;

		public int VertexCount => _vertexCount;

		public int[] Elements => _elements;

		public int ElementCount => _elementCount;

		private global::LibTessDotNet.Tess.ActiveRegion RegionBelow(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			return reg._nodeUp._prev._key;
		}

		private global::LibTessDotNet.Tess.ActiveRegion RegionAbove(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			return reg._nodeUp._next._key;
		}

		private bool EdgeLeq(global::LibTessDotNet.Tess.ActiveRegion reg1, global::LibTessDotNet.Tess.ActiveRegion reg2)
		{
			global::LibTessDotNet.MeshUtils.Edge eUp = reg1._eUp;
			global::LibTessDotNet.MeshUtils.Edge eUp2 = reg2._eUp;
			if (eUp._Dst == _event)
			{
				if (eUp2._Dst == _event)
				{
					if (global::LibTessDotNet.Geom.VertLeq(eUp._Org, eUp2._Org))
					{
						return global::LibTessDotNet.Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) <= 0f;
					}
					return global::LibTessDotNet.Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) >= 0f;
				}
				return global::LibTessDotNet.Geom.EdgeSign(eUp2._Dst, _event, eUp2._Org) <= 0f;
			}
			if (eUp2._Dst == _event)
			{
				return global::LibTessDotNet.Geom.EdgeSign(eUp._Dst, _event, eUp._Org) >= 0f;
			}
			float num = global::LibTessDotNet.Geom.EdgeEval(eUp._Dst, _event, eUp._Org);
			float num2 = global::LibTessDotNet.Geom.EdgeEval(eUp2._Dst, _event, eUp2._Org);
			return num >= num2;
		}

		private void DeleteRegion(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			if (reg._fixUpperEdge)
			{
			}
			reg._eUp._activeRegion = null;
			_dict.Remove(reg._nodeUp);
		}

		private void FixUpperEdge(global::LibTessDotNet.Tess.ActiveRegion reg, global::LibTessDotNet.MeshUtils.Edge newEdge)
		{
			_mesh.Delete(reg._eUp);
			reg._fixUpperEdge = false;
			reg._eUp = newEdge;
			newEdge._activeRegion = reg;
		}

		private global::LibTessDotNet.Tess.ActiveRegion TopLeftRegion(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			global::LibTessDotNet.MeshUtils.Vertex org = reg._eUp._Org;
			do
			{
				reg = RegionAbove(reg);
			}
			while (reg._eUp._Org == org);
			if (reg._fixUpperEdge)
			{
				global::LibTessDotNet.MeshUtils.Edge newEdge = _mesh.Connect(RegionBelow(reg)._eUp._Sym, reg._eUp._Lnext);
				FixUpperEdge(reg, newEdge);
				reg = RegionAbove(reg);
			}
			return reg;
		}

		private global::LibTessDotNet.Tess.ActiveRegion TopRightRegion(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			global::LibTessDotNet.MeshUtils.Vertex dst = reg._eUp._Dst;
			do
			{
				reg = RegionAbove(reg);
			}
			while (reg._eUp._Dst == dst);
			return reg;
		}

		private global::LibTessDotNet.Tess.ActiveRegion AddRegionBelow(global::LibTessDotNet.Tess.ActiveRegion regAbove, global::LibTessDotNet.MeshUtils.Edge eNewUp)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = new global::LibTessDotNet.Tess.ActiveRegion();
			activeRegion._eUp = eNewUp;
			activeRegion._nodeUp = _dict.InsertBefore(regAbove._nodeUp, activeRegion);
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = false;
			activeRegion._dirty = false;
			eNewUp._activeRegion = activeRegion;
			return activeRegion;
		}

		private void ComputeWinding(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			reg._windingNumber = RegionAbove(reg)._windingNumber + reg._eUp._winding;
			reg._inside = global::LibTessDotNet.Geom.IsWindingInside(_windingRule, reg._windingNumber);
		}

		private void FinishRegion(global::LibTessDotNet.Tess.ActiveRegion reg)
		{
			global::LibTessDotNet.MeshUtils.Edge eUp = reg._eUp;
			global::LibTessDotNet.MeshUtils.Face lface = eUp._Lface;
			lface._inside = reg._inside;
			lface._anEdge = eUp;
			DeleteRegion(reg);
		}

		private global::LibTessDotNet.MeshUtils.Edge FinishLeftRegions(global::LibTessDotNet.Tess.ActiveRegion regFirst, global::LibTessDotNet.Tess.ActiveRegion regLast)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = regFirst;
			global::LibTessDotNet.MeshUtils.Edge eUp = regFirst._eUp;
			while (activeRegion != regLast)
			{
				activeRegion._fixUpperEdge = false;
				global::LibTessDotNet.Tess.ActiveRegion activeRegion2 = RegionBelow(activeRegion);
				global::LibTessDotNet.MeshUtils.Edge edge = activeRegion2._eUp;
				if (edge._Org != eUp._Org)
				{
					if (!activeRegion2._fixUpperEdge)
					{
						FinishRegion(activeRegion);
						break;
					}
					edge = _mesh.Connect(eUp._Lprev, edge._Sym);
					FixUpperEdge(activeRegion2, edge);
				}
				if (eUp._Onext != edge)
				{
					_mesh.Splice(edge._Oprev, edge);
					_mesh.Splice(eUp, edge);
				}
				FinishRegion(activeRegion);
				eUp = activeRegion2._eUp;
				activeRegion = activeRegion2;
			}
			return eUp;
		}

		private void AddRightEdges(global::LibTessDotNet.Tess.ActiveRegion regUp, global::LibTessDotNet.MeshUtils.Edge eFirst, global::LibTessDotNet.MeshUtils.Edge eLast, global::LibTessDotNet.MeshUtils.Edge eTopLeft, bool cleanUp)
		{
			bool flag = true;
			global::LibTessDotNet.MeshUtils.Edge edge = eFirst;
			do
			{
				AddRegionBelow(regUp, edge._Sym);
				edge = edge._Onext;
			}
			while (edge != eLast);
			if (eTopLeft == null)
			{
				eTopLeft = RegionBelow(regUp)._eUp._Rprev;
			}
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = regUp;
			global::LibTessDotNet.MeshUtils.Edge edge2 = eTopLeft;
			while (true)
			{
				global::LibTessDotNet.Tess.ActiveRegion activeRegion2 = RegionBelow(activeRegion);
				edge = activeRegion2._eUp._Sym;
				if (edge._Org != edge2._Org)
				{
					break;
				}
				if (edge._Onext != edge2)
				{
					_mesh.Splice(edge._Oprev, edge);
					_mesh.Splice(edge2._Oprev, edge);
				}
				activeRegion2._windingNumber = activeRegion._windingNumber - edge._winding;
				activeRegion2._inside = global::LibTessDotNet.Geom.IsWindingInside(_windingRule, activeRegion2._windingNumber);
				activeRegion._dirty = true;
				if (!flag && CheckForRightSplice(activeRegion))
				{
					global::LibTessDotNet.Geom.AddWinding(edge, edge2);
					DeleteRegion(activeRegion);
					_mesh.Delete(edge2);
				}
				flag = false;
				activeRegion = activeRegion2;
				edge2 = edge;
			}
			activeRegion._dirty = true;
			if (cleanUp)
			{
				WalkDirtyRegions(activeRegion);
			}
		}

		private void SpliceMergeVertices(global::LibTessDotNet.MeshUtils.Edge e1, global::LibTessDotNet.MeshUtils.Edge e2)
		{
			_mesh.Splice(e1, e2);
		}

		private void VertexWeights(global::LibTessDotNet.MeshUtils.Vertex isect, global::LibTessDotNet.MeshUtils.Vertex org, global::LibTessDotNet.MeshUtils.Vertex dst, out float w0, out float w1)
		{
			float num = global::LibTessDotNet.Geom.VertL1dist(org, isect);
			float num2 = global::LibTessDotNet.Geom.VertL1dist(dst, isect);
			w0 = num2 / (num + num2) / 2f;
			w1 = num / (num + num2) / 2f;
			isect._coords.X += w0 * org._coords.X + w1 * dst._coords.X;
			isect._coords.Y += w0 * org._coords.Y + w1 * dst._coords.Y;
			isect._coords.Z += w0 * org._coords.Z + w1 * dst._coords.Z;
		}

		private void GetIntersectData(global::LibTessDotNet.MeshUtils.Vertex isect, global::LibTessDotNet.MeshUtils.Vertex orgUp, global::LibTessDotNet.MeshUtils.Vertex dstUp, global::LibTessDotNet.MeshUtils.Vertex orgLo, global::LibTessDotNet.MeshUtils.Vertex dstLo)
		{
			isect._coords = global::LibTessDotNet.Vec3.Zero;
			VertexWeights(isect, orgUp, dstUp, out var w, out var w2);
			VertexWeights(isect, orgLo, dstLo, out var w3, out var w4);
			if (_combineCallback != null)
			{
				isect._data = _combineCallback(isect._coords, new object[4] { orgUp._data, dstUp._data, orgLo._data, dstLo._data }, new float[4] { w, w2, w3, w4 });
			}
		}

		private bool CheckForRightSplice(global::LibTessDotNet.Tess.ActiveRegion regUp)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = RegionBelow(regUp);
			global::LibTessDotNet.MeshUtils.Edge eUp = regUp._eUp;
			global::LibTessDotNet.MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (global::LibTessDotNet.Geom.VertLeq(eUp._Org, eUp2._Org))
			{
				if (global::LibTessDotNet.Geom.EdgeSign(eUp2._Dst, eUp._Org, eUp2._Org) > 0f)
				{
					return false;
				}
				if (!global::LibTessDotNet.Geom.VertEq(eUp._Org, eUp2._Org))
				{
					_mesh.SplitEdge(eUp2._Sym);
					_mesh.Splice(eUp, eUp2._Oprev);
					regUp._dirty = (activeRegion._dirty = true);
				}
				else if (eUp._Org != eUp2._Org)
				{
					_pq.Remove(eUp._Org._pqHandle);
					SpliceMergeVertices(eUp2._Oprev, eUp);
				}
			}
			else
			{
				if (global::LibTessDotNet.Geom.EdgeSign(eUp._Dst, eUp2._Org, eUp._Org) < 0f)
				{
					return false;
				}
				RegionAbove(regUp)._dirty = (regUp._dirty = true);
				_mesh.SplitEdge(eUp._Sym);
				_mesh.Splice(eUp2._Oprev, eUp);
			}
			return true;
		}

		private bool CheckForLeftSplice(global::LibTessDotNet.Tess.ActiveRegion regUp)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = RegionBelow(regUp);
			global::LibTessDotNet.MeshUtils.Edge eUp = regUp._eUp;
			global::LibTessDotNet.MeshUtils.Edge eUp2 = activeRegion._eUp;
			if (global::LibTessDotNet.Geom.VertLeq(eUp._Dst, eUp2._Dst))
			{
				if (global::LibTessDotNet.Geom.EdgeSign(eUp._Dst, eUp2._Dst, eUp._Org) < 0f)
				{
					return false;
				}
				RegionAbove(regUp)._dirty = (regUp._dirty = true);
				global::LibTessDotNet.MeshUtils.Edge edge = _mesh.SplitEdge(eUp);
				_mesh.Splice(eUp2._Sym, edge);
				edge._Lface._inside = regUp._inside;
			}
			else
			{
				if (global::LibTessDotNet.Geom.EdgeSign(eUp2._Dst, eUp._Dst, eUp2._Org) > 0f)
				{
					return false;
				}
				regUp._dirty = (activeRegion._dirty = true);
				global::LibTessDotNet.MeshUtils.Edge edge2 = _mesh.SplitEdge(eUp2);
				_mesh.Splice(eUp._Lnext, eUp2._Sym);
				edge2._Rface._inside = regUp._inside;
			}
			return true;
		}

		private bool CheckForIntersect(global::LibTessDotNet.Tess.ActiveRegion regUp)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = RegionBelow(regUp);
			global::LibTessDotNet.MeshUtils.Edge eUp = regUp._eUp;
			global::LibTessDotNet.MeshUtils.Edge eUp2 = activeRegion._eUp;
			global::LibTessDotNet.MeshUtils.Vertex org = eUp._Org;
			global::LibTessDotNet.MeshUtils.Vertex org2 = eUp2._Org;
			global::LibTessDotNet.MeshUtils.Vertex dst = eUp._Dst;
			global::LibTessDotNet.MeshUtils.Vertex dst2 = eUp2._Dst;
			if (org == org2)
			{
				return false;
			}
			float num = global::System.Math.Min(org._t, dst._t);
			float num2 = global::System.Math.Max(org2._t, dst2._t);
			if (num > num2)
			{
				return false;
			}
			if (global::LibTessDotNet.Geom.VertLeq(org, org2))
			{
				if (global::LibTessDotNet.Geom.EdgeSign(dst2, org, org2) > 0f)
				{
					return false;
				}
			}
			else if (global::LibTessDotNet.Geom.EdgeSign(dst, org2, org) < 0f)
			{
				return false;
			}
			global::LibTessDotNet.MeshUtils.Vertex vertex = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Vertex>.Create();
			global::LibTessDotNet.Geom.EdgeIntersect(dst, org, dst2, org2, vertex);
			if (global::LibTessDotNet.Geom.VertLeq(vertex, _event))
			{
				vertex._s = _event._s;
				vertex._t = _event._t;
			}
			global::LibTessDotNet.MeshUtils.Vertex vertex2 = (global::LibTessDotNet.Geom.VertLeq(org, org2) ? org : org2);
			if (global::LibTessDotNet.Geom.VertLeq(vertex2, vertex))
			{
				vertex._s = vertex2._s;
				vertex._t = vertex2._t;
			}
			if (global::LibTessDotNet.Geom.VertEq(vertex, org) || global::LibTessDotNet.Geom.VertEq(vertex, org2))
			{
				CheckForRightSplice(regUp);
				return false;
			}
			if ((!global::LibTessDotNet.Geom.VertEq(dst, _event) && global::LibTessDotNet.Geom.EdgeSign(dst, _event, vertex) >= 0f) || (!global::LibTessDotNet.Geom.VertEq(dst2, _event) && global::LibTessDotNet.Geom.EdgeSign(dst2, _event, vertex) <= 0f))
			{
				if (dst2 == _event)
				{
					_mesh.SplitEdge(eUp._Sym);
					_mesh.Splice(eUp2._Sym, eUp);
					regUp = TopLeftRegion(regUp);
					eUp = RegionBelow(regUp)._eUp;
					FinishLeftRegions(RegionBelow(regUp), activeRegion);
					AddRightEdges(regUp, eUp._Oprev, eUp, eUp, cleanUp: true);
					return true;
				}
				if (dst == _event)
				{
					_mesh.SplitEdge(eUp2._Sym);
					_mesh.Splice(eUp._Lnext, eUp2._Oprev);
					activeRegion = regUp;
					regUp = TopRightRegion(regUp);
					global::LibTessDotNet.MeshUtils.Edge rprev = RegionBelow(regUp)._eUp._Rprev;
					activeRegion._eUp = eUp2._Oprev;
					eUp2 = FinishLeftRegions(activeRegion, null);
					AddRightEdges(regUp, eUp2._Onext, eUp._Rprev, rprev, cleanUp: true);
					return true;
				}
				if (global::LibTessDotNet.Geom.EdgeSign(dst, _event, vertex) >= 0f)
				{
					RegionAbove(regUp)._dirty = (regUp._dirty = true);
					_mesh.SplitEdge(eUp._Sym);
					eUp._Org._s = _event._s;
					eUp._Org._t = _event._t;
				}
				if (global::LibTessDotNet.Geom.EdgeSign(dst2, _event, vertex) <= 0f)
				{
					regUp._dirty = (activeRegion._dirty = true);
					_mesh.SplitEdge(eUp2._Sym);
					eUp2._Org._s = _event._s;
					eUp2._Org._t = _event._t;
				}
				return false;
			}
			_mesh.SplitEdge(eUp._Sym);
			_mesh.SplitEdge(eUp2._Sym);
			_mesh.Splice(eUp2._Oprev, eUp);
			eUp._Org._s = vertex._s;
			eUp._Org._t = vertex._t;
			eUp._Org._pqHandle = _pq.Insert(eUp._Org);
			if (eUp._Org._pqHandle._handle == global::LibTessDotNet.PQHandle.Invalid)
			{
				throw new global::System.InvalidOperationException("PQHandle should not be invalid");
			}
			GetIntersectData(eUp._Org, org, dst, org2, dst2);
			RegionAbove(regUp)._dirty = (regUp._dirty = (activeRegion._dirty = true));
			return false;
		}

		private void WalkDirtyRegions(global::LibTessDotNet.Tess.ActiveRegion regUp)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = RegionBelow(regUp);
			while (true)
			{
				bool flag = true;
				while (activeRegion._dirty)
				{
					regUp = activeRegion;
					activeRegion = RegionBelow(activeRegion);
				}
				if (!regUp._dirty)
				{
					activeRegion = regUp;
					regUp = RegionAbove(regUp);
					if (regUp == null || !regUp._dirty)
					{
						break;
					}
				}
				regUp._dirty = false;
				global::LibTessDotNet.MeshUtils.Edge eUp = regUp._eUp;
				global::LibTessDotNet.MeshUtils.Edge eUp2 = activeRegion._eUp;
				if (eUp._Dst != eUp2._Dst && CheckForLeftSplice(regUp))
				{
					if (activeRegion._fixUpperEdge)
					{
						DeleteRegion(activeRegion);
						_mesh.Delete(eUp2);
						activeRegion = RegionBelow(regUp);
						eUp2 = activeRegion._eUp;
					}
					else if (regUp._fixUpperEdge)
					{
						DeleteRegion(regUp);
						_mesh.Delete(eUp);
						regUp = RegionAbove(activeRegion);
						eUp = regUp._eUp;
					}
				}
				if (eUp._Org != eUp2._Org)
				{
					if (eUp._Dst != eUp2._Dst && !regUp._fixUpperEdge && !activeRegion._fixUpperEdge && (eUp._Dst == _event || eUp2._Dst == _event))
					{
						if (CheckForIntersect(regUp))
						{
							break;
						}
					}
					else
					{
						CheckForRightSplice(regUp);
					}
				}
				if (eUp._Org == eUp2._Org && eUp._Dst == eUp2._Dst)
				{
					global::LibTessDotNet.Geom.AddWinding(eUp2, eUp);
					DeleteRegion(regUp);
					_mesh.Delete(eUp);
					regUp = RegionAbove(activeRegion);
				}
			}
		}

		private void ConnectRightVertex(global::LibTessDotNet.Tess.ActiveRegion regUp, global::LibTessDotNet.MeshUtils.Edge eBottomLeft)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = eBottomLeft._Onext;
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = RegionBelow(regUp);
			global::LibTessDotNet.MeshUtils.Edge eUp = regUp._eUp;
			global::LibTessDotNet.MeshUtils.Edge eUp2 = activeRegion._eUp;
			bool flag = false;
			if (eUp._Dst != eUp2._Dst)
			{
				CheckForIntersect(regUp);
			}
			if (global::LibTessDotNet.Geom.VertEq(eUp._Org, _event))
			{
				_mesh.Splice(edge._Oprev, eUp);
				regUp = TopLeftRegion(regUp);
				edge = RegionBelow(regUp)._eUp;
				FinishLeftRegions(RegionBelow(regUp), activeRegion);
				flag = true;
			}
			if (global::LibTessDotNet.Geom.VertEq(eUp2._Org, _event))
			{
				_mesh.Splice(eBottomLeft, eUp2._Oprev);
				eBottomLeft = FinishLeftRegions(activeRegion, null);
				flag = true;
			}
			if (flag)
			{
				AddRightEdges(regUp, eBottomLeft._Onext, edge, edge, cleanUp: true);
				return;
			}
			global::LibTessDotNet.MeshUtils.Edge eDst = ((!global::LibTessDotNet.Geom.VertLeq(eUp2._Org, eUp._Org)) ? eUp : eUp2._Oprev);
			eDst = _mesh.Connect(eBottomLeft._Lprev, eDst);
			AddRightEdges(regUp, eDst, eDst._Onext, eDst._Onext, cleanUp: false);
			eDst._Sym._activeRegion._fixUpperEdge = true;
			WalkDirtyRegions(regUp);
		}

		private void ConnectLeftDegenerate(global::LibTessDotNet.Tess.ActiveRegion regUp, global::LibTessDotNet.MeshUtils.Vertex vEvent)
		{
			global::LibTessDotNet.MeshUtils.Edge eUp = regUp._eUp;
			if (global::LibTessDotNet.Geom.VertEq(eUp._Org, vEvent))
			{
				throw new global::System.InvalidOperationException("Vertices should have been merged before");
			}
			if (!global::LibTessDotNet.Geom.VertEq(eUp._Dst, vEvent))
			{
				_mesh.SplitEdge(eUp._Sym);
				if (regUp._fixUpperEdge)
				{
					_mesh.Delete(eUp._Onext);
					regUp._fixUpperEdge = false;
				}
				_mesh.Splice(vEvent._anEdge, eUp);
				SweepEvent(vEvent);
				return;
			}
			throw new global::System.InvalidOperationException("Vertices should have been merged before");
		}

		private void ConnectLeftVertex(global::LibTessDotNet.MeshUtils.Vertex vEvent)
		{
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = new global::LibTessDotNet.Tess.ActiveRegion();
			activeRegion._eUp = vEvent._anEdge._Sym;
			global::LibTessDotNet.Tess.ActiveRegion key = _dict.Find(activeRegion).Key;
			global::LibTessDotNet.Tess.ActiveRegion activeRegion2 = RegionBelow(key);
			if (activeRegion2 == null)
			{
				return;
			}
			global::LibTessDotNet.MeshUtils.Edge eUp = key._eUp;
			global::LibTessDotNet.MeshUtils.Edge eUp2 = activeRegion2._eUp;
			if (global::LibTessDotNet.Geom.EdgeSign(eUp._Dst, vEvent, eUp._Org) == 0f)
			{
				ConnectLeftDegenerate(key, vEvent);
				return;
			}
			global::LibTessDotNet.Tess.ActiveRegion activeRegion3 = (global::LibTessDotNet.Geom.VertLeq(eUp2._Dst, eUp._Dst) ? key : activeRegion2);
			if (key._inside || activeRegion3._fixUpperEdge)
			{
				global::LibTessDotNet.MeshUtils.Edge edge = ((activeRegion3 != key) ? _mesh.Connect(eUp2._Dnext, vEvent._anEdge)._Sym : _mesh.Connect(vEvent._anEdge._Sym, eUp._Lnext));
				if (activeRegion3._fixUpperEdge)
				{
					FixUpperEdge(activeRegion3, edge);
				}
				else
				{
					ComputeWinding(AddRegionBelow(key, edge));
				}
				SweepEvent(vEvent);
			}
			else
			{
				AddRightEdges(key, vEvent._anEdge, vEvent._anEdge, null, cleanUp: true);
			}
		}

		private void SweepEvent(global::LibTessDotNet.MeshUtils.Vertex vEvent)
		{
			_event = vEvent;
			global::LibTessDotNet.MeshUtils.Edge edge = vEvent._anEdge;
			while (edge._activeRegion == null)
			{
				edge = edge._Onext;
				if (edge == vEvent._anEdge)
				{
					ConnectLeftVertex(vEvent);
					return;
				}
			}
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = TopLeftRegion(edge._activeRegion);
			global::LibTessDotNet.Tess.ActiveRegion activeRegion2 = RegionBelow(activeRegion);
			global::LibTessDotNet.MeshUtils.Edge eUp = activeRegion2._eUp;
			global::LibTessDotNet.MeshUtils.Edge edge2 = FinishLeftRegions(activeRegion2, null);
			if (edge2._Onext == eUp)
			{
				ConnectRightVertex(activeRegion, edge2);
			}
			else
			{
				AddRightEdges(activeRegion, edge2._Onext, eUp, eUp, cleanUp: true);
			}
		}

		private void AddSentinel(float smin, float smax, float t)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = _mesh.MakeEdge();
			edge._Org._s = smax;
			edge._Org._t = t;
			edge._Dst._s = smin;
			edge._Dst._t = t;
			_event = edge._Dst;
			global::LibTessDotNet.Tess.ActiveRegion activeRegion = new global::LibTessDotNet.Tess.ActiveRegion();
			activeRegion._eUp = edge;
			activeRegion._windingNumber = 0;
			activeRegion._inside = false;
			activeRegion._fixUpperEdge = false;
			activeRegion._sentinel = true;
			activeRegion._dirty = false;
			activeRegion._nodeUp = _dict.Insert(activeRegion);
		}

		private void InitEdgeDict()
		{
			_dict = new global::LibTessDotNet.Dict<global::LibTessDotNet.Tess.ActiveRegion>(EdgeLeq);
			AddSentinel(0f - SentinelCoord, SentinelCoord, 0f - SentinelCoord);
			AddSentinel(0f - SentinelCoord, SentinelCoord, SentinelCoord);
		}

		private void DoneEdgeDict()
		{
			int num = 0;
			global::LibTessDotNet.Tess.ActiveRegion key;
			while ((key = _dict.Min().Key) != null)
			{
				if (!key._sentinel)
				{
				}
				DeleteRegion(key);
			}
			_dict = null;
		}

		private void RemoveDegenerateEdges()
		{
			global::LibTessDotNet.MeshUtils.Edge eHead = _mesh._eHead;
			global::LibTessDotNet.MeshUtils.Edge edge = eHead._next;
			while (edge != eHead)
			{
				global::LibTessDotNet.MeshUtils.Edge next = edge._next;
				global::LibTessDotNet.MeshUtils.Edge lnext = edge._Lnext;
				if (global::LibTessDotNet.Geom.VertEq(edge._Org, edge._Dst) && edge._Lnext._Lnext != edge)
				{
					SpliceMergeVertices(lnext, edge);
					_mesh.Delete(edge);
					edge = lnext;
					lnext = edge._Lnext;
				}
				if (lnext._Lnext == edge)
				{
					if (lnext != edge)
					{
						if (lnext == next || lnext == next._Sym)
						{
							next = next._next;
						}
						_mesh.Delete(lnext);
					}
					if (edge == next || edge == next._Sym)
					{
						next = next._next;
					}
					_mesh.Delete(edge);
				}
				edge = next;
			}
		}

		private void InitPriorityQ()
		{
			global::LibTessDotNet.MeshUtils.Vertex vHead = _mesh._vHead;
			int num = 0;
			for (global::LibTessDotNet.MeshUtils.Vertex next = vHead._next; next != vHead; next = next._next)
			{
				num++;
			}
			num += 8;
			_pq = new global::LibTessDotNet.PriorityQueue<global::LibTessDotNet.MeshUtils.Vertex>(num, global::LibTessDotNet.Geom.VertLeq);
			vHead = _mesh._vHead;
			for (global::LibTessDotNet.MeshUtils.Vertex next = vHead._next; next != vHead; next = next._next)
			{
				next._pqHandle = _pq.Insert(next);
				if (next._pqHandle._handle == global::LibTessDotNet.PQHandle.Invalid)
				{
					throw new global::System.InvalidOperationException("PQHandle should not be invalid");
				}
			}
			_pq.Init();
		}

		private void DonePriorityQ()
		{
			_pq = null;
		}

		private void RemoveDegenerateFaces()
		{
			global::LibTessDotNet.MeshUtils.Face face = _mesh._fHead._next;
			while (face != _mesh._fHead)
			{
				global::LibTessDotNet.MeshUtils.Face next = face._next;
				global::LibTessDotNet.MeshUtils.Edge anEdge = face._anEdge;
				if (anEdge._Lnext._Lnext == anEdge)
				{
					global::LibTessDotNet.Geom.AddWinding(anEdge._Onext, anEdge);
					_mesh.Delete(anEdge);
				}
				face = next;
			}
		}

		protected void ComputeInterior()
		{
			RemoveDegenerateEdges();
			InitPriorityQ();
			RemoveDegenerateFaces();
			InitEdgeDict();
			global::LibTessDotNet.MeshUtils.Vertex vertex;
			while ((vertex = _pq.ExtractMin()) != null)
			{
				while (true)
				{
					global::LibTessDotNet.MeshUtils.Vertex vertex2 = _pq.Minimum();
					if (vertex2 == null || !global::LibTessDotNet.Geom.VertEq(vertex2, vertex))
					{
						break;
					}
					vertex2 = _pq.ExtractMin();
					SpliceMergeVertices(vertex._anEdge, vertex2._anEdge);
				}
				SweepEvent(vertex);
			}
			DoneEdgeDict();
			DonePriorityQ();
			RemoveDegenerateFaces();
		}

		public Tess()
		{
			_normal = global::LibTessDotNet.Vec3.Zero;
			_bminX = (_bminY = (_bmaxX = (_bmaxY = 0f)));
			_windingRule = global::LibTessDotNet.WindingRule.EvenOdd;
			_mesh = null;
			_vertices = null;
			_vertexCount = 0;
			_elements = null;
			_elementCount = 0;
		}

		private void ComputeNormal(ref global::LibTessDotNet.Vec3 norm)
		{
			global::LibTessDotNet.MeshUtils.Vertex next = _mesh._vHead._next;
			float[] array = new float[3]
			{
				next._coords.X,
				next._coords.Y,
				next._coords.Z
			};
			global::LibTessDotNet.MeshUtils.Vertex[] array2 = new global::LibTessDotNet.MeshUtils.Vertex[3] { next, next, next };
			float[] array3 = new float[3]
			{
				next._coords.X,
				next._coords.Y,
				next._coords.Z
			};
			global::LibTessDotNet.MeshUtils.Vertex[] array4 = new global::LibTessDotNet.MeshUtils.Vertex[3] { next, next, next };
			while (next != _mesh._vHead)
			{
				if (next._coords.X < array[0])
				{
					array[0] = next._coords.X;
					array2[0] = next;
				}
				if (next._coords.Y < array[1])
				{
					array[1] = next._coords.Y;
					array2[1] = next;
				}
				if (next._coords.Z < array[2])
				{
					array[2] = next._coords.Z;
					array2[2] = next;
				}
				if (next._coords.X > array3[0])
				{
					array3[0] = next._coords.X;
					array4[0] = next;
				}
				if (next._coords.Y > array3[1])
				{
					array3[1] = next._coords.Y;
					array4[1] = next;
				}
				if (next._coords.Z > array3[2])
				{
					array3[2] = next._coords.Z;
					array4[2] = next;
				}
				next = next._next;
			}
			int num = 0;
			if (array3[1] - array[1] > array3[0] - array[0])
			{
				num = 1;
			}
			if (array3[2] - array[2] > array3[num] - array[num])
			{
				num = 2;
			}
			if (array[num] >= array3[num])
			{
				norm = new global::LibTessDotNet.Vec3
				{
					X = 0f,
					Y = 0f,
					Z = 1f
				};
				return;
			}
			float num2 = 0f;
			global::LibTessDotNet.MeshUtils.Vertex vertex = array2[num];
			global::LibTessDotNet.MeshUtils.Vertex vertex2 = array4[num];
			global::LibTessDotNet.Vec3.Sub(ref vertex._coords, ref vertex2._coords, out var result);
			global::LibTessDotNet.Vec3 vec = default(global::LibTessDotNet.Vec3);
			for (next = _mesh._vHead._next; next != _mesh._vHead; next = next._next)
			{
				global::LibTessDotNet.Vec3.Sub(ref next._coords, ref vertex2._coords, out var result2);
				vec.X = result.Y * result2.Z - result.Z * result2.Y;
				vec.Y = result.Z * result2.X - result.X * result2.Z;
				vec.Z = result.X * result2.Y - result.Y * result2.X;
				float num3 = vec.X * vec.X + vec.Y * vec.Y + vec.Z * vec.Z;
				if (num3 > num2)
				{
					num2 = num3;
					norm = vec;
				}
			}
			if (num2 <= 0f)
			{
				norm = global::LibTessDotNet.Vec3.Zero;
				num = global::LibTessDotNet.Vec3.LongAxis(ref result);
				norm[num] = 1f;
			}
		}

		private void CheckOrientation()
		{
			float num = 0f;
			for (global::LibTessDotNet.MeshUtils.Face next = _mesh._fHead._next; next != _mesh._fHead; next = next._next)
			{
				if (next._anEdge._winding > 0)
				{
					num += global::LibTessDotNet.MeshUtils.FaceArea(next);
				}
			}
			if (num < 0f)
			{
				for (global::LibTessDotNet.MeshUtils.Vertex next2 = _mesh._vHead._next; next2 != _mesh._vHead; next2 = next2._next)
				{
					next2._t = 0f - next2._t;
				}
				global::LibTessDotNet.Vec3.Neg(ref _tUnit);
			}
		}

		private void ProjectPolygon()
		{
			global::LibTessDotNet.Vec3 norm = _normal;
			bool flag = false;
			if (norm.X == 0f && norm.Y == 0f && norm.Z == 0f)
			{
				ComputeNormal(ref norm);
				_normal = norm;
				flag = true;
			}
			int num = global::LibTessDotNet.Vec3.LongAxis(ref norm);
			_sUnit[num] = 0f;
			_sUnit[(num + 1) % 3] = SUnitX;
			_sUnit[(num + 2) % 3] = SUnitY;
			_tUnit[num] = 0f;
			_tUnit[(num + 1) % 3] = ((norm[num] > 0f) ? (0f - SUnitY) : SUnitY);
			_tUnit[(num + 2) % 3] = ((norm[num] > 0f) ? SUnitX : (0f - SUnitX));
			for (global::LibTessDotNet.MeshUtils.Vertex next = _mesh._vHead._next; next != _mesh._vHead; next = next._next)
			{
				global::LibTessDotNet.Vec3.Dot(ref next._coords, ref _sUnit, out next._s);
				global::LibTessDotNet.Vec3.Dot(ref next._coords, ref _tUnit, out next._t);
			}
			if (flag)
			{
				CheckOrientation();
			}
			bool flag2 = true;
			for (global::LibTessDotNet.MeshUtils.Vertex next2 = _mesh._vHead._next; next2 != _mesh._vHead; next2 = next2._next)
			{
				if (flag2)
				{
					_bminX = (_bmaxX = next2._s);
					_bminY = (_bmaxY = next2._t);
					flag2 = false;
				}
				else
				{
					if (next2._s < _bminX)
					{
						_bminX = next2._s;
					}
					if (next2._s > _bmaxX)
					{
						_bmaxX = next2._s;
					}
					if (next2._t < _bminY)
					{
						_bminY = next2._t;
					}
					if (next2._t > _bmaxY)
					{
						_bmaxY = next2._t;
					}
				}
			}
		}

		private void TessellateMonoRegion(global::LibTessDotNet.MeshUtils.Face face)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = face._anEdge;
			while (global::LibTessDotNet.Geom.VertLeq(edge._Dst, edge._Org))
			{
				edge = edge._Lprev;
			}
			while (global::LibTessDotNet.Geom.VertLeq(edge._Org, edge._Dst))
			{
				edge = edge._Lnext;
			}
			global::LibTessDotNet.MeshUtils.Edge edge2 = edge._Lprev;
			while (edge._Lnext != edge2)
			{
				if (global::LibTessDotNet.Geom.VertLeq(edge._Dst, edge2._Org))
				{
					while (edge2._Lnext != edge && (global::LibTessDotNet.Geom.EdgeGoesLeft(edge2._Lnext) || global::LibTessDotNet.Geom.EdgeSign(edge2._Org, edge2._Dst, edge2._Lnext._Dst) <= 0f))
					{
						edge2 = _mesh.Connect(edge2._Lnext, edge2)._Sym;
					}
					edge2 = edge2._Lprev;
				}
				else
				{
					while (edge2._Lnext != edge && (global::LibTessDotNet.Geom.EdgeGoesRight(edge._Lprev) || global::LibTessDotNet.Geom.EdgeSign(edge._Dst, edge._Org, edge._Lprev._Org) >= 0f))
					{
						edge = _mesh.Connect(edge, edge._Lprev)._Sym;
					}
					edge = edge._Lnext;
				}
			}
			while (edge2._Lnext._Lnext != edge)
			{
				edge2 = _mesh.Connect(edge2._Lnext, edge2)._Sym;
			}
		}

		private void TessellateInterior()
		{
			global::LibTessDotNet.MeshUtils.Face face = _mesh._fHead._next;
			while (face != _mesh._fHead)
			{
				global::LibTessDotNet.MeshUtils.Face next = face._next;
				if (face._inside)
				{
					TessellateMonoRegion(face);
				}
				face = next;
			}
		}

		private void DiscardExterior()
		{
			global::LibTessDotNet.MeshUtils.Face face = _mesh._fHead._next;
			while (face != _mesh._fHead)
			{
				global::LibTessDotNet.MeshUtils.Face next = face._next;
				if (!face._inside)
				{
					_mesh.ZapFace(face);
				}
				face = next;
			}
		}

		private void SetWindingNumber(int value, bool keepOnlyBoundary)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = _mesh._eHead._next;
			while (edge != _mesh._eHead)
			{
				global::LibTessDotNet.MeshUtils.Edge next = edge._next;
				if (edge._Rface._inside != edge._Lface._inside)
				{
					edge._winding = (edge._Lface._inside ? value : (-value));
				}
				else if (!keepOnlyBoundary)
				{
					edge._winding = 0;
				}
				else
				{
					_mesh.Delete(edge);
				}
				edge = next;
			}
		}

		private int GetNeighbourFace(global::LibTessDotNet.MeshUtils.Edge edge)
		{
			if (edge._Rface == null)
			{
				return -1;
			}
			if (!edge._Rface._inside)
			{
				return -1;
			}
			return edge._Rface._n;
		}

		private void OutputPolymesh(global::LibTessDotNet.ElementType elementType, int polySize)
		{
			int num = 0;
			int num2 = 0;
			if (polySize < 3)
			{
				polySize = 3;
			}
			if (polySize > 3)
			{
				_mesh.MergeConvexFaces(polySize);
			}
			for (global::LibTessDotNet.MeshUtils.Vertex next = _mesh._vHead._next; next != _mesh._vHead; next = next._next)
			{
				next._n = -1;
			}
			for (global::LibTessDotNet.MeshUtils.Face next2 = _mesh._fHead._next; next2 != _mesh._fHead; next2 = next2._next)
			{
				next2._n = -1;
				if (!next2._inside)
				{
					continue;
				}
				if (NoEmptyPolygons)
				{
					float value = global::LibTessDotNet.MeshUtils.FaceArea(next2);
					if (global::System.Math.Abs(value) < float.Epsilon)
					{
						continue;
					}
				}
				global::LibTessDotNet.MeshUtils.Edge edge = next2._anEdge;
				int num3 = 0;
				do
				{
					global::LibTessDotNet.MeshUtils.Vertex next = edge._Org;
					if (next._n == -1)
					{
						next._n = num2;
						num2++;
					}
					num3++;
					edge = edge._Lnext;
				}
				while (edge != next2._anEdge);
				next2._n = num;
				num++;
			}
			_elementCount = num;
			if (elementType == global::LibTessDotNet.ElementType.ConnectedPolygons)
			{
				num *= 2;
			}
			_elements = new int[num * polySize];
			_vertexCount = num2;
			_vertices = new global::LibTessDotNet.ContourVertex[_vertexCount];
			for (global::LibTessDotNet.MeshUtils.Vertex next = _mesh._vHead._next; next != _mesh._vHead; next = next._next)
			{
				if (next._n != -1)
				{
					_vertices[next._n].Position = next._coords;
					_vertices[next._n].Data = next._data;
				}
			}
			int num4 = 0;
			for (global::LibTessDotNet.MeshUtils.Face next2 = _mesh._fHead._next; next2 != _mesh._fHead; next2 = next2._next)
			{
				if (!next2._inside)
				{
					continue;
				}
				if (NoEmptyPolygons)
				{
					float value2 = global::LibTessDotNet.MeshUtils.FaceArea(next2);
					if (global::System.Math.Abs(value2) < float.Epsilon)
					{
						continue;
					}
				}
				global::LibTessDotNet.MeshUtils.Edge edge = next2._anEdge;
				int num3 = 0;
				do
				{
					global::LibTessDotNet.MeshUtils.Vertex next = edge._Org;
					_elements[num4++] = next._n;
					num3++;
					edge = edge._Lnext;
				}
				while (edge != next2._anEdge);
				for (int i = num3; i < polySize; i++)
				{
					_elements[num4++] = -1;
				}
				if (elementType == global::LibTessDotNet.ElementType.ConnectedPolygons)
				{
					edge = next2._anEdge;
					do
					{
						_elements[num4++] = GetNeighbourFace(edge);
						edge = edge._Lnext;
					}
					while (edge != next2._anEdge);
					for (int i = num3; i < polySize; i++)
					{
						_elements[num4++] = -1;
					}
				}
			}
		}

		private void OutputContours()
		{
			int num = 0;
			int num2 = 0;
			_vertexCount = 0;
			_elementCount = 0;
			for (global::LibTessDotNet.MeshUtils.Face next = _mesh._fHead._next; next != _mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					global::LibTessDotNet.MeshUtils.Edge edge2;
					global::LibTessDotNet.MeshUtils.Edge edge = (edge2 = next._anEdge);
					do
					{
						_vertexCount++;
						edge2 = edge2._Lnext;
					}
					while (edge2 != edge);
					_elementCount++;
				}
			}
			_elements = new int[_elementCount * 2];
			_vertices = new global::LibTessDotNet.ContourVertex[_vertexCount];
			int num3 = 0;
			int num4 = 0;
			num = 0;
			for (global::LibTessDotNet.MeshUtils.Face next = _mesh._fHead._next; next != _mesh._fHead; next = next._next)
			{
				if (next._inside)
				{
					num2 = 0;
					global::LibTessDotNet.MeshUtils.Edge edge2;
					global::LibTessDotNet.MeshUtils.Edge edge = (edge2 = next._anEdge);
					do
					{
						_vertices[num3].Position = edge2._Org._coords;
						_vertices[num3].Data = edge2._Org._data;
						num3++;
						num2++;
						edge2 = edge2._Lnext;
					}
					while (edge2 != edge);
					_elements[num4++] = num;
					_elements[num4++] = num2;
					num += num2;
				}
			}
		}

		private float SignedArea(global::LibTessDotNet.ContourVertex[] vertices)
		{
			float num = 0f;
			for (int i = 0; i < vertices.Length; i++)
			{
				global::LibTessDotNet.ContourVertex contourVertex = vertices[i];
				global::LibTessDotNet.ContourVertex contourVertex2 = vertices[(i + 1) % vertices.Length];
				num += contourVertex.Position.X * contourVertex2.Position.Y;
				num -= contourVertex.Position.Y * contourVertex2.Position.X;
			}
			return 0.5f * num;
		}

		public void AddContour(global::LibTessDotNet.ContourVertex[] vertices)
		{
			AddContour(vertices, global::LibTessDotNet.ContourOrientation.Original);
		}

		public void AddContour(global::LibTessDotNet.ContourVertex[] vertices, global::LibTessDotNet.ContourOrientation forceOrientation)
		{
			if (_mesh == null)
			{
				_mesh = new global::LibTessDotNet.Mesh();
			}
			bool flag = false;
			if (forceOrientation != global::LibTessDotNet.ContourOrientation.Original)
			{
				float num = SignedArea(vertices);
				flag = (forceOrientation == global::LibTessDotNet.ContourOrientation.Clockwise && num < 0f) || (forceOrientation == global::LibTessDotNet.ContourOrientation.CounterClockwise && num > 0f);
			}
			global::LibTessDotNet.MeshUtils.Edge edge = null;
			for (int i = 0; i < vertices.Length; i++)
			{
				if (edge == null)
				{
					edge = _mesh.MakeEdge();
					_mesh.Splice(edge, edge._Sym);
				}
				else
				{
					_mesh.SplitEdge(edge);
					edge = edge._Lnext;
				}
				int num2 = (flag ? (vertices.Length - 1 - i) : i);
				edge._Org._coords = vertices[num2].Position;
				edge._Org._data = vertices[num2].Data;
				edge._winding = 1;
				edge._Sym._winding = -1;
			}
		}

		public void Tessellate(global::LibTessDotNet.WindingRule windingRule, global::LibTessDotNet.ElementType elementType, int polySize)
		{
			Tessellate(windingRule, elementType, polySize, null);
		}

		public void Tessellate(global::LibTessDotNet.WindingRule windingRule, global::LibTessDotNet.ElementType elementType, int polySize, global::LibTessDotNet.CombineCallback combineCallback)
		{
			_normal = global::LibTessDotNet.Vec3.Zero;
			_vertices = null;
			_elements = null;
			_windingRule = windingRule;
			_combineCallback = combineCallback;
			if (_mesh != null)
			{
				ProjectPolygon();
				ComputeInterior();
				if (elementType == global::LibTessDotNet.ElementType.BoundaryContours)
				{
					SetWindingNumber(1, keepOnlyBoundary: true);
				}
				else
				{
					TessellateInterior();
				}
				if (elementType == global::LibTessDotNet.ElementType.BoundaryContours)
				{
					OutputContours();
				}
				else
				{
					OutputPolymesh(elementType, polySize);
				}
				if (UsePooling)
				{
					_mesh.Free();
				}
				_mesh = null;
			}
		}
	}
}
