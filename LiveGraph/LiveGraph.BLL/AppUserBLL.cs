using LiveGraph.Common;
using LiveGraph.InterfaceBLL;
using LiveGraph.InterfaceDao;
using System.Linq;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System;
using LiveGraph.Validation.Interface;

namespace LiveGraph.BLL
{
	public class AppUserBLL : IAppUserBLL
	{
		#region [Fierlds]

		private IAppUserDao _appUserDao;
		IAppUserValidation _appUserValidation;
		#endregion [Fierlds]

		#region [Ctor]
		public AppUserBLL(IAppUserDao appUserDao, IAppUserValidation appUserValidation)
		{
			_appUserDao = appUserDao;
			_appUserValidation = appUserValidation;
		}

		#endregion	[/Ctor]

		#region [Method]
		public Guid? Login(string login, string password)
		{
			byte[] data = password.Select(x => (byte)x).ToArray();
			byte[] result;
			SHA512 shaM =  SHA512.Create();
			result = shaM.ComputeHash(data);

			return _appUserDao.Login(login, result.ToList());
		}

		public bool Registration(AppUser appUser, out List<CustomError> errors)
		{
			if(!_appUserValidation.IsValid(appUser, out errors))
			{
				return false;
			}

			SHA512 shaM = SHA512.Create();
			var temp = appUser.Password.ToArray();
			appUser.Password = shaM.ComputeHash(temp).ToList();
			return _appUserDao.Registration(appUser);
		}

		public ClaimsPrincipal ClaimsPrincipalById(Guid id)
		{
			var claims = _appUserDao.GetClaimsById(id).ToList();
			var name = _appUserDao.GetNameAppUserById(id);
			claims.Add
			(
				new Claim(ClaimTypes.Name, name)
			);
			var claimsIdentity = new ClaimsIdentity(claims, "password");
			var principal = new ClaimsPrincipal(claimsIdentity);

			return principal;
		}

		#endregion [/Method]
	}
}

