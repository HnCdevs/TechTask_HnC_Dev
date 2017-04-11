using System.ComponentModel.DataAnnotations;

namespace TechnicalTask.Models
{
    public interface IModel
    {
        [Key]
        int Id { get; set; }
    }
}
