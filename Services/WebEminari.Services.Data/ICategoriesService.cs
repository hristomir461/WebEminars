using System;
using System.Collections.Generic;
using System.Text;

namespace WebEminari.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();
    }
}
