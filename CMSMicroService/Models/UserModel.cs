using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSMicroService.Models
{
    public class UserModel
    {
        [Key]
        public int userId { get; set; }

        public string username { get; set; }
    }
}
