using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDB;
using PetaPoco;
using MoviesRememberDomain;

namespace MoviesRememberDao.Interface
{
    public abstract class AbstractUserMovieDAO : AbstractDAO<user_movie>
    {
        public AbstractUserMovieDAO(PetaPoco.Database db)
            :base(db)
        {
        }

        public abstract TinyUserMovieList GetByUserId(Guid userId, int numPage);
    }
}
