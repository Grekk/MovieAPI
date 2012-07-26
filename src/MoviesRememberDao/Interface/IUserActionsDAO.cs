using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoviesRememberDomain;

namespace MoviesRememberDao.Interface
{
    public interface IUserActionsDAO
    {
        IList<UserAction> AddActionAtFirstIndex(UserAction action);

        IList<UserAction> RemoveLastAction();

        IList<UserAction> GetActions();
    }
}
