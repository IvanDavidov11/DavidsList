﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DavidsList.Models.ViewModels
{
    public class SearchResultsViewModel
    {
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string MoviePath { get; set; }
        public int Year { get; set; }
    }
}