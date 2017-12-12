namespace XFLabsPCL.Services
{
    public interface INetworkConnectionCheckerService
    {
        /// <summary>
		/// Gets or sets a value indicating whether this instance is connected.
		/// </summary>
		bool IsConnected { get; set; }

        /// <summary>
        /// Checks connection.
        /// </summary>
        bool Check();
    }
}
