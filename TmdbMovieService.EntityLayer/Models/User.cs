using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovieService.EntityLayer.Models
{
    public class User
    {
        [Key]
        public Guid RowId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
