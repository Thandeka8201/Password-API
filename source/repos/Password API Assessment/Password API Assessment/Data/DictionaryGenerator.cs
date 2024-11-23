using Password_API_Assessment.Interfaces;
using System;
using System.IO;

namespace Password_API_Assessment.Data
{
    public class DictionaryGenerator : IDictionaryGenerator
    {
        public void GenerateDictionary(string fileName, string baseWord)
        {
            try
            {
                Console.WriteLine("Generating dictionary...");
                char[] substitutions = { '@', '5', '0' };

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    GeneratePermutations(baseWord, 0, substitutions, writer);
                }

                Console.WriteLine($"Dictionary saved as {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating dictionary: {ex.Message}");
            }
        }

        private void GeneratePermutations(string word, int index, char[] substitutions, StreamWriter writer)
        {
            if (index == word.Length)
            {
                writer.WriteLine(word);
                return;
            }

            char original = word[index];
            GeneratePermutations(word, index + 1, substitutions, writer);

            if (char.IsLetter(original))
            {
                char toggled = char.IsLower(original) ? char.ToUpper(original) : char.ToLower(original);
                string modifiedWord = ReplaceCharAt(word, index, toggled);
                GeneratePermutations(modifiedWord, index + 1, substitutions, writer);
            }

            if (original == 'a' || original == 's' || original == 'o')
            {
                foreach (var substitution in substitutions)
                {
                    string modifiedWord = ReplaceCharAt(word, index, substitution);
                    GeneratePermutations(modifiedWord, index + 1, substitutions, writer);
                }
            }
        }

        private string ReplaceCharAt(string word, int index, char newChar)
        {
            char[] chars = word.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
