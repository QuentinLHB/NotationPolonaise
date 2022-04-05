/**
 * RPN calculation "Polish calculation"
 * author : Mathieu Seligmann
 * date : November, 9th 2020
 */
using System;
using System.Net.NetworkInformation;

namespace TestNotationPolonaise
{
    class Program
    {

        ///
        /// <summary>
        /// Gives the result of a RPN formula also known as Reverse Polish Notation
        /// </summary>
        /// <param name="message">formula written in RPN</param>
        /// <returns>result contained in the first index of vect[] array</returns>
        static Double RPN_Calculation(string message)
        {
            try
            {

                string[] vect = message.Split(' ');

                int nbrElements = vect.Length;

                while (nbrElements > 1)
                {

                    int k = nbrElements - 1;

                    while (k > 0 && vect[k] != "+" && vect[k] != "-" && vect[k] != "*" && vect[k] != "/")
                    {
                        k--;
                    }

                    float a = float.Parse(vect[k + 1]);
                    float b = float.Parse(vect[k + 2]);

                    float result = 0;
                    switch (vect[k])
                    {
                        case "+":
                            result = a + b;
                            break;
                        case "-":
                            result = a - b;
                            break;
                        case "*":
                            result = a * b;
                            break;
                        case "/":
                            if (b == 0)
                            {
                                return Double.NaN;
                            }
                            result = a / b;
                            break;
                    }

                    vect[k] = result.ToString();

                    for (int j = k + 1; j < nbrElements - 2; j++)
                    {
                        vect[j] = vect[j + 2];
                    }

                    for (int j = nbrElements - 2; j < nbrElements; j++)
                    {
                        vect[j] = " ";
                    }
                    nbrElements = nbrElements - 2;
                }
                return Double.Parse(vect[0]);
            }
            catch
            {
                return Double.NaN;
            }

        }

        /// <summary>
        /// user_selection in between 2 potential characters
        /// </summary>
        /// <param name="message">message to be shown to console</param>
        /// <param name="first_char">first potential character</param>
        /// <param name="second_char">second potential character</param>
        /// <returns>character selected by user</returns>
        static char user_selection(string message, char first_char, char second_char)
        {
            char reply;
            do
            {
                Console.WriteLine();
                Console.Write(message + " (" + first_char + "/" + second_char + ") ");
                reply = Console.ReadKey().KeyChar;
            } while (reply != first_char && reply != second_char);
            return reply;
        }

        /// <summary>
        /// RPN formulas selection
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            char reply = 'A';
            // loop for the user selection
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter a RPN formula with a space between each element ");
                string formula = Console.ReadLine();
                // result shown to console
                Console.WriteLine("Result =  " + RPN_Calculation(formula));
                reply = user_selection("Would you like to continue ?", 'Y', 'N');
            } while (reply == 'Y');
        }
    }
}