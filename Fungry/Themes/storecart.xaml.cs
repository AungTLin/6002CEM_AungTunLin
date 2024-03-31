using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class storecart : ContentPage
{
	public storecart(CartVIewModel cartVIewModel)
	{
        InitializeComponent();
		BindingContext =cartVIewModel;
	}

   
}