using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text;

namespace KryptogrAPPia.Pages
{
    public partial class BlokoweDes
    {
        private string InputText { get; set; }

        private string EncryptionKey { get; set; }

        private string EncryptedText { get; set; }

        private string DecryptedText { get; set; }

        private string ErrorMessage { get; set; } = string.Empty;

        private bool isEncrypted { get; set; } = false;

        private bool isDecrypting { get; set; } = false;

        // Funkcja odpowiedzialna za szyfrowanie tekstu.
        private async Task EncryptText()
        {
            try
            {
                ErrorMessage = string.Empty;

                // Walidacja d³ugoœci klucza szyfrowania
                if (string.IsNullOrEmpty(EncryptionKey) || EncryptionKey.Length < 8)
                {
                    throw new Exception("Klucz szyfrowania musi mieæ co najmniej 8 znaków.");
                }

                // Szyfrowanie tekstu za pomoc¹ DES
                EncryptedText = await JS.InvokeAsync<string>("encryptDES", InputText, EncryptionKey);
                Console.WriteLine($"Zaszyfrowany tekst: {EncryptedText}");

                // Zablokowanie przycisków po zaszyfrowaniu
                isEncrypted = true;
                isDecrypting = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        // Funkcja odpowiedzialna za deszyfrowanie tekstu.
        private async Task DecryptText()
        {
            try
            {
                ErrorMessage = string.Empty;

                // Walidacja d³ugoœci klucza deszyfrowania
                if (string.IsNullOrEmpty(EncryptionKey) || EncryptionKey.Length < 8)
                {
                    throw new Exception("Klucz deszyfrowania musi mieæ co najmniej 8 znaków.");
                }

                // Zablokowanie przycisku "Odszyfruj" podczas deszyfrowania
                isDecrypting = true;

                // Odszyfrowanie tekstu za pomoc¹ DES
                DecryptedText = await JS.InvokeAsync<string>("decryptDES", EncryptedText, EncryptionKey);
                Console.WriteLine($"Odszyfrowany tekst: {DecryptedText}");

                // Odblokowanie przycisków po deszyfrowaniu
                isEncrypted = false;
                isDecrypting = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                isDecrypting = false;
            }
        }

        // Funkcja odpowiedzialna za za³adowanie pliku tekstowego do zaszyfrowania.
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
