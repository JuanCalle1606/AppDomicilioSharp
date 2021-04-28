namespace ICommon
{
	[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	public sealed class AuthorAttribute : System.Attribute
	{
		// See the attribute guidelines at
		//  http://go.microsoft.com/fwlink/?LinkId=85236
		readonly string name;

		// This is a positional argument
		public AuthorAttribute(string name)
		{
			this.name = name;

			// TODO: Implement code here
			throw new System.NotImplementedException();
		}

		public string Name
		{
			get { return name; }
		}
	}
}