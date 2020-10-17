using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCore5TestApplication.Data;

namespace NetCore5TestApplication.Pages
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
