
// dotnet new webapi -n API_StringList_Versioning --framework net8.0
//
// Net CLI
//
//   dotnet add package Asp.Versioning.Mvc --version 8.1.0
//   dotnet add package Asp.Versioning.Mvc.ApiExplorer --version 8.1.0
//   dotnet add package Asp.Versioning.Http --version 8.1.0
//
//
// NuGet Package Console - For Visual Studio 2022.
//
//   Install-Package Asp.Versioning.Mvc
//   Install-Package Asp.Versioning.Mvc.ApiExplorer
//   Install-Package Asp.Versioning.Http

// youtube:
//          https://www.youtube.com/watch?v=i6kkKBsHEJs

using Api_StringList_Versioning;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// 1.-
// QueryString : http://localhost:5000/api/stringlist?api-version=1.0
// Headers     : Accept -> application/json;ver=1.0
//               http://localhost:5000/api/stringlist
// URL         : http://localhost:5000/api/v3/stringlist
//
builder.Services.AddApiVersioning(o => { 
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Asp.Versioning.ApiVersion(2, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver")
        );
}).AddApiExplorer( options => { 
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
});

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

// 2.-
var apiVersionSet = app.NewApiVersionSet()
        .HasDeprecatedApiVersion(new ApiVersion(1, 0))
        .HasApiVersion(new ApiVersion(2, 0))
        .HasApiVersion(new ApiVersion(3, 0))
        .ReportApiVersions()
        .Build();

// 3.-
app.MapGet("api/minimal/StringList", () =>
{
    var strings = Data.Summaries.Where(x => x.StartsWith("B"));

    return TypedResults.Ok(strings);
})
.WithApiVersionSet(apiVersionSet)
.MapToApiVersion(new ApiVersion(1, 0));

app.MapGet("api/minimal/StringList", () =>
{
    var strings = Data.Summaries.Where(x => x.StartsWith("S"));

    return TypedResults.Ok(strings);
})
.WithApiVersionSet(apiVersionSet)
.MapToApiVersion(new ApiVersion(2, 0));

app.MapGet("api/minimal/v{version:apiVersion}/StringList", () =>
{
    var strings = Data.Summaries.Where(x => x.StartsWith("C"));

    return TypedResults.Ok(strings);
})
.WithApiVersionSet(apiVersionSet)
.MapToApiVersion(new ApiVersion(3, 0));

app.Run();