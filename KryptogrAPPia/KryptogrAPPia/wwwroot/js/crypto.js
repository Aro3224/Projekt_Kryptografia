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

// Szyfrowanie DES
window.encryptDES = function (plainText, key) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Tworzenie klucza i wektora inicjalizacyjnego (IV) - przyjmujemy 8 pierwszych znaków klucza jako IV
    const keyBytes = CryptoJS.enc.Utf8.parse(key); // Klucz DES
    const iv = CryptoJS.enc.Utf8.parse(key.substring(0, 8)); // Pierwsze 8 znaków klucza jako IV

    // Szyfrowanie DES
    const encrypted = CryptoJS.DES.encrypt(plainText, keyBytes, { iv: iv });
    return encrypted.toString();
};

// Deszyfrowanie DES
window.decryptDES = function (encryptedText, key) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Tworzenie klucza i wektora inicjalizacyjnego (IV) - przyjmujemy 8 pierwszych znaków klucza jako IV
    const keyBytes = CryptoJS.enc.Utf8.parse(key); // Klucz DES
    const iv = CryptoJS.enc.Utf8.parse(key.substring(0, 8)); // Pierwsze 8 znaków klucza jako IV

    // Deszyfrowanie DES
    const decrypted = CryptoJS.DES.decrypt(encryptedText, keyBytes, { iv: iv });
    return decrypted.toString(CryptoJS.enc.Utf8);
};

// Szyfrowanie strumieniowe (RC4, z IV)
window.encryptStream = function (plainText, key, iv) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Generowanie IV w formacie odpowiednim dla CryptoJS
    const ivBytes = CryptoJS.enc.Utf8.parse(iv); // Wektor inicjalizacji
    const keyBytes = CryptoJS.enc.Utf8.parse(key); // Klucz

    // Szyfrowanie strumieniowe przy użyciu RC4
    const encrypted = CryptoJS.RC4.encrypt(plainText, keyBytes);
    return encrypted.toString();
};

// Deszyfrowanie strumieniowe (RC4, z IV)
window.decryptStream = function (encryptedText, key, iv) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Generowanie IV w formacie odpowiednim dla CryptoJS
    const ivBytes = CryptoJS.enc.Utf8.parse(iv); // Wektor inicjalizacji
    const keyBytes = CryptoJS.enc.Utf8.parse(key); // Klucz

    // Deszyfrowanie strumieniowe przy użyciu RC4
    const decrypted = CryptoJS.RC4.decrypt(encryptedText, keyBytes);
    return decrypted.toString(CryptoJS.enc.Utf8);
};
