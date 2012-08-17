using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AutoMapper;
using MoviesRememberDao.Interface;
using MoviesRememberDB;
using PetaPoco;
using MoviesRememberDomain;

namespace MoviesRememberDao
{
    public class UserMovieDAO : AbstractUserMovieDAO
    {
        public UserMovieDAO(PetaPoco.Database db)
            : base(db)
        {
        }

        public override user_movie Fetch(Guid id)
        {
            var fetchSql = PetaPoco.Sql.Builder.Select("*").From("user_movie").Where("user_movie_id = @0", id);
            return _db.SingleOrDefault<user_movie>(fetchSql);
        }


        public override TinyUserMovieList GetByUserId(Guid userId, int numPage)
        {
            var fetchSql = PetaPoco.Sql.Builder.Select("*").From("user_movie").Where("user_movie_user_id = @0", userId);
            TinyUserMovieList result = new TinyUserMovieList();
            Page<user_movie> dbResult = _db.Page<user_movie>(numPage, MovieCountByPage, fetchSql);
            result.TinyUserMovies.CurrentPage = numPage;
            result.TinyUserMovies.EntityList = Mapper.Map<IList<user_movie>, IList<UserMovie>>(dbResult.Items);
            result.TinyUserMovies.TotalPage = (int)dbResult.TotalPages;
            result.TinyUserMovies.TotalResult = (int)dbResult.TotalItems;

            return result;
        }

        public override void DeleteById(long uid)
        {
            _db.Execute("DELETE FROM user_movie WHERE user_movie_id = @0", uid);
        }


    }
}
