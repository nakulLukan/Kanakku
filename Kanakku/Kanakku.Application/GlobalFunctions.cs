namespace Kanakku.Application
{
    public static class GlobalFunctions
    {
        public static string GetWorkingStatus(DateTime? resignedOn)
            => resignedOn.HasValue && resignedOn.Value.Date <= DateTime.Now.Date ? "Resigned" : "Working";
    }
}
