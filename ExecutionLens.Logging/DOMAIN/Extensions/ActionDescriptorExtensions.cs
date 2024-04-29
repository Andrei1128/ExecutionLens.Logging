using Microsoft.AspNetCore.Mvc.Abstractions;

namespace ExecutionLens.Logging.DOMAIN.Extensions;

internal static class ActionDescriptorExtensions
{
    public static string GetMethodName(this ActionDescriptor actionDescriptor)
    {
        string fullName = actionDescriptor.DisplayName;

        string halfName = fullName.Split(' ')[0];
        int startIndex = halfName.LastIndexOf('.') + 1;

        return halfName[startIndex..];
    }
}
