using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class Alldetails : ContentPage
{
	public Alldetails(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();
		BindingContext = detailsViewModel;
	}
}