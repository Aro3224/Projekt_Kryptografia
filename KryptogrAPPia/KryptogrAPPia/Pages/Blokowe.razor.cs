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
        private string DecryptedText { get; set; }
        private string ErrorMessage { get; set; } = string.Empty;
        private bool isEncrypted { get; set; } = false; // Zmienna kontroluj�ca blokad� edytowalno�ci

        private async Task EncryptText()
        {
            try
            {
                // Walidacja d�ugo�ci klucza
                if (string.IsNullOrEmpty(EncryptionKey) || (EncryptionKey.Length != 16 && EncryptionKey.Length != 24 && EncryptionKey.Length != 32))
                {
                    throw new Exception("Klucz szyfrowania musi mie� 16, 24 lub 32 znaki.");
                }

                // Szyfrowanie tekstu
                EncryptedText = await JS.InvokeAsync<string>("encryptAES", InputText, EncryptionKey);
                Console.WriteLine($"Zaszyfrowany tekst: {EncryptedText}");

                // Zablokowanie edycji
                isEncrypted = true; // Blokada dla p�l
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message; // Zapisz komunikat b��du
            }
        }

        private async Task DecryptText()
        {
            try
            {
                // Walidacja d�ugo�ci klucza
                if (string.IsNullOrEmpty(EncryptionKey) || (EncryptionKey.Length != 16 && EncryptionKey.Length != 24 && EncryptionKey.Length != 32))
                {
                    throw new Exception("Klucz deszyfrowania musi mie� 16, 24 lub 32 znaki.");
                }

                // Odszyfrowanie tekstu
                DecryptedText = await JS.InvokeAsync<string>("decryptAES", EncryptedText, EncryptionKey);
                Console.WriteLine($"Odszyfrowany tekst: {DecryptedText}");

                // Odblokowanie edycji
                isEncrypted = false; // Odblokowanie dla p�l
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message; // Zapisz komunikat b��du
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
