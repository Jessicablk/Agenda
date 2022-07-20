using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        //Torna o campo obrigatorio e delimita o campo
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Rua { get; set; }

        [Display (Name = "Número")]
        public double Numero { get; set; }

        //Torna o campo obrigatorio e delimita o campo
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Bairro { get; set; }

        //Torna o campo obrigatorio e delimita o campo
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Cidade { get; set; }

        //Torna o campo obrigatorio e delimita o campo
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Estado { get; set; }

        [Display(Name = "CEP")]
        //Torna o campo obrigatório
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Cep { get; set; }

        [Display(Name = "Tipo Endereço")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public TipoEndereco TipoEndereco { get; set; }

        [Display(Name = "Contato")]
        public int IdContato { get; set; }
        public Contato Contato { get; set; }
        
    }
    public enum TipoEndereco
    {
        Pessoal, Comercial
    }
}
