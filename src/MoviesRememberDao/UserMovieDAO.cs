using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MoviesRememberDao.Interface;
using MoviesRememberDB;

namespace MoviesRememberDao
{
    public class UserMovieDAO : AbstractUserMovieDAO
    {
        public UserMovieDAO(PetaPoco.Database db)
            :base(db)
        {
        }

        public override user_movie Fetch(Guid id)
        {
            var fetchSql = PetaPoco.Sql.Builder.Select("*").From("user_movie").Where("user_movie_id = @0", id);
            return _db.SingleOrDefault<user_movie>(fetchSql);
        }


        public override IList<user_movie> GetByUserId(Guid userId, int numPage)
        {
            var fetchSql = PetaPoco.Sql.Builder.Select("*").From("user_movie").Where("user_movie_user_id = @0", userId);
            return _db.Page<user_movie>(numPage, _movieCountByPage , fetchSql).Items;
        }

        public override void DeleteById(long uid)
        {
            _db.Execute("DELETE FROM user_movie WHERE user_movie_id = @0",uid);
        }


    }
}
