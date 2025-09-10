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
                query = query.Where(a => a.ServiceProviderIdExt == filters.ServiceProviderId);
            }

            if (filters.IsPaid != null)
            {
                if (filters.IsPaid == true)
                    query = query.Where(a => a.IsPaid == filters.IsPaid);
                if (filters.IsPaid == false)
                {
                    query = query.Where(a => a.IsPaid == filters.IsPaid || a.IsPaid == null);
                }
            }

            if (filters.CustomerName != null)
            {
                query = query.Where(a => a.Customer.Name.Contains( filters.CustomerName));
            }

            if (filters.skip.HasValue)
                query = query.Skip(filters.skip.Value);

            if (filters.take.HasValue)
                query = query.Take(filters.take.Value);

            return query;
        }
    }
}
