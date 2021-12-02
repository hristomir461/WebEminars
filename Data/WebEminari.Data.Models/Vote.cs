using System;
using System.Collections.Generic;
using System.Text;

using WebEminari.Data.Common.Models;

namespace WebEminari.Data.Models
{
    public class Vote : BaseModel<int>
    {
    public int WebEminarId { get; set; }

    public virtual WebEminar WebEminar { get; set; }

    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public byte Value { get; set; }

    }
}
