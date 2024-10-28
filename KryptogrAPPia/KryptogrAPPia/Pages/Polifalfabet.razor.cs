namespace KryptogrAPPia.Pages
{
    public partial class Polifalfabet
    {
        private string OriginalText { get; set; } = string.Empty;
        private string Key { get; set; } = string.Empty;
        private string EncryptedText { get; set; }
        private string DecryptedText { get; set; }
        private bool IsInputDisabled { get; set; } = false;
        private string ErrorMessage { get; set; } = string.Empty;

        // Metoda szyfrowania z walidacją
        private void EncryptPoli()
        {
            // Sprawdzanie, czy tekst i klucz są podane
            if (string.IsNullOrWhiteSpace(OriginalText) || string.IsNullOrWhiteSpace(Key))
            {
                ErrorMessage = "Tekst oraz klucz muszą być podane.";
                return;
            }

            ErrorMessage = string.Empty; // Czyszczenie komunikatu o błędzie

            EncryptedText = VigenereCipher(OriginalText, Key, true);
            IsInputDisabled = true; // Blokowanie pól po szyfrowaniu
        }

        // Metoda deszyfrowania
        private void DecryptPoli()
        {
            DecryptedText = VigenereCipher(EncryptedText, Key, false);
            IsInputDisabled = false; // Odblokowanie pól po odszyfrowaniu
        }

        // Algorytm Vigenère’a
        private string VigenereCipher(string text, string key, bool encrypt)
        {
            string result = "";
            key = key.ToUpper();
            text = text.ToUpper();
            int keyIndex = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                if (char.IsLetter(currentChar))
                {
                    int offset = key[keyIndex] - 'A';
                    if (!encrypt) offset = -offset;

                    char newChar = (char)((currentChar + offset - 'A' + 26) % 26 + 'A');
                    result += newChar;

                    keyIndex = (keyIndex + 1) % key.Length;
                }
                else
                {
                    result += currentChar;
                }
            }

            return result;
        }
    }
}
