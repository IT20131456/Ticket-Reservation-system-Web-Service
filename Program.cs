// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();





using MongoDotnetDemo.Models;
using MongoDotnetDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});





builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseSettings>(
     builder.Configuration.GetSection("MyDb")
    );
//resolving the CategoryService dependency here
builder.Services.AddTransient<ICategoryService,CategoryService>();
//resolving the ProductService dependency here
builder.Services.AddTransient<IProductService,ProductService>();

//resolving the TravelerService dependency here
builder.Services.AddTransient<ITravelerService,TravelerService>();


//resolving the TrainScheduleService dependency here
builder.Services.AddTransient<ITrainScheduleService,TrainScheduleService>();

//resolving the TicketBookingService dependency here
builder.Services.AddTransient<ITicketBookingService,TicketBookingService>();

//resolving the StaffService dependency
builder.Services.AddTransient<IStaffService, StaffService>();
//resolving the TravelAgent dependency
builder.Services.AddTransient<ITravelAgentService, TravelAgentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();


app.Run();

