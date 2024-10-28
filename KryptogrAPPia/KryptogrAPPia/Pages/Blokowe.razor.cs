using Microsoft.JSInterop;

namespace KryptogrAPPia.Pages
{
    public partial class Blokowe
    {
        private string InputText { get; set; }
        private string EncryptionKey { get; set; }
        private string EncryptedText { get; set; }
        private string ErrorMessage { get; set; }

        private async Task EncryptText()
        {
            try
            {
                // Walidacja d³ugoœci klucza
                if (string.IsNullOrEmpty(EncryptionKey) ||
                    (EncryptionKey.Length != 16 && EncryptionKey.Length != 24 && EncryptionKey.Length != 32))
                {
                    throw new Exception("Klucz szyfrowania musi mieæ 16, 24 lub 32 znaki.");
                }

                EncryptedText = await JS.InvokeAsync<string>("encryptAES", InputText, EncryptionKey);
                Console.WriteLine($"Zaszyfrowany tekst: {EncryptedText}");
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Console.WriteLine($"B³¹d: {ex.Message}");
            }
        }
    }
}
