﻿namespace Kronos
{
    using System;
    using System.Globalization;
    using Kronos.Properties;

    internal class MainFormPresenter : Presenter<IMainFormView>
    {
        private DateTime lastActTime;
        private TimeSpan totalDuration;

        public MainFormPresenter(IMainFormView view) : base(view)
        {
            view.AddActivity += OnAddActivity;
        }

        public void OnAddActivity(object sender, EventArgs e)
        {
            if (View.Activity.ToUpperInvariant() == "ARRIVED")
            {
                lastActTime = DateTime.Now;
                TimeSpan arrived = lastActTime - lastActTime;
                AddLineToLog(arrived, lastActTime, lastActTime, "Arrived");
                return;
            }

            DateTime currTime = DateTime.Now;
            TimeSpan activityDuration = currTime - lastActTime;
            totalDuration += activityDuration;

            AddLineToLog(activityDuration, lastActTime, currTime, View.Activity);

            if (!View.Activity.EndsWith("**", StringComparison.OrdinalIgnoreCase))
            {
                UpdateTotalDuration();
            }

            lastActTime = currTime;

            View.Activity = string.Empty;
        }

        protected override void OnViewLoad(object sender, EventArgs e)
        {
            View.Time = string.Empty;
            lastActTime = DateTime.Now;
        }

        private void AddLineToLog(TimeSpan duration, DateTime startTime, DateTime endTime, string activity)
        {
            string durationSring = string.Format(CultureInfo.CurrentCulture, Resources.DurationF, duration.Hours.ToString(), duration.Minutes.ToString());
            string message = string.Format(CultureInfo.CurrentCulture, Resources.ActLogF, durationSring, startTime.ToShortTimeString(), endTime.ToShortTimeString(), activity, Environment.NewLine);
            View.ActivityLog += message;
        }

        private void UpdateTotalDuration() => View.Time = string.Format(CultureInfo.CurrentCulture, Resources.DurationF, totalDuration.Hours.ToString(), totalDuration.Minutes.ToString());
    }
}