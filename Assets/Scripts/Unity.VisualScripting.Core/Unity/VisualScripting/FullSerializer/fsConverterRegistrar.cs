namespace Unity.VisualScripting.FullSerializer
{
	public class fsConverterRegistrar
	{
		public static global::Unity.VisualScripting.FullSerializer.AnimationCurve_DirectConverter Register_AnimationCurve_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.Bounds_DirectConverter Register_Bounds_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.Gradient_DirectConverter Register_Gradient_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.GUIStyleState_DirectConverter Register_GUIStyleState_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.GUIStyle_DirectConverter Register_GUIStyle_DirectConverter;

		[global::JetBrains.Annotations.UsedImplicitly]
		public static global::Unity.VisualScripting.FullSerializer.InputAction_DirectConverter Register_InputAction_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.Keyframe_DirectConverter Register_Keyframe_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.LayerMask_DirectConverter Register_LayerMask_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.RectOffset_DirectConverter Register_RectOffset_DirectConverter;

		public static global::Unity.VisualScripting.FullSerializer.Rect_DirectConverter Register_Rect_DirectConverter;

		public static global::System.Collections.Generic.List<global::System.Type> Converters;

		static fsConverterRegistrar()
		{
			Converters = new global::System.Collections.Generic.List<global::System.Type>();
			global::System.Reflection.FieldInfo[] declaredFields = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredFields(typeof(global::Unity.VisualScripting.FullSerializer.fsConverterRegistrar));
			foreach (global::System.Reflection.FieldInfo fieldInfo in declaredFields)
			{
				if (fieldInfo.Name.StartsWith("Register_"))
				{
					Converters.Add(fieldInfo.FieldType);
				}
			}
			global::System.Reflection.MethodInfo[] declaredMethods = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredMethods(typeof(global::Unity.VisualScripting.FullSerializer.fsConverterRegistrar));
			foreach (global::System.Reflection.MethodInfo methodInfo in declaredMethods)
			{
				if (methodInfo.Name.StartsWith("Register_"))
				{
					methodInfo.Invoke(null, null);
				}
			}
		}
	}
}
