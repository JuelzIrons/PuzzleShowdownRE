namespace DG.Tweening.Plugins.Core.PathCore
{
	internal class CatmullRomDecoder : global::DG.Tweening.Plugins.Core.PathCore.ABSPathDecoder
	{
		private static readonly global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[] _PartialControlPs = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[2];

		private static readonly global::UnityEngine.Vector3[] _PartialWps = new global::UnityEngine.Vector3[2];

		internal override int minInputWaypoints => 1;

		internal override void FinalizePath(global::DG.Tweening.Plugins.Core.PathCore.Path p, global::UnityEngine.Vector3[] wps, bool isClosedPath)
		{
			int num = wps.Length;
			if (p.controlPoints == null || p.controlPoints.Length != 2)
			{
				p.controlPoints = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[2];
			}
			if (isClosedPath)
			{
				p.controlPoints[0] = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint(wps[num - 2], global::UnityEngine.Vector3.zero);
				p.controlPoints[1] = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint(wps[1], global::UnityEngine.Vector3.zero);
			}
			else
			{
				p.controlPoints[0] = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint(wps[1], global::UnityEngine.Vector3.zero);
				global::UnityEngine.Vector3 vector = wps[num - 1];
				global::UnityEngine.Vector3 vector2 = vector - wps[num - 2];
				p.controlPoints[1] = new global::DG.Tweening.Plugins.Core.PathCore.ControlPoint(vector + vector2, global::UnityEngine.Vector3.zero);
			}
			p.subdivisions = num * p.subdivisionsXSegment;
			SetTimeToLengthTables(p, p.subdivisions);
			SetWaypointsLengths(p, p.subdivisionsXSegment);
		}

		internal override global::UnityEngine.Vector3 GetPoint(float perc, global::UnityEngine.Vector3[] wps, global::DG.Tweening.Plugins.Core.PathCore.Path p, global::DG.Tweening.Plugins.Core.PathCore.ControlPoint[] controlPoints)
		{
			int num = wps.Length - 1;
			int num2 = (int)global::System.Math.Floor(perc * (float)num);
			int num3 = num - 1;
			if (num3 > num2)
			{
				num3 = num2;
			}
			float num4 = perc * (float)num - (float)num3;
			global::UnityEngine.Vector3 vector = ((num3 == 0) ? controlPoints[0].a : wps[num3 - 1]);
			global::UnityEngine.Vector3 vector2 = wps[num3];
			global::UnityEngine.Vector3 vector3 = wps[num3 + 1];
			global::UnityEngine.Vector3 vector4 = ((num3 + 2 > wps.Length - 1) ? controlPoints[1].a : wps[num3 + 2]);
			return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num4 * num4 * num4) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num4 * num4) + (-vector + vector3) * num4 + 2f * vector2);
		}

		internal void SetTimeToLengthTables(global::DG.Tweening.Plugins.Core.PathCore.Path p, int subdivisions)
		{
			float num = 0f;
			float num2 = 1f / (float)subdivisions;
			float[] array = new float[subdivisions];
			float[] array2 = new float[subdivisions];
			global::UnityEngine.Vector3 b = GetPoint(0f, p.wps, p, p.controlPoints);
			for (int i = 1; i < subdivisions + 1; i++)
			{
				float num3 = num2 * (float)i;
				global::UnityEngine.Vector3 point = GetPoint(num3, p.wps, p, p.controlPoints);
				num += global::UnityEngine.Vector3.Distance(point, b);
				b = point;
				array[i - 1] = num3;
				array2[i - 1] = num;
			}
			p.length = num;
			p.timesTable = array;
			p.lengthsTable = array2;
		}

		internal void SetWaypointsLengths(global::DG.Tweening.Plugins.Core.PathCore.Path p, int subdivisions)
		{
			int num = p.wps.Length;
			float[] array = new float[num];
			array[0] = 0f;
			for (int i = 1; i < num; i++)
			{
				_PartialControlPs[0].a = ((i == 1) ? p.controlPoints[0].a : p.wps[i - 2]);
				_PartialWps[0] = p.wps[i - 1];
				_PartialWps[1] = p.wps[i];
				_PartialControlPs[1].a = ((i == num - 1) ? p.controlPoints[1].a : p.wps[i + 1]);
				float num2 = 0f;
				float num3 = 1f / (float)subdivisions;
				global::UnityEngine.Vector3 b = GetPoint(0f, _PartialWps, p, _PartialControlPs);
				for (int j = 1; j < subdivisions + 1; j++)
				{
					float perc = num3 * (float)j;
					global::UnityEngine.Vector3 point = GetPoint(perc, _PartialWps, p, _PartialControlPs);
					num2 += global::UnityEngine.Vector3.Distance(point, b);
					b = point;
				}
				array[i] = num2;
			}
			p.wpLengths = array;
		}
	}
}
