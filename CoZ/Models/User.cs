﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoZ.Models
{
    public class User
    {
        public int Id { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
    }
}