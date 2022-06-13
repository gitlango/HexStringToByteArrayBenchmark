using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexStringToByteArrayBenchmark
{
    [RankColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class HexToByteBenchmark
    {
        string hexString = "200900000622000a210014";

        static byte aByte = 255;
        
        [Benchmark]
        public void BitwiseAndOne()
        {
            Span<int> bitsOfTheByteOfControlPacket = stackalloc int[8];
            for (int i = 0; i < 8; i++)
            {
                bitsOfTheByteOfControlPacket[i] = (aByte >> i) & 1;
            }
        }

        [Benchmark]
        public void Mod()
        {
            Span<int> bitsOfTheByteOfControlPacket = stackalloc int[8];
            for (int i = 0; i < 8; i++)
            {
                bitsOfTheByteOfControlPacket[i] = (aByte >> i) % 2;
            }
        }

        public void ByteToBinaryOTHERS()
        {
            Span<int> bitsOfTheByte = stackalloc int[8];
            for (int i = 0; i < 8; i++)
            {
                bitsOfTheByte[i] = (aByte >> i) & 1;
            }

            int unsignedIntegerInBits7to4 = 0;

            int timesToDoLeftShft = 0;
            for (int i = 4; i <= 7; i++)
            {
                int thisBitAsInteger = (1 << timesToDoLeftShft++) * bitsOfTheByte[i];
                unsignedIntegerInBits7to4 += thisBitAsInteger;
            }


        }
        //[Benchmark] public void BuiltinConvertFromHex() { var result = Convert.FromHexString(hexString); }
        //[Benchmark] public void CodeProjectUnsafe() { var result = CodeProject.FromHexString(hexString); }
        //[Benchmark]public void StringToByteArray1() { var result =  StackOverflow.StringToByteArray1(hexString); }
        //[Benchmark]public void StringToByteArray2() { var result = StackOverflow.StringToByteArray2(hexString); }
        //[Benchmark]public void StringToByteArray3  () { var result = StackOverflow.StringToByteArray3 (hexString); }
        //[Benchmark]public void StringToByteArray4  () { var result = StackOverflow.StringToByteArray4 (hexString); }
        //[Benchmark]public void StringToByteArray5  () { var result = StackOverflow.StringToByteArray5 (hexString); }
        //[Benchmark]public void StringToByteArray6  () { var result = StackOverflow.StringToByteArray6 (hexString); }
        //[Benchmark]public void StringToByteArray7  () { var result = StackOverflow.StringToByteArray7 (hexString); }
        //[Benchmark]public void StringToByteArray8  () { var result = StackOverflow.StringToByteArray8 (hexString); }
        //[Benchmark]public void StringToByteArray9  () { var result = StackOverflow.StringToByteArray9 (hexString); }
        //[Benchmark]public void StringToByteArray10 () { var result = StackOverflow.StringToByteArray10(hexString); }
        //[Benchmark]public void StringToByteArray11 () { var result = StackOverflow.StringToByteArray11(hexString); }
        //[Benchmark]public void StringToByteArray12 () { var result = StackOverflow.StringToByteArray12(hexString); }
        //[Benchmark]public void StringToByteArray14 () { var result = StackOverflow.StringToByteArray14(hexString); }
        //[Benchmark]public void StringToByteArray15 () { var result = StackOverflow.StringToByteArray15(hexString); }
        //[Benchmark]public void StringToByteArray16 () { var result = StackOverflow.StringToByteArray16(hexString); }
        //[Benchmark]public void StringToByteArray17 () { var result = StackOverflow.StringToByteArray17(hexString); }
        //[Benchmark]public void StringToByteArray18 () { var result = StackOverflow.StringToByteArray18(hexString); }
        //[Benchmark]public void StringToByteArray19 () { var result = StackOverflow.StringToByteArray19(hexString); }
        //[Benchmark]public void StringToByteArray20 () { var result = StackOverflow.StringToByteArray20(hexString); }
        //[Benchmark]public void StringToByteArray21 () { var result = StackOverflow.StringToByteArray21(hexString); }
        //[Benchmark]public void StringToByteArray22 () { var result = StackOverflow.StringToByteArray22(hexString); }
        //[Benchmark]public void StringToByteArray23 () { var result = StackOverflow.StringToByteArray23(hexString); }
        //[Benchmark]public void StringToByteArray24 () { var result = StackOverflow.StringToByteArray24(hexString); }
        //[Benchmark]public void StringToByteArray26 () { var result = StackOverflow.StringToByteArray26(hexString); }
        //[Benchmark] public void StringToByteArray27() { var result = StackOverflow.StringToByteArray27(hexString); }

    }
}
