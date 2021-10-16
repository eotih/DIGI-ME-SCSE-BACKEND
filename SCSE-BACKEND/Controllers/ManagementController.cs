using SCSE_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace SCSE_BACKEND.Controllers
{
    [RoutePrefix("API/Management")]
    public class ManagementController : ApiController
    {
        SCSE_DBEntities db = new SCSE_DBEntities();
        
        [Route("AddOrEditPost")]
        [HttpPost]
        public object AddOrEditPost(Posts1 Posts1)
        {
            if (Posts1.IDPost == 0)
            {
                Post Posts = new Post
                {
                    IDCat = Posts1.IDCat,
                    Title = Posts1.Title,
                    Slug = Utils.ReplaceSpecialChars(Posts1.Title),
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
                    obj.Slug = Utils.ReplaceSpecialChars(Posts1.Title);
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
        [Route("EditStatePost")]
        [HttpPost]
        public object EditStatePost(Posts1 Posts1)
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
        [Route("ShowAllPost")]
        [HttpGet]
        public object ShowAllPost()
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
        [Route("DeletePost")]
        [HttpDelete]
        public object DeletePost(int ID)
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
        
        
        [Route("GetByStatePost")]
        [HttpGet]
        public object GetByStatePost(int IdState)
        {
            var category = db.Posts.Where(x => x.IDState == IdState).ToList();
            return category;
        }
        // getbyID bài viết
        [Route("GetBySlugBaiViet")]
        [HttpGet]
        public object GetBySlugBaiViet(string slug)
        {
            var obj = db.Posts.Where(x => x.Slug == slug).FirstOrDefault();
            return obj;
        }
        // getbyID the loai
        [Route("GetByIdCategory")]
        [HttpGet]
        public object GetByIdCategory(int idcat)
        {
            var obj = db.Posts.Where(x => x.IDCat == idcat).ToList();
            return obj;
        }
        [Route("AddPostEN")]
        [HttpPost]
        public object AddPostEN(PostsEN1 PostsEN1)
        {
            try
            {
                PostsEN PostsEN = new PostsEN
                {
                    IDPostEN = PostsEN1.IDPostEN,
                    IDCat = PostsEN1.IDCat,
                    Title = PostsEN1.Title,
                    SlugEN = Utils.ReplaceSpecialChars(PostsEN1.Title),
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
        [Route("EditPostEN")]
        [HttpPost]
        public object EditPostEN(PostsEN1 PostsEN1)
        {
            var obj = db.PostsENs.Where(x => x.IDPostEN == PostsEN1.IDPostEN).FirstOrDefault();
            if (obj.IDPostEN > 0)
            {
                obj.IDCat = PostsEN1.IDCat;
                obj.Title = PostsEN1.Title;
                obj.SlugEN = Utils.ReplaceSpecialChars(PostsEN1.Title);
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
        [Route("ShowAllPostEN")]
        [HttpGet]
        public object ShowAllPostEN()
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
        [Route("EditStatePostEN")]
        [HttpPost]
        public object EditStatePostEN(PostsEN1 PostsEN1)
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
        [Route("DeletePostEN")]
        [HttpDelete]
        public object DeletePostEN(int ID)
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


        [Route("GetByStatePostEN")]
        [HttpGet]
        public object GetByStatePostEN(int IdStateen)
        {
            var category = db.PostsENs.Where(x => x.IDState == IdStateen).ToList();
            return category;
        }
        // getbyID bài viết
        [Route("GetBySlugBaiVietEN")]
        [HttpGet]
        public object GetBySlugBaiVietEN(string slugen)
        {
            var obj = db.PostsENs.Where(x => x.SlugEN == slugen).FirstOrDefault();
            return obj;
        }
        // getbyID the loai
        [Route("GetByIdCategoryEN")]
        [HttpGet]
        public object GetByIdCategoryEN(int idcat)
        {
            var obj = db.PostsENs.Where(x => x.IDCat == idcat).ToList();
            return obj;
        }
        
        [Route("GetMultiLanguesSlug")]
        [HttpGet]   
        public object GetMultiLanguesSlug()
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
        [Route("ShowAllPostENNeedPost")]
        [HttpGet]
        public object ShowAllPostENNeedPost()
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
        [Route("RegisterVolunteer")]
        [HttpPost]
        public object RegisterVolunteer(Volunteer1 vol1)
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
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }
        [Route("EditState")]
        [HttpPost]
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
        [Route("ShowAllVolunteers")]
        [HttpGet]
        public object XemDanhSachDangKy()
        {
            var result = db.Volunteers.ToList();
            return result;
        }
        // Xóa bài viết
        [Route("DeleteVolunteer")]
        [HttpDelete]
        public object DeleteVolunteer(int id)
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
        [Route("GetByIdVolunteer")]
        [HttpGet]
        public object GetByIdVolunteer(int id)
        {
            var obj = db.Volunteers.Where(x => x.ID == id).ToList().FirstOrDefault();
            return obj;
        }
        // Get State 
        [Route("GetByStateVolunteers")]
        [HttpGet]
        public object GetByStateVolunteers(int IDState)
        {
            var obj = db.Volunteers.Where(x => x.IDState == IDState).ToList();
            return obj;
        }
        [Route("Notification")]
        [HttpPost]
        public object addoreditNotification(Notification1 no1)
        {
            if (no1.ID == 0)
            {
                Notification no = new Notification
                {
                    ID = no1.ID,
                    Title = no1.Title,
                    Image = no1.Image,
                    Decription = no1.Decription,
                    Status = no1.Status,
                    Url = no1.Url,
                    CreatedByDate = DateTime.Now
                };
                db.Notifications.Add(no);
                db.SaveChanges();
                return new Response
                {
                    Status = "Success",
                    Message = "Data Success"
                };
            }
            else
            {
                var obj = db.Notifications.Where(x => x.ID == no1.ID).ToList().FirstOrDefault();
                if (obj.ID > 0)
                {
                    obj.Status = no1.Status;
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

        [Route("ListNotification")]
        [HttpGet]
        public object ListNotification(string status)
        {
            var obj = db.Notifications.Where(x => x.Status == status).ToList();
            return obj;
        }

    }
}