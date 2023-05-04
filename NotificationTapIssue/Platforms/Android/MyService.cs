// <copyright file="LocateService.cs" company="Vismo Ltd">
// Copyright (c) All Rights Reserved
// </copyright>

namespace NotificationTapIssue;

using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

/// <summary>
/// A foregound service which provides the timed location functionality.
/// </summary>
/// <seealso cref="Android.App.Service" />
[Service(Name = "MyService.LocateService")]
public class MyService : Service
{
    /// <summary>
    /// The intent action sent to start the service.
    /// </summary>
    public const string IntentActionStart = "com.myapp.android.jobs.MyService.ACTION_START";

    /// <summary>
    /// The intent action sent to stop the service.
    /// </summary>
    public const string IntentActionStop = "com.myapp.android.jobs.MyService.ACTION_STOP";

    /// <summary>
    /// Return the communication channel to the service.
    /// </summary>
    /// <param name="intent">The Intent that was used to bind to this service.</param>
    /// <returns>
    /// A binder instance.
    /// </returns>
    public override IBinder? OnBind(Intent? intent)
    {
        return null;
    }

    /// <summary>
    /// Called by the system when the service is first created.
    /// </summary>
    public override void OnCreate()
    {
        base.OnCreate();
    }

    /// <inheritdoc/>
    public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
    {
        if (intent != null)
        {
            if (intent.Action == IntentActionStart)
            {
                var notificationIntent = new Intent(this, typeof(MainActivity));
                notificationIntent.AddFlags(ActivityFlags.SingleTop | ActivityFlags.ClearTask);
                var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);

                var notification = new NotificationCompat.Builder(this, "MyChannel")
                .SetContentTitle("Notification Title")
                .SetContentText("Notification Description")
                .SetSmallIcon(Resource.Drawable.DefaultNotificationIcon)
                .SetContentIntent(pendingIntent)
                .SetOngoing(true)
                .SetShowWhen(false)
                .Build();

                StartForeground(10000, notification);
            }
            else if (intent.Action == IntentActionStop)
            {
                StopForeground(StopForegroundFlags.Remove);
                StopSelf();
            }
        }

        return StartCommandResult.Sticky;
    }
}
