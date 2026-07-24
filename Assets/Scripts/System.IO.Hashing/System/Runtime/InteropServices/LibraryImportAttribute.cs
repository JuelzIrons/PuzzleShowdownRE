namespace System.Runtime.InteropServices
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class LibraryImportAttribute : global::System.Attribute
	{
		public string LibraryName { get; }

		public string EntryPoint { get; set; }

		public global::System.Runtime.InteropServices.StringMarshalling StringMarshalling { get; set; }

		public global::System.Type StringMarshallingCustomType { get; set; }

		public bool SetLastError { get; set; }

		public LibraryImportAttribute(string libraryName)
		{
			LibraryName = libraryName;
		}
	}
}
