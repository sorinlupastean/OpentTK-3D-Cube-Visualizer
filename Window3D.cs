using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using System;
using System.Drawing;

namespace OpenTK3_CLI_template {
    internal class Window3D : GameWindow {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;

        private readonly Axes ax;
        private Camera cam;

        private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);

        Randomizer randomizer;
        Cub cub;
        private float moveSpeed = 1.0f;


        int previousMouseX = Mouse.GetCursorState().X;
        int previousMouseY = Mouse.GetCursorState().Y;

        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8)) {
            VSync = VSyncMode.On;

            // inits
            ax = new Axes();
            DiplayHelp();
            randomizer = new Randomizer();
            cub = new Cub("cub_coordonate.txt");
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
                       
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            // set background
            GL.ClearColor(DEFAULT_BKG_COLOR);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            cam = new Camera();
        }

        protected override void OnUpdateFrame(FrameEventArgs e) {
            base.OnUpdateFrame(e);

            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            int deltaX = previousMouseX - currentMouse.X;
            int deltaY = previousMouseY - currentMouse.Y;

            cub.RotateCube(deltaX * 0.1f, deltaY * 0.1f);

            previousMouseX = currentMouse.X;
            previousMouseY = currentMouse.Y;



            if (currentKeyboard[Key.Escape]) {
                Exit();
            }
            if (currentKeyboard[Key.C] && !previousKeyboard[Key.C]) {
                GL.ClearColor(randomizer.ChangeColor());
            }
            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            { 
                cub.ToogleVisiblity();
            }
            if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
            {
                cub.ChangeColor();
            }
            if (currentKeyboard[Key.T] && !previousKeyboard[Key.T])
            {
                cub.ResetColorCube();
            }

            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                int delta = currentKeyboard[Key.ShiftLeft] || currentKeyboard[Key.ShiftRight] ? -10 : 10;
                cub.AdjustColor(delta, 0, 0);
            }
            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                int delta = currentKeyboard[Key.ShiftLeft] || currentKeyboard[Key.ShiftRight] ? -10 : 10;
                cub.AdjustColor(0, delta, 0);
            }
            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                int delta = currentKeyboard[Key.ShiftLeft] || currentKeyboard[Key.ShiftRight] ? -10 : 10;
                cub.AdjustColor(0, 0, delta);
            }

            if (currentKeyboard[Key.W])
            {
                cub.Move(0, moveSpeed, 0);
            }
            if (currentKeyboard[Key.A])
            {
                cub.Move(-moveSpeed, 0, 0);
            }
            if (currentKeyboard[Key.S])
            {
                cub.Move( 0, -moveSpeed, 0);
            }
            if (currentKeyboard[Key.D])
            {
                cub.Move(moveSpeed, 0,  0);
            }

            if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
            {
                cub.ResetPosition();
            }



            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;
            // END logic code
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE
            ax.Draw();
            cub.Draw();


            // END render code

            SwapBuffers();
        }

        public void DiplayHelp()
        {
            Console.WriteLine("==========================================");
            Console.WriteLine("                  MENIU                   ");
            Console.WriteLine("==========================================");
            Console.WriteLine(" [ESC]       - Iesiti din program");
            Console.WriteLine(" [C]         - Schimbati culoarea fundalului");
            Console.WriteLine(" [L]         - Schimbati culoarea cubului");
            Console.WriteLine(" [V]         - Ascundeti/Afisati cubul");
            Console.WriteLine(" [T]         - Resetati culoarea cubului");
            Console.WriteLine();
            Console.WriteLine(" === Ajustare culoare cub (RGB) ===");
            Console.WriteLine(" [Shift+R/R] - Ajusteaza componenta R (rosu)");
            Console.WriteLine(" [Shift+G/G] - Ajusteaza componenta G (verde)");
            Console.WriteLine(" [Shift+B/B] - Ajusteaza componenta B (albastru)");
            Console.WriteLine();
            Console.WriteLine(" === Taste de miscare ===");
            Console.WriteLine(" [W]         - Miscare inainte");
            Console.WriteLine(" [S]         - Miscare inapoi");
            Console.WriteLine(" [A]         - Miscare la stanga");
            Console.WriteLine(" [D]         - Miscare la dreapta");
            Console.WriteLine();
            Console.WriteLine(" === Functii suplimentare ===");
            Console.WriteLine(" [K]         - Resetati pozitia cubului la coordonatele initiale (0, 0, 0)");
            Console.WriteLine("==========================================");



        }

    }
}
