using Dual.Server.Dtos;
using Eszi.Demo.Database.Models;
using Mapster;

namespace Dual.Server
{
    public static class MapsterConfigurator
    {
        public static void Configure()
        {
            TypeAdapterConfig<ProductDto, Product>
                .NewConfig()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.Name, s => s.Name)
                .Map(d => d.Description, s => s.Description)
                .Map(d => d.Price, s => s.Price);

            TypeAdapterConfig<Product, ProductDto>
                .NewConfig()
                .Map(d => d.Id, s => s.Id)
                .Map(d => d.Name, s => s.Name)
                .Map(d => d.Description, s => s.Description)
                .Map(d => d.Price, s => s.Price);
        }
    }
}
