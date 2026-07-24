namespace Unity.Services.Authentication.Shared
{
	internal interface IApiAccessor
	{
		global::Unity.Services.Authentication.Shared.IApiConfiguration Configuration { get; }

		string GetBasePath();
	}
}
