using LiveGraph.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGraph.UI.Extension
{
    public static class ExtensionModelStateDictionary
    {
		public static void AddModelError(this ModelStateDictionary modelState, List<CustomError> errors)
		{
			foreach (var item in errors)
			{
				modelState.AddModelError(item.Key, item.ErrorMessage);
			}
		}
	}
}
