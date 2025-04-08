
using WebAPI;

var app = new AppBuilder(args).Build();
await RoleSeeder.SeedRolesAsync(app);
app.Run();
