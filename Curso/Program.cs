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
            InserirDados();
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

            var registros = db.SaveChanges(); // Persisti no banco de dados
            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}
