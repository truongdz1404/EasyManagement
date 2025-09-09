using EasyMN.Shared.Entities;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mappings
{
    public class ClassRoomMap : ClassMap<ClassRoom>
    {
        public ClassRoomMap()
        {
            Table("ClassRooms");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.ClassCode).Not.Nullable().Length(20).Unique();
            Map(x => x.ClassName).Not.Nullable().Length(100);
            Map(x => x.Subject).Not.Nullable().Length(100);
            References(x => x.Teacher).Column("TeacherId").Not.Nullable().Cascade.None();
            HasMany(x => x.Students).KeyColumn("ClassRoomId").Inverse().Cascade.All();
        }   
    }
}
