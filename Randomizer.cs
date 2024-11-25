using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK3_CLI_template
{
    internal class Randomizer
    {
        private Random random;
        private Color color;

        public Randomizer()
        {
            random = new Random();
        }

        public Color ChangeColor()
        {
            int genR = random.Next(0, 255);
            int genG = random.Next(0, 255);
            int genB = random.Next(0, 255);
            color = Color.FromArgb(genR, genG, genB);
            return color;
        }
    }
}
