using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Extensions
{
    public static class CustomLinqExtensions
    {
        /// <summary>
        /// Setting a default value for id column in tables.
        /// This means the id will be generated from the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public static void SetDefaultConstraintForId<T>(this EntityTypeBuilder<T> builder)
            where T : BaseEntity
        {
            builder.Property(_ => _.Id).HasDefaultValueSql("NEWID()");
        }
    }
}
