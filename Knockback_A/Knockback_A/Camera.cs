﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knockback_A
{
    public abstract class Camera
    {
        Matrix view;
        Matrix projection;
        public Matrix View
        {
            get { return view; }
            protected set
            {
                view = value;
                generateFrustum();
            }
        }
        public Matrix Projection
        {
            get { return projection; }
            protected set
            {
                projection = value;
                generateFrustum();
            }
        } 
        public BoundingFrustum Frustum { get; private set; }
        protected GraphicsDevice GraphicsDevice { get; set; }

        

        public Camera(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
            generatePerspectiveProjectionMatrix(MathHelper.PiOver4);
        }

        private void generatePerspectiveProjectionMatrix(float FieldOfView)
        {
            PresentationParameters pp = GraphicsDevice.PresentationParameters;

            float aspectRatio = (float)pp.BackBufferWidth / 
                                (float)pp.BackBufferHeight;
            this.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45), aspectRatio, 1.0f, 1000000.0f);
        }
        private void generateFrustum()
        {
            Matrix viewProjection = View * Projection;
            Frustum = new BoundingFrustum(viewProjection);
        }
        public bool BoundingVolumeIsInView(BoundingSphere sphere)
        {
            return (Frustum.Contains(sphere) != ContainmentType.Disjoint);
        }
        public bool BoundingVolumeIsInView(BoundingBox box)
        {
            return (Frustum.Contains(box) != ContainmentType.Disjoint);
        }
        public virtual void Update()
        {

        }
    }
}
