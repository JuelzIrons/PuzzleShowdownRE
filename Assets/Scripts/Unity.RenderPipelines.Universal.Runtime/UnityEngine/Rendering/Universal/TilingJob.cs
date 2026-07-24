namespace UnityEngine.Rendering.Universal
{
	[global::Unity.Burst.BurstCompile(FloatMode = global::Unity.Burst.FloatMode.Default, DisableSafetyChecks = true, OptimizeFor = global::Unity.Burst.OptimizeFor.Performance)]
	internal struct TilingJob : global::Unity.Jobs.IJobFor
	{
		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleLight> lights;

		[global::Unity.Collections.ReadOnly]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.VisibleReflectionProbe> reflectionProbes;

		[global::Unity.Collections.ReadOnly]
		public bool reflectionProbeRotation;

		[global::Unity.Collections.NativeDisableParallelForRestriction]
		public global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.InclusiveRange> tileRanges;

		public int itemsPerTile;

		public int rangesPerItem;

		public global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4x4> worldToViews;

		public global::Unity.Mathematics.float2 tileScale;

		public global::Unity.Mathematics.float2 tileScaleInv;

		public global::UnityEngine.Rendering.Universal.Fixed2<float> viewPlaneBottoms;

		public global::UnityEngine.Rendering.Universal.Fixed2<float> viewPlaneTops;

		public global::UnityEngine.Rendering.Universal.Fixed2<global::Unity.Mathematics.float4> viewToViewportScaleBiases;

		public global::Unity.Mathematics.int2 tileCount;

		public float near;

		public bool isOrthographic;

		private global::UnityEngine.Rendering.Universal.InclusiveRange m_TileYRange;

		private int m_Offset;

		private int m_ViewIndex;

		private global::Unity.Mathematics.float2 m_CenterOffset;

		private static readonly global::Unity.Mathematics.float3[] k_CubePoints = new global::Unity.Mathematics.float3[8]
		{
			new global::Unity.Mathematics.float3(-1f, -1f, -1f),
			new global::Unity.Mathematics.float3(-1f, -1f, 1f),
			new global::Unity.Mathematics.float3(-1f, 1f, -1f),
			new global::Unity.Mathematics.float3(-1f, 1f, 1f),
			new global::Unity.Mathematics.float3(1f, -1f, -1f),
			new global::Unity.Mathematics.float3(1f, -1f, 1f),
			new global::Unity.Mathematics.float3(1f, 1f, -1f),
			new global::Unity.Mathematics.float3(1f, 1f, 1f)
		};

		private static readonly global::Unity.Mathematics.int4[] k_CubeLineIndices = new global::Unity.Mathematics.int4[4]
		{
			new global::Unity.Mathematics.int4(0, 4, 2, 1),
			new global::Unity.Mathematics.int4(3, 7, 1, 2),
			new global::Unity.Mathematics.int4(5, 1, 7, 4),
			new global::Unity.Mathematics.int4(6, 2, 4, 7)
		};

		public void Execute(int jobIndex)
		{
			int num = jobIndex % itemsPerTile;
			m_ViewIndex = jobIndex / itemsPerTile;
			m_Offset = jobIndex * rangesPerItem;
			m_TileYRange = new global::UnityEngine.Rendering.Universal.InclusiveRange(short.MaxValue, short.MinValue);
			for (int i = 0; i < rangesPerItem; i++)
			{
				tileRanges[m_Offset + i] = new global::UnityEngine.Rendering.Universal.InclusiveRange(short.MaxValue, short.MinValue);
			}
			if (num < lights.Length)
			{
				if (isOrthographic)
				{
					TileLightOrthographic(num);
				}
				else
				{
					TileLight(num);
				}
			}
			else
			{
				TileReflectionProbe(num);
			}
		}

		private void TileLight(int lightIndex)
		{
			global::UnityEngine.Rendering.VisibleLight light = lights[lightIndex];
			if (light.lightType != global::UnityEngine.LightType.Point && light.lightType != global::UnityEngine.LightType.Spot)
			{
				return;
			}
			global::Unity.Mathematics.float4x4 float4x5 = light.localToWorldMatrix;
			global::Unity.Mathematics.float3 lightPositionVS = global::Unity.Mathematics.math.mul(worldToViews[m_ViewIndex], global::Unity.Mathematics.math.float4(float4x5.c3.xyz, 1f)).xyz;
			lightPositionVS.z *= -1f;
			if (lightPositionVS.z >= near)
			{
				ExpandY(lightPositionVS);
			}
			global::Unity.Mathematics.float3 lightDirectionVS = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.mul(worldToViews[m_ViewIndex], global::Unity.Mathematics.math.float4(float4x5.c2.xyz, 0f)).xyz);
			lightDirectionVS.z *= -1f;
			float x = global::Unity.Mathematics.math.radians(light.spotAngle * 0.5f);
			float range = light.range;
			float num = square(range);
			float cosHalfAngle = global::Unity.Mathematics.math.cos(x);
			float coneHeight = cosHalfAngle * range;
			float clipRadius = global::Unity.Mathematics.math.sqrt(num - square(near - lightPositionVS.z));
			GetSphereHorizon(lightPositionVS.yz, range, near, clipRadius, out var p, out var p2);
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.float3(lightPositionVS.x, p);
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.float3(lightPositionVS.x, p2);
			if (SpherePointIsValid(float5))
			{
				ExpandY(float5);
			}
			if (SpherePointIsValid(float6))
			{
				ExpandY(float6);
			}
			GetSphereHorizon(lightPositionVS.xz, range, near, clipRadius, out var p3, out var p4);
			global::Unity.Mathematics.float3 float7 = global::Unity.Mathematics.math.float3(p3.x, lightPositionVS.y, p3.y);
			global::Unity.Mathematics.float3 float8 = global::Unity.Mathematics.math.float3(p4.x, lightPositionVS.y, p4.y);
			if (SpherePointIsValid(float7))
			{
				ExpandY(float7);
			}
			if (SpherePointIsValid(float8))
			{
				ExpandY(float8);
			}
			if (light.lightType == global::UnityEngine.LightType.Spot)
			{
				float num2 = global::Unity.Mathematics.math.sqrt(range * range - coneHeight * coneHeight);
				global::Unity.Mathematics.float3 float9 = lightPositionVS + lightDirectionVS * coneHeight;
				global::Unity.Mathematics.float3 float10 = ((global::Unity.Mathematics.math.abs(global::Unity.Mathematics.math.abs(lightDirectionVS.x) - 1f) < 1E-06f) ? global::Unity.Mathematics.math.float3(0f, 1f, 0f) : global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.cross(lightDirectionVS, global::Unity.Mathematics.math.float3(1f, 0f, 0f))));
				global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.cross(lightDirectionVS, float10);
				GetProjectedCircleHorizon(float9.yz, num2, float10.yz, float11.yz, out var uv, out var uv2);
				global::Unity.Mathematics.float3 positionVS = float9 + uv.x * float10 + uv.y * float11;
				global::Unity.Mathematics.float3 positionVS2 = float9 + uv2.x * float10 + uv2.y * float11;
				if (positionVS.z >= near)
				{
					ExpandY(positionVS);
				}
				if (positionVS2.z >= near)
				{
					ExpandY(positionVS2);
				}
				global::Unity.Mathematics.float3 float12 = ((global::Unity.Mathematics.math.abs(global::Unity.Mathematics.math.abs(lightDirectionVS.y) - 1f) < 1E-06f) ? global::Unity.Mathematics.math.float3(1f, 0f, 0f) : global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.cross(lightDirectionVS, global::Unity.Mathematics.math.float3(0f, 1f, 0f))));
				global::Unity.Mathematics.float3 float13 = global::Unity.Mathematics.math.cross(lightDirectionVS, float12);
				GetProjectedCircleHorizon(float9.xz, num2, float12.xz, float13.xz, out var uv3, out var uv4);
				global::Unity.Mathematics.float3 positionVS3 = float9 + uv3.x * float12 + uv3.y * float13;
				global::Unity.Mathematics.float3 positionVS4 = float9 + uv4.x * float12 + uv4.y * float13;
				if (positionVS3.z >= near)
				{
					ExpandY(positionVS3);
				}
				if (positionVS4.z >= near)
				{
					ExpandY(positionVS4);
				}
				if (GetCircleClipPoints(float9, lightDirectionVS, num2, near, out var p5, out var p6))
				{
					ExpandY(p5);
					ExpandY(p6);
				}
				float num3 = num2 * global::Unity.Mathematics.math.sqrt(1f - square(lightDirectionVS.z));
				bool flag = near >= global::Unity.Mathematics.math.min(float9.z - num3, lightPositionVS.z) && near <= global::Unity.Mathematics.math.max(float9.z + num3, lightPositionVS.z);
				global::Unity.Mathematics.float3 x2 = global::Unity.Mathematics.math.cross(lightDirectionVS, lightPositionVS);
				x2 = ((global::Unity.Mathematics.math.csum(x2) != 0f) ? global::Unity.Mathematics.math.normalize(x2) : global::Unity.Mathematics.math.float3(1f, 0f, 0f));
				global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.cross(lightDirectionVS, x2);
				if (flag)
				{
					float r = num2 / coneHeight;
					global::Unity.Mathematics.float2 float15 = FindNearConicTangentTheta(lightPositionVS.yz, lightDirectionVS.yz, r, x2.yz, float14.yz);
					global::Unity.Mathematics.float3 float16 = EvaluateNearConic(near, lightPositionVS, lightDirectionVS, r, x2, float14, float15.x);
					global::Unity.Mathematics.float3 float17 = EvaluateNearConic(near, lightPositionVS, lightDirectionVS, r, x2, float14, float15.y);
					if (ConicPointIsValid(float16))
					{
						ExpandY(float16);
					}
					if (ConicPointIsValid(float17))
					{
						ExpandY(float17);
					}
					global::Unity.Mathematics.float2 float18 = FindNearConicTangentTheta(lightPositionVS.xz, lightDirectionVS.xz, r, x2.xz, float14.xz);
					global::Unity.Mathematics.float3 float19 = EvaluateNearConic(near, lightPositionVS, lightDirectionVS, r, x2, float14, float18.x);
					global::Unity.Mathematics.float3 float20 = EvaluateNearConic(near, lightPositionVS, lightDirectionVS, r, x2, float14, float18.y);
					if (ConicPointIsValid(float19))
					{
						ExpandY(float19);
					}
					if (ConicPointIsValid(float20))
					{
						ExpandY(float20);
					}
				}
				GetConeSideTangentPoints(lightPositionVS, lightDirectionVS, cosHalfAngle, num2, coneHeight, range, x2, float14, out var l, out var l2);
				global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.float3(0f, 1f, viewPlaneBottoms[m_ViewIndex]);
				float num4 = global::Unity.Mathematics.math.dot(-lightPositionVS, y) / global::Unity.Mathematics.math.dot(l, y);
				global::Unity.Mathematics.float3 positionVS5 = lightPositionVS + l * num4;
				if (num4 >= 0f && num4 <= 1f && positionVS5.z >= near)
				{
					ExpandY(positionVS5);
				}
				global::Unity.Mathematics.float3 y2 = global::Unity.Mathematics.math.float3(0f, 1f, viewPlaneTops[m_ViewIndex]);
				float num5 = global::Unity.Mathematics.math.dot(-lightPositionVS, y2) / global::Unity.Mathematics.math.dot(l, y2);
				global::Unity.Mathematics.float3 positionVS6 = lightPositionVS + l * num5;
				if (num5 >= 0f && num5 <= 1f && positionVS6.z >= near)
				{
					ExpandY(positionVS6);
				}
				m_TileYRange.Clamp(0, (short)(tileCount.y - 1));
				for (int i = m_TileYRange.start + 1; i <= m_TileYRange.end; i++)
				{
					global::UnityEngine.Rendering.Universal.InclusiveRange empty = global::UnityEngine.Rendering.Universal.InclusiveRange.empty;
					float num6 = global::Unity.Mathematics.math.lerp(viewPlaneBottoms[m_ViewIndex], viewPlaneTops[m_ViewIndex], (float)i * tileScaleInv.y);
					global::Unity.Mathematics.float3 y3 = global::Unity.Mathematics.math.float3(0f, 1f, 0f - num6);
					float num7 = global::Unity.Mathematics.math.dot(-lightPositionVS, y3) / global::Unity.Mathematics.math.dot(l, y3);
					global::Unity.Mathematics.float3 positionVS7 = lightPositionVS + l * num7;
					if (num7 >= 0f && num7 <= 1f && positionVS7.z >= near)
					{
						empty.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(positionVS7).x, 0f, tileCount.x - 1));
					}
					float num8 = global::Unity.Mathematics.math.dot(-lightPositionVS, y3) / global::Unity.Mathematics.math.dot(l2, y3);
					global::Unity.Mathematics.float3 positionVS8 = lightPositionVS + l2 * num8;
					if (num8 >= 0f && num8 <= 1f && positionVS8.z >= near)
					{
						empty.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(positionVS8).x, 0f, tileCount.x - 1));
					}
					if (IntersectCircleYPlane(num6, float9, lightDirectionVS, float10, float11, num2, out var p7, out var p8))
					{
						if (p7.z >= near)
						{
							empty.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(p7).x, 0f, tileCount.x - 1));
						}
						if (p8.z >= near)
						{
							empty.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(p8).x, 0f, tileCount.x - 1));
						}
					}
					if (flag)
					{
						float y4 = num6 * near;
						float r2 = num2 / coneHeight;
						global::Unity.Mathematics.float2 float21 = FindNearConicYTheta(near, lightPositionVS, lightDirectionVS, r2, x2, float14, y4);
						global::Unity.Mathematics.float3 float22 = global::Unity.Mathematics.math.float3(EvaluateNearConic(near, lightPositionVS, lightDirectionVS, r2, x2, float14, float21.x).x, y4, near);
						global::Unity.Mathematics.float3 float23 = global::Unity.Mathematics.math.float3(EvaluateNearConic(near, lightPositionVS, lightDirectionVS, r2, x2, float14, float21.y).x, y4, near);
						if (ConicPointIsValid(float22))
						{
							empty.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(float22).x, 0f, tileCount.x - 1));
						}
						if (ConicPointIsValid(float23))
						{
							empty.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(float23).x, 0f, tileCount.x - 1));
						}
					}
					int num9 = m_Offset + 1 + i;
					tileRanges[num9] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num9], empty);
					tileRanges[num9 - 1] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num9 - 1], empty);
				}
			}
			m_TileYRange.Clamp(0, (short)(tileCount.y - 1));
			for (int j = m_TileYRange.start + 1; j <= m_TileYRange.end; j++)
			{
				global::UnityEngine.Rendering.Universal.InclusiveRange empty2 = global::UnityEngine.Rendering.Universal.InclusiveRange.empty;
				float y5 = global::Unity.Mathematics.math.lerp(viewPlaneBottoms[m_ViewIndex], viewPlaneTops[m_ViewIndex], (float)j * tileScaleInv.y);
				GetSphereYPlaneHorizon(lightPositionVS, range, near, clipRadius, y5, out var left, out var right);
				if (SpherePointIsValid(left))
				{
					empty2.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(left).x, 0f, tileCount.x - 1));
				}
				if (SpherePointIsValid(right))
				{
					empty2.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpace(right).x, 0f, tileCount.x - 1));
				}
				int num10 = m_Offset + 1 + j;
				tileRanges[num10] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num10], empty2);
				tileRanges[num10 - 1] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num10 - 1], empty2);
			}
			tileRanges[m_Offset] = m_TileYRange;
			bool ConicPointIsValid(global::Unity.Mathematics.float3 float24)
			{
				if (global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.normalize(float24 - lightPositionVS), lightDirectionVS) >= 0f)
				{
					return global::Unity.Mathematics.math.dot(float24 - lightPositionVS, lightDirectionVS) <= coneHeight;
				}
				return false;
			}
			bool SpherePointIsValid(global::Unity.Mathematics.float3 float24)
			{
				if (light.lightType != global::UnityEngine.LightType.Point)
				{
					return global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.normalize(float24 - lightPositionVS), lightDirectionVS) >= cosHalfAngle;
				}
				return true;
			}
		}

		private void TileLightOrthographic(int lightIndex)
		{
			global::UnityEngine.Rendering.VisibleLight light = lights[lightIndex];
			global::Unity.Mathematics.float4x4 float4x5 = light.localToWorldMatrix;
			global::Unity.Mathematics.float3 lightPosVS = global::Unity.Mathematics.math.mul(worldToViews[m_ViewIndex], global::Unity.Mathematics.math.float4(float4x5.c3.xyz, 1f)).xyz;
			lightPosVS.z *= -1f;
			ExpandOrthographic(lightPosVS);
			global::Unity.Mathematics.float3 lightDirVS = global::Unity.Mathematics.math.mul(worldToViews[m_ViewIndex], global::Unity.Mathematics.math.float4(float4x5.c2.xyz, 0f)).xyz;
			lightDirVS.z *= -1f;
			lightDirVS = global::Unity.Mathematics.math.normalize(lightDirVS);
			float x = global::Unity.Mathematics.math.radians(light.spotAngle * 0.5f);
			float range = light.range;
			float num = square(range);
			float cosHalfAngle = global::Unity.Mathematics.math.cos(x);
			float num2 = cosHalfAngle * range;
			float num3 = square(num2);
			float num4 = 1f / num2;
			float num5 = square(num4);
			global::Unity.Mathematics.float3 float5 = lightPosVS - global::Unity.Mathematics.math.float3(0f, range, 0f);
			global::Unity.Mathematics.float3 float6 = lightPosVS + global::Unity.Mathematics.math.float3(0f, range, 0f);
			global::Unity.Mathematics.float3 float7 = lightPosVS - global::Unity.Mathematics.math.float3(range, 0f, 0f);
			global::Unity.Mathematics.float3 float8 = lightPosVS + global::Unity.Mathematics.math.float3(range, 0f, 0f);
			if (SpherePointIsValid(float5))
			{
				ExpandOrthographic(float5);
			}
			if (SpherePointIsValid(float6))
			{
				ExpandOrthographic(float6);
			}
			if (SpherePointIsValid(float7))
			{
				ExpandOrthographic(float7);
			}
			if (SpherePointIsValid(float8))
			{
				ExpandOrthographic(float8);
			}
			global::Unity.Mathematics.float3 float9 = lightPosVS + lightDirVS * num2;
			float num6 = global::Unity.Mathematics.math.sqrt(num - num3);
			float num7 = square(num6);
			global::Unity.Mathematics.float3 float10 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float3(0f, 1f, 0f) - lightDirVS * lightDirVS.y);
			global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float3(1f, 0f, 0f) - lightDirVS * lightDirVS.x);
			global::Unity.Mathematics.float3 positionVS = float9 - float10 * num6;
			global::Unity.Mathematics.float3 positionVS2 = float9 + float10 * num6;
			if (light.lightType == global::UnityEngine.LightType.Spot)
			{
				global::Unity.Mathematics.float3 positionVS3 = float9 - float11 * num6;
				global::Unity.Mathematics.float3 positionVS4 = float9 + float11 * num6;
				ExpandOrthographic(positionVS);
				ExpandOrthographic(positionVS2);
				ExpandOrthographic(positionVS3);
				ExpandOrthographic(positionVS4);
			}
			m_TileYRange.Clamp(0, (short)(tileCount.y - 1));
			float num8 = 0f;
			float num9 = 0f;
			float num10 = 0f;
			float num11 = 0f;
			if (light.lightType == global::UnityEngine.LightType.Spot)
			{
				float num12 = num2 + num7 * num4;
				float x2 = global::Unity.Mathematics.math.sqrt(square(num7) * num5 + num7);
				float num13 = global::Unity.Mathematics.math.rcp(global::Unity.Mathematics.math.lengthsq(lightDirVS.xy));
				global::Unity.Mathematics.float2 float12 = (0f - num7) * num4 * num13 * lightDirVS.xy;
				global::Unity.Mathematics.float2 float13 = global::Unity.Mathematics.math.sqrt((square(x2) - global::Unity.Mathematics.math.lengthsq(float12)) * num13) * global::Unity.Mathematics.math.float2(lightDirVS.y, 0f - lightDirVS.x);
				global::Unity.Mathematics.float2 obj = lightPosVS.xy + num12 * lightDirVS.xy + float12;
				global::Unity.Mathematics.float2 float14 = obj - float13;
				global::Unity.Mathematics.float2 obj2 = obj + float13;
				num8 = float14.x - lightPosVS.x;
				num9 = global::Unity.Mathematics.math.rcp(float14.y - lightPosVS.y);
				num10 = obj2.x - lightPosVS.x;
				num11 = global::Unity.Mathematics.math.rcp(obj2.y - lightPosVS.y);
			}
			for (int i = m_TileYRange.start + 1; i <= m_TileYRange.end; i++)
			{
				global::UnityEngine.Rendering.Universal.InclusiveRange range2 = global::UnityEngine.Rendering.Universal.InclusiveRange.empty;
				float num14 = global::Unity.Mathematics.math.lerp(viewPlaneBottoms[m_ViewIndex], viewPlaneTops[m_ViewIndex], (float)i * tileScaleInv.y);
				float num15 = global::Unity.Mathematics.math.sqrt(num - square(num14 - lightPosVS.y));
				global::Unity.Mathematics.float3 p = global::Unity.Mathematics.math.float3(lightPosVS.x - num15, num14, lightPosVS.z);
				global::Unity.Mathematics.float3 p2 = global::Unity.Mathematics.math.float3(lightPosVS.x + num15, num14, lightPosVS.z);
				if (SpherePointIsValid(p))
				{
					ExpandRangeOrthographic(ref range2, p.x);
				}
				if (SpherePointIsValid(p2))
				{
					ExpandRangeOrthographic(ref range2, p2.x);
				}
				if (light.lightType == global::UnityEngine.LightType.Spot)
				{
					if (num14 >= positionVS.y && num14 <= positionVS2.y)
					{
						float num16 = (num14 - float9.y) / float10.y;
						float num17 = float9.x + num16 * float10.x;
						float num18 = (0f - lightDirVS.z) / global::Unity.Mathematics.math.length(global::Unity.Mathematics.math.float3(0f - lightDirVS.z, 0f, lightDirVS.x));
						float num19 = global::Unity.Mathematics.math.sqrt(square(num6) - square(num16));
						float xVS = num17 - num19 * num18;
						float xVS2 = num17 + num19 * num18;
						ExpandRangeOrthographic(ref range2, xVS);
						ExpandRangeOrthographic(ref range2, xVS2);
					}
					float num20 = num14 - lightPosVS.y;
					float num21 = num20 * num9;
					float num22 = num20 * num11;
					if (num21 >= 0f && num21 <= 1f)
					{
						ExpandRangeOrthographic(ref range2, lightPosVS.x + num21 * num8);
					}
					if (num22 >= 0f && num22 <= 1f)
					{
						ExpandRangeOrthographic(ref range2, lightPosVS.x + num22 * num10);
					}
				}
				int num23 = m_Offset + 1 + i;
				tileRanges[num23] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num23], range2);
				tileRanges[num23 - 1] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num23 - 1], range2);
			}
			tileRanges[m_Offset] = m_TileYRange;
			bool SpherePointIsValid(global::Unity.Mathematics.float3 float15)
			{
				if (light.lightType != global::UnityEngine.LightType.Point)
				{
					return global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.normalize(float15 - lightPosVS), lightDirVS) >= cosHalfAngle;
				}
				return true;
			}
		}

		private void TileReflectionProbe(int index)
		{
			global::UnityEngine.Rendering.VisibleReflectionProbe visibleReflectionProbe = reflectionProbes[index - lights.Length];
			global::Unity.Mathematics.float3 float5 = visibleReflectionProbe.bounds.center;
			global::Unity.Mathematics.float3 float6 = visibleReflectionProbe.bounds.extents;
			global::Unity.Mathematics.quaternion q = ((!reflectionProbeRotation) ? global::Unity.Mathematics.quaternion.identity : ((global::Unity.Mathematics.quaternion)visibleReflectionProbe.localToWorldMatrix.rotation));
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3> nativeArray = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float3>(k_CubePoints.Length, global::Unity.Collections.Allocator.Temp);
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> nativeArray2 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(k_CubePoints.Length + k_CubeLineIndices.Length * 3, global::Unity.Collections.Allocator.Temp);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < k_CubePoints.Length; i++)
			{
				global::Unity.Mathematics.float3 xyz = float5 + global::Unity.Mathematics.math.rotate(q, float6 * k_CubePoints[i]);
				global::Unity.Mathematics.float3 xyz2 = global::Unity.Mathematics.math.mul(worldToViews[m_ViewIndex], global::Unity.Mathematics.math.float4(xyz, 1f)).xyz;
				xyz2.z *= -1f;
				nativeArray[i] = xyz2;
				if (xyz2.z >= near)
				{
					global::Unity.Mathematics.float2 value = (isOrthographic ? xyz2.xy : (xyz2.xy / xyz2.z));
					int num3 = num++;
					nativeArray2[num3] = value;
					if (value.x < nativeArray2[num2].x)
					{
						num2 = num3;
					}
				}
			}
			for (int j = 0; j < k_CubeLineIndices.Length; j++)
			{
				global::Unity.Mathematics.int4 int5 = k_CubeLineIndices[j];
				global::Unity.Mathematics.float3 start = nativeArray[int5.x];
				for (int k = 0; k < 3; k++)
				{
					global::Unity.Mathematics.float3 end = nativeArray[int5[k + 1]];
					if ((!(start.z < near) || !(end.z < near)) && (start.z < near || end.z < near))
					{
						float t = (near - start.z) / (end.z - start.z);
						global::Unity.Mathematics.float3 float7 = global::Unity.Mathematics.math.lerp(start, end, t);
						global::Unity.Mathematics.float2 value2 = (isOrthographic ? float7.xy : (float7.xy / float7.z));
						int num4 = num++;
						nativeArray2[num4] = value2;
						if (value2.x < nativeArray2[num2].x)
						{
							num2 = num4;
						}
					}
				}
			}
			global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2> nativeArray3 = new global::Unity.Collections.NativeArray<global::Unity.Mathematics.float2>(num, global::Unity.Collections.Allocator.Temp);
			int num5 = 0;
			if (num > 0)
			{
				int num6 = num2;
				do
				{
					global::Unity.Mathematics.float2 float8 = nativeArray2[num6];
					ExpandY(global::Unity.Mathematics.math.float3(float8, 1f));
					nativeArray3[num5++] = float8;
					int num7 = 0;
					global::Unity.Mathematics.float2 float9 = nativeArray2[num7] - float8;
					for (int l = 0; l < num; l++)
					{
						global::Unity.Mathematics.float2 float10 = nativeArray2[l] - float8;
						float num8 = global::Unity.Mathematics.math.determinant(global::Unity.Mathematics.math.float2x2(float9, float10));
						if (num7 == num6 || num8 > 0f || (num8 == 0f && global::Unity.Mathematics.math.lengthsq(float10) > global::Unity.Mathematics.math.lengthsq(float9)))
						{
							num7 = l;
							float9 = float10;
						}
					}
					num6 = num7;
				}
				while (num6 != num2 && num5 < num);
				m_TileYRange.Clamp(0, (short)(tileCount.y - 1));
				for (int m = m_TileYRange.start + 1; m <= m_TileYRange.end; m++)
				{
					global::UnityEngine.Rendering.Universal.InclusiveRange empty = global::UnityEngine.Rendering.Universal.InclusiveRange.empty;
					float num9 = global::Unity.Mathematics.math.lerp(viewPlaneBottoms[m_ViewIndex], viewPlaneTops[m_ViewIndex], (float)m * tileScaleInv.y);
					for (int n = 0; n < num5; n++)
					{
						global::Unity.Mathematics.float2 float11 = nativeArray3[n];
						global::Unity.Mathematics.float2 float12 = nativeArray3[(n + 1) % num5];
						float num10 = (num9 - float11.y) / (float12.y - float11.y);
						if (!(num10 < 0f) && !(num10 > 1f))
						{
							global::Unity.Mathematics.float3 positionVS = global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.lerp(float11.x, float12.x, num10), num9, 1f);
							empty.Expand((short)global::Unity.Mathematics.math.clamp((isOrthographic ? ViewToTileSpaceOrthographic(positionVS) : ViewToTileSpace(positionVS)).x, 0f, tileCount.x - 1));
						}
					}
					int num11 = m_Offset + 1 + m;
					tileRanges[num11] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num11], empty);
					tileRanges[num11 - 1] = global::UnityEngine.Rendering.Universal.InclusiveRange.Merge(tileRanges[num11 - 1], empty);
				}
				tileRanges[m_Offset] = m_TileYRange;
			}
			nativeArray3.Dispose();
			nativeArray2.Dispose();
			nativeArray.Dispose();
		}

		private global::Unity.Mathematics.float2 ViewToTileSpace(global::Unity.Mathematics.float3 positionVS)
		{
			return (positionVS.xy / positionVS.z * viewToViewportScaleBiases[m_ViewIndex].xy + viewToViewportScaleBiases[m_ViewIndex].zw) * tileScale;
		}

		private global::Unity.Mathematics.float2 ViewToTileSpaceOrthographic(global::Unity.Mathematics.float3 positionVS)
		{
			return (positionVS.xy * viewToViewportScaleBiases[m_ViewIndex].xy + viewToViewportScaleBiases[m_ViewIndex].zw) * tileScale;
		}

		private void ExpandY(global::Unity.Mathematics.float3 positionVS)
		{
			global::Unity.Mathematics.float2 obj = ViewToTileSpace(positionVS);
			int num = (int)obj.y;
			int num2 = (int)obj.x;
			m_TileYRange.Expand((short)global::Unity.Mathematics.math.clamp(num, 0, tileCount.y - 1));
			if (num >= 0 && num < tileCount.y && num2 >= 0 && num2 < tileCount.x)
			{
				global::UnityEngine.Rendering.Universal.InclusiveRange value = tileRanges[m_Offset + 1 + num];
				value.Expand((short)num2);
				tileRanges[m_Offset + 1 + num] = value;
			}
		}

		private void ExpandOrthographic(global::Unity.Mathematics.float3 positionVS)
		{
			global::Unity.Mathematics.float2 obj = ViewToTileSpaceOrthographic(positionVS);
			int num = (int)obj.y;
			int num2 = (int)obj.x;
			m_TileYRange.Expand((short)global::Unity.Mathematics.math.clamp(num, 0, tileCount.y - 1));
			if (num >= 0 && num < tileCount.y && num2 >= 0 && num2 < tileCount.x)
			{
				global::UnityEngine.Rendering.Universal.InclusiveRange value = tileRanges[m_Offset + 1 + num];
				value.Expand((short)num2);
				tileRanges[m_Offset + 1 + num] = value;
			}
		}

		private void ExpandRangeOrthographic(ref global::UnityEngine.Rendering.Universal.InclusiveRange range, float xVS)
		{
			range.Expand((short)global::Unity.Mathematics.math.clamp(ViewToTileSpaceOrthographic(xVS).x, 0f, tileCount.x - 1));
		}

		private static float square(float x)
		{
			return x * x;
		}

		private static void GetSphereHorizon(global::Unity.Mathematics.float2 center, float radius, float near, float clipRadius, out global::Unity.Mathematics.float2 p0, out global::Unity.Mathematics.float2 p1)
		{
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.normalize(center);
			float num = global::Unity.Mathematics.math.length(center);
			float num2 = global::Unity.Mathematics.math.sqrt(num * num - radius * radius);
			float num3 = num2 * radius / num;
			global::Unity.Mathematics.float2 obj = float5 * (num2 * num3 / radius);
			p0 = global::Unity.Mathematics.math.float2(float.MinValue, 1f);
			p1 = global::Unity.Mathematics.math.float2(float.MaxValue, 1f);
			if (center.y - radius < near)
			{
				p0 = global::Unity.Mathematics.math.float2(center.x + clipRadius, near);
				p1 = global::Unity.Mathematics.math.float2(center.x - clipRadius, near);
			}
			global::Unity.Mathematics.float2 float6 = obj + global::Unity.Mathematics.math.float2(0f - float5.y, float5.x) * num3;
			if (square(num) >= square(radius) && float6.y >= near)
			{
				if (float6.x > p0.x)
				{
					p0 = float6;
				}
				if (float6.x < p1.x)
				{
					p1 = float6;
				}
			}
			global::Unity.Mathematics.float2 float7 = obj + global::Unity.Mathematics.math.float2(float5.y, 0f - float5.x) * num3;
			if (square(num) >= square(radius) && float7.y >= near)
			{
				if (float7.x > p0.x)
				{
					p0 = float7;
				}
				if (float7.x < p1.x)
				{
					p1 = float7;
				}
			}
		}

		private static void GetSphereYPlaneHorizon(global::Unity.Mathematics.float3 center, float sphereRadius, float near, float clipRadius, float y, out global::Unity.Mathematics.float3 left, out global::Unity.Mathematics.float3 right)
		{
			float num = y * near;
			float num2 = global::Unity.Mathematics.math.sqrt(square(clipRadius) - square(num - center.y));
			left = global::Unity.Mathematics.math.float3(center.x - num2, num, near);
			right = global::Unity.Mathematics.math.float3(center.x + num2, num, near);
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float3(0f, y, 1f));
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.float3(1f, 0f, 0f);
			float x = global::Unity.Mathematics.math.abs(global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float3(0f, 1f, 0f - y)), center));
			global::Unity.Mathematics.float2 obj = global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(center, float5), global::Unity.Mathematics.math.dot(center, float6));
			float num3 = global::Unity.Mathematics.math.length(obj);
			global::Unity.Mathematics.float2 float7 = obj / num3;
			float num4 = global::Unity.Mathematics.math.sqrt(square(sphereRadius) - square(x));
			if (square(x) <= square(sphereRadius) && square(num4) <= square(num3))
			{
				float num5 = global::Unity.Mathematics.math.sqrt(square(num3) - square(num4));
				float num6 = num5 * num4 / num3;
				global::Unity.Mathematics.float2 obj2 = float7 * (num5 * num6 / num4);
				global::Unity.Mathematics.float2 float8 = obj2 + global::Unity.Mathematics.math.float2(float7.y, 0f - float7.x) * num6;
				global::Unity.Mathematics.float2 float9 = obj2 + global::Unity.Mathematics.math.float2(0f - float7.y, float7.x) * num6;
				global::Unity.Mathematics.float3 float10 = float8.x * float5 + float8.y * float6;
				if (float10.z >= near)
				{
					left = float10;
				}
				global::Unity.Mathematics.float3 float11 = float9.x * float5 + float9.y * float6;
				if (float11.z >= near)
				{
					right = float11;
				}
			}
		}

		private static bool GetCircleClipPoints(global::Unity.Mathematics.float3 circleCenter, global::Unity.Mathematics.float3 circleNormal, float circleRadius, float near, out global::Unity.Mathematics.float3 p0, out global::Unity.Mathematics.float3 p1)
		{
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.cross(circleNormal, global::Unity.Mathematics.math.float3(0f, 0f, 1f)));
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.cross(float5, circleNormal);
			float num = (near - circleCenter.z) / float6.z;
			global::Unity.Mathematics.float3 float7 = circleCenter + float6 * num;
			float num2 = global::Unity.Mathematics.math.sqrt(square(circleRadius) - square(num));
			p0 = float7 + float5 * num2;
			p1 = float7 - float5 * num2;
			return global::Unity.Mathematics.math.abs(num) <= circleRadius;
		}

		private static (float, float) IntersectEllipseLine(float a, float b, global::Unity.Mathematics.float3 line)
		{
			float num = global::Unity.Mathematics.math.rcp(square(line.y) * square(b));
			float num2 = 1f / square(a) + square(line.x) * num;
			float num3 = 2f * line.x * line.z * num;
			float num4 = square(line.z) * num - 1f;
			float num5 = global::Unity.Mathematics.math.sqrt(num3 * num3 - 4f * num2 * num4);
			float item = (0f - num3 + num5) / (2f * num2);
			float item2 = (0f - num3 - num5) / (2f * num2);
			return (item, item2);
		}

		private static void GetProjectedCircleHorizon(global::Unity.Mathematics.float2 center, float radius, global::Unity.Mathematics.float2 U, global::Unity.Mathematics.float2 V, out global::Unity.Mathematics.float2 uv1, out global::Unity.Mathematics.float2 uv2)
		{
			float num = global::Unity.Mathematics.math.length(V);
			if (num < 1E-06f)
			{
				uv1 = global::Unity.Mathematics.math.float2(radius, 0f);
				uv2 = global::Unity.Mathematics.math.float2(0f - radius, 0f);
				return;
			}
			float num2 = global::Unity.Mathematics.math.length(U);
			float num3 = global::Unity.Mathematics.math.rcp(num2);
			float num4 = global::Unity.Mathematics.math.rcp(num);
			global::Unity.Mathematics.float2 y = U * num3;
			global::Unity.Mathematics.float2 y2 = V * num4;
			float num5 = num2 * radius;
			float num6 = num * radius;
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(-center, y), global::Unity.Mathematics.math.dot(-center, y2));
			global::Unity.Mathematics.float3 line = global::Unity.Mathematics.math.float3(float5.x / square(num5), float5.y / square(num6), -1f);
			(float, float) tuple = IntersectEllipseLine(num5, num6, line);
			float item = tuple.Item1;
			float item2 = tuple.Item2;
			uv1 = global::Unity.Mathematics.math.float2(item * num3, ((0f - line.x) / line.y * item - line.z / line.y) * num4);
			uv2 = global::Unity.Mathematics.math.float2(item2 * num3, ((0f - line.x) / line.y * item2 - line.z / line.y) * num4);
		}

		private static bool IntersectCircleYPlane(float y, global::Unity.Mathematics.float3 circleCenter, global::Unity.Mathematics.float3 circleNormal, global::Unity.Mathematics.float3 circleU, global::Unity.Mathematics.float3 circleV, float circleRadius, out global::Unity.Mathematics.float3 p1, out global::Unity.Mathematics.float3 p2)
		{
			p1 = (p2 = 0);
			float num = global::Unity.Mathematics.math.dot(circleCenter, circleNormal);
			global::Unity.Mathematics.float3 x = global::Unity.Mathematics.math.float3(1f, y, 1f) * num / global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.float3(1f, y, 1f), circleNormal) - circleCenter;
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(x, circleU), global::Unity.Mathematics.math.dot(x, circleV));
			global::Unity.Mathematics.float3 x2 = global::Unity.Mathematics.math.float3(-1f, y, 1f) * num / global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.float3(-1f, y, 1f), circleNormal) - circleCenter;
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(x2, circleU), global::Unity.Mathematics.math.dot(x2, circleV)) - float5);
			global::Unity.Mathematics.float2 float7 = global::Unity.Mathematics.math.float2(float6.y, 0f - float6.x);
			float num2 = global::Unity.Mathematics.math.dot(float5, float7);
			global::Unity.Mathematics.float2 float8 = float7 * num2;
			if (num2 > circleRadius)
			{
				return false;
			}
			float num3 = global::Unity.Mathematics.math.sqrt(circleRadius * circleRadius - num2 * num2);
			global::Unity.Mathematics.float2 float9 = float8 + num3 * float6;
			global::Unity.Mathematics.float2 float10 = float8 - num3 * float6;
			p1 = circleCenter + float9.x * circleU + float9.y * circleV;
			p2 = circleCenter + float10.x * circleU + float10.y * circleV;
			return true;
		}

		private static void GetConeSideTangentPoints(global::Unity.Mathematics.float3 vertex, global::Unity.Mathematics.float3 axis, float cosHalfAngle, float circleRadius, float coneHeight, float range, global::Unity.Mathematics.float3 circleU, global::Unity.Mathematics.float3 circleV, out global::Unity.Mathematics.float3 l1, out global::Unity.Mathematics.float3 l2)
		{
			l1 = (l2 = 0);
			if (!(global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.normalize(-vertex), axis) >= cosHalfAngle))
			{
				float num = 0f - global::Unity.Mathematics.math.dot(vertex, axis);
				if (num == 0f)
				{
					num = 1E-06f;
				}
				float num2 = ((num < 0f) ? (-1f) : 1f);
				global::Unity.Mathematics.float3 float5 = vertex + axis * num;
				float num3 = global::Unity.Mathematics.math.abs(num) * circleRadius / coneHeight;
				global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(circleU, -float5), global::Unity.Mathematics.math.dot(circleV, -float5)), 0f - square(num3));
				global::Unity.Mathematics.float2 float7 = global::Unity.Mathematics.math.float2(-1f, (0f - float6.x) / float6.y * -1f - float6.z / float6.y);
				global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.normalize(global::Unity.Mathematics.math.float2(1f, (0f - float6.x) / float6.y * 1f - float6.z / float6.y) - float7);
				global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(float8.y, 0f - float8.x);
				float num4 = global::Unity.Mathematics.math.dot(float7, float9);
				global::Unity.Mathematics.float2 obj = float9 * num4;
				float num5 = global::Unity.Mathematics.math.sqrt(num3 * num3 - num4 * num4);
				global::Unity.Mathematics.float2 float10 = obj + num5 * float8;
				global::Unity.Mathematics.float2 float11 = obj - num5 * float8;
				global::Unity.Mathematics.float3 float12 = global::Unity.Mathematics.math.normalize(float5 + float10.x * circleU + float10.y * circleV - vertex) * num2;
				global::Unity.Mathematics.float3 float13 = global::Unity.Mathematics.math.normalize(float5 + float11.x * circleU + float11.y * circleV - vertex) * num2;
				l1 = float12 * range;
				l2 = float13 * range;
			}
		}

		private static global::Unity.Mathematics.float3 EvaluateNearConic(float near, global::Unity.Mathematics.float3 o, global::Unity.Mathematics.float3 d, float r, global::Unity.Mathematics.float3 u, global::Unity.Mathematics.float3 v, float theta)
		{
			float num = (near - o.z) / (d.z + r * u.z * global::Unity.Mathematics.math.cos(theta) + r * v.z * global::Unity.Mathematics.math.sin(theta));
			return global::Unity.Mathematics.math.float3(o.xy + num * (d.xy + r * u.xy * global::Unity.Mathematics.math.cos(theta) + r * v.xy * global::Unity.Mathematics.math.sin(theta)), near);
		}

		private static global::Unity.Mathematics.float2 FindNearConicTangentTheta(global::Unity.Mathematics.float2 o, global::Unity.Mathematics.float2 d, float r, global::Unity.Mathematics.float2 u, global::Unity.Mathematics.float2 v)
		{
			float num = global::Unity.Mathematics.math.sqrt(square(d.x) * square(u.y) + square(d.x) * square(v.y) - 2f * d.x * d.y * u.x * u.y - 2f * d.x * d.y * v.x * v.y + square(d.y) * square(u.x) + square(d.y) * square(v.x) - square(r) * square(u.x) * square(v.y) + 2f * square(r) * u.x * u.y * v.x * v.y - square(r) * square(u.y) * square(v.x));
			float num2 = d.x * v.y - d.y * v.x - r * u.x * v.y + r * u.y * v.x;
			return 2f * global::Unity.Mathematics.math.atan(((0f - d.x) * u.y + d.y * u.x + global::Unity.Mathematics.math.float2(1f, -1f) * num) / num2);
		}

		private static global::Unity.Mathematics.float2 FindNearConicYTheta(float near, global::Unity.Mathematics.float3 o, global::Unity.Mathematics.float3 d, float r, global::Unity.Mathematics.float3 u, global::Unity.Mathematics.float3 v, float y)
		{
			float num = global::Unity.Mathematics.math.sqrt((0f - square(d.y)) * square(o.z) + 2f * square(d.y) * o.z * near - square(d.y) * square(near) + 2f * d.y * d.z * o.y * o.z - 2f * d.y * d.z * o.y * near - 2f * d.y * d.z * o.z * y + 2f * d.y * d.z * y * near - square(d.z) * square(o.y) + 2f * square(d.z) * o.y * y - square(d.z) * square(y) + square(o.y) * square(r) * square(u.z) + square(o.y) * square(r) * square(v.z) - 2f * o.y * o.z * square(r) * u.y * u.z - 2f * o.y * o.z * square(r) * v.y * v.z - 2f * o.y * y * square(r) * square(u.z) - 2f * o.y * y * square(r) * square(v.z) + 2f * o.y * square(r) * u.y * u.z * near + 2f * o.y * square(r) * v.y * v.z * near + square(o.z) * square(r) * square(u.y) + square(o.z) * square(r) * square(v.y) + 2f * o.z * y * square(r) * u.y * u.z + 2f * o.z * y * square(r) * v.y * v.z - 2f * o.z * square(r) * square(u.y) * near - 2f * o.z * square(r) * square(v.y) * near + square(y) * square(r) * square(u.z) + square(y) * square(r) * square(v.z) - 2f * y * square(r) * u.y * u.z * near - 2f * y * square(r) * v.y * v.z * near + square(r) * square(u.y) * square(near) + square(r) * square(v.y) * square(near));
			float num2 = d.y * o.z - d.y * near - d.z * o.y + d.z * y + o.y * r * u.z - o.z * r * u.y - y * r * u.z + r * u.y * near;
			return 2f * global::Unity.Mathematics.math.atan((r * (o.y * v.z - o.z * v.y - y * v.z + v.y * near) + global::Unity.Mathematics.math.float2(1f, -1f) * num) / num2);
		}
	}
}
