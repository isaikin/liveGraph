using LiveGraph.Common;
using LiveGraph.InterfaceDao;
using System.Resources;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using LiveGraph.Validation.Interface;

namespace LiveGraph.Validation
{
	public class AppUserValidation: IAppUserValidation
	{
		private readonly IAppUserDao _appUserDao;
		private readonly IStringLocalizer<AppUserValidation> _localize;
		public AppUserValidation(IAppUserDao appUserDao, IStringLocalizer<AppUserValidation> localize)
		{
			_appUserDao = appUserDao;
			_localize = localize;
		}
		public bool IsValid(AppUser appUser,out List<CustomError> errors)
		{
			errors = new List<CustomError>();

			return IsUserExists(appUser, errors);
		}

		private bool IsUserExists(AppUser appUser, List<CustomError> errors)
		{
			var appUserErrors = _appUserDao.IsUserExists(appUser);

			var flag = true;

			if (appUserErrors.IsUserExistsLogin)
			{
				errors.Add
					(
						new CustomError()
						{
							ErrorMessage = _localize["ErrorUserExistsLogin"].Value,
							Key = nameof(appUser.Login)
						}
					);
				flag = false;
			}

			if (appUserErrors.IsUserExislEmail)
			{
				errors.Add
					(
						new CustomError()
						{
							ErrorMessage = _localize["ErrorUserExistsEmail"].Value,
							Key = nameof(appUser.Email)
						}
					);

				flag = false;
			}

			return flag;
		}
	}
}
