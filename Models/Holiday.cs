﻿using System;
using System.Collections.Generic;

namespace ARBUserManagement.Models
{
    public partial class Holiday
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
    }
}
