using ConferencePlanner.Application;
using ConferencePlanner.GraphQL.Mutations;
using ConferencePlanner.GraphQL.Nodes;
using ConferencePlanner.GraphQL.Queries;
using ConferencePlanner.GraphQL.Subscriptions;
using ConferencePlanner.Infrastructure;
using ConferencePlanner.Infrastructure.Logger;
using ConferencePlanner.Infrastructure.Persistence.Imports;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Serilog;
using LoggerConfigurationExtensions = ConferencePlanner.Infrastructure.Logger.LoggerConfigurationExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string APPLICATION_NAME = "ConferencePlanner-GraphQL";
LoggerConfigurationExtensions.SetupLoggerConfiguration(APPLICATION_NAME);
        
builder.Services.AddApplicationInsightsTelemetry();

builder.Host.UseSerilog((hostBuilderContext, services, loggerConfiguration) =>
{
    loggerConfiguration.ConfigureBaseLogging(APPLICATION_NAME);
    loggerConfiguration.AddApplicationInsightsLogging(services, hostBuilderContext.Configuration);
});

builder.Services
    .AddCors(o =>
        o.AddDefaultPolicy(b =>
            b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()));
                
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();

// This adds the GraphQL server core service and declares a schema.
builder.Services
    .AddMemoryCache()
            
    .AddGraphQLServer()

    // Next we add the types to our schema.
    .AddQueryType()
    .AddMutationType()
    .AddSubscriptionType()
    .AddTypeExtension<AttendeeQueries>()
    .AddTypeExtension<AttendeeMutations>()
    .AddTypeExtension<AttendeeSubscriptions>()
    .AddTypeExtension<AttendeeNode>()
    .AddTypeExtension<SessionQueries>()
    .AddTypeExtension<SessionMutations>()
    .AddTypeExtension<SessionSubscriptions>()
    .AddTypeExtension<SessionNode>()
    .AddTypeExtension<SpeakerQueries>()
    .AddTypeExtension<SpeakerMutations>()
    .AddTypeExtension<SpeakerNode>()
    .AddTypeExtension<TrackQueries>()
    .AddTypeExtension<TrackMutations>()
    .AddTypeExtension<TrackNode>()
    .AddDataLoaders()

    // In this section we are adding extensions like relay helpers,
    // filtering and sorting.
    .AddFiltering()
    .AddSorting()
    .AddGlobalObjectIdentification()

    // we make sure that the db exists and prefill it with conference data.
    .EnsureDatabaseIsCreated()

    // Since we are using subscriptions, we need to register a pub/sub system.
    // for our demo we are using a in-memory pub/sub system.
    .AddInMemorySubscriptions()

    // Last we add support for automatic persisted queries. 
    // The first line adds persisted query processing pipeline, 
    // the second one adds the persisted query storage.
    .UseAutomaticPersistedQueryPipeline()
    .AddInMemoryQueryStorage();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();

app.MapHealthChecks("/healthz");

app.UseWebSockets();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // We will be using the new routing API to host our GraphQL middleware.
    endpoints.MapGraphQL()
        .WithOptions(new GraphQLServerOptions
        {
            Tool =
            {
                GaTrackingId = "G-2Y04SFDV8F"
            }
        });

    app.UseVoyager("/graphql","/graphql-voyager");
            
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/graphql", true);
        return Task.CompletedTask;
    });
});

app.Run();