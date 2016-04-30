using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    class PlayerController : Component
    {
        public PlayerController(Entity entity) : base(entity)
        {
        }

        public override void Update(float dt)
        {
            KeyboardState newState = Keyboard.GetState();
            if(newState.IsKeyDown(Keys.W))
            {
                m_entity.Move(0, -10);
            }

            if (newState.IsKeyDown(Keys.S))
            {
                m_entity.Move(0, 10);
            }

            if (newState.IsKeyDown(Keys.A))
            {
                m_entity.Move(-10,0);
            }

            if(newState.IsKeyDown(Keys.D))
            {
                m_entity.Move(10, 0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void SendMsg(string msg)
        {
            
        }
    }
}
