namespace LibTessDotNet
{
	internal class Mesh : global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.Mesh>
	{
		internal global::LibTessDotNet.MeshUtils.Vertex _vHead;

		internal global::LibTessDotNet.MeshUtils.Face _fHead;

		internal global::LibTessDotNet.MeshUtils.Edge _eHead;

		internal global::LibTessDotNet.MeshUtils.Edge _eHeadSym;

		public Mesh()
		{
			global::LibTessDotNet.MeshUtils.Vertex vertex = (_vHead = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Vertex>.Create());
			global::LibTessDotNet.MeshUtils.Face face = (_fHead = global::LibTessDotNet.MeshUtils.Pooled<global::LibTessDotNet.MeshUtils.Face>.Create());
			global::LibTessDotNet.MeshUtils.EdgePair edgePair = global::LibTessDotNet.MeshUtils.EdgePair.Create();
			global::LibTessDotNet.MeshUtils.Edge edge = (_eHead = edgePair._e);
			global::LibTessDotNet.MeshUtils.Edge edge2 = (_eHeadSym = edgePair._eSym);
			vertex._next = (vertex._prev = vertex);
			vertex._anEdge = null;
			face._next = (face._prev = face);
			face._anEdge = null;
			face._trail = null;
			face._marked = false;
			face._inside = false;
			edge._next = edge;
			edge._Sym = edge2;
			edge._Onext = null;
			edge._Lnext = null;
			edge._Org = null;
			edge._Lface = null;
			edge._winding = 0;
			edge._activeRegion = null;
			edge2._next = edge2;
			edge2._Sym = edge;
			edge2._Onext = null;
			edge2._Lnext = null;
			edge2._Org = null;
			edge2._Lface = null;
			edge2._winding = 0;
			edge2._activeRegion = null;
		}

		public override void Reset()
		{
			_vHead = null;
			_fHead = null;
			_eHead = (_eHeadSym = null);
		}

		public override void OnFree()
		{
			global::LibTessDotNet.MeshUtils.Face face = _fHead._next;
			global::LibTessDotNet.MeshUtils.Face fHead = _fHead;
			while (face != _fHead)
			{
				fHead = face._next;
				face.Free();
				face = fHead;
			}
			global::LibTessDotNet.MeshUtils.Vertex vertex = _vHead._next;
			global::LibTessDotNet.MeshUtils.Vertex vHead = _vHead;
			while (vertex != _vHead)
			{
				vHead = vertex._next;
				vertex.Free();
				vertex = vHead;
			}
			global::LibTessDotNet.MeshUtils.Edge edge = _eHead._next;
			global::LibTessDotNet.MeshUtils.Edge eHead = _eHead;
			while (edge != _eHead)
			{
				eHead = edge._next;
				edge.Free();
				edge = eHead;
			}
		}

		public global::LibTessDotNet.MeshUtils.Edge MakeEdge()
		{
			global::LibTessDotNet.MeshUtils.Edge edge = global::LibTessDotNet.MeshUtils.MakeEdge(_eHead);
			global::LibTessDotNet.MeshUtils.MakeVertex(edge, _vHead);
			global::LibTessDotNet.MeshUtils.MakeVertex(edge._Sym, _vHead);
			global::LibTessDotNet.MeshUtils.MakeFace(edge, _fHead);
			return edge;
		}

		public void Splice(global::LibTessDotNet.MeshUtils.Edge eOrg, global::LibTessDotNet.MeshUtils.Edge eDst)
		{
			if (eOrg != eDst)
			{
				bool flag = false;
				if (eDst._Org != eOrg._Org)
				{
					flag = true;
					global::LibTessDotNet.MeshUtils.KillVertex(eDst._Org, eOrg._Org);
				}
				bool flag2 = false;
				if (eDst._Lface != eOrg._Lface)
				{
					flag2 = true;
					global::LibTessDotNet.MeshUtils.KillFace(eDst._Lface, eOrg._Lface);
				}
				global::LibTessDotNet.MeshUtils.Splice(eDst, eOrg);
				if (!flag)
				{
					global::LibTessDotNet.MeshUtils.MakeVertex(eDst, eOrg._Org);
					eOrg._Org._anEdge = eOrg;
				}
				if (!flag2)
				{
					global::LibTessDotNet.MeshUtils.MakeFace(eDst, eOrg._Lface);
					eOrg._Lface._anEdge = eOrg;
				}
			}
		}

