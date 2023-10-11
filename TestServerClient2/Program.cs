using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapPost("/credentials", ([FromBody] string asd) =>
{
    // Assuming you have a SQL Server database connection string in your appsettings.json
    string connectionString = _configuration.GetConnectionString("DefaultConnection");

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        // Replace 'YourUsersTable' and column names with your actual table and column names
        string query = $"SELECT COUNT(*) FROM YourUsersTable WHERE Username = @Username AND Password = @Password";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Username", credentials.Username);
            command.Parameters.AddWithValue("@Password", credentials.Password);

            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                return Ok("Login successful");
            }
            else
            {
                return Unauthorized("Incorrect username or password");
            }
        }
    }
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}