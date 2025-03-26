using InventoryApp.Contexts;
using Microsoft.Extensions.DependencyInjection;
using InventoryApp.Managers;
using InventoryApp.Commands;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDataProtection();
serviceCollection.AddSingleton<ConfigurationManager>();
serviceCollection.AddSingleton<AuthenticationManager>();
serviceCollection.AddSingleton<ConnectionManager>();
serviceCollection.AddDbContext<MariaDatabaseContext>();
serviceCollection.AddSingleton<App>();
  
var services = serviceCollection.BuildServiceProvider();
GlobalServiceProvider.Initialize(services);

CommandRegistry.RegisterCommands();

var app = services.GetRequiredService<App>();
app.Start();


