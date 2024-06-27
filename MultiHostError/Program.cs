// See https://aka.ms/new-console-template for more information

using Npgsql;

Console.WriteLine("Hello, World!");

var dataSourceBuilder = new NpgsqlDataSourceBuilder("User ID=postgres;Password=mysecretpassword;Host=localhost,localhost;Port=44217;Database=postgres;Pooling=true;LoadTableComposites=true; No Reset On Close=true;");

await using var dataSource = dataSourceBuilder.BuildMultiHost();
var ds = dataSource.WithTargetSession(TargetSessionAttributes.ReadOnly);

for (int i = 0; i < 1_000; i++)
{
    try
    {
        var conn = ds.CreateConnection();
        var connection1 = await ds.OpenConnectionAsync();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}