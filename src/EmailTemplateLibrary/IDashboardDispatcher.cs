using EmailTemplateLibrary.Dashboard;
using System.Threading.Tasks;

namespace EmailTemplateLibrary
{
    public interface IDashboardDispatcher
    {
        Task Dispatch(DashboardContext context);
    }
}

