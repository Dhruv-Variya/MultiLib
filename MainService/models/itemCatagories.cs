﻿using System.ComponentModel.DataAnnotations;

namespace MainService.models
{
    public class itemCatagories
    {
        [Key]
        public int id { get; set; }
        public string? itemCode { get; set; }

        public int? catagoryId { get; set; }
    }
}
