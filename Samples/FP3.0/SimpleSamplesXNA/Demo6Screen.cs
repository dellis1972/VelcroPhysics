﻿using System.Text;
using FarseerPhysics.DemoBaseXNA;
using FarseerPhysics.DemoBaseXNA.DemoShare;
using FarseerPhysics.DemoBaseXNA.ScreenSystem;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace SimpleSamplesXNA
{
    public class Demo6Screen : GameScreen, IDemoScreen
    {
        private Spider[] _spiders;
        private Agent _agent;

        #region IDemoScreen Members

        public string GetTitle()
        {
            return "Demo6: Dynamic Angle Joints";
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("This demo demonstrates the use of revolute joints ");
            sb.AppendLine("combined with angle joints that have a dynamic ");
            sb.AppendLine("target angle");
            sb.AppendLine(string.Empty);
            sb.AppendLine("GamePad:");
            sb.AppendLine("  -Rotate: left and right triggers");
            sb.AppendLine("  -Move: left thumbstick");
            sb.AppendLine(string.Empty);
            sb.AppendLine("Mouse");
            sb.AppendLine("  -Hold down left button and drag");
            return sb.ToString();
        }

        #endregion

        public override void Initialize()
        {
            World = new World(new Vector2(0, -20));
            base.Initialize();
        }

        public override void LoadContent()
        {
            _agent = new Agent(World, new Vector2(0, -10));
            _spiders = new Spider[8];

            for (int i = 0; i < _spiders.Length; i++)
            {
                _spiders[i] = new Spider(World, new Vector2(0, ((i + 1) * 3) - 7));
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (IsActive)
            {
                for (int i = 0; i < _spiders.Length; i++)
                {
                    _spiders[i].Update(gameTime);
                }
            }

            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(InputState input)
        {
            if (input.CurrentGamePadState.IsConnected)
            {
                Vector2 force = 1000 * input.CurrentGamePadState.ThumbSticks.Left;
                _agent.Body.ApplyForce(force);

                float rotation = 400 * input.CurrentGamePadState.Triggers.Left;
                _agent.Body.ApplyTorque(rotation);

                rotation = -400 * input.CurrentGamePadState.Triggers.Right;
                _agent.Body.ApplyTorque(rotation);
            }

            base.HandleInput(input);
        }
    }
}