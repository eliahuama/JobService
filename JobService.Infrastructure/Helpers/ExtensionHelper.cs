namespace JobService.Infrastructure.Helpers;

public static class ExtensionHelper
{
    public static List<string> GetAllowedExtensions()
    {
        return ["application/pdf", "application/msword"];
    }
}