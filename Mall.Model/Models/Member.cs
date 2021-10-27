using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mall.Model.Models
{
    public class Member
    {
        /// <summary>
        /// Id
        /// </summary>    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>    
        [Display(Name = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>    
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>    
        [Display(Name = "Nickname")]
        public string Nickname { get; set; }
    }
}
