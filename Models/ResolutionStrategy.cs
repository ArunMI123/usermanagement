﻿using System;
using System.Collections.Generic;

namespace ARBUserManagement.Models
{
    public partial class ResolutionStrategy
    {
        public int ResolutionStrategyId { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
    }
}
