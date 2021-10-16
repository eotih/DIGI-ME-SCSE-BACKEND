using SCSE_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SCSE_BACKEND.Controllers
{
    [System.Web.Http.RoutePrefix("Management")]
    public class ManagementController : ApiController
    {
        SCSE_DBEntities db = new SCSE_DBEntities();
        public static string ReplaceSpecialChars(string str)
        {
            string[] chars = new string[] { " ", ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "-");
                }
            }
            return str;
        }
        [System.Web.Http.Route("ThemBaiViet")]
        [System.Web.Http.HttpPost]
        public object ThemBaiViet(Posts1 Posts1)
        {
            if (Posts1.IDPost == 0)
            {
                Post Posts = new Post
                {
                    IDCat = Posts1.IDCat,
                    Title = Posts1.Title,
                    Slug = ReplaceSpecialChars(Posts1.Title),
                    Details = Posts1.Details,
                    Image = Posts1.Image,
                    IDState = 1,
                    Author = Posts1.Author,
                    CreatedByDate = DateTime.Now
                };
                db.Posts.Add(Posts);
                db.SaveChanges();
                return new Response
                {
                    Status = "Success",
                    Message = "Data Success"
                };
            }
            else
            {
                var obj = db.Posts.Where(x => x.IDPost == Posts1.IDPost).ToList().FirstOrDefault();
                if (obj.IDPost > 0)
                {
                    obj.IDCat = Posts1.IDCat;
                    obj.Title = Posts1.Title;
                    obj.Slug = ReplaceSpecialChars(Posts1.Title);
                    obj.Details = Posts1.Details;
                    obj.Image = Posts1.Image;
                    obj.IDState = Posts1.IDState;
                    obj.Author = Posts1.Author;
                    obj.UpdatedByDate = DateTime.Now;
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Updated",
                        Message = "Updated Successfully"
                    };
                }
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }
        [System.Web.Http.Route("SuaTrangThaiBaiViet")]
        [System.Web.Http.HttpPost]
        public object SuaTrangThaiBaiViet(Posts1 Posts1)
        {
            var obj = db.Posts.Where(x => x.IDPost == Posts1.IDPost).FirstOrDefault();
            if (obj.IDPost > 0)
            {
                obj.IDState = Posts1.IDState;
                obj.UpdatedByDate = DateTime.Now;
                db.SaveChanges();
                return new Response
                {
                    Status = "Updated",
                    Message = "Updated Successfully"
                };
            }
            return new Response
            {
                Status = "Fail",
                Message = "Updated Fail"
            };
        }
        // Xem danh sách tài khoản
        [System.Web.Http.Route("XemDanhSachBaiViet")]
        [System.Web.Http.HttpGet]
        public object xemDanhSachBaiViet()
        {
            var a = (from Posts in db.Posts
                     from cat in db.Categories
                     where cat.IDCat == Posts.IDCat

                     select new
                     {
                         Posts.IDPost,
                         Posts.IDCat,
                         Posts.Title,
                         Posts.Slug,
                         Posts.Details,
                         Posts.Image,
                         Posts.CreatedByDate,
                         Posts.Author,
                         Posts.IDState,
                         cat.CategoryName
                     }).ToList();
            return a;
        }
        // Xóa bài viết
        [System.Web.Http.Route("XoaBaiViet")]
        [System.Web.Http.HttpDelete]
        public object XoaBaiViet(int ID)
        {
            var obj = db.Posts.Where(x => x.IDPost == ID).FirstOrDefault();
            db.Posts.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        
        
        [System.Web.Http.Route("GetByStatePost")]
        [System.Web.Http.HttpGet]
        public object GetByStatePost(int IdState)
        {
            var category = db.Posts.Where(x => x.IDState == IdState).ToList();
            return category;
        }
        // getbyID bài viết
        [System.Web.Http.Route("GetBySlugBaiViet")]
        [System.Web.Http.HttpGet]
        public object GetBySlugBaiViet(string slug)
        {
            var obj = db.Posts.Where(x => x.Slug == slug).FirstOrDefault();
            return obj;
        }
        // getbyID the loai
        [System.Web.Http.Route("GetByIdCategory")]
        [System.Web.Http.HttpGet]
        public object getbyidCategory(int idcat)
        {
            var obj = db.Posts.Where(x => x.IDCat == idcat).ToList();
            return obj;
        }
        [System.Web.Http.Route("ThemBaiVietEN")]
        [System.Web.Http.HttpPost]
        public object ThemBaiVietEN(PostsEN1 PostsEN1)
        {
            try
            {
                PostsEN PostsEN = new PostsEN
                {
                    IDPostEN = PostsEN1.IDPostEN,
                    IDCat = PostsEN1.IDCat,
                    Title = PostsEN1.Title,
                    SlugEN = ReplaceSpecialChars(PostsEN1.Title),
                    Details = PostsEN1.Details,
                    Image = PostsEN1.Image,
                    IDState = 1,
                    Author = PostsEN1.Author,
                    CreatedByDate = DateTime.Now,

                };
                db.PostsENs.Add(PostsEN);
                db.SaveChanges();
                return new Response
                {
                    Status = "Success",
                    Message = "Data Success"
                };
            }
            catch
            {
                return new Response
                {
                    Status = "Fail",
                    Message = "Data Fail"
                };
            }
        }
        [System.Web.Http.Route("SuaBaiVietEN")]
        [System.Web.Http.HttpPost]
        public object SuaBaiVietEN(PostsEN1 PostsEN1)
        {
            var obj = db.PostsENs.Where(x => x.IDPostEN == PostsEN1.IDPostEN).FirstOrDefault();
            if (obj.IDPostEN > 0)
            {
                obj.IDCat = PostsEN1.IDCat;
                obj.Title = PostsEN1.Title;
                obj.SlugEN = ReplaceSpecialChars(PostsEN1.Title);
                obj.Details = PostsEN1.Details;
                obj.Image = PostsEN1.Image;
                obj.IDState = PostsEN1.IDState;
                obj.Author = PostsEN1.Author;
                obj.UpdatedByDate = DateTime.Now;
                db.SaveChanges();
                return new Response
                {
                    Status = "Updated",
                    Message = "Updated Successfully"
                };
            }
            return new Response
            {
                Status = "Fail",
                Message = "Updated Fail"
            };
        }
        // Xem danh sách tài khoản
        [System.Web.Http.Route("XemDanhSachBaiVietEN")]
        [System.Web.Http.HttpGet]
        public object xemDanhSachBaiVietEN()
        {
            var a = (from PostsEN in db.PostsENs
                     from cat in db.Categories
                     where cat.IDCat == PostsEN.IDCat

                     select new
                     {
                         PostsEN.IDPostEN,
                         PostsEN.IDCat,
                         PostsEN.Title,
                         PostsEN.SlugEN,
                         PostsEN.Details,
                         PostsEN.Image,
                         PostsEN.CreatedByDate,
                         PostsEN.Author,
                         PostsEN.IDState,
                         cat.CategoryName
                     }).ToList();
            return a;
        }
        [System.Web.Http.Route("SuaTrangThaiBaiVietEN")]
        [System.Web.Http.HttpPost]
        public object SuaTrangThaiBaiVietEN(PostsEN1 PostsEN1)
        {
            var obj = db.PostsENs.Where(x => x.IDPostEN == PostsEN1.IDPostEN).FirstOrDefault();
            if (obj.IDPostEN > 0)
            {
                obj.IDState = PostsEN1.IDState;
                obj.UpdatedByDate = DateTime.Now;
                db.SaveChanges();
                return new Response
                {
                    Status = "Updated",
                    Message = "Updated Successfully"
                };
            }
            return new Response
            {
                Status = "Fail",
                Message = "Updated Fail"
            };
        }
        // Xóa bài viết
        [System.Web.Http.Route("XoaBaiVietEN")]
        [System.Web.Http.HttpDelete]
        public object XoaBaiVietEN(int ID)
        {
            var obj = db.PostsENs.Where(x => x.IDPostEN == ID).FirstOrDefault();
            db.PostsENs.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }


        [System.Web.Http.Route("GetByStatePostEN")]
        [System.Web.Http.HttpGet]
        public object GetByStatePostEN(int IdStateen)
        {
            var category = db.PostsENs.Where(x => x.IDState == IdStateen).ToList();
            return category;
        }
        // getbyID bài viết
        [System.Web.Http.Route("GetBySlugBaiVietEN")]
        [System.Web.Http.HttpGet]
        public object GetBySlugBaiVietEN(string slugen)
        {
            var obj = db.PostsENs.Where(x => x.SlugEN == slugen).FirstOrDefault();
            return obj;
        }
        // getbyID the loai
        [System.Web.Http.Route("GetByIdCategoryEN")]
        [System.Web.Http.HttpGet]
        public object getbyidCategoryEN(int idcat)
        {
            var obj = db.PostsENs.Where(x => x.IDCat == idcat).ToList();
            return obj;
        }
        
        [System.Web.Http.Route("XemChiTietBaiViet")]
        [System.Web.Http.HttpGet]   
        public object XemChiTietBaiVietEN()
        {
            var a = (from PostsEN in db.PostsENs
                     from Posts in db.Posts
                     from cat in db.Categories
                     where Posts.IDPost == PostsEN.IDPostEN
                     select new
                     {
                         PostsEN.SlugEN,
                         Posts.Slug
                     }
                     ).FirstOrDefault();
            return a;
        }
        [System.Web.Http.Route("DSBaiVietTiengAnhCanDang")]
        [System.Web.Http.HttpGet]
        public object DSBaiVietTiengAnhCanDang()
        {
            var a = (from PostsEN in db.PostsENs
                     from Posts in db.Posts
                     where Posts.IDPost != PostsEN.IDPostEN
                     select new
                     {
                         Posts.IDPost,
                         Posts.Title,
                         Posts.Slug,
                         Posts.IDCat,
                         Posts.Details,
                         Posts.Image,
                         Posts.Author
                     }
                     ).ToList();
            return a;
        }
        //---Đăng kí internship or Volunteers
        [System.Web.Http.Route("DangKiThamGia")]
        [System.Web.Http.HttpPost]
        public object DangKiThamGia(Volunteer1 vol1)
        {
            if (vol1.ID == 0)
            {
                Volunteer vol = new Volunteer
                {
                    ID = vol1.ID,
                    FirstName = vol1.FirstName,
                    LastName = vol1.LastName,
                    DOB = vol1.DOB,
                    Phone = vol1.Phone,
                    Email = vol1.Email,
                    Address = vol1.Address,
                    Purpose = vol1.Purpose,
                    Project = vol1.Project,
                    IDState = 1 // Pending 
                };
                db.Volunteers.Add(vol);
                db.SaveChanges();
                return new Response
                {
                    Status = "Success",
                    Message = "Data Success"
                };
            }
            else
            {
                var obj = db.Volunteers.Where(x => x.ID == vol1.ID).ToList().FirstOrDefault();
                if (obj.ID > 0)
                {
                    obj.IDState = vol1.IDState;
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Updated",
                        Message = "Updated Successfully"
                    };
                }
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }
        [System.Web.Http.Route("EditState")]
        [System.Web.Http.HttpPost]
        public object EditState(Volunteer1 vol1)
        {
            var obj = db.Volunteers.Where(x => x.ID == vol1.ID).ToList().FirstOrDefault();
            if (obj.ID > 0)
            {
                obj.IDState = vol1.IDState;
                db.SaveChanges();
                return new Response
                {
                    Status = "Updated",
                    Message = "Updated Successfully"
                };
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }
        // Xem danh sách bài viết
        [System.Web.Http.Route("XemDanhSachDangKy")]
        [System.Web.Http.HttpGet]
        public object XemDanhSachDangKy()
        {
            var result = db.Volunteers.ToList();
            return result;
        }
        // Xóa bài viết
        [System.Web.Http.Route("XoaNguoiDangKy")]
        [System.Web.Http.HttpDelete]
        public object XoaNguoiDangKy(int id)
        {
            var obj = db.Volunteers.Where(x => x.ID == id).ToList().FirstOrDefault();
            db.Volunteers.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        // getbyID bài viết
        [System.Web.Http.Route("GetByIdNguoiDangKy")]
        [System.Web.Http.HttpGet]
        public object GetbyidNguoiDangKy(int id)
        {
            var obj = db.Volunteers.Where(x => x.ID == id).ToList().FirstOrDefault();
            return obj;
        }
        // Get State 
        [System.Web.Http.Route("GetByStateVolunteers")]
        [System.Web.Http.HttpGet]
        public object GetByStateVolunteers(int IDState)
        {
            var obj = db.Volunteers.Where(x => x.IDState == IDState).ToList();
            return obj;
        }

    }
}