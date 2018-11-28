using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamPlayerRecords.Models
{
    public interface ITeamMock
    {
        IQueryable<Team> Teams { get; }

        Team Save(Team team);
        void Delete(Team team);
    }
}
