using LiveGraph.InterfaceDao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LiveGraph.DAL
{
	public abstract class BaseDao
	{
		#region [Fields]
		private readonly string connectionString;

		#endregion [/Fields]

		#region [Ctor]
		public BaseDao(string connectionString)
		{
			this.connectionString = connectionString;
		}
		#endregion [/Ctor]

		#region [Method]
		protected SqlConnection GetConnection()
		{
			return new SqlConnection(connectionString);
		}
		#endregion [/Method]
	}
}
