﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ressources.Back.Data.Models
{
    public class RessourceModel
    {
        public int Id { get; set; }
        public string Titre { get; set; } = "";
        public int IdCategory { get; set; }
        public int IdUser { get; set; }
    }
}
