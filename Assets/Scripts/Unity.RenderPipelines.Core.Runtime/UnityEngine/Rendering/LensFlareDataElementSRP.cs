namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public sealed class LensFlareDataElementSRP
	{
		public global::UnityEngine.Rendering.LensFlareDataSRP lensFlareDataSRP;

		public bool visible;

		public float position;

		public global::UnityEngine.Vector2 positionOffset;

		public float angularOffset;

		public global::UnityEngine.Vector2 translationScale;

		[global::UnityEngine.Range(0f, 1f)]
		public float ringThickness;

		[global::UnityEngine.Range(-1f, 1f)]
		public float hoopFactor;

		public float noiseAmplitude;

		public int noiseFrequency;

		public float noiseSpeed;

		public float shapeCutOffSpeed;

		public float shapeCutOffRadius;

		[global::UnityEngine.Min(0f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("localIntensity")]
		private float m_LocalIntensity;

		public global::UnityEngine.Texture lensFlareTexture;

		public float uniformScale;

		public global::UnityEngine.Vector2 sizeXY;

		public bool allowMultipleElement;

		[global::UnityEngine.Min(1f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("count")]
		private int m_Count;

		public bool preserveAspectRatio;

		public float rotation;

		public global::UnityEngine.Rendering.SRPLensFlareColorType tintColorType;

		public global::UnityEngine.Color tint;

		public global::UnityEngine.Rendering.TextureGradient tintGradient;

		public global::UnityEngine.Rendering.SRPLensFlareBlendMode blendMode;

		public bool autoRotate;

		public global::UnityEngine.Rendering.SRPLensFlareType flareType;

		public bool modulateByLightColor;

		[global::UnityEngine.SerializeField]
		private bool isFoldOpened;

		public global::UnityEngine.Rendering.SRPLensFlareDistribution distribution;

		public float lengthSpread;

		public global::UnityEngine.AnimationCurve positionCurve;

		public global::UnityEngine.AnimationCurve scaleCurve;

		public int seed;

		public global::UnityEngine.Gradient colorGradient;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("intensityVariation")]
		private float m_IntensityVariation;

		public global::UnityEngine.Vector2 positionVariation;

		public float scaleVariation;

		public float rotationVariation;

		public bool enableRadialDistortion;

		public global::UnityEngine.Vector2 targetSizeDistortion;

		public global::UnityEngine.AnimationCurve distortionCurve;

		public bool distortionRelativeToCenter;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("fallOff")]
		private float m_FallOff;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("edgeOffset")]
		private float m_EdgeOffset;

		[global::UnityEngine.Min(3f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("sideCount")]
		private int m_SideCount;

		[global::UnityEngine.Range(0f, 1f)]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Serialization.FormerlySerializedAs("sdfRoundness")]
		private float m_SdfRoundness;

		public bool inverseSDF;

		public float uniformAngle;

		public global::UnityEngine.AnimationCurve uniformAngleCurve;

		public float localIntensity
		{
			get
			{
				return m_LocalIntensity;
			}
			set
			{
				m_LocalIntensity = global::UnityEngine.Mathf.Max(0f, value);
			}
		}

		public int count
		{
			get
			{
				return m_Count;
			}
			set
			{
				m_Count = global::UnityEngine.Mathf.Max(1, value);
			}
		}

		public float intensityVariation
		{
			get
			{
				return m_IntensityVariation;
			}
			set
			{
				m_IntensityVariation = global::UnityEngine.Mathf.Max(0f, value);
			}
		}

		public float fallOff
		{
			get
			{
				return m_FallOff;
			}
			set
			{
				m_FallOff = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public float edgeOffset
		{
			get
			{
				return m_EdgeOffset;
			}
			set
			{
				m_EdgeOffset = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public int sideCount
		{
			get
			{
				return m_SideCount;
			}
			set
			{
				m_SideCount = global::UnityEngine.Mathf.Max(3, value);
			}
		}

		public float sdfRoundness
		{
			get
			{
				return m_SdfRoundness;
			}
			set
			{
				m_SdfRoundness = global::UnityEngine.Mathf.Clamp01(value);
			}
		}

		public LensFlareDataElementSRP()
		{
			lensFlareDataSRP = null;
			visible = true;
			localIntensity = 1f;
			position = 0f;
			positionOffset = new global::UnityEngine.Vector2(0f, 0f);
			angularOffset = 0f;
			translationScale = new global::UnityEngine.Vector2(1f, 1f);
			lensFlareTexture = null;
			uniformScale = 1f;
			sizeXY = global::UnityEngine.Vector2.one;
			allowMultipleElement = false;
			count = 5;
			rotation = 0f;
			preserveAspectRatio = false;
			ringThickness = 0.25f;
			hoopFactor = 1f;
			noiseAmplitude = 1f;
			noiseFrequency = 1;
			noiseSpeed = 0f;
			shapeCutOffSpeed = 0f;
			shapeCutOffRadius = 10f;
			tintColorType = global::UnityEngine.Rendering.SRPLensFlareColorType.Constant;
			tint = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);
			tintGradient = new global::UnityEngine.Rendering.TextureGradient(new global::UnityEngine.GradientColorKey[2]
			{
				new global::UnityEngine.GradientColorKey(global::UnityEngine.Color.black, 0f),
				new global::UnityEngine.GradientColorKey(global::UnityEngine.Color.white, 1f)
			}, new global::UnityEngine.GradientAlphaKey[2]
			{
				new global::UnityEngine.GradientAlphaKey(0f, 0f),
				new global::UnityEngine.GradientAlphaKey(1f, 1f)
			});
			blendMode = global::UnityEngine.Rendering.SRPLensFlareBlendMode.Additive;
			autoRotate = false;
			isFoldOpened = true;
			flareType = global::UnityEngine.Rendering.SRPLensFlareType.Circle;
			distribution = global::UnityEngine.Rendering.SRPLensFlareDistribution.Uniform;
			lengthSpread = 1f;
			colorGradient = new global::UnityEngine.Gradient();
			colorGradient.SetKeys(new global::UnityEngine.GradientColorKey[2]
			{
				new global::UnityEngine.GradientColorKey(global::UnityEngine.Color.white, 0f),
				new global::UnityEngine.GradientColorKey(global::UnityEngine.Color.white, 1f)
			}, new global::UnityEngine.GradientAlphaKey[2]
			{
				new global::UnityEngine.GradientAlphaKey(1f, 0f),
				new global::UnityEngine.GradientAlphaKey(1f, 1f)
			});
			positionCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f, 1f, 1f), new global::UnityEngine.Keyframe(1f, 1f, 1f, -1f));
			scaleCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 1f), new global::UnityEngine.Keyframe(1f, 1f));
			uniformAngle = 0f;
			uniformAngleCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f), new global::UnityEngine.Keyframe(1f, 0f));
			seed = 0;
			intensityVariation = 0.75f;
			positionVariation = new global::UnityEngine.Vector2(1f, 0f);
			scaleVariation = 1f;
			rotationVariation = 180f;
			enableRadialDistortion = false;
			targetSizeDistortion = global::UnityEngine.Vector2.one;
			distortionCurve = new global::UnityEngine.AnimationCurve(new global::UnityEngine.Keyframe(0f, 0f, 1f, 1f), new global::UnityEngine.Keyframe(1f, 1f, 1f, -1f));
			distortionRelativeToCenter = false;
			fallOff = 1f;
			edgeOffset = 0.1f;
			sdfRoundness = 0f;
			sideCount = 6;
			inverseSDF = false;
		}

		public global::UnityEngine.Rendering.LensFlareDataElementSRP Clone()
		{
			global::UnityEngine.Rendering.LensFlareDataElementSRP lensFlareDataElementSRP = new global::UnityEngine.Rendering.LensFlareDataElementSRP();
			lensFlareDataElementSRP.lensFlareDataSRP = lensFlareDataSRP;
			lensFlareDataElementSRP.visible = visible;
			lensFlareDataElementSRP.localIntensity = localIntensity;
			lensFlareDataElementSRP.position = position;
			lensFlareDataElementSRP.positionOffset = positionOffset;
			lensFlareDataElementSRP.angularOffset = angularOffset;
			lensFlareDataElementSRP.translationScale = translationScale;
			lensFlareDataElementSRP.lensFlareTexture = lensFlareTexture;
			lensFlareDataElementSRP.uniformScale = uniformScale;
			lensFlareDataElementSRP.sizeXY = sizeXY;
			lensFlareDataElementSRP.allowMultipleElement = allowMultipleElement;
			lensFlareDataElementSRP.count = count;
			lensFlareDataElementSRP.rotation = rotation;
			lensFlareDataElementSRP.preserveAspectRatio = preserveAspectRatio;
			lensFlareDataElementSRP.ringThickness = ringThickness;
			lensFlareDataElementSRP.hoopFactor = hoopFactor;
			lensFlareDataElementSRP.noiseAmplitude = noiseAmplitude;
			lensFlareDataElementSRP.noiseFrequency = noiseFrequency;
			lensFlareDataElementSRP.noiseSpeed = noiseSpeed;
			lensFlareDataElementSRP.shapeCutOffSpeed = shapeCutOffSpeed;
			lensFlareDataElementSRP.shapeCutOffRadius = shapeCutOffRadius;
			lensFlareDataElementSRP.tintColorType = tintColorType;
			lensFlareDataElementSRP.tint = tint;
			lensFlareDataElementSRP.tintGradient = new global::UnityEngine.Rendering.TextureGradient(tintGradient.colorKeys, tintGradient.alphaKeys, tintGradient.mode, tintGradient.colorSpace, tintGradient.textureSize);
			lensFlareDataElementSRP.tintGradient = new global::UnityEngine.Rendering.TextureGradient(tintGradient.colorKeys, tintGradient.alphaKeys);
			lensFlareDataElementSRP.blendMode = blendMode;
			lensFlareDataElementSRP.autoRotate = autoRotate;
			lensFlareDataElementSRP.isFoldOpened = isFoldOpened;
			lensFlareDataElementSRP.flareType = flareType;
			lensFlareDataElementSRP.distribution = distribution;
			lensFlareDataElementSRP.lengthSpread = lengthSpread;
			lensFlareDataElementSRP.colorGradient = new global::UnityEngine.Gradient();
			lensFlareDataElementSRP.colorGradient.SetKeys(colorGradient.colorKeys, colorGradient.alphaKeys);
			lensFlareDataElementSRP.colorGradient.mode = colorGradient.mode;
			lensFlareDataElementSRP.colorGradient.colorSpace = colorGradient.colorSpace;
			lensFlareDataElementSRP.positionCurve = new global::UnityEngine.AnimationCurve(positionCurve.keys);
			lensFlareDataElementSRP.scaleCurve = new global::UnityEngine.AnimationCurve(scaleCurve.keys);
			lensFlareDataElementSRP.uniformAngle = uniformAngle;
			lensFlareDataElementSRP.uniformAngleCurve = new global::UnityEngine.AnimationCurve(uniformAngleCurve.keys);
			lensFlareDataElementSRP.seed = seed;
			lensFlareDataElementSRP.intensityVariation = intensityVariation;
			lensFlareDataElementSRP.positionVariation = positionVariation;
			lensFlareDataElementSRP.scaleVariation = scaleVariation;
			lensFlareDataElementSRP.rotationVariation = rotationVariation;
			lensFlareDataElementSRP.enableRadialDistortion = enableRadialDistortion;
			lensFlareDataElementSRP.targetSizeDistortion = targetSizeDistortion;
			lensFlareDataElementSRP.distortionCurve = new global::UnityEngine.AnimationCurve(distortionCurve.keys);
			lensFlareDataElementSRP.distortionRelativeToCenter = distortionRelativeToCenter;
			lensFlareDataElementSRP.fallOff = fallOff;
			lensFlareDataElementSRP.edgeOffset = edgeOffset;
			lensFlareDataElementSRP.sdfRoundness = sdfRoundness;
			lensFlareDataElementSRP.sideCount = sideCount;
			lensFlareDataElementSRP.inverseSDF = inverseSDF;
			return lensFlareDataElementSRP;
		}
	}
}
