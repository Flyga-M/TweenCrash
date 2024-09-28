using Blish_HUD.Content;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweenCrash.UI.Views;

namespace TweenCrash.UI.Windows
{
    public class TweenUsingWindow : StandardWindow
    {
        public TweenUsingWindow(AsyncTexture2D background, Rectangle windowRegion, Rectangle contentRegion) : base(background, windowRegion, contentRegion)
        {
            this.Show(new WindowView());
        }
    }
}
