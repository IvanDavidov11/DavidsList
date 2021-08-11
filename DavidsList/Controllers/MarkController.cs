namespace DavidsList.Controllers
{
    using DavidsList.Data;
    using DavidsList.Data.DbModels;
    using DavidsList.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    public class MarkController : Controller
    {
        private readonly DavidsListDbContext data;
        private readonly IMarkMovieService mark;

        public MarkController(DavidsListDbContext db, IMarkMovieService marker)
        {
            this.data = db;
            this.mark = marker;
        }
        public IActionResult Like(string id)
        {
            string referer = Request.Headers["Referer"].ToString();
            var userName = this.User.Identity.Name;
            mark.MarkMovieAsLiked(id, userName);
            return Redirect(referer);
        }

    }
}
