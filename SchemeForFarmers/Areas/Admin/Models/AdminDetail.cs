﻿using System;
using System.Collections.Generic;

namespace SchemeForFarmers.Areas.Admin.Models
{
    public partial class AdminDetail
    {
        public int UsertypeId { get; set; }
        public int AdminId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
