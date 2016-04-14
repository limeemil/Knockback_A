using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knockback_A {
    public class CModel {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public Model Model { get; private set; }
        private Matrix[] modelTransforms;

        private GraphicsDevice graphicsDevice;

        public CModel(Model Model, Vector3 Position, Vector3 Rotation,
            Vector3 Scale, GraphicsDevice graphicsDevice) {
            this.Model = Model;

            modelTransforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(modelTransforms);

            this.Position = Position;
            this.Rotation = Rotation;
            this.Scale = Scale;

            this.graphicsDevice = graphicsDevice;
        }

        public void Draw(Matrix View, Matrix Projection) {
            Matrix baseWorld = Matrix.CreateScale(Scale)
                * Matrix.CreateFromYawPitchRoll(
                    Rotation.Y, Rotation.X, Rotation.Z)
                * Matrix.CreateTranslation(Position);

            foreach (ModelMesh mesh in Model.Meshes) {
                foreach (BasicEffect effect in mesh.Effects) {
                    effect.EnableDefaultLighting();
                    effect.World = modelTransforms[mesh.ParentBone.Index] *
                        Matrix.CreateTranslation(Position);
                    effect.View = View;
                    effect.Projection = Projection;
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}
