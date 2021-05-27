namespace EmailTemplateLibrary.Dashboard
{
    public interface IDashboardAuthorizationFilter
    {
        bool Authorize(DashboardContext context);
    }
}
