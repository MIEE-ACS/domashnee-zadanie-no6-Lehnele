using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DZ6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        List<Triangle> Triangles = new List<Triangle>();
        public MainWindow()
        {
            InitializeComponent();    
        }

        public abstract class Triangle
        {
           public Exception inCorrectData = new Exception("Это не прямоугольный треугольник!");
            public double A { get; private set; }
            public double B { get; private set; }
            public double V { get; private set; }
            public Triangle(double a, double b, double v)
            {
                A = a;
                B = b;
                V = v;
            }
         

            public double getArea(Triangle triangle)
            {
                return (1 / 2) * triangle.A * triangle.B * Math.Sin(V);
            }

            public double getPerimeter(Triangle triangle)
            {
                return Math.Sqrt(Math.Pow(triangle.A, 2) + Math.Pow(triangle.B, 2) - 2 * triangle.A * triangle.A * Math.Cos(V)) + triangle.A + triangle.B;
            }

            public override string ToString()
            {
                return $"Треугольник:{A}{B}{V}\n\tCторона A: {A}\n\tCторона B: {B}\n\tУгол между ними: {V}\n\t ";
            }

            public static string printTriangleList(List<Triangle> Triangles)
            {
                string output_string = "";
                foreach (var triangle in Triangles)
                {
                    output_string += triangle.ToString() + "\n";
                }
                return output_string;
            }
        }

        public class Isosceles : Triangle
        {
            public Isosceles(double a, double b, double v) :
                base(a, b, v)
            {
                if(a != b)
                {
                    throw new Exception("Это не равнобедренный треугольник!");
                }
            }
        }

        public class Equilateral : Triangle
        {
            public Equilateral(double a, double b, double v) :
                base(a, b, v)
            {
                if (a != b && v != 60)
                {
                    throw new Exception("Это не равносторонний треугольник!");
                }
            }
        }

        public class Right : Triangle
        {
            
            public Right(double a, double b, double v) :
                base(a, b, v)
            {
                if ( v != 90)
                {

                    throw inCorrectData;
                }
            }
        }

        private void Add_Btn(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (choiceBox.SelectedIndex)
                {
                    case 0:
                        Triangles.Add(new Isosceles(int.Parse(a.Text), int.Parse(b.Text), int.Parse(v.Text)));
                        mas.Text = Triangle.printTriangleList(Triangles);
                        break;
                    case 1:
                        Triangles.Add(new Equilateral(int.Parse(a.Text), int.Parse(b.Text), int.Parse(v.Text)));
                        mas.Text = Triangle.printTriangleList(Triangles);
                        break;
                    case 2:
                        Triangles.Add(new Right(int.Parse(a.Text), int.Parse(b.Text), int.Parse(v.Text)));
                        mas.Text = Triangle.printTriangleList(Triangles);
                        break;
                }
            }
            catch (System.FormatException)
            {   
                    window.Background = Brushes.Red;         
            }
    
        }

        private void Remove_Btn(object sender, RoutedEventArgs e)
        {
            if (Triangles.Count != 0)
            {
                Triangles.Remove(Triangles.Last());
                mas.Text = Triangle.printTriangleList(Triangles);
            }
        }

        private void S_Click(object sender, RoutedEventArgs e)
        {
            //S
            if (Triangles.Count != 0)
            {
                result.Text = $"{Math.Round((double)1 / 2 * Triangles[Triangles.Count - 1].A * Triangles[Triangles.Count - 1].B * Math.Sin(Triangles[Triangles.Count - 1].V * Math.PI / 180), 2)}";
            }
        }

        private void P_Click(object sender, RoutedEventArgs e)
        {
            //P
            if (Triangles.Count != 0)
            {
                result.Text = $"{Math.Round((double)Math.Sqrt(Math.Pow(Triangles[Triangles.Count - 1].A, 2) + Math.Pow(Triangles[Triangles.Count - 1].B, 2) - 2 * Triangles[Triangles.Count - 1].A * Triangles[Triangles.Count - 1].A * Math.Cos(Triangles[Triangles.Count - 1].V * Math.PI / 180)) + Triangles[Triangles.Count - 1].A + Triangles[Triangles.Count - 1].B, 2)}";
            }
        }
    }
}
