using System.Diagnostics;

namespace AoC2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var wheel = new SafeWheel();
            wheel.Solve();

            Debug.WriteLine(wheel.ZeroCount);
        }
    }
}
