using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HexStringToByteArrayBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {


            //new HexToByteBenchmark().Mod();
            //new HexToByteBenchmark().BitwiseAndOne();
            //
            //new HexToByteBenchmark().ByteToBinaryOTHERS();
            //
            //string hexString = "200900000622000a210014";

            ////Test.Execute();

            //int numero = 9;

            //var binary32 = Convert.ToString(numero, 2).PadLeft(8, '0');

            //var aByte = (byte)numero;

            //List<int> bits = new List<int>();

            //for (int i = 0; i < 8; i++)
            //{
            //    var iMod8 = i % 8;
            //    var left = aByte >> iMod8;

            //    var leftAsBinaryString = Convert.ToString(left, 2).PadLeft(8, '0');
            //    var oneAsBinaryString = "00000001";

            //    var leftBitwiseAndOne = left & 1;

            //    Console.WriteLine($"i = {i}");
            //    Console.WriteLine($"iMod8 = {iMod8}");
            //    Console.WriteLine($"var left = aByte >> iMod8; = {left}");
            //    Console.WriteLine($"leftAsBinaryString = {leftAsBinaryString}");
            //    Console.WriteLine($"oneAsBinaryStringg = {oneAsBinaryString}");
            //    Console.WriteLine($"leftBitwiseAndOne = {leftBitwiseAndOne}");
            //    Console.WriteLine("");

            //    bits.Add(leftBitwiseAndOne);
            //}


            //string hexString = "200900000622000a210014";

            //Span<Byte> spanOfBytes = stackalloc byte[hexString.Length >> 1];
            //spanOfBytes = StackOverflow.HexStringToSpanOfBytesLower(hexString, spanOfBytes);

            //Span<Byte> spanOfBytes2 = stackalloc byte[hexString.Length >> 1];
            //spanOfBytes = StackOverflow.HexStringToSpanOfBytesUpper(hexString, spanOfBytes);

            //int remainingLength = spanOfBytes[1];

            BenchmarkRunner.Run<HexToByteBenchmark>();
        }

        
            
        
        

    }
}
        
