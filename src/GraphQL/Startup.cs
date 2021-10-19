using ConferencePlanner.Application;
using ConferencePlanner.GraphQL.Mutations;
using ConferencePlanner.GraphQL.Nodes;
using ConferencePlanner.GraphQL.Queries;
using ConferencePlanner.GraphQL.Subscriptions;
using ConferencePlanner.Infrastructure;
using ConferencePlanner.Infrastructure.Persistence.Imports;
using HotChocolate.AspNetCore;

namespace ConferencePlanner.GraphQL;

/// <summary>
/// This class will configure all of the required services and request handling  pipeline.
/// </summary>
public class Startup
{
    /// <summary>
    /// Constructor of the startup class
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Global Configurations.
    /// </summary>
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCors(o =>
                o.AddDefaultPolicy(b =>
                    b.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()));

        services.AddApplication();
        services.AddInfrastructure(Configuration);

        // This adds the GraphQL server core service and declares a schema.
        services
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
            .AddInMemorySubscriptions();

        // Last we add support for persisted queries. 
        // The first line adds the persisted query storage, 
        // the second one the persisted query processing pipeline.
        // .AddInMemoryQueryStorage()
        // .UsePersistedQueryPipeline();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors();

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

            endpoints.MapGet("/", context =>
            {
                context.Response.Redirect("/graphql", true);
                return Task.CompletedTask;
            });
        });
    }
}