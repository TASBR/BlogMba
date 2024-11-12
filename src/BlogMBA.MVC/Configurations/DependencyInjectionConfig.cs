using BlogMBA.Business.Models.Autores;
using BlogMBA.Business.Models.Autors;
using BlogMBA.Business.Models.Comentarios;
using BlogMBA.Business.Models.Posts;
using BlogMBA.Business.Notificacoes;
using BlogMBA.Data.Context;
using BlogMBA.Data.Repository;
using BlogMBA.MVC.Configurations;
using Microsoft.AspNetCore.Identity;

namespace DevIO.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection DependenciesInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<UserManager<IdentityUser>, CustomUserManager>();

            
            services.AddScoped<ICustomUserManager, CustomUserManager>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IComentarioService, ComentarioService>();

            return services;
        }
    }
}