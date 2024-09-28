using Blish_HUD;
using Blish_HUD.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TweenCrash.UI.Controls
{
    public class TweenUsingControl : Control
    {
        private static Random _globalRandom = new Random();

        private Glide.Tween _tween;

        private int _animatedProperty;

        private int _currentValue = 0;

        public override void RecalculateLayout()
        {
            ResetTween();
        }

        private void ResetTween()
        {
            int oldValue = _currentValue;

            RecalculateValue();

            if (oldValue != _currentValue)
            {
                _tween?.CancelAndComplete();
                _tween = null;

                _animatedProperty = oldValue;

                _tween = Animation.Tweener.Tween(this, new { _animatedProperty = _currentValue }, 0.65f)
                    .Ease(Glide.Ease.QuintIn);
            }
        }

        private void RecalculateValue()
        {
            _currentValue = _globalRandom.Next(0, 50);
        }

        protected override void Paint(SpriteBatch spriteBatch, Rectangle bounds)
        {
            spriteBatch.DrawStringOnCtrl(this, _animatedProperty.ToString(), Content.DefaultFont14, bounds, Color.Black);
        }

        protected override void DisposeControl()
        {
            _tween?.CancelAndComplete();
            _tween = null;
        }
    }
}
