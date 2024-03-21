namespace PROGRAM
{
    /// <summary>
    /// Provides methods for reading and parsing input.
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Prompts the user to enter a choice from a set of valid inputs.
        /// </summary>
        /// <param name="validInputs">An array of strings representing valid inputs.</param>
        /// <param name="prompt">The prompt message displayed to the user.</param>
        /// <returns>The user's choice as a string.</returns>
        internal static string GetChoiceFromUser(string[] validInputs, string prompt)
        {
            Console.Write(prompt + ": ");
            string? input = Console.ReadLine();

            while (string.IsNullOrEmpty(input) || !validInputs.Contains(input))
            {
                Console.Write($"Invalid input. Please try again.\n{prompt}: ");
                input = Console.ReadLine();
            }

            return input;
        }

        /// <summary>
        /// Prompts the user to enter an integer value.
        /// </summary>
        /// <param name="prompt">The prompt message displayed to the user.</param>
        /// <returns>The integer value entered by the user.</returns>
        internal static int GetIntFromUser(string prompt)
        {
            Console.Write(prompt + ": ");
            string? input = Console.ReadLine();
            int result;

            while (string.IsNullOrEmpty(input) || !int.TryParse(input, out result))
            {
                Console.Write($"Invalid input. Please try again.\n{prompt}: ");
                input = Console.ReadLine();
            }

            return result;
        }

        /// <summary>
        /// Reads a file containing integers, one per line, and returns an array of those integers.
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>An array of integers read from the file.</returns>
        internal static int[] ReadIntFile(string fileName)
        {
            // Read each line as a string
            string[] lines = File.ReadAllLines(fileName);
            int[] data = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                // Parse each line to an integer and add to the array
                if (int.TryParse(lines[i], out int number))
                    data[i] = number;
                else
                    Console.WriteLine($"Could not parse '{lines[i]}' to an integer.");
            }

            return data;
        }
    }
}
