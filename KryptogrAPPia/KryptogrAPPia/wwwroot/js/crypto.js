// crypto.js
window.encryptAES = function (plainText, key) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Szyfrowanie AES
    const encrypted = CryptoJS.AES.encrypt(plainText, key).toString();
    return encrypted;
};

window.decryptAES = function (encryptedText, key) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Deszyfrowanie AES
    const decrypted = CryptoJS.AES.decrypt(encryptedText, key);
    return decrypted.toString(CryptoJS.enc.Utf8);
};
