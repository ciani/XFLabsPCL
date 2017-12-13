[assembly: Xamarin.Forms.Dependency(typeof(XFLabsPCL.Droid.Services.NetworkConnectionCheckerService))]
namespace XFLabsPCL.Droid.Services
{
    using ReactiveUI;
    using System;
    using System.Diagnostics;
    using XFLabsPCL.Services;


    public class NetworkConnectionCheckerService : ReactiveObject, INetworkConnectionCheckerService
    {
        private NetworkStatusMonitor networkStatusMonitor;
        private bool isConnected;


        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionChecker_Droid"/> class.
        /// </summary>
        public NetworkConnectionCheckerService()
        {
            NetworkStatusMonitor.Start();
            NetworkStatusMonitor.NetworkStatusChanged += NetworkStatusMonitor_NetworkStatusChanged;
        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected {
            get { return isConnected; }
            set { this.RaiseAndSetIfChanged(ref isConnected, value); }
        }

        /// <summary>
        /// Gets the network status monitor.
        /// </summary>
        /// <value>
        /// The network status monitor.
        /// </value>
        protected NetworkStatusMonitor NetworkStatusMonitor
        {
            get
            {
                networkStatusMonitor = networkStatusMonitor ?? new NetworkStatusMonitor();
                return networkStatusMonitor;
            }
        }

        /// <summary>
        /// Checks connection.
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            try
            {
                IsConnected =
                    NetworkStatusMonitor.State == NetworkState.ConnectedData ||
                    NetworkStatusMonitor.State == NetworkState.ConnectedWifi ?
                    true : false;
                if (IsConnected != isConnected)
                    isConnected = IsConnected;

                return IsConnected;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void NetworkStatusMonitor_NetworkStatusChanged(object sender, EventArgs e)
        {
            if (Check())
            {
                try
                {
                    Debug.Write("NetworkConnectionChecker.Check");
                }
                catch (Exception ex)
                {
                    Debug.Write("Error in NetworkConnectionChecker.Check");
                }
            }
        }
    }
}