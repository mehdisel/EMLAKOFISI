using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.BLL.Abstract
{
    public interface INumberOfRoomService
    {
        IDataResult<IEnumerable<SelectListItem>> GetAllSelectList();
        IDataResult<NumberOfRoom> GetNumberOfRoomNameById(int Id);
    }
}
