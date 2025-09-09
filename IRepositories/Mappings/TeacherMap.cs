using EasyMN.Shared.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Mappings
{
    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Table("Teachers");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.TeacherCode).Not.Nullable().Length(20).Unique();
            Map(x => x.Name).Not.Nullable().Length(100);
            Map(x => x.Dob).Not.Nullable();
            HasMany(x => x.Classes).KeyColumn("TeacherId").Inverse().Cascade.All();
        }   
    }
}
