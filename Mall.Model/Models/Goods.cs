using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        /// <summary>
        /// 颜色
        /// </summary>    
        [Display(Name = "Color")]
        public int Color { get; set; }

        /// <summary>
        /// 价格
        /// </summary>    
        [Display(Name = "Price")]
        public double Price { get; set; }

        /// <summary>
        /// 价格
        /// </summary>    
        [Display(Name = "Status")]
        public double Status { get; set; }

        /// <summary>
        /// 库存
        /// </summary>    
        [Display(Name = "Stock")]
        public int Stock { get; set; }
    }
}
