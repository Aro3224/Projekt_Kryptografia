using System.Text;

namespace KryptogrAPPia.Pages
{
    public partial class Polifalfabet
    {
        private string OriginalText { get; set; }
        private string Key { get; set; }
        private string EncryptedText { get; set; }
        private string DecryptedText { get; set; }
        private string ErrorMessage { get; set; }
        private bool IsInputDisabled { get; set; } = false;

        //Wywo�anie funkcji szyfrowania tekstu za pomoc� szyfru Gronsfelda
        private void EncryptGronsfeld()
        {
            if (string.IsNullOrWhiteSpace(OriginalText) || string.IsNullOrWhiteSpace(Key))
            {
                ErrorMessage = "Prosz� wprowadzi� zar�wno tekst jak i klucz.";
                return;
            }

            if (!IsKeyValid(Key))
            {
                ErrorMessage = "Klucz musi sk�ada� si� wy��cznie z cyfr.";
                return;
            }

            ErrorMessage = string.Empty;
            EncryptedText = GronsfeldCipher(OriginalText, Key, encrypt: true);
            IsInputDisabled = true;
        }

        //Odszyfrowanie tekstu
        private void DecryptGronsfeld()
        {
            if (string.IsNullOrWhiteSpace(EncryptedText) || string.IsNullOrWhiteSpace(Key))
            {
                ErrorMessage = "Prosz� wprowadzi� zaszyfrowany tekst i klucz.";
                return;
            }

            if (!IsKeyValid(Key))
            {
                ErrorMessage = "Klucz musi sk�ada� si� wy��cznie z cyfr.";
                return;
            }

            ErrorMessage = string.Empty;
            DecryptedText = GronsfeldCipher(EncryptedText, Key, encrypt: false);
            IsInputDisabled = false;
        }

        //Implementacja szyfru Gronsfelda
        private string GronsfeldCipher(string text, string cipherKey, bool encrypt)
        {
            var result = new StringBuilder();
            var keyIndex = 0;
            var keyLength = cipherKey.Length;

            foreach (var ch in text)
            {
                //Przesuwanie tylko liter
                if (char.IsLetter(ch))
                {
                    //Okre�lenie przesuni�cia na podstawie cyfry w kluczu
                    int shift = int.Parse(cipherKey[keyIndex % keyLength].ToString());

                    //Je�li odszyfrowujemy, to u�ywamy przesuni�cia w przeciwn� stron�
                    if (!encrypt)
                    {
                        shift = 26 - shift; //Odejmujemy przesuni�cie zamiast dodawa�
                    }

                    //Przesuni�cie dla ma�ych liter
                    if (char.IsLower(ch))
                    {
                        result.Append((char)((((ch - 'a') + shift) % 26) + 'a'));
                    }
                    //Przesuni�cie dla du�ych liter
                    else if (char.IsUpper(ch))
                    {
                        result.Append((char)((((ch - 'A') + shift) % 26) + 'A'));
                    }

                    keyIndex++; //Przechodzimy do kolejnej cyfry w kluczu
                }
                else
                {
                    result.Append(ch); //Dodajemy inne znaki bez zmian (np. spacje, przecinki)
                }
            }

            return result.ToString();
        }

        //Sprawdzanie, czy klucz zawiera tylko cyfry
        private bool IsKeyValid(string key)
        {
            return key.All(char.IsDigit);
        }
    }
}