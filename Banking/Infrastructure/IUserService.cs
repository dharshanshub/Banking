
using Banking.Models;
using System.Collections.Generic;

namespace Banking.Infrastructure
{
    public interface IUserService
    {

        List<User> Authenticate(LoginViewModel model);
        bool TransactionAuthenticate(Customer customer);

        User LoggedInUser { get; }
    }
        
}
