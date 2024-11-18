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

        private void EncryptTransposition()
        {
            if (string.IsNullOrWhiteSpace(OriginalText) || ColumnCount < 1)
            {
                ErrorMessage = "Tekst oraz liczba kolumn musz¹ byæ podane.";
                return;
            }

            ErrorMessage = string.Empty;

            // Wygeneruj macierz transpozycji i zaszyfruj tekst
            Matrix = GenerateMatrix(OriginalText, ColumnCount);
            EncryptedText = TransposeMatrix(Matrix, ColumnCount, encrypt: true);
            IsInputDisabled = true;
        }

        private void DecryptTransposition()
        {
            if (string.IsNullOrWhiteSpace(EncryptedText) || ColumnCount < 1)
            {
                ErrorMessage = "Zaszyfrowany tekst oraz liczba kolumn musz¹ byæ podane.";
                return;
            }

            ErrorMessage = string.Empty;

            // Deszyfrowanie poprzez odbudowanie macierzy z zaszyfrowanego tekstu
            Matrix = GenerateDecryptionMatrix(EncryptedText, ColumnCount);
            DecryptedText = TransposeMatrix(Matrix, ColumnCount, encrypt: false);
            IsInputDisabled = false;
        }

        // Generowanie macierzy do szyfrowania na podstawie tekstu i liczby kolumn
        private char[][] GenerateMatrix(string text, int columns)
        {
            int rows = (int)Math.Ceiling((double)text.Length / columns);
            var matrix = new char[rows][];
            int charIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new char[columns];
                for (int j = 0; j < columns; j++)
                {
                    matrix[i][j] = charIndex < text.Length ? text[charIndex++] : '\0';
                }
            }
            return matrix;
        }

        // Generowanie macierzy do deszyfrowania na podstawie zaszyfrowanego tekstu i liczby kolumn
        private char[][] GenerateDecryptionMatrix(string encryptedText, int columns)
        {
            int rows = (int)Math.Ceiling((double)encryptedText.Length / columns);
            var matrix = new char[rows][];
            int charIndex = 0;

            // Oblicz, ile znaków przypada na ka¿d¹ kolumnê
            int fullColumns = encryptedText.Length % columns;
            int fullColumnHeight = rows;
            int shortColumnHeight = rows - 1;

            // Inicjalizacja macierzy
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new char[columns];
            }

            // Wype³nianie macierzy kolumnami, uwzglêdniaj¹c wysokoœæ ka¿dej kolumny
            for (int col = 0; col < columns; col++)
            {
                int currentHeight = (col < fullColumns) ? fullColumnHeight : shortColumnHeight;
                for (int row = 0; row < currentHeight; row++)
                {
                    if (charIndex < encryptedText.Length)
                    {
                        matrix[row][col] = encryptedText[charIndex++];
                    }
                }
            }

            return matrix;
        }

        // Algorytm transpozycyjny do szyfrowania i deszyfrowania
        private string TransposeMatrix(char[][] matrix, int columns, bool encrypt)
        {
            int rows = matrix.Length;
            string result = "";

            if (encrypt)
            {
                // Czytanie kolumnami dla szyfrowania
                for (int j = 0; j < columns; j++)
                {
                    for (int i = 0; i < rows; i++)
                    {
                        if (matrix[i][j] != '\0')
                        {
                            result += matrix[i][j];
                        }
                    }
                }
            }
            else
            {
                // Odczytywanie wierszami dla odszyfrowania
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (matrix[i][j] != '\0')
                        {
                            result += matrix[i][j];
                        }
                    }
                }
            }

            return result;
        }
    }
}
