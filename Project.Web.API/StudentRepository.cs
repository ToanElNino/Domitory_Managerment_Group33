using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure;

namespace Project.Infastructure.Context
{
    public class StudentRepository : RepositoryBase<Context, StudentEntity, long>, IStudentRepository
    {
        public StudentRepository(Context studentContext)
            : base(studentContext)
        {

        }
    }
}
