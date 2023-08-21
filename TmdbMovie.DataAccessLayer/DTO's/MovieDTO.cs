using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovie.DataAccessLayer.DTO_s
{
    public class MovieDTO
    {
        public int page { get; set; }
        public Result[] results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }

        public class Result
        {
            public System.Guid RowID { get; set; }
            public bool? adult { get; set; }
            public string backdrop_path { get; set; } = string.Empty;
            public int? genre_id { get; set; } = 0;
            public int? id { get; set; }
            public string original_language { get; set; } = string.Empty;   
            public string original_title { get; set; } = string.Empty;
            public string overview { get; set; } = string.Empty;
            public float? popularity { get; set; }
            public string poster_path { get; set; } = string.Empty;
            public string release_date { get; set; } = string.Empty;
            public string title { get; set; } = string.Empty;
            public bool? video { get; set; }
            public float? vote_average { get; set; }
            public int? vote_count { get; set; }
        }
    }
}
