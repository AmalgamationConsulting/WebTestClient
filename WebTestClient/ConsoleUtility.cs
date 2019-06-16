namespace WebTestClient
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary><c>ConsoleUtility</c> contains methods that interact with the console.</summary>
    internal static class ConsoleUtility
    {
        /// <summary>Prompts the user for input.</summary>
        /// <param name="prompt">The prompt message.</param>
        /// <returns>
        /// The next line of characters from the standard input stream
        /// or <c>null</c> if the escape key was pressed.
        /// </returns>
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        internal static string Prompt(string prompt)
        {
            StringBuilder buffer = new StringBuilder();
            Console.Write(prompt);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    return null;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return buffer.ToString();
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    int length = buffer.Length;
                    if (length > 0)
                    {
                        buffer.Length = length - 1;
                        Console.Write("\b \b");
                    }
                }
                else if (key.KeyChar >= 0x20 && key.KeyChar <= 0x7E)
                {
                    buffer.Append(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
        }

        /// <summary>Prompts the user for an integer.</summary>
        /// <param name="prompt">The prompt message.</param>
        /// <param name="minValue">The minimum allowable value.</param>
        /// <param name="maxValue">The maximum allowable value.</param>
        /// <returns>The integer read from the standard input stream
        /// or <c>null</c> if the escape key was pressed.
        /// </returns>
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        internal static int? PromptForInteger(string prompt, int minValue, int maxValue)
        {
            while (true)
            {
                string input = ConsoleUtility.Prompt(prompt);
                if (input == null)
                {
                    return null;
                }

                int value;
                if (int.TryParse(input, NumberStyles.None, NumberFormatInfo.InvariantInfo, out value))
                {
                    if (value >= minValue && value <= maxValue)
                    {
                        return value;
                    }
                }

                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>Displays information about an <see cref="Exception"/>.</summary>
        /// <param name="ex">The <see cref="Exception"/>.</param>
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        internal static void ReportException(Exception ex)
        {
            Console.WriteLine();
            Console.WriteLine("An error occurred.");
            Console.WriteLine(ex.GetType().ToString());
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        /// <summary>Displays a pause message and waits for user input.</summary>
        [method: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Application is not localized.")]
        internal static void PauseAndWait()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
