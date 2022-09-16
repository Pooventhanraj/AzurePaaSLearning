using System.ComponentModel.DataAnnotations;

namespace AzBlob.Models
{
    public class Container
    {
        [Required]
        public string Name { get; set; }
    }
}
