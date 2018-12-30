using Microsoft.EntityFrameworkCore;

namespace WebApplication2.DataAccess
{
    public interface IEntityTypeConfiguration
    {
        void Configure();
        void RegisterEntityOnModel(ModelBuilder builder);
    }
}