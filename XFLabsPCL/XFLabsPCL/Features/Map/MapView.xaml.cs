namespace XFLabsPCL.Features.Map
{
    using ReactiveUI;
    using System.Windows.Input;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView
    {
        public MapView()
        {
            InitializeComponent();
            BindingContext = new MapViewModel();

            this.WhenActivated(d => 
            {
                (ViewModel.GetGPSPositionCommand as ICommand).Execute(null);
            });
        }
    }
}