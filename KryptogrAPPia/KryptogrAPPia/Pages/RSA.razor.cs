using Microsoft.JSInterop;

namespace KryptogrAPPia.Pages
{
    public partial class RSA
    {
        private string PublicKey { get; set; } = string.Empty;
        private string PrivateKey { get; set; } = string.Empty;
        private string Message { get; set; } = string.Empty;
        private string EncryptedMessage { get; set; } = string.Empty;
        private string DecryptedMessage { get; set; } = string.Empty;
        private string ErrorMessage { get; set; } = string.Empty;

        private async Task GenerateKeys()
        {
            try
            {
                ErrorMessage = string.Empty;
                var keys = await JS.InvokeAsync<KeyPair>("generateRSAKeys");
                PublicKey = keys.PublicKey;
                PrivateKey = keys.PrivateKey;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task EncryptMessage()
        {
            try
            {
                ErrorMessage = string.Empty;
                EncryptedMessage = await JS.InvokeAsync<string>("encryptRSA", Message, PublicKey);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task DecryptMessage()
        {
            try
            {
                ErrorMessage = string.Empty;
                DecryptedMessage = await JS.InvokeAsync<string>("decryptRSA", EncryptedMessage, PrivateKey);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private class KeyPair
        {
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
        }
    }
}