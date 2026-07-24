namespace Unity.Services.Lobbies.Http
{
	[global::UnityEngine.Scripting.Preserve]
	internal class BasicError : global::Unity.Services.Lobbies.Http.IError
	{
		[global::UnityEngine.Scripting.Preserve]
		public string Type { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string Title { get; }

		[global::UnityEngine.Scripting.Preserve]
		public int? Status { get; }

		[global::UnityEngine.Scripting.Preserve]
		public int Code { get; }

		[global::UnityEngine.Scripting.Preserve]
		public string Detail { get; }

		[global::UnityEngine.Scripting.Preserve]
		public BasicError(string type, string title, int? status, int code, string detail)
		{
			Type = type;
			Title = title;
			Status = status;
			Code = code;
			Detail = detail;
		}

		public override string ToString()
		{
			return global::Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}
	}
}
