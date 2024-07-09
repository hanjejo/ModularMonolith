using ModularMonolith;
using ModularMonolith.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assemblies = AssemblyLoader.LoadModules(builder.Services, builder.Configuration);
builder.Services.AddControllers(c =>
{
    c.ModelBinderProviders.Insert(0, new UlidModelBinderProvider());
});

builder.Services.Configure<RouteOptions>(cfg =>
{
    cfg.ConstraintMap.Add("ulid", typeof(UlidRouteConstraint));
});
// TODO: 각종 어셈블리 추가해야함
// TODO: Swagger 추가해야함

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