		public void Delete(global::LibTessDotNet.MeshUtils.Edge eDel)
		{
			global::LibTessDotNet.MeshUtils.Edge sym = eDel._Sym;
			bool flag = false;
			if (eDel._Lface != eDel._Rface)
			{
				flag = true;
				global::LibTessDotNet.MeshUtils.KillFace(eDel._Lface, eDel._Rface);
			}
			if (eDel._Onext == eDel)
			{
				global::LibTessDotNet.MeshUtils.KillVertex(eDel._Org, null);
			}
			else
			{
				eDel._Rface._anEdge = eDel._Oprev;
				eDel._Org._anEdge = eDel._Onext;
				global::LibTessDotNet.MeshUtils.Splice(eDel, eDel._Oprev);
				if (!flag)
				{
					global::LibTessDotNet.MeshUtils.MakeFace(eDel, eDel._Lface);
				}
			}
			if (sym._Onext == sym)
			{
				global::LibTessDotNet.MeshUtils.KillVertex(sym._Org, null);
				global::LibTessDotNet.MeshUtils.KillFace(sym._Lface, null);
			}
			else
			{
				eDel._Lface._anEdge = sym._Oprev;
				sym._Org._anEdge = sym._Onext;
				global::LibTessDotNet.MeshUtils.Splice(sym, sym._Oprev);
			}
			global::LibTessDotNet.MeshUtils.KillEdge(eDel);
		}

		public global::LibTessDotNet.MeshUtils.Edge AddEdgeVertex(global::LibTessDotNet.MeshUtils.Edge eOrg)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = global::LibTessDotNet.MeshUtils.MakeEdge(eOrg);
			global::LibTessDotNet.MeshUtils.Edge sym = edge._Sym;
			global::LibTessDotNet.MeshUtils.Splice(edge, eOrg._Lnext);
			edge._Org = eOrg._Dst;
			global::LibTessDotNet.MeshUtils.MakeVertex(sym, edge._Org);
			edge._Lface = (sym._Lface = eOrg._Lface);
			return edge;
		}

