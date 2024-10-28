window.encryptAES = function (plainText, key) {
    console.log("Encrypting text:", plainText); // Dodano log
    console.log("Using key:", key); // Dodano log

    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Szyfrowanie AES
    const encrypted = CryptoJS.AES.encrypt(plainText, key).toString();
    console.log("Encrypted text:", encrypted); // Dodano log
    return encrypted;
};
