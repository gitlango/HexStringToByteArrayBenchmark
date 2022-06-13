using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexStringToByteArrayBenchmark
{
    public static class StackOverflow
    {

        public static Span<Byte> HexStringToSpanOfBytesLotsOfVariables(string hex, Span<Byte> arr)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException("The hexadecimal stream must have an even number of digits");

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                var iLeftShift1 = i << 1;

                var hexChar = hex[iLeftShift1];
                var leftSide = GetHexValLowerCase(hexChar) << 4;

                var hexChar2 = hex[iLeftShift1 + 1];
                var rightSide = GetHexValLowerCase(hexChar2);

                arr[i] = (byte)(leftSide + rightSide);
            }

            return arr;
        }
        
        public static Span<Byte> HexStringToSpanOfBytesCompact(string hex, ref Span<Byte> arr)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException("The hexadecimal stream must have an even number of digits");

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexValLowerCase(hex[i << 1]) << 4) + (GetHexValLowerCase(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexValIgnoreCase(char hexChar)
        {
            int val = (int)hexChar;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public static int GetHexValLowerCase(char hexChar)
        {
            int val = (int)hexChar;
            return val - (val < 58 ? 48 : 87);
        }

        public static int GetHexValUpperCase(char hexChar)
        {
            int val = (int)hexChar;
            return val - (val < 58 ? 48 : 55);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        public static byte[] StringToByteArray1(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                                .Where(x => x % 2 == 0)
                                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                .ToArray();
        }

        //public static byte[] StringToByteArray2(string hexStream)
        //{
        //    byte[] arr = new byte[hexStream.Length >> 1];

        //    for (int i = 0; i < hexStream.Length >> 1; ++i)
        //    {
        //        arr[i] = (byte)((GetHexVal(hexStream[i << 1]) << 4) + (GetHexVal(hexStream[(i << 1) + 1])));
        //    }

        //    return arr;
        //}

        public static byte[] StringToByteArray3(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }

        public static byte[] StringToByteArray4(string str)
        {
            Dictionary<string, byte> hexindex = new Dictionary<string, byte>();
            for (int i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte)i);

            List<byte> hexres = new List<byte>();
            for (int i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        public static byte[] StringToByteArray5(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] StringToByteArray6(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            for (var i = 0; i < outputLength; i++)
                output[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);
            return output;
        }

        public static byte[] StringToByteArray7(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                    output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return output;
        }

        public static byte[] StringToByteArray8(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            var numeral = new char[2];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                {
                    var read = sr.Read(numeral, 0, 2);
                    Debug.Assert(read == 2);
                    output[i] = Convert.ToByte(new string(numeral), 16);
                }
            }
            return output;
        }

        public static byte[] StringToByteArray9(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            var numeral = new char[2];
            using (var sr = new StringReader(input))
            {
                for (var i = 0; i < outputLength; i++)
                {
                    numeral[0] = (char)sr.Read();
                    numeral[1] = (char)sr.Read();
                    output[i] = Convert.ToByte(new string(numeral), 16);
                }
            }
            return output;
        }

        public static byte[] StringToByteArray10(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            var numeral = new char[2];
            for (int i = 0, j = 0; i < outputLength; i++, j += 2)
            {
                input.CopyTo(j, numeral, 0, 2);
                output[i] = Convert.ToByte(new string(numeral), 16);
            }
            return output;
        }

        public static byte[] StringToByteArray11(string input)
        {
            var outputLength = input.Length / 2;
            var output = new byte[outputLength];
            var numeral = new char[2];
            for (int i = 0; i < outputLength; i++)
            {
                input.CopyTo(i * 2, numeral, 0, 2);
                output[i] = Convert.ToByte(new string(numeral), 16);
            }
            return output;
        }

        public static byte[] StringToByteArray12(string s)
        {
            byte[] bytes = new byte[s.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int hi = s[i * 2] - 65;
                hi = hi + 10 + ((hi >> 31) & 7);

                int lo = s[i * 2 + 1] - 65;
                lo = lo + 10 + ((lo >> 31) & 7) & 0x0f;

                bytes[i] = (byte)(lo | hi << 4);
            }
            return bytes;
        }

        private static readonly byte[] LookupTable = new byte[] {
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
};

        private static byte Lookup(char c)
        {
            var b = LookupTable[c];
            if (b == 255)
                throw new IOException("Expected a hex character, got " + c);
            return b;
        }
               

        public static byte[] StringToByteArray14(string hexString)
        {
            int hexStringLength = hexString.Length;
            byte[] b = new byte[hexStringLength / 2];
            for (int i = 0; i < hexStringLength; i += 2)
            {
                int topChar = (hexString[i] > 0x40 ? hexString[i] - 0x37 : hexString[i] - 0x30) << 4;
                int bottomChar = hexString[i + 1] > 0x40 ? hexString[i + 1] - 0x37 : hexString[i + 1] - 0x30;
                b[i / 2] = Convert.ToByte(topChar + bottomChar);
            }
            return b;
        }

        public static byte[] StringToByteArray15(string hexString)
        {
            if (hexString.Length % 2 != 0) throw new ArgumentException("String must have an even length");
            var array = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                array[i / 2] = ByteFromTwoChars(hexString[i], hexString[i + 1]);
            }
            return array;
        }

        private static byte ByteFromTwoChars(char p, char p_2)
        {
            byte ret;
            if (p <= '9' && p >= '0')
            {
                ret = (byte)((p - '0') << 4);
            }
            else if (p <= 'f' && p >= 'a')
            {
                ret = (byte)((p - 'a' + 10) << 4);
            }
            else if (p <= 'F' && p >= 'A')
            {
                ret = (byte)((p - 'A' + 10) << 4);
            }
            else throw new ArgumentException("Char is not a hex digit: " + p, "p");

            if (p_2 <= '9' && p_2 >= '0')
            {
                ret |= (byte)((p_2 - '0'));
            }
            else if (p_2 <= 'f' && p_2 >= 'a')
            {
                ret |= (byte)((p_2 - 'a' + 10));
            }
            else if (p_2 <= 'F' && p_2 >= 'A')
            {
                ret |= (byte)((p_2 - 'A' + 10));
            }
            else throw new ArgumentException("Char is not a hex digit: " + p_2, "p_2");

            return ret;
        }

        public static byte[] StringToByteArray16(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length % 2 != 0)
                throw new ArgumentException("Hexadecimal value length must be even.", "value");

            unchecked
            {
                byte[] result = new byte[value.Length / 2];
                for (int i = 0; i < result.Length; i++)
                {
                    // 0(48) - 9(57) -> 0 - 9
                    // A(65) - F(70) -> 10 - 15
                    int b = value[i * 2]; // High 4 bits.
                    int val = ((b - '0') + ((('9' - b) >> 31) & -7)) << 4;
                    b = value[i * 2 + 1]; // Low 4 bits.
                    val += (b - '0') + ((('9' - b) >> 31) & -7);
                    result[i] = checked((byte)val);
                }
                return result;
            }
        }

        public static unsafe byte[] StringToByteArray17(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length % 2 != 0)
                throw new ArgumentException("Hexadecimal value length must be even.", "value");

            unchecked
            {
                byte[] result = new byte[value.Length / 2];
                fixed (char* valuePtr = value)
                {
                    char* valPtr = valuePtr;
                    for (int i = 0; i < result.Length; i++)
                    {
                        // 0(48) - 9(57) -> 0 - 9
                        // A(65) - F(70) -> 10 - 15
                        int b = *valPtr++; // High 4 bits.
                        int val = ((b - '0') + ((('9' - b) >> 31) & -7)) << 4;
                        b = *valPtr++; // Low 4 bits.
                        val += (b - '0') + ((('9' - b) >> 31) & -7);
                        result[i] = checked((byte)val);
                    }
                }
                return result;
            }
        }

        public static byte[] StringToByteArray18(this string hexString)
        {
            byte[] b = new byte[hexString.Length / 2];
            char c;
            for (int i = 0; i < hexString.Length / 2; i++)
            {
                c = hexString[i * 2];
                b[i] = (byte)((c < 0x40 ? c - 0x30 : (c < 0x47 ? c - 0x37 : c - 0x57)) << 4);
                c = hexString[i * 2 + 1];
                b[i] += (byte)(c < 0x40 ? c - 0x30 : (c < 0x47 ? c - 0x37 : c - 0x57));
            }

            return b;
        }

        static public byte[] StringToByteArray19(string str)
        {
            byte[] res = new byte[(str.Length % 2 != 0 ? 0 : str.Length / 2)]; //check and allocate memory
            for (int i = 0, j = 0; j < res.Length; i += 2, j++) //convert loop
                res[j] = (byte)((str[i] % 32 + 9) % 25 * 16 + (str[i + 1] % 32 + 9) % 25);
            return res;
        }

        public static byte[] StringToByteArray20(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            int bl = bytes.Length;
            for (int i = 0; i < bl; ++i)
            {
                bytes[i] = (byte)((hex[2 * i] > 'F' ? hex[2 * i] - 0x57 : hex[2 * i] > '9' ? hex[2 * i] - 0x37 : hex[2 * i] - 0x30) << 4);
                bytes[i] |= (byte)(hex[2 * i + 1] > 'F' ? hex[2 * i + 1] - 0x57 : hex[2 * i + 1] > '9' ? hex[2 * i + 1] - 0x37 : hex[2 * i + 1] - 0x30);
            }
            return bytes;
        }

        public static byte[] StringToByteArray21(String hex)
        {

            // pre-create the array
            int resultLength = hex.Length / 2;
            byte[] result = new byte[resultLength];
            // set validity = 0 (0 = valid, anything else is not valid)
            int validity = 0;
            int c, isLetter, value, validDigitStruct, validDigit, validLetterStruct, validLetter;
            for (int i = 0, hexOffset = 0; i < resultLength; i++, hexOffset += 2)
            {
                c = hex[hexOffset];

                // check using calculation over bits to see if first char is a letter
                // isLetter is zero if it is a digit, 1 if it is a letter (upper & lowercase)
                isLetter = (c >> 6) & 1;

                // calculate the tuple value using a multiplication to make up the difference between
                // a digit character and an alphanumerical character
                // minus 1 for the fact that the letters are not zero based
                value = ((c & 0xF) + isLetter * (-1 + 10)) << 4;

                // check validity of all the other bits
                validity |= c >> 7; // changed to >>, maybe not OK, use UInt?

                validDigitStruct = (c & 0x30) ^ 0x30;
                validDigit = ((c & 0x8) >> 3) * (c & 0x6);
                validity |= (isLetter ^ 1) * (validDigitStruct | validDigit);

                validLetterStruct = c & 0x18;
                validLetter = (((c - 1) & 0x4) >> 2) * ((c - 1) & 0x2);
                validity |= isLetter * (validLetterStruct | validLetter);

                // do the same with the lower (less significant) tuple
                c = hex[hexOffset + 1];
                isLetter = (c >> 6) & 1;
                value ^= (c & 0xF) + isLetter * (-1 + 10);
                result[i] = (byte)value;

                // check validity of all the other bits
                validity |= c >> 7; // changed to >>, maybe not OK, use UInt?

                validDigitStruct = (c & 0x30) ^ 0x30;
                validDigit = ((c & 0x8) >> 3) * (c & 0x6);
                validity |= (isLetter ^ 1) * (validDigitStruct | validDigit);

                validLetterStruct = c & 0x18;
                validLetter = (((c - 1) & 0x4) >> 2) * ((c - 1) & 0x2);
                validity |= isLetter * (validLetterStruct | validLetter);
            }

            if (validity != 0)
            {
                throw new ArgumentException("Hexadecimal encoding incorrect for input " + hex);
            }

            return result;
        }

        private static readonly byte[] LookupTableLow = new byte[] {
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
};

        private static readonly byte[] LookupTableHigh = new byte[] {
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0x00, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
};

        private static byte LookupLow(char c)
        {
            var b = LookupTableLow[c];
            if (b == 255)
                throw new IOException("Expected a hex character, got " + c);
            return b;
        }

        private static byte LookupHigh(char c)
        {
            var b = LookupTableHigh[c];
            if (b == 255)
                throw new IOException("Expected a hex character, got " + c);
            return b;
        }

        public static byte StringToByteArray21(char[] chars, int offset)
        {
            return (byte)(LookupHigh(chars[offset++]) | LookupLow(chars[offset]));
        }

        public static byte[] StringToByteArray22(string hexString)
        {
            int hexStringLength = hexString.Length;
            byte[] b = new byte[hexStringLength / 2];
            for (int i = 0; i < hexStringLength; i += 2)
            {
                int topChar = hexString[i];
                topChar = (topChar > 0x40 ? (topChar & ~0x20) - 0x37 : topChar - 0x30) << 4;
                int bottomChar = hexString[i + 1];
                bottomChar = bottomChar > 0x40 ? (bottomChar & ~0x20) - 0x37 : bottomChar - 0x30;
                b[i / 2] = (byte)(topChar + bottomChar);
            }
            return b;
        }

        public static byte[] StringToByteArray23(string src)
        {
            if (String.IsNullOrEmpty(src))
                return null;

            int index = src.Length;
            int sz = index / 2;
            if (sz <= 0)
                return null;

            byte[] rc = new byte[sz];

            while (--sz >= 0)
            {
                char lo = src[--index];
                char hi = src[--index];

                rc[sz] = (byte)(
                    (
                        (hi >= '0' && hi <= '9') ? hi - '0' :
                        (hi >= 'a' && hi <= 'f') ? hi - 'a' + 10 :
                        (hi >= 'A' && hi <= 'F') ? hi - 'A' + 10 :
                        0
                    )
                    << 4 |
                    (
                        (lo >= '0' && lo <= '9') ? lo - '0' :
                        (lo >= 'a' && lo <= 'f') ? lo - 'a' + 10 :
                        (lo >= 'A' && lo <= 'F') ? lo - 'A' + 10 :
                        0
                    )
                );
            }

            return rc;
        }

        public static byte[] StringToByteArray24(String HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        public static byte[] StringToByteArray26(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        public static byte[] StringToByteArray27(string s)
        {
            byte[] a = new byte[s.Length / 2];
            for (int i = 0, h = 0; h < s.Length; i++, h += 2)
            {
                a[i] = (byte)Int32.Parse(s.Substring(h, 2), System.Globalization.NumberStyles.HexNumber);
            }

            return a;
        }
    }
}
