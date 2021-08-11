namespace DavidsList.Controllers
{
    using DavidsList.Data;
    using Microsoft.AspNetCore.Mvc;
    using DavidsList.Services.Interfaces;

    public class MarkController : Controller
    {
        private readonly IMarkMovieService marker;

        public MarkController(IMarkMovieService mark)
        {
            this.marker = mark;
        }
        public IActionResult Like(string id)
        {
            string referer = Request.Headers["Referer"].ToString();
            var userName = this.User.Identity.Name;
            marker.MarkMovieAsLiked(id, userName);
            return Redirect(referer);
        }
        public IActionResult Dislike(string id)
        {
            string referer = Request.Headers["Referer"].ToString();
            var userName = this.User.Identity.Name;
            marker.MarkMovieAsDisliked(id, userName);
            return Redirect(referer);
        }
        public IActionResult Favourite(string id)
        {
            string referer = Request.Headers["Referer"].ToString();
            var userName = this.User.Identity.Name;
            marker.MarkMovieAsFavourite(id, userName);
            return Redirect(referer);
        }
        public IActionResult Seen(string id)
        {
            string referer = Request.Headers["Referer"].ToString();
            var userName = this.User.Identity.Name;
            marker.MarkMovieAsSeen(id, userName);
            return Redirect(referer);
        }
        public IActionResult Flagg(string id)
        {
            string referer = Request.Headers["Referer"].ToString();
            var userName = this.User.Identity.Name;
            marker.MarkMovieAsFlagged(id, userName);
            return Redirect(referer);
        }

    }
}
