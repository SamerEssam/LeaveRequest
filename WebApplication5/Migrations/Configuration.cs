namespace WebApplication5.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication5.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication5.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            List<VacationType> vacationTypes = new List<VacationType>();
            var VacNames = Enum.GetNames(typeof(VacType));
            var VacIds = (int[])Enum.GetValues(typeof(VacType));
            for (int i = 0; i < VacNames.Length; i++)
            {
                vacationTypes.Add(new VacationType { Id = VacIds[i], LeaveType = VacNames[i] });
            }
            vacationTypes.ForEach(item => context.VacationTypes.AddOrUpdate(y => y.Id, item));



            List<RequestStatus> requestStatuses = new List<RequestStatus>();
            var StatNames = Enum.GetNames(typeof(ReqStatus));
            for (int i = 0; i < StatNames.Length; i++)
            {
                requestStatuses.Add(new RequestStatus { Id = i + 1, Status = StatNames[i] });
                //Enum.GetName(typeof( ReqStatus),i)
            }
            requestStatuses.ForEach(rItem => context.RequestStatuses.AddOrUpdate(y => y.Id, rItem));

            context.SaveChanges();
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