		public global::LibTessDotNet.MeshUtils.Edge SplitEdge(global::LibTessDotNet.MeshUtils.Edge eOrg)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = AddEdgeVertex(eOrg);
			global::LibTessDotNet.MeshUtils.Edge sym = edge._Sym;
			global::LibTessDotNet.MeshUtils.Splice(eOrg._Sym, eOrg._Sym._Oprev);
			global::LibTessDotNet.MeshUtils.Splice(eOrg._Sym, sym);
			eOrg._Dst = sym._Org;
			sym._Dst._anEdge = sym._Sym;
			sym._Rface = eOrg._Rface;
			sym._winding = eOrg._winding;
			sym._Sym._winding = eOrg._Sym._winding;
			return sym;
		}

		public global::LibTessDotNet.MeshUtils.Edge Connect(global::LibTessDotNet.MeshUtils.Edge eOrg, global::LibTessDotNet.MeshUtils.Edge eDst)
		{
			global::LibTessDotNet.MeshUtils.Edge edge = global::LibTessDotNet.MeshUtils.MakeEdge(eOrg);
			global::LibTessDotNet.MeshUtils.Edge sym = edge._Sym;
			bool flag = false;
			if (eDst._Lface != eOrg._Lface)
			{
				flag = true;
				global::LibTessDotNet.MeshUtils.KillFace(eDst._Lface, eOrg._Lface);
			}
			global::LibTessDotNet.MeshUtils.Splice(edge, eOrg._Lnext);
			global::LibTessDotNet.MeshUtils.Splice(sym, eDst);
			edge._Org = eOrg._Dst;
			sym._Org = eDst._Org;
			edge._Lface = (sym._Lface = eOrg._Lface);
			eOrg._Lface._anEdge = sym;
			if (!flag)
			{
				global::LibTessDotNet.MeshUtils.MakeFace(edge, eOrg._Lface);
			}
			return edge;
		}

		public void ZapFace(global::LibTessDotNet.MeshUtils.Face fZap)
		{
			global::LibTessDotNet.MeshUtils.Edge anEdge = fZap._anEdge;
			global::LibTessDotNet.MeshUtils.Edge lnext = anEdge._Lnext;
			global::LibTessDotNet.MeshUtils.Edge edge;
			do
			{
				edge = lnext;
				lnext = edge._Lnext;
				edge._Lface = null;
				if (edge._Rface == null)
				{
					if (edge._Onext == edge)
					{
						global::LibTessDotNet.MeshUtils.KillVertex(edge._Org, null);
					}
					else
					{
						edge._Org._anEdge = edge._Onext;
						global::LibTessDotNet.MeshUtils.Splice(edge, edge._Oprev);
					}
					global::LibTessDotNet.MeshUtils.Edge sym = edge._Sym;
					if (sym._Onext == sym)
					{
						global::LibTessDotNet.MeshUtils.KillVertex(sym._Org, null);
					}
					else
					{
						sym._Org._anEdge = sym._Onext;
						global::LibTessDotNet.MeshUtils.Splice(sym, sym._Oprev);
					}
					global::LibTessDotNet.MeshUtils.KillEdge(edge);
				}
			}
			while (edge != anEdge);
			global::LibTessDotNet.MeshUtils.Face prev = fZap._prev;
			global::LibTessDotNet.MeshUtils.Face next = fZap._next;
			next._prev = prev;
			prev._next = next;
			fZap.Free();
		}

		public void MergeConvexFaces(int maxVertsPerFace)
		{
			for (global::LibTessDotNet.MeshUtils.Face next = _fHead._next; next != _fHead; next = next._next)
			{
				if (next._inside)
				{
					global::LibTessDotNet.MeshUtils.Edge edge = next._anEdge;
					global::LibTessDotNet.MeshUtils.Vertex org = edge._Org;
					while (true)
					{
						global::LibTessDotNet.MeshUtils.Edge lnext = edge._Lnext;
						global::LibTessDotNet.MeshUtils.Edge sym = edge._Sym;
						if (sym != null && sym._Lface != null && sym._Lface._inside)
						{
							int vertsCount = next.VertsCount;
							int vertsCount2 = sym._Lface.VertsCount;
							if (vertsCount + vertsCount2 - 2 <= maxVertsPerFace && global::LibTessDotNet.Geom.VertCCW(edge._Lprev._Org, edge._Org, sym._Lnext._Lnext._Org) && global::LibTessDotNet.Geom.VertCCW(sym._Lprev._Org, sym._Org, edge._Lnext._Lnext._Org))
							{
								lnext = sym._Lnext;
								Delete(sym);
								edge = null;
							}
						}
						if (edge != null && edge._Lnext._Org == org)
						{
							break;
						}
						edge = lnext;
					}
				}
			}
		}

		[global::System.Diagnostics.Conditional("DEBUG")]
		public void Check()
		{
			global::LibTessDotNet.MeshUtils.Face fHead = _fHead;
			fHead = _fHead;
			global::LibTessDotNet.MeshUtils.Face next;
			global::LibTessDotNet.MeshUtils.Edge edge;
			while ((next = fHead._next) != _fHead)
			{
				edge = next._anEdge;
				do
				{
					edge = edge._Lnext;
				}
				while (edge != next._anEdge);
				fHead = next;
			}
			global::LibTessDotNet.MeshUtils.Vertex vHead = _vHead;
			vHead = _vHead;
			global::LibTessDotNet.MeshUtils.Vertex next2;
			while ((next2 = vHead._next) != _vHead)
			{
				edge = next2._anEdge;
				do
				{
					edge = edge._Onext;
				}
				while (edge != next2._anEdge);
				vHead = next2;
			}
			global::LibTessDotNet.MeshUtils.Edge eHead = _eHead;
			eHead = _eHead;
			while ((edge = eHead._next) != _eHead)
			{
				eHead = edge;
			}
		}
	}
}
