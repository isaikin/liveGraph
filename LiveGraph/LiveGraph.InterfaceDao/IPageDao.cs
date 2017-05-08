using LiveGraph.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveGraph.InterfaceDao
{
	public interface IPageDao
	{
		IEnumerable<PageDto> GetAll();

		void Add(PageDto page);

		PageDto GetById(int id);

		void Update(PageDto page);
	}
}
