using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Contato
    {
        //Chave primaria do projeto
        [Key]
        public int Id { get; set; }

        //Torna o campo obrigatorio e delimita o campo
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Nome { get; set; }

        //Torna o campo obrigatorio e delimita o campo
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        public string Sobrenome { get; set; }

        //Torna o campo obrigatorio e permite apenas idade a partir dos 18 anos
        [Required(ErrorMessage = "{0} é obrigatório")]
        [Range(18, int.MaxValue, ErrorMessage = "Idade inválida")]
        public int Idade { get; set; }

        //Torna o campo obrigatório
        [Required(ErrorMessage = "{0} é obrigatório")]
        public string Telefone { get; set; }

        [Display(Name = "Tipo Telefone")]
        [Required(ErrorMessage = "{0} é obrigatório")]
        public TipoTelefone TipoTelefone { get; set; }

        public string Foto { get; set; }

        public List<Email> Emails { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
    
    public enum TipoTelefone
    {
        Pessoal, Comercial
    }
}
