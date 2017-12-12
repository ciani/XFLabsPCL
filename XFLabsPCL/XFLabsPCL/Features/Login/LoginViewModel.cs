namespace XFLabsPCL.Features.Login
{
    using ReactiveUI;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using XFLabsPCL.Services;

    public class LoginViewModel : ReactiveObject
    {
        private readonly INavigationService navigationService;
        private string user;
        private string pass;
        private ReactiveCommand doLoginAsync;
        private ObservableAsPropertyHelper<bool> isLoading;


        /// <summary>
        /// Initializes a new instance of the <see cref="LoginViewModel"/> class.
        /// </summary>
        public LoginViewModel()
        {
            navigationService = DependencyService.Get<INavigationService>();
            doLoginAsync = ReactiveCommand.CreateFromTask(DoLoginAsync, CanExecuteLogin());
            doLoginAsync.IsExecuting.ToProperty(this, vm => vm.IsLoading, out isLoading);

        }

        public string User
        {
            get { return user; }
            set { this.RaiseAndSetIfChanged(ref user, value); }
        }

        public string Pass
        {
            get { return pass; }
            set { this.RaiseAndSetIfChanged(ref pass, value); }
        }

        public bool IsLoading => isLoading?.Value ?? false;


        public ReactiveCommand DoLogin => doLoginAsync;

        private async Task DoLoginAsync()
        {
            await navigationService.NavigateToColorListAsync();
        }


        private IObservable<bool> CanExecuteLogin()
        {
            return this.WhenAnyValue(vm => vm.User, vm => vm.Pass, vm => vm.IsLoading, (usr, pwd, ldg) =>
            {
                return !ldg &&
                !string.IsNullOrWhiteSpace(usr) &&
                !string.IsNullOrWhiteSpace(pwd);
            });
        }

    }
}
