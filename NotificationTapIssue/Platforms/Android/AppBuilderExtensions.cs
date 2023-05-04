using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.Maui.LifecycleEvents;
using NotificationTapIssue.Platforms.Android.CustomPermissions;
using AndroidNotificationManager = Android.App.NotificationManager;

namespace NotificationTapIssue.Platforms.Android;

internal static class AppBuilderExtensions
{
    private static bool _hasSetupNotifications;

    public static MauiAppBuilder AddNotifications(this MauiAppBuilder builder)
    {
        builder.ConfigureLifecycleEvents(lce =>
        {
            lce.AddAndroid(a =>
            {
                a.OnCreate(OnCreate);
                a.OnDestroy(OnDestroy);
            });
        });

        return builder;
    }

    public static void OnCreate(Activity activity, Bundle? savedInstanceState)
    {
        if (_hasSetupNotifications)
        {
            return;
        }

        _hasSetupNotifications = true;

        MainThread.InvokeOnMainThreadAsync(async () =>
        {
            if (!OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                return;
            }

            var status = await Permissions.RequestAsync<Notifications>().ConfigureAwait(true);
            if (status == PermissionStatus.Granted)
            {
                if (activity.ApplicationContext.GetSystemService(Context.NotificationService) is AndroidNotificationManager notificationManager)
                {
                    using var channel = new NotificationChannel(
                        "MyChannel",
                        "Test Channel",
                        NotificationImportance.Default)
                    {
                        LockscreenVisibility = NotificationVisibility.Public,
                        Description = "Test chan description.",
                    };

                    channel.SetShowBadge(false);
                    notificationManager.CreateNotificationChannel(channel);
                }

                var startServiceIntent = new Intent(activity.ApplicationContext, typeof(MyService));
                startServiceIntent.SetAction(MyService.IntentActionStart);
                activity.ApplicationContext.StartService(startServiceIntent);
            }
        });

    }

    public static void OnDestroy(Activity activity)
    {

    }
}
