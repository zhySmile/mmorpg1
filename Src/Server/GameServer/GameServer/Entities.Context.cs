﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameServer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mmorpg1Entities1 : DbContext
    {
        public mmorpg1Entities1()
            : base("name=mmorpg1Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TUser> Users { get; set; }
        public virtual DbSet<TPlayer> Players { get; set; }
        public virtual DbSet<TCharacter> Characters { get; set; }
        public virtual DbSet<TCharacterItem> CharacterItems { get; set; }
    }
}
