using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Nacionalidade { get; set; }
        public string Cep { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail com formato inválido")]  
        public string Email { get; set; }
        public string Telefone { get; set; }
    }
}
