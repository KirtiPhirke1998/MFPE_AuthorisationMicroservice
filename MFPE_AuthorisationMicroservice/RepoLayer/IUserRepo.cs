using MFPE_AuthorisationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFPE_AuthorisationMicroservice.RepoLayer
{
    public interface IUserRepo
    {
        UserDetails GetUserDetails(UserDetails user);



    }
}
