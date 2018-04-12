﻿using Game3.Managers;
using Game3.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3.Sprites
{
    public class Sprite
    {
        #region Fields

        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 _position;
        protected Texture2D _texture;

        #endregion
        #region Properties
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public Input Input;
        public float Speed = 1f;
        public Vector2 Velocity;
        #endregion

        #region Method
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch);
            else throw new Exception("Error");
        }

        protected virtual void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
        }
        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
                _animationManager.Play(_animations["WalkRight"]);
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["WalkLeft"]);
            else if (Velocity.Y > 0)
                _animationManager.Play(_animations["WalkDown"]);
            else if (Velocity.Y < 0)
                _animationManager.Play(_animations["WalkUp"]);
        }
        public Sprite(Dictionary<string,Animation> animations)
        {
            _animations = animations;
                _animationManager = new AnimationManager(_animations.First().Value);
            
        }
        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }
        public virtual void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            SetAnimations();

            _animationManager.Update(gameTime);


            Position += Velocity;
            Velocity = Vector2.Zero;
        }

       
        #endregion

    }
}
