using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.DAL.Abstract;
using EmlakOfisi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmlakOfisi.BLL.Concrete
{
    public class NumberOfRoomManager : INumberOfRoomService
    {
        private readonly INumberOfRoomDal _numberOfRoomDal;
        public NumberOfRoomManager(INumberOfRoomDal numberOfRoomDal)
        {
            _numberOfRoomDal = numberOfRoomDal;
        }
        public IDataResult<IEnumerable<SelectListItem>> GetAllSelectList()
        {
            var numberOfRoomList = _numberOfRoomDal.GetList();
            var model = numberOfRoomList.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() });
            return new SuccessDataResult<IEnumerable<SelectListItem>>(model);
        }

        public IDataResult<NumberOfRoom> GetNumberOfRoomNameById(int Id)
        {
            var result = _numberOfRoomDal.Get(x => x.Id == Id);
            return new SuccessDataResult<NumberOfRoom>(result);
           
        }
    }
}
