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
        public object themBaiViet(Posts1 Posts1)
        {
            if (Posts1.IDPost == 0)
            {
                Post Posts = new Post();
                
                Posts.IDCat = Posts1.IDCat;
                Posts.Title = Posts1.Title;
                Posts.Slug = ReplaceSpecialChars(Posts1.Title);
                Posts.Details = Posts1.Details;
                Posts.Image = Posts1.Image;
                Posts.Status = Posts1.Status;
                Posts.Author = Posts1.Author;
                Posts.CreatedByDate = DateTime.Now;
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
                    obj.Status = Posts1.Status;
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
                         Posts.Status,
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
        // getbyID bài viết
        [System.Web.Http.Route("GetByIdBaiViet")]
        [System.Web.Http.HttpGet]
        public object getbyidBaiViet(int idPosts)
        {
            var obj = db.Posts.Where(x => x.IDPost == idPosts).ToList().FirstOrDefault();
            return obj;
        }
        //---Đăng kí internship or Volunteers
        [System.Web.Http.Route("DangKiThamGia")]
        [System.Web.Http.HttpPost]
        public object DangKiThamGia(Volunteer1 vol1)
        {
            if (vol1.ID == 0)
            {
                Volunteer vol = new Volunteer();
                vol.ID = vol1.ID;
                vol.FirstName = vol1.FirstName;
                vol.LastName = vol1.LastName;
                vol.DOB = vol1.DOB;
                vol.Phone = vol1.Phone;
                vol.Email = vol1.Email;
                vol.Address = vol1.Address;
                vol.Purpose = vol1.Purpose;
                vol.Project = vol1.Project;
                vol.Status = vol1.Status;
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
                    obj.Status = vol1.Status;
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
        public object xemDanhSachDangKy()
        {
            var a = db.Volunteers.ToList();
            return a;
        }
        // Xóa bài viết
        [System.Web.Http.Route("XoaNguoiDangKy")]
        [System.Web.Http.HttpDelete]
        public object xoaNguoiDangKy(int id)
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
        public object getbyidNguoiDangKy(int id)
        {
            var obj = db.Volunteers.Where(x => x.ID == id).ToList().FirstOrDefault();
            return obj;
        }

    }
}