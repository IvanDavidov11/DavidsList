namespace DavidsList.Services.Interfaces
{
    using DavidsList.Models.ViewModels;
    using System.Collections.Generic;
    public interface IDatabaseInteractor
    {
        List<MarkedMovieViewModel> GetMarkedMovieViewModel_Favourited();
        List<MarkedMovieViewModel> GetMarkedMovieViewModel_Seen();
        List<MarkedMovieViewModel> GetMarkedMovieViewModel_Flagged();
    }
}
