using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDao.Interface;

namespace MoviesRememberDao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PetaPoco.Database _db;

        public UnitOfWork(PetaPoco.Database db)
        {
            _db = db;
        }
        public void StartNew()
        {
            _db.BeginTransaction();
        }

        public void Commit()
        {
            _db.CompleteTransaction();
        }

        public void Rollback()
        {
            _db.AbortTransaction();
        }
    }
}
