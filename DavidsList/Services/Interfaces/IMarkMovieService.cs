namespace DavidsList.Services.Interfaces
{
    public interface IMarkMovieService
    {
        public void MarkMovieAsLiked(string movieId, string userName);
        public void MarkMovieAsSeen(string movieId, string userName);
        public void MarkMovieAsDisliked(string movieId, string userName);
        public void MarkMovieAsFavourite(string movieId, string userName);
        public void MarkMovieAsFlagged(string movieId, string userName);
    }
}
