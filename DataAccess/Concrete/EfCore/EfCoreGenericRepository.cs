using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EfCore
{
    public class EfCoreGenericRepository<Tentity, Tcontext> : IRepository<Tentity>
    where Tentity : class
    where Tcontext : DbContext, new()
    {
        public void Create(Tentity entity)
        {
            using(var db = new Tcontext())
            {
                db.Set<Tentity>().Add(entity);
                db.SaveChanges();
            }
        }

        public void Delete(Tentity entity)
        {
            using (var db = new Tcontext())
            {
                db.Set<Tentity>().Remove(entity);
                db.SaveChanges();
            }
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>>? filter = null)
        {
            using (var db = new Tcontext())
            {
                return filter == null ? db.Set<Tentity>().ToList() : db.Set<Tentity>().Where(filter).ToList(); 
            }
        }

        public Tentity GetById(int id)
        {
            using (var db = new Tcontext())
            {
#pragma warning disable CS8603 // Possible null reference return.
                return db.Set<Tentity>().Find(id);
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public Tentity GetOne(Expression<Func<Tentity, bool>> filter)
        {
            using (var db = new Tcontext())
            {
                return db.Set<Tentity>().Where(filter).Single();
            }
        }

        public virtual void Update(Tentity entity)
        {
            using (var db = new Tcontext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
