using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGraph.UI.Extension
{
    public static class ExtansionServeces
    {
		public static void AddClaims(this IServiceCollection services, IConfigurationRoot configuration)
		{
			var temp = configuration.GetSection("Claims").Value.Split(new[] { ',', ' '}, StringSplitOptions.RemoveEmptyEntries);

			services.AddAuthorization(options =>
			{
				for (int i = 0; i < temp.Length; i += 2)
				{
					options.AddPolicy(temp[i], policy => policy.RequireClaim(temp[i + 1]));
				}
			});
		}
    }
}
