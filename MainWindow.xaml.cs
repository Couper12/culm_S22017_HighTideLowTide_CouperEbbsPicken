/*
 * CouperEbbsPicken
 * 6/18/2018
 * Do a problem
 */ 
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
using System.IO;

namespace culm_S2HighTideLowTide_CouperEbbsPicken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global variables
        StreamReader streamReader;
        int N;
        string input2;
        int[] spaces;
        int counter;
        int[] tides;
        int tempInt;
        bool good;
        int[] lowTides;
        int[] highTides;
        string output;

        public MainWindow()
        {
            InitializeComponent();
        }

        // when the run button is clicked
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            // setting variables
            streamReader = new StreamReader("Input.txt");
            int.TryParse(streamReader.ReadLine(), out N);
            input2 = streamReader.ReadLine();
            spaces = new int[N - 1];
            tides = new int[N];
            output = "";

            // makes an array of the indexes of all the space characters
            counter = 0;
            foreach (char c in input2)
            {
                if (c == ' ')
                {
                    spaces[counter] = input2.IndexOf(c);
                    input2 = input2.Substring(0, input2.IndexOf(c)) + "_" + input2.Substring(input2.IndexOf(c) + 1);
                    counter++;

                }
            }

            // adds the first tide to an array of tides
            int.TryParse(input2.Substring(0, spaces[0]), out tempInt);
            tides[0] = tempInt;

            // adds all the middle tides
            for (int i = 0; i < N - 2; i++)
            {
                int.TryParse(input2.Substring(spaces[i] + 1, spaces[i + 1] - (spaces[i] + 1)), out tempInt);
                tides[i + 1] = tempInt;
            }

            // adds the last tide
            int.TryParse(input2.Substring(spaces[N - 2] + 1), out tempInt);
            tides[tides.Length - 1] = tempInt;

            // sorts the tides from smallest to greatest
            good = false;
            while (good == false)
            {
                counter = 0;
                for (int i = 0; i < N - 1; i++)
                {
                    if (tides[i + 1] < tides[i])
                    {
                        tempInt = tides[i + 1];
                        tides[i + 1] = tides[i];
                        tides[i] = tempInt;
                        counter++;
                    }
                }

                if (counter == 0)
                {
                    good = true;
                }
            }

            // checks to see if it's an even number of tides and writes the output
            if (N % 2 == 0)
            {
                for (int i = 0; i < N / 2; i++)
                {
                    output += " " + tides[N / 2 - i - 1] + " " + tides[N / 2 + i];
                }
            }

            // if odd number it writes the output starting at one higher
            else
            {
                for (int i = 0; i < N / 2; i++)
                {
                    output += " " + tides[N / 2 - i] + " " + tides[N / 2 + i + 1];
                }
            }

            // outputs the answer
            lblOutput.Content = output;
        }
    }
}
