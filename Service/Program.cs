
using ProtoBuf.Grpc.Server;
using Repositories.Helpers;
using Repositories.Impliment;
using Repositories.IRepositories;
using Service.Services;
using EasyMN.Shared.IServices;

var builder = WebApplication.CreateBuilder(args);
NHibernateHelper.Initialize(builder.Services, builder.Configuration);
builder.Services.AddScoped<IClassRepository, ClassRoomRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
// Add services to the container.
builder.Services.AddCodeFirstGrpc();
builder.Services.AddScoped<IStudentGrpcService, StudentGrpcService>();
builder.Services.AddScoped<IClassGrpcService, ClassGrpcService>();
builder.Services.AddScoped<IReportGrpcService, ReportGrpcService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    DataSeeder.Seed(serviceProvider);
}

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
// Configure the HTTP request pipeline.
app.MapGrpcService<StudentGrpcService>().EnableGrpcWeb();
app.MapGrpcService<ClassGrpcService>().EnableGrpcWeb();
app.MapGrpcService<ReportGrpcService>().EnableGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
