using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace TCAssociationTool.Areas.Admin.Controllers
{
     [Models.SessionClass.PermitAccess(Roles = "SuperAdmin,Photos")]
    public class PhotoGalleryController : Controller
    {
        TCAssociationTool.BLL.PhotoCategories _PhotoCategory = new TCAssociationTool.BLL.PhotoCategories();
        List<Entities.PhotoCategories> lstPhotoCategories = new List<Entities.PhotoCategories>();
        TCAssociationTool.BLL.Photos _Photo = new TCAssociationTool.BLL.Photos();
        List<Entities.Photos> lstPhotos = new List<Entities.Photos>();

        #region Photos Category

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult Index()
        {
            int status = 0;
            lstPhotoCategories = _PhotoCategory.GetPhotoCategoriesList(ref status);
            ViewBag.lstPhotoCategories = lstPhotoCategories;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult PhotosCategoryList(string Search = "", string Year = "", string SortColumn = "", string SortOrder = "", int PageNo = 1, int Items = 20)
        {
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            int Total = 0;

            try
            {
                lstPhotoCategories = _PhotoCategory.GetPhotoCategoriesListByVariable(Search, Year, Sort, PageNo, Items, ref Total);               
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.total = Total;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.lstPhotoCategories = lstPhotoCategories;
            ViewBag.sortcolumn = SortColumn;
            ViewBag.sortorder = SortOrder.ToLower();
            return View();
        }
        
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddPhotoCategory(Entities.PhotoCategories objPhotoCategory)
        {
            string str = "";
            bool _bool = true;
            try
            {
                objPhotoCategory.UpdatedBy = HttpContext.User.Identity.Name.ToString();

                Int64 _status = _PhotoCategory.InsertPhotoCategories(objPhotoCategory);
                switch (_status)
                {
                    case 1:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        _bool = true;
                        break;
                    case 2:
                        TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        _bool = true;
                        break;
                    case -1:
                        TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                    default:
                        TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                        _bool = false;
                        break;
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                _bool = false;
            }
            return RedirectToAction("Index", "PhotoGallery");
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult EditPhotoCategory(Int64 PhotoCategoryId = 0)
        {
            string str = "";
            try
            {
                int _qstatus = 0;
                Entities.PhotoCategories objPhotoCategory = _PhotoCategory.GetPhotoCategoriesById(PhotoCategoryId, ref _qstatus);

                if (objPhotoCategory != null)
                {
                    return Json(new { ok = true, data = objPhotoCategory });
                }
                else
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Failed Transaction</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }
        
        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public JsonResult DeletePhotoCategory(Int64 PhotoCategoryId)
        {
            string str = "";
            try
            {
                Int64 _status = _PhotoCategory.DeletePhotoCategories(PhotoCategoryId);
                if (_status == 1)
                {
                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"alert alert-danger alert-dismissable\">Failed deleting page</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        #endregion       

        #region Photos

        [HttpGet]
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult Photos(Int64 PhotoCategoryId = 0)
        {
            try{
            int Total = 0;
            ViewBag.PhotoCategoryId = PhotoCategoryId;
            lstPhotoCategories = _PhotoCategory.GetPhotoCategoriesList(ref Total);
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.catlist = lstPhotoCategories;
            return View();
        }

        [HttpGet]
        [Models.SessionClass.SessionExpireFilter]
        public ActionResult PhotosList(Int64 PhotoCategoryId = 0, string Search = "", string SortColumn = "UpdatedTime", string SortOrder = "Desc", int PageNo = 1, int Items = 20)
        {
            int Total = 0;
            string Sort = (SortColumn != "" ? SortColumn + " " + SortOrder : "");
            List<Entities.Photos> lstPhotos =new List<Entities.Photos>();
            try
            {
                lstPhotos = _Photo.GetPhotosListByVariable(PhotoCategoryId, Search, Sort, PageNo, Items, ref Total);
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }

            ViewBag.lstPhotosLst = lstPhotos;
            ViewBag.pageno = PageNo;
            ViewBag.items = Items;
            ViewBag.PhotoCategoryId = PhotoCategoryId;
            ViewBag.total = Total;
            return View();
        }

        [Areas.Admin.Models.SessionClass.SessionExpireFilter]
        public ActionResult AddPhoto(Int64 PhotoCategoryId = 0)
        {
            int _qstatus = 0;
            try
            {
                List<Entities.PhotoCategories> lstPhotoCategories = _PhotoCategory.GetPhotoCategoriesList(ref _qstatus);

                if (_qstatus == 1)
                {
                    ViewBag.lstPhotoCategories = lstPhotoCategories;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Index", "PhotoGallery");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            ViewBag.PhotoCategoryId = PhotoCategoryId;
            return View();
        }

        [Models.SessionClass.SessionExpireFilter]
        [HttpPost]
        public ActionResult AddPhoto(Entities.Photos objPhoto, List<HttpPostedFileBase> file1, Int64 PhotoId = 0)
        {
            try
            {
                foreach (var file in file1)
                {
                    if (file != null)
                    {
                        file.SaveAs(ConfigurationManager.AppSettings["uploadPath"] + "\\photogallery\\normalphoto\\" + file.FileName);
                        WebImage image = new WebImage(ConfigurationManager.AppSettings["uploadPath"] + "\\photogallery\\normalphoto\\" + file.FileName);
                        string imageurl = (image != null ? image.ImageFormat : "NA");

                        objPhoto.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                        Int64 status = _Photo.InsertPhotos(objPhoto, ref imageurl);

                        switch (status)
                        {
                            case 1:
                                if (image != null)
                                {
                                    image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\photogallery\\normalphoto\\" + imageurl);
                                    var image_thumb = image;
                                    image_thumb.Resize(130, 103, true, false);
                                    image_thumb.Crop(1, 1, 1, 1);
                                    image_thumb.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\photogallery\\thumbphoto\\" + imageurl);
                                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\photogallery\\normalphoto\\" + file.FileName);
                                }
                                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Records Successfully</div>";
                                break;
                            case 2:
                                if (image != null)
                                {
                                    image.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\photogallery\\normalphoto\\" + imageurl);
                                    var image_thumb = image;
                                    image_thumb.Resize(130, 103, true, false);
                                    image_thumb.Crop(1, 1, 1, 1);
                                    image_thumb.Save(ConfigurationManager.AppSettings["uploadPath"] + "\\photogallery\\thumbphoto\\" + imageurl);
                                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadpath"] + "\\photogallery\\normalphoto\\" + file.FileName);
                                }
                                TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                                break;
                            case -1:
                                TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                                break;
                            default:
                                TempData["message"] = "<div class=\"error-alert closable\">Failed processing your request.</div>";
                                break;
                        }
                    }
                    else if (PhotoId != 0)
                    {
                        string imageurl = "NA";
                        objPhoto.UpdatedBy = HttpContext.User.Identity.Name.ToString();
                        Int64 _status = _Photo.InsertPhotos(objPhoto, ref imageurl);
                        if (_status == 1)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Inserted Record Successfully</div>";
                        }
                        else if (_status == 2)
                        {
                            TempData["message"] = "<div class=\"alert alert-success alert-dismissable\">Changes has been Updated Successfully</div>";
                        }
                        else
                        {
                            TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed uploading image.</div>";
                        }
                    }
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
            }
            return RedirectToAction("Photos", "PhotoGallery", new { PhotoCategoryId = objPhoto.PhotoCategoryId });
        }
                
        [HttpPost]
        public JsonResult PhotoDelete(Int64 PhotoId, string ImageFileName)
        {
            string str = "";

            try
            {
                Int64 _status = _Photo.DeletePhotos(PhotoId);
                if (_status == 1)
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\photogallery\normalphoto\" + ImageFileName);
                    System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\photogallery\thumbphoto\" + ImageFileName);

                    str = "<div class=\"alert alert-success alert-dismissable\">Record Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"error-alert closable\">Failed deleting  image.</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [HttpPost]
        public JsonResult PhotoDeleteAll(string PhotoIds)
        {
            string str = "";
            string ImageUrl = "";
            try
            {
                Int64 _status = _Photo.DeleteAllPhotos(PhotoIds,ref ImageUrl);
                if (_status == 1)
                {
                    string[] Url=ImageUrl.Split(',');
                    foreach (var item in Url)
                    {
                        if (item != "")
                        {
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\photogallery\normalphoto\" + item);
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadPath"] + @"\photogallery\thumbphoto\" + item);
                        }
                    }

                    str = "<div class=\"alert alert-success alert-dismissable\">Records Deleted Successfully</div>";
                    return Json(new { ok = true, data = str });
                }
                else
                {
                    str = "<div class=\"error-alert closable\">Failed deleting photos.</div>";
                    return Json(new { ok = false, data = str });
                }
            }
            catch 
            {
                str = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return Json(new { ok = false, data = str });
            }
        }

        [Models.SessionClass.SessionExpireFilter]
        public ActionResult PhotoEdit(Int64 PhotoId)
        {
            try
            {
                int _qstatus = 0;
                Entities.Photos objPhotos = _Photo.GetPhotosById(PhotoId, ref _qstatus);
                int qstatus = 0;
                List<Entities.PhotoCategories> lstPhotoCategories = _PhotoCategory.GetPhotoCategoriesList(ref qstatus);

                if (_qstatus == 1 && qstatus==1)
                {
                    ViewBag.objPhotos = objPhotos;
                    ViewBag.lstPhotoCategories = lstPhotoCategories;
                    ViewBag.status = _qstatus;
                }
                else
                {
                    TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                    return RedirectToAction("Photos", "PhotoGallery");
                }
            }
            catch 
            {
                TempData["message"] = "<div class=\"alert alert-danger alert-dismissable\">Failed transaction.</div>";
                return RedirectToAction("Photos", "PhotoGallery");
            }
            return View();
        }        

        #endregion

    }
}
