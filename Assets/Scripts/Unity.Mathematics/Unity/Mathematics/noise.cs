namespace Unity.Mathematics
{
	[global::Unity.IL2CPP.CompilerServices.Il2CppEagerStaticClassConstruction]
	public static class noise
	{
		public static global::Unity.Mathematics.float2 cellular(global::Unity.Mathematics.float2 P)
		{
			global::Unity.Mathematics.float2 float5 = mod289(global::Unity.Mathematics.math.floor(P));
			global::Unity.Mathematics.float2 obj = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.float3(-1f, 0f, 1f);
			global::Unity.Mathematics.float3 float7 = global::Unity.Mathematics.math.float3(-0.5f, 0.5f, 1.5f);
			global::Unity.Mathematics.float3 float8 = permute(float5.x + float6);
			global::Unity.Mathematics.float3 obj2 = permute(float8.x + float5.y + float6);
			global::Unity.Mathematics.float3 float9 = global::Unity.Mathematics.math.frac(obj2 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float10 = mod7(global::Unity.Mathematics.math.floor(obj2 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float11 = obj.x + 0.5f + 1f * float9;
			global::Unity.Mathematics.float3 float12 = obj.y - float7 + 1f * float10;
			global::Unity.Mathematics.float3 x = float11 * float11 + float12 * float12;
			global::Unity.Mathematics.float3 obj3 = permute(float8.y + float5.y + float6);
			float9 = global::Unity.Mathematics.math.frac(obj3 * (1f / 7f)) - 0.42857143f;
			float10 = mod7(global::Unity.Mathematics.math.floor(obj3 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			float11 = obj.x - 0.5f + 1f * float9;
			float12 = obj.y - float7 + 1f * float10;
			global::Unity.Mathematics.float3 y = float11 * float11 + float12 * float12;
			global::Unity.Mathematics.float3 obj4 = permute(float8.z + float5.y + float6);
			float9 = global::Unity.Mathematics.math.frac(obj4 * (1f / 7f)) - 0.42857143f;
			float10 = mod7(global::Unity.Mathematics.math.floor(obj4 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			float11 = obj.x - 1.5f + 1f * float9;
			float12 = obj.y - float7 + 1f * float10;
			global::Unity.Mathematics.float3 y2 = float11 * float11 + float12 * float12;
			global::Unity.Mathematics.float3 x2 = global::Unity.Mathematics.math.min(x, y);
			y = global::Unity.Mathematics.math.max(x, y);
			y = global::Unity.Mathematics.math.min(y, y2);
			x = global::Unity.Mathematics.math.min(x2, y);
			y = global::Unity.Mathematics.math.max(x2, y);
			x.xy = ((x.x < x.y) ? x.xy : x.yx);
			x.xz = ((x.x < x.z) ? x.xz : x.zx);
			x.yz = global::Unity.Mathematics.math.min(x.yz, y.yz);
			x.y = global::Unity.Mathematics.math.min(x.y, x.z);
			x.y = global::Unity.Mathematics.math.min(x.y, y.x);
			return global::Unity.Mathematics.math.sqrt(x.xy);
		}

		public static global::Unity.Mathematics.float2 cellular2x2(global::Unity.Mathematics.float2 P)
		{
			global::Unity.Mathematics.float2 float5 = mod289(global::Unity.Mathematics.math.floor(P));
			global::Unity.Mathematics.float2 obj = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float4 float6 = obj.x + global::Unity.Mathematics.math.float4(-0.5f, -1.5f, -0.5f, -1.5f);
			global::Unity.Mathematics.float4 float7 = obj.y + global::Unity.Mathematics.math.float4(-0.5f, -0.5f, -1.5f, -1.5f);
			global::Unity.Mathematics.float4 obj2 = permute(permute(float5.x + global::Unity.Mathematics.math.float4(0f, 1f, 0f, 1f)) + float5.y + global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f));
			global::Unity.Mathematics.float4 float8 = mod7(obj2) * (1f / 7f) + 1f / 14f;
			global::Unity.Mathematics.float4 float9 = mod7(global::Unity.Mathematics.math.floor(obj2 * (1f / 7f))) * (1f / 7f) + 1f / 14f;
			global::Unity.Mathematics.float4 obj3 = float6 + 0.8f * float8;
			global::Unity.Mathematics.float4 float10 = float7 + 0.8f * float9;
			global::Unity.Mathematics.float4 float11 = obj3 * obj3 + float10 * float10;
			float11.xy = ((float11.x < float11.y) ? float11.xy : float11.yx);
			float11.xz = ((float11.x < float11.z) ? float11.xz : float11.zx);
			float11.xw = ((float11.x < float11.w) ? float11.xw : float11.wx);
			float11.y = global::Unity.Mathematics.math.min(float11.y, float11.z);
			float11.y = global::Unity.Mathematics.math.min(float11.y, float11.w);
			return global::Unity.Mathematics.math.sqrt(float11.xy);
		}

		public static global::Unity.Mathematics.float2 cellular2x2x2(global::Unity.Mathematics.float3 P)
		{
			global::Unity.Mathematics.float3 float5 = mod289(global::Unity.Mathematics.math.floor(P));
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float4 float7 = float6.x + global::Unity.Mathematics.math.float4(0f, -1f, 0f, -1f);
			global::Unity.Mathematics.float4 float8 = float6.y + global::Unity.Mathematics.math.float4(0f, 0f, -1f, -1f);
			global::Unity.Mathematics.float4 obj = permute(permute(float5.x + global::Unity.Mathematics.math.float4(0f, 1f, 0f, 1f)) + float5.y + global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f));
			global::Unity.Mathematics.float4 float9 = permute(obj + float5.z);
			global::Unity.Mathematics.float4 obj2 = permute(obj + float5.z + global::Unity.Mathematics.math.float4(1f, 1f, 1f, 1f));
			global::Unity.Mathematics.float4 float10 = global::Unity.Mathematics.math.frac(float9 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float4 float11 = mod7(global::Unity.Mathematics.math.floor(float9 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float4 float12 = global::Unity.Mathematics.math.floor(float9 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float4 float13 = global::Unity.Mathematics.math.frac(obj2 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float4 float14 = mod7(global::Unity.Mathematics.math.floor(obj2 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float4 float15 = global::Unity.Mathematics.math.floor(obj2 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float4 float16 = float7 + 0.8f * float10;
			global::Unity.Mathematics.float4 float17 = float8 + 0.8f * float11;
			global::Unity.Mathematics.float4 float18 = float6.z + 0.8f * float12;
			global::Unity.Mathematics.float4 float19 = float7 + 0.8f * float13;
			global::Unity.Mathematics.float4 float20 = float8 + 0.8f * float14;
			global::Unity.Mathematics.float4 float21 = float6.z - 1f + 0.8f * float15;
			global::Unity.Mathematics.float4 x = float16 * float16 + float17 * float17 + float18 * float18;
			global::Unity.Mathematics.float4 y = float19 * float19 + float20 * float20 + float21 * float21;
			global::Unity.Mathematics.float4 float22 = global::Unity.Mathematics.math.min(x, y);
			y = global::Unity.Mathematics.math.max(x, y);
			float22.xy = ((float22.x < float22.y) ? float22.xy : float22.yx);
			float22.xz = ((float22.x < float22.z) ? float22.xz : float22.zx);
			float22.xw = ((float22.x < float22.w) ? float22.xw : float22.wx);
			float22.yzw = global::Unity.Mathematics.math.min(float22.yzw, y.yzw);
			float22.y = global::Unity.Mathematics.math.min(float22.y, float22.z);
			float22.y = global::Unity.Mathematics.math.min(float22.y, float22.w);
			float22.y = global::Unity.Mathematics.math.min(float22.y, y.x);
			return global::Unity.Mathematics.math.sqrt(float22.xy);
		}

		public static global::Unity.Mathematics.float2 cellular(global::Unity.Mathematics.float3 P)
		{
			global::Unity.Mathematics.float3 float5 = mod289(global::Unity.Mathematics.math.floor(P));
			global::Unity.Mathematics.float3 obj = global::Unity.Mathematics.math.frac(P) - 0.5f;
			global::Unity.Mathematics.float3 float6 = obj.x + global::Unity.Mathematics.math.float3(1f, 0f, -1f);
			global::Unity.Mathematics.float3 float7 = obj.y + global::Unity.Mathematics.math.float3(1f, 0f, -1f);
			global::Unity.Mathematics.float3 float8 = obj.z + global::Unity.Mathematics.math.float3(1f, 0f, -1f);
			global::Unity.Mathematics.float3 obj2 = permute(float5.x + global::Unity.Mathematics.math.float3(-1f, 0f, 1f));
			global::Unity.Mathematics.float3 float9 = permute(obj2 + float5.y - 1f);
			global::Unity.Mathematics.float3 float10 = permute(obj2 + float5.y);
			global::Unity.Mathematics.float3 obj3 = permute(obj2 + float5.y + 1f);
			global::Unity.Mathematics.float3 float11 = permute(float9 + float5.z - 1f);
			global::Unity.Mathematics.float3 float12 = permute(float9 + float5.z);
			global::Unity.Mathematics.float3 float13 = permute(float9 + float5.z + 1f);
			global::Unity.Mathematics.float3 float14 = permute(float10 + float5.z - 1f);
			global::Unity.Mathematics.float3 float15 = permute(float10 + float5.z);
			global::Unity.Mathematics.float3 float16 = permute(float10 + float5.z + 1f);
			global::Unity.Mathematics.float3 float17 = permute(obj3 + float5.z - 1f);
			global::Unity.Mathematics.float3 float18 = permute(obj3 + float5.z);
			global::Unity.Mathematics.float3 obj4 = permute(obj3 + float5.z + 1f);
			global::Unity.Mathematics.float3 float19 = global::Unity.Mathematics.math.frac(float11 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float20 = mod7(global::Unity.Mathematics.math.floor(float11 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float21 = global::Unity.Mathematics.math.floor(float11 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float22 = global::Unity.Mathematics.math.frac(float12 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float23 = mod7(global::Unity.Mathematics.math.floor(float12 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float24 = global::Unity.Mathematics.math.floor(float12 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float25 = global::Unity.Mathematics.math.frac(float13 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float26 = mod7(global::Unity.Mathematics.math.floor(float13 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float27 = global::Unity.Mathematics.math.floor(float13 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float28 = global::Unity.Mathematics.math.frac(float14 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float29 = mod7(global::Unity.Mathematics.math.floor(float14 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float30 = global::Unity.Mathematics.math.floor(float14 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float31 = global::Unity.Mathematics.math.frac(float15 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float32 = mod7(global::Unity.Mathematics.math.floor(float15 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float33 = global::Unity.Mathematics.math.floor(float15 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float34 = global::Unity.Mathematics.math.frac(float16 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float35 = mod7(global::Unity.Mathematics.math.floor(float16 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float36 = global::Unity.Mathematics.math.floor(float16 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float37 = global::Unity.Mathematics.math.frac(float17 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float38 = mod7(global::Unity.Mathematics.math.floor(float17 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float39 = global::Unity.Mathematics.math.floor(float17 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float40 = global::Unity.Mathematics.math.frac(float18 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float41 = mod7(global::Unity.Mathematics.math.floor(float18 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float42 = global::Unity.Mathematics.math.floor(float18 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float43 = global::Unity.Mathematics.math.frac(obj4 * (1f / 7f)) - 0.42857143f;
			global::Unity.Mathematics.float3 float44 = mod7(global::Unity.Mathematics.math.floor(obj4 * (1f / 7f))) * (1f / 7f) - 0.42857143f;
			global::Unity.Mathematics.float3 float45 = global::Unity.Mathematics.math.floor(obj4 * (1f / 49f)) * (1f / 6f) - 5f / 12f;
			global::Unity.Mathematics.float3 float46 = float6 + 1f * float19;
			global::Unity.Mathematics.float3 float47 = float7.x + 1f * float20;
			global::Unity.Mathematics.float3 float48 = float8.x + 1f * float21;
			global::Unity.Mathematics.float3 float49 = float6 + 1f * float22;
			global::Unity.Mathematics.float3 float50 = float7.x + 1f * float23;
			global::Unity.Mathematics.float3 float51 = float8.y + 1f * float24;
			global::Unity.Mathematics.float3 float52 = float6 + 1f * float25;
			global::Unity.Mathematics.float3 float53 = float7.x + 1f * float26;
			global::Unity.Mathematics.float3 float54 = float8.z + 1f * float27;
			global::Unity.Mathematics.float3 float55 = float6 + 1f * float28;
			global::Unity.Mathematics.float3 float56 = float7.y + 1f * float29;
			global::Unity.Mathematics.float3 float57 = float8.x + 1f * float30;
			global::Unity.Mathematics.float3 float58 = float6 + 1f * float31;
			global::Unity.Mathematics.float3 float59 = float7.y + 1f * float32;
			global::Unity.Mathematics.float3 float60 = float8.y + 1f * float33;
			global::Unity.Mathematics.float3 float61 = float6 + 1f * float34;
			global::Unity.Mathematics.float3 float62 = float7.y + 1f * float35;
			global::Unity.Mathematics.float3 float63 = float8.z + 1f * float36;
			global::Unity.Mathematics.float3 float64 = float6 + 1f * float37;
			global::Unity.Mathematics.float3 float65 = float7.z + 1f * float38;
			global::Unity.Mathematics.float3 float66 = float8.x + 1f * float39;
			global::Unity.Mathematics.float3 float67 = float6 + 1f * float40;
			global::Unity.Mathematics.float3 float68 = float7.z + 1f * float41;
			global::Unity.Mathematics.float3 float69 = float8.y + 1f * float42;
			global::Unity.Mathematics.float3 obj5 = float6 + 1f * float43;
			global::Unity.Mathematics.float3 float70 = float7.z + 1f * float44;
			global::Unity.Mathematics.float3 float71 = float8.z + 1f * float45;
			global::Unity.Mathematics.float3 x = float46 * float46 + float47 * float47 + float48 * float48;
			global::Unity.Mathematics.float3 y = float49 * float49 + float50 * float50 + float51 * float51;
			global::Unity.Mathematics.float3 y2 = float52 * float52 + float53 * float53 + float54 * float54;
			global::Unity.Mathematics.float3 x2 = float55 * float55 + float56 * float56 + float57 * float57;
			global::Unity.Mathematics.float3 y3 = float58 * float58 + float59 * float59 + float60 * float60;
			global::Unity.Mathematics.float3 y4 = float61 * float61 + float62 * float62 + float63 * float63;
			global::Unity.Mathematics.float3 x3 = float64 * float64 + float65 * float65 + float66 * float66;
			global::Unity.Mathematics.float3 y5 = float67 * float67 + float68 * float68 + float69 * float69;
			global::Unity.Mathematics.float3 y6 = obj5 * obj5 + float70 * float70 + float71 * float71;
			global::Unity.Mathematics.float3 x4 = global::Unity.Mathematics.math.min(x, y);
			y = global::Unity.Mathematics.math.max(x, y);
			x = global::Unity.Mathematics.math.min(x4, y2);
			y2 = global::Unity.Mathematics.math.max(x4, y2);
			y = global::Unity.Mathematics.math.min(y, y2);
			global::Unity.Mathematics.float3 x5 = global::Unity.Mathematics.math.min(x2, y3);
			y3 = global::Unity.Mathematics.math.max(x2, y3);
			x2 = global::Unity.Mathematics.math.min(x5, y4);
			y4 = global::Unity.Mathematics.math.max(x5, y4);
			y3 = global::Unity.Mathematics.math.min(y3, y4);
			global::Unity.Mathematics.float3 x6 = global::Unity.Mathematics.math.min(x3, y5);
			y5 = global::Unity.Mathematics.math.max(x3, y5);
			x3 = global::Unity.Mathematics.math.min(x6, y6);
			y6 = global::Unity.Mathematics.math.max(x6, y6);
			y5 = global::Unity.Mathematics.math.min(y5, y6);
			global::Unity.Mathematics.float3 x7 = global::Unity.Mathematics.math.min(x, x2);
			x2 = global::Unity.Mathematics.math.max(x, x2);
			x = global::Unity.Mathematics.math.min(x7, x3);
			x3 = global::Unity.Mathematics.math.max(x7, x3);
			x.xy = ((x.x < x.y) ? x.xy : x.yx);
			x.xz = ((x.x < x.z) ? x.xz : x.zx);
			y = global::Unity.Mathematics.math.min(y, x2);
			y = global::Unity.Mathematics.math.min(y, y3);
			y = global::Unity.Mathematics.math.min(y, x3);
			y = global::Unity.Mathematics.math.min(y, y5);
			x.yz = global::Unity.Mathematics.math.min(x.yz, y.xy);
			x.y = global::Unity.Mathematics.math.min(x.y, y.z);
			x.y = global::Unity.Mathematics.math.min(x.y, x.z);
			return global::Unity.Mathematics.math.sqrt(x.xy);
		}

		public static float cnoise(global::Unity.Mathematics.float2 P)
		{
			global::Unity.Mathematics.float4 x = global::Unity.Mathematics.math.floor(P.xyxy) + global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f);
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.frac(P.xyxy) - global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f);
			x = mod289(x);
			global::Unity.Mathematics.float4 xzxz = x.xzxz;
			global::Unity.Mathematics.float4 yyww = x.yyww;
			global::Unity.Mathematics.float4 xzxz2 = float5.xzxz;
			global::Unity.Mathematics.float4 yyww2 = float5.yyww;
			global::Unity.Mathematics.float4 obj = global::Unity.Mathematics.math.frac(permute(permute(xzxz) + yyww) * (1f / 41f)) * 2f - 1f;
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.abs(obj) - 0.5f;
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.floor(obj + 0.5f);
			global::Unity.Mathematics.float4 obj2 = obj - float7;
			global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.float2(obj2.x, float6.x);
			global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(obj2.y, float6.y);
			global::Unity.Mathematics.float2 float10 = global::Unity.Mathematics.math.float2(obj2.z, float6.z);
			global::Unity.Mathematics.float2 float11 = global::Unity.Mathematics.math.float2(obj2.w, float6.w);
			global::Unity.Mathematics.float4 float12 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float8, float8), global::Unity.Mathematics.math.dot(float10, float10), global::Unity.Mathematics.math.dot(float9, float9), global::Unity.Mathematics.math.dot(float11, float11)));
			float8 *= float12.x;
			float10 *= float12.y;
			float9 *= float12.z;
			float11 *= float12.w;
			float x2 = global::Unity.Mathematics.math.dot(float8, global::Unity.Mathematics.math.float2(xzxz2.x, yyww2.x));
			float x3 = global::Unity.Mathematics.math.dot(float9, global::Unity.Mathematics.math.float2(xzxz2.y, yyww2.y));
			float y = global::Unity.Mathematics.math.dot(float10, global::Unity.Mathematics.math.float2(xzxz2.z, yyww2.z));
			float y2 = global::Unity.Mathematics.math.dot(float11, global::Unity.Mathematics.math.float2(xzxz2.w, yyww2.w));
			global::Unity.Mathematics.float2 float13 = fade(float5.xy);
			global::Unity.Mathematics.float2 float14 = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float2(x2, y), global::Unity.Mathematics.math.float2(x3, y2), float13.x);
			float num = global::Unity.Mathematics.math.lerp(float14.x, float14.y, float13.y);
			return 2.3f * num;
		}

		public static float pnoise(global::Unity.Mathematics.float2 P, global::Unity.Mathematics.float2 rep)
		{
			global::Unity.Mathematics.float4 x = global::Unity.Mathematics.math.floor(P.xyxy) + global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f);
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.frac(P.xyxy) - global::Unity.Mathematics.math.float4(0f, 0f, 1f, 1f);
			x = global::Unity.Mathematics.math.fmod(x, rep.xyxy);
			x = mod289(x);
			global::Unity.Mathematics.float4 xzxz = x.xzxz;
			global::Unity.Mathematics.float4 yyww = x.yyww;
			global::Unity.Mathematics.float4 xzxz2 = float5.xzxz;
			global::Unity.Mathematics.float4 yyww2 = float5.yyww;
			global::Unity.Mathematics.float4 obj = global::Unity.Mathematics.math.frac(permute(permute(xzxz) + yyww) * (1f / 41f)) * 2f - 1f;
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.abs(obj) - 0.5f;
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.floor(obj + 0.5f);
			global::Unity.Mathematics.float4 obj2 = obj - float7;
			global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.float2(obj2.x, float6.x);
			global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(obj2.y, float6.y);
			global::Unity.Mathematics.float2 float10 = global::Unity.Mathematics.math.float2(obj2.z, float6.z);
			global::Unity.Mathematics.float2 float11 = global::Unity.Mathematics.math.float2(obj2.w, float6.w);
			global::Unity.Mathematics.float4 float12 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float8, float8), global::Unity.Mathematics.math.dot(float10, float10), global::Unity.Mathematics.math.dot(float9, float9), global::Unity.Mathematics.math.dot(float11, float11)));
			float8 *= float12.x;
			float10 *= float12.y;
			float9 *= float12.z;
			float11 *= float12.w;
			float x2 = global::Unity.Mathematics.math.dot(float8, global::Unity.Mathematics.math.float2(xzxz2.x, yyww2.x));
			float x3 = global::Unity.Mathematics.math.dot(float9, global::Unity.Mathematics.math.float2(xzxz2.y, yyww2.y));
			float y = global::Unity.Mathematics.math.dot(float10, global::Unity.Mathematics.math.float2(xzxz2.z, yyww2.z));
			float y2 = global::Unity.Mathematics.math.dot(float11, global::Unity.Mathematics.math.float2(xzxz2.w, yyww2.w));
			global::Unity.Mathematics.float2 float13 = fade(float5.xy);
			global::Unity.Mathematics.float2 float14 = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float2(x2, y), global::Unity.Mathematics.math.float2(x3, y2), float13.x);
			float num = global::Unity.Mathematics.math.lerp(float14.x, float14.y, float13.y);
			return 2.3f * num;
		}

		public static float cnoise(global::Unity.Mathematics.float3 P)
		{
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.floor(P);
			global::Unity.Mathematics.float3 x = float5 + global::Unity.Mathematics.math.float3(1f);
			float5 = mod289(float5);
			x = mod289(x);
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float3 y = float6 - global::Unity.Mathematics.math.float3(1f);
			global::Unity.Mathematics.float4 x2 = global::Unity.Mathematics.math.float4(float5.x, x.x, float5.x, x.x);
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.float4(float5.yy, x.yy);
			global::Unity.Mathematics.float4 zzzz = float5.zzzz;
			global::Unity.Mathematics.float4 zzzz2 = x.zzzz;
			global::Unity.Mathematics.float4 obj = permute(permute(x2) + float7);
			global::Unity.Mathematics.float4 float8 = permute(obj + zzzz);
			global::Unity.Mathematics.float4 obj2 = permute(obj + zzzz2);
			global::Unity.Mathematics.float4 x3 = float8 * (1f / 7f);
			global::Unity.Mathematics.float4 x4 = global::Unity.Mathematics.math.frac(global::Unity.Mathematics.math.floor(x3) * (1f / 7f)) - 0.5f;
			x3 = global::Unity.Mathematics.math.frac(x3);
			global::Unity.Mathematics.float4 threshold = global::Unity.Mathematics.math.float4(0.5f) - global::Unity.Mathematics.math.abs(x3) - global::Unity.Mathematics.math.abs(x4);
			global::Unity.Mathematics.float4 float9 = global::Unity.Mathematics.math.step(threshold, global::Unity.Mathematics.math.float4(0f));
			x3 -= float9 * (global::Unity.Mathematics.math.step(0f, x3) - 0.5f);
			x4 -= float9 * (global::Unity.Mathematics.math.step(0f, x4) - 0.5f);
			global::Unity.Mathematics.float4 x5 = obj2 * (1f / 7f);
			global::Unity.Mathematics.float4 x6 = global::Unity.Mathematics.math.frac(global::Unity.Mathematics.math.floor(x5) * (1f / 7f)) - 0.5f;
			x5 = global::Unity.Mathematics.math.frac(x5);
			global::Unity.Mathematics.float4 threshold2 = global::Unity.Mathematics.math.float4(0.5f) - global::Unity.Mathematics.math.abs(x5) - global::Unity.Mathematics.math.abs(x6);
			global::Unity.Mathematics.float4 float10 = global::Unity.Mathematics.math.step(threshold2, global::Unity.Mathematics.math.float4(0f));
			x5 -= float10 * (global::Unity.Mathematics.math.step(0f, x5) - 0.5f);
			x6 -= float10 * (global::Unity.Mathematics.math.step(0f, x6) - 0.5f);
			global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.float3(x3.x, x4.x, threshold.x);
			global::Unity.Mathematics.float3 float12 = global::Unity.Mathematics.math.float3(x3.y, x4.y, threshold.y);
			global::Unity.Mathematics.float3 float13 = global::Unity.Mathematics.math.float3(x3.z, x4.z, threshold.z);
			global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.float3(x3.w, x4.w, threshold.w);
			global::Unity.Mathematics.float3 float15 = global::Unity.Mathematics.math.float3(x5.x, x6.x, threshold2.x);
			global::Unity.Mathematics.float3 float16 = global::Unity.Mathematics.math.float3(x5.y, x6.y, threshold2.y);
			global::Unity.Mathematics.float3 float17 = global::Unity.Mathematics.math.float3(x5.z, x6.z, threshold2.z);
			global::Unity.Mathematics.float3 float18 = global::Unity.Mathematics.math.float3(x5.w, x6.w, threshold2.w);
			global::Unity.Mathematics.float4 float19 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float11, float11), global::Unity.Mathematics.math.dot(float13, float13), global::Unity.Mathematics.math.dot(float12, float12), global::Unity.Mathematics.math.dot(float14, float14)));
			float11 *= float19.x;
			float13 *= float19.y;
			float12 *= float19.z;
			float14 *= float19.w;
			global::Unity.Mathematics.float4 float20 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float15, float15), global::Unity.Mathematics.math.dot(float17, float17), global::Unity.Mathematics.math.dot(float16, float16), global::Unity.Mathematics.math.dot(float18, float18)));
			float15 *= float20.x;
			float17 *= float20.y;
			float16 *= float20.z;
			float18 *= float20.w;
			float x7 = global::Unity.Mathematics.math.dot(float11, float6);
			float y2 = global::Unity.Mathematics.math.dot(float12, global::Unity.Mathematics.math.float3(y.x, float6.yz));
			float z = global::Unity.Mathematics.math.dot(float13, global::Unity.Mathematics.math.float3(float6.x, y.y, float6.z));
			float w = global::Unity.Mathematics.math.dot(float14, global::Unity.Mathematics.math.float3(y.xy, float6.z));
			float x8 = global::Unity.Mathematics.math.dot(float15, global::Unity.Mathematics.math.float3(float6.xy, y.z));
			float y3 = global::Unity.Mathematics.math.dot(float16, global::Unity.Mathematics.math.float3(y.x, float6.y, y.z));
			float z2 = global::Unity.Mathematics.math.dot(float17, global::Unity.Mathematics.math.float3(float6.x, y.yz));
			float w2 = global::Unity.Mathematics.math.dot(float18, y);
			global::Unity.Mathematics.float3 float21 = fade(float6);
			global::Unity.Mathematics.float4 float22 = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float4(x7, y2, z, w), global::Unity.Mathematics.math.float4(x8, y3, z2, w2), float21.z);
			global::Unity.Mathematics.float2 float23 = global::Unity.Mathematics.math.lerp(float22.xy, float22.zw, float21.y);
			float num = global::Unity.Mathematics.math.lerp(float23.x, float23.y, float21.x);
			return 2.2f * num;
		}

		public static float pnoise(global::Unity.Mathematics.float3 P, global::Unity.Mathematics.float3 rep)
		{
			global::Unity.Mathematics.float3 float5 = global::Unity.Mathematics.math.fmod(global::Unity.Mathematics.math.floor(P), rep);
			global::Unity.Mathematics.float3 x = global::Unity.Mathematics.math.fmod(float5 + global::Unity.Mathematics.math.float3(1f), rep);
			float5 = mod289(float5);
			x = mod289(x);
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float3 y = float6 - global::Unity.Mathematics.math.float3(1f);
			global::Unity.Mathematics.float4 x2 = global::Unity.Mathematics.math.float4(float5.x, x.x, float5.x, x.x);
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.float4(float5.yy, x.yy);
			global::Unity.Mathematics.float4 zzzz = float5.zzzz;
			global::Unity.Mathematics.float4 zzzz2 = x.zzzz;
			global::Unity.Mathematics.float4 obj = permute(permute(x2) + float7);
			global::Unity.Mathematics.float4 float8 = permute(obj + zzzz);
			global::Unity.Mathematics.float4 obj2 = permute(obj + zzzz2);
			global::Unity.Mathematics.float4 x3 = float8 * (1f / 7f);
			global::Unity.Mathematics.float4 x4 = global::Unity.Mathematics.math.frac(global::Unity.Mathematics.math.floor(x3) * (1f / 7f)) - 0.5f;
			x3 = global::Unity.Mathematics.math.frac(x3);
			global::Unity.Mathematics.float4 threshold = global::Unity.Mathematics.math.float4(0.5f) - global::Unity.Mathematics.math.abs(x3) - global::Unity.Mathematics.math.abs(x4);
			global::Unity.Mathematics.float4 float9 = global::Unity.Mathematics.math.step(threshold, global::Unity.Mathematics.math.float4(0f));
			x3 -= float9 * (global::Unity.Mathematics.math.step(0f, x3) - 0.5f);
			x4 -= float9 * (global::Unity.Mathematics.math.step(0f, x4) - 0.5f);
			global::Unity.Mathematics.float4 x5 = obj2 * (1f / 7f);
			global::Unity.Mathematics.float4 x6 = global::Unity.Mathematics.math.frac(global::Unity.Mathematics.math.floor(x5) * (1f / 7f)) - 0.5f;
			x5 = global::Unity.Mathematics.math.frac(x5);
			global::Unity.Mathematics.float4 threshold2 = global::Unity.Mathematics.math.float4(0.5f) - global::Unity.Mathematics.math.abs(x5) - global::Unity.Mathematics.math.abs(x6);
			global::Unity.Mathematics.float4 float10 = global::Unity.Mathematics.math.step(threshold2, global::Unity.Mathematics.math.float4(0f));
			x5 -= float10 * (global::Unity.Mathematics.math.step(0f, x5) - 0.5f);
			x6 -= float10 * (global::Unity.Mathematics.math.step(0f, x6) - 0.5f);
			global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.float3(x3.x, x4.x, threshold.x);
			global::Unity.Mathematics.float3 float12 = global::Unity.Mathematics.math.float3(x3.y, x4.y, threshold.y);
			global::Unity.Mathematics.float3 float13 = global::Unity.Mathematics.math.float3(x3.z, x4.z, threshold.z);
			global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.float3(x3.w, x4.w, threshold.w);
			global::Unity.Mathematics.float3 float15 = global::Unity.Mathematics.math.float3(x5.x, x6.x, threshold2.x);
			global::Unity.Mathematics.float3 float16 = global::Unity.Mathematics.math.float3(x5.y, x6.y, threshold2.y);
			global::Unity.Mathematics.float3 float17 = global::Unity.Mathematics.math.float3(x5.z, x6.z, threshold2.z);
			global::Unity.Mathematics.float3 float18 = global::Unity.Mathematics.math.float3(x5.w, x6.w, threshold2.w);
			global::Unity.Mathematics.float4 float19 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float11, float11), global::Unity.Mathematics.math.dot(float13, float13), global::Unity.Mathematics.math.dot(float12, float12), global::Unity.Mathematics.math.dot(float14, float14)));
			float11 *= float19.x;
			float13 *= float19.y;
			float12 *= float19.z;
			float14 *= float19.w;
			global::Unity.Mathematics.float4 float20 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float15, float15), global::Unity.Mathematics.math.dot(float17, float17), global::Unity.Mathematics.math.dot(float16, float16), global::Unity.Mathematics.math.dot(float18, float18)));
			float15 *= float20.x;
			float17 *= float20.y;
			float16 *= float20.z;
			float18 *= float20.w;
			float x7 = global::Unity.Mathematics.math.dot(float11, float6);
			float y2 = global::Unity.Mathematics.math.dot(float12, global::Unity.Mathematics.math.float3(y.x, float6.yz));
			float z = global::Unity.Mathematics.math.dot(float13, global::Unity.Mathematics.math.float3(float6.x, y.y, float6.z));
			float w = global::Unity.Mathematics.math.dot(float14, global::Unity.Mathematics.math.float3(y.xy, float6.z));
			float x8 = global::Unity.Mathematics.math.dot(float15, global::Unity.Mathematics.math.float3(float6.xy, y.z));
			float y3 = global::Unity.Mathematics.math.dot(float16, global::Unity.Mathematics.math.float3(y.x, float6.y, y.z));
			float z2 = global::Unity.Mathematics.math.dot(float17, global::Unity.Mathematics.math.float3(float6.x, y.yz));
			float w2 = global::Unity.Mathematics.math.dot(float18, y);
			global::Unity.Mathematics.float3 float21 = fade(float6);
			global::Unity.Mathematics.float4 float22 = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float4(x7, y2, z, w), global::Unity.Mathematics.math.float4(x8, y3, z2, w2), float21.z);
			global::Unity.Mathematics.float2 float23 = global::Unity.Mathematics.math.lerp(float22.xy, float22.zw, float21.y);
			float num = global::Unity.Mathematics.math.lerp(float23.x, float23.y, float21.x);
			return 2.2f * num;
		}

		public static float cnoise(global::Unity.Mathematics.float4 P)
		{
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.floor(P);
			global::Unity.Mathematics.float4 x = float5 + 1f;
			float5 = mod289(float5);
			x = mod289(x);
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float4 y = float6 - 1f;
			global::Unity.Mathematics.float4 x2 = global::Unity.Mathematics.math.float4(float5.x, x.x, float5.x, x.x);
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.float4(float5.yy, x.yy);
			global::Unity.Mathematics.float4 float8 = global::Unity.Mathematics.math.float4(float5.zzzz);
			global::Unity.Mathematics.float4 float9 = global::Unity.Mathematics.math.float4(x.zzzz);
			global::Unity.Mathematics.float4 float10 = global::Unity.Mathematics.math.float4(float5.wwww);
			global::Unity.Mathematics.float4 float11 = global::Unity.Mathematics.math.float4(x.wwww);
			global::Unity.Mathematics.float4 obj = permute(permute(x2) + float7);
			global::Unity.Mathematics.float4 float12 = permute(obj + float8);
			global::Unity.Mathematics.float4 obj2 = permute(obj + float9);
			global::Unity.Mathematics.float4 float13 = permute(float12 + float10);
			global::Unity.Mathematics.float4 float14 = permute(float12 + float11);
			global::Unity.Mathematics.float4 float15 = permute(obj2 + float10);
			global::Unity.Mathematics.float4 obj3 = permute(obj2 + float11);
			global::Unity.Mathematics.float4 x3 = float13 * (1f / 7f);
			global::Unity.Mathematics.float4 x4 = global::Unity.Mathematics.math.floor(x3) * (1f / 7f);
			global::Unity.Mathematics.float4 x5 = global::Unity.Mathematics.math.floor(x4) * (1f / 6f);
			x3 = global::Unity.Mathematics.math.frac(x3) - 0.5f;
			x4 = global::Unity.Mathematics.math.frac(x4) - 0.5f;
			x5 = global::Unity.Mathematics.math.frac(x5) - 0.5f;
			global::Unity.Mathematics.float4 threshold = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x3) - global::Unity.Mathematics.math.abs(x4) - global::Unity.Mathematics.math.abs(x5);
			global::Unity.Mathematics.float4 float16 = global::Unity.Mathematics.math.step(threshold, global::Unity.Mathematics.math.float4(0f));
			x3 -= float16 * (global::Unity.Mathematics.math.step(0f, x3) - 0.5f);
			x4 -= float16 * (global::Unity.Mathematics.math.step(0f, x4) - 0.5f);
			global::Unity.Mathematics.float4 x6 = float14 * (1f / 7f);
			global::Unity.Mathematics.float4 x7 = global::Unity.Mathematics.math.floor(x6) * (1f / 7f);
			global::Unity.Mathematics.float4 x8 = global::Unity.Mathematics.math.floor(x7) * (1f / 6f);
			x6 = global::Unity.Mathematics.math.frac(x6) - 0.5f;
			x7 = global::Unity.Mathematics.math.frac(x7) - 0.5f;
			x8 = global::Unity.Mathematics.math.frac(x8) - 0.5f;
			global::Unity.Mathematics.float4 threshold2 = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x6) - global::Unity.Mathematics.math.abs(x7) - global::Unity.Mathematics.math.abs(x8);
			global::Unity.Mathematics.float4 float17 = global::Unity.Mathematics.math.step(threshold2, global::Unity.Mathematics.math.float4(0f));
			x6 -= float17 * (global::Unity.Mathematics.math.step(0f, x6) - 0.5f);
			x7 -= float17 * (global::Unity.Mathematics.math.step(0f, x7) - 0.5f);
			global::Unity.Mathematics.float4 x9 = float15 * (1f / 7f);
			global::Unity.Mathematics.float4 x10 = global::Unity.Mathematics.math.floor(x9) * (1f / 7f);
			global::Unity.Mathematics.float4 x11 = global::Unity.Mathematics.math.floor(x10) * (1f / 6f);
			x9 = global::Unity.Mathematics.math.frac(x9) - 0.5f;
			x10 = global::Unity.Mathematics.math.frac(x10) - 0.5f;
			x11 = global::Unity.Mathematics.math.frac(x11) - 0.5f;
			global::Unity.Mathematics.float4 threshold3 = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x9) - global::Unity.Mathematics.math.abs(x10) - global::Unity.Mathematics.math.abs(x11);
			global::Unity.Mathematics.float4 float18 = global::Unity.Mathematics.math.step(threshold3, global::Unity.Mathematics.math.float4(0f));
			x9 -= float18 * (global::Unity.Mathematics.math.step(0f, x9) - 0.5f);
			x10 -= float18 * (global::Unity.Mathematics.math.step(0f, x10) - 0.5f);
			global::Unity.Mathematics.float4 x12 = obj3 * (1f / 7f);
			global::Unity.Mathematics.float4 x13 = global::Unity.Mathematics.math.floor(x12) * (1f / 7f);
			global::Unity.Mathematics.float4 x14 = global::Unity.Mathematics.math.floor(x13) * (1f / 6f);
			x12 = global::Unity.Mathematics.math.frac(x12) - 0.5f;
			x13 = global::Unity.Mathematics.math.frac(x13) - 0.5f;
			x14 = global::Unity.Mathematics.math.frac(x14) - 0.5f;
			global::Unity.Mathematics.float4 threshold4 = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x12) - global::Unity.Mathematics.math.abs(x13) - global::Unity.Mathematics.math.abs(x14);
			global::Unity.Mathematics.float4 float19 = global::Unity.Mathematics.math.step(threshold4, global::Unity.Mathematics.math.float4(0f));
			x12 -= float19 * (global::Unity.Mathematics.math.step(0f, x12) - 0.5f);
			x13 -= float19 * (global::Unity.Mathematics.math.step(0f, x13) - 0.5f);
			global::Unity.Mathematics.float4 float20 = global::Unity.Mathematics.math.float4(x3.x, x4.x, x5.x, threshold.x);
			global::Unity.Mathematics.float4 float21 = global::Unity.Mathematics.math.float4(x3.y, x4.y, x5.y, threshold.y);
			global::Unity.Mathematics.float4 float22 = global::Unity.Mathematics.math.float4(x3.z, x4.z, x5.z, threshold.z);
			global::Unity.Mathematics.float4 float23 = global::Unity.Mathematics.math.float4(x3.w, x4.w, x5.w, threshold.w);
			global::Unity.Mathematics.float4 float24 = global::Unity.Mathematics.math.float4(x9.x, x10.x, x11.x, threshold3.x);
			global::Unity.Mathematics.float4 float25 = global::Unity.Mathematics.math.float4(x9.y, x10.y, x11.y, threshold3.y);
			global::Unity.Mathematics.float4 float26 = global::Unity.Mathematics.math.float4(x9.z, x10.z, x11.z, threshold3.z);
			global::Unity.Mathematics.float4 float27 = global::Unity.Mathematics.math.float4(x9.w, x10.w, x11.w, threshold3.w);
			global::Unity.Mathematics.float4 obj4 = global::Unity.Mathematics.math.float4(x6.x, x7.x, x8.x, threshold2.x);
			global::Unity.Mathematics.float4 float28 = global::Unity.Mathematics.math.float4(x6.y, x7.y, x8.y, threshold2.y);
			global::Unity.Mathematics.float4 float29 = global::Unity.Mathematics.math.float4(x6.z, x7.z, x8.z, threshold2.z);
			global::Unity.Mathematics.float4 float30 = global::Unity.Mathematics.math.float4(x6.w, x7.w, x8.w, threshold2.w);
			global::Unity.Mathematics.float4 float31 = global::Unity.Mathematics.math.float4(x12.x, x13.x, x14.x, threshold4.x);
			global::Unity.Mathematics.float4 float32 = global::Unity.Mathematics.math.float4(x12.y, x13.y, x14.y, threshold4.y);
			global::Unity.Mathematics.float4 float33 = global::Unity.Mathematics.math.float4(x12.z, x13.z, x14.z, threshold4.z);
			global::Unity.Mathematics.float4 float34 = global::Unity.Mathematics.math.float4(x12.w, x13.w, x14.w, threshold4.w);
			global::Unity.Mathematics.float4 float35 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float20, float20), global::Unity.Mathematics.math.dot(float22, float22), global::Unity.Mathematics.math.dot(float21, float21), global::Unity.Mathematics.math.dot(float23, float23)));
			float20 *= float35.x;
			float22 *= float35.y;
			float21 *= float35.z;
			float23 *= float35.w;
			global::Unity.Mathematics.float4 float36 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(obj4, obj4), global::Unity.Mathematics.math.dot(float29, float29), global::Unity.Mathematics.math.dot(float28, float28), global::Unity.Mathematics.math.dot(float30, float30)));
			global::Unity.Mathematics.float4 x15 = obj4 * float36.x;
			float29 *= float36.y;
			float28 *= float36.z;
			float30 *= float36.w;
			global::Unity.Mathematics.float4 float37 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float24, float24), global::Unity.Mathematics.math.dot(float26, float26), global::Unity.Mathematics.math.dot(float25, float25), global::Unity.Mathematics.math.dot(float27, float27)));
			float24 *= float37.x;
			float26 *= float37.y;
			float25 *= float37.z;
			float27 *= float37.w;
			global::Unity.Mathematics.float4 float38 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float31, float31), global::Unity.Mathematics.math.dot(float33, float33), global::Unity.Mathematics.math.dot(float32, float32), global::Unity.Mathematics.math.dot(float34, float34)));
			float31 *= float38.x;
			float33 *= float38.y;
			float32 *= float38.z;
			float34 *= float38.w;
			float x16 = global::Unity.Mathematics.math.dot(float20, float6);
			float y2 = global::Unity.Mathematics.math.dot(float21, global::Unity.Mathematics.math.float4(y.x, float6.yzw));
			float z = global::Unity.Mathematics.math.dot(float22, global::Unity.Mathematics.math.float4(float6.x, y.y, float6.zw));
			float w = global::Unity.Mathematics.math.dot(float23, global::Unity.Mathematics.math.float4(y.xy, float6.zw));
			float x17 = global::Unity.Mathematics.math.dot(float24, global::Unity.Mathematics.math.float4(float6.xy, y.z, float6.w));
			float y3 = global::Unity.Mathematics.math.dot(float25, global::Unity.Mathematics.math.float4(y.x, float6.y, y.z, float6.w));
			float z2 = global::Unity.Mathematics.math.dot(float26, global::Unity.Mathematics.math.float4(float6.x, y.yz, float6.w));
			float w2 = global::Unity.Mathematics.math.dot(float27, global::Unity.Mathematics.math.float4(y.xyz, float6.w));
			float x18 = global::Unity.Mathematics.math.dot(x15, global::Unity.Mathematics.math.float4(float6.xyz, y.w));
			float y4 = global::Unity.Mathematics.math.dot(float28, global::Unity.Mathematics.math.float4(y.x, float6.yz, y.w));
			float z3 = global::Unity.Mathematics.math.dot(float29, global::Unity.Mathematics.math.float4(float6.x, y.y, float6.z, y.w));
			float w3 = global::Unity.Mathematics.math.dot(float30, global::Unity.Mathematics.math.float4(y.xy, float6.z, y.w));
			float x19 = global::Unity.Mathematics.math.dot(float31, global::Unity.Mathematics.math.float4(float6.xy, y.zw));
			float y5 = global::Unity.Mathematics.math.dot(float32, global::Unity.Mathematics.math.float4(y.x, float6.y, y.zw));
			float z4 = global::Unity.Mathematics.math.dot(float33, global::Unity.Mathematics.math.float4(float6.x, y.yzw));
			float w4 = global::Unity.Mathematics.math.dot(float34, y);
			global::Unity.Mathematics.float4 float39 = fade(float6);
			global::Unity.Mathematics.float4 start = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float4(x16, y2, z, w), global::Unity.Mathematics.math.float4(x18, y4, z3, w3), float39.w);
			global::Unity.Mathematics.float4 end = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float4(x17, y3, z2, w2), global::Unity.Mathematics.math.float4(x19, y5, z4, w4), float39.w);
			global::Unity.Mathematics.float4 float40 = global::Unity.Mathematics.math.lerp(start, end, float39.z);
			global::Unity.Mathematics.float2 float41 = global::Unity.Mathematics.math.lerp(float40.xy, float40.zw, float39.y);
			float num = global::Unity.Mathematics.math.lerp(float41.x, float41.y, float39.x);
			return 2.2f * num;
		}

		public static float pnoise(global::Unity.Mathematics.float4 P, global::Unity.Mathematics.float4 rep)
		{
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.fmod(global::Unity.Mathematics.math.floor(P), rep);
			global::Unity.Mathematics.float4 x = global::Unity.Mathematics.math.fmod(float5 + 1f, rep);
			float5 = mod289(float5);
			x = mod289(x);
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.frac(P);
			global::Unity.Mathematics.float4 y = float6 - 1f;
			global::Unity.Mathematics.float4 x2 = global::Unity.Mathematics.math.float4(float5.x, x.x, float5.x, x.x);
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.float4(float5.yy, x.yy);
			global::Unity.Mathematics.float4 float8 = global::Unity.Mathematics.math.float4(float5.zzzz);
			global::Unity.Mathematics.float4 float9 = global::Unity.Mathematics.math.float4(x.zzzz);
			global::Unity.Mathematics.float4 float10 = global::Unity.Mathematics.math.float4(float5.wwww);
			global::Unity.Mathematics.float4 float11 = global::Unity.Mathematics.math.float4(x.wwww);
			global::Unity.Mathematics.float4 obj = permute(permute(x2) + float7);
			global::Unity.Mathematics.float4 float12 = permute(obj + float8);
			global::Unity.Mathematics.float4 obj2 = permute(obj + float9);
			global::Unity.Mathematics.float4 float13 = permute(float12 + float10);
			global::Unity.Mathematics.float4 float14 = permute(float12 + float11);
			global::Unity.Mathematics.float4 float15 = permute(obj2 + float10);
			global::Unity.Mathematics.float4 obj3 = permute(obj2 + float11);
			global::Unity.Mathematics.float4 x3 = float13 * (1f / 7f);
			global::Unity.Mathematics.float4 x4 = global::Unity.Mathematics.math.floor(x3) * (1f / 7f);
			global::Unity.Mathematics.float4 x5 = global::Unity.Mathematics.math.floor(x4) * (1f / 6f);
			x3 = global::Unity.Mathematics.math.frac(x3) - 0.5f;
			x4 = global::Unity.Mathematics.math.frac(x4) - 0.5f;
			x5 = global::Unity.Mathematics.math.frac(x5) - 0.5f;
			global::Unity.Mathematics.float4 threshold = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x3) - global::Unity.Mathematics.math.abs(x4) - global::Unity.Mathematics.math.abs(x5);
			global::Unity.Mathematics.float4 float16 = global::Unity.Mathematics.math.step(threshold, global::Unity.Mathematics.math.float4(0f));
			x3 -= float16 * (global::Unity.Mathematics.math.step(0f, x3) - 0.5f);
			x4 -= float16 * (global::Unity.Mathematics.math.step(0f, x4) - 0.5f);
			global::Unity.Mathematics.float4 x6 = float14 * (1f / 7f);
			global::Unity.Mathematics.float4 x7 = global::Unity.Mathematics.math.floor(x6) * (1f / 7f);
			global::Unity.Mathematics.float4 x8 = global::Unity.Mathematics.math.floor(x7) * (1f / 6f);
			x6 = global::Unity.Mathematics.math.frac(x6) - 0.5f;
			x7 = global::Unity.Mathematics.math.frac(x7) - 0.5f;
			x8 = global::Unity.Mathematics.math.frac(x8) - 0.5f;
			global::Unity.Mathematics.float4 threshold2 = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x6) - global::Unity.Mathematics.math.abs(x7) - global::Unity.Mathematics.math.abs(x8);
			global::Unity.Mathematics.float4 float17 = global::Unity.Mathematics.math.step(threshold2, global::Unity.Mathematics.math.float4(0f));
			x6 -= float17 * (global::Unity.Mathematics.math.step(0f, x6) - 0.5f);
			x7 -= float17 * (global::Unity.Mathematics.math.step(0f, x7) - 0.5f);
			global::Unity.Mathematics.float4 x9 = float15 * (1f / 7f);
			global::Unity.Mathematics.float4 x10 = global::Unity.Mathematics.math.floor(x9) * (1f / 7f);
			global::Unity.Mathematics.float4 x11 = global::Unity.Mathematics.math.floor(x10) * (1f / 6f);
			x9 = global::Unity.Mathematics.math.frac(x9) - 0.5f;
			x10 = global::Unity.Mathematics.math.frac(x10) - 0.5f;
			x11 = global::Unity.Mathematics.math.frac(x11) - 0.5f;
			global::Unity.Mathematics.float4 threshold3 = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x9) - global::Unity.Mathematics.math.abs(x10) - global::Unity.Mathematics.math.abs(x11);
			global::Unity.Mathematics.float4 float18 = global::Unity.Mathematics.math.step(threshold3, global::Unity.Mathematics.math.float4(0f));
			x9 -= float18 * (global::Unity.Mathematics.math.step(0f, x9) - 0.5f);
			x10 -= float18 * (global::Unity.Mathematics.math.step(0f, x10) - 0.5f);
			global::Unity.Mathematics.float4 x12 = obj3 * (1f / 7f);
			global::Unity.Mathematics.float4 x13 = global::Unity.Mathematics.math.floor(x12) * (1f / 7f);
			global::Unity.Mathematics.float4 x14 = global::Unity.Mathematics.math.floor(x13) * (1f / 6f);
			x12 = global::Unity.Mathematics.math.frac(x12) - 0.5f;
			x13 = global::Unity.Mathematics.math.frac(x13) - 0.5f;
			x14 = global::Unity.Mathematics.math.frac(x14) - 0.5f;
			global::Unity.Mathematics.float4 threshold4 = global::Unity.Mathematics.math.float4(0.75f) - global::Unity.Mathematics.math.abs(x12) - global::Unity.Mathematics.math.abs(x13) - global::Unity.Mathematics.math.abs(x14);
			global::Unity.Mathematics.float4 float19 = global::Unity.Mathematics.math.step(threshold4, global::Unity.Mathematics.math.float4(0f));
			x12 -= float19 * (global::Unity.Mathematics.math.step(0f, x12) - 0.5f);
			x13 -= float19 * (global::Unity.Mathematics.math.step(0f, x13) - 0.5f);
			global::Unity.Mathematics.float4 float20 = global::Unity.Mathematics.math.float4(x3.x, x4.x, x5.x, threshold.x);
			global::Unity.Mathematics.float4 float21 = global::Unity.Mathematics.math.float4(x3.y, x4.y, x5.y, threshold.y);
			global::Unity.Mathematics.float4 float22 = global::Unity.Mathematics.math.float4(x3.z, x4.z, x5.z, threshold.z);
			global::Unity.Mathematics.float4 float23 = global::Unity.Mathematics.math.float4(x3.w, x4.w, x5.w, threshold.w);
			global::Unity.Mathematics.float4 float24 = global::Unity.Mathematics.math.float4(x9.x, x10.x, x11.x, threshold3.x);
			global::Unity.Mathematics.float4 float25 = global::Unity.Mathematics.math.float4(x9.y, x10.y, x11.y, threshold3.y);
			global::Unity.Mathematics.float4 float26 = global::Unity.Mathematics.math.float4(x9.z, x10.z, x11.z, threshold3.z);
			global::Unity.Mathematics.float4 float27 = global::Unity.Mathematics.math.float4(x9.w, x10.w, x11.w, threshold3.w);
			global::Unity.Mathematics.float4 obj4 = global::Unity.Mathematics.math.float4(x6.x, x7.x, x8.x, threshold2.x);
			global::Unity.Mathematics.float4 float28 = global::Unity.Mathematics.math.float4(x6.y, x7.y, x8.y, threshold2.y);
			global::Unity.Mathematics.float4 float29 = global::Unity.Mathematics.math.float4(x6.z, x7.z, x8.z, threshold2.z);
			global::Unity.Mathematics.float4 float30 = global::Unity.Mathematics.math.float4(x6.w, x7.w, x8.w, threshold2.w);
			global::Unity.Mathematics.float4 float31 = global::Unity.Mathematics.math.float4(x12.x, x13.x, x14.x, threshold4.x);
			global::Unity.Mathematics.float4 float32 = global::Unity.Mathematics.math.float4(x12.y, x13.y, x14.y, threshold4.y);
			global::Unity.Mathematics.float4 float33 = global::Unity.Mathematics.math.float4(x12.z, x13.z, x14.z, threshold4.z);
			global::Unity.Mathematics.float4 float34 = global::Unity.Mathematics.math.float4(x12.w, x13.w, x14.w, threshold4.w);
			global::Unity.Mathematics.float4 float35 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float20, float20), global::Unity.Mathematics.math.dot(float22, float22), global::Unity.Mathematics.math.dot(float21, float21), global::Unity.Mathematics.math.dot(float23, float23)));
			float20 *= float35.x;
			float22 *= float35.y;
			float21 *= float35.z;
			float23 *= float35.w;
			global::Unity.Mathematics.float4 float36 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(obj4, obj4), global::Unity.Mathematics.math.dot(float29, float29), global::Unity.Mathematics.math.dot(float28, float28), global::Unity.Mathematics.math.dot(float30, float30)));
			global::Unity.Mathematics.float4 x15 = obj4 * float36.x;
			float29 *= float36.y;
			float28 *= float36.z;
			float30 *= float36.w;
			global::Unity.Mathematics.float4 float37 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float24, float24), global::Unity.Mathematics.math.dot(float26, float26), global::Unity.Mathematics.math.dot(float25, float25), global::Unity.Mathematics.math.dot(float27, float27)));
			float24 *= float37.x;
			float26 *= float37.y;
			float25 *= float37.z;
			float27 *= float37.w;
			global::Unity.Mathematics.float4 float38 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float31, float31), global::Unity.Mathematics.math.dot(float33, float33), global::Unity.Mathematics.math.dot(float32, float32), global::Unity.Mathematics.math.dot(float34, float34)));
			float31 *= float38.x;
			float33 *= float38.y;
			float32 *= float38.z;
			float34 *= float38.w;
			float x16 = global::Unity.Mathematics.math.dot(float20, float6);
			float y2 = global::Unity.Mathematics.math.dot(float21, global::Unity.Mathematics.math.float4(y.x, float6.yzw));
			float z = global::Unity.Mathematics.math.dot(float22, global::Unity.Mathematics.math.float4(float6.x, y.y, float6.zw));
			float w = global::Unity.Mathematics.math.dot(float23, global::Unity.Mathematics.math.float4(y.xy, float6.zw));
			float x17 = global::Unity.Mathematics.math.dot(float24, global::Unity.Mathematics.math.float4(float6.xy, y.z, float6.w));
			float y3 = global::Unity.Mathematics.math.dot(float25, global::Unity.Mathematics.math.float4(y.x, float6.y, y.z, float6.w));
			float z2 = global::Unity.Mathematics.math.dot(float26, global::Unity.Mathematics.math.float4(float6.x, y.yz, float6.w));
			float w2 = global::Unity.Mathematics.math.dot(float27, global::Unity.Mathematics.math.float4(y.xyz, float6.w));
			float x18 = global::Unity.Mathematics.math.dot(x15, global::Unity.Mathematics.math.float4(float6.xyz, y.w));
			float y4 = global::Unity.Mathematics.math.dot(float28, global::Unity.Mathematics.math.float4(y.x, float6.yz, y.w));
			float z3 = global::Unity.Mathematics.math.dot(float29, global::Unity.Mathematics.math.float4(float6.x, y.y, float6.z, y.w));
			float w3 = global::Unity.Mathematics.math.dot(float30, global::Unity.Mathematics.math.float4(y.xy, float6.z, y.w));
			float x19 = global::Unity.Mathematics.math.dot(float31, global::Unity.Mathematics.math.float4(float6.xy, y.zw));
			float y5 = global::Unity.Mathematics.math.dot(float32, global::Unity.Mathematics.math.float4(y.x, float6.y, y.zw));
			float z4 = global::Unity.Mathematics.math.dot(float33, global::Unity.Mathematics.math.float4(float6.x, y.yzw));
			float w4 = global::Unity.Mathematics.math.dot(float34, y);
			global::Unity.Mathematics.float4 float39 = fade(float6);
			global::Unity.Mathematics.float4 start = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float4(x16, y2, z, w), global::Unity.Mathematics.math.float4(x18, y4, z3, w3), float39.w);
			global::Unity.Mathematics.float4 end = global::Unity.Mathematics.math.lerp(global::Unity.Mathematics.math.float4(x17, y3, z2, w2), global::Unity.Mathematics.math.float4(x19, y5, z4, w4), float39.w);
			global::Unity.Mathematics.float4 float40 = global::Unity.Mathematics.math.lerp(start, end, float39.z);
			global::Unity.Mathematics.float2 float41 = global::Unity.Mathematics.math.lerp(float40.xy, float40.zw, float39.y);
			float num = global::Unity.Mathematics.math.lerp(float41.x, float41.y, float39.x);
			return 2.2f * num;
		}

		private static float mod289(float x)
		{
			return x - global::Unity.Mathematics.math.floor(x * 0.0034602077f) * 289f;
		}

		private static global::Unity.Mathematics.float2 mod289(global::Unity.Mathematics.float2 x)
		{
			return x - global::Unity.Mathematics.math.floor(x * 0.0034602077f) * 289f;
		}

		private static global::Unity.Mathematics.float3 mod289(global::Unity.Mathematics.float3 x)
		{
			return x - global::Unity.Mathematics.math.floor(x * 0.0034602077f) * 289f;
		}

		private static global::Unity.Mathematics.float4 mod289(global::Unity.Mathematics.float4 x)
		{
			return x - global::Unity.Mathematics.math.floor(x * 0.0034602077f) * 289f;
		}

		private static global::Unity.Mathematics.float3 mod7(global::Unity.Mathematics.float3 x)
		{
			return x - global::Unity.Mathematics.math.floor(x * (1f / 7f)) * 7f;
		}

		private static global::Unity.Mathematics.float4 mod7(global::Unity.Mathematics.float4 x)
		{
			return x - global::Unity.Mathematics.math.floor(x * (1f / 7f)) * 7f;
		}

		private static float permute(float x)
		{
			return mod289((34f * x + 1f) * x);
		}

		private static global::Unity.Mathematics.float3 permute(global::Unity.Mathematics.float3 x)
		{
			return mod289((34f * x + 1f) * x);
		}

		private static global::Unity.Mathematics.float4 permute(global::Unity.Mathematics.float4 x)
		{
			return mod289((34f * x + 1f) * x);
		}

		private static float taylorInvSqrt(float r)
		{
			return 1.7928429f - 0.85373473f * r;
		}

		private static global::Unity.Mathematics.float4 taylorInvSqrt(global::Unity.Mathematics.float4 r)
		{
			return 1.7928429f - 0.85373473f * r;
		}

		private static global::Unity.Mathematics.float2 fade(global::Unity.Mathematics.float2 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		private static global::Unity.Mathematics.float3 fade(global::Unity.Mathematics.float3 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		private static global::Unity.Mathematics.float4 fade(global::Unity.Mathematics.float4 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		private static global::Unity.Mathematics.float4 grad4(float j, global::Unity.Mathematics.float4 ip)
		{
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.float4(1f, 1f, 1f, -1f);
			global::Unity.Mathematics.float3 float6 = global::Unity.Mathematics.math.floor(global::Unity.Mathematics.math.frac(global::Unity.Mathematics.math.float3(j) * ip.xyz) * 7f) * ip.z - 1f;
			float w = 1.5f - global::Unity.Mathematics.math.dot(global::Unity.Mathematics.math.abs(float6), float5.xyz);
			global::Unity.Mathematics.float4 float7 = global::Unity.Mathematics.math.float4(float6, w);
			global::Unity.Mathematics.float4 float8 = global::Unity.Mathematics.math.float4(float7 < 0f);
			float7.xyz += (float8.xyz * 2f - 1f) * float8.www;
			return float7;
		}

		private static global::Unity.Mathematics.float2 rgrad2(global::Unity.Mathematics.float2 p, float rot)
		{
			float x = permute(permute(p.x) + p.y) * (1f / 41f) + rot;
			x = global::Unity.Mathematics.math.frac(x) * (global::System.MathF.PI * 2f);
			return global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.cos(x), global::Unity.Mathematics.math.sin(x));
		}

		public static float snoise(global::Unity.Mathematics.float2 v)
		{
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.float4(0.21132487f, 0.36602542f, -0.57735026f, 1f / 41f);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.floor(v + global::Unity.Mathematics.math.dot(v, float5.yy));
			global::Unity.Mathematics.float2 float7 = v - float6 + global::Unity.Mathematics.math.dot(float6, float5.xx);
			global::Unity.Mathematics.float2 float8 = ((float7.x > float7.y) ? global::Unity.Mathematics.math.float2(1f, 0f) : global::Unity.Mathematics.math.float2(0f, 1f));
			global::Unity.Mathematics.float4 float9 = float7.xyxy + float5.xxzz;
			float9.xy -= float8;
			float6 = mod289(float6);
			global::Unity.Mathematics.float3 float10 = permute(permute(float6.y + global::Unity.Mathematics.math.float3(0f, float8.y, 1f)) + float6.x + global::Unity.Mathematics.math.float3(0f, float8.x, 1f));
			global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.max(0.5f - global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float7, float7), global::Unity.Mathematics.math.dot(float9.xy, float9.xy), global::Unity.Mathematics.math.dot(float9.zw, float9.zw)), 0f);
			float11 *= float11;
			float11 *= float11;
			global::Unity.Mathematics.float3 obj = 2f * global::Unity.Mathematics.math.frac(float10 * float5.www) - 1f;
			global::Unity.Mathematics.float3 float12 = global::Unity.Mathematics.math.abs(obj) - 0.5f;
			global::Unity.Mathematics.float3 float13 = global::Unity.Mathematics.math.floor(obj + 0.5f);
			global::Unity.Mathematics.float3 float14 = obj - float13;
			float11 *= 1.7928429f - 0.85373473f * (float14 * float14 + float12 * float12);
			float x = float14.x * float7.x + float12.x * float7.y;
			global::Unity.Mathematics.float2 yz = float14.yz * float9.xz + float12.yz * float9.yw;
			global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.float3(x, yz);
			return 130f * global::Unity.Mathematics.math.dot(float11, y);
		}

		public static float snoise(global::Unity.Mathematics.float3 v)
		{
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.float2(1f / 6f, 1f / 3f);
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.float4(0f, 0.5f, 1f, 2f);
			global::Unity.Mathematics.float3 float7 = global::Unity.Mathematics.math.floor(v + global::Unity.Mathematics.math.dot(v, float5.yyy));
			global::Unity.Mathematics.float3 float8 = v - float7 + global::Unity.Mathematics.math.dot(float7, float5.xxx);
			global::Unity.Mathematics.float3 float9 = global::Unity.Mathematics.math.step(float8.yzx, float8.xyz);
			global::Unity.Mathematics.float3 float10 = 1f - float9;
			global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.min(float9.xyz, float10.zxy);
			global::Unity.Mathematics.float3 float12 = global::Unity.Mathematics.math.max(float9.xyz, float10.zxy);
			global::Unity.Mathematics.float3 float13 = float8 - float11 + float5.xxx;
			global::Unity.Mathematics.float3 float14 = float8 - float12 + float5.yyy;
			global::Unity.Mathematics.float3 float15 = float8 - float6.yyy;
			float7 = mod289(float7);
			global::Unity.Mathematics.float4 float16 = permute(permute(permute(float7.z + global::Unity.Mathematics.math.float4(0f, float11.z, float12.z, 1f)) + float7.y + global::Unity.Mathematics.math.float4(0f, float11.y, float12.y, 1f)) + float7.x + global::Unity.Mathematics.math.float4(0f, float11.x, float12.x, 1f));
			global::Unity.Mathematics.float3 float17 = 1f / 7f * float6.wyz - float6.xzx;
			global::Unity.Mathematics.float4 obj = float16 - 49f * global::Unity.Mathematics.math.floor(float16 * float17.z * float17.z);
			global::Unity.Mathematics.float4 float18 = global::Unity.Mathematics.math.floor(obj * float17.z);
			global::Unity.Mathematics.float4 obj2 = global::Unity.Mathematics.math.floor(obj - 7f * float18);
			global::Unity.Mathematics.float4 x = float18 * float17.x + float17.yyyy;
			global::Unity.Mathematics.float4 x2 = obj2 * float17.x + float17.yyyy;
			global::Unity.Mathematics.float4 threshold = 1f - global::Unity.Mathematics.math.abs(x) - global::Unity.Mathematics.math.abs(x2);
			global::Unity.Mathematics.float4 x3 = global::Unity.Mathematics.math.float4(x.xy, x2.xy);
			global::Unity.Mathematics.float4 x4 = global::Unity.Mathematics.math.float4(x.zw, x2.zw);
			global::Unity.Mathematics.float4 float19 = global::Unity.Mathematics.math.floor(x3) * 2f + 1f;
			global::Unity.Mathematics.float4 float20 = global::Unity.Mathematics.math.floor(x4) * 2f + 1f;
			global::Unity.Mathematics.float4 float21 = -global::Unity.Mathematics.math.step(threshold, global::Unity.Mathematics.math.float4(0f));
			global::Unity.Mathematics.float4 float22 = x3.xzyw + float19.xzyw * float21.xxyy;
			global::Unity.Mathematics.float4 float23 = x4.xzyw + float20.xzyw * float21.zzww;
			global::Unity.Mathematics.float3 float24 = global::Unity.Mathematics.math.float3(float22.xy, threshold.x);
			global::Unity.Mathematics.float3 float25 = global::Unity.Mathematics.math.float3(float22.zw, threshold.y);
			global::Unity.Mathematics.float3 float26 = global::Unity.Mathematics.math.float3(float23.xy, threshold.z);
			global::Unity.Mathematics.float3 float27 = global::Unity.Mathematics.math.float3(float23.zw, threshold.w);
			global::Unity.Mathematics.float4 float28 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float24, float24), global::Unity.Mathematics.math.dot(float25, float25), global::Unity.Mathematics.math.dot(float26, float26), global::Unity.Mathematics.math.dot(float27, float27)));
			float24 *= float28.x;
			float25 *= float28.y;
			float26 *= float28.z;
			float27 *= float28.w;
			global::Unity.Mathematics.float4 float29 = global::Unity.Mathematics.math.max(0.6f - global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float8, float8), global::Unity.Mathematics.math.dot(float13, float13), global::Unity.Mathematics.math.dot(float14, float14), global::Unity.Mathematics.math.dot(float15, float15)), 0f);
			float29 *= float29;
			return 42f * global::Unity.Mathematics.math.dot(float29 * float29, global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float24, float8), global::Unity.Mathematics.math.dot(float25, float13), global::Unity.Mathematics.math.dot(float26, float14), global::Unity.Mathematics.math.dot(float27, float15)));
		}

		public static float snoise(global::Unity.Mathematics.float3 v, out global::Unity.Mathematics.float3 gradient)
		{
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.float2(1f / 6f, 1f / 3f);
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.float4(0f, 0.5f, 1f, 2f);
			global::Unity.Mathematics.float3 float7 = global::Unity.Mathematics.math.floor(v + global::Unity.Mathematics.math.dot(v, float5.yyy));
			global::Unity.Mathematics.float3 float8 = v - float7 + global::Unity.Mathematics.math.dot(float7, float5.xxx);
			global::Unity.Mathematics.float3 float9 = global::Unity.Mathematics.math.step(float8.yzx, float8.xyz);
			global::Unity.Mathematics.float3 float10 = 1f - float9;
			global::Unity.Mathematics.float3 float11 = global::Unity.Mathematics.math.min(float9.xyz, float10.zxy);
			global::Unity.Mathematics.float3 float12 = global::Unity.Mathematics.math.max(float9.xyz, float10.zxy);
			global::Unity.Mathematics.float3 float13 = float8 - float11 + float5.xxx;
			global::Unity.Mathematics.float3 float14 = float8 - float12 + float5.yyy;
			global::Unity.Mathematics.float3 float15 = float8 - float6.yyy;
			float7 = mod289(float7);
			global::Unity.Mathematics.float4 float16 = permute(permute(permute(float7.z + global::Unity.Mathematics.math.float4(0f, float11.z, float12.z, 1f)) + float7.y + global::Unity.Mathematics.math.float4(0f, float11.y, float12.y, 1f)) + float7.x + global::Unity.Mathematics.math.float4(0f, float11.x, float12.x, 1f));
			global::Unity.Mathematics.float3 float17 = 1f / 7f * float6.wyz - float6.xzx;
			global::Unity.Mathematics.float4 obj = float16 - 49f * global::Unity.Mathematics.math.floor(float16 * float17.z * float17.z);
			global::Unity.Mathematics.float4 float18 = global::Unity.Mathematics.math.floor(obj * float17.z);
			global::Unity.Mathematics.float4 obj2 = global::Unity.Mathematics.math.floor(obj - 7f * float18);
			global::Unity.Mathematics.float4 x = float18 * float17.x + float17.yyyy;
			global::Unity.Mathematics.float4 x2 = obj2 * float17.x + float17.yyyy;
			global::Unity.Mathematics.float4 threshold = 1f - global::Unity.Mathematics.math.abs(x) - global::Unity.Mathematics.math.abs(x2);
			global::Unity.Mathematics.float4 x3 = global::Unity.Mathematics.math.float4(x.xy, x2.xy);
			global::Unity.Mathematics.float4 x4 = global::Unity.Mathematics.math.float4(x.zw, x2.zw);
			global::Unity.Mathematics.float4 float19 = global::Unity.Mathematics.math.floor(x3) * 2f + 1f;
			global::Unity.Mathematics.float4 float20 = global::Unity.Mathematics.math.floor(x4) * 2f + 1f;
			global::Unity.Mathematics.float4 float21 = -global::Unity.Mathematics.math.step(threshold, global::Unity.Mathematics.math.float4(0f));
			global::Unity.Mathematics.float4 float22 = x3.xzyw + float19.xzyw * float21.xxyy;
			global::Unity.Mathematics.float4 float23 = x4.xzyw + float20.xzyw * float21.zzww;
			global::Unity.Mathematics.float3 float24 = global::Unity.Mathematics.math.float3(float22.xy, threshold.x);
			global::Unity.Mathematics.float3 float25 = global::Unity.Mathematics.math.float3(float22.zw, threshold.y);
			global::Unity.Mathematics.float3 float26 = global::Unity.Mathematics.math.float3(float23.xy, threshold.z);
			global::Unity.Mathematics.float3 float27 = global::Unity.Mathematics.math.float3(float23.zw, threshold.w);
			global::Unity.Mathematics.float4 float28 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float24, float24), global::Unity.Mathematics.math.dot(float25, float25), global::Unity.Mathematics.math.dot(float26, float26), global::Unity.Mathematics.math.dot(float27, float27)));
			float24 *= float28.x;
			float25 *= float28.y;
			float26 *= float28.z;
			float27 *= float28.w;
			global::Unity.Mathematics.float4 float29 = global::Unity.Mathematics.math.max(0.6f - global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float8, float8), global::Unity.Mathematics.math.dot(float13, float13), global::Unity.Mathematics.math.dot(float14, float14), global::Unity.Mathematics.math.dot(float15, float15)), 0f);
			global::Unity.Mathematics.float4 obj3 = float29 * float29;
			global::Unity.Mathematics.float4 x5 = obj3 * obj3;
			global::Unity.Mathematics.float4 float30 = global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float24, float8), global::Unity.Mathematics.math.dot(float25, float13), global::Unity.Mathematics.math.dot(float26, float14), global::Unity.Mathematics.math.dot(float27, float15));
			global::Unity.Mathematics.float4 float31 = obj3 * float29 * float30;
			gradient = -8f * (float31.x * float8 + float31.y * float13 + float31.z * float14 + float31.w * float15);
			gradient += x5.x * float24 + x5.y * float25 + x5.z * float26 + x5.w * float27;
			gradient *= 42f;
			return 42f * global::Unity.Mathematics.math.dot(x5, float30);
		}

		public static float snoise(global::Unity.Mathematics.float4 v)
		{
			global::Unity.Mathematics.float4 float5 = global::Unity.Mathematics.math.float4(0.1381966f, 0.2763932f, 0.4145898f, -0.4472136f);
			global::Unity.Mathematics.float4 float6 = global::Unity.Mathematics.math.floor(v + global::Unity.Mathematics.math.dot(v, global::Unity.Mathematics.math.float4(0.309017f)));
			global::Unity.Mathematics.float4 float7 = v - float6 + global::Unity.Mathematics.math.dot(float6, float5.xxxx);
			global::Unity.Mathematics.float4 float8 = global::Unity.Mathematics.math.float4(0f);
			global::Unity.Mathematics.float3 float9 = global::Unity.Mathematics.math.step(float7.yzw, float7.xxx);
			global::Unity.Mathematics.float3 float10 = global::Unity.Mathematics.math.step(float7.zww, float7.yyz);
			float8.x = float9.x + float9.y + float9.z;
			float8.yzw = 1f - float9;
			float8.y += float10.x + float10.y;
			float8.zw += 1f - float10.xy;
			float8.z += float10.z;
			float8.w += 1f - float10.z;
			global::Unity.Mathematics.float4 float11 = global::Unity.Mathematics.math.clamp(float8, 0f, 1f);
			global::Unity.Mathematics.float4 float12 = global::Unity.Mathematics.math.clamp(float8 - 1f, 0f, 1f);
			global::Unity.Mathematics.float4 float13 = global::Unity.Mathematics.math.clamp(float8 - 2f, 0f, 1f);
			global::Unity.Mathematics.float4 float14 = float7 - float13 + float5.xxxx;
			global::Unity.Mathematics.float4 float15 = float7 - float12 + float5.yyyy;
			global::Unity.Mathematics.float4 float16 = float7 - float11 + float5.zzzz;
			global::Unity.Mathematics.float4 float17 = float7 + float5.wwww;
			float6 = mod289(float6);
			float j = permute(permute(permute(permute(float6.w) + float6.z) + float6.y) + float6.x);
			global::Unity.Mathematics.float4 obj = permute(permute(permute(permute(float6.w + global::Unity.Mathematics.math.float4(float13.w, float12.w, float11.w, 1f)) + float6.z + global::Unity.Mathematics.math.float4(float13.z, float12.z, float11.z, 1f)) + float6.y + global::Unity.Mathematics.math.float4(float13.y, float12.y, float11.y, 1f)) + float6.x + global::Unity.Mathematics.math.float4(float13.x, float12.x, float11.x, 1f));
			global::Unity.Mathematics.float4 ip = global::Unity.Mathematics.math.float4(0.0034013605f, 1f / 49f, 1f / 7f, 0f);
			global::Unity.Mathematics.float4 float18 = grad4(j, ip);
			global::Unity.Mathematics.float4 float19 = grad4(obj.x, ip);
			global::Unity.Mathematics.float4 float20 = grad4(obj.y, ip);
			global::Unity.Mathematics.float4 float21 = grad4(obj.z, ip);
			global::Unity.Mathematics.float4 float22 = grad4(obj.w, ip);
			global::Unity.Mathematics.float4 float23 = taylorInvSqrt(global::Unity.Mathematics.math.float4(global::Unity.Mathematics.math.dot(float18, float18), global::Unity.Mathematics.math.dot(float19, float19), global::Unity.Mathematics.math.dot(float20, float20), global::Unity.Mathematics.math.dot(float21, float21)));
			float18 *= float23.x;
			float19 *= float23.y;
			float20 *= float23.z;
			float21 *= float23.w;
			float22 *= taylorInvSqrt(global::Unity.Mathematics.math.dot(float22, float22));
			global::Unity.Mathematics.float3 float24 = global::Unity.Mathematics.math.max(0.6f - global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float7, float7), global::Unity.Mathematics.math.dot(float14, float14), global::Unity.Mathematics.math.dot(float15, float15)), 0f);
			global::Unity.Mathematics.float2 float25 = global::Unity.Mathematics.math.max(0.6f - global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(float16, float16), global::Unity.Mathematics.math.dot(float17, float17)), 0f);
			float24 *= float24;
			float25 *= float25;
			return 49f * (global::Unity.Mathematics.math.dot(float24 * float24, global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float18, float7), global::Unity.Mathematics.math.dot(float19, float14), global::Unity.Mathematics.math.dot(float20, float15))) + global::Unity.Mathematics.math.dot(float25 * float25, global::Unity.Mathematics.math.float2(global::Unity.Mathematics.math.dot(float21, float16), global::Unity.Mathematics.math.dot(float22, float17))));
		}

		public static global::Unity.Mathematics.float3 psrdnoise(global::Unity.Mathematics.float2 pos, global::Unity.Mathematics.float2 per, float rot)
		{
			pos.y += 0.01f;
			global::Unity.Mathematics.float2 x = global::Unity.Mathematics.math.float2(pos.x + pos.y * 0.5f, pos.y);
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.floor(x);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.frac(x);
			global::Unity.Mathematics.float2 float7 = ((float6.x > float6.y) ? global::Unity.Mathematics.math.float2(1f, 0f) : global::Unity.Mathematics.math.float2(0f, 1f));
			global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.float2(float5.x - float5.y * 0.5f, float5.y);
			global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(float8.x + float7.x - float7.y * 0.5f, float8.y + float7.y);
			global::Unity.Mathematics.float2 float10 = global::Unity.Mathematics.math.float2(float8.x + 0.5f, float8.y + 1f);
			global::Unity.Mathematics.float2 float11 = pos - float8;
			global::Unity.Mathematics.float2 float12 = pos - float9;
			global::Unity.Mathematics.float2 float13 = pos - float10;
			global::Unity.Mathematics.float3 obj = global::Unity.Mathematics.math.fmod(global::Unity.Mathematics.math.float3(float8.x, float9.x, float10.x), per.x);
			global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.fmod(global::Unity.Mathematics.math.float3(float8.y, float9.y, float10.y), per.y);
			global::Unity.Mathematics.float3 obj2 = obj + 0.5f * float14;
			global::Unity.Mathematics.float3 float15 = float14;
			global::Unity.Mathematics.float2 float16 = rgrad2(global::Unity.Mathematics.math.float2(obj2.x, float15.x), rot);
			global::Unity.Mathematics.float2 float17 = rgrad2(global::Unity.Mathematics.math.float2(obj2.y, float15.y), rot);
			global::Unity.Mathematics.float2 float18 = rgrad2(global::Unity.Mathematics.math.float2(obj2.z, float15.z), rot);
			global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float16, float11), global::Unity.Mathematics.math.dot(float17, float12), global::Unity.Mathematics.math.dot(float18, float13));
			global::Unity.Mathematics.float3 float19 = 0.8f - global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float11, float11), global::Unity.Mathematics.math.dot(float12, float12), global::Unity.Mathematics.math.dot(float13, float13));
			global::Unity.Mathematics.float3 float20 = -2f * global::Unity.Mathematics.math.float3(float11.x, float12.x, float13.x);
			global::Unity.Mathematics.float3 float21 = -2f * global::Unity.Mathematics.math.float3(float11.y, float12.y, float13.y);
			if (float19.x < 0f)
			{
				float20.x = 0f;
				float21.x = 0f;
				float19.x = 0f;
			}
			if (float19.y < 0f)
			{
				float20.y = 0f;
				float21.y = 0f;
				float19.y = 0f;
			}
			if (float19.z < 0f)
			{
				float20.z = 0f;
				float21.z = 0f;
				float19.z = 0f;
			}
			global::Unity.Mathematics.float3 obj3 = float19 * float19;
			global::Unity.Mathematics.float3 x2 = obj3 * obj3;
			global::Unity.Mathematics.float3 float22 = obj3 * float19;
			float x3 = global::Unity.Mathematics.math.dot(x2, y);
			global::Unity.Mathematics.float2 float23 = global::Unity.Mathematics.math.float2(float20.x, float21.x) * 4f * float22.x;
			global::Unity.Mathematics.float2 float24 = x2.x * float16 + float23 * y.x;
			global::Unity.Mathematics.float2 float25 = global::Unity.Mathematics.math.float2(float20.y, float21.y) * 4f * float22.y;
			global::Unity.Mathematics.float2 float26 = x2.y * float17 + float25 * y.y;
			global::Unity.Mathematics.float2 float27 = global::Unity.Mathematics.math.float2(float20.z, float21.z) * 4f * float22.z;
			global::Unity.Mathematics.float2 float28 = x2.z * float18 + float27 * y.z;
			return 11f * global::Unity.Mathematics.math.float3(x3, float24 + float26 + float28);
		}

		public static global::Unity.Mathematics.float3 psrdnoise(global::Unity.Mathematics.float2 pos, global::Unity.Mathematics.float2 per)
		{
			return psrdnoise(pos, per, 0f);
		}

		public static float psrnoise(global::Unity.Mathematics.float2 pos, global::Unity.Mathematics.float2 per, float rot)
		{
			pos.y += 0.001f;
			global::Unity.Mathematics.float2 x = global::Unity.Mathematics.math.float2(pos.x + pos.y * 0.5f, pos.y);
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.floor(x);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.frac(x);
			global::Unity.Mathematics.float2 float7 = ((float6.x > float6.y) ? global::Unity.Mathematics.math.float2(1f, 0f) : global::Unity.Mathematics.math.float2(0f, 1f));
			global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.float2(float5.x - float5.y * 0.5f, float5.y);
			global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(float8.x + float7.x - float7.y * 0.5f, float8.y + float7.y);
			global::Unity.Mathematics.float2 float10 = global::Unity.Mathematics.math.float2(float8.x + 0.5f, float8.y + 1f);
			global::Unity.Mathematics.float2 float11 = pos - float8;
			global::Unity.Mathematics.float2 float12 = pos - float9;
			global::Unity.Mathematics.float2 float13 = pos - float10;
			global::Unity.Mathematics.float3 obj = global::Unity.Mathematics.math.fmod(global::Unity.Mathematics.math.float3(float8.x, float9.x, float10.x), per.x);
			global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.fmod(global::Unity.Mathematics.math.float3(float8.y, float9.y, float10.y), per.y);
			global::Unity.Mathematics.float3 obj2 = obj + 0.5f * float14;
			global::Unity.Mathematics.float3 float15 = float14;
			global::Unity.Mathematics.float2 x2 = rgrad2(global::Unity.Mathematics.math.float2(obj2.x, float15.x), rot);
			global::Unity.Mathematics.float2 x3 = rgrad2(global::Unity.Mathematics.math.float2(obj2.y, float15.y), rot);
			global::Unity.Mathematics.float2 x4 = rgrad2(global::Unity.Mathematics.math.float2(obj2.z, float15.z), rot);
			global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(x2, float11), global::Unity.Mathematics.math.dot(x3, float12), global::Unity.Mathematics.math.dot(x4, float13));
			global::Unity.Mathematics.float3 obj3 = global::Unity.Mathematics.math.max(0.8f - global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float11, float11), global::Unity.Mathematics.math.dot(float12, float12), global::Unity.Mathematics.math.dot(float13, float13)), 0f);
			global::Unity.Mathematics.float3 obj4 = obj3 * obj3;
			float num = global::Unity.Mathematics.math.dot(obj4 * obj4, y);
			return 11f * num;
		}

		public static float psrnoise(global::Unity.Mathematics.float2 pos, global::Unity.Mathematics.float2 per)
		{
			return psrnoise(pos, per, 0f);
		}

		public static global::Unity.Mathematics.float3 srdnoise(global::Unity.Mathematics.float2 pos, float rot)
		{
			pos.y += 0.001f;
			global::Unity.Mathematics.float2 x = global::Unity.Mathematics.math.float2(pos.x + pos.y * 0.5f, pos.y);
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.floor(x);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.frac(x);
			global::Unity.Mathematics.float2 float7 = ((float6.x > float6.y) ? global::Unity.Mathematics.math.float2(1f, 0f) : global::Unity.Mathematics.math.float2(0f, 1f));
			global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.float2(float5.x - float5.y * 0.5f, float5.y);
			global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(float8.x + float7.x - float7.y * 0.5f, float8.y + float7.y);
			global::Unity.Mathematics.float2 float10 = global::Unity.Mathematics.math.float2(float8.x + 0.5f, float8.y + 1f);
			global::Unity.Mathematics.float2 float11 = pos - float8;
			global::Unity.Mathematics.float2 float12 = pos - float9;
			global::Unity.Mathematics.float2 float13 = pos - float10;
			global::Unity.Mathematics.float3 obj = global::Unity.Mathematics.math.float3(float8.x, float9.x, float10.x);
			global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.float3(float8.y, float9.y, float10.y);
			global::Unity.Mathematics.float3 x2 = obj + 0.5f * float14;
			global::Unity.Mathematics.float3 x3 = float14;
			global::Unity.Mathematics.float3 obj2 = mod289(x2);
			x3 = mod289(x3);
			global::Unity.Mathematics.float2 float15 = rgrad2(global::Unity.Mathematics.math.float2(obj2.x, x3.x), rot);
			global::Unity.Mathematics.float2 float16 = rgrad2(global::Unity.Mathematics.math.float2(obj2.y, x3.y), rot);
			global::Unity.Mathematics.float2 float17 = rgrad2(global::Unity.Mathematics.math.float2(obj2.z, x3.z), rot);
			global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float15, float11), global::Unity.Mathematics.math.dot(float16, float12), global::Unity.Mathematics.math.dot(float17, float13));
			global::Unity.Mathematics.float3 float18 = 0.8f - global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float11, float11), global::Unity.Mathematics.math.dot(float12, float12), global::Unity.Mathematics.math.dot(float13, float13));
			global::Unity.Mathematics.float3 float19 = -2f * global::Unity.Mathematics.math.float3(float11.x, float12.x, float13.x);
			global::Unity.Mathematics.float3 float20 = -2f * global::Unity.Mathematics.math.float3(float11.y, float12.y, float13.y);
			if (float18.x < 0f)
			{
				float19.x = 0f;
				float20.x = 0f;
				float18.x = 0f;
			}
			if (float18.y < 0f)
			{
				float19.y = 0f;
				float20.y = 0f;
				float18.y = 0f;
			}
			if (float18.z < 0f)
			{
				float19.z = 0f;
				float20.z = 0f;
				float18.z = 0f;
			}
			global::Unity.Mathematics.float3 obj3 = float18 * float18;
			global::Unity.Mathematics.float3 x4 = obj3 * obj3;
			global::Unity.Mathematics.float3 float21 = obj3 * float18;
			float x5 = global::Unity.Mathematics.math.dot(x4, y);
			global::Unity.Mathematics.float2 float22 = global::Unity.Mathematics.math.float2(float19.x, float20.x) * 4f * float21.x;
			global::Unity.Mathematics.float2 float23 = x4.x * float15 + float22 * y.x;
			global::Unity.Mathematics.float2 float24 = global::Unity.Mathematics.math.float2(float19.y, float20.y) * 4f * float21.y;
			global::Unity.Mathematics.float2 float25 = x4.y * float16 + float24 * y.y;
			global::Unity.Mathematics.float2 float26 = global::Unity.Mathematics.math.float2(float19.z, float20.z) * 4f * float21.z;
			global::Unity.Mathematics.float2 float27 = x4.z * float17 + float26 * y.z;
			return 11f * global::Unity.Mathematics.math.float3(x5, float23 + float25 + float27);
		}

		public static global::Unity.Mathematics.float3 srdnoise(global::Unity.Mathematics.float2 pos)
		{
			return srdnoise(pos, 0f);
		}

		public static float srnoise(global::Unity.Mathematics.float2 pos, float rot)
		{
			pos.y += 0.001f;
			global::Unity.Mathematics.float2 x = global::Unity.Mathematics.math.float2(pos.x + pos.y * 0.5f, pos.y);
			global::Unity.Mathematics.float2 float5 = global::Unity.Mathematics.math.floor(x);
			global::Unity.Mathematics.float2 float6 = global::Unity.Mathematics.math.frac(x);
			global::Unity.Mathematics.float2 float7 = ((float6.x > float6.y) ? global::Unity.Mathematics.math.float2(1f, 0f) : global::Unity.Mathematics.math.float2(0f, 1f));
			global::Unity.Mathematics.float2 float8 = global::Unity.Mathematics.math.float2(float5.x - float5.y * 0.5f, float5.y);
			global::Unity.Mathematics.float2 float9 = global::Unity.Mathematics.math.float2(float8.x + float7.x - float7.y * 0.5f, float8.y + float7.y);
			global::Unity.Mathematics.float2 float10 = global::Unity.Mathematics.math.float2(float8.x + 0.5f, float8.y + 1f);
			global::Unity.Mathematics.float2 float11 = pos - float8;
			global::Unity.Mathematics.float2 float12 = pos - float9;
			global::Unity.Mathematics.float2 float13 = pos - float10;
			global::Unity.Mathematics.float3 obj = global::Unity.Mathematics.math.float3(float8.x, float9.x, float10.x);
			global::Unity.Mathematics.float3 float14 = global::Unity.Mathematics.math.float3(float8.y, float9.y, float10.y);
			global::Unity.Mathematics.float3 x2 = obj + 0.5f * float14;
			global::Unity.Mathematics.float3 x3 = float14;
			global::Unity.Mathematics.float3 obj2 = mod289(x2);
			x3 = mod289(x3);
			global::Unity.Mathematics.float2 x4 = rgrad2(global::Unity.Mathematics.math.float2(obj2.x, x3.x), rot);
			global::Unity.Mathematics.float2 x5 = rgrad2(global::Unity.Mathematics.math.float2(obj2.y, x3.y), rot);
			global::Unity.Mathematics.float2 x6 = rgrad2(global::Unity.Mathematics.math.float2(obj2.z, x3.z), rot);
			global::Unity.Mathematics.float3 y = global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(x4, float11), global::Unity.Mathematics.math.dot(x5, float12), global::Unity.Mathematics.math.dot(x6, float13));
			global::Unity.Mathematics.float3 obj3 = global::Unity.Mathematics.math.max(0.8f - global::Unity.Mathematics.math.float3(global::Unity.Mathematics.math.dot(float11, float11), global::Unity.Mathematics.math.dot(float12, float12), global::Unity.Mathematics.math.dot(float13, float13)), 0f);
			global::Unity.Mathematics.float3 obj4 = obj3 * obj3;
			float num = global::Unity.Mathematics.math.dot(obj4 * obj4, y);
			return 11f * num;
		}

		public static float srnoise(global::Unity.Mathematics.float2 pos)
		{
			return srnoise(pos, 0f);
		}
	}
}
