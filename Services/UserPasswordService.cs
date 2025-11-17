using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zxcvbn;

using Entities;
using Repositories;

namespace Services
{
    public class UserPasswordService : IUserPasswordService
    {
        private readonly IUserPasswordRepository _userPasswordRepository;
        public UserPasswordService(IUserPasswordRepository userPasswordRepository)
        {
            _userPasswordRepository = userPasswordRepository;
        }

        public int CheckPassword(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
    }
}
