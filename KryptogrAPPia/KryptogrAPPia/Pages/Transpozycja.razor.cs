namespace KryptogrAPPia.Pages
{
    public partial class Transpozycja
    {
        private string OriginalText { get; set; } = string.Empty;
        private int ColumnCount { get; set; } = 0;
        private string EncryptedText { get; set; }
        private string DecryptedText { get; set; }
        private bool IsInputDisabled { get; set; } = false;
        private string ErrorMessage { get; set; } = string.Empty;
        private char[][] Matrix { get; set; }

        // Metoda szyfrowania z walidacj¹
        private void EncryptTransposition()
        {
            if (string.IsNullOrWhiteSpace(OriginalText) || ColumnCount < 1)
            {
                ErrorMessage = "Tekst oraz liczba kolumn musz¹ byæ podane.";
                return;
            }

            ErrorMessage = string.Empty;

            // Wygeneruj macierz transpozycji i zaszyfruj tekst
            EncryptedText = TranspositionCipher(OriginalText, ColumnCount, true);
            IsInputDisabled = true; // Blokowanie pól po szyfrowaniu
        }

        // Metoda deszyfrowania
        private void DecryptTransposition()
        {
            DecryptedText = TranspositionCipher(EncryptedText, ColumnCount, false);
            IsInputDisabled = false; // Odblokowanie pól po odszyfrowaniu
        }

        // Algorytm transpozycyjny
        private string TranspositionCipher(string text, int columns, bool encrypt)
        {
            int rows = (int)Math.Ceiling((double)text.Length / columns);
            Matrix = new char[rows][];

            int charIndex = 0;
            for (int i = 0; i < rows; i++)
            {
                Matrix[i] = new char[columns];
                for (int j = 0; j < columns; j++)
                {
                    Matrix[i][j] = charIndex < text.Length ? text[charIndex++] : '\0'; // Puste miejsce '\0'
                }
            }

            if (encrypt)
            {
                string result = "";
                for (int j = 0; j < columns; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (Matrix[i][j] != '\0') // Pomijanie pustych miejsc na koñcu
                        {
                            result += Matrix[i][j];
                        }
                    }
                }
                return result;
            }
            else
            {
                charIndex = 0;
                string result = "";
                for (int j = 0; j < columns; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (charIndex < text.Length)
                        {
                            Matrix[i][j] = text[charIndex++];
                        }
                    }
                }

                foreach (var row in Matrix)
                {
                    result += new string(row);
                }
                return result.TrimEnd('\0'); // Usuniêcie pustych miejsc na koñcu
            }
        }
    }
}