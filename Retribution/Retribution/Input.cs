using Microsoft.Xna.Framework.Input;

namespace Retribution
{
    public static class Input
    {

        static KeyboardState m_lastState, m_curState;

        public static void Init()
        {
            m_lastState = m_curState = Keyboard.GetState();
        }

        public static void Update()
        {
            m_lastState = m_curState;
            m_curState = Keyboard.GetState();
        }

        public static bool GetKey(Keys keycode)
        {
            return m_curState.IsKeyDown(keycode);
        }

        public static bool GetKeyDown(Keys keycode)
        {
            return m_curState.IsKeyDown(keycode) && m_lastState.IsKeyUp(keycode);
        }

        public static bool GetKeyUp(Keys keycode)
        {
            return m_curState.IsKeyUp(keycode) && m_lastState.IsKeyDown(keycode);
        }
    }
}
