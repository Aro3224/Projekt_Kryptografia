using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text;

namespace KryptogrAPPia.Pages
{
    public partial class Blokowe
    {
        private string InputText { get; set; }
        private string EncryptionKey { get; set; }
        private string EncryptedText { get; set; }
        private string ErrorMessage { get; set; } = string.Empty;

        private async Task EncryptText()
        {
            try
            {
                // Walidacja d³ugoœci klucza
                if (string.IsNullOrEmpty(EncryptionKey) || (EncryptionKey.Length != 16 && EncryptionKey.Length != 24 && EncryptionKey.Length != 32))
                {
                    throw new Exception("Klucz szyfrowania musi mieæ 16, 24 lub 32 znaki.");
                }

                EncryptedText = await JS.InvokeAsync<string>("encryptAES", InputText, EncryptionKey);
                Console.WriteLine($"Zaszyfrowany tekst: {EncryptedText}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message; // Zapisz komunikat b³êdu
            }
        }

        private async Task LoadFile(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);
                InputText = Encoding.UTF8.GetString(buffer);
            }
        }
    }
}
