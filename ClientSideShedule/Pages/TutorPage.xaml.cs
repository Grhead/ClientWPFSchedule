using ApiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientSideShedule.Pages
{
    /// <summary>
    /// Логика взаимодействия для TutorPage.xaml
    /// </summary>
    public partial class TutorPage : Page
    {
        private TutorsFromDb TutorArr = Service.Client.GetTutorsFromDbRPC(new Wrap());
        public TutorPage()
        {
            InitializeComponent();
            DataContext = new SubjectForConcreteDay();
            foreach (var item in TutorArr.Tutors)
            {
                Tutor_ComboBox.Items.Add(item.Id + ". " + item.SecondName + " " + item.FirstName + " " + item.LastName);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Service.frame.Navigate(new AuthPage());
        }
    }
}
