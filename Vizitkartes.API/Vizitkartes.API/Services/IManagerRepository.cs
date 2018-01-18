using System.Collections.Generic;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public interface IManagerRepository
    {
        void ApproveEmployee(VizitkartesUser user);
        void RemoveEmployee(VizitkartesUser user);

    }
}