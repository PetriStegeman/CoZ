using CoZ.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoZ.Repositories
{
    public abstract class Repository : IDisposable
    {
        private ApplicationDbContext dbContext;
        protected ApplicationDbContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    return ApplicationDbContext.Create();
                }
                else
                {
                    return dbContext;
                }
            }
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}