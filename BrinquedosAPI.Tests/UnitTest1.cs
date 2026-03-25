using Xunit;
using Microsoft.EntityFrameworkCore;
using BrinquedosAPI.Data;
using BrinquedosAPI.Models;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace BrinquedosAPI.Tests
{
    public class BrinquedoTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // banco isolado
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Deve_Criar_Brinquedo()
        {
            var context = GetDbContext();

            var brinquedo = new Brinquedo { Nome_brinquedo = "Boneca" };

            context.Brinquedos.Add(brinquedo);
            await context.SaveChangesAsync();

            var total = await context.Brinquedos.CountAsync();

            Assert.Equal(1, total);
        }

        [Fact]
        public async Task Deve_Listar_Brinquedos()
        {
            var context = GetDbContext();

            context.Brinquedos.Add(new Brinquedo { Nome_brinquedo = "Carrinho" });
            await context.SaveChangesAsync();

            var lista = await context.Brinquedos.ToListAsync();

            Assert.Single(lista);
        }

        [Fact]
        public async Task Deve_Buscar_Por_Id()
        {
            var context = GetDbContext();

            var brinquedo = new Brinquedo { Nome_brinquedo = "Bola" };
            context.Brinquedos.Add(brinquedo);
            await context.SaveChangesAsync();

            var result = await context.Brinquedos.FindAsync(brinquedo.Id_brinquedo);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Deve_Atualizar_Brinquedo()
        {
            var context = GetDbContext();

            var brinquedo = new Brinquedo { Nome_brinquedo = "Boneca" };
            context.Brinquedos.Add(brinquedo);
            await context.SaveChangesAsync();

            brinquedo.Nome_brinquedo = "Boneca Nova";
            await context.SaveChangesAsync();

            var atualizado = await context.Brinquedos.FindAsync(brinquedo.Id_brinquedo);

            Assert.NotNull(atualizado);
            Assert.Equal("Boneca Nova", atualizado!.Nome_brinquedo);
        }

        [Fact]
        public async Task Deve_Deletar_Brinquedo()
        {
            var context = GetDbContext();

            var brinquedo = new Brinquedo { Nome_brinquedo = "Carrinho" };
            context.Brinquedos.Add(brinquedo);
            await context.SaveChangesAsync();

            context.Brinquedos.Remove(brinquedo);
            await context.SaveChangesAsync();

            var lista = await context.Brinquedos.ToListAsync();

            Assert.Empty(lista);
        }
    }
}