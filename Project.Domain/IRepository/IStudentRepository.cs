using AppShared.Repositories;
using Project.Domain.Entity;

namespace Project.Domain.Infastructure
{
    public interface IStudentRepository : IRepository<StudentEntity, long>
    {
    }
}
