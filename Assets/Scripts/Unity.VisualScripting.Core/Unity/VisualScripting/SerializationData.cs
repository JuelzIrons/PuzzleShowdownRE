namespace Unity.VisualScripting
{
	[global::System.Serializable]
	public struct SerializationData
	{
		[global::UnityEngine.SerializeField]
		private string _json;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Object[] _objectReferences;

		public string json => _json;

		public global::UnityEngine.Object[] objectReferences => _objectReferences;

		public SerializationData(string json, global::System.Collections.Generic.IEnumerable<global::UnityEngine.Object> objectReferences)
		{
			_json = json;
			_objectReferences = global::System.Linq.Enumerable.ToArray(objectReferences?) ?? global::Unity.VisualScripting.Empty<global::UnityEngine.Object>.array;
		}

		public SerializationData(string json, params global::UnityEngine.Object[] objectReferences)
			: this(json, (global::System.Collections.Generic.IEnumerable<global::UnityEngine.Object>)objectReferences)
		{
		}

		internal void Clear()
		{
			_json = null;
			_objectReferences = null;
		}

		public string ToString(string title)
		{
			using global::System.IO.StringWriter stringWriter = new global::System.IO.StringWriter();
			if (!string.IsNullOrEmpty(title))
			{
				stringWriter.WriteLine(title);
				stringWriter.WriteLine();
			}
			stringWriter.WriteLine("Object References: ");
			if (objectReferences.Length == 0)
			{
				stringWriter.WriteLine("(None)");
			}
			else
			{
				int num = 0;
				global::UnityEngine.Object[] array = objectReferences;
				foreach (global::UnityEngine.Object obj in array)
				{
					if (obj.IsUnityNull())
					{
						stringWriter.WriteLine($"{num}: null");
					}
					else if (global::Unity.VisualScripting.UnityThread.allowsAPI)
					{
						stringWriter.WriteLine($"{num}: {obj.GetType().FullName} [{obj.GetHashCode()}] \"{obj.name}\"");
					}
					else
					{
						stringWriter.WriteLine($"{num}: {obj.GetType().FullName} [{obj.GetHashCode()}]");
					}
					num++;
				}
			}
			stringWriter.WriteLine();
			stringWriter.WriteLine("JSON: ");
			stringWriter.WriteLine(global::Unity.VisualScripting.Serialization.PrettyPrint(json));
			return stringWriter.ToString();
		}

		public override string ToString()
		{
			return ToString(null);
		}

		public void ShowString(string title = null)
		{
			string text = global::System.IO.Path.GetTempPath() + global::System.Guid.NewGuid().ToString() + ".json";
			global::System.IO.File.WriteAllText(text, ToString(title));
			global::System.Diagnostics.Process.Start(text);
		}
	}
}
