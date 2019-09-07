using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NetCoreTestApplication.Data;

namespace NetCoreTestApplication.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(FirstDatabaseContext firstDatabaseContext, SecondDatabaseContext secondDatabaseContext)
        {
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
