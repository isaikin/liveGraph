using Dapper;
using LiveGraph.Common;
using LiveGraph.InterfaceDao;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LiveGraph.DAL
{
	public class PageDao : BaseDao, IPageDao
	{
		public PageDao(IOptions<SettingsDao> connectionString) : base(connectionString.Value.ConnectionString)
		{
		}

		public IEnumerable<PageDto> GetAll()
		{
			using (var connection = base.GetConnection())
			{
				var result = connection.Query<PageDto>("GetAllPages", null, commandType: CommandType.StoredProcedure);

				return result;
			}
		}

		public PageDto GetById(int id)
		{
			using (var connection = base.GetConnection())
			{
				var result = connection.QueryFirst<PageDto>("GetPageById", new { Id = id }, commandType: CommandType.StoredProcedure);

				return result;
			}
		}

		public void Add(PageDto page)
		{
			using (var connection = base.GetConnection())
			{
				connection.Execute("AddPages",new { Name = page.Name, Text  = page.Text, Description = page.Description}, commandType: CommandType.StoredProcedure);
			}
		}

		public void Update(PageDto page)
		{
			using (var connection = base.GetConnection())
			{
				connection.Execute("UpdatePages", new { Name = page.Name, Text = page.Text, Description = page.Description, Id = page.Id }, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
