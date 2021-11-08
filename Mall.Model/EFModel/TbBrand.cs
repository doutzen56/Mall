using System;
using System.Collections.Generic;

namespace Mall.Model.EFModel
{
    public partial class TbBrand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Letter { get; set; }
    }
}
