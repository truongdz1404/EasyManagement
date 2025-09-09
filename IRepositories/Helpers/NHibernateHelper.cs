using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Repositories.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
namespace Repositories.Helpers
{
    public static class NHibernateHelper
    {
        private static ISessionFactory? _sessionFactory;

        public static void Initialize(IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");

            _sessionFactory = Fluently.Configure()
                .Database(
                    PostgreSQLConfiguration.Standard
                        .ConnectionString(connectionString)
                        .ShowSql()
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssembly(typeof(NHibernateHelper).Assembly)
                )
                .ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg).Execute(false, true);
                })
                .BuildSessionFactory();

            services.AddSingleton(_sessionFactory);
            services.AddScoped(factory => _sessionFactory.OpenSession());
        }
    }

}
