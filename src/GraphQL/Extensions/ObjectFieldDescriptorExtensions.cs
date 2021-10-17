using HotChocolate.Types;
using HotChocolate.Types.Descriptors.Definitions;

namespace ConferencePlanner.GraphQL.Extensions;

public static class ObjectFieldDescriptorExtensions
{
    public static IObjectFieldDescriptor UseUpperCase(
        this IObjectFieldDescriptor descriptor)
    {
        // TODO : we need a better API for the user.
        descriptor.Extend().Definition.ResultConverters.Add(
            new ResultConverterDefinition((_, result) =>
            {
                if (result is string s) return s.ToUpperInvariant();

                return result;
            }));

        return descriptor;
    }
}