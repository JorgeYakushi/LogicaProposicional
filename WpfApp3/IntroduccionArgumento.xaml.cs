using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for IntroduccionArgumento.xaml
    /// </summary>
    public partial class IntroduccionArgumento : Page
    {
     
        string conclusion;
        List<string> premisas = new List<string>();
        

        string variables = @"[a-z]";
        string operadoresDiadicos = @"[→∧∨↔]";
        string operadoresMonadicos = @"[¬]";
        string simbolosDeAsociacion = @"[\)\(]";
        string lexicoValido = @"[^a-z→∧∨↔¬\)\(]";

        string premisaOConclusion;

        int valor1;
        int valor2;
        bool contieneLexico;
        string formula;
        public IntroduccionArgumento()
        {
            InitializeComponent();
            textBox.Focus();
        }

        private void validarInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(lexicoValido);
            e.Handled = regex.IsMatch(e.Text);
        }

        int jerarquia0;

        bool estructuraCorrecta;
        int cantidadNegaciones;
        int contador;
        void RevisarEstructura()
        {
            estructuraCorrecta = false;
            formula = textBox.Text;
            formula = Regex.Replace(formula, " ", "");
            formula = Regex.Replace(formula, variables, "a");
            formula = Regex.Replace(formula, operadoresDiadicos, "O");
            
            
            foreach (char n in formula)
            {
                if (n == '¬')
                {
                    cantidadNegaciones++;
                    
                }
            }
            int a = 0;
            
   


            do
            {
                while (formula.Contains("¬a"))
                {
                    formula = Regex.Replace(formula, "¬a", "a");
                }

                while (formula.Contains("¬(aOa)"))
                {
                    formula = Regex.Replace(formula, @"(\¬\(aOa\))", "(aOa)");
                }

                while (formula.Contains("(aOa)"))
                {
                    formula = Regex.Replace(formula, @"(\(aOa\))", "a");
                }
                contador++;
            }
            while (cantidadNegaciones > contador);
                        
            if (formula == "aOa" || formula == "a" || formula == "(aOa)")
            {
                estructuraCorrecta = true;
                textBox1.Text = "fbf";
            }
            else
            {
                textBox1.Text = "no fbf";
            }
        }


        private void btn_AgregarPremisa_Click(object sender, RoutedEventArgs e)
        {
            premisaOConclusion = "premisa";
            cantidadNegaciones = 0;
            contador = 0;
            RevisarEstructura();
            if (estructuraCorrecta == true)
            {
                premisas.Add(textBox.Text);
                listBox.Items.Add("V  " + textBox.Text);
                textBox.Text = "";
            }
        }


        private void btn_AgregarConclusion_Click(object sender, RoutedEventArgs e)
        {
            premisaOConclusion = "conclusion";
            RevisarEstructura();
            if (estructuraCorrecta == true)
            {
                listBox2.Items.Add("F  " + textBox.Text);
                conclusion = textBox.Text;
                textBox.Text = "";
                btn_AgregarConclusion.IsEnabled = false;
            }

        }

        private void btn_Validar_Click(object sender, RoutedEventArgs e)
        {
            if (conclusion != null && premisas != null)
            {
                Temporal enviarArg = Temporal.ConseguirInstancia();
                enviarArg.premisasTemporal = premisas;
                enviarArg.ConclusionTemporal = conclusion;
                textBox.Text = "";
                btn_AgregarConclusion.IsEnabled = true;
                listBox.Items.Clear();
                listBox2.Items.Clear();

                Validacion Navegar = new Validacion();

         
            this.NavigationService.Navigate(Navegar);
            }
        }

        private void input(string x)
        {
            textBox.Text = textBox.Text.Insert(textBox.SelectionStart, x);
        }
        int posCursor;
        private void Button_Ingreso_Operador(object sender, RoutedEventArgs e)
        {
            posCursor = textBox.CaretIndex;
            Button Button_Operador = (Button)sender;
            input(Button_Operador.Content.ToString());
            textBox.Focus();
            textBox.CaretIndex = posCursor + 1;
        }

        private void btn_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            conclusion = "";
            premisas.Clear();
            textBox.Text = "";
            textBox1.Text = "";
            listBox.Items.Clear();
            listBox2.Items.Clear();
            btn_AgregarConclusion.IsEnabled = true;
        }

       

    }


}
