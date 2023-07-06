using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        //generic olacak o yüzden T-db nesnesi yazabilirsin, interface yazamazsın.
        //Tek nesne çekerken.İd geçebiliriz tc olabilir vs o yüzden Linq Expiression olarak   yazarız.Değer girmeyebilir:null
        T Get(Expression<Func<T,bool>> filter=null);
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);//bana entity ver db ye ekleyeyim
        
        void Delete(T entity);
        void Update(T entity);

    }
}
