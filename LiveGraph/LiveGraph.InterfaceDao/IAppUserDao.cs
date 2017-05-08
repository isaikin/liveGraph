using LiveGraph.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LiveGraph.InterfaceDao
{
    public interface IAppUserDao
    {
		Guid? Login(string login, List<byte> password);

		bool Registration(AppUser appUser);

		IEnumerable<Claim> GetClaimsById(Guid id);

		string GetNameAppUserById(Guid id);

		UserErrors IsUserExists(AppUser appUser);
	}
}
