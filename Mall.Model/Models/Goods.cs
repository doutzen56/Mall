using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mall.Model.Models
{
    public class Goods
    {
        /// <summary>
        /// Id
        /// </summary>    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>    
        [Display(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>    
        [Display(Name = "Code")]
        public string Code { get; set; }
        
    }
}
