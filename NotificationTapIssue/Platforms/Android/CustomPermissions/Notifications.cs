using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Platform;

namespace NotificationTapIssue.Platforms.Android.CustomPermissions;

public class Notifications : Microsoft.Maui.ApplicationModel.Permissions.BasePlatformPermission
{
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions
    {
        get
        {
            List<(string, bool)> list = new List<(string, bool)>();
            if (OperatingSystem.IsAndroidVersionAtLeast(33) && Microsoft.Maui.ApplicationModel.Platform.AppContext.TargetSdkVersion() >= 33)
            {
                list.Add(("android.permission.POST_NOTIFICATIONS", true));
            }

            return list.ToArray();
        }
    }
}
