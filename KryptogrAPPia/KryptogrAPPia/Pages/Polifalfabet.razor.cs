namespace KryptogrAPPia.Pages
{
    public partial class Polifalfabet
    {
        private string OriginalText { get; set; }

        private string Key { get; set; }
        private string EncryptedText { get; set; }

        private string DecryptedText { get; set; }

        public string EncryptPoli(string text)
        {
            EncryptedText = text;
            return EncryptedText;
        }

        private void DecryptPoli()
        {
            DecryptedText = EncryptedText;
        }
    }
}
