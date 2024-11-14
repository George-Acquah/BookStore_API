using BookStore.Application.Interfaces;
using BookStore.Application.Interfaces.Services;
using BookStore.Application.Services;
using BookStore.Infrastructure.DB;
using BookStore.Infrastructure.Helpers;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Extensions
{
    public static class ServiceCollection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BookStoreDB");
            services.AddDbContext<BookStoreAPIDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IFilesRepository, FileRepository>();
            services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            services.AddScoped<IBooksRepository, BookRepository>();


            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IBookCategoryService, BookCategoryService>();
            services.AddScoped<IBookService, BookService>();

            services.AddSingleton<CryptographicHelpers>();
        }
    }
}
