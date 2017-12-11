namespace XFLabsPCL.Droid.Services
{
    using Android.App;
    using Android.Content;
    using Android.Util;
    using Firebase.Iid;
    using System;
    using System.Diagnostics;
    using WindowsAzure.Messaging;

    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseRegistrationService : FirebaseInstanceIdService
    {
        private NotificationHub Hub;
        const string TAG = "FirebaseRegistrationService";

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            SendRegistrationTokenToAzureNotificationHub(refreshedToken);
        }

        private void SendRegistrationTokenToAzureNotificationHub(string token)
        {
            // Register notification hub registration
            Subscribe("AccionaNH"
                    , "Endpoint=sb://accionans.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=a8qdFRh2rdlrLDsbXa3xfeb/SkoUSjB4VBdWKgXbNrA="
                    , token);
        }

        public void Subscribe(string hubNotificationPath, string connectionString, string token)
        {
            Hub = new NotificationHub(hubNotificationPath, connectionString, Application.Context);

            try
            {
                Hub.UnregisterAll(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            try
            {
                var hubRegistration = Hub.Register(token);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
