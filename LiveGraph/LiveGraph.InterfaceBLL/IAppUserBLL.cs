using LiveGraph.Common;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Security.Claims;
namespace LiveGraph.InterfaceBLL
{
	public interface IAppUserBLL
	{
		Guid? Login(string login, string password);

		bool Registration(AppUser appUser, out List<CustomError> errors);

		ClaimsPrincipal ClaimsPrincipalById(Guid id);
	}
}
