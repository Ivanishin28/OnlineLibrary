using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShelfContext.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShelfContext.DL.SqlServer.ValueConverters
{
    public class EntityIdValueConverter<TId, T> : ValueConverter<TId, T> where TId : EntityId<T>
    {
        public EntityIdValueConverter() 
            : base(
                  value => From(value),
                  value => From(value)
                )
        {
        }

        public static T From(TId entityId)
        {
            return entityId.Value;
        }

        public static TId From(T value)
        {
            return (TId)Activator.CreateInstance(typeof(TId), value)!;
        }
    }
}
