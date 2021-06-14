using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EmlakOfisi.Core.DAL
{
    public interface IRepository<T>
    {
        T Get(Expression<Func<T, bool>> filter);

        bool Any(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        int Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
