namespace LibTessDotNet
{
	internal static class MeshUtils
	{
		public abstract class Pooled<T> where T : global::LibTessDotNet.MeshUtils.Pooled<T>, new()
		{
			private static global::System.Collections.Generic.Stack<T> _stack;

			public abstract void Reset();

			public virtual void OnFree()
			{
			}

			public static T Create()
			{
				if (_stack != null && _stack.Count > 0)
				{
					return _stack.Pop();
				}
				return new T();
			}

			public void Free()
			{
				OnFree();
				Reset();
				if (_stack == null)
				{
					_stack = new global::System.Collections.Generic.Stack<T>();
				}
				_stack.Push((T)this);
			}
		}

		public class Vertex : global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Vertex>
		{
			internal global::LibTessDotNet.MeshUtils.Vertex _prev;

			internal global::LibTessDotNet.MeshUtils.Vertex _next;

			internal global::LibTessDotNet.MeshUtils.Edge _anEdge;

			internal global::LibTessDotNet.Vec3 _coords;

			internal float _s;

			internal float _t;

			internal global::LibTessDotNet.PQHandle _pqHandle;

			internal int _n;

			internal object _data;

			public override void Reset()
			{
				_prev = (_next = null);
				_anEdge = null;
				_coords = global::LibTessDotNet.Vec3.Zero;
				_s = 0f;
				_t = 0f;
				_pqHandle = default(global::LibTessDotNet.PQHandle);
				_n = 0;
				_data = null;
			}
		}

		public class Face : global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Face>
		{
			internal global::LibTessDotNet.MeshUtils.Face _prev;

			internal global::LibTessDotNet.MeshUtils.Face _next;

			internal global::LibTessDotNet.MeshUtils.Edge _anEdge;

			internal global::LibTessDotNet.MeshUtils.Face _trail;

			internal int _n;

			internal bool _marked;

			internal bool _inside;

			internal int VertsCount
			{
				get
				{
					int num = 0;
					global::LibTessDotNet.MeshUtils.Edge edge = _anEdge;
					do
					{
						num++;
						edge = edge._Lnext;
					}
					while (edge != _anEdge);
					return num;
				}
			}

			public override void Reset()
			{
				_prev = (_next = null);
				_anEdge = null;
				_trail = null;
				_n = 0;
				_marked = false;
				_inside = false;
			}
		}

		public struct EdgePair
		{
			internal global::LibTessDotNet.MeshUtils.Edge _e;

			internal global::LibTessDotNet.MeshUtils.Edge _eSym;

			public static global::LibTessDotNet.MeshUtils.EdgePair Create()
			{
				global::LibTessDotNet.MeshUtils.EdgePair edgePair = default(global::LibTessDotNet.MeshUtils.EdgePair);
				edgePair._e = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Edge>.Create();
				edgePair._e._pair = edgePair;
				edgePair._eSym = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Edge>.Create();
				edgePair._eSym._pair = edgePair;
				return edgePair;
			}

			public void Reset()
			{
				_e = (_eSym = null);
			}
		}

		public class Edge : global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Edge>
		{
			internal global::LibTessDotNet.MeshUtils.EdgePair _pair;

			internal global::LibTessDotNet.MeshUtils.Edge _next;

			internal global::LibTessDotNet.MeshUtils.Edge _Sym;

			internal global::LibTessDotNet.MeshUtils.Edge _Onext;

			internal global::LibTessDotNet.MeshUtils.Edge _Lnext;

			internal global::LibTessDotNet.MeshUtils.Vertex _Org;

			internal global::LibTessDotNet.MeshUtils.Face _Lface;

			internal global::LibTessDotNet.Tess.ActiveRegion _activeRegion;

			internal int _winding;

