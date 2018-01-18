using System.Collections.Generic;
using System.Linq;
using Vizitkartes.API.Entities;

namespace Vizitkartes.API.Services
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly VizitkartesContext _dbContext;

        public ManagerRepository(VizitkartesContext context)
        {
            _dbContext = context;
        }
        
        public void ApproveEmployee(VizitkartesUser user)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveEmployee(VizitkartesUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}