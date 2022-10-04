﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bouvet.AssetHub.API.Domain.Asset.Model
{
    public class CategoryEntity : Entity 
    {
        [Required]
        public string Name { get; set; } = "";
    }
}