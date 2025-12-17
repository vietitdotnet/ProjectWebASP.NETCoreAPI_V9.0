using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Models.Requests.Auths
{
    public class ExternalAuthRequest
    {
        public string LoginProvider { get; set; }           // Google, Facebook, Microsoft
        public string ProviderKey { get; set; }     // info.ProviderKey        
        public string ProviderDisplayName { get; set; }
        public string UserName { get; set; }    
        public string Email { get; set; }
        
        public string FullName { get; set; }
    }
}
