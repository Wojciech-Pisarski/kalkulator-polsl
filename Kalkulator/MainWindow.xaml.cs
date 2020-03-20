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

namespace Kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void number_Click(object sender, RoutedEventArgs e)
        {
            if (wynikPole.Text == "Nie można dzielić przez 0!")
                return;

            Button przycisk = sender as Button;
            if (przycisk.Content.ToString() == "0")
            {
                int iterator = 1;
                String ciag = "";

                if (wynikPole.Text.Length - iterator < 0)
                {
                    wynikPole.Text = $"{wynikPole.Text}0";
                    return;
                }

                while (wynikPole.Text.Length - iterator >= 0 && Convert.ToString(wynikPole.Text.ElementAt(wynikPole.Text.Length - iterator)) != "+" && Convert.ToString(wynikPole.Text.ElementAt(wynikPole.Text.Length - iterator)) != "-" && Convert.ToString(wynikPole.Text.ElementAt(wynikPole.Text.Length - iterator)) != "x" && Convert.ToString(wynikPole.Text.ElementAt(wynikPole.Text.Length - iterator)) != "/")
                {
                    ciag = Convert.ToString(wynikPole.Text.ElementAt(wynikPole.Text.Length - iterator)) + ciag;
                    iterator++;
                }

                if (wynikPole.Text.Length - iterator == 0 || ciag.Contains(".") || ciag.Contains(",") || ciag == "" || ciag != "0")
                {
                    wynikPole.Text = $"{wynikPole.Text}0";
                    return;
                } 
                else if (ciag == "0")
                    return;
            } else {
                wynikPole.Text = $"{wynikPole.Text}{przycisk.Content.ToString()}";
            }            
        }    
        
        private void operator_Click(object sender, RoutedEventArgs e)
        {
            Button przycisk = sender as Button;
            if (wynikPole.Text == "Nie można dzielić przez 0!" && przycisk.Content.ToString() != "CLEAR")
                return;
            switch (przycisk.Content.ToString())
            {
                case ".":
                    string ciag = "";
                    int iterator2 = 1;
                    while (wynikPole.Text.Length - iterator2 >= 0 && Convert.ToString(wynikPole.Text[wynikPole.Text.Length - iterator2]) != "/" && Convert.ToString(wynikPole.Text[wynikPole.Text.Length - iterator2]) != "+" && Convert.ToString(wynikPole.Text[wynikPole.Text.Length - iterator2]) != "-" && Convert.ToString(wynikPole.Text[wynikPole.Text.Length - iterator2]) != "x")
                    {
                        ciag = Convert.ToString(wynikPole.Text[wynikPole.Text.Length - iterator2]) + ciag;
                        iterator2++;
                    }
                    if (ciag.Contains("."))
                        return;
                    if (wynikPole.Text != "" && !wynikPole.Text.EndsWith("x") && !wynikPole.Text.EndsWith("/") && !wynikPole.Text.EndsWith("+") && !wynikPole.Text.EndsWith("-") && !wynikPole.Text.EndsWith("."))
                        wynikPole.Text = $"{wynikPole.Text}.";
                    break;

                case "+":
                    if (wynikPole.Text != "" && !wynikPole.Text.EndsWith("x") && !wynikPole.Text.EndsWith("/") && !wynikPole.Text.EndsWith("+") && !wynikPole.Text.EndsWith("-") && !wynikPole.Text.EndsWith("."))
                        wynikPole.Text = $"{wynikPole.Text}+";
                    break;

                case "-":
                    if (!wynikPole.Text.EndsWith("-") && !wynikPole.Text.EndsWith("."))
                        wynikPole.Text = $"{wynikPole.Text}-";
                    break;

                case "x":
                    if (wynikPole.Text != "" && !wynikPole.Text.EndsWith("+") && !wynikPole.Text.EndsWith("-") && !wynikPole.Text.EndsWith(".") && !wynikPole.Text.EndsWith("/"))
                        wynikPole.Text = $"{wynikPole.Text}x";
                    break;
                case "/":

                    if (wynikPole.Text != "" && !wynikPole.Text.EndsWith("x") && !wynikPole.Text.EndsWith("/") && !wynikPole.Text.EndsWith("+") && !wynikPole.Text.EndsWith("-") && !wynikPole.Text.EndsWith("."))
                        wynikPole.Text = $"{wynikPole.Text}/";
                    break;

                case "BACK":
                    wynikPole.Text = wynikPole.Text.Remove(wynikPole.Text.Length - 1);
                    break;

                case "CLEAR":
                    wynikPole.Text = "";
                    break;

                case "=":
                    if (wynikPole.Text != "" && !wynikPole.Text.EndsWith("+") && !wynikPole.Text.EndsWith("-") && !wynikPole.Text.EndsWith("/") && !wynikPole.Text.EndsWith("x"))
                    {
                        String[] wagiDzialan = { "/", "x", "+", "-" };
                        String[] wynikPom = new String[wynikPole.Text.Length];

                        for (int k = 0; k < wynikPom.Length; k++)
                            wynikPom[k] = Convert.ToString(wynikPole.Text.ElementAt(k));

                        double wynikCzastkowy = 0.0;
                        String lewa = "";
                        String prawa = "";
                        int nowyLewyIndeks = 0;
                        int nowyPrawyIndeks = 0;
                        int iterator = 1;
                        
                        for (int i = 0; i < wagiDzialan.Length; i++)
                        {
                            //iteracja przez każde z możliwych działań
                            for (int j = 0; j < wynikPom.Length; j++)
                            {
                                //przejście po każdym elemencie działania
                                if (wynikPom[j] == wagiDzialan[i])
                                {
                                    while ((j - iterator >= 0) && (wynikPom[j - iterator] == "-" || !wagiDzialan.Contains(wynikPom[j - iterator])))
                                    {
                                        lewa = wynikPom[j - iterator] + lewa;
                                        nowyLewyIndeks = j - iterator;

                                        if (wynikPom[j - iterator] == "-")
                                            break;

                                        iterator++;
                                    }
                                    if (lewa == "")
                                        lewa = "0";

                                    iterator = 1;

                                    while (j + iterator < wynikPom.Length && wynikPom[j + iterator] != "" && (!wagiDzialan.Contains(wynikPom[j + iterator]) || iterator == 1))
                                    {
                                        if (wynikPom[j + iterator].Contains("-") && prawa != "")
                                            break;

                                        prawa = prawa + wynikPom[j + iterator];

                                        if (wynikPom[j + iterator] == wynikPom[j + iterator])
                                            wynikPom[j + iterator] = wynikPom[j + iterator];

                                        iterator++;
                                        nowyPrawyIndeks = j + iterator;
                                    }

                                    if (lewa.Contains("E") || lewa.Contains("e") || prawa.Contains("E") || prawa.Contains("e"))
                                        return;

                                    Double lewaD = 0.0;
                                    Double prawaD = 0.0;

                                    if (Double.TryParse(lewa, out lewaD));
                                    else
                                    {
                                        if (lewa.Contains("."))
                                        {
                                            lewa = lewa.Replace(".", ",");
                                            lewaD = Convert.ToDouble(lewa);
                                        }
                                        else
                                        {
                                            lewa = lewa.Replace(",", ".");
                                            lewaD = Convert.ToDouble(lewa);
                                        }
                                    }

                                    if (Double.TryParse(prawa, out prawaD));
                                    else
                                    {
                                        if (prawa.Contains("."))
                                        {
                                            prawa = prawa.Replace(".", ",");
                                            prawaD = Convert.ToDouble(prawa);
                                        }
                                        else
                                        {
                                            prawa = prawa.Replace(",", ".");
                                            prawaD = Convert.ToDouble(prawa);
                                        }
                                    }
                                    
                                    if (prawaD == Double.Parse("0") && wynikPom[j] == "/")
                                    {
                                        wynikPole.Text = "Nie można dzielić przez 0!";
                                        return;
                                    }

                                    switch (wynikPom[j])
                                    {
                                        case "+":
                                            wynikCzastkowy = lewaD + prawaD;
                                            break;
                                        case "-":
                                            wynikCzastkowy = lewaD - prawaD;
                                            break;
                                        case "x":
                                            wynikCzastkowy = lewaD * prawaD;
                                            break;
                                        case "/":
                                            wynikCzastkowy = lewaD / prawaD;
                                            break;
                                    }

                                    iterator = 0;
                                    for (int k = nowyLewyIndeks; k < wynikPom.Length; k++)
                                    {
                                        if (k == nowyLewyIndeks)
                                        {
                                            if (k > 0)
                                            {
                                                if (wynikCzastkowy < 0)
                                                {
                                                    wynikCzastkowy *= (-1);
                                                    wynikPom[k] = "-";
                                                    k++;
                                                    if (Convert.ToString(wynikCzastkowy).Contains(","))
                                                        wynikPom[k] = Convert.ToString(wynikCzastkowy).Replace(",", ".");
                                                }
                                            }
                                            if (Convert.ToString(wynikCzastkowy).Contains(","))
                                                wynikPom[k] = Convert.ToString(wynikCzastkowy).Replace(",", ".");
                                            else
                                                wynikPom[k] = Convert.ToString(wynikCzastkowy);
                                        }
                                        else
                                        {
                                            if ((nowyPrawyIndeks + iterator) < wynikPom.Length)
                                            {
                                                wynikPom[k] = wynikPom[nowyPrawyIndeks + iterator];
                                                iterator++;
                                            }
                                            else
                                            {
                                                while (k < wynikPom.Length)
                                                {
                                                    wynikPom[k] = "";
                                                    k++;
                                                }
                                            }
                                        }
                                    }
                                    
                                    iterator = 1;
                                    i = 0;
                                    j = 0;
                                    lewa = "";
                                    prawa = "";
                                }
                            }
                        }
                        wynikPole.Text = "";
                        for (int i = 0; i < wynikPom.Length; i++)
                        {
                            if (wynikPom[i] == "")
                                break;
                            else
                                wynikPole.Text += wynikPom[i];
                        }
                    }
                    break;
            }
        }
    }
}
