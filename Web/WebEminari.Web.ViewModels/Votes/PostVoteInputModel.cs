using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebEminari.Web.ViewModels.Votes
{
    public class PostVoteInputModel
    {
        public int WebEminarId { get; set; }

        [Range(1, 5)]
        public byte Value { get; set; }
    }
}
