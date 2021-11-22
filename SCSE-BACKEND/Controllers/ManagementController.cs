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

    [RoutePrefix("Management")]
    public class ManagementController : ApiController
    {
        string data = TokenManager.ValidateCheck();
        SCSE_DBEntities db = new SCSE_DBEntities();

        [Route("AddOrEditPost")]
        [HttpPost]
        public object AddOrEditPost(Posts1 Posts1)
        {
            if (data == "OK" || data == "Mod")
            {
                if (Posts1.IDPost == 0)
                {
                    Post Posts = new Post
                    {
                        IDCat = Posts1.IDCat,
                        Title = Posts1.Title,
                        IDField = Posts1.IDField,
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
                        obj.IDField = Posts1.IDField;
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        [Route("EditStatePost")]
        [HttpPost]
        public object EditStatePost(Posts1 Posts1)
        {
            if (data == "OK")
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

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
                         Posts.IDField,
                         Posts.Slug,
                         Posts.Details,
                         Posts.Image,
                         Posts.CreatedByDate,
                         Posts.Author,
                         Posts.IDState,
                         cat.CategoryName,
                     }).ToList();
            return a;
        }
        // Xóa bài viết
        [Route("DeletePost")]
        [HttpDelete]
        public object DeletePost(int ID)
        {
            if (data == "OK")
            {
                var obj = db.Posts.Where(x => x.IDPost == ID).ToList().FirstOrDefault();
                db.Posts.Remove(obj);
                db.SaveChanges();
                return new Response
                {
                    Status = "Delete",
                    Message = "Delete Successfuly"
                };
            }
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        [Route("GetByStatePost")]
        [HttpGet]
        public object GetByStatePost(int IdState)
        {
            var category = db.Posts.Where(x => x.IDState == IdState).ToList();
            return category;
        }
        // getbyID bài viết
        [Route("GetBySlugPost")]
        [HttpGet]
        public object GetBySlugPost(string slug)
        {
            var obj = db.Posts.Where(x => x.Slug == slug).ToList();
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
            if (data == "OK")
            {
                try
                {
                    PostsEN PostsEN = new PostsEN
                    {
                        IDPostEN = PostsEN1.IDPostEN,
                        IDCat = PostsEN1.IDCat,
                        Title = PostsEN1.Title,
                        IDField = PostsEN1.IDField,
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        [Route("EditPostEN")]
        [HttpPost]
        public object EditPostEN(PostsEN1 PostsEN1)
        {
            if (data == "OK")
            {
                var obj = db.PostsENs.Where(x => x.IDPostEN == PostsEN1.IDPostEN).FirstOrDefault();
                if (obj.IDPostEN > 0)
                {
                    obj.IDCat = PostsEN1.IDCat;
                    obj.Title = PostsEN1.Title;
                    obj.IDField = PostsEN1.IDField;
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

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
                         PostsEN.IDField,
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
            if (data == "OK")
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        // Xóa bài viết
        [Route("DeletePostEN")]
        [HttpDelete]
        public object DeletePostEN(int ID)
        {
            if (data == "OK")
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }


        [Route("GetByStatePostEN")]
        [HttpGet]
        public object GetByStatePostEN(int IdStateen)
        {
            var category = db.PostsENs.Where(x => x.IDState == IdStateen).ToList();
            return category;
        }
        // getbyID bài viết
        [Route("GetBySlugPostEN")]
        [HttpGet]
        public object GetBySlugPostEN(string slugen)
        {
            var obj = db.PostsENs.Where(x => x.SlugEN == slugen).ToList();
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
        [Route("GetByIdPosts")]
        [HttpGet]
        public object GetByIdPosts(int ID)
        {
            var obj = db.Posts.Where(x => x.IDPost == ID).ToList().FirstOrDefault();
            return obj;
        }
        [Route("GetByIdPostsEN")]
        [HttpGet]
        public object GetByIdPostsEN(int ID)
        {
            var obj = db.PostsENs.Where(x => x.IDPostEN == ID).ToList().FirstOrDefault();
            return obj;
        }
        [Route("GetMultiLanguagePost")]
        [HttpGet]
        public object GetMultiLanguagePost(string slug)
        {
            var obj = db.Posts.Where(x => x.Slug == slug).FirstOrDefault();
            var result = (from post in db.Posts
                          select new
                          {
                              TiengViet = db.Posts.Where(x => x.Slug == slug).FirstOrDefault(),
                              English = db.PostsENs.Where(x => x.IDPostEN == obj.IDPost).FirstOrDefault()
                          }).FirstOrDefault();
            return result;
        }
        [Route("ShowAllPostENNeedPost")]
        [HttpGet]
        public object ShowAllPostENNeedPost()
        {
            var a = db.PostsforENs.ToList();
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
                    return new Response
                    {
                        Status = "Error",
                        Message = "Data not insert"
                    };
                }
        }
        [Route("EditState")]
        [HttpPost]
        public object EditState(Volunteer1 vol1)
        {
            if (data == "OK")
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

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
            if (data == "OK")
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

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

        //TIN TỨC
        [Route("AddOrEditNewsVN")]
        [HttpPost]
        public object AddOrEditNewsVN(NewsVN1 newsVN1)
        {
            if (data == "OK" || data == "Mod")
            {
                if (newsVN1.IDNews == 0)
                {
                    NewsVN newsVN = new NewsVN
                    {
                        IdField = newsVN1.IdField,
                        Title = newsVN1.Title,
                        Slug = Utils.ReplaceSpecialChars(newsVN1.Title),
                        Details = newsVN1.Details,
                        Image = newsVN1.Image,
                        IDState = 1,
                        Author = newsVN1.Author,
                        CreatedByDate = DateTime.Now
                    };
                    db.NewsVNs.Add(newsVN);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Success"
                    };
                }
                else
                {
                    var obj = db.NewsVNs.Where(x => x.IDNews == newsVN1.IDNews).ToList().FirstOrDefault();
                    if (obj.IDNews > 0)
                    {
                        obj.IdField = newsVN1.IdField;
                        obj.Title = newsVN1.Title;
                        obj.Slug = Utils.ReplaceSpecialChars(newsVN1.Title);
                        obj.Details = newsVN1.Details;
                        obj.Image = newsVN1.Image;
                        obj.IDState = newsVN1.IDState;
                        obj.Author = newsVN1.Author;
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }

        [Route("EditStateNewsVN")]
        [HttpPost]
        public object EditStateNewsVN(NewsVN1 new1)
        {
            if (data == "OK")
            {
                var obj = db.NewsVNs.Where(x => x.IDNews == new1.IDNews).FirstOrDefault();
                if (obj.IDNews > 0)
                {
                    obj.IDState = new1.IDState;
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        // Xem danh sách tin tức
        [Route("ShowAllNewsVN")]
        [HttpGet]
        public object ShowAllNewsVN()
        {
            var NEWS = (from newsvn in db.NewsVNs
                        from field in db.Fields
                        where field.IdField == newsvn.IdField

                        select new
                        {
                            newsvn.IDNews,
                            newsvn.IdField,
                            newsvn.Title,
                            newsvn.Slug,
                            newsvn.Details,
                            newsvn.Image,
                            newsvn.CreatedByDate,
                            newsvn.Author,
                            newsvn.IDState,
                            field.FieldName
                        }).ToList();
            return NEWS;
        }
        // Xóa tin tức
        [Route("DeleteNewsVN")]
        [HttpDelete]
        public object DeleteNewsVN(int ID)
        {
            if (data == "OK")
            {
                var obj = db.NewsVNs.Where(x => x.IDNews == ID).FirstOrDefault();
                db.NewsVNs.Remove(obj);
                db.SaveChanges();
                return new Response
                {
                    Status = "Delete",
                    Message = "Delete Successfuly"
                };
            }
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }

        [Route("GetByStateNewsVN")]
        [HttpGet]
        public object GetByStateNewsVN(int IdState)
        {
            var list = db.NewsVNs.Where(x => x.IDState == IdState).ToList();
            return list;
        }
        // getbyID bài viết 
        [Route("GetBySlugNewsVN")]
        [HttpGet]
        public object GetBySlugNewsVN(string slug)
        {
            var obj = db.NewsVNs.Where(x => x.Slug == slug).FirstOrDefault();
            return obj;
        }
        // getbyID the loai
        [Route("GetByIdField")]
        [HttpGet]
        public object GetByIdField(int IdField)
        {
            var obj = db.NewsVNs.Where(x => x.IdField == IdField).ToList();
            return obj;
        }
        [Route("GetByIdNewsVN")]
        [HttpGet]
        public object GetByIdNewsVN(int ID)
        {
            var obj = db.NewsVNs.Where(x => x.IDNews == ID).ToList().FirstOrDefault();
            return obj;
        }
        [Route("GetByIdNewsEN")]
        [HttpGet]
        public object GetByIdNewsEN(int ID)
        {
            var obj = db.NewsENs.Where(x => x.IDNewsEN == ID).ToList().FirstOrDefault();
            return obj;
        }
        [Route("AddNewsEN")]
        [HttpPost]
        public object AddNewsEN(NewsEN1 newsEN1)
        {
            if (data == "OK" || data == "Mod")
            {
                try
                {
                    NewsEN newsEN = new NewsEN
                    {
                        IDNewsEN = newsEN1.IDNewsEN,
                        IdField = newsEN1.IdField,
                        Title = newsEN1.Title,
                        SlugEN = Utils.ReplaceSpecialChars(newsEN1.Title),
                        Details = newsEN1.Details,
                        Image = newsEN1.Image,
                        IDState = 1,
                        Author = newsEN1.Author,
                        CreatedByDate = DateTime.Now,

                    };
                    db.NewsENs.Add(newsEN);
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        [Route("EditNewsEN")]
        [HttpPost]
        public object EditNewsEN(NewsEN1 newsEN1)
        {
            if (data == "OK")
            {
                var obj = db.NewsENs.Where(x => x.IDNewsEN == newsEN1.IDNewsEN).FirstOrDefault();
                if (obj.IDNewsEN > 0)
                {
                    obj.IdField = newsEN1.IdField;
                    obj.Title = newsEN1.Title;
                    obj.SlugEN = Utils.ReplaceSpecialChars(newsEN1.Title);
                    obj.Details = newsEN1.Details;
                    obj.Image = newsEN1.Image;
                    obj.IDState = newsEN1.IDState;
                    obj.Author = newsEN1.Author;
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        // Xem danh sách tin tức tiếng anh
        [Route("ShowAllNewsEN")]
        [HttpGet]
        public object ShowAllNewsEN()
        {
            var a = (from newsEN in db.NewsENs
                     from field in db.Fields
                     where field.IdField == newsEN.IdField

                     select new
                     {
                         newsEN.IDNewsEN,
                         newsEN.IdField,
                         newsEN.Title,
                         newsEN.SlugEN,
                         newsEN.Details,
                         newsEN.Image,
                         newsEN.CreatedByDate,
                         newsEN.Author,
                         newsEN.IDState,
                         field.FieldName
                     }).ToList();
            return a;
        }
        [Route("EditStateNewsEN")]
        [HttpPost]
        public object EditStateNewsEN(NewsEN1 newsEN1)
        {
            if (data == "OK")
            {
                var obj = db.NewsENs.Where(x => x.IDNewsEN == newsEN1.IDNewsEN).FirstOrDefault();
                if (obj.IDNewsEN > 0)
                {
                    obj.IDState = newsEN1.IDState;
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
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }
        // Xóa tin tức
        [Route("DeleteNewsEN")]
        [HttpDelete]
        public object DeleteNewsEN(int ID)
        {
            if (data == "OK")
            {
                var obj = db.NewsENs.Where(x => x.IDNewsEN == ID).FirstOrDefault();
                db.NewsENs.Remove(obj);
                db.SaveChanges();
                return new Response
                {
                    Status = "Delete",
                    Message = "Delete Successfuly"
                };
            }
            else
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Token Fail"
                };
            }

        }

        [Route("GetByStateNewsEN")]
        [HttpGet]
        public object GetByStateNewsEN(int IdStateEN)
        {
            var listEN = db.NewsENs.Where(x => x.IDState == IdStateEN).ToList();
            return listEN;
        }
        // getbyID tin tức
        [Route("GetBySlugNewsEN")]
        [HttpGet]
        public object GetBySlugNewsEN(string slugEN)
        {
            var obj = db.NewsENs.Where(x => x.SlugEN == slugEN).FirstOrDefault();
            return obj;
        }
        // getbyID the loai
        [Route("GetByIdFieldEN")]
        [HttpGet]
        public object GetByIdFieldEN(int IDField)
        {
            var obj = db.NewsENs.Where(x => x.IdField == IDField).ToList();
            return obj;
        }

        [Route("GetMultiLanguesSlugNews")]
        [HttpGet]
        public object GetMultiLanguesSlugNEW()
        {
            var a = (from newsEN in db.NewsENs
                     from newsVN in db.NewsVNs
                     from field in db.Fields
                     where newsVN.IDNews == newsEN.IDNewsEN
                     select new
                     {
                         newsEN.SlugEN,
                         newsVN.Slug
                     }
                     ).FirstOrDefault();
            return a;
        }
        [Route("ListNewsNotVersionEN")]
        [HttpGet]
        public object ListNewsNotVersionEN()
        {
            var a = db.NewsVNforENs.ToList();
            return a;
        }

        [Route("ListImageTitle")]
        [HttpGet]
        public object ListImageTitle()
        {
            PhotoGallery[] photo = db.PhotoGalleries.ToArray();
            List<object> listImageTitle = new List<object>();
            for (int i = 1; i < photo.Count(); i++)
            {
                if (photo[i].Slug != photo[i - 1].Slug)
                {
                    listImageTitle.Add(photo[i - 1]);
                }
                else if (i == photo.Count() - 1)
                {
                    listImageTitle.Add(photo[i]);
                }

            }
            return listImageTitle;
        }
        [Route("GetCountForHomeCMS")]
        [HttpGet]
        public object GetCountForHomeCMS()
        {
            var NewsAll = db.NewsVNs.Where(x => x.IDState == 2).ToList().Count;
            var NewsPending = db.NewsVNs.Where(x => x.IDState == 1).ToList().Count;
            var DuAnAll = db.Posts.Where(x => x.IDCat == 1 && x.IDState == 2).ToList().Count;
            var DuAnPending = db.Posts.Where(x => x.IDCat == 1 && x.IDState == 1).ToList().Count;
            var HDTNAll = db.Posts.Where(x => x.IDCat == 3 && x.IDState == 2).ToList().Count;
            var HDTNPending = db.Posts.Where(x => x.IDCat == 3 && x.IDState == 1).ToList().Count;
            var HTNCAll = db.Posts.Where(x => x.IDCat == 2 && x.IDState == 2).ToList().Count;
            var HTNCPending = db.Posts.Where(x => x.IDCat == 2 && x.IDState == 1).ToList().Count;
            var AccountPending = db.Accounts.Where(x => x.IDState == 1).ToList().Count;
            var VolunteerPending = db.Volunteers.Where(x => x.IDState == 1).ToList().Count;
            var CountData = (from p in db.Posts
                             select new
                             {
                                 CountNewsVnAll = NewsAll,
                                 CountNewsPending = NewsPending,
                                 CountDuAn = DuAnAll,
                                 CountDuAnPending = DuAnPending,
                                 CountHDTNAll = HDTNAll,
                                 CountHDTNPending = HDTNPending,
                                 CountHTNCAll = HTNCAll,
                                 CountHTNCPending = HTNCPending,
                                 CountAccountPending = AccountPending,
                                 CountVolunteerPending = VolunteerPending
                             }
                     ).FirstOrDefault();
            return CountData;
        }

        [Route("GetCountForHomeCMSEN")]
        [HttpGet]
        public object GetCountForHomeCMSEN()
        {
            var NewsAll = db.NewsENs.Where(x => x.IDState == 2).ToList().Count;
            var NewsPending = db.NewsENs.Where(x => x.IDState == 1).ToList().Count;
            var DuAnAll = db.PostsENs.Where(x => x.IDCat == 1 && x.IDState == 2).ToList().Count;
            var DuAnPending = db.PostsENs.Where(x => x.IDCat == 1 && x.IDState == 1).ToList().Count;
            var HDTNAll = db.PostsENs.Where(x => x.IDCat == 3 && x.IDState == 2).ToList().Count;
            var HDTNPending = db.PostsENs.Where(x => x.IDCat == 3 && x.IDState == 1).ToList().Count;
            var HTNCAll = db.PostsENs.Where(x => x.IDCat == 2 && x.IDState == 2).ToList().Count;
            var HTNCPending = db.PostsENs.Where(x => x.IDCat == 2 && x.IDState == 1).ToList().Count;
            var CountData = (from p in db.Posts
                             select new
                             {
                                 CountNewsEnAll = NewsAll,
                                 CountNewsPending = NewsPending,
                                 CountDuAn = DuAnAll,
                                 CountDuAnPending = DuAnPending,
                                 CountHDTNAll = HDTNAll,
                                 CountHDTNPending = HDTNPending,
                                 CountHTNCAll = HTNCAll,
                                 CountHTNCPending = HTNCPending
                             }
                     ).FirstOrDefault();
            return CountData;
        }
    }
}