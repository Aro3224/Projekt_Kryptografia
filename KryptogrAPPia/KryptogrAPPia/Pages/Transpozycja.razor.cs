using Microsoft.AspNetCore.Components;

namespace KryptogrAPPia.Pages
{
    public partial class Transpozycja
    {
        private string OriginalText { get; set; } = string.Empty;
        private int ColumnCount { get; set; } = 0;
        private string EncryptedText { get; set; }
        private string DecryptedText { get; set; }
        private bool IsInputDisabled { get; set; } = false;
        private bool IsDecryptButtonDisabled { get; set; } = true;
        private string ErrorMessage { get; set; } = string.Empty;
        private char[][] Matrix { get; set; }

        private void EncryptTransposition()
        {
            if (string.IsNullOrWhiteSpace(OriginalText))
            {
                ErrorMessage = "Tekst do zaszyfrowania nie mo¿e byæ pusty.";
                return;
            }

            if (ColumnCount < 1 || ColumnCount >= OriginalText.Length)
            {
                ErrorMessage = "Liczba kolumn musi byæ wiêksza od 0 i mniejsza ni¿ d³ugoœæ tekstu.";
                return;
            }

            ErrorMessage = string.Empty;
            Matrix = GenerateMatrix(OriginalText, ColumnCount);
            EncryptedText = TransposeMatrix(Matrix, ColumnCount, encrypt: true);
            IsInputDisabled = true;
            IsDecryptButtonDisabled = false;
        }

        private void DecryptTransposition()
        {
            if (string.IsNullOrWhiteSpace(EncryptedText) || ColumnCount < 1)
            {
                ErrorMessage = "Zaszyfrowany tekst oraz liczba kolumn musz¹ byæ podane.";
                return;
            }

            ErrorMessage = string.Empty;
            Matrix = GenerateDecryptionMatrix(EncryptedText, ColumnCount);
            DecryptedText = TransposeMatrix(Matrix, ColumnCount, encrypt: false);
            IsInputDisabled = false;
            IsDecryptButtonDisabled = true;
        }

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

        private char[][] GenerateDecryptionMatrix(string encryptedText, int columns)
        {
            int rows = (int)Math.Ceiling((double)encryptedText.Length / columns);
            var matrix = new char[rows][];
            int charIndex = 0;

            int fullColumns = encryptedText.Length % columns;
            int fullColumnHeight = rows;
            int shortColumnHeight = rows - 1;

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = new char[columns];
            }

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

        private string TransposeMatrix(char[][] matrix, int columns, bool encrypt)
        {
            int rows = matrix.Length;
            string result = "";

            if (encrypt)
            {
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

        private void DisableDecryptButton(ChangeEventArgs e)
        {
            IsDecryptButtonDisabled = true;
            DecryptedText = null;
        }
    }
}
