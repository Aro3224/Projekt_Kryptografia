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

window.encryptDES = function (plainText, key) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Szyfrowanie DES
    const encrypted = CryptoJS.DES.encrypt(plainText, key).toString();
    return encrypted;
};

window.decryptDES = function (encryptedText, key) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    // Deszyfrowanie DES
    const decrypted = CryptoJS.DES.decrypt(encryptedText, key);
    return decrypted.toString(CryptoJS.enc.Utf8);
};

//Funkcja szyfrująca tekst w trybie CTR za pomocą AES
window.encryptStream = function (plainText, key, iv) {
    //Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    //Konwertowanie klucza i wektora inicjalizacyjnego do formatu WordArray
    var keyHex = CryptoJS.enc.Utf8.parse(key);
    var ivHex = CryptoJS.enc.Utf8.parse(iv);

    //Szyfrowanie za pomocą AES w trybie CTR
    const encrypted = CryptoJS.AES.encrypt(plainText, keyHex, {
        iv: ivHex,
        mode: CryptoJS.mode.CTR,
        padding: CryptoJS.pad.NoPadding
    });

    return encrypted.toString(); //Zwracamy zaszyfrowany tekst w formacie Base64
};

// Funkcja deszyfrująca tekst w trybie CTR za pomocą AES
window.decryptStream = function (encryptedText, key, iv) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    //Konwertowanie klucza i wektora inicjalizacyjnego do formatu WordArray
    var keyHex = CryptoJS.enc.Utf8.parse(key);
    var ivHex = CryptoJS.enc.Utf8.parse(iv);

    //Deszyfrowanie za pomocą AES w trybie CTR
    const decrypted = CryptoJS.AES.decrypt(encryptedText, keyHex, {
        iv: ivHex,
        mode: CryptoJS.mode.CTR,
        padding: CryptoJS.pad.NoPadding
    });

    //Zwracamy odszyfrowany tekst
    return decrypted.toString(CryptoJS.enc.Utf8);
};

window.encryptDESStream = function (plainText, key, iv) {
    // Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    //Konwersja klucza i wektora do odpowiednich formatów
    const keyHex = CryptoJS.enc.Utf8.parse(key);
    const ivHex = CryptoJS.enc.Utf8.parse(iv);

    // Szyfrowanie w trybie strumieniowym (ECB z IV)
    const encrypted = CryptoJS.DES.encrypt(plainText, keyHex, {
        iv: ivHex,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });

    return encrypted.toString();
};

window.decryptDESStream = function (encryptedText, key, iv) {
    //Sprawdzenie, czy CryptoJS jest załadowane
    if (typeof CryptoJS === 'undefined') {
        console.error("CryptoJS is not loaded.");
        return;
    }

    //Konwersja klucza i wektora do odpowiednich formatów
    const keyHex = CryptoJS.enc.Utf8.parse(key);
    const ivHex = CryptoJS.enc.Utf8.parse(iv);

    //Deszyfrowanie w trybie strumieniowym (ECB z IV)
    const decrypted = CryptoJS.DES.decrypt(encryptedText, keyHex, {
        iv: ivHex,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });

    return decrypted.toString(CryptoJS.enc.Utf8);
};

window.generateRSAKeys = function () {
    if (typeof forge === 'undefined') {
        console.error("Forge library is not loaded.");
        return;
    }

    const keypair = forge.pki.rsa.generateKeyPair({ bits: 2048, e: 0x10001 });
    return {
        PublicKey: forge.pki.publicKeyToPem(keypair.publicKey),
        PrivateKey: forge.pki.privateKeyToPem(keypair.privateKey),
    };
};

window.encryptRSA = function (message, publicKeyPem) {
    if (typeof forge === 'undefined') {
        console.error("Forge library is not loaded.");
        return;
    }

    const publicKey = forge.pki.publicKeyFromPem(publicKeyPem);
    const encrypted = publicKey.encrypt(message, 'RSA-OAEP');
    return forge.util.encode64(encrypted);
};

window.decryptRSA = function (encryptedMessage, privateKeyPem) {
    if (typeof forge === 'undefined') {
        console.error("Forge library is not loaded.");
        return;
    }

    const privateKey = forge.pki.privateKeyFromPem(privateKeyPem);
    const encryptedBytes = forge.util.decode64(encryptedMessage);
    return privateKey.decrypt(encryptedBytes, 'RSA-OAEP');
};

window.generateHMAC = async function (message, key, algorithm) {
    try {
        if (!window.crypto || !window.crypto.subtle) {
            throw new Error("Web Crypto API nie jest obsługiwane w tej przeglądarce.");
        }

        const encoder = new TextEncoder();
        const keyData = encoder.encode(key);
        const messageData = encoder.encode(message);

        const cryptoKey = await window.crypto.subtle.importKey(
            "raw",
            keyData,
            { name: "HMAC", hash: { name: algorithm } },
            false,
            ["sign"]
        );

        const signature = await window.crypto.subtle.sign(
            "HMAC",
            cryptoKey,
            messageData
        );

        return btoa(String.fromCharCode(...new Uint8Array(signature)));
    } catch (error) {
        console.error("Błąd generowania HMAC:", error);
        throw error;
    }
};
