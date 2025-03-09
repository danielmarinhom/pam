using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rpgapi.Models;
using rpgapi.Models.Enuns;

namespace rpgapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
        private static List<Personagem> personagens = new List<Personagem>()
        {
            //Colar os objetos da lista do chat aqui
            new Personagem() { Id = 1, Nome = "Frodo", Hp=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", Hp=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", Hp=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", Hp=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", Hp=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", Hp=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", Hp=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };
        //a
        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
            Personagem? personagem = personagens.Find(p => p.Nome == nome);
            if(personagem != null)
                return Ok(personagem);
            else
                return NotFound("Personagem com nome nao encontrado");
        }
        //b
        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> personagensOrdenados = personagens
            .Where(p => p.Classe != ClasseEnum.Cavaleiro)
            .OrderByDescending(p => p.Hp)
            .ToList();

            return Ok(personagensOrdenados);
        }
        //c
        [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int inteligencia = personagens.Sum(p => p.Inteligencia);
            int quantidade = personagens.Count;
            return Ok(new { InteligenciaTotal = inteligencia, QuantidadePersonagens = quantidade});
        }
        //d
        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem model)    
        {
            if (model.Defesa < 10 || model.Inteligencia > 30)
                return BadRequest("Personagem Invalido");
            return Ok();
        }
        //e
        [HttpPost("PostValidacaoMago")]
        public IActionResult PostValidacaoMago(Personagem model)    
        {
            if (model.Classe != ClasseEnum.Mago || model.Inteligencia < 35)
                return BadRequest("Mago Invalido");
            return Ok();
        }
        //f
        [HttpGet("GetByClasse/{id}")]
        public IActionResult GetByClasse(int id)
        {
            ClasseEnum classe = (ClasseEnum)id;
            List<Personagem>? personagens_classe = personagens.FindAll(p => p.Classe == classe);
            if(personagens_classe.Any())
                return Ok(personagens_classe);
            else
                return BadRequest("Nao encontrado");
        }
    }
}