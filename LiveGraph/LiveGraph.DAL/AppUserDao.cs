using LiveGraph.InterfaceDao;
using System;
using System.Collections.Generic;
using System.Text;
using LiveGraph.Common;
using Dapper;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Linq;
using System.Data;

namespace LiveGraph.DAL
{
    public class AppUserDao : BaseDao, IAppUserDao
	{
		public AppUserDao(IOptions<SettingsDao> connectionString) : base(connectionString.Value.ConnectionString)
		{
		}

		public Guid? Login(string login, List<byte> password)
		{
			using (var connection = base.GetConnection())
			{
				var result = connection.ExecuteScalar<Guid?>("AppUserLoggin", new { Login = login, Password = password.ToArray() }, commandType: System.Data.CommandType.StoredProcedure);

				return result;
			}
		}
		public IEnumerable<Claim> GetClaimsById(Guid id)
		{
			using (var connection = base.GetConnection())
			{
				var result = connection.Query<ClaimDto>("GetClaims", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);


				return result.Select(claim =>  new Claim(claim.Type,claim.Value)) ;
			}
		}

		public string GetNameAppUserById(Guid id)
		{
			using (var connection = base.GetConnection())
			{
				var result = connection.QueryFirst<string>("GetNameAppUserById", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);


				return result;
			}
		}
		public bool Registration(AppUser appUser)
		{
			using (var connection = base.GetConnection())
			{
				 connection.Execute("AppUserRegistration",
					new
					{
						Password = appUser.Password.ToArray(),
						Email = appUser.Email,
						Login = appUser.Login,
					}, commandType: System.Data.CommandType.StoredProcedure);

				return true;
			}
		}

		public UserErrors IsUserExists(AppUser appUser)
		{
			using (var connection = base.GetConnection())
			{
				var errors = new UserErrors();
				var parametrs = new DynamicParameters();

				parametrs.Add("Email", appUser.Email, System.Data.DbType.String, ParameterDirection.Input);
				parametrs.Add("Login", appUser.Login, System.Data.DbType.String, ParameterDirection.Input);
				parametrs.Add("IsUserExislEmail", errors.IsUserExislEmail, System.Data.DbType.Boolean, ParameterDirection.Output);
				parametrs.Add("IsUserExistsLogin", errors.IsUserExistsLogin, System.Data.DbType.Boolean, ParameterDirection.Output);

				connection.Query<int>("IsUserExists", parametrs, commandType: CommandType.StoredProcedure);
				
				errors.IsUserExislEmail = parametrs.Get<bool>("IsUserExislEmail");
				errors.IsUserExistsLogin = parametrs.Get<bool>("IsUserExistsLogin");

				return errors;
			}
		}
	}
}
