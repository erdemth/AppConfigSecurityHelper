# AppConfigSecurityHelper

This simple tool makes use of **System.Security.Cryptography.ProtectedData** to encrypt data per machine (e.g. to protect configuration sections or certain values of configuration-keys).

ProtectedData in this case operates on machine level. So one needs to recreate the encrypted data on every machine the configuration should be used on.

## Encrypt
For better "data exchange" the string is additionally encoded as Base64-string.

    CustomSoft.AppConfigSecurityHelper.exe encrypt TehSecretKey TheDataToEncryptAsString

## Decrypt

    CustomSoft.AppConfigSecurityHelper.exe decrypt TehSecretKey AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAKGFSrmM/OkGBk6KFhhQzrgQAAAACAAAAAAADZgAAwAAAABAAAAAO8oCByALV94R/j7Q8s6PYAAAAAASAAACgAAAAEAAAAG6vDJxPuW9VUnRPYtQpvvoQAAAApMTSVm2R1/ZRRvhEwZ3qXBQAAACkDSCGCC+NnnuAI8ZcVGMw5kjhZA==
    
    
**Note:** One can only decrypt this base64 string on the machine it was encrypted (or the configured machine key on that machine).
