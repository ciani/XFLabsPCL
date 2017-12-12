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
                d(this.Bind(ViewModel, vm => vm.IsLoading, v => v.ColorListView.IsRefreshing));
                d(this.Bind(ViewModel, vm => vm.SelectedColor, v => v.ColorListView.SelectedItem));

                (ViewModel.GetColorsCommand as ICommand).Execute(null);
            });

		}
	}
}