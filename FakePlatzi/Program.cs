using FakePlatzi.Services;

var builder = WebApplication.CreateBuilder(args);

//Servicio FakeAPI
builder.Services.AddHttpClient("FakeAPI", client =>
{
	client.BaseAddress = new Uri("https://api.escuelajs.co/api/v1/");
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Inyeccion de dependencias
builder.Services.AddScoped<IProductService, ProductService>();



// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
