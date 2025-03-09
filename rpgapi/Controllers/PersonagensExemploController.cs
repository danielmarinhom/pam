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
    public class PersonagensExemploController : ControllerBase
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
        [HttpGet("GetFirst")]
        public IActionResult GetFirst()
        {
            //Personagem p = personagens[0];
            return Ok(personagens[0]);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //List<Personagem> p = personagens;
            return Ok(personagens);
        }
        [HttpPost("AddPersonagem")]
        public IActionResult AddPersonagem(Personagem model)    
        {
            if (model.Inteligencia == 0)
                return BadRequest("Inteligência não pode ter o valor igual a 0 (zero).");
            personagens.Add(model);
            return Ok(personagens);
        }
        [HttpDelete("DeletePersonagem/{id}")]
        public IActionResult DeletePersonagemFor(int id){
            for(int i = 0; i < personagens.Count; ++i){
                Personagem personagem = personagens[i];
                if(personagem.Id == id){
                    personagens.RemoveAt(i);
                    return Ok(personagens);
                }
            }
            return BadRequest("ID INVALIDO");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            personagens.RemoveAll(pers => pers.Id == id);
            return Ok(personagens);
        }
        [HttpPut("UpdatePersonagem")]
        public IActionResult Update(Personagem model){
            Personagem? atual = personagens.Find(atual => atual.Id == model.Id);
            if(atual != null){
                atual.Classe = model.Classe;
                atual.Defesa = model.Defesa;
                atual.Forca = model.Forca;
                atual.Hp = model.Hp;
                atual.Inteligencia = model.Inteligencia;
                atual.Nome = model.Nome;
                return Ok(personagens);
            }else{
                return BadRequest("PERSONAGEM NAO ENCONTRADO");
            }
        }
        [HttpGet("GetRemovendoMago")]
        public IActionResult GetRemovendoMagos(){
            Personagem? pRemove = personagens.Find(p => p.Classe == ClasseEnum.Mago);
            personagens.Remove(pRemove);
            return Ok("Personagem removido: " +pRemove.Nome);
        }
        [HttpGet("GetByForca/{forca}")]
        public IActionResult Get(int forca){
            List<Personagem> listaFinal = personagens.FindAll(p => p.Forca == forca);
            return Ok(listaFinal);
        }
        //atividade
        

    }
}