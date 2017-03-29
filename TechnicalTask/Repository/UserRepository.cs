﻿using TechnicalTask.Data;
using TechnicalTask.Models;

namespace TechnicalTask.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(TtContext context) : base(context)
        {
        }

        public override bool IsValid(User item)
        {
            throw new System.NotImplementedException();
        }
    }
}