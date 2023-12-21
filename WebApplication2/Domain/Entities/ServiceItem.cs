using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Domain.Entities
{
    public class ServiceItem: EntityBase
    {

        [Required(ErrorMessage = "Заполните заголовок новости")]
        [Display(Name = "Заголовок новости")]
        public override string Title { get; set; }

        [Display(Name = "Полное описание новости")]
        public override string Text { get; set; }
    }
}
