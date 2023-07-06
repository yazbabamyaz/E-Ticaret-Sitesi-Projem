using CoreLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        //ef kullananlar bilirler ki 2 şeye ihtiyaç var biri nesne:TEntity diğeri:Context
        //tüm operasyonlar burada
        public void Add(TEntity entity)
        {            

            using (var context=new TContext())
            {
                //context.Set<TEntity>().Add(entity);
                //Ya da
                // benim nesnem eklenecek bir nesnedir de
                var addedEntity= context.Entry(entity);
                addedEntity.State= EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                //context.Set<TEntity>().Add(entity);
                //Ya da
                
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context=new TContext())
            {
                
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
               ? context.Set<TEntity>().ToList()
               : context.Set<TEntity>().Where(filter).ToList();
            }           

        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
               
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
