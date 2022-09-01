﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAndReporting.Models
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public List<MenuItem> Items { get; set; }
    }
}