

namespace Bouldering_Company.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class UserEntities3 : DbContext
    {
        public UserEntities3()
            : base("name=UserEntities3")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<AdminData> AdminDatas { get; set; }
    }
}