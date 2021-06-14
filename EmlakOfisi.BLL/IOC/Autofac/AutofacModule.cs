using Autofac;
using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.BLL.Concrete;
using EmlakOfisi.DAL.Abstract;
using EmlakOfisi.DAL.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.BLL.IOC.Autofac
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

           
            builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

            builder.RegisterType<EfCompanyUserDal>().As<ICompanyUserDal>();

            builder.RegisterType<EfRoleDal>().As<IRoleDal>();

            builder.RegisterType<RoleManager>().As<IRoleService>();

            builder.RegisterType<NumberOfRoomManager>().As<INumberOfRoomService>();
            builder.RegisterType<EfNumberOfRoomDal>().As<INumberOfRoomDal>();

            builder.RegisterType<RealEstateAdManager>().As<IRealEstateAdService>();
            builder.RegisterType<EfRealEstateAdDal>().As<IRealEstateAdDal>();

        }
    }
}
