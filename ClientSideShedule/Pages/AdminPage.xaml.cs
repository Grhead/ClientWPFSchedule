using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;
using ApiService;
using Grpc.Net.Client;

namespace ClientSideShedule.Pages
{
    public partial class AdminPage : Page
    {
        List<SheduleObjectClass> LocalScheduleList = new List<SheduleObjectClass>();
        private SubjectsFromDb SubjectArr = Service.Client.GetSubjectFromDbRPC(new Wrap());
        private AuditoriumsFromDb AuditoriumArr = Service.Client.GetAuditoriumFromDbRPC(new Wrap());
        private GroupsFromDb GroupArr = Service.Client.GetGroupFromDbRPC(new Wrap());
        private TutorsFromDb TutorArr = Service.Client.GetTutorsFromDbRPC(new Wrap());
        private TypesFromDb TypeArr = Service.Client.GetTypeFromDbRPC(new Wrap());


        public AdminPage()
        {
            InitializeComponent();
            DataContext = new SubjectForConcreteDay();
            foreach (var item in SubjectArr.Subjects)
            {
                Subject_ComboBox.Items.Add(item.SubjectItem);
            }
            foreach (var item in AuditoriumArr.Auditoriums)
            {
                Auditorium_ComboBox.Items.Add(item.Classroom);
            }
            foreach (var item in GroupArr.Groups)
            {
                Group_ComboBox.Items.Add(item.GroupName);
            }
            foreach (var item in TutorArr.Tutors)
            {
                Tutor_ComboBox.Items.Add(item.Id + ". " + item.SecondName + " " + item.FirstName + " " + item.LastName);
            }
            foreach (var item in TypeArr.Types_)
            {
                Type_ComboBox.Items.Add(item.Type);
            }
            for (int i = 1; i < 9; i++)
            {
                Number_ComboBox.Items.Add("Пара №" + i);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (Subject_ComboBox.SelectedItem != null && Auditorium_ComboBox.SelectedItem != null 
                && Group_ComboBox.SelectedItem != null && Tutor_ComboBox.SelectedItem != null 
                && Type_ComboBox.SelectedItem != null && Number_ComboBox.SelectedItem != null
                && DatePickerSelecter.SelectedDate != null)
            {
                var NewObject = new SheduleObjectClass()
                {
                    Subject = SubjectArr.Subjects.FirstOrDefault(x => x.SubjectItem == (string)Subject_ComboBox.SelectedItem).Id,
                    Auditorium = AuditoriumArr.Auditoriums.FirstOrDefault(x => x.Classroom == (string)Auditorium_ComboBox.SelectedItem).Id,
                    Group = GroupArr.Groups.FirstOrDefault(x => x.GroupName == (string)Group_ComboBox.SelectedItem).Id,
                    Tutor = TutorArr.Tutors.FirstOrDefault(x => x.Id.ToString() == Tutor_ComboBox.SelectedItem.ToString().Substring(0, Tutor_ComboBox.SelectedItem.ToString().IndexOf("."))).Id,
                    Type = TypeArr.Types_.FirstOrDefault(x => x.Type == (string)Type_ComboBox.SelectedItem).Id,
                    Dates = (System.DateTime)DatePickerSelecter.SelectedDate,
                    Number = Convert.ToInt32(Number_ComboBox.SelectedItem.ToString().Substring(Number_ComboBox.SelectedItem.ToString().IndexOf("№") + 1)) - 1
                };
                if (LocalScheduleList.Exists(x => x.Dates == NewObject.Dates && x.Group == NewObject.Group && x.Number == NewObject.Number) == false)
                {
                    LocalScheduleList.Add(NewObject);
                    LocalListCountTextBlock.Text = LocalScheduleList.Count.ToString();
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Service.frame.Navigate(new AuthPage());
        }
        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Subject_ComboBox.SelectedItem = null;
            Auditorium_ComboBox.SelectedItem = null;
            Group_ComboBox.SelectedItem = null;
            Tutor_ComboBox.SelectedItem = null;
            Type_ComboBox.SelectedItem = null;
            Number_ComboBox.SelectedItem = null;
            DatePickerSelecter.SelectedDate = null;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            AllSheduleArray ConfirmationArray = new AllSheduleArray();
            SheduleObject? SO;
            if (LocalScheduleList.Count() > 0)
            {
                foreach (var item in LocalScheduleList)
                {
                    SO = new SheduleObject();
                    ApiService.DateTime ApiDateTime = new ApiService.DateTime(new ApiService.DateTime
                    {
                        Year = item.Dates.Year,
                        Month = item.Dates.Month,
                        Day = item.Dates.Day,
                        Hours = 0,
                        Minutes = 0,
                        Seconds = 0
                    });
                    SO.Auditorium = item.Auditorium;
                    SO.Number = item.Number;
                    SO.Subject = item.Subject;
                    SO.Dates = ApiDateTime;
                    SO.Tutor = item.Tutor;
                    SO.Type = item.Type;
                    SO.Group = item.Group;
                    ConfirmationArray.Objects.Add(SO);
                }
                Service.Client.AddShedule(ConfirmationArray);
                LocalScheduleList.Clear();
            }
        }
    }
}
