using System;
using System.Collections;
using System.Linq;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
#if UNITY_IOS || UNITY_IPHONE
using Unity.Notifications.iOS;
#endif

using UnityEngine;

public class NotificationManager : SingletonTemplate<NotificationManager>
{
    private const string CHANNEL_18H = "notification-18h";
    private const int ID_18H = 1000;

    public void Init()
    {
        ScheduleLocal();
    }

    public void ScheduleLocal()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
#if UNITY_ANDROID
                ScheduleAndroid18H();
#endif
                break;
            case RuntimePlatform.IPhonePlayer:
#if UNITY_IOS || UNITY_IPHONE
                StartCoroutine(ScheduleIos18H());
#endif
                break;
        }
    }

#if UNITY_ANDROID
    void ScheduleAndroid18H()
    {
        var status = AndroidNotificationCenter.CheckScheduledNotificationStatus(ID_18H);
        switch (status)
        {
            case NotificationStatus.Unavailable:
            case NotificationStatus.Unknown:
                CreateAndroid18H();
                break;
        }
    }

    void CreateAndroid18H()
    {
        //channel
        var channel = new AndroidNotificationChannel()
        {
            Id = CHANNEL_18H,
            Name = "Channel 18h",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        //notification
        DateTime now = DateTime.Now;
        DateTime time = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0);
        var notification = new AndroidNotification
        {
            Title = "Time to relax now!",
            Text = "Let's play and enjoy",
            FireTime = time,
            RepeatInterval = new TimeSpan(1, 0, 0, 0)
        };
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, CHANNEL_18H, ID_18H);
    }
#endif

#if UNITY_IOS || UNITY_IPHONE

    IEnumerator ScheduleIos18H()
    {
        var authorizationOption = AuthorizationOption.Alert | AuthorizationOption.Badge;
        using (var req = new AuthorizationRequest(authorizationOption, true))
        {
            while (!req.IsFinished)
            {
                yield return null;
            };
            if (req.Granted)
                CreateIos18H();
        }
    }

    void CreateIos18H()
    {
        iOSNotification[] arr = iOSNotificationCenter.GetScheduledNotifications();
        if (arr.Any(t => t.Identifier.Equals(ID_18H.ToString())))
        {
            return;
        }
        var calendarTrigger = new iOSNotificationCalendarTrigger()
        {
            // Year = 2020,
            // Month = 6,
            //Day = 1,
            Hour = 18,
            Minute = 0,
            Second = 0,
            Repeats = true
        };
        var notification = new iOSNotification()
        {
            Identifier = ID_18H.ToString(),
            Title = "Time to relax with BALL now!",
            Body = "Let's play and enjoy",
            Subtitle = "Let's play and enjoy",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_local_repeat",
            ThreadIdentifier = "thread_local_repeat",
            Trigger = calendarTrigger,
        };
        iOSNotificationCenter.ScheduleNotification(notification);
    }
#endif
}