﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmdbMovieService.BusinessLayer.Services.IServices
{
    public interface IHttpService
    {
        public Task<string> PostAsync(string url, object requestBody);

        public Task<string> GetAsync(string url);
    }
}
