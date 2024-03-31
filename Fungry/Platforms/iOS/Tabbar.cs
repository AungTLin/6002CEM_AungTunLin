using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fungry.ViewMo;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using UIKit;

namespace Fungry;

public class Tabbar : ShellRenderer
{
    protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
    {
        return new BadgeShellTabbarAppearenceTracker();
    }
    class BadgeShellTabbarAppearenceTracker : ShellTabBarAppearanceTracker
    {
        private UITabBarItem? _storeTabbarCart;
        public override void UpdateLayout(UITabBarController controller)
        {
            base.UpdateLayout(controller);
            if (_storeTabbarCart is null)
            {


                const int storeTabbarCartIndex = 1;

                _storeTabbarCart = controller.TabBar.Items?[storeTabbarCartIndex];
                UpdateBadge(CartVIewModel.TotalCartCount);
                CartVIewModel.TotalCartCountChanged += CartVIewModel_TotalCartCount;
                
            }
        }

        private void CartVIewModel_TotalCartCount(object? sender, int newCount)=>
            UpdateBadge(newCount);
        

        private void UpdateBadge(int count)
        {
            if (_storeTabbarCart is not null)
            {
                if(count<=0)
                {
                    _storeTabbarCart.BadgeValue = null;
                }
            else
                {
                    _storeTabbarCart.BadgeValue = count.ToString();
                    _storeTabbarCart.BadgeColor = Colors.SeaGreen.ToPlatform();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            CartVIewModel.TotalCartCountChanged -= CartVIewModel_TotalCartCount;
            base.Dispose(disposing);
        }
    }
}

