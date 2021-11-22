using SCSE_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.Entity;
using System.Web.Http;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace SCSE_BACKEND.Controllers
{
    [RoutePrefix("Interface")]
    public class InterfaceController : ApiController
    {
        //Nội Dung Trang Chủ 
        string data = TokenManager.ValidateCheck();
        SCSE_DBEntities db = new SCSE_DBEntities();

        //---------------------------Video Model---------------------------//
        [Route("ShowAllVideo")]
        [HttpGet]
        public object ShowAllVideo()
        {
            var result = db.VideoGalleries.ToList();
            return result;
        }
        [Route("AddOrEditVideo")]
        [HttpPost]
        public object AddOrEditVideo(VideoGallery1 video1)
        {
            if (data == "OK")
            {
                if (video1.ID == 0)
                {
                    VideoGallery video = new VideoGallery
                    {
                        ID = video1.ID,
                        IDField = video1.IDField,
                        IDCat = video1.IDCat,
                        TitleEN = video1.TitleEN,
                        Title = video1.Title,
                        VideoID = video1.VideoID,
                        DescriptionEN = video1.DescriptionEN,
                        Description = video1.Description,
                        Image = video1.Image,
                        LinkYTB = video1.LinkYTB,
                        CreatedByDate = DateTime.Now,
                    };
                    db.VideoGalleries.Add(video);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Success"
                    };
                }
                else
                {
                    var obj = db.VideoGalleries.Where(x => x.ID == video1.ID).ToList().FirstOrDefault();
                    if (obj.ID > 0)
                    {
                        obj.IDCat = video1.IDCat;
                        obj.IDField = video1.IDField;
                        obj.Title = video1.Title;
                        obj.TitleEN = video1.TitleEN;
                        obj.VideoID = video1.VideoID;
                        obj.DescriptionEN = video1.DescriptionEN;
                        obj.Description = video1.Description;
                        obj.Image = video1.Image;
                        obj.LinkYTB = video1.LinkYTB;
                        obj.UpdateByDate = DateTime.Now;
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
        [Route("DeleteVideo")]
        [HttpDelete]
        public object DeleteVideo(int id)
        {
            if (data == "OK")
            {
                var obj = db.VideoGalleries.Where(x => x.ID == id).FirstOrDefault();
                db.VideoGalleries.Remove(obj);
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
        [Route("GetByIDVideo")]
        [HttpGet]
        public object GetByIDVideo(int id)
        {
            var result = db.VideoGalleries.Where(x => x.ID == id).FirstOrDefault();
            return result;
        }
        [Route("GetVideoByTitleEN")]
        [HttpGet]
        public object GetVideoByTitleEN(string TitleEN)
        {
            var result = db.VideoGalleries.Where(x => x.TitleEN == TitleEN).FirstOrDefault();
            return result;
        }
        //Dữ liệu ban giám đốc 
        [Route("AddOrEditPortfolios")]
        [HttpPost]
        public object AddOrEditPortfolios(Portfolio1 po1)
        {
            if (data == "OK")
            {
                if (po1.ID == 0)
                {
                    Portfolio po = new Portfolio
                    {
                        Details = po1.Details,
                        DetailsEN = po1.DetailsEN,
                        FullName = po1.FullName,
                        Position = po1.Position,
                        PositionEN = po1.PositionEN,
                        Image1 = po1.Image1,
                        Image2 = po1.Image2,
                        Image3 = po1.Image3,
                    };
                    db.Portfolios.Add(po);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Success"
                    };
                }
                else
                {
                    var obj = db.Portfolios.Where(x => x.ID == po1.ID).ToList().FirstOrDefault();
                    if (obj.ID > 0)
                    {
                        obj.FullName = po1.FullName;
                        obj.Position = po1.Position;
                        obj.PositionEN = po1.PositionEN;
                        obj.Details = po1.Details;
                        obj.DetailsEN = po1.DetailsEN;
                        //obj.Image1 = po1.Image1;
                        //obj.Image2 = po1.Image2;
                        //obj.Image3 = po1.Image3;
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
        [Route("EditImagePortfolios")]
        [HttpPost]
        public object EditImagePortfolios(Portfolio1 po1)
        {
            if (data == "OK")
            {
                var obj = db.Portfolios.Where(x => x.ID == po1.ID).FirstOrDefault();
                if (obj.ID > 0)
                {
                    obj.Image1 = po1.Image1;
                    obj.Image2 = po1.Image2;
                    obj.Image3 = po1.Image3;
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
        [Route("ShowAllPortfolio")]
        [HttpGet]
        public object ShowAllPortfolio()
        {
            var result = db.Portfolios.ToList();
            return result;
        }
        [Route("GetByNamePortfolios")]
        [HttpGet]
        public object GetByNamePortfolios(string name)
        {
            var obj = db.Portfolios.Where(x => x.FullName == name).FirstOrDefault();
            return obj;
        }
        [Route("GetByIdPortfolios")]
        [HttpGet]
        public object GetByIdPortfolios(int id)
        {
            var obj = db.Portfolios.Where(x => x.ID == id).FirstOrDefault();
            return obj;
        }
        [Route("DeletePortfolio")]
        [HttpDelete]
        public object DeletePortfolio(string fullname)
        {
            if (data == "OK")
            {

                var obj = db.Portfolios.Where(x => x.FullName == fullname).FirstOrDefault();
                db.Portfolios.Remove(obj);
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
        //  Thêm/Sửa Thông Tin Ngân Hàng 
        [Route("AddOrEditBankInfo")]
        [HttpPost]
        public object AddOrEditBankInfo(BankInformation1 bank1)
        {
            if (data == "OK")
            {
                if (bank1.ID == 0)
                {
                    BankInformation bank = new BankInformation();
                    bank.BankName = bank1.BankName;
                    bank.ImageQR = bank1.ImageQR;
                    db.BankInformations.Add(bank);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfully"
                    };
                }
                else
                {
                    var obj = db.BankInformations.Where(x => x.ID == bank1.ID).ToList().FirstOrDefault();
                    if (obj.ID > 0)
                    {
                        obj.ImageQR = bank1.ImageQR;
                        obj.BankName = bank1.BankName;
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

        // getbyID OrganizationConfigurations
        [Route("GetByIdBankInfo")]
        [HttpGet]
        public object GetByIdBankInfo(int ID)
        {
            var obj = db.BankInformations.Where(x => x.ID == ID).ToList().FirstOrDefault();
            return obj;
        }
        [Route("ShowAllBankInfo")]
        [HttpGet]
        public object ShowAllBankInfo()
        {
            var bank = db.BankInformations.ToList();
            return bank;
        }
        //Xóa thông tin ngân hàng 
        [Route("DeleteBankInfo")]
        [HttpDelete]
        public object DeleteBankInfo(int ID)
        {
            if (data == "OK")
            {
                var obj = db.BankInformations.Where(x => x.ID == ID).ToList().FirstOrDefault();
                db.BankInformations.Remove(obj);
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
        //Thông Tin Đối Tác 
        [Route("AddOrEditPartner")]
        [HttpPost]
        public object AddOrEditPartnerAsync(Partner1 pn1)
        {
            if (pn1.ID == 0)
            {
                Partner pn = new Partner
                {
                    ContactPerson = pn1.ContactPerson,
                    OrganizationName = pn1.OrganizationName,
                    Image = pn1.Image,
                    OrganizationProgrames = pn1.OrganizationProgrames,
                    Phone = pn1.Phone,
                    Email = pn1.Email,
                    Address = pn1.Address,
                    Link = pn1.Link,
                    Purpose = pn1.Purpose,
                    LinkFile = pn1.LinkFile,
                    IDState = 1
                };
                db.Partners.Add(pn);
                db.SaveChanges();
                return new Response
                {
                    Status = "Success",
                    Message = "Data Successfuly"
                };
            }
            else
            {
                string data = TokenManager.ValidateCheck();
                if (data == "OK")
                {
                    var obj = db.Partners.Where(x => x.ID == pn1.ID).ToList().FirstOrDefault();
                    if (obj.ID > 0)
                    {
                        obj.OrganizationName = pn1.OrganizationName;
                        obj.ContactPerson = pn1.ContactPerson;
                        obj.Image = pn1.Image;
                        obj.OrganizationProgrames = pn1.OrganizationProgrames;
                        obj.Purpose = pn1.Purpose;
                        obj.Phone = pn1.Phone;
                        obj.Email = pn1.Email;
                        obj.Address = pn1.Address;
                        obj.Link = pn1.Link;
                        obj.LinkFile = pn1.LinkFile;
                        obj.IDState = pn1.IDState;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfuly"
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
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }
        [Route("UploadFileVipPro")]
        [HttpPost]
        // Upload file to directory
        public async Task<string> UploadFile()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/assets");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content
                    .ReadAsMultipartAsync(provider);
                Partner1 p = new Partner1
                {
                    OrganizationName = provider.FormData["OrganizationName"],
                    ContactPerson = provider.FormData["ContactPerson"],
                    OrganizationProgrames = provider.FormData["OrganizationProgrames"],
                    Phone = provider.FormData["Phone"],
                    Email = provider.FormData["Email"],
                    Address = provider.FormData["Address"],
                    Link = provider.FormData["Link"],
                    Purpose = provider.FormData["Purpose"]
                };
                var pathName = provider.FormData["OrganizationName"];
                var folderUpload = Utils.ReplaceSpecialChars(pathName);
                var pathString = Path.Combine(root, folderUpload);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                    foreach (var file in provider.FileData)
                    {
                        var name = file.Headers.ContentDisposition.FileName;
                        name = name.Trim('"');
                        var localFileName = file.LocalFileName;
                        var filePath = Path.Combine(pathString, name);
                        if (File.Exists(filePath))
                        {
                            File.Delete(file.LocalFileName);
                            return $"Error: File Exists";
                        }
                        else
                        {
                            p.LinkFile = "assets/" + folderUpload + "/" + name;
                            AddOrEditPartnerAsync(p);
                            File.Move(localFileName, filePath);
                        }
                    }
                }
                else
                {
                    foreach (var file in provider.FileData)
                    {
                        var name = file.Headers.ContentDisposition.FileName;
                        name = name.Trim('"');
                        var localFileName = file.LocalFileName;
                        var filePath = Path.Combine(pathString, name);
                        if (File.Exists(filePath))
                        {
                            File.Delete(file.LocalFileName);
                            return $"Error: File Exists";
                        }
                        else
                        {
                            p.LinkFile = "assets/" + folderUpload + "/" + name;
                            AddOrEditPartnerAsync(p);
                            File.Move(localFileName, filePath);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
            return "File uploaded!";
        }
        [Route("GetByIdPartner")]
        [HttpGet]
        public object GetByIdPartner(int ID)
        {
            var partner = db.Partners.Where(x => x.ID == ID).ToList().FirstOrDefault();
            return partner;
        }

        [Route("ListPartner")]
        [HttpGet]
        public object ListPartner()
        {
            var partner = db.Partners.ToList();
            return partner;
        }

        [Route("DeletePartner")]
        [HttpDelete]
        public object DeletePartner(int ID)
        {
            if (data == "OK")
            {
                var partner = db.Partners.Where(x => x.ID == ID).ToList().FirstOrDefault();
                db.Partners.Remove(partner);
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

        //Thông tin liên hệ 

        [Route("ViewAllContact")]
        [HttpGet]
        public object ViewAllContact()
        {
            var result = db.Contacts.ToList();
            return result;
        }
        [Route("DeleteContact")]
        [HttpDelete]
        public object DeleteContact(int ID)
        {
            if (data == "OK")
            {
                var result = db.Contacts.Where(x => x.ID == ID).ToList().FirstOrDefault();
                db.Contacts.Remove(result);
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

        [Route("AddOrEditContact")]
        [HttpPost]
        public object AddOrEditContact(Contact1 ct1)
        {
                if (ct1.ID == 0)
                {
                    Contact ct = new Contact
                    {
                        Address = ct1.Address,
                        FullName = ct1.FullName,
                        Subtitle = ct1.Subtitle,
                        Phone = ct1.Phone,
                        Email = ct1.Email,
                        Details = ct1.Details,
                        CreatedByDate = DateTime.Now
                    };
                    db.Contacts.Add(ct);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfuly"
                    };
                }
                else
                {
                    var contact = db.Contacts.Where(x => x.ID == ct1.ID).ToList().FirstOrDefault();
                    if (ct1.ID > 0)
                    {
                        contact.FullName = ct1.FullName;
                        contact.Subtitle = ct1.Subtitle;
                        contact.Phone = ct1.Phone;
                        contact.Email = ct1.Email;
                        contact.Details = ct1.Details;
                        contact.Address = ct1.Address;
                        contact.UpdatedByDate = DateTime.Now;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfuly"
                        };
                    }
                }
                return new Response
                {
                    Status = "Error",
                    Message = "Data not insert"
                };
        }
        [Route("AddOrEditCategory")]
        [HttpPost]
        public object AddOrEditCategory(Category1 category1)
        {
            if (data == "OK")
            {
                if (category1.IDCat == 0)
                {
                    Category category = new Category();
                    category.CategoryName = category1.CategoryName;
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfuly"
                    };
                }
                else
                {
                    var obj = db.Categories.Where(x => x.IDCat == category1.IDCat).ToList().FirstOrDefault();
                    if (category1.IDCat > 0)
                    {
                        obj.CategoryName = category1.CategoryName;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfuly"
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
        [Route("ListCategory")]
        [HttpGet]
        public object ListCategory()
        {
            var cate = db.Categories.ToList();
            return cate;
        }

        [Route("GetByIdCategory")]
        [HttpGet]
        public object GetByIdCategory(int ID)
        {
            var category = db.Categories.Where(x => x.IDCat == ID).ToList();
            return category;
        }
        [Route("ShowAllField")]
        [HttpGet]
        public object ShowAllField()
        {
            var filed = db.Fields.ToList();
            return filed;
        }
        [Route("GetPostBySlug")]
        [HttpGet]
        public object GetPostBySlug(string slug)
        {
            var category = db.Posts.Where(x => x.Slug == slug).ToList();
            return category;
        }
        //Thư Viện 
        [Route("AddOrEditPhotoGallery")]
        [HttpPost]
        public object AddOrEditPhotoGallery(PhotoGallery1 photo1)
        {
            if (data == "OK")
            {
                if (photo1.ID == 0)
                {
                    PhotoGallery photo = new PhotoGallery
                    {
                        IDCat = photo1.IDCat,
                        IDField = photo1.IDField,
                        Title = photo1.Title,
                        TitleEN = photo1.TitleEN,
                        Slug = Utils.ReplaceSpecialChars(photo1.Title),
                        Image = photo1.Image,
                        CreatedByDate = DateTime.Now
                    };
                    db.PhotoGalleries.Add(photo);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data successfuly"
                    };
                }
                else
                {
                    var obj = db.PhotoGalleries.Where(x => x.ID == photo1.ID).ToList().FirstOrDefault();
                    if (photo1.ID > 0)
                    {
                        obj.IDCat = photo1.IDCat;
                        obj.IDField = photo1.IDField;
                        obj.Title = photo1.Title;
                        obj.TitleEN = photo1.TitleEN;
                        obj.Image = photo1.Image;
                        obj.Slug = Utils.ReplaceSpecialChars(photo1.Title);
                        obj.UpdatedByDate = DateTime.Now;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfuly"
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
        [Route("UploadAlbum")]
        [HttpPost]
        public async Task<string> UploadAlbum()
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/assets/albums");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                PhotoGallery1 photo = new PhotoGallery1
                {
                    IDCat = Int32.Parse(provider.FormData["IDCat"]),
                    IDField = Int32.Parse(provider.FormData["IDField"]),
                    Title = provider.FormData["Title"],
                    TitleEN = provider.FormData["TitleEN"],
                };
                var pathName = provider.FormData["Title"];
                var folderUpload = Utils.ReplaceSpecialChars(pathName);
                var pathString = Path.Combine(root, folderUpload);
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                    for (var i = 0; i < provider.FileData.Count; i++)
                    {
                        var file = provider.FileData[i];
                        var name = file.Headers.ContentDisposition.FileName;
                        name = name.Trim('"');
                        var localFileName = file.LocalFileName;
                        var filePath = Path.Combine(pathString, name);
                        if (File.Exists(filePath))
                        {
                            File.Delete(file.LocalFileName);
                            return $"Error: File Exists";
                        }
                        else
                        {
                            photo.Image = "assets/albums/" + folderUpload + "/" + name;
                            AddOrEditPhotoGallery(photo);
                            File.Move(localFileName, filePath);
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < provider.FileData.Count; i++)
                    {
                        var file = provider.FileData[i];
                        var name = file.Headers.ContentDisposition.FileName;
                        name = name.Trim('"');
                        var localFileName = file.LocalFileName;
                        var filePath = Path.Combine(pathString, name);
                        if (File.Exists(filePath))
                        {
                            File.Delete(file.LocalFileName);
                            return $"Error: File Exists";
                        }
                        else
                        {
                            photo.Image = "assets/albums/" + folderUpload + "/" + name;
                            AddOrEditPhotoGallery(photo);
                            File.Move(localFileName, filePath);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
            return "File uploaded!";
        }
        [Route("DeletePhoto")]
        [HttpDelete]
        public object DeletePhoto(int id)
        {
            if (data == "OK")
            {

                var result = db.PhotoGalleries.Where(x => x.ID == id).FirstOrDefault();
                var root = HttpContext.Current.Server.MapPath("~/");
                var pathString = Path.Combine(root, result.Image);
                File.Delete(pathString);
                db.PhotoGalleries.Remove(result);
                db.SaveChanges();
                return new Response
                {
                    Status = "Delete",
                    Message = "Delete successfully"
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
        [Route("ListPhoto")]
        [HttpGet]
        public object ListPhoto()
        {
            var photo = db.PhotoGalleries.ToList();
            return photo;
        }
        [Route("GetByIDPhotoGallery")]
        [HttpGet]
        public object GetByIDPhotoGallery(int ID)
        {
            var category = db.PhotoGalleries.Where(x => x.ID == ID).FirstOrDefault();
            return category;
        }
        [Route("GetPhotosByTitleEN")]
        [HttpGet]
        public object GetPhotosByTitleEN(string TitleEN)
        {
            var category = db.PhotoGalleries.Where(x => x.TitleEN == TitleEN).ToList();
            return category;
        }
        [Route("GetBySlugPhotoGallery")]
        [HttpGet]
        public object GetBySlugGallery(string slug)
        {
            var category = db.PhotoGalleries.Where(x => x.Slug == slug).ToList();
            return category;
        }
        [Route("GetPostBySlugToAdd")]
        [HttpGet]
        public object GetPostBySlugToAdd(string slug)
        {
            var category = db.PhotoGalleries.Where(x => x.Slug == slug).FirstOrDefault();
            return category;
        }
        [Route("DeletePhotosByTitle")]
        [HttpDelete]
        public object DeletePhotosByTitle(string title)
        {
            if (data == "OK")
            {
                var result = db.PhotoGalleries.Where(x => x.Title == title).ToList();
                var root = HttpContext.Current.Server.MapPath("~/");
                for (var i = 0; i < result.Count; i++)
                {
                    db.PhotoGalleries.Remove(result[i]);
                    var pathString = Path.Combine(root, result[i].Image);
                    File.Delete(pathString);
                }
                db.SaveChanges();
                return new Response
                {
                    Status = "Delete",
                    Message = "Delete successfully"
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
        [Route("AddOrEditDocument")]
        [HttpPost]
        public object AddOrEditDocument(Document1 doc1)
        {
            if (data == "OK")
            {
                if (doc1.ID == 0)
                {
                    Document doc = new Document
                    {
                        ID = doc1.ID,
                        Title = doc1.Title,
                        Slug = Utils.ReplaceSpecialChars(doc1.Title),
                        Details = doc1.Details,
                        CreatedByDate = DateTime.Now
                    };
                    db.Documents.Add(doc);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data successfuly"
                    };
                }
                else
                {
                    var obj = db.Documents.Where(x => x.ID == doc1.ID).ToList().FirstOrDefault();
                    if (doc1.ID > 0)
                    {
                        obj.Title = doc1.Title;
                        obj.Details = doc1.Details;
                        obj.UpdatedByDate = DateTime.Now;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfuly"
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

        [Route("ListDocument")]
        [HttpGet]
        public object ListDocument()
        {
            var doc = db.Documents.ToList();
            return doc;
        }

        [Route("GetBySlugDocument")]
        [HttpGet]
        public object GetBySlugDocument(string slug)
        {
            var category = db.Documents.Where(x => x.Slug == slug).ToList();
            return category;
        }
        [Route("GetByIDDocument")]
        [HttpGet]
        public object GetByIDDocument(int ID)
        {
            var category = db.Documents.Where(x => x.ID == ID).FirstOrDefault();
            return category;
        }
        [Route("DeleteDocument")]
        [HttpDelete]
        public object DeleteDocument(int id)
        {
            if (data == "OK")
            {

                var result = db.Documents.Where(x => x.ID == id).ToList().FirstOrDefault();
                db.Documents.Remove(result);
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
        //---------------Tài liệu tiếng anh ---------------------------
        [Route("AddDocumentEN")]
        [HttpPost]
        public object AddDocumentEN(DocumentEN1 doc1)
        {
            if (data == "OK")
            {

                try
                {
                    DocumentEN doc = new DocumentEN
                    {
                        IDEN = doc1.IDEN,
                        Title = doc1.Title,
                        SlugEN = Utils.ReplaceSpecialChars(doc1.Title),
                        Details = doc1.Details,
                        CreatedByDate = DateTime.Now,

                    };
                    db.DocumentENs.Add(doc);
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
        [Route("GetByIDDocumentEN")]
        [HttpGet]
        public object GetByIDDocumentEN(int IDEN)
        {
            var category = db.DocumentENs.Where(x => x.IDEN == IDEN).FirstOrDefault();
            return category;
        }
        /*[Route("EditDocumentEN")]
        [HttpPost]
        public object EditDocumentEN(DocumentEN1 doc1)
        {
            var obj = db.DocumentENs.Where(x => x.IDEN == doc1.IDEN).FirstOrDefault();
            if (obj.IDEN > 0)
            {
                obj.Title = doc1.Title;
                obj.SlugEN = Utils.ReplaceSpecialChars(doc1.Title);
                obj.Details = doc1.Details;
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
        */
        [Route("ListDocumentEN")]
        [HttpGet]
        public object ListDocumentEN()
        {
            var doc = db.DocumentENs.ToList();
            return doc;
        }

        [Route("GetBySlugDocumentEN")]
        [HttpGet]
        public object GetBySlugDocumentEN(string slugen)
        {
            var category = db.DocumentENs.Where(x => x.SlugEN == slugen).ToList();
            return category;
        }
        [Route("ListDocumentNotVersionEN")]
        [HttpGet]
        public object ListDocumentNotVersionEN()
        {
            var a = db.DocumentsforENs.ToList();
            return a;
        }
        [Route("DeleteDocumentEN")]
        [HttpDelete]
        public object DeleteDocumentEN(int id)
        {
            if (data == "OK")
            {
                var result = db.DocumentENs.Where(x => x.IDEN == id).ToList().FirstOrDefault();
                db.DocumentENs.Remove(result);
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
        //------------------------------------------
        [Route("AddOrEditBanner")]
        [HttpPost]
        public object AddOrEditBanner(Banner1 bn1)
        {
            if (data == "OK")
            {
                if (bn1.ID == 0)
                {
                    Banner bn = new Banner
                    {
                        Name = bn1.Name,
                        Image = bn1.Image,
                        CreatedByUser = bn1.CreatedByUser,
                        CreatedByDate = DateTime.Now,

                    };
                    db.Banners.Add(bn);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfuly"
                    };
                }
                else
                {
                    var obj = db.Banners.Where(x => x.ID == bn1.ID).ToList().FirstOrDefault();
                    if (obj.ID > 0)
                    {
                        obj.Name = bn1.Name;
                        obj.Image = bn1.Image;
                        obj.UpdateByUser = bn1.UpdateByUser;
                        obj.UpdatedByDate = DateTime.Now;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfuly"
                        };
                    }
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

            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }

        [Route("GetByIdBanner")]
        [HttpGet]
        public object GetByIdBanner(int ID)
        {
            var partner = db.Banners.Where(x => x.ID == ID).ToList().FirstOrDefault();
            return partner;
        }

        [Route("ListBanner")]
        [HttpGet]
        public object ListBanner()
        {
            var partner = db.Banners.ToList();
            return partner;
        }

        [Route("DeleteBanner")]
        [HttpDelete]
        public object DeleteBanner(int ID)
        {
            if (data == "OK")
            {
                var partner = db.Banners.Where(x => x.ID == ID).ToList().FirstOrDefault();
                db.Banners.Remove(partner);
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

    }
}