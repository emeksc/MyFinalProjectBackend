using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

//core katmanı evrensel bir katmanbdır 
//core katmanlar diğer katmanları referans almamalı çünkü bağımsız evrensel bir katman olmalı
namespace Core.DataAccess
{
    //generic constraint
    //class: referans tip anlamına gelir
    //t yerine gelecek olan şeyi kısıtlarız 
    //new: newlenecek bir şey gelmeli bu yüzden ientityi koyamayız t yerine sadece ientityi implemente ettiğimiz nesneleri kullanabiliriz
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        //expression kulladığımız metodun içinde filtre yapmamızı sağlayabilir
        //mesela get metoduna ürün adı ve categoryidye göre filtre verebiliriz
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
