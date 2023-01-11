using GameStore.Domain.Identity;
using GameStore.Domain.Infrastructure;
using GameStore.WebUI.Areas.Admin.Models;
using GameStore.WebUI.Areas.Admin.Models.DTO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GameStore.WebUI.Apis
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseApiController
    {
        // GET api/<controller>
        public List<RoleDTO> Get()
        {
            if (HttpContext.Current.Cache["RoleList"] != null)
            {
                return (List<RoleDTO>)HttpContext.Current.Cache["RoleList"];
            }
            else
            {
                List<RoleDTO> roles = RoleManager.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name, Description = r.Description }).ToList();
                HttpContext.Current.Cache["RoleList"] = roles;
                return roles;
            }
        }

        // GET api/<controller>/5
        public RoleDTO Get(string id)
        {
            if (HttpContext.Current.Cache["Role" + id] != null)
            {
                return (RoleDTO)HttpContext.Current.Cache["Role" + id];
            }
            else
            {
                AppRole r = RoleManager.FindById(id);
                RoleDTO role = new RoleDTO { Id = r.Id, Name = r.Name, Description = r.Description };
                HttpContext.Current.Cache["Role" + id] = role;
                return role;
            }            
        }

        // GET: api/Category/GetCount/
        [Route("api/Role/GetCount")]
        public int GetCount()
        {
            if (HttpContext.Current.Cache["RoleList"] != null)
            {
                List<RoleDTO> list = (List<RoleDTO>)HttpContext.Current.Cache["RoleList"];
                return list.Count();
            }
            else
            {
                List<RoleDTO> roles = RoleManager.Roles.Select(r => new RoleDTO { Id = r.Id, Name = r.Name, Description = r.Description }).ToList();
                HttpContext.Current.Cache["RoleList"] = roles;
                return roles.Count();
            }
        }

        [Route("api/Role/Create")]
        public HttpResponseMessage Create([FromBody]RoleViewModel value)
        {
            if (ModelState.IsValid)
            {
                AppRole existRole = RoleManager.FindByName(value.Name);
                if (existRole != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Роль [" + value.Name + "] уже существует, попробуйте другое имя!");
                }

                AppRole role = new AppRole();
                role.Name = value.Name;
                role.Description = value.Description;
                IdentityResult result = RoleManager.Create(role);
                if (result.Succeeded)
                {
                    HttpContext.Current.Cache.Remove("RoleList");
                    return Request.CreateResponse(HttpStatusCode.OK, "Okay");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, GetErrorMessage(result));
                }                
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ModelState.IsValid=false");
            }
        }
        public HttpResponseMessage Post([FromBody]RoleViewModel value)
        {
            if (ModelState.IsValid)
            {
                AppRole role = RoleManager.FindById(value.Id);
                if (role == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Роль [" + value.Id + "] не существует!");
                }
                role.Name = value.Name;
                role.Description = value.Description;
                IdentityResult result = RoleManager.Update(role);
                if (result.Succeeded)
                {
                    HttpContext.Current.Cache.Remove("RoleList");
                    HttpContext.Current.Cache.Remove("Role" + role.Id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Okay");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, GetErrorMessage(result));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ModelState.IsValid=false");
            }            
        }
        
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(string id)
        {
            AppRole role = RoleManager.FindById(id);
            if (role == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Роль ["+id+"] не найдена.");
            }
            else if (role.Users.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Пользователи с такой ролью уже существуют [" + role.Name + "], сначала удалите их!");
            }
            else
            {
                IdentityResult result = RoleManager.Delete(role);
                if (result.Succeeded)
                {
                    HttpContext.Current.Cache.Remove("RoleList");
                    HttpContext.Current.Cache.Remove("Role" + id);
                    return Request.CreateResponse(HttpStatusCode.OK, "Okay");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, GetErrorMessage(result));
                }
            }            
        }
    }
}
