﻿using System;
using System.Globalization;
using System.Text;

namespace Lab01.Models
{
    //#nullable enable
    internal class User
    {
        #region Fields
        // each month has two western zodiac signs in it
        // this array represents the last days of the first zodiac sign of each month
        private static readonly int[] MonthDividingDays = {20, 18, 20, 20, 20, 21, 22, 23, 23, 23, 22, 21};

        private static readonly string[] WesternZodiacs = {"Capricorn", "Aquarius", "Pisces", "Aries", 
                                                            "Taurus", "Gemini", "Cancer", "Leo", 
                                                            "Virgo", "Libra", "Scorpio", "Sagittarius"};

        private static readonly string[] ChineseZodiacs = {"Tiger", "Rabbit", "Dragon", "Snake", 
                                                            "Horse", "Goat", "Monkey", "Rooster", 
                                                            "Dog", "Pig", "Rat", "Ox"};

        private static readonly string[] ChineseElements = {"Yang Water", "Yin Water" ,"Yang Wood", "Yin Wood", 
                                                             "Yang Fire", "Yin Fire", "Yang Earth", "Yin Earth", 
                                                             "Yang Metal", "Yin Metal"};
        // the first year all dates of which work correctly in ChineseLunisolarCalendar
        private static readonly int _anchorYear = 1902;
        private static readonly ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();
        private DateTime _birthDate;
        private string _westernZodiac;
        private string _chineseZodiac;
        #endregion

        #region Properties
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                CheckAge();
                ChangeDependentFields();
            }
        }

        public string WesternZodiac => _westernZodiac;

        public string ChineseZodiac => _chineseZodiac;
        #endregion

        public int AgeInYears()
        {
            var today = System.DateTime.Today;
            var years = today.Year - _birthDate.Year;
            if (_birthDate.Month == today.Month &&
                today.Day < _birthDate.Day
                || today.Month < _birthDate.Month)
            {
                years--;
            }
            return years;
        }

        public bool Birthday()
        {
            var today = System.DateTime.Today;
            return _birthDate.Month == today.Month && _birthDate.Day == today.Day;
        }

        private string DetermineWesternZodiac()
        {
            var index = _birthDate.Month - 1;
            if (_birthDate.Day > MonthDividingDays[index])
                index = (index + 1) % 12;
            return WesternZodiacs[index];
        }

        private string DetermineChineseZodiac()
        {
            var zodiac = new StringBuilder();
            var year = _birthDate.Year < _anchorYear ? _birthDate.Year : ChineseCalendar.GetYear(_birthDate);
            var elementIndex = (year - _anchorYear) % 10;
            if (elementIndex < 0) elementIndex += 10;
            zodiac.Append(ChineseElements[elementIndex]);
            zodiac.Append(" ");
            var zodiacIndex = (year - _anchorYear) % 12;
            if (zodiacIndex < 0) zodiacIndex += 12;
            zodiac.Append(ChineseZodiacs[zodiacIndex]);
            return zodiac.ToString();
        }

        private void ChangeDependentFields()
        {
            _westernZodiac = DetermineWesternZodiac();
            _chineseZodiac = DetermineChineseZodiac();
        }

        private void CheckAge()
        {
            var age = AgeInYears();
            if (age >= 0 && age <= 135) return;
            var message = String.Concat("It seems like You were either born more", 
                " than 135 years ago or in future.", " If this is not a mistake, we are sorry to tell", 
                " this app is probably not designed for You :(");
            Console.WriteLine(message);
            throw new ArgumentException(message);
        }

        internal User(DateTime? birthDate)
        {
            if (birthDate.HasValue)
                BirthDate = birthDate.Value;
            else throw new ArgumentException("Argument should have a value");
        }
    }
}