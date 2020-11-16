using System;

namespace DataEncryptionStandard
{
    public class DES
    {
        private string _text;
        private string _key;

        public DES(string text, string key)
        {
            _text = text;
            _key = key;
        }
    }
}