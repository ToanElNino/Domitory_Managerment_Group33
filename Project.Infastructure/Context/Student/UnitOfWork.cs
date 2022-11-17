using App.Shared.Uow;

namespace Project.Infastructure.Context.Student
{
    public class UnitOfWork : UnitOfWorkBase<StudentContext>, IMaxUnitOfWork
    {
        public UnitOfWork(StudentContext modelContext) : base(modelContext)
        {
        }
    }
}
