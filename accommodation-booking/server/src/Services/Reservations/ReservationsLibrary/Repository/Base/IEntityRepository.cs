using ReservationsLibrary.Models;
using ReservationsLibrary.Repository.Base;

namespace Reservations.API.Infrasructure.Base
{
    public interface IEntityRepository<TEntity> where TEntity : BaseEntityModel
    {
        IUnitOfWork UnitOfWork { get; }
        public List<TEntity> GetAll();
        public TEntity GetById(Guid id);
        public TEntity Create(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(Guid id);
    }
}
