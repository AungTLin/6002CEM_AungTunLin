using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fungry.ViewMo;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;

namespace Fungry;

public class Tabbar: ShellRenderer
{
    protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
    {
       return new BadgeShellBottomNavViewAppearenceTracker(this, shellItem);

    }
   
    class BadgeShellBottomNavViewAppearenceTracker : ShellBottomNavViewAppearanceTracker
    {
        private BadgeDrawable _badgeDrawable;
        public BadgeShellBottomNavViewAppearenceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);

            if (_badgeDrawable is null)
            {

                const int storeTabbarCartIndex = 1;

                _badgeDrawable = bottomView.GetOrCreateBadge(storeTabbarCartIndex);
                UpdateBadge(CartVIewModel.TotalCartCount);
                CartVIewModel.TotalCartCountChanged += CartVIewModel_TotalCartCountChanged;
            }

        }

        private void CartVIewModel_TotalCartCountChanged(object? sender, int newCount) => 
            UpdateBadge(newCount);
        

        private void UpdateBadge(int count )
        {
            if (count <= 0)
            {
                _badgeDrawable.SetVisible(false);
            }
            else
            {
                _badgeDrawable.Number = count;
                _badgeDrawable.BackgroundColor = Colors.SeaGreen.ToPlatform();
                _badgeDrawable.BadgeTextColor = Colors.PeachPuff.ToPlatform();
                _badgeDrawable.SetVisible(true);
            }
        }
        protected override void Dispose(bool disposing)
        {
            CartVIewModel.TotalCartCountChanged -= CartVIewModel_TotalCartCountChanged;
            base.Dispose(disposing);
        }
    }
}
