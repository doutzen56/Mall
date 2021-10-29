using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall.Model.Models
{
    public class Order
    {
        /// <summary>
        /// Id
        /// </summary>    
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>    
        [Display(Name = "UserId")]
        public int UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>    
        [Display(Name = "Status")]
        public int Status { get; set; }
        /// <summary>
        /// 支付类型
        /// </summary>    
        [Display(Name = "PaymentType")]
        public int PaymentType { get; set; }
        /// <summary>
        /// 总计支付
        /// </summary>    
        [Display(Name = "TotalPay")]
        public double TotalPay { get; set; }
        /// <summary>
        /// 实际支付
        /// </summary>    
        [Display(Name = "ActualPay")]
        public double ActualPay { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>    
        [Display(Name = "CreateTime")]
        public DateTime CreateTime { get; set; }

    }
}
