using LiveGraph.InterfaceBLL;
using System;
using System.Collections.Generic;
using System.Text;
using LiveGraph.Common;
using LiveGraph.InterfaceDao;

namespace LiveGraph.BLL
{
	public class PageBLL : IPageBLL
	{
		public readonly IPageDao _pageDao;
		public PageBLL(IPageDao pageDao)
		{
			_pageDao = pageDao;
		}
		public void Add(PageDto page)
		{
			_pageDao.Add(page);
		}

		public IEnumerable<PageDto> GetAll()
		{
			return _pageDao.GetAll();
		}

		public PageDto GetById(int id)
		{
			return _pageDao.GetById(id);
		}
		public void Update(PageDto page)
		{
			_pageDao.Update(page);
		}
	}
}
