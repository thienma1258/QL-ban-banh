﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
     public interface IRateReposibitory
    {
        void AddRate(Rating rate);
        List<Rating> getlist();
    }
}
