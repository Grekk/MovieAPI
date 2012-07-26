using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesRememberDao.Interface
{
    public abstract class AbstractDAO<TEntity>
    {
        protected PetaPoco.Database _db;

        public AbstractDAO(PetaPoco.Database db)
        {
            _db = db;
        }

        public void Insert(TEntity entity)
        {
            try
            {
                _db.BeginTransaction();
                _db.Insert(entity);
                _db.CompleteTransaction();
            }
            catch
            {
                _db.AbortTransaction();
            }

        }

        public void Update(TEntity entity)
        {
            try
            {
                _db.BeginTransaction();
                _db.Update(entity);
                _db.CompleteTransaction();
            }
            catch
            {
                _db.AbortTransaction();
            }
        }

        public void Delete(TEntity entity)
        {
            try
            {
                _db.BeginTransaction();
                _db.Delete(entity);
                _db.CompleteTransaction();
            }
            catch
            {
                _db.AbortTransaction();
            }
        }

        public List<TEntity> FetchAll()
        {
            throw new NotImplementedException();
        }

        public abstract TEntity Fetch(Guid uid);

        public abstract void DeleteById(long uid);
    }
}