			internal global::LibTessDotNet.MeshUtils.Face _Rface
			{
				get
				{
					return _Sym._Lface;
				}
				set
				{
					_Sym._Lface = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Vertex _Dst
			{
				get
				{
					return _Sym._Org;
				}
				set
				{
					_Sym._Org = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Edge _Oprev
			{
				get
				{
					return _Sym._Lnext;
				}
				set
				{
					_Sym._Lnext = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Edge _Lprev
			{
				get
				{
					return _Onext._Sym;
				}
				set
				{
					_Onext._Sym = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Edge _Dprev
			{
				get
				{
					return _Lnext._Sym;
				}
				set
				{
					_Lnext._Sym = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Edge _Rprev
			{
				get
				{
					return _Sym._Onext;
				}
				set
				{
					_Sym._Onext = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Edge _Dnext
			{
				get
				{
					return _Rprev._Sym;
				}
				set
				{
					_Rprev._Sym = value;
				}
			}

			internal global::LibTessDotNet.MeshUtils.Edge _Rnext
			{
				get
				{
					return _Oprev._Sym;
				}
				set
				{
					_Oprev._Sym = value;
				}
			}

			internal static void EnsureFirst(ref global::LibTessDotNet.MeshUtils.Edge e)
			{
				if (e == e._pair._eSym)
				{
					e = e._Sym;
				}
			}

			public override void Reset()
			{
				_pair.Reset();
				_next = (_Sym = (_Onext = (_Lnext = null)));
				_Org = null;
				_Lface = null;
				_activeRegion = null;
				_winding = 0;
			}
		}

		public const int Undef = -1;

		public static global::LibTessDotNet.MeshUtils.Edge MakeEdge(global::LibTessDotNet.MeshUtils.Edge eNext)
		{
			global::LibTessDotNet.MeshUtils.EdgePair edgePair = global::LibTessDotNet.MeshUtils.EdgePair.Create();
			global::LibTessDotNet.MeshUtils.Edge e = edgePair._e;
			global::LibTessDotNet.MeshUtils.Edge eSym = edgePair._eSym;
			global::LibTessDotNet.MeshUtils.Edge.EnsureFirst(ref eNext);
			(eSym._next = eNext._Sym._next)._Sym._next = e;
			e._next = eNext;
			eNext._Sym._next = eSym;
			e._Sym = eSym;
			e._Onext = e;
			e._Lnext = eSym;
			e._Org = null;
			e._Lface = null;
			e._winding = 0;
			e._activeRegion = null;
			eSym._Sym = e;
			eSym._Onext = eSym;
			eSym._Lnext = e;
			eSym._Org = null;
			eSym._Lface = null;
			eSym._winding = 0;
			eSym._activeRegion = null;
			return e;
		}

		public static void Splice(global::LibTessDotNet.MeshUtils.Edge a, global::LibTessDotNet.MeshUtils.Edge b)
		{
			global::LibTessDotNet.MeshUtils.Edge onext = a._Onext;
			global::LibTessDotNet.MeshUtils.Edge onext2 = b._Onext;
			onext._Sym._Lnext = b;
			onext2._Sym._Lnext = a;
			a._Onext = onext2;
			b._Onext = onext;
		}

		public static void MakeVertex(global::LibTessDotNet.MeshUtils.Edge eOrig, global::LibTessDotNet.MeshUtils.Vertex vNext)
		{
			global::LibTessDotNet.MeshUtils.Vertex vertex = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Vertex>.Create();
			(vertex._prev = vNext._prev)._next = vertex;
			vertex._next = vNext;
			vNext._prev = vertex;
			vertex._anEdge = eOrig;
			global::LibTessDotNet.MeshUtils.Edge edge = eOrig;
			do
			{
				edge._Org = vertex;
				edge = edge._Onext;
			}
			while (edge != eOrig);
		}

		public static void MakeFace(global::LibTessDotNet.MeshUtils.Edge eOrig, global::LibTessDotNet.MeshUtils.Face fNext)
		{
			global::LibTessDotNet.MeshUtils.Face face = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Face>.Create();
			(face._prev = fNext._prev)._next = face;
			face._next = fNext;
			fNext._prev = face;
			face._anEdge = eOrig;
			face._trail = null;
			face._marked = false;
			face._inside = fNext._inside;
			global::LibTessDotNet.MeshUtils.Edge edge = eOrig;
			do
			{
				edge._Lface = face;
				edge = edge._Lnext;
			}
			while (edge != eOrig);
		}

		public static void KillEdge(global::LibTessDotNet.MeshUtils.Edge eDel)
		{
			global::LibTessDotNet.MeshUtils.Edge.EnsureFirst(ref eDel);
			global::LibTessDotNet.MeshUtils.Edge next = eDel._next;
			global::LibTessDotNet.MeshUtils.Edge next2 = eDel._Sym._next;
			next._Sym._next = next2;
			next2._Sym._next = next;
			eDel.Free();
		}

		public static void KillVertex(global::LibTessDotNet.MeshUtils.Vertex vDel, global::LibTessDotNet.MeshUtils.Vertex newOrg)
		{
			global::LibTessDotNet.MeshUtils.Edge anEdge = vDel._anEdge;
			global::LibTessDotNet.MeshUtils.Edge edge = anEdge;
			do
			{
				edge._Org = newOrg;
				edge = edge._Onext;
			}
			while (edge != anEdge);
			global::LibTessDotNet.MeshUtils.Vertex prev = vDel._prev;
			global::LibTessDotNet.MeshUtils.Vertex next = vDel._next;
			next._prev = prev;
			prev._next = next;
			vDel.Free();
		}

		public static void KillFace(global::LibTessDotNet.MeshUtils.Face fDel, global::LibTessDotNet.MeshUtils.Face newLFace)
		{
			global::LibTessDotNet.MeshUtils.Edge anEdge = fDel._anEdge;
			global::LibTessDotNet.MeshUtils.Edge edge = anEdge;
			do
			{
				edge._Lface = newLFace;
				edge = edge._Lnext;
			}
			while (edge != anEdge);
			global::LibTessDotNet.MeshUtils.Face prev = fDel._prev;
			global::LibTessDotNet.MeshUtils.Face next = fDel._next;
			next._prev = prev;
			prev._next = next;
			fDel.Free();
		}

		public static float FaceArea(global::LibTessDotNet.MeshUtils.Face f)
		{
			float num = 0f;
			global::LibTessDotNet.MeshUtils.Edge edge = f._anEdge;
			do
			{
				num += (edge._Org._s - edge._Dst._s) * (edge._Org._t + edge._Dst._t);
				edge = edge._Lnext;
			}
			while (edge != f._anEdge);
			return num;
		}
	}
}
