using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Validacion.xaml
    /// </summary>
    public partial class Validacion : Page
    {
        List<string> premisas = new List<string>();
        string conclusion;
        string simbolosDeAsociacion = @"[\)\(]";
        string operadoresDiadicos = @"[→∧∨↔]";
        string operadoresMonadicos = "¬";
        int cantidadNegaciones;
        string ramaAbiertaOCerrada;
        bool valorDeVdeSubF;
        string resultadoEsperado;
        
        string booleanoEnString;
        string nuevaFormula;
        
        string nivelComplejidadFormula;
        string formulaSinNegaciones;

        int jerarquia0;
        bool contieneLexico;
        int valor1;
        int valor2;

        int contadorDeErrores;

        bool valorDeVDelOperador;

        bool seAbrenRamas;

        string operadorFormulaCompleja;

        string subFormula1;
        string subFormula2;

        string operador1;
        string operador2;

        string error1;
        string error2;
        string error3;
        string error4;


        public Validacion()
        {
            InitializeComponent();
            ConseguirValores();
            sp_1.Visibility = Visibility.Collapsed;
            sp_2.Visibility = Visibility.Collapsed;
            sp_3.Visibility = Visibility.Collapsed;
            textBox_VoF1.Visibility = Visibility.Collapsed;
            textBox_VoF2.Visibility = Visibility.Collapsed;


        }

        void ConseguirValores()
        {
            Temporal enviarArg = Temporal.ConseguirInstancia();
            conclusion = enviarArg.ConclusionTemporal;
            premisas = enviarArg.premisasTemporal;
          
            foreach (var prem in premisas)
            {
                listBox.Items.Add("V  " + prem);
            }
            listBox.Items.Add("F  " + conclusion);
            premisas.Add(conclusion);
           

        }

        private void listboxClick(object sender, RoutedEventArgs e)
        {
            textBox_1.Text = "";
            textBox_2a.Text = "";
            textBox_2b.Text = "";
            textBox_3a.Text = "";
            textBox_3b.Text = "";
            textBox_VoF1.Text = "";
            textBox_VoF2.Text = "";
            label_formula.Content = listBox.Items[listBox.SelectedIndex];
            nuevaFormula = listBox.Items[listBox.SelectedIndex].ToString();
            nuevaFormula = nuevaFormula.Remove(0, 3);

            // RevisionDeSubformula(premisas[listBox.SelectedIndex]);
            RevisionDeSubformula(nuevaFormula);
        }

        void RevisionDeSubformula (string subF)
        {
            if (Regex.IsMatch(subF, simbolosDeAsociacion))
            {

                nivelComplejidadFormula = "2";
                RevisionDeValorDeV(subF);
            }
            else
            {
                if (Regex.IsMatch(subF, operadoresDiadicos))
                {
                    nivelComplejidadFormula = "1";
                }
                else
                {
                    if (subF.Contains(operadoresMonadicos))
                    {
                        sp_1.Visibility = Visibility.Collapsed;
                        sp_2.Visibility = Visibility.Collapsed;
                        sp_3.Visibility = Visibility.Collapsed;
                        textBox_VoF1.Visibility = Visibility.Visible;
                        textBox_VoF1.Margin = new Thickness(152, 129, 0, 0);
                        textBox_VoF2.Visibility = Visibility.Collapsed;
                        nivelComplejidadFormula = "0";
                        RevisionDeValorDeV(subF);
                    }
                    else
                    {
                        sp_1.Visibility = Visibility.Collapsed;
                        sp_2.Visibility = Visibility.Collapsed;
                        sp_3.Visibility = Visibility.Collapsed;
                        textBox_VoF1.Visibility = Visibility.Collapsed;
                        textBox_VoF2.Visibility = Visibility.Collapsed;


                    }
                }

            }
        }

        void RevisionDeValorDeV (string subF)
        {
            cantidadNegaciones = 0;
            foreach (char negacion in nuevaFormula)
            {
                if (negacion == '¬')
                {
                    cantidadNegaciones++;
                }
            }
            string formulaConValorDeV = listBox.Items[listBox.SelectedIndex].ToString();
            if (char.ToString(formulaConValorDeV[0]) == "V")
            {
                switch (nivelComplejidadFormula)
                {
                    case "0":
                        if ((cantidadNegaciones % 2) == 0)
                        {
                            valorDeVdeSubF = true;
                        }
                        else
                        {
                            valorDeVdeSubF = false;
                        }
                        break;
                    case "1":

                        break;
                    case "2":
                        RevisarJerarquia(nuevaFormula);
                        switch (operadorFormulaCompleja)
                        {
                           
                            case "∧":
                                NoAbrirRamas();
                                operador1 = "V";
                                operador2 = "V";
                                break;
                            case "∨":
                                AbrirRamas();
                                operador1 = "V";
                                operador2 = "V";
                                break;
                            case "→":
                                AbrirRamas();
                                operador1 = "F";
                                operador2 = "V";
                                break;
                            case "↔":
                                AbrirRamas();
                                operador1 = "F";
                                operador2 = "V";
                                break;
                               

                        }

                        break;


                }
            }
            else
            {
                switch (nivelComplejidadFormula)
                {
                    case "0":
                        if ((cantidadNegaciones % 2) == 0)
                        {
                            valorDeVdeSubF = false;
                        }
                        else
                        {
                            valorDeVdeSubF = true;
                        }
                        break;
                    case "1":

                        break;
                    case "2":
                        RevisarJerarquia(nuevaFormula);
                        switch (operadorFormulaCompleja)
                        {

                            case "∧":
                                AbrirRamas();
                                operador1 = "F";
                                operador2 = "F";
                                break;
                            case "∨":
                                NoAbrirRamas();
                                operador1 = "F";
                                operador2 = "F";
                                break;
                            case "→":
                                NoAbrirRamas();
                                operador1 = "V";
                                operador2 = "F";
                                break;
                            case "↔":
                                AbrirRamas();
                                operador1 = "V";
                                operador2 = "F";
                                break;


                        }
                        break;


                }
            }
            
        }

        public static IEnumerable<BracketPair> ParseBracketPairs(string text)
        {
            var startPositions = new Stack<int>();

            for (int index = 0; index < text.Length; index++)
                if (text[index] == '(')
                {
                    startPositions.Push(index);
                }
                else if (text[index] == ')')
                {
                    if (startPositions.Count == 0)
                        throw new ArgumentException(string.Format("mismatched end bracket at index {0}", index));

                    var depth = startPositions.Count - 1;
                    var start = startPositions.Pop();

                    yield return new BracketPair(start, index, depth);
                }

            if (startPositions.Count > 0)
                throw new ArgumentException(string.Format("mismatched start brackets, {0} total", startPositions.Count));
        }

        // You can even go one step further and handle TextReaders  
        // Remember you need using System.IO
        public static IEnumerable<BracketPair> ParseBracketPairs(TextReader reader)
        {
            var startPositions = new Stack<int>();

            for (int index = 0; reader.Peek() != -1; ++index)
            {
                // Detect overflow
                if (index < 0)
                    throw new ArgumentException(string.Format("input text too long, must be shorter than {0} characters", int.MaxValue));

                var c = (char)reader.Read();
                if (c == '[')
                {
                    startPositions.Push(index);
                }
                else if (c == ']')
                {
                    // Error on mismatch
                    if (startPositions.Count == 0)
                        throw new ArgumentException(string.Format("mismatched end bracket at index {0}", index));

                    // Depth tends to be zero-based
                    var depth = startPositions.Count - 1;
                    var start = startPositions.Pop();

                    yield return new BracketPair(start, index, depth);
                }
            }

            // Error on mismatch
            if (startPositions.Count > 0)
                throw new ArgumentException(string.Format("mismatched start brackets, {0} total", startPositions.Count));
        }

        private void btn_Agregar_Click(object sender, RoutedEventArgs e)
        {
            contadorDeErrores = 0;
            error1 = "";
            error2 = "";
            error3 = "";
            error4 = "";
            label_error1.Content = "";
            label_error2.Content = "";
            label_error3.Content = "";
            label_error4.Content = "";
            if (valorDeVdeSubF == true)
            {
                booleanoEnString = "V";
            }
            else
            {
                booleanoEnString = "F";
            }
            switch (nivelComplejidadFormula)
            {
                case "0":
                    formulaSinNegaciones = nuevaFormula;
                    formulaSinNegaciones = formulaSinNegaciones.Replace("¬", "");
                    listBox.Items.Add(booleanoEnString + "  " + formulaSinNegaciones);
                    break;
                case "1":

                    break;
                case "2":
                    subFormula1 = nuevaFormula.Substring(0,valor1+1);
                    subFormula2 = nuevaFormula.Substring(valor2);
                    if (subFormula1[0] == '(' && subFormula1.EndsWith(")"))
                    {
                        subFormula1 = subFormula1.Substring(1, subFormula1.Length - 2);
                    }
                    if (subFormula2[0] == '(' && subFormula2.EndsWith(")"))
                    {
                        subFormula2 = subFormula2.Substring(1, subFormula2.Length - 2);
                    }
                    
                    if (seAbrenRamas == true)
                    {
                        if (subFormula1 == textBox_2a.Text && subFormula2 == textBox_2b.Text && operador1 == textBox_VoF1.Text && operador2 == textBox_VoF2.Text)
                        {

                        }
                    }
                    else if (seAbrenRamas == false)
                    {
                        if (subFormula1 == textBox_3a.Text) 
                        {
                            contadorDeErrores++;    
                        }
                        else
                        {
                            error1 = "- La fórmula de arriba es incorrecta -";
                            label_error1.Content = error1;
                        }

                        if (subFormula2 == textBox_3b.Text)
                        {
                            contadorDeErrores++;
                        }
                        else
                        {
                            error2 = "- La fórmula de abajo es incorrecta -";
                            label_error2.Content = error2;
                        }

                        if (operador1 == textBox_VoF1.Text)
                        {
                            contadorDeErrores++;
                        }
                        else
                        {
                            error3 = "- El primer valor de verdad es incorrecto -";
                            label_error3.Content = error3;
                        }

                        if (operador2 == textBox_VoF2.Text)
                        {
                            contadorDeErrores++;
                        }
                        else
                        {
                            error4 = "- El segundo valor de verdad es incorrecto -";
                            label_error4.Content = error4;
                        }
                        if (contadorDeErrores == 4)
                        {
                            premisas.Add(subFormula1);
                            premisas.Add(subFormula2);
                            listBox.Items.Add(operador1 + "  " + subFormula1);
                            listBox.Items.Add(operador1 + "  " + subFormula2);
                        }
                        else
                        {
                            
                        }
                    }
                    break;
                  

            }
        }



        void AbrirRamas()
        {
            sp_1.Visibility = Visibility.Collapsed;
            sp_2.Visibility = Visibility.Visible;
            sp_3.Visibility = Visibility.Collapsed;
            textBox_VoF1.Visibility = Visibility.Visible;
            textBox_VoF1.Margin = new Thickness(48, 129, 0, 0);
            textBox_VoF2.Visibility = Visibility.Visible;
            textBox_VoF2.Margin = new Thickness(254, 129, 0, 0);
            seAbrenRamas = true;
        }

        void NoAbrirRamas()
        {
        
            sp_1.Visibility = Visibility.Collapsed;
            sp_2.Visibility = Visibility.Collapsed;
            sp_3.Visibility = Visibility.Visible;
            textBox_VoF1.Visibility = Visibility.Visible;
            textBox_VoF1.Margin = new Thickness(152, 116, 0, 0);
            textBox_VoF2.Visibility = Visibility.Visible;
            textBox_VoF2.Margin = new Thickness(152, 141, 0, 0);
            seAbrenRamas = false;
        }
        void RevisarJerarquia(string input)
        {
            contieneLexico = true;
           
            Regex regex = new Regex(@"[^a-z^→^∧^∨^↔^¬^\)^\(]");
            foreach (char simbolo in input)
            {
                if (regex.IsMatch(simbolo.ToString()))
                {
                    contieneLexico = false;
                }
            }

            jerarquia0 = 0;

            var list = new List<Tuple<int, int, int>>();
            if (contieneLexico == true)
            {
                foreach (var pairs in ParseBracketPairs(input))
                {
                    list.Add(new Tuple<int, int, int>(pairs.StartIndex, pairs.EndIndex, pairs.Depth));
                    if (pairs.Depth == 0)
                    {


                        if (jerarquia0 > 0)
                        {
                            valor2 = pairs.StartIndex;
                        }
                        else
                        {
                            valor1 = pairs.EndIndex;
                        }
                        jerarquia0++;
                    }
                }


                if (jerarquia0 > 2)

                {

                }
                else
                {

                    operadorFormulaCompleja = input[valor1 + 1].ToString();
                }
            }
            else
            {
             

            }

        }

        private void input(string x)
        {
            //_currentTextbox.Text = _currentTextbox.Text.Insert(_currentTextbox.SelectionStart, x);
            Clipboard.SetText(x);
        }
        int posCursor;
        private void Button_Ingreso_Operador(object sender, RoutedEventArgs e)
        {
            //     posCursor = _currentTextbox.CaretIndex;
            Button Button_Operador = (Button)sender;
            input(Button_Operador.Content.ToString());
            //     _currentTextbox.Focus();
            //_currentTextbox.CaretIndex = posCursor + 1;
        }

        private TextBox _currentTextbox;

        private void TextBoxLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            _currentTextbox = e.Source as TextBox;
        }
    }



    public struct BracketPair
    {
        private int startIndex;
        private int endIndex;
        private int depth;

        public BracketPair(int startIndex, int endIndex, int depth)
        {
            if (startIndex > endIndex)
                throw new ArgumentException("startIndex must be less than endIndex");

            this.startIndex = startIndex;
            this.endIndex = endIndex;
            this.depth = depth;
        }

        public int StartIndex
        {
            get { return this.startIndex; }
        }

        public int EndIndex
        {
            get { return this.endIndex; }
        }

        public int Depth
        {
            get { return this.depth; }
        }
    }


}
