﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paksell
{
    public class CityArea : INameEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual User? User { get; set; }

    }
}
