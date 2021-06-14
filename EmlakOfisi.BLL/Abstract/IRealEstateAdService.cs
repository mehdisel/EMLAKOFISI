using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.Entities.Concrete;
using EmlakOfisi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmlakOfisi.BLL.Abstract
{
    public interface IRealEstateAdService
    {
        IDataResult<List<RealEstateAdViewModel>> GetRealEstateAdsByUserId(int userId);

        IResult AddRealEstateAd(AddRealEstateAdViewModel addRealEstateAdViewModel);
        IResult EditRealEstateAd(EditRealEstateAdViewModel editRealEstateAdViewModel);

        IDataResult<RealEstateAdFilterViewModel> GetRealEstateList(int pageCounter, RealEstateAdFilterInModel realEstateAdFilterInModel=null);
        IDataResult<RealEstateAdFilterDefaults> GetRealEstateAdsFilterDefaults();
        IDataResult<EditRealEstateAdViewModel> GetRealEstateAdById(int Id);
    }
}
