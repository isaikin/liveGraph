using LiveGraph.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.Validation.Interface
{
    public interface IAppUserValidation
    {
		bool IsValid(AppUser appUser, out List<CustomError> errors);
	}
}
