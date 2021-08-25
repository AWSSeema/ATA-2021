using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.User.Controllers
{
    public class GalleryController : Controller
    {
        BLL.VideoCategories _VideoCategories = new BLL.VideoCategories();
        BLL.Videos _Videos = new BLL.Videos();
        BLL.Photos _Photos = new BLL.Photos();        
        
        public ActionResult Photos(string Year = "")
        {
            int status = 0;

            List<Entities.PhotoCategories> lstPhotoCategories = new List<Entities.PhotoCategories>();
            List<Entities.Photos> lstPhotos = new List<Entities.Photos>();
            try
            {
                lstPhotos = _Photos.FETOP4GetPhotoList(ref lstPhotoCategories, Year, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstPhotoCategories = lstPhotoCategories;
            ViewBag.lstPhotos = lstPhotos;
            ViewBag.Year = Year;
            return View();
        }

        public ActionResult PhotosList(string CategoryName)
        {
            int status = 0;
            List<Entities.PhotoCategories> lstPhotoCategories = new List<Entities.PhotoCategories>();
            List<Entities.Photos> lstPhotos = new List<Entities.Photos>();
            try
            {
                lstPhotos = _Photos.FEGetPhotosListById(CategoryName, ref lstPhotoCategories, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstPhotoCategories = lstPhotoCategories;
            ViewBag.CategoryName = CategoryName;
            ViewBag.lstPhotos = lstPhotos;
            return View();
        }

        public ActionResult Videos()
        {
            List<Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();
            int status = 0;
            try
            {
                lstVideos = _Videos.FEGetVideoList(ref lstVideoCategories, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstVideos=lstVideos;
            ViewBag.lstVideoCategories = lstVideoCategories;
            
            return View();
        }

        public ActionResult MediaCoverage()
        {
            List<Entities.VideoCategories> lstVideoCategories = new List<Entities.VideoCategories>();
            List<Entities.Videos> lstVideos = new List<Entities.Videos>();
            int status = 0;
            try
            {
                lstVideos = _Videos.FEGetVideoList(ref lstVideoCategories, ref status);
            }
            catch
            {
                return RedirectToAction("Error404", "Error");
            }
            ViewBag.lstVideos = lstVideos;
            ViewBag.lstVideoCategories = lstVideoCategories;

            return View();
        }
    }
}
