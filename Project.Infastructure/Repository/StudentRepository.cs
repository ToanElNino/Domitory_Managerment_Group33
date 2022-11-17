using App.Shared.Repositories;
using Project.Domain.Infastructure;
using Project.Domain.Entity;
using Project.Infastructure.Context.Student;

namespace Project.Infastructure.Repositories
{
    public class StudentRepository : RepositoryBase<StudentContext, StudentEntity, long>, IStudentRepository
    {
        public StudentRepository(StudentContext studentContext)
            : base(studentContext)
        {

        }
    }
}
