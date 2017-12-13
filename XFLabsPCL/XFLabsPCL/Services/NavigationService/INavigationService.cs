namespace XFLabsPCL.Services
{
    using System.Threading.Tasks;

    public interface INavigationService
    {
        /// <summary>
        /// Navigates to main view asynchronous.
        /// </summary>
        /// <returns></returns>
        Task NavigateToMainViewAsync();

        /// <summary>
        /// Navigates to color list asynchronous.
        /// </summary>
        /// <returns></returns>
        Task NavigateToColorListAsync();

        /// <summary>
        /// Navigates to map asynchronous.
        /// </summary>
        /// <returns></returns>
        Task NavigateToMapAsync();

        /// <summary>
        /// Pops the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task PopAsync();
    }
}
