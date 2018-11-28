using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamPlayerRecords.Models
{
    public class EFTeams : ITeamMock
    {
        private Model1 db = new Model1();

        public IQueryable<Team> Teams { get { return db.Teams; } }


        public void Delete(Team team)
        {
            db.Teams.Remove(team);
            db.SaveChanges();

        }

        public Team Save(Team team)
        {
            if (team.ID == 0)
            {
                db.Teams.Add(team);
            }
            else
            {
                db.Entry(team).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return team;
        }
    }
}