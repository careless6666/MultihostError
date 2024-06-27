// See https://aka.ms/new-console-template for more information

using System;
using Npgsql;
using Serilog;

Console.WriteLine("Hello, World!");

var dataSourceBuilder = new NpgsqlDataSourceBuilder("User ID=postgres;Password=mysecretpassword;Host=localhost,localhost;Port=44217;Database=postgres;Pooling=true;LoadTableComposites=true; No Reset On Close=true;");

await using var dataSource = dataSourceBuilder.BuildMultiHost();
var ds = dataSource.WithTargetSession(TargetSessionAttributes.ReadOnly);

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

log.Information("Hello, Serilog!");

for (int i = 0; i < 1_000; i++)
{
    try
    {
        var conn = ds.CreateConnection();
        //var connection = await dataSource.OpenConnectionAsync(TargetSessionAttributes.ReadWrite);
        //var connection1 = await dataSource.OpenConnectionAsync();
        var connection1 = await ds.OpenConnectionAsync();
    }
    catch (Exception e)
    {
        log.Error(e, "my error, failed to open connect");
        Console.WriteLine(e);
    }
}