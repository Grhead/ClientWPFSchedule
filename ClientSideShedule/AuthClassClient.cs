using ClientSideShedule.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSideShedule
{
    public class AuthClassClient
    {
        private string _loginTextBox;
        private static string _passwordTextBox;

        public string LoginTextBox
        {
            get { return _loginTextBox; }
            set { _loginTextBox = value; }
        }

        public static string PasswordTextBox
        {
            get { return _passwordTextBox; }
            set { _passwordTextBox = value; }
        }
        private RelayCommand _check;
        public RelayCommand CheckCommand => _check ?? (_check = new RelayCommand(x =>
        {
            ApiService.AuthorizationResult GetUser = new ApiService.AuthorizationResult{IsAccept = "false", Role = -1};
            if (LoginTextBox != null && PasswordTextBox != null)
            {
                GetUser = Service.Client.CheckAuth(new ApiService.AuthorizationContext
                    { Login = LoginTextBox, Pass = PasswordTextBox });
            }

            if (GetUser.IsAccept == "true" && GetUser.Role == 1)
            {
                Service.frame.Navigate(new AdminPage());
            } else if (GetUser.IsAccept == "true" && GetUser.Role == 2)
            {
                Service.frame.Navigate(new TutorPage());
            }
        }));
    }
}
