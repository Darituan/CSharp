using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab01.Tools;
using Lab01.Tools.Managers;
using Lab01.Models;
namespace Lab01.ViewModels.AgeAndZodiac
{
    internal class AgeAndZodiacViewModel: BaseViewModel
    {
        #region Fields
        private DateTime? _userBirthDate;
        private string _userAge;
        private string _userWesternZodiac;
        private string _userChineseZodiac;

        #region Commands
        private RelayCommand<object> _showDateInfoCommand;
        private RelayCommand<object> _closeCommand;
        #endregion
        #endregion

        #region Properties

        public DateTime? UserBirthDate
        {
            get => _userBirthDate;
            set
            {
                _userBirthDate = value;
                OnPropertyChanged();
            }
        }
        
        public string UserAge
        {
            get => _userAge;
            set
            {
                _userAge = value;
                OnPropertyChanged();
            }
        }
        
        public string UserWesternZodiac
        {
            get => _userWesternZodiac;
            set
            {
                _userWesternZodiac = value;
                OnPropertyChanged();
            }
        }
        
        public string UserChineseZodiac
        {
            get => _userChineseZodiac;
            set
            {
                _userChineseZodiac = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public RelayCommand<object> ShowDateInfoCommand
        {
            get
            {
                return _showDateInfoCommand ??= new RelayCommand<object>(
                    ShowDateInfoImplementation, o => CanExecuteCommand());
            }
        }

        public RelayCommand<object> CloseCommand => _closeCommand ??= new RelayCommand<object>(o => Environment.Exit(0));

        #endregion
        #endregion

        private bool CanExecuteCommand()
        {
            return _userBirthDate != null;
        }

        private void ShowDateInfo()
        {
            // sleep is used in order to show that async await actually works
            Thread.Sleep(2000);
            try
            {
                var user = new User(UserBirthDate);
                UserAge = $"Age: {user.AgeInYears().ToString()}";
                UserWesternZodiac = $"Western zodiac: {user.WesternZodiac}";
                UserChineseZodiac = $"Chinese zodiac: {user.ChineseZodiac}";
                if (user.Birthday()) MessageBox.Show("Happy Birthday! May all your dreams come true!");
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private async void ShowDateInfoImplementation(object obj)
        {
            UserAge = string.Empty;
            UserWesternZodiac = string.Empty;
            UserChineseZodiac = string.Empty;
            LoaderManager.Instance.ShowLoader();
            await Task.Run(ShowDateInfo);
            LoaderManager.Instance.HideLoader();
        }
    }
}