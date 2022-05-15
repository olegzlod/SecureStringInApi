using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace SecureStringInApi
{

    public class BoltSecureString : IDisposable
    {
        private SecureString _securedValue = new SecureString();
        private int _unmaskedSize = 4;
        private StringBuilder _strBld = new StringBuilder();
        private static readonly List<char> _charsToIgnore = new List<char> { ' ', '\t', '\r', '\n' };

        public string Value
        {
            set
            {

                int length = value.Length;
                for (int i = 0; i < length; i++)
                {
                    char c = value[i];
                    //ignore whitespaces etc
                    if (_charsToIgnore.Contains(c))
                    {
                        continue;
                    }
                    // adding a char to securestring
                    _securedValue.AppendChar(c);
                    // mask for serialization
                    // if last positions that should not be masked copy it
                    if (i >= length - _unmaskedSize)
                    {
                        _strBld.Append(c);
                    }
                    // mask character
                    else
                    {
                        _strBld.Append('*');
                    }
                }
            }
            get
            {
                //return masked value
                return _strBld.ToString();
            }
        }

        public void Dispose()
        {
            _securedValue.Dispose();
        }

        // returns the value as pain text
        public string SecureStringToString()
        {
            SecureString value = _securedValue;
            IntPtr valuePtr = IntPtr.Zero;
            
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(valuePtr);

            //#if NET45
            //                Password = Marshal.SecureStringToGlobalAllocUnicode(password);
            //#else
            //            Password = SecureStringMarshal.SecureStringToGlobalAllocUnicode(password);
            //#endif

        }
    }
    public class CreditCard : IDisposable
    {
        public BoltSecureString CreditCardNumber { get; set; }
        public string FullName { get; set; }

        public void Dispose()
        {
            CreditCardNumber.Dispose();
        }
    }

    public class AllData
    {
        public string OriginalData { get; set; }
        public CreditCard CreditCard { get; set; }
    }

}
