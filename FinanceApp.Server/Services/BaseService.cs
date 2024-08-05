using FinanceApp.Server.Data;

namespace FinanceApp.Server.Services;

public class BaseService
{
    protected readonly DataContext _ctx;

    public BaseService(DataContext ctx)
    {
        _ctx = ctx;
    }
}
