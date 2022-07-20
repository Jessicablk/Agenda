using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaTelefonica.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }

        //Torna o campo obrigatorio, delimita o campo e mostra que o campo é para um e-mail
        [Required(ErrorMessage = "{0} é obrigatório")]
        [StringLength(40, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "Endereço de E-mail")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string EnderecoEmail { get; set; }
        public int IdContato { get; set; }
        public Contato Contato { get; set; }
    }
}
