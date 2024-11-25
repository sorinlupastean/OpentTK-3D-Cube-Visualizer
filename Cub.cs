using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK3_CLI_template
{
    internal class Cub
    {
        private List<Vector3> varfuri;
        private bool vizibil;
        private Color DEFAULT_COLOR = Color.White;
        private Color culoare;
        Randomizer randomizer;
        private float angleX = 0f;
        private float angleY = 0f;
        private Vector3 position;

        public Cub( string caleFisier) 
        {
            varfuri = new List<Vector3>();
            CitesteDinFisier(caleFisier);
            vizibil = true;
            culoare = Color.White;
            randomizer = new Randomizer();
            position = new Vector3(0, 0, 0);
        }

        public void CitesteDinFisier(string caleFisier)
        {
            try
            {
                string[] linii = File.ReadAllLines(caleFisier);
                foreach (string linie in linii)
                {
                    string[] date = linie.Split(' ');

                    float x = float.Parse(date[0]);
                    float y = float.Parse(date[1]);
                    float z = float.Parse(date[2]);

                    varfuri.Add(new Vector3(x, y, z));
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void DrawEdge(int startIndex, int endIndex)
        {
            GL.Vertex3(varfuri[startIndex]);
            GL.Vertex3(varfuri[endIndex]);
        }

        public void Draw()
        {
            if (vizibil)
            {
                GL.PushMatrix();

                GL.Rotate(angleX, 1.0f, 0.0f, 0.0f);
                GL.Rotate(angleY, 0.0f, 1.0f, 0.0f);
                GL.Translate(position);

                GL.Color3(culoare);

                GL.LineWidth(1.0f);
                GL.Begin(PrimitiveType.Lines);


                DrawEdge(0, 1);
                DrawEdge(1, 2);
                DrawEdge(2, 3);
                DrawEdge(3, 0);

                DrawEdge(4, 5);
                DrawEdge(5, 6);
                DrawEdge(6, 7);
                DrawEdge(7, 4);

                DrawEdge(0, 4);
                DrawEdge(1, 5);
                DrawEdge(2, 6);
                DrawEdge(3, 7);

                GL.End();
                GL.PopMatrix();
            }
            
        }

        public void ToogleVisiblity()
        {
            vizibil = !vizibil;
        }

        public void ChangeColor()
        {
            culoare = randomizer.ChangeColor();
        }

        public void ResetColorCube()
        {
            culoare = DEFAULT_COLOR;
        }

        public void AdjustColor(int deltaR, int deltaG, int deltaB)
        {
            int newR = culoare.R + deltaR;
            int newG = culoare.G + deltaG;
            int newB = culoare.B + deltaB;

            if(newR < 0) {
                newR = 0;
            } else if (newR > 255)
            {
                newR = 255;
            }
            if(newG < 0)
            {
               newG = 0; 
            } else if(newG > 255)
            {
                newG = 255;
            }
            if (newB < 0) {
                newB = 0;
            } else if(newB > 255)
            {
                newB=255;
            }

            culoare = Color.FromArgb(newR, newG, newB);
        }

        public void RotateCube(float deltaX, float deltaY)
        {
            angleX += deltaX;
            angleY += deltaY;
        }

        public void Move(float deltaX, float deltaY, float deltaZ)
        {
            position.X += deltaX;
            position.Y += deltaY;
            position.Z += deltaZ;
        }

        public void ResetPosition()
        {
            position = new Vector3(0, 0, 0);
        }




    }
}
