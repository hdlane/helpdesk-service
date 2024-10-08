using System.Text.Json;
using System.Text;
using ComputerInfo;
namespace HelpdeskService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string? server = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Endpoint")["Server"];
    readonly Computer computer = new Computer();

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public async Task<string> PostJson(string url, Computer computer)
    {
        if (server == null || server == "")
        {
            _logger.LogError("Endpoint server URL not provided. Update the appsettings.json file.");
            Environment.Exit(1);
        }
        using (var client = new HttpClient())
        {
            string jsonString = JsonSerializer.Serialize(computer);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            try
            {
                var response = await client.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "{Message}", ex.Message);
                return ex.Message;
            }
        }
    }

    public bool InfoChanged(Computer computer)
    {
        var oldInfo = new Dictionary<string, string>();
        var newInfo = new Dictionary<string, string>();

        oldInfo.Add("ComputerName", computer.ComputerName);
        oldInfo.Add("UserName", computer.UserName);
        oldInfo.Add("IpAddress", computer.IpAddress);

        computer.GetInfo();

        newInfo.Add("ComputerName", computer.ComputerName);
        newInfo.Add("UserName", computer.UserName);
        newInfo.Add("IpAddress", computer.IpAddress);

        foreach (var key in oldInfo.Keys)
        {
            if (oldInfo[key] != newInfo[key])
            {
                return true;
            }
        }
        return false;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this.PostJson(server, computer);
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (InfoChanged(computer))
                {
                    await this.PostJson(server, computer);
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{0}", ex.Message);
            Environment.Exit(1);
        }
    }
}
