using System.ComponentModel.DataAnnotations;

namespace Loja.ViewModels
{
    public class ExtratoCreateViewModel
    {
        [Required]
        public int NumeroExtrato { get; set; }
    }

    // public class CategoryUpdateViewModel
    // {
    //     [Required]
    //     public string NumeroExtrato { get; set; } = string.Empty;
    // }
}
