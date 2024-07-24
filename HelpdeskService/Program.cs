using HelpdeskService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Helpdesk Service";
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
