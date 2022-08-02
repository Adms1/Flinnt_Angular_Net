using System.ComponentModel.DataAnnotations;

namespace Flinnt.Domain
{
    public partial class Account : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}