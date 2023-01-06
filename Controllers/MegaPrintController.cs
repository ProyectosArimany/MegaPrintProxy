using System;
using Microsoft.AspNetCore.Mvc;
using MegaPrintProxy.Services;
using MegaPrintProxy.Models;

namespace MegaPrintProxy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MegaPrintController : ControllerBase
    {
        [HttpPost,Route("Service")]
        public EstadoServicio MegaPrintService(ServiceParams parametros)
        {
            WebClient webclient = new WebClient();
            EstadoServicio status = webclient.Post(parametros);
            return status;
        }
        
    }
}
