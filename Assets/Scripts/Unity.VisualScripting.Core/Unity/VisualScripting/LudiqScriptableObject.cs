namespace Unity.VisualScripting
{
	public abstract class LudiqScriptableObject : global::UnityEngine.ScriptableObject, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::UnityEngine.SerializeField]
		[global::Unity.VisualScripting.DoNotSerialize]
		protected global::Unity.VisualScripting.SerializationData _data;

		internal event global::System.Action OnDestroyActions;

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			if (!global::Unity.VisualScripting.Serialization.isCustomSerializing)
			{
				global::Unity.VisualScripting.Serialization.isUnitySerializing = true;
				try
				{
					OnBeforeSerialize();
					_data = this.Serialize(forceReflected: true);
					OnAfterSerialize();
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogError($"Failed to serialize scriptable object.\n{arg}", this);
				}
				global::Unity.VisualScripting.Serialization.isUnitySerializing = false;
			}
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (!global::Unity.VisualScripting.Serialization.isCustomSerializing)
			{
				global::Unity.VisualScripting.Serialization.isUnitySerializing = true;
				try
				{
					object instance = this;
					OnBeforeDeserialize();
					_data.DeserializeInto(ref instance, forceReflected: true);
					OnAfterDeserialize();
					_data.Clear();
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogError($"Failed to deserialize scriptable object.\n{arg}", this);
				}
				global::Unity.VisualScripting.Serialization.isUnitySerializing = false;
			}
		}

		protected virtual void OnBeforeSerialize()
		{
		}

		protected virtual void OnAfterSerialize()
		{
		}

		protected virtual void OnBeforeDeserialize()
		{
		}

		protected virtual void OnAfterDeserialize()
		{
		}

		protected virtual void OnPostDeserializeInEditor()
		{
		}

		private void OnDestroy()
		{
			this.OnDestroyActions?.Invoke();
		}

		protected virtual void ShowData()
		{
			global::Unity.VisualScripting.SerializationData serializationData = this.Serialize(forceReflected: true);
			serializationData.ShowString(ToString());
			serializationData.Clear();
		}

		public override string ToString()
		{
			return this.ToSafeString();
		}
	}
}
