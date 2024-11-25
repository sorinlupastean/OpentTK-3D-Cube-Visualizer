using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTK3_CLI_template {
    internal class Camera {

        private Vector3 eye = new Vector3(125, 100, 100);
        private Vector3 target = new Vector3(0, 0, 0);
        private Vector3 up_vector = new Vector3(0, 1, 0);

        public Camera() {
            // set the eye
            Matrix4 camera = Matrix4.LookAt(eye, target, up_vector);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }
    }
}
