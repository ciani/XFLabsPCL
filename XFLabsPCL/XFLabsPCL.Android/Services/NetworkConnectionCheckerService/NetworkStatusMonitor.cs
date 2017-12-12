namespace XFLabsPCL.Droid.Services
{
    using Android.App;
    using Android.Content;
    using Android.Net;
    using System;
    public class NetworkStatusMonitor
    {
        private NetworkState state;

        public NetworkStatusMonitor()
        {
        }

        public NetworkState State
        {
            get
            {
                UpdateNetworkStatus();

                return state;
            }
        }

        public void UpdateNetworkStatus()
        {
            state = NetworkState.Unknown;

            // Retrieve the connectivity manager service
            var connectivityManager = (ConnectivityManager)
                Application.Context.GetSystemService(Context.ConnectivityService);

            // Check if the network is connected or connecting.
            // This means that it will be available, 
            // or become available in a few seconds.
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;

            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                // Now that we know it's connected, determine if we're on WiFi or something else.
                state = activeNetworkInfo.Type == ConnectivityType.Wifi ?
                    NetworkState.ConnectedWifi : NetworkState.ConnectedData;
            }
            else
            {
                state = NetworkState.Disconnected;
            }
        }
        
        public event EventHandler NetworkStatusChanged;

        private NetworkStatusBroadcastReceiver broadcastReceiver;

        public void Start()
        {
            if (broadcastReceiver != null)
            {
                throw new InvalidOperationException(
                    "Network status monitoring already active.");
            }

            // Create the broadcast receiver and bind the event handler
            // so that the app gets updates of the network connectivity status
            broadcastReceiver = new NetworkStatusBroadcastReceiver();
            broadcastReceiver.ConnectionStatusChanged += OnNetworkStatusChanged;

            // Register the broadcast receiver
            Application.Context.RegisterReceiver(broadcastReceiver,
                new IntentFilter(ConnectivityManager.ConnectivityAction));
        }

        void OnNetworkStatusChanged(object sender, EventArgs e)
        {
            var currentStatus = state;

            UpdateNetworkStatus();

            if (currentStatus != state && NetworkStatusChanged != null)
            {
                NetworkStatusChanged(this, EventArgs.Empty);
            }
        }

        public void Stop()
        {
            if (broadcastReceiver == null)
            {
                throw new InvalidOperationException("Network status monitoring not active.");
            }

            // Unregister the receiver so we no longer get updates.
            Application.Context.UnregisterReceiver(broadcastReceiver);

            // Set the variable to nil, so that we know the receiver is no longer used.
            broadcastReceiver.ConnectionStatusChanged -= OnNetworkStatusChanged;
            broadcastReceiver = null;
        }
    }

    public enum NetworkState
    {
        Unknown,
        ConnectedWifi,
        ConnectedData,
        Disconnected
    }
}
