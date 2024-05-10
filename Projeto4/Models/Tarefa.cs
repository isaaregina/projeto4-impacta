using System.ComponentModel.DataAnnotations;

namespace Projeto4.Models
{
    public class Tarefa
    {
        public int TarefaId { get; set; }

        [Required]
        //[StringLength(30, MinimumLength = 2, ErrorMessage = "No máximo 30 caracteres permitidos e no mínimo 2.")]
        [Display(Name = "Nome")]
        public string? NomeTarefa { get; set; }

        [Display(Name = "Data de Início")]
        public DateOnly? DataInicio { get; set; }

        [Display(Name = "Data de Conclusão")]
        public DateOnly? DataConclusao {  get; set; }

        [Required]
        public string? Status { get; set; }
    }
}
