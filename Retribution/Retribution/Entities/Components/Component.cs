using Microsoft.Xna.Framework.Graphics;

namespace Retribution.Entities.Components
{
    abstract class Component
    {
        abstract public void Update(float dt);
        abstract public void Draw(SpriteBatch spriteBatch);
        abstract public void SendMsg(string msg);
    }
}
