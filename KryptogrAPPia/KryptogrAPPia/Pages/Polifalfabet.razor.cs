namespace KryptogrAPPia.Pages
{
    public partial class Polifalfabet
    {
        private string OriginalText { get; set; }

        private string Key { get; set; }
        private string EncryptedText { get; set; }

        private string DecryptedText { get; set; }

        //Szyfrowanie
        public string EncryptPoli(string text, string key)
        {
            EncryptedText = VigenereCipher(text, key, true);
            return EncryptedText;
        }

        //Deszyfrowanie
        private void DecryptPoli()
        {
            DecryptedText = VigenereCipher(EncryptedText, Key, false);
        }

        //Algorytm do szyfrowania Vigenère’a
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
                    // Znajdź przesunięcie na podstawie klucza
                    int offset = key[keyIndex] - 'A';
                    if (!encrypt) offset = -offset; //Deszyfrowanie poprzez odwrócenie

                    //Szyfrowanie/Deszyfrowanie
                    char newChar = (char)((currentChar + offset - 'A' + 26) % 26 + 'A');
                    result += newChar;

                    //Następna litera klucza
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
