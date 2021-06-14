using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Core.Utilities.Results;
using EmlakOfisi.DAL.Abstract;
using EmlakOfisi.Entities.Concrete;
using EmlakOfisi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EmlakOfisi.BLL.Concrete
{
    public class RealEstateAdManager : IRealEstateAdService
    {
        private readonly IRealEstateAdDal _realEstateAdDal;
        private readonly INumberOfRoomService _numberOfRoomService;

        public RealEstateAdManager(IRealEstateAdDal realEstateAdDal, INumberOfRoomService numberOfRoomService)
        {
            _realEstateAdDal = realEstateAdDal;
            _numberOfRoomService = numberOfRoomService;
        }

        public IResult AddRealEstateAd(AddRealEstateAdViewModel addRealEstateAdViewModel)
        {
            if (addRealEstateAdViewModel.Image != null)
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(addRealEstateAdViewModel.Image.FileName);
                var imageName = Guid.NewGuid() + extension;
                var savedPath = currentDirectory + "\\wwwroot\\img\\" + imageName;
                using (var stream = new FileStream(savedPath, FileMode.Create))
                {
                    addRealEstateAdViewModel.Image.CopyTo(stream);
                };
                addRealEstateAdViewModel.ImagePath = imageName;
            }

            RealEstateAd newRealEstateAd = new RealEstateAd()
            {
                UserId = addRealEstateAdViewModel.UserId,
                AdName = addRealEstateAdViewModel.AdName,
                SquareMeter = addRealEstateAdViewModel.SquareMeter,
                NumberOfRoomsId = addRealEstateAdViewModel.NumberOfRoomsId,
                YearBuilt = addRealEstateAdViewModel.YearBuilt,
                Price = addRealEstateAdViewModel.Price,
                ImagePath = addRealEstateAdViewModel.ImagePath,
                CreatedDate = DateTime.Now

            };
            _realEstateAdDal.Add(newRealEstateAd);
            return new SuccessResult();
        }
        public IResult EditRealEstateAd(EditRealEstateAdViewModel editRealEstateAdViewModel)
        {

            if (editRealEstateAdViewModel.Image != null)
            {

                var currentDirectory = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(editRealEstateAdViewModel.Image.FileName);
                var imageName = Guid.NewGuid() + extension;
                var savedPath = currentDirectory + "\\wwwroot\\img\\" + imageName;
                using (var stream = new FileStream(savedPath, FileMode.Create))
                {
                    editRealEstateAdViewModel.Image.CopyTo(stream);
                };
                editRealEstateAdViewModel.ImagePath = imageName;
            }
            RealEstateAd realEstateAd = _realEstateAdDal.Get(x => x.Id == editRealEstateAdViewModel.Id);
            RealEstateAd updatedRealEstateAd = new RealEstateAd()
            {
                Id = editRealEstateAdViewModel.Id,
                UserId = editRealEstateAdViewModel.UserId,
                AdName = editRealEstateAdViewModel.AdName,
                SquareMeter = editRealEstateAdViewModel.SquareMeter,
                NumberOfRoomsId = editRealEstateAdViewModel.NumberOfRoomsId,
                YearBuilt = editRealEstateAdViewModel.YearBuilt,
                Price = editRealEstateAdViewModel.Price,
                ImagePath = editRealEstateAdViewModel.ImagePath,
                ModifiedDate = DateTime.Now,
                CreatedDate = realEstateAd.CreatedDate
            };
            _realEstateAdDal.Update(updatedRealEstateAd);
            return new SuccessResult();
        }

        public IDataResult<EditRealEstateAdViewModel> GetRealEstateAdById(int Id)
        {
            var result = _realEstateAdDal.Get(x => x.Id == Id);
            EditRealEstateAdViewModel realEstateAdViewModel = new EditRealEstateAdViewModel()
            {
                Id = result.Id,
                UserId = result.UserId,
                AdName = result.AdName,
                SquareMeter = result.SquareMeter,
                NumberOfRoomsId = result.NumberOfRoomsId,
                YearBuilt = result.YearBuilt,
                Price = result.Price,
                ImagePath = result.ImagePath,

            };


            return new SuccessDataResult<EditRealEstateAdViewModel>(realEstateAdViewModel);
        }

        public IDataResult<List<RealEstateAdViewModel>> GetRealEstateAdsByUserId(int userId)
        {
            var realEstateAdList = _realEstateAdDal.GetList(x => x.UserId == userId).ToList();
            List<RealEstateAdViewModel> realEstateAds = new List<RealEstateAdViewModel>();
            foreach (var item in realEstateAdList)
            {
                RealEstateAdViewModel realEstateAdViewModel = new RealEstateAdViewModel()
                {
                    Id = item.Id,
                    AdName = item.AdName,
                    SquareMeter = item.SquareMeter,
                    ImagePath = item.ImagePath,
                    CreatedDate = item.CreatedDate,
                    NumberOfRoom = _numberOfRoomService.GetNumberOfRoomNameById(item.NumberOfRoomsId).Data.Name,
                    Price = item.Price,
                    YearBuilt = item.YearBuilt
                };
                realEstateAds.Add(realEstateAdViewModel);
            }
            return new SuccessDataResult<List<RealEstateAdViewModel>>(realEstateAds);
        }

        public IDataResult<RealEstateAdFilterDefaults> GetRealEstateAdsFilterDefaults()
        {
            var Items = _realEstateAdDal.GetList();
            var existingNumberOfRoomsIds = Items.GroupBy(x => x.NumberOfRoomsId).Select(x => x.Key);
            var existingNumberOfRooms = new List<RealEstateAdNumberOfRoom>();
            foreach (var item in existingNumberOfRoomsIds)
            {
                var numberOfRoomsData = _numberOfRoomService.GetNumberOfRoomNameById(item);
                RealEstateAdNumberOfRoom realEstateAdNumberOfRoom = new RealEstateAdNumberOfRoom()
                {
                    Id = numberOfRoomsData.Data.Id,
                    Name = numberOfRoomsData.Data.Name
                };
                existingNumberOfRooms.Add(realEstateAdNumberOfRoom);
            }
            var model = new RealEstateAdFilterDefaults()
            {
                MinPrice = (int)Items.Min(x => x.Price),
                MaxPrice = (int)Items.Max(x => x.Price),
                MinSquareMeter = Items.Min(x => x.SquareMeter),
                MaxSquareMeter = Items.Max(x => x.SquareMeter),
                MinYearBuilt = Items.Min(x => x.YearBuilt),
                MaxYearBuilt = Items.Max(x => x.YearBuilt),
                SinceDate = Items.Min(x => x.CreatedDate),
                ExistingNumberOfRooms = existingNumberOfRooms

            };

            return new SuccessDataResult<RealEstateAdFilterDefaults>(model);
        }

        public IDataResult<RealEstateAdFilterViewModel> GetRealEstateList(int pageCounter, RealEstateAdFilterInModel realEstateAdFilterInModel = null)
        {
            IEnumerable<RealEstateAd> Items;
            if (realEstateAdFilterInModel != null)
            {
                Items = _realEstateAdDal.GetList(x =>
                 (x.YearBuilt >= realEstateAdFilterInModel.MinYearBuilt && x.YearBuilt <= realEstateAdFilterInModel.MaxYearBuilt) &&
                 (x.Price >= realEstateAdFilterInModel.MinPrice && x.Price <= realEstateAdFilterInModel.MaxPrice) &&
                 (x.SquareMeter >= realEstateAdFilterInModel.MinSquareMeter && x.SquareMeter <= realEstateAdFilterInModel.MaxSquareMeter));
                if (realEstateAdFilterInModel.SelectedNumberOfRooms != 0)
                {
                    Items = Items.Where(x => x.NumberOfRoomsId == realEstateAdFilterInModel.SelectedNumberOfRooms);
                }

            }
            else
            {
                Items = _realEstateAdDal.GetList();
            }
            var result = Items.Skip(pageCounter * 10).Take(12).Select(x => new RealEstateAdViewModel()
            {
                AdName = x.AdName,
                Id = x.Id,
                ImagePath = x.ImagePath,
                NumberOfRoom = _numberOfRoomService.GetNumberOfRoomNameById(x.NumberOfRoomsId).Data.Name,
                Price = x.Price,
                PriceConverted = x.Price.ToString("C"),
                YearBuilt = x.YearBuilt,
                SquareMeter = x.SquareMeter,
                CreatedDate = x.CreatedDate,
                CreatedDateConverted = x.CreatedDate.ToShortDateString()
            }).ToList();

            var model = new RealEstateAdFilterViewModel()
            {
                Items = result
            };
            return new SuccessDataResult<RealEstateAdFilterViewModel>(model);
        }
    }
}
