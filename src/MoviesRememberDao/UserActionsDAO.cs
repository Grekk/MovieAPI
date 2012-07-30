using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDao.Interface;
using MoviesRememberDomain;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using StructureMap;
using Action = MoviesRememberDomain.Action;

namespace MoviesRememberDao
{
    public class UserActionsDAO : IUserActionsDAO
    {
        private readonly AbstractUserMovieDAO _userMovieDAO;
        private IRedisClient _redisClient;

        public UserActionsDAO(IRedisClient redisClient, AbstractUserMovieDAO userMovieDAO)
        {
            _userMovieDAO = userMovieDAO;
            _redisClient = redisClient;
        }

        public IList<UserAction> AddActionAtFirstIndex(UserAction action)
        {
            using (IRedisTypedClient<UserAction> redis = _redisClient.GetTypedClient<UserAction>())
            {
                IRedisList<UserAction> userActions = redis.Lists["user:actions"];
                userActions.Enqueue(action);
                return userActions;
            }
        }

        public IList<UserAction> RemoveLastAction()
        {
            using (IRedisTypedClient<UserAction> redis = _redisClient.GetTypedClient<UserAction>())
            {
                IRedisList<UserAction> userActions = redis.Lists["user:actions"];
                userActions.RemoveEnd();
                return userActions;
            }
        }

        public IList<UserAction> GetActions()
        {
            IList<UserAction> userActions = new List<UserAction>();
            UserAction us = new UserAction();
            us.Action = Action.ADD_MOVIE;
            us.MovieId = "22";
            us.MovieName = "dd";
            us.UserName = "dd";
            userActions.Add(us);

            using (IRedisTypedClient<UserAction> redis = _redisClient.GetTypedClient<UserAction>())
            {
                //userActions = redis.Lists["user:actions"];
            }

            return userActions;
        }
    }
}
