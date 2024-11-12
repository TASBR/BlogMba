using BlogMBA.Business.Models.Autores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BlogMBA.MVC.Configurations
{
    public class CustomUserManager : UserManager<IdentityUser>, ICustomUserManager
    {
        private readonly IAutorService _autorService;
        public CustomUserManager(
        IUserStore<IdentityUser> store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher,
        IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<UserManager<IdentityUser>> logger,
        IAutorService autorService)
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _autorService = autorService;
        }
        
        public override async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            var result = await base.CreateAsync(user, password);

            if (result.Succeeded)
            {
                Autor autor = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse(user.Id)
                };
                await _autorService.Adicionar(autor);
            }

            return result;
        }

        public async Task<IdentityUser> GetIdentityUserByName(string name)
        {
            return await base.FindByNameAsync(name);
        }
    }

    public interface ICustomUserManager
    {
        Task<IdentityUser> GetIdentityUserByName(string name);
    }
}
