namespace CustomSoft.AppConfigSecurityHelper
{
  using System;
  using System.Security;
  using System.Text;

  class Program
  {
    static void Main(string[] args)
    {
      if (args.Length != 3)
      {
        Console.WriteLine("This tool decrypts and/or encrypts certain strings with the given key for the LocalMachine.");
        Console.WriteLine("Usage: AppConfigSecurityHelper method (decrypt|encrypt) key input");
        return;
      }

      Console.WriteLine("--");
      if (args[0].ToLowerInvariant().Equals("decrypt"))
      {
        var dec = DecryptString(args[2], Encoding.Unicode.GetBytes(args[1]));
        Console.WriteLine(ToInsecureString(dec));
      }
      else
      {
        var enc = EncryptString(args[2], Encoding.Unicode.GetBytes(args[1]));

        Console.WriteLine(enc);
        Console.WriteLine(ToSecureString(enc));
      }
      Console.WriteLine("--");
    }

    #region Encryption code
    public static string EncryptString(string input, Byte[] key)
    {
      byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
          System.Text.Encoding.Unicode.GetBytes(input),
          key,
          System.Security.Cryptography.DataProtectionScope.LocalMachine);
      return Convert.ToBase64String(encryptedData);
    }

    public static SecureString DecryptString(string encryptedData, Byte[] key)
    {
      try
      {
        byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
            Convert.FromBase64String(encryptedData),
            key,
            System.Security.Cryptography.DataProtectionScope.LocalMachine);
        return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
      }
      catch
      {
        return new SecureString();
      }
    }

    public static SecureString ToSecureString(string input)
    {
      SecureString secure = new SecureString();
      foreach (char c in input.ToCharArray())
      {
        secure.AppendChar(c);
      }
      secure.MakeReadOnly();
      return secure;
    }

    public static string ToInsecureString(SecureString input)
    {
      string returnValue = string.Empty;
      IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
      try
      {
        returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
      }
      finally
      {
        System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
      }
      return returnValue;
    } 
    #endregion
  }
}
