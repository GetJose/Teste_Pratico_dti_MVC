using Microsoft.EntityFrameworkCore;

namespace Teste_Pratico_dti_MVC.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<DbContext> options): base(options) 
        { }   
    }
}
