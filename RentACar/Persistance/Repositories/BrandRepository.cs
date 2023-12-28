using Application.Services.Repositories;
using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using Core.Persistance.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BrandRepository : EfRepositoryBase<Brand, Guid, BaseDbContext>, IBrandRepository
    {
        public BrandRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
