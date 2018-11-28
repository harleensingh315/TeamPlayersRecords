using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeamPlayerRecords.Controllers;
using TeamPlayerRecords.Models;

namespace TeamPlayerRecords.Tests.Controllers
{
    [TestClass]
    public class TeamControllerTest
    {

        #region Objects Declaration Section
        TeamsController controller;
        List<Team> teams;
        Mock<ITeamMock> mock;
        #endregion

        #region Objects Initialization
        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<ITeamMock>();
            teams = new List<Team>
            {
                new Team{ ID=100, TeamName="Australia", CoachName="David Miller", Ratings=5  },
                new Team{ ID=200, TeamName="India", CoachName="Alex Fin", Ratings=4  }
            };
            mock.Setup(m => m.Teams).Returns(teams.AsQueryable());
            controller = new TeamsController(mock.Object);
        }

        #endregion

        #region Test Case Section For Teams List - GET: /Teams/

        [TestMethod]
        public void IndexReturnsView()
        {
            ViewResult result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsTeams()
        {
            var actual = (List<Team>)((ViewResult)controller.Index()).Model;
            CollectionAssert.AreEqual(teams, actual);
        }

        #endregion

        #region Test Case Section For Team Details - GET: /Teams/Details 
        [TestMethod]
        public void Details_NoId_LoadsError()
        {
            ViewResult result = (ViewResult)controller.Details(null);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Details_InvalidId_LoadsError()
        {
            ViewResult result = (ViewResult)controller.Details(101);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Details_ValidId_LoadsView()
        {
            ViewResult result = (ViewResult)controller.Details(100);
            Assert.AreEqual("Details", result.ViewName);
        }
        [TestMethod]
        public void Details_ValidId_LoadsTeams()
        {
            Team result = (Team)((ViewResult)controller.Details(100)).Model;
            Assert.AreEqual(teams[0], result);
        }
        #endregion


    }
}
