using EasyMN.Shared.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mappings
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("Students");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.StudentCode).Not.Nullable().Length(20).Unique();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.Dob).Not.Nullable();
            Map(x => x.Address).Not.Nullable().Length(200);
            References(x => x.ClassRoom).Column("ClassRoomId").Not.Nullable().Cascade.None();
        }   
    }
}
