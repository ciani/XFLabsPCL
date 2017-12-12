namespace XFLabsPCL.Droid.Services
{
    using Android.Content;
    using System;

    [BroadcastReceiver()]
    [Android.App.IntentFilter(new string[] { "android.net.conn.CONNECTIVITY_CHANGE" })]
    public class NetworkStatusBroadcastReceiver : BroadcastReceiver
    {
        public event EventHandler ConnectionStatusChanged;

        public override void OnReceive(Context context, Intent intent)
        {
            if (ConnectionStatusChanged != null)
                ConnectionStatusChanged(this, EventArgs.Empty);
        }
    }
}