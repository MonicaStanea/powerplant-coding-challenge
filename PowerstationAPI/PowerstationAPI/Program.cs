using Microsoft.AspNetCore.Mvc;
using PowerstationAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region the code

app.MapPost("/productionplan", ([FromBody] Payload requestBody) =>
{
    return PowerCalculator.ProcessPowerplants(requestBody);
}).Produces<List<PowerplantOn>>(200).Produces(400);


#endregion


app.UseHttpsRedirection();

app.Run();
