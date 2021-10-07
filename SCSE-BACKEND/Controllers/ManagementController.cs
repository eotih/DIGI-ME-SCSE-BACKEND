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
                    IDState = Posts1.IDState,
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
                    obj.CreatedByDate = DateTime.Now;
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
        public object xoaBaiViet(int idPosts)
        {
            var obj = db.Posts.Where(x => x.IDPost == idPosts).ToList().FirstOrDefault();
            db.Posts.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        [System.Web.Http.Route("GetByNameCategory")]
        [System.Web.Http.HttpGet]
        public object GetByNameCategory(string NameCate)
        {
            var category = db.Categories.Where(x => x.CategoryName == NameCate).ToList();
            return category;
        }
        [System.Web.Http.Route("GetByStatePost")]
        [System.Web.Http.HttpGet]
        public object GetByStatePost(int IdState)
        {
            var category = db.Posts.Where(x => x.IDState == IdState).ToList();
            return category;
        }
        // getbyID bài viết
        [System.Web.Http.Route("GetByIdBaiViet")]
        [System.Web.Http.HttpGet]
        public object getbyidBaiViet(int idPosts)
        {
            var obj = db.Posts.Where(x => x.IDPost == idPosts).ToList().FirstOrDefault();
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
        public object GetByStateVolunteers(int id)
        {
            var obj = db.Volunteers.Where(x => x.ID == id).ToList();
            return obj;
        }

    }
}