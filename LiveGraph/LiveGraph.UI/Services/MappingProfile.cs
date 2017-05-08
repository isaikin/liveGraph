using AutoMapper;
using LiveGraph.Common;
using LiveGraph.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGraph.UI.Services
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
		
			CreateMap<CreatePage, PageDto>().ForMember(x => x.Id, x => x.Ignore());
			CreateMap<PageDto, CreatePage>();
			CreateMap<IEnumerable<PageDto>, IEnumerable<CreatePage>>();
			CreateMap<PageDto, Page>();
			CreateMap<Page, PageDto>();
		}
	}
}
