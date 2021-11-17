using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetCore6TestApplication.Data;

namespace NetCore6TestApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, FirstDatabaseContext firstDatabaseContext, SecondDatabaseContext secondDatabaseContext)
        {
            _logger = logger;

            FirstDatabaseContext = firstDatabaseContext;
            SecondDatabaseContext = secondDatabaseContext;
        }

        public FirstDatabaseContext FirstDatabaseContext { get; }
        public SecondDatabaseContext SecondDatabaseContext { get; }

        public async Task OnGetAsync()
        {
            var firstData = await FirstDatabaseContext.FirstDataModels.ToListAsync();
            var secondData = await SecondDatabaseContext.SecondDataModels.ToListAsync();
        }
    }
}