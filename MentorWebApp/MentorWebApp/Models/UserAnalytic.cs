using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorWebApp.Models
{
    public class UserAnalytic : Analytic
    {
        public UserAnalytic()
        {
            NewIdentity = Guid.NewGuid().ToString();
        }

        public UserAnalytic(string userId)
        {
            UserId = userId;
            NewIdentity = Guid.NewGuid().ToString();
            WeekLoginCheck = new List<bool>(7);
            ResetWeekStats();
            NumberOfQuestions = 0;
            NumberOfReplies = 0;
        }

        //Count is the number of times this person has logged on

        //Last time they logged on
        public DateTime LastLoginDate { get; set; }

        //Start of the current week
        [NotMapped]
        public DateTime WeekStartDate { get; set; }

        //User Id
        public string UserId { get; set; }

        public int NumberOfQuestions { get; set; }
        public int NumberOfReplies { get; set; }


        //Sunday = 0, Monday = 1, etc...
        public List<bool> WeekLoginCheck { get; set; }


        /*
         * 
         * Want to track which days this user logged in for this week (Sun to Sat)
         * Eg: if they log in on Monday then it makes the 2nd (i=1) item in the list true. And same for the rest of the days.
         * This stat change only happens once a day at most (isnt affected by multiple logins on a day)
         * The weeks stats must reset after a week. NOTE: the week interval doesnt shift day by day, it always starts on the last Sunday and ends on the next Saturday.
         * If they havent logged after the weeks start (the last sunday), then their week stats are reset
         * The login Count is incremented everytime they login (even if its on the same day)
         * 
         */


        public void UserLogin()
        {
            var todayDate = DateTime.Today;
            var today = todayDate.DayOfWeek;

            //If they already logged in today then no need to continue
            if (LastLoginDate.Equals(todayDate))
            {
                Count++;
                return;
            }

            CalculateWeekStart();
            if (OutOfDate()) ResetWeekStats();

            //Here we assume that their week check array is the current week they on now.
            //Add one to their login count
            Count++;

            //Set the flag to true for today in the week check
            switch (today)
            {
                case DayOfWeek.Sunday:
                    WeekLoginCheck[0] = true;
                    break;
                case DayOfWeek.Monday:
                    WeekLoginCheck[1] = true;
                    break;
                case DayOfWeek.Tuesday:
                    WeekLoginCheck[2] = true;
                    break;
                case DayOfWeek.Wednesday:
                    WeekLoginCheck[3] = true;
                    break;
                case DayOfWeek.Thursday:
                    WeekLoginCheck[4] = true;
                    break;
                case DayOfWeek.Friday:
                    WeekLoginCheck[5] = true;
                    break;
                case DayOfWeek.Saturday:
                    WeekLoginCheck[6] = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            //Finally update their last logged in date
            LastLoginDate = DateTime.Today;
        }

        private void ResetWeekStats()
        {
            //reset their week checks
            for (var i = 0; i < WeekLoginCheck.Count; i++)
                WeekLoginCheck[i] = false;
        }

        private bool OutOfDate()
        {
            //means they logged in before the start of this week
            return LastLoginDate.CompareTo(WeekStartDate) < 0;
        }


        public void CalculateWeekStart()
        {
            //Calculate the Week Start date. This should be a Sunday always.

            var todayDate = DateTime.Today;
            var today = todayDate.DayOfWeek;

            //make sure its not null
            WeekStartDate = todayDate;

            switch (today)
            {
                case DayOfWeek.Sunday:
                    //If today is sunday then this is the new start of the week.
                    WeekStartDate = todayDate;
                    break;
                case DayOfWeek.Monday:
                    WeekStartDate = todayDate.Subtract(TimeSpan.FromDays(1)).Date;
                    break;
                case DayOfWeek.Tuesday:
                    WeekStartDate = todayDate.Subtract(TimeSpan.FromDays(2)).Date;
                    break;
                case DayOfWeek.Wednesday:
                    WeekStartDate = todayDate.Subtract(TimeSpan.FromDays(3)).Date;
                    break;
                case DayOfWeek.Thursday:
                    WeekStartDate = todayDate.Subtract(TimeSpan.FromDays(4)).Date;
                    break;
                case DayOfWeek.Friday:
                    WeekStartDate = todayDate.Subtract(TimeSpan.FromDays(5)).Date;
                    break;
                case DayOfWeek.Saturday:
                    WeekStartDate = todayDate.Subtract(TimeSpan.FromDays(6)).Date;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            //Make sure Start of the week is Sunday
            if (!WeekStartDate.DayOfWeek.Equals(DayOfWeek.Sunday)) throw new Exception();
        }
    }
}