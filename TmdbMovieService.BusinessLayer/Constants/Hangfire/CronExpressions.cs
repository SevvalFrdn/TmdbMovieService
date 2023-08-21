using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovieService.BusinessLayer.Constants.Hangfire
{
    public class CronExpressions
    {
        public const string Every_Minute = "* * * * *";
        public const string Every_23_30 = "30 23 * * *";
        public const string hour = "30 23 * * *";
    }
}
