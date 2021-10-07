using SCSE_BACKEND.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Net.Http;

namespace SCSE_BACKEND.Controllers
{
    [RoutePrefix("User")]
    
    public class UserController : ApiController
    {
       
        SCSE_DBEntities db = new SCSE_DBEntities();
        //------------------ Login -------------------------//
        [Route("Login")]
        [HttpPost]
        public Response EmployeeLogin(Login lg)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(lg.Password);
                var user = db.LoginRoles.Where(s => s.Username.Equals(lg.Username) && s.Password.Equals(f_password)).FirstOrDefault();
                if (user != null)
                {
                    return new UserResponse() { Status = "Success", Message = TokenManager.GenerateToken(user.FullName, user.RoleName, user.Username, user.Password, user.Email, user.IDState.ToString()) };
                }
            }
            else
            {
                return new Response { Status = "Fail", Message = "Login Fail" };
            }
            return new Response { Status = "Sai", Message = "Sai" };
        }
     
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        
        [Route("ThemTaiKhoan")]
        [HttpPost]
        public object ThemTaiKhoan(Account1 acc1)
        {
            if (acc1.IDUser == 0)
            {
                Account acc = new Account
                {
                    IDRole = acc1.IDRole,
                    Email = acc1.Email,
                    Username = acc1.Username,
                    Password = GetMD5(acc1.Password),
                    IDState = acc1.IDState,
                    CreatedByDate = DateTime.Now
                };
                db.Accounts.Add(acc);
                db.SaveChanges();
                return new Response
                {
                    Status = "Success",
                    Message = "Data Success"
                };
            }
            else
            {
                var obj = db.Accounts.Where(x => x.IDUser == acc1.IDUser).ToList().FirstOrDefault();
                if (obj.IDUser > 0)
                {
                    obj.IDRole = acc1.IDRole;
                    obj.IDUser = acc1.IDUser;
                    obj.Email = acc1.Email;
                    obj.Username = acc1.Username;
                    obj.Password = GetMD5(acc1.Password);
                    obj.IDState = acc1.IDState;
                    obj.Phone = acc1.Phone;
                    obj.FullName = acc1.FullName;
                    obj.Image = acc1.Image;
                    obj.Sex = acc1.Sex;
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
        [Route("XemDanhSachTaiKhoan")]
        [HttpGet]
        public object XemDanhSachTaiKhoan()
        {
            
                var a = (from acc in db.Accounts
                         from quyen in db.Roles
                         where quyen.IDRole == acc.IDRole

                         select new
                         {
                             acc.IDUser,
                             acc.FullName,
                             quyen.RoleName,
                             acc.Username,
                             acc.Password,
                             acc.Email,
                             acc.CreatedByDate,
                             acc.IDState,
                             acc.Phone,
                             acc.Image,
                             acc.Sex
                         }).ToList();
                return a;
            
                
        }
        // Xóa tài khoản
        [Route("XoaTaiKhoan")]
        [HttpDelete]
        public object XoaTaiKhoan(int iduser)
        {
            var obj = db.Accounts.Where(x => x.IDUser == iduser).ToList().FirstOrDefault();
            db.Accounts.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }

        // GetByStateUser
        [Route("GetByStateUser")]
        [HttpGet]
        public object GetByStateUser(int IDState)
        {
            var obj = db.Accounts.Where(x => x.IDState == IDState).ToList().ToList();
            return obj;
        }

        // getbyID tài khoản
        [Route("GetByIdTaiKhoan")]
        [HttpGet]
        public object GetbyidTaiKhoan(int iduser)
        {
            var obj = db.Accounts.Where(x => x.IDUser == iduser).ToList().FirstOrDefault();
            return obj;
        }


        //--Trạng thái-----
        //---Thêm Trạng thái-----------
        [Route("AddState")]
        [HttpPost]
        public object AddState(State1 state1)
        {
            if (state1.Id == 0)
            {
                State state = new State
                {
                    StateName = state1.StateName
                };
                db.States.Add(state);
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
        // Xem danh sách state
        [Route("ViewAllState")]
        [HttpGet]
        public object ViewAllState()
        {
            var result = db.States.ToList();
            return result;
        }
        // Xóa State
        [Route("DeleteState")]
        [HttpDelete]
        public object DeleteState(int IDState)
        {
            var obj = db.States.Where(x => x.Id == IDState).ToList().FirstOrDefault();
            db.States.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        [Route("GetByIdState")]
        [HttpGet]
        public object GetByIdState(int IdState)
        {
            var obj = db.States.Where(x => x.Id == IdState).ToList().FirstOrDefault();
            return obj;
        }


        //--Quyền-----
        //---Thêm quyền-----------
        [Route("ThemQuyen")]
        [HttpPost]
        public object ThemQuyen(Role1 quyen)
        {
            if (quyen.IDRole == 0)
            {
                Role role = new Role();
                role.IDRole = quyen.IDRole;
                role.RoleName = quyen.RoleName;
                db.Roles.Add(role);
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
        // Xem danh sách quyền
        [Route("XemDanhSachQuyen")]
        [HttpGet]
        public object xemDanhSachQuyen()
        {
            var a = db.Roles.ToList();
            return a;
        }
        // Xóa quyền
        [Route("XoaQuyen")]
        [HttpDelete]
        public object xoaQuyen(int idrole)
        {
            var obj = db.Roles.Where(x => x.IDRole == idrole).ToList().FirstOrDefault();
            db.Roles.Remove(obj);
            db.SaveChanges();
            return new Response
            {
                Status = "Delete",
                Message = "Delete Successfuly"
            };
        }
        // getbyID quyền
        [Route("GetByIdQuyen")]
        [HttpGet]
        public object getbyIdQuyen(int idrole)
        {
            var obj = db.Roles.Where(x => x.IDRole == idrole).ToList().FirstOrDefault();
            return obj;
        }
        
    }
}