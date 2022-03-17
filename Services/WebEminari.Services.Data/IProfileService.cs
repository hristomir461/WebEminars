using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace WebEminari.Services.Data
{
    public interface IProfileService
    {
         void UploadImage(IFormFile file);
    }
}
