using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;
using System;

namespace Retribution.Entities.Components
{
    class PlayerController : Component
    {
        Direction m_onWall;
        private float m_verticalVel;
        private float m_horizontalVel;
        private int m_health;
        private bool m_doubleJump;
        private bool m_jumped;
        private Direction m_faceing;
        private bool attacking;
        private const float MOVESPEED = 10.0f;
        private const float GRAVITY = 0.5f;
        private const float JUMPVEL = 10.0f;
        private const float HORIZONTAL_DAMPING = 0.1f;

        public PlayerController(Entity entity) : base(entity, ComponentType.PlayerController)
        {
            m_onWall = Direction.None;
            m_verticalVel = 0;
            m_horizontalVel = 0;
            m_health = 3;
            m_doubleJump = true;
            m_jumped = false;
        }

        public override void Init()
        {
        }

        public override void PreUpdate(float dt)
        {
        }

        public int GetHealth()
        {
            return m_health;
        }

        public override void Update(float dt)
        {
            if (Input.GetKeyDown(Keys.Space))
            {
                if (m_onWall == Direction.Down)
                {
                    m_verticalVel = -JUMPVEL;
                    m_jumped = true;
                }
                else if (m_onWall == Direction.Left)
                {
                    m_horizontalVel = JUMPVEL;
                    m_jumped = true;
                }
                else if (m_onWall == Direction.Right)
                {
                    m_horizontalVel = -JUMPVEL;
                    m_jumped = true;
                }
                else if (m_onWall == Direction.Up)
                {
                    m_verticalVel = JUMPVEL;
                    m_jumped = true;
                }
                else
                {
                    if (m_doubleJump)
                    {
                        m_doubleJump = false;
                        m_jumped = false;
                        m_verticalVel = -JUMPVEL;
                    }
                }
            }
            else if(Input.GetKeyUp(Keys.Space) && m_jumped)
            {
                if (m_verticalVel < 0)
                    m_verticalVel = 0;
            }

            if(m_onWall == Direction.Left || m_onWall == Direction.Right)
                if(Input.GetKey(Keys.W))
                    m_entity.Move(0, -10);

            if (m_onWall != Direction.Down && m_onWall != Direction.None)
                if (Input.GetKey(Keys.S))
                    m_entity.Move(0, 10);

            if(m_onWall != Direction.Left)
                if (Input.GetKey(Keys.A))
                    m_entity.Move(-10,0);
                
            if(m_onWall != Direction.Right)
                if(Input.GetKey(Keys.D))
                    m_entity.Move(10, 0);

            if (m_onWall != Direction.None)
            {
                //so collision system thinks your on the wall
                if(m_onWall == Direction.Left)
                    m_entity.Move(-1, 0);
                else if (m_onWall == Direction.Right)
                    m_entity.Move(1, 0);
                else if (m_onWall == Direction.Up)
                    m_entity.Move(0, -1);
                else if (m_onWall == Direction.Down)
                    m_entity.Move(0, 1);
            }
            else
            {
                m_verticalVel += GRAVITY;
            }
            if (m_horizontalVel > 0)
                m_horizontalVel -= HORIZONTAL_DAMPING;
            else if (m_horizontalVel < 0)
                m_horizontalVel += HORIZONTAL_DAMPING;

            m_entity.Move(m_horizontalVel, m_verticalVel);

        }


        public override void PostUpdate(float dt)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public override void Msg(string msg, params object[] args)
        {
            if(msg == "Collision")
            {
                var direction1 = (Direction)args[0];
                var direction2 = (Direction)args[1];

                if (direction1 == Direction.None && direction2 == Direction.None)
                {
                    m_onWall = Direction.None;
                }
                else
                {
                    m_jumped = false;
                    m_doubleJump = true;
                    if (m_onWall != direction1)
                    {
                        m_verticalVel = 0;
                        m_horizontalVel = 0;
                        if (direction2 == Direction.Down)
                            m_onWall = Direction.Down;
                        else
                            m_onWall = direction1;
                    }
                }
            }
            else if(msg == "Hit")
            {
                var entity = (Entity)args[0];
                var direction = (Direction)args[1];

                if(entity.Tag == "Sticky")
                {
                    m_jumped = false;
                    m_doubleJump = true;
                    if (m_onWall != direction)
                    {
                        m_verticalVel = 0;
                        m_horizontalVel = 0;
                        m_onWall = direction;
                    }
                }

                if(entity.Tag == "Bad")
                {
                    
                    if (direction == Direction.Down)
                    {
                        entity.Msg("TakeDmg", 100);
                        m_doubleJump = true;
                        m_jumped = false;
                        m_verticalVel = -JUMPVEL;

                        if (m_onWall != direction)
                        {
                           // m_horizontalVel = 0;
                            m_onWall = direction;
                        }
                    }
                    else if(direction == Direction.Up)
                        m_verticalVel = 0;
                    else if (direction == Direction.Right || direction == Direction.Left)
                        m_horizontalVel = 0;

                    
                }
            }
            else if(msg == "TakeDmg")
            {
                m_health -= (int)args[0];
            }

        }
    }
}
