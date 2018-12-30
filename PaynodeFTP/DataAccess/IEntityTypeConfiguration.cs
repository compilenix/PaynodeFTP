using Microsoft.EntityFrameworkCore;

namespace PaynodeFTP.DataAccess
{
    public interface IEntityTypeConfiguration
    {
        void Configure();
        void RegisterEntityOnModel(ModelBuilder builder);
    }
}