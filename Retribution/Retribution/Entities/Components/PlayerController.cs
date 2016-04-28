using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                m_entity.Move(new Microsoft.Xna.Framework.Vector2(10, 10));
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
