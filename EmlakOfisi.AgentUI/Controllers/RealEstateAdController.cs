using EmlakOfisi.BLL.Abstract;
using EmlakOfisi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmlakOfisi.AgentUI.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class RealEstateAdController : Controller
    {
        private readonly INumberOfRoomService _numberOfRoomService;
        private readonly IRealEstateAdService _realEstateAdService;
        public RealEstateAdController(INumberOfRoomService numberOfRoomService, IRealEstateAdService realEstateAdService)
        {
            _numberOfRoomService = numberOfRoomService;
            _realEstateAdService = realEstateAdService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {

            return View();
        }
        [AllowAnonymous]
        public IActionResult GetEstateAdList()
        {
            var filtersDefaultValues = _realEstateAdService.GetRealEstateAdsFilterDefaults();
            var result=_realEstateAdService.GetRealEstateList(0);
            result.Data.Filters = filtersDefaultValues.Data;
            return View(result.Data);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetEstateAdList(RealEstateAdFilterInModel realEstateAdFilterInModel)
        {
            var filtersDefaultValues = _realEstateAdService.GetRealEstateAdsFilterDefaults();
            var result = _realEstateAdService.GetRealEstateList(realEstateAdFilterInModel.PageCounter, realEstateAdFilterInModel);
            
            return Json(result.Data);

        }

        public IActionResult RealEstateAdListByUser()
        {
            var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (user != null)
            {
                var userId = int.Parse(user.Value);
                var result= _realEstateAdService.GetRealEstateAdsByUserId(userId);
                if (result.Success)
                {
                    return View(result.Data);
                }
            }
            return View();
        }

        public IActionResult EditEstateAd(int Id)
        {
            var result = _realEstateAdService.GetRealEstateAdById(Id);
            return View(EditEstateAdReturnModel(Id, result.Data));
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult EditEstateAd(EditRealEstateAdViewModel editRealEstateAdViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (user == null)
                {
                    return View("EditEstateAd", EditEstateAdReturnModel(editRealEstateAdViewModel.Id, editRealEstateAdViewModel));
                }
                editRealEstateAdViewModel.UserId = int.Parse(user.Value);
                var result = _realEstateAdService.EditRealEstateAd(editRealEstateAdViewModel);
                if (result.Success)
                {
                    return RedirectToAction("RealEstateAdListByUser");
                }
                return View("EditEstateAd", EditEstateAdReturnModel(editRealEstateAdViewModel.Id, editRealEstateAdViewModel));
            }
            return View("EditEstateAd", EditEstateAdReturnModel(editRealEstateAdViewModel.Id, editRealEstateAdViewModel));
        }
        public IActionResult AddEstateAd()
        {
            return View(AddEstateAdReturnModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEstateAd([FromForm]AddRealEstateAdViewModel addRealEstateAdViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (user==null)
                {
                    return View("AddEstateAd", AddEstateAdReturnModel(addRealEstateAdViewModel));
                }
                addRealEstateAdViewModel.UserId = int.Parse(user.Value);
                var result= _realEstateAdService.AddRealEstateAd(addRealEstateAdViewModel);
                if (result.Success)
                {
                    return RedirectToAction("RealEstateAdListByUser");
                }
                return View("AddEstateAd", AddEstateAdReturnModel(addRealEstateAdViewModel));
            }
            return View("AddEstateAd", AddEstateAdReturnModel(addRealEstateAdViewModel));
        }

        private AddRealEstateAdViewModel AddEstateAdReturnModel(AddRealEstateAdViewModel addRealEstateAdViewModel=null)
        {
            if (addRealEstateAdViewModel!=null)
            {
                addRealEstateAdViewModel.NumberOfRoomsItems = _numberOfRoomService.GetAllSelectList().Data;
                return addRealEstateAdViewModel;
            }
            var model = new AddRealEstateAdViewModel()
            {
                NumberOfRoomsItems = _numberOfRoomService.GetAllSelectList().Data
            };
            return model;
        }
        private EditRealEstateAdViewModel EditEstateAdReturnModel(int realEstateAdId=0,EditRealEstateAdViewModel editRealEstateAdViewModel = null)
        {
            if (editRealEstateAdViewModel != null)
            {
                editRealEstateAdViewModel.NumberOfRoomsItems = _numberOfRoomService.GetAllSelectList().Data;
                return editRealEstateAdViewModel;
            }
            var model = new EditRealEstateAdViewModel()
            {
                Id= realEstateAdId,
                NumberOfRoomsItems = _numberOfRoomService.GetAllSelectList().Data
            };
            return model;
        }
    }
}
