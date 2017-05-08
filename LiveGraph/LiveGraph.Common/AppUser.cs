using System;
using System.Collections.Generic;
using System.Security;

namespace LiveGraph.Common
{
	public class AppUser
	{
		public Guid? Id { get; set; }

		public string Login { get; set; }

		public List<byte> Password { get; set; }

		public string Email { get; set; }
	}
}