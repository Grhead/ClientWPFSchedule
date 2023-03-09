using ApiService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientSideShedule
{
    public class SubjectForConcreteDay : ClassForSchedule
    {
        private ObservableCollection<TimeSchedule> _timeScheduleList;
        public ObservableCollection<TimeSchedule> TimeScheduleList
        {
            get { return _timeScheduleList; }
            set { _timeScheduleList = value; OnPropertyChanged(); }
        }
        public SubjectForConcreteDay()
        {
            _timeScheduleList = new ObservableCollection<TimeSchedule>();
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "08:45", EndTime = "10:20" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "10:35", EndTime = "12:10" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "12:25", EndTime = "14:00" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "14:45", EndTime = "16:20" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "16:35", EndTime = "18:10" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "18:25", EndTime = "20:00" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "20:15", EndTime = "21:50" });
            TimeScheduleList.Add(new TimeSchedule() { StartTime = "22:05", EndTime = "23:40" });
        }

        private string _selectedGroupFromComboBox;
        public string SelectedGroupFromComboBox
        {
            get { return _selectedGroupFromComboBox; }
            set { _selectedGroupFromComboBox = value; }
        }

        private string _selectedTutorFromComboBox;
        public string SelectedTutorFromComboBox
        {
            get { return _selectedTutorFromComboBox; }
            set { _selectedTutorFromComboBox = value; }
        }

        private RelayCommand _update;
        public RelayCommand UpdateCommand => _update ?? (_update = new RelayCommand(x =>
        {
            if (SelectedGroupFromComboBox != null)
            {
                Update();
            }
        }));
        private RelayCommand _updateDays;
        public RelayCommand UpdateDaysCommand => _updateDays ?? (_updateDays = new RelayCommand(x =>
        {
            if (SelectedGroupFromComboBox != null)
            {
                UpdateDays();
            }
        }));
        private RelayCommand _updateDaysForTutor;
        public RelayCommand UpdateDaysCommandForTutor => _updateDaysForTutor ?? (_updateDaysForTutor = new RelayCommand(x =>
        {
            if (SelectedTutorFromComboBox != null)
            {
                UpdateForTutor();
            }
        }));

        public string _day1;
        public string _day2;
        public string _day3;
        public string _day4;
        public string _day5;
        public string _day6;
        public string _day7;

        public string Day1
        {
            get => _day1;
            set
            {
                _day1 = value;
                OnPropertyChanged();
            }
        }
        public string Day2
        {
            get => _day2;
            set
            {
                _day2 = value;
                OnPropertyChanged();
            }
        }
        public string Day3
        {
            get => _day3;
            set
            {
                _day3 = value;
                OnPropertyChanged();
            }
        }
        public string Day4
        {
            get => _day4;
            set
            {
                _day4 = value;
                OnPropertyChanged();
            } 
        }
        public string Day5
        {
            get => _day5;
            set
            {
                _day5 = value;
                OnPropertyChanged();
            }
        }
        public string Day6
        {
            get => _day6;
            set
            {
                _day6 = value;
                OnPropertyChanged();
            }
        }
        public string Day7
        {
            get => _day7;
            set
            {
                _day7 = value;
                OnPropertyChanged();
            }
        }

        private void Update()
        {
            WeekList = Service.Client.GetSheduleFromDb(new Filter { Filter_ = SelectedGroupFromComboBox, Value = 0 });
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (WeekList.DaysOfObjects[i].ObjectsString[j].Subject == "-1")
                    {
                        WeekList.DaysOfObjects[i].ObjectsString[j].Subject = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Auditorium = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Group = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Tutor = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Type = " ";
                    }
                }
            }
            MondayScheduleList = WeekList.DaysOfObjects[0];
            TuesdayScheduleList = WeekList.DaysOfObjects[1];
            WednesdayScheduleList = WeekList.DaysOfObjects[2];
            ThursdayScheduleList = WeekList.DaysOfObjects[3];
            FridayScheduleList = WeekList.DaysOfObjects[4];
            SaturdayScheduleList = WeekList.DaysOfObjects[5];
            SundayScheduleList = WeekList.DaysOfObjects[6];

        }

        private void UpdateForTutor()
        {
            string[] parts = SelectedTutorFromComboBox.Split('.');
            WeekList = Service.Client.GetSheduleFromDbForTutor(new Filter { Filter_ = "0", Value = Convert.ToInt32(parts[0])});
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (WeekList.DaysOfObjects[i].ObjectsString[j].Subject == "-1")
                    {
                        WeekList.DaysOfObjects[i].ObjectsString[j].Subject = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Auditorium = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Group = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Tutor = " ";
                        WeekList.DaysOfObjects[i].ObjectsString[j].Type = " ";
                    }
                }
            }
            MondayScheduleList = WeekList.DaysOfObjects[0];
            TuesdayScheduleList = WeekList.DaysOfObjects[1];
            WednesdayScheduleList = WeekList.DaysOfObjects[2];
            ThursdayScheduleList = WeekList.DaysOfObjects[3];
            FridayScheduleList = WeekList.DaysOfObjects[4];
            SaturdayScheduleList = WeekList.DaysOfObjects[5];
            SundayScheduleList = WeekList.DaysOfObjects[6];

            Day1 = MondayScheduleList.ObjectsString[0].Dates.Day + "." + MondayScheduleList.ObjectsString[0].Dates.Month + "." + MondayScheduleList.ObjectsString[0].Dates.Year;
            Day2 = TuesdayScheduleList.ObjectsString[1].Dates.Day + "." + TuesdayScheduleList.ObjectsString[1].Dates.Month + "." + TuesdayScheduleList.ObjectsString[1].Dates.Year;
            Day3 = WednesdayScheduleList.ObjectsString[2].Dates.Day + "." + WednesdayScheduleList.ObjectsString[2].Dates.Month + "." + WednesdayScheduleList.ObjectsString[2].Dates.Year;
            Day4 = ThursdayScheduleList.ObjectsString[3].Dates.Day + "." + ThursdayScheduleList.ObjectsString[3].Dates.Month + "." + ThursdayScheduleList.ObjectsString[3].Dates.Year;
            Day5 = FridayScheduleList.ObjectsString[4].Dates.Day + "." + FridayScheduleList.ObjectsString[4].Dates.Month + "." + FridayScheduleList.ObjectsString[4].Dates.Year;
            Day6 = SaturdayScheduleList.ObjectsString[5].Dates.Day + "." + SaturdayScheduleList.ObjectsString[5].Dates.Month + "." + SaturdayScheduleList.ObjectsString[5].Dates.Year;
            Day7 = SundayScheduleList.ObjectsString[6].Dates.Day + "." + SundayScheduleList.ObjectsString[6].Dates.Month + "." + SundayScheduleList.ObjectsString[6].Dates.Year;
        }

        private void UpdateDays()
        {
            Update();
            Day1 = MondayScheduleList.ObjectsString [0].Dates.Day + "." + MondayScheduleList.ObjectsString [0].Dates.Month + "." + MondayScheduleList.ObjectsString [0].Dates.Year;
            Day2 = TuesdayScheduleList.ObjectsString [1].Dates.Day + "." + TuesdayScheduleList.ObjectsString [1].Dates.Month + "." + TuesdayScheduleList.ObjectsString [1].Dates.Year;
            Day3 = WednesdayScheduleList.ObjectsString [2].Dates.Day + "." + WednesdayScheduleList.ObjectsString [2].Dates.Month + "." + WednesdayScheduleList.ObjectsString [2].Dates.Year;
            Day4 = ThursdayScheduleList.ObjectsString [3].Dates.Day + "." + ThursdayScheduleList.ObjectsString [3].Dates.Month + "." + ThursdayScheduleList.ObjectsString [3].Dates.Year;
            Day5 = FridayScheduleList.ObjectsString [4].Dates.Day + "." + FridayScheduleList.ObjectsString [4].Dates.Month + "." + FridayScheduleList.ObjectsString [4].Dates.Year;
            Day6 = SaturdayScheduleList.ObjectsString [5].Dates.Day + "." + SaturdayScheduleList.ObjectsString [5].Dates.Month + "." + SaturdayScheduleList.ObjectsString [5].Dates.Year;
            Day7 = SundayScheduleList.ObjectsString [6].Dates.Day + "." + SundayScheduleList.ObjectsString [6].Dates.Month + "." + SundayScheduleList.ObjectsString [6].Dates.Year;

        }

        public SheduleArrayByWeek WeekList = new SheduleArrayByWeek();
        private SheduleArrayByDay _mondayScheduleList;
        public SheduleArrayByDay MondayScheduleList
        {
            get { return _mondayScheduleList; }
            set { _mondayScheduleList = value; OnPropertyChanged(); }
        }
        private SheduleArrayByDay _tuesdayScheduleList;
        public SheduleArrayByDay TuesdayScheduleList
        {
            get { return _tuesdayScheduleList; }
            set { _tuesdayScheduleList = value; OnPropertyChanged(); }
        }
        private SheduleArrayByDay _wednesdayScheduleList;
        public SheduleArrayByDay WednesdayScheduleList
        {
            get { return _wednesdayScheduleList; }
            set { _wednesdayScheduleList = value; OnPropertyChanged(); }
        }
        private SheduleArrayByDay _thursdayScheduleList;
        public SheduleArrayByDay ThursdayScheduleList
        {
            get { return _thursdayScheduleList; }
            set { _thursdayScheduleList = value; OnPropertyChanged(); }
        }
        private SheduleArrayByDay _fridayScheduleList;
        public SheduleArrayByDay FridayScheduleList
        {
            get { return _fridayScheduleList; }
            set { _fridayScheduleList = value; OnPropertyChanged(); }
        }
        private SheduleArrayByDay _saturdayScheduleList;
        public SheduleArrayByDay SaturdayScheduleList
        {
            get { return _saturdayScheduleList; }
            set { _saturdayScheduleList = value; OnPropertyChanged(); }
        }
        private SheduleArrayByDay _sundayScheduleList;
        public SheduleArrayByDay SundayScheduleList
        {
            get { return _sundayScheduleList; }
            set { _sundayScheduleList = value; OnPropertyChanged(); }
        }
    }
}
