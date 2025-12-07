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
            var sw = Stopwatch.StartNew();

            //day 1
            //var wheel = new SafeWheel();
            //wheel.Solve();

            //day 2
            //var v = new ProductIdValidator();
            //v.Validate();
            //v.ValidatePart2();

            //day 3
            //var j = new JoltageCalculator();
            //j.Calculate();

            //day 4
            //var r = new RollLocator();
            //r.FindEm();

            //day 5
            //var ingredientFinder = new IngredientFinder();
            //ingredientFinder.CountEm();

            //day 6
            //var cephMath = new CephalopodMaths();
            //cephMath.DoHomework2();

            //day 7
            var tma = new TachyonManifoldAnalyser();
            tma.Analyse2();

            Debug.WriteLine(sw.ElapsedMilliseconds.ToString() + "ms");
        }
    }
}
