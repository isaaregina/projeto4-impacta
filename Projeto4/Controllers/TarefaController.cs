using Microsoft.AspNetCore.Mvc;
using Projeto4.Models;

namespace Projeto4.Controllers
{
    public class TarefaController : Controller
    {
        private static List<Tarefa> _tarefas = new List<Tarefa>
        {
            new Tarefa { TarefaId=1, NomeTarefa="Varrer a casa", DataInicio=DateOnly.FromDateTime(DateTime.Now), Status="Por Fazer" },
            new Tarefa { TarefaId=2, NomeTarefa="Lavar os pratos", DataInicio=DateOnly.FromDateTime(DateTime.Now), Status="Por Fazer" },
            new Tarefa { TarefaId=3, NomeTarefa="Estudar", DataInicio=DateOnly.FromDateTime(DateTime.Now), Status="Por Fazer" },
        };

        public IActionResult Index()
        {
            return View(_tarefas);
        }

        [HttpGet] // Anotação de PEGAR
        public IActionResult Criar() //chama o form de cadastro
        {
            return View();
        }

        [HttpPost] // Anotação de ENVIAR
        public IActionResult Criar(Tarefa tarefa) //recebe os dados do form
        {
            if (ModelState.IsValid)
            {
                tarefa.TarefaId = _tarefas.Count > 0 ? _tarefas.Max(c => c.TarefaId) + 1 : 1;
                _tarefas.Add(tarefa);
            }
            //return View(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Deletar(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(c => c.TarefaId == id);
            if (tarefa == null) return NotFound();

            _tarefas.Remove(tarefa);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(c => c.TarefaId == id);
            if (tarefa == null) return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                var existingTarefa = _tarefas.FirstOrDefault(c => c.TarefaId == tarefa.TarefaId);
                if (existingTarefa != null)
                {
                    existingTarefa.NomeTarefa = tarefa.NomeTarefa;
                    existingTarefa.Status = tarefa.Status;

                    // Define a data de início apenas se o status for "Por Fazer"
                    existingTarefa.DataInicio = existingTarefa.Status == "Por Fazer" ? DateOnly.FromDateTime(DateTime.Now) : existingTarefa.DataInicio;
                    existingTarefa.DataConclusao = existingTarefa.Status == "Concluído" ? DateOnly.FromDateTime(DateTime.Now) : existingTarefa.DataConclusao;

                }
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        public IActionResult Detalhes(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(c => c.TarefaId == id);
            if (tarefa == null) return NotFound();

            return View(tarefa);
        }

        public IActionResult TarefaFazer()
        {
            var tarefasPorFazer = _tarefas.Where(t => t.Status == "Por Fazer").ToList();
            return View(tarefasPorFazer);
        }

        public IActionResult TarefaConcluida()
        {
            var tarefasPorFazer = _tarefas.Where(t => t.Status == "Concluído").ToList();
            return View(tarefasPorFazer);
        }
    }
}
