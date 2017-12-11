namespace XFLabsPCL.Features.Login
{
    using System;
    using ReactiveUI;
    using Xamarin.Forms.Xaml;
    using Xamarin.Forms;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
            this.WhenActivated(d =>
            {
                d(this.Bind(ViewModel, vm => vm.User, v => v.EntryUser.Text));
                d(this.Bind(ViewModel, vm => vm.Pass, v => v.EntryPass.Text));

                d(this.BindCommand(ViewModel, vm => vm.DoLogin, v => v.BtnLogin));
                d(this.Bind(ViewModel, vm => vm.IsLoading, v => v.ActIsLoading.IsVisible));

                d(this.WhenAny(v => v.EntryPass.Text, x => x.Value)
                  .Subscribe(pass =>
                  {
                      if (!string.IsNullOrWhiteSpace(pass))
                          EntryPass.BackgroundColor = Color.Green;
                      else
                          EntryPass.BackgroundColor = Color.White;

                  }));


            });
        }
    }
}