using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using TweenCrash.UI.Controls;

namespace TweenCrash.UI.Views
{
    public class TweenUsingView : View
    {
        private FlowPanel _flowPanel;

        protected override void Build(Container buildPanel)
        {
            _flowPanel = new FlowPanel()
            {
                Width = buildPanel.ContentRegion.Width,
                Height = buildPanel.ContentRegion.Height,
                Parent = buildPanel
            };

            for (int i = 0; i < 100; i++)
            {
                TweenUsingControl control = new TweenUsingControl()
                {
                    Width = 70,
                    Height = 50,
                    Parent = _flowPanel
                };
            }
        }

        protected override void Unload()
        {
            _flowPanel?.Dispose();
            _flowPanel = null;
        }
    }
}
