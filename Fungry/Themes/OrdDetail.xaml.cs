using Fungry.ViewMo;

namespace Fungry.Themes;

public partial class OrdDetail : ContentPage
{
	public OrdDetail(Vieworddtl vieworddtl)
	{
		InitializeComponent();
		BindingContext = vieworddtl;
	}
}