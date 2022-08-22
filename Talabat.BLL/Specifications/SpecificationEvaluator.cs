using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> spec)
        {
            var query = InputQuery;
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if(spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);
            if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);
            if(spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (CurrentQuery, include) => CurrentQuery.Include(include));

            return query;
        }
    }
}
