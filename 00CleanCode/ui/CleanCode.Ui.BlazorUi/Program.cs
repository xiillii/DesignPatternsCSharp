using CleanCode.Ui.BlazorUi;
using CleanCode.Ui.BlazorUi.Contracts;
using CleanCode.Ui.BlazorUi.Services;
using CleanCode.Ui.BlazorUi.Services.Base;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

const string API_URI = @"https://localhost:7069";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IClient, Client>(client =>
    client.BaseAddress = new Uri(API_URI));

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();

await builder.Build().RunAsync();
