using EmlakOfisi.Core.DAL.EntityFramework;
using EmlakOfisi.DAL.Abstract;
using EmlakOfisi.DAL.Concrete.EntityFramework.Contexts;
using EmlakOfisi.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.DAL.Concrete.EntityFramework
{
    public class EfNumberOfRoomDal:EfEntityRepositoryBase<NumberOfRoom,EmlakOfisiContext>,INumberOfRoomDal
    {
    }
}
