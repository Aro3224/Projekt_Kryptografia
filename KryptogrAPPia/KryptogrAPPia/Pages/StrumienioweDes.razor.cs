using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text;

namespace KryptogrAPPia.Pages
{
    public partial class StrumienioweDes
    {
        private string InputText { get; set; }
        private string EncryptionKey { get; set; }
        private string IV { get; set; }
        private string EncryptedText { get; set; }
        private string DecryptedText { get; set; }
        private string ErrorMessage { get; set; } = string.Empty;
        private bool isEncrypted { get; set; } = false;
        private bool isDecrypting { get; set; } = false;

        //Funkcja szyfruj�ca tekst
        private async Task EncryptText()
        {
            try
            {
                ErrorMessage = string.Empty;

                //Walidacja d�ugo�ci klucza
                if (string.IsNullOrEmpty(EncryptionKey) || EncryptionKey.Length < 8)
                {
                    throw new Exception("Klucz szyfrowania musi mie� co najmniej 8 znak�w.");
                }

                if (string.IsNullOrEmpty(IV) || IV.Length < 8)
                {
                    throw new Exception("Wektor inicjalizacyjny musi mie� co najmniej 8 znak�w.");
                }

                EncryptedText = await JS.InvokeAsync<string>("encryptDESStream", InputText, EncryptionKey, IV);
                Console.WriteLine($"Zaszyfrowany tekst: {EncryptedText}");

                isEncrypted = true;
                isDecrypting = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        //Funkcja deszyfruj�ca tekst
        private async Task DecryptText()
        {
            try
            {
                ErrorMessage = string.Empty;

                //Walidacja d�ugo�ci klucza
                if (string.IsNullOrEmpty(EncryptionKey) || EncryptionKey.Length < 8)
                {
                    throw new Exception("Klucz deszyfrowania musi mie� co najmniej 8 znak�w.");
                }

                if (string.IsNullOrEmpty(IV) || IV.Length < 8)
                {
                    throw new Exception("Wektor inicjalizacyjny musi mie� co najmniej 8 znak�w.");
                }

                isDecrypting = true;

                //Odszyfrowanie tekstu
                DecryptedText = await JS.InvokeAsync<string>("decryptDESStream", EncryptedText, EncryptionKey, IV);
                Console.WriteLine($"Odszyfrowany tekst: {DecryptedText}");

                isEncrypted = false;
                isDecrypting = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                isDecrypting = false;
            }
        }

        // Funkcja odpowiedzialna za za�adowanie pliku tekstowego
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
