using SCSE_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Data.Entity;
using System.Web.Http;

namespace SCSE_BACKEND.Controllers
{
    [RoutePrefix("API/Interface")]
    public class InterfaceController : ApiController
    {
        //Nội Dung Trang Chủ 
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
            if (video1.ID == 0)
            {
                VideoGallery video = new VideoGallery
                {
                    ID = video1.ID,
                    IDCat = video1.IDCat,
                    Title = video1.Title,
                    VideoID = video1.VideoID,
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
                    obj.Title = video1.Title;
                    obj.VideoID = video1.VideoID;
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
        [Route("DeleteVideo")]
        [HttpDelete]
        public object DeleteVideo(int id)
        {
            var obj = db.VideoGalleries.Where(x => x.ID == id).ToList().FirstOrDefault();
            db.VideoGalleries.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        [Route("GetByIDVideo")]
        [HttpGet]
        public object GetByIDVideo(int id)
        {
            var result = db.VideoGalleries.Where(x => x.ID == id).FirstOrDefault();
            return result;
        }

        //Dữ liệu ban giám đốc 
        [Route("AddOrEditPortfolios")]
        [HttpPost]
        public object AddOrEditPortfolios(Portfolio1 po1)
        {
            if (po1.ID == 0)
            {
                Portfolio po = new Portfolio
                {
                    Details = po1.Details,
                    FullName = po1.FullName,
                    Position = po1.Position,
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
                    obj.Details = po1.Details;
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
        [Route("ShowAllPorfolio")]
        [HttpGet]
        public object ShowAllPorfolio()
        {
            var result = (from po in db.Portfolios
                          select new
                          {
                              po.ID,
                              po.FullName,
                              po.Details,
                              po.Position,
                              Hinhanh = db.ImgPortfolios.Where(x => x.FullName == po.FullName).ToList()
                          }).ToList();
            return result;
        }
        [Route("GetByIdPortfolios")]
        [HttpGet]
        public object GetByIdPortfolios(int id)
        {
            var obj = db.Portfolios.Where(x => x.ID == id).ToList().FirstOrDefault();
            return obj;
        }

        [Route("EditImagePortfolios")]
        [HttpPost]
        public object EditImagePortfolios(ImgPortfolio1 imgpo1)
        {
            try
            {
                if (imgpo1.ID == 0)
                {
                    ImgPortfolio imgpo = new ImgPortfolio();
                    imgpo.FullName = imgpo1.FullName;
                    imgpo.ImagePortfolio = imgpo1.ImagePortfolio;
                    db.ImgPortfolios.Add(imgpo);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "Data Successfully"
                    };
                }
                else
                {
                    var obj = db.ImgPortfolios.Where(x => x.ID == imgpo1.ID).ToList().FirstOrDefault();
                    if (obj.ID > 0)
                    {
                        obj.ImagePortfolio = imgpo1.ImagePortfolio;
                        db.SaveChanges();
                        return new Response
                        {
                            Status = "Updated",
                            Message = "Updated Successfully"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return new Response
            {
                Status = "Error",
                Message = "Data not insert"
            };
        }

        [Route("GetByFullNameImgPortfolios")]
        [HttpGet]
        public object GetByFullNameImgPortfolios(string FullName)
        {
            var obj = db.ImgPortfolios.Where(x => x.FullName == FullName).ToList();
            return obj;
        }


        [Route("DeletePortfolio")]
        [HttpDelete]
        public object DeletePortfolio(int id)
        {
            var obj = db.Portfolios.Where(x => x.ID == id).FirstOrDefault();
            db.Portfolios.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }


        //  Thêm/Sửa Thông Tin Ngân Hàng 
        [Route("AddOrEditBankInfo")]
        [HttpPost]
        public object AddOrEditBankInfo(BankInformation1 bank1)
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
            var obj = db.BankInformations.Where(x => x.ID == ID).ToList().FirstOrDefault();
            db.BankInformations.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        //Thông Tin Đối Tác 
        [Route("AddOrEditPartner")]
        [HttpPost]
        public object AddOrEditPartner(Partner1 pn1)
        {
            if (pn1.ID == 0)
            {
                Partner pn = new Partner
                {
                    Name = pn1.Name,
                    Image = pn1.Image,
                    Field = pn1.Field,
                    Phone = pn1.Phone,
                    Email = pn1.Email,
                    Address = Utils.ReplaceSpecialChars(pn1.Address),
                    Link = pn1.Link
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
                var obj = db.Partners.Where(x => x.ID == pn1.ID).ToList().FirstOrDefault();
                if (obj.ID > 0)
                {
                    obj.Name = pn1.Name;
                    obj.Image = pn1.Image;
                    obj.Field = pn1.Field;
                    obj.Phone = pn1.Phone;
                    obj.Email = pn1.Email;
                    obj.Address = pn1.Address;
                    obj.Link = pn1.Link;
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

        [Route("GetByIdPartner")]
        [HttpGet]
        public object GetByIdPartner(int ID)
        {
            var partner = db.Partners.Where(x => x.ID == ID).ToList().FirstOrDefault();
            return partner;
        }

        [Route("ListPartner")]
        [HttpGet]
        public object listPartner()
        {
            var partner = db.Partners.ToList();
            return partner;
        }

        [Route("DeletePartner")]
        [HttpDelete]
        public object DeletePartner(int ID)
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
            var result = db.Contacts.Where(x => x.ID == ID).ToList().FirstOrDefault();
            db.Contacts.Remove(result);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }

        [Route("AddOrEditContact")]
        [HttpPost]
        public object AddOrEditContact(Contact1 ct1)
        {
            if (ct1.ID == 0)
            {
                Contact ct = new Contact
                {
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
            if (photo1.ID == 0)
            {
                PhotoGallery photo = new PhotoGallery
                {
                    IDCat = photo1.IDCat,
                    Title = photo1.Title,
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
                    obj.Title = photo1.Title;
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
            var category = db.PhotoGalleries.Where(x => x.ID == ID).ToList().FirstOrDefault();
            return category;
        }
        [Route("GetBySlugPhotoGallery")]
        [HttpGet]
        public object GetBySlugGallery(string slug)
        {
            var category = db.PhotoGalleries.Where(x => x.Slug == slug).ToList();
            return category;
        }
        [Route("AddOrEditDocument")]
        [HttpPost]
        public object AddOrEditDocument(Document1 doc1)
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
        [Route("DeleteDocument")]
        [HttpDelete]
        public object DeleteDocument(int id)
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
        [Route("AddOrEditBanner")]
        [HttpPost]
        public object AddOrEditBanner(Banner1 bn1)
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
            var partner = db.Banners.Where(x => x.ID == ID).ToList().FirstOrDefault();
            db.Banners.Remove(partner);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }

    }
}