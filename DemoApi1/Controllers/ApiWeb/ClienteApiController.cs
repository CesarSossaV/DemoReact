using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using DemoApi1.Models;

namespace DemoApi1.Controllers.ApiWeb
{
    public class ClienteApiController : ApiController
    {
        //DemoEntities1 context = new DemoEntities1();
        
        [HttpGet]
        [Route("api/cliente/lista")]
        public IHttpActionResult listaCliente()
        {
            try
            {
                using (DemoEntities context = new DemoEntities())
                {
                    context.Database.Connection.Open();
                    var lista = (from x in context.Clientes
                                 select x).ToList();
                    return Json(new { Data = new { Success = true, Data = lista } });
                }
            }
                    
            catch (Exception e)
            {
                return Json(new { Data = new { Success = false, Data = "", Menssage = e.Message } });
            }
        }
        [HttpPost]
        [Route("api/cliente/registro")]
        public IHttpActionResult registro([FromBody] Clientes clienteData)
        {
            try
            {
                DemoEntities context = new DemoEntities();
                var listaCliente = (from x in context.Clientes
                                    select x).ToList();
                foreach (var cliente in listaCliente) { 
                    if(cliente.ci == clienteData.ci)
                    {
                        return Json(new { Data = new { Success = false, Data = "", Menssage = "El Cliente ya existe" } });
                    }
                }
                Clientes data = new Clientes();
                data.ci = clienteData.ci;
                data.nombre = clienteData.nombre;
                data.apellidos = clienteData.apellidos;
                data.edad = clienteData.edad;
                data.Nacionalidad = clienteData.Nacionalidad;
                data.estado = 1;
                context.Clientes.Add(data);
                context.SaveChanges();

                return Json(new { Data = new { Success = true, Data="" ,Menssage = "Se registro correctamente" } });

            }
            catch (Exception e)
            {
                return Json(new { Data = new { Success = false, Data = "", Menssage = e.Message } });
            }
        }
        [HttpPost]
        [Route("api/cliente/actulizar")]
        public IHttpActionResult actulizacion([FromBody] Clientes clienteData)
        {
            try
            {
                DemoEntities context = new DemoEntities();
                Clientes data = (from x in context.Clientes
                                    where x.idCliente == clienteData.idCliente
                                    select x).First();
                /*foreach (var cliente in listaCliente)
                {
                    if (cliente.ci == clienteData.ci)
                    {
                        return Json(new { Data = new { Success = false, Data = "", Message = "El Cliente ya existe" } });
                    }
                }*/
                //Clientes data = cliente;
                data.ci = clienteData.ci;
                data.nombre = clienteData.nombre;
                data.apellidos = clienteData.apellidos;
                data.edad = clienteData.edad;
                data.Nacionalidad = clienteData.Nacionalidad;
                
                context.SaveChanges();

                return Json(new { Data = new { Success = true, Data = "Actualizado correctamente" } });

            }
            catch (Exception e)
            {
                return Json(new { Data = new { Success = false, Data = "", Menssage = e.Message } });
            }
        }
        [HttpPost]
        [Route("api/cliente/deshabilitar")]
        public IHttpActionResult deshabilitarEstado([FromBody] Clientes clienteData)
        {
            try
            {
                DemoEntities context = new DemoEntities();
                Clientes data = (from x in context.Clientes
                                 where x.idCliente == clienteData.idCliente
                                 select x).First();
                data.estado = 0;
                context.SaveChanges();
                return Json(new { Data = new { Success = true, Data = "", Menssage = "Deshabilitado" } });
            }
            catch (Exception e)
            {
                return Json(new { Data = new { Success = false, Data = "", Menssage = e.Message } });
            }
        }
        [HttpPost]
        [Route("api/cliente/habilitar")]
        public IHttpActionResult habilitarEstado([FromBody] Clientes clienteData)
        {
            try
            {
                DemoEntities context = new DemoEntities();
                Clientes data = (from x in context.Clientes
                                 where x.idCliente == clienteData.idCliente
                                 select x).First();
                data.estado = 1;
                context.SaveChanges();
                return Json(new { Data = new { Success = true, Data="", Menssage = "habilitado" } });
            }
            catch (Exception e)
            {
                return Json(new { Data = new { Success = false, Data = "", Menssage = e.Message } });
            }
        }
    }
}