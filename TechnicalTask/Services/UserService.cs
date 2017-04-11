using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class UserService : Service<User>
    {
        public UserService(IRepository<User> userRepository) : base(userRepository)
        {
        }        
    }
}
