using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var db = new Data.ApplicationContext();
            //// db.Database.Migrate();
            //var existeMigrationPendente = db.Database.GetPendingMigrations().Any();
            //Console.WriteLine("Curso EFCore!");

            //if (existeMigrationPendente)
            //{
            //    // Regra
            //}
            Console.WriteLine("\nCurso EFCore!");
            //InserirDados();
            //InserirDadosEmMassa();
            ConsultarDados();
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678900",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Carlos",
                CEP = "3158100",
                Cidade = "Belo Horizonte",
                Estado = "MG",
                Telefone = "3198998765"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);
            var registros = db.SaveChanges();
            Console.WriteLine($"\nTotal Registros: {registros}");

        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };
            using var db = new Data.ApplicationContext(); // Todas as interações que ocorrem com o banco é através do contexto
            // 4 opções, as indicadas são as duas primeiras
            // db.Produtos.Add(produto); ou
            // db.Set<Produto>().Add(produto); ou
            // db.Entry(produto).State = EntityState.Added; ou
            db.Add(produto);

            var registro = db.SaveChanges(); // Persisti no banco de dados
            Console.WriteLine($"Total Registro: {registro}");
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            // var consultaPorSintaxe = (from p in db.Produtos where p.Id > 0 select p).ToList();
            // AsNoTracking
            var consultaPorMetodo = db.Produtos
                .Where(p => p.Id > 0)
                .OrderBy(p => p.Id)
                .ToList(); //.AsNoTracking()

            foreach (var produto in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando produto: {produto.Id}");
                // db.Produtos.Find(produto.Id); // Faz a consulta em memória, se não encontrar vai na base de dados
                db.Produtos.FirstOrDefault(p => p.Id == produto.Id); // consulta na base de dados
            }
        }
    }
}
