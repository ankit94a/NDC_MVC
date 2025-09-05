using AutoMapper;
using NDCWeb.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NDCWeb.Infrastructure.Constants;

namespace NDCWeb.Infrastructure.Helpers.Menu
{
    public class SiteMenuManager
    {
        public List<MenuControlVM> GetMenuItems(PositionType positionType = PositionType.Top, string area="NA")
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var menuItmMstr = uow.MenuItemMstrRepo.Find(x => x.IsVisible == true && x.PositionType == positionType && x.MenuArea == area, np => np.MenuUrlMasters);

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuControlVM>()
                    .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
                    .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
                    .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
                    .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType))
                    .ForMember(dest => dest.UrlArea, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlArea))
                    .ForMember(dest => dest.MenuLevel, opts => opts.MapFrom(src => src.MenuUrlMasters.MenuLevel));
                });
                IMapper mapper = config.CreateMapper();
                var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstr).ToList();
                uow.Commit();
                return SiteMenu;
            }
        }

        //public List<MenuControlVM> GetMenuItems(PositionType positionType=PositionType.Top)
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var menuItmMstr = uow.MenuItemMstrRepo.GetAll().Where(x => x.IsVisible == true && x.PositionType == positionType);

        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<MenuItemMaster, MenuControlVM>()
        //            .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
        //            .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
        //            .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
        //            .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType))
        //            .ForMember(dest => dest.UrlArea, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlArea))
        //            .ForMember(dest => dest.MenuLevel, opts => opts.MapFrom(src => src.MenuUrlMasters.MenuLevel));
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstr).ToList();
        //        uow.Commit();
        //        return SiteMenu;
        //    }
        //}
        
        public List<MenuControlVM> GetMenuItems(IEnumerable<MenuItemMaster> menuItmMstrs)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<MenuItemMaster, MenuControlVM>()
                    .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
                    .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
                    .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
                    .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType))
                    .ForMember(dest => dest.UrlArea, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlArea))
                    .ForMember(dest => dest.MenuLevel, opts => opts.MapFrom(src => src.MenuUrlMasters.MenuLevel));
                });
                IMapper mapper = config.CreateMapper();
                var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstrs).ToList();
                uow.Commit();
                return SiteMenu;
            }
        }
        #region Old Code
        //public List<MenuControlVM> GetMenuItems2()
        //{
        //    using (var uow = new UnitOfWork(new NDCWebContext()))
        //    {
        //        var menuItmMstr = uow.MenuItemMstrRepo.GetAll();
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<MenuItemMaster, MenuControlVM>()
        //            .ForMember(dest => dest.UrlPrefix, opts => opts.MapFrom(src => src.MenuUrlMasters.UrlPrefix))
        //            .ForMember(dest => dest.Controller, opts => opts.MapFrom(src => src.MenuUrlMasters.Controller))
        //            .ForMember(dest => dest.Action, opts => opts.MapFrom(src => src.MenuUrlMasters.Action))
        //            .ForMember(dest => dest.PageType, opts => opts.MapFrom(src => src.MenuUrlMasters.PageType));
        //        });
        //        IMapper mapper = config.CreateMapper();
        //        var SiteMenu = mapper.Map<IEnumerable<MenuItemMaster>, IEnumerable<MenuControlVM>>(menuItmMstr).ToList();
        //        uow.Commit();
        //        return SiteMenu;
        //    }
        //}
        #endregion
    }
}