using Microsoft.JSInterop;

namespace KryptogrAPPia.Pages
{
    public partial class PodpisCyfrowy
    {
        private string PublicKey { get; set; } = string.Empty;
        private string PrivateKey { get; set; } = string.Empty;
        private string Message { get; set; } = string.Empty;
        private string Signature { get; set; } = string.Empty;
        private bool? VerificationResult { get; set; } = null;
        private string ErrorMessage { get; set; } = string.Empty;

        private bool isSigningInProgress { get; set; } = false;

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

        private async Task SignMessage()
        {
            try
            {
                ErrorMessage = string.Empty;
                Signature = await JS.InvokeAsync<string>("signMessage", Message, PrivateKey);

                isSigningInProgress = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task VerifySignature()
        {
            try
            {
                ErrorMessage = string.Empty;
                VerificationResult = await JS.InvokeAsync<bool>("verifyMessage", Message, Signature, PublicKey);

                isSigningInProgress = false;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                VerificationResult = null;

                isSigningInProgress = false;
            }
        }

        private class KeyPair
        {
            public string PublicKey { get; set; }
            public string PrivateKey { get; set; }
        }
    }
}