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

        #region Test Cases For Edit Teams - GET & POST 

        #region Test Case Section For GET: /Team/Edit
        [TestMethod]
        public void Edit_NoId()
        {
            int? id = null;
            var result = (ViewResult)controller.Edit(id);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Edit_ViewLoads()
        {
            ViewResult actual = (ViewResult)controller.Edit(100);
            Assert.AreEqual("Edit", actual.ViewName);
        }
        [TestMethod]
        public void Edit_LoadsTeams()
        {
            Team actual = (Team)((ViewResult)controller.Edit(100)).Model;
            Assert.AreEqual(teams[0], actual);
        }
        #endregion

        #region Test Cases For POST: Team/Edit - Saved Edited Team Details
        [TestMethod]
        public void EditPostTeam_LoadsIndex()
        {
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(teams[0]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPost_InvalidLoadView()
        {
            Team id = new Team { ID = 201 };
            controller.ModelState.AddModelError("Error", "It'll Not Save");
            ViewResult result = (ViewResult)controller.Edit(id);
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditPost_InvalidLoadTeam()
        {
            Team id = new Team { ID = 101 };
            controller.ModelState.AddModelError("Error", "It'll Not Save");
            Team result = (Team)((ViewResult)controller.Edit(id)).Model;
            Assert.AreEqual(id, result);
        }
        #endregion

        #endregion

        #region Test Cases For Add New Team Get & POST

        #region Test Cases For GET: /Team/Create - Add New Team
        [TestMethod]
        public void Create_ViewLoads()
        {
            var result = (ViewResult)controller.Create();
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #region Test Cases For POST: /Team/Create - Add New Team POST Save Team Details
        [TestMethod]
        public void Create_ValidTeam()
        {
            Team newTeam = new Team
            {
                ID = 300,
                TeamName = "England",
                CoachName = "Steve Waugh",
                Ratings = 3
            };
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newTeam);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Create_InValidTeam()
        {
            Team newTeam = new Team();
            controller.ModelState.AddModelError("Unable To Create Team", "Team Creation Exception");
            ViewResult result = (ViewResult)controller.Create(newTeam);
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #endregion

        #region Test Cases For Delete Team - Get & Post

        #region Test Cases For GET: /Team/Delete - Delete Team
        [TestMethod]
        public void Delete_NoId()
        {
            var result = (ViewResult)controller.Delete(null);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Delete_InvalidId()
        {
            var result = (ViewResult)controller.Delete(101);
            Assert.AreEqual("Error", result.ViewName);
        }
        [TestMethod]
        public void Delete_ValidId_LoadsView()
        {
            var result = (ViewResult)controller.Delete(100);
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void Delete_ValidId_LoadsModel()
        {
            Team actual = (Team)((ViewResult)controller.Delete(100)).Model;
            Assert.AreEqual(teams[0], actual);
        }
        #endregion

        #region Test Case For POST: /Team/DeleteConfirmed - POST Team Delete Confirmation
        [TestMethod]
        public void Delete_Confirmed_NoId()
        {
            ViewResult result = (ViewResult)controller.DeleteConfirmed(null);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void Delete_Confirmed_InvalidId()
        {
            ViewResult result = (ViewResult)controller.DeleteConfirmed(201);
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void Delete_Confirmed_validId()
        {
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(100);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        #endregion

        #endregion
    }
}
