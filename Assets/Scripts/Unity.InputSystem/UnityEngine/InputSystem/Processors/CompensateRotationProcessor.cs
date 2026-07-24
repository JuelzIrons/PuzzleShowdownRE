namespace UnityEngine.InputSystem.Processors
{
	[global::System.ComponentModel.DesignTimeVisible(false)]
	internal class CompensateRotationProcessor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Quaternion>
	{
		public override global::UnityEngine.InputSystem.InputProcessor.CachingPolicy cachingPolicy => global::UnityEngine.InputSystem.InputProcessor.CachingPolicy.EvaluateOnEveryRead;

		public override global::UnityEngine.Quaternion Process(global::UnityEngine.Quaternion value, global::UnityEngine.InputSystem.InputControl control)
		{
			if (!global::UnityEngine.InputSystem.InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.identity;
			switch (global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.screenOrientation)
			{
			case global::UnityEngine.ScreenOrientation.PortraitUpsideDown:
				quaternion = new global::UnityEngine.Quaternion(0f, 0f, 1f, 0f);
				break;
			case global::UnityEngine.ScreenOrientation.LandscapeLeft:
				quaternion = new global::UnityEngine.Quaternion(0f, 0f, 0.70710677f, -0.70710677f);
				break;
			case global::UnityEngine.ScreenOrientation.LandscapeRight:
				quaternion = new global::UnityEngine.Quaternion(0f, 0f, -0.70710677f, -0.70710677f);
				break;
			}
			return value * quaternion;
		}

		public override string ToString()
		{
			return "CompensateRotation()";
		}
	}
}
