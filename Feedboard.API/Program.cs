using Feedboard.API.Helplers;
using Feedboard.Core;
using Feedboard.DAL;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.

// Add CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder =>
		{
			builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
		});
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapper = MapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Add Services
builder.Services.AddDAL(ConnectionStringHelper.GetDatabaseConnectionString());
builder.Services.AddCore();

var app = builder.Build();

// Configure CORS policy
app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
// in production need to uncomment this "if"
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
