using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            // ConsultarDados();
            // CadastrarPedido();
            // CadastrarPedidoCarregamentoAdiantado();
            // AtualizarDados();
            // RemoverRegistro();
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

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto) // produto está dentro de Itens, por isso thenInclude
                .ToList(); // ou Include("Itens"), inclui a propriedade de navegação
            Console.WriteLine(pedidos.Count);
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.FirstOrDefault(p => p.Id == 1);
            // var cliente = db.Clientes.Find(1); // como você já sabe o ID, pesquise por find
            //cliente.Nome = "Cliente Alterado passo 2";

            var cliente = new Cliente
            {
                Id = 1
            };
            
            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado",
                Telefone = "987786554"
            };

            db.Attach(cliente);
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            // db.Entry(cliente).State = EntityState.Modified;
            // db.Clientes.Update(cliente); Sobrescreve todas as propriedades, mesmo aquelas que não foram alteradas
            // Você pode remover essa linha de código
            db.SaveChanges();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2); // o find utiliza a chave primária da entidade
            // db.Clientes.Remove(cliente); ou
            // db.Remove(cliente); ou 
            // db.Entry(cliente).State = EntityState.Deleted;
            // db.SaveChanges();

            // Forma desconectada
            var cliente = new { id = 3 };
            db.Entry(cliente).State = EntityState.Deleted;
            db.SaveChanges();
        }

    }
}
