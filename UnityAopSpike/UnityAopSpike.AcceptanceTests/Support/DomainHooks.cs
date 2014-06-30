using System;
using TechTalk.SpecFlow;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.AcceptanceTests.Support
{
    [Binding]
    public class DomainHooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            Console.WriteLine("Cleaning up the database...");
            using (var db = new EntityDatabaseContext())
            {
                db.Database.ExecuteSqlCommand("delete from Orders");
                db.Database.ExecuteSqlCommand("delete from Products");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}