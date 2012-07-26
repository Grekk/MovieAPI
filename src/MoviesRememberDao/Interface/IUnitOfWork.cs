using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoviesRememberDao.Interface
{
    public interface IUnitOfWork
    {
        void StartNew();
        void Commit();
        void Rollback();
    }
}
