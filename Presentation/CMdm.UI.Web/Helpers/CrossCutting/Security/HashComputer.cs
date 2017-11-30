using System;
using System.Security.Cryptography;

namespace CMdm.UI.Web.Helpers.CrossCutting.Security
{
    public class HashComputer
    {
        public string GetPasswordHashAndSalt(string message)
        {
            //// Let us use SHA256 algorithm to 
            //// generate the hash from this salted password
            //SHA256 sha = new SHA256CryptoServiceProvider();
            //byte[] dataBytes = Utility.GetBytes(message);
            //byte[] resultBytes = sha.ComputeHash(dataBytes);


            // Create a new instance of the hash crypto service provider.
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
            // Convert the data to hash to an array of Bytes.
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(message);
            // Compute the Hash. This returns an array of Bytes.
            byte[] bytHash = hashAlg.ComputeHash(bytValue);
            // Optionally, represent the hash value as a base64-encoded string, 
            // For example, if you need to display the value or transmit it over a network.
            string base64 = Convert.ToBase64String(bytHash);

            return base64;
            // return the hash string to the caller
            //return Utility.GetString(resultBytes);
        }
    }
}