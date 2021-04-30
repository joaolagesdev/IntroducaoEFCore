using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();
            // db.Database.Migrate();
            var existeMigrationPendente = db.Database.GetPendingMigrations().Any();
            Console.WriteLine("Curso EFCore!");

            if (existeMigrationPendente)
            {
                // Regra
            }
        }
    }
}
