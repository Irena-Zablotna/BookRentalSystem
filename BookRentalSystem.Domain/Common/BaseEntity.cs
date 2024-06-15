﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalSystem.Domain.Common
{
   public class BaseEntity:AuditableModel
    {
        public int Id { get; set; }
        public BaseEntity()
        {
            Id = 1;
        }
    }
}
