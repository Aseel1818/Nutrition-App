using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using oauth.Models;
using oauth.Services;

namespace oauth.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set => SetProperty(ref isRefreshing, value); }

        List<UsersModel> myUsers;
        public List<UsersModel> MyUsers { get => myUsers; set => SetProperty(ref myUsers, value); }

        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }

        public LoginViewModel()
        {
            MyUsers = new List<UsersModel>();
            IsBusy = false;

            RefreshCommand = new Command(async () => await ExecuteRefreshCommand());
            DeleteCommand = new Command<UsersModel>(async (myTask) => await ExecuteDeleteCommand(myTask));
        }

        async Task ExecuteDeleteCommand(UsersModel task)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var mongoService = new AuthenticationMongoService();
                await mongoService.DeleteTask(task);

                MyUsers = await mongoService.GetAllUsers();
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var mongoService = new AuthenticationMongoService();
                MyUsers = await mongoService.GetAllUsers();
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
