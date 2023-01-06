using System;
namespace MegaPrintProxy.Models
{
    public class ServiceParams
    {
        public string url { get; set; }
        public string token { get; set; }
        public string body { get; set; }
    }
    public class EstadoServicio
        {
                public bool statusOk { get; set; }
                public String response { get; set; }
                public String msgerror { get; set; }
        }
}

