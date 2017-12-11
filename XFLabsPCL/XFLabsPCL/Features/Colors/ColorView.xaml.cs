namespace XFLabsPCL.Features.Colors
{
    using ReactiveUI;
    using System.Windows.Input;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ColorView
	{
		public ColorView ()
		{
			InitializeComponent ();
            BindingContext = new ColorViewModel();
            this.WhenActivated(d => 
            {
                d(this.OneWayBind(ViewModel, vm => vm.ColorList, v => v.ColorListView.ItemsSource));


                (ViewModel.GetColorsCommand as ICommand).Execute(null);
            });

		}
	}
}