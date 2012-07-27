using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MoviesRememberDomain;

namespace MovieAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        void AddMovie(Guid userId, string userName, Movie movie);

        [OperationContract]
        void DeleteMovie(string userName, UserMovie movie);

        [OperationContract]
        void UpdateMovie(UserMovie movie);

        [OperationContract]
        IList<UserMovie> GetUserMovieList(Guid userId);

        [OperationContract]
        IList<UserAction> AddUserAction(UserAction action);

        [OperationContract]
        IList<UserAction> GetUsersActions();

        [OperationContract]
        bool AddNewMember(string email);
    }
}
