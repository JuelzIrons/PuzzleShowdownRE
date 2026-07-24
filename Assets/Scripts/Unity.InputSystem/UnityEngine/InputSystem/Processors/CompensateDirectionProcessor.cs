namespace UnityEngine.InputSystem.Processors
{
	[global::System.ComponentModel.DesignTimeVisible(false)]
	internal class CompensateDirectionProcessor : global::UnityEngine.InputSystem.InputProcessor<global::UnityEngine.Vector3>
	{
		public override global::UnityEngine.InputSystem.InputProcessor.CachingPolicy cachingPolicy => global::UnityEngine.InputSystem.InputProcessor.CachingPolicy.EvaluateOnEveryRead;

		public override global::UnityEngine.Vector3 Process(global::UnityEngine.Vector3 value, global::UnityEngine.InputSystem.InputControl control)
		{
			if (!global::UnityEngine.InputSystem.InputSystem.settings.compensateForScreenOrientation)
			{
				return value;
			}
			global::UnityEngine.Quaternion quaternion = global::UnityEngine.Quaternion.identity;
			switch (global::UnityEngine.InputSystem.LowLevel.InputRuntime.s_Instance.screenOrientation)
			{
			case global::UnityEngine.ScreenOrientation.PortraitUpsideDown:
				quaternion = global::UnityEngine.Quaternion.Euler(0f, 0f, 180f);
				break;
			case global::UnityEngine.ScreenOrientation.LandscapeLeft:
				quaternion = global::UnityEngine.Quaternion.Euler(0f, 0f, 90f);
				break;
			case global::UnityEngine.ScreenOrientation.LandscapeRight:
				quaternion = global::UnityEngine.Quaternion.Euler(0f, 0f, 270f);
				break;
			}
			return quaternion * value;
		}

		public override string ToString()
		{
			return "CompensateDirection()";
		}
	}
}
