using DAL.Data;
using DAL.Entities;
using DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Assignments
{
    public static class AssignmentsUtils
    {

        public static IQueryable<Assignment> ApplyFilters(IQueryable<Assignment> query, AssignmentsFilters? filters)
        {
            if (filters == null)
            {
                return query;
            }

            if (filters.PickupStatus != null)
            {
                query = query.Where(a => a.PickupStatus == filters.PickupStatus);
            }

            if (filters.Status != null)
            {
                query = query.Where(a => a.Status == filters.Status);
            }

            if (filters.ServiceProviderId != null)
            {
                query = query.Where(a => a.ServiceProviderId == filters.ServiceProviderId);
            }

            return query;
        }
    }
}
