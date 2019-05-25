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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for Ejercicio1.xaml
    /// </summary>
    public partial class Ejercicio1 : Page
    {
        public Ejercicio1()
        {
            InitializeComponent();
            label4.Visibility = Visibility.Hidden;
            label5.Visibility = Visibility.Hidden;
            label6.Visibility = Visibility.Hidden;
            label7.Visibility = Visibility.Hidden;
            label8.Visibility = Visibility.Hidden;
            label9.Visibility = Visibility.Hidden;
            label10.Visibility = Visibility.Hidden;
            label11.Visibility = Visibility.Hidden;
        }
        int idFormula;
        string formulaAEvaluar;
        int check;
        string formulaIngresada1;
        string formulaIngresada2;

        private void label1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            label_formulaAResolver.Content = label1.Content;
            idFormula = 1;
        }

        private void label2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            label_formulaAResolver.Content = label2.Content;
            idFormula = 2;
        }


        private void label6_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            label_formulaAResolver.Content = label6.Content;
            idFormula = 6;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_1a.Text != "" && textBox_1b.Text != "")
            {
                formulaAEvaluar = Convert.ToString(label_formulaAResolver.Content);
                formulaIngresada1 = textBox_1a.Text;
                formulaIngresada2 = textBox_1b.Text;
                check = 0;
                switch (idFormula)
                {
                    case 1:
                        if (formulaIngresada1 == "q")
                        {
                           if (formulaIngresada2 == "¬p")
                            {
                                if (label6.IsVisible == true)
                                {
                                    label8.Visibility = Visibility.Visible;
                                    label9.Visibility = Visibility.Visible;
                               
                                    button1.IsEnabled = false;
                                    button2.IsEnabled = false;
                                    label14.Visibility = Visibility.Visible;
                                    if (label12.IsVisible == false)
                                    {
                                        label10.Visibility = Visibility.Visible;
                                        label11.Visibility = Visibility.Visible;
                                    }
                                }
                                else if(label12.IsVisible == true)
                                {
                                    label10.Visibility = Visibility.Visible;
                                    label11.Visibility = Visibility.Visible;
                                    button1.IsEnabled = false;
                                    button2.IsEnabled = false;
                                    label14.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    label4.Visibility = Visibility.Visible;
                                    label5.Visibility = Visibility.Visible;
                                    button1.IsEnabled = false;
                                    button2.IsEnabled = false;
                                    label14.Visibility = Visibility.Visible;
                                }
                            }   
                           else
                            {
                                MessageBox.Show("Revisa de nuevo la tabla de verdad del condicional cuando está negado para el segundo valor que ingresaste");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Revisa de nuevo la tabla de verdad del condicional cuando está negado para el primer valor que ingresaste");
                        }
                        break;
                    case 2:
                        MessageBox.Show("Revisa nuevamente cual es el operador de mayor jerarquía para la fórmula" + formulaAEvaluar);
                        break;

                    case 6:
                        if (formulaIngresada1 == "q")
                        {
                            if (formulaIngresada2 == "s")
                            {
                                label12.Visibility = Visibility.Visible;
                                label13.Visibility = Visibility.Visible;
                            }
                        }
                            break;


            }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_2a.Text != "" && textBox_2b.Text != "")
            {
                formulaAEvaluar = Convert.ToString(label_formulaAResolver.Content);
                formulaIngresada1 = textBox_2a.Text;
                formulaIngresada2 = textBox_2b.Text;
                check = 0;
                switch (idFormula)
                {
                    case 1:
                        MessageBox.Show("Revisa nuevamente cual es el operador de mayor jerarquía para la fórmula" + formulaAEvaluar);
                        break;

                    case 2:
                        if (formulaIngresada1 == "q^s")
                        {
                            if (formulaIngresada2 == "p")
                            {
                                label6.Visibility = Visibility.Visible;
                                label7.Visibility = Visibility.Visible;
                            }
                        }
                        else if (formulaIngresada1 == "(q^s)" || formulaIngresada1 == "(q^s" || formulaIngresada1 == "q^s)")
                        {
                            MessageBox.Show("Revisa los paréntesis para " + formulaIngresada1);
                        }
                        else
                        {
                            MessageBox.Show("Revisa nuevamente cual es el operador de mayor jerarquía para la fórmula" + formulaAEvaluar);
                        }
                        break;

                        case 6:
                        MessageBox.Show("Revisa nuevamente cual es el operador de mayor jerarquía para la fórmula" + formulaAEvaluar);

                        break;



                }
            }
        }

       
    }
}
