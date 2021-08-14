namespace DavidsList.Services.Interfaces
{
    using DavidsList.Models.FormModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountInteractor
    {
        public Task<Dictionary<string, string>> TryRegisteringUser(RegisterFormModel model);
        public Task<Dictionary<string, string>> TryLoggingUserIn(LoginFormModel model);

    }
}
