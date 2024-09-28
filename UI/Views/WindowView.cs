using Blish_HUD.Controls;
using Blish_HUD.Graphics.UI;
using Blish_HUD.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweenCrash.UI.Views
{
    public class WindowView : View
    {
        private ViewContainer _tweenViewContainer;
        private StandardButton _refreshButton;

        protected override void Build(Container buildPanel)
        {
            _refreshButton = new StandardButton()
            {
                Text = "Refresh View",
                Width = 100,
                Height = 40,
                Parent = buildPanel
            };

            _refreshButton.Click += OnRefreshClicked;

            _tweenViewContainer = new ViewContainer()
            {
                Width = buildPanel.ContentRegion.Width - _refreshButton.Width,
                Height = buildPanel.ContentRegion.Height,
                Left = _refreshButton.Right,
                ShowBorder = true,
                Parent = buildPanel
            };
        }

        private void OnRefreshClicked(object _, MouseEventArgs e)
        {
            _tweenViewContainer.Show(new TweenUsingView());
        }

        protected override void Unload()
        {
            _tweenViewContainer?.Dispose();
            _tweenViewContainer = null;

            if (_refreshButton != null)
            {
                _refreshButton.Click -= OnRefreshClicked;
            }

            _refreshButton?.Dispose();
            _refreshButton= null;
        }
    }
}
