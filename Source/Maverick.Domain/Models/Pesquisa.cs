using System.ComponentModel.DataAnnotations;

namespace Maverick.Domain.Models
{
    public class Pesquisa
    {
        /// <summary>
        /// Termo a ser pesquisado.
        /// </summary>
        //[Display(Name = "teste")]
        [Required(ErrorMessage = "O campo {0} é invalido" )]
        public string TermoPesquisa { get; set; }

        /// <summary>
        /// Ano de lançamento.
        /// </summary>
        public int? AnoLancamento { get; set; }
    }
}
