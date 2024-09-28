using Blish_HUD;
using Blish_HUD.Content;
using Blish_HUD.Controls;
using Blish_HUD.Modules;
using Blish_HUD.Modules.Managers;
using Microsoft.Xna.Framework;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using TweenCrash.UI.Views;

namespace TweenCrash
{
    [Export(typeof(Module))]
    public class TweenCrash : Module
    {
        private static readonly Logger Logger = Logger.GetLogger<TweenCrash>();

        internal static TweenCrash Instance;

        private AsyncTexture2D _cornerIconTexture;
        private AsyncTexture2D _windowBackgroundTexture;
        private CornerIcon _cornerIcon;
        private StandardWindow _window;

        internal SettingsManager SettingsManager => this.ModuleParameters.SettingsManager;
        internal ContentsManager ContentsManager => this.ModuleParameters.ContentsManager;
        internal DirectoriesManager DirectoriesManager => this.ModuleParameters.DirectoriesManager;
        internal Gw2ApiManager Gw2ApiManager => this.ModuleParameters.Gw2ApiManager;

        [ImportingConstructor]
        public TweenCrash([Import("ModuleParameters")] ModuleParameters moduleParameters) : base(moduleParameters)
        {
            Instance = this;
        }

        protected override async Task LoadAsync()
        {
            _cornerIconTexture = AsyncTexture2D.FromAssetId(102439);
            _windowBackgroundTexture = AsyncTexture2D.FromAssetId(155985);

            // just to give the textures some time to load
            await Task.Delay(1000);

            CreateWindow();
            CreateCornerIcon();
        }

        protected override void Update(GameTime gameTime)
        {
            
        }

        
        protected override void Unload()
        {
            Instance = null;
        }
        
        private void CreateWindow()
        {
            Rectangle _fixedWindowRegion = new Rectangle(40, 26, 913, 691);
            Rectangle _fixedContentRegion = new Rectangle(40 + 5, 31, 903 - 5, 675);


            _window = new StandardWindow(_windowBackgroundTexture, _fixedWindowRegion, _fixedContentRegion)
            {
                Parent = GameService.Graphics.SpriteScreen
            };

            _window.Show(new WindowView());
        }

        private void CreateCornerIcon()
        {
            _cornerIcon = new CornerIcon()
            {
                Icon = _cornerIconTexture,
                BasicTooltipText = $"TweenCrash",
                Priority = 534643,
                Parent = GameService.Graphics.SpriteScreen
            };

            // Clicking on the cornerIcon shows/hides the example window
            _cornerIcon.Click += (s, e) => _window.ToggleWindow();
        }


    }
}
