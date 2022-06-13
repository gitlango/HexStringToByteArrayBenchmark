using System;

// Given the hex stream bellow, convert it to a byte array and use the public MQTT5 protocol specification to
// read the 'Control Packet' and the 'Remaining Length' from the 'Fixed Header'.

// Full MQTT5 Docs:
//    https://docs.oasis-open.org/mqtt/mqtt/v5.0/mqtt-v5.0.html
// Structure of an MQTT Control Packet and Fixed Header section:
//    https://docs.oasis-open.org/mqtt/mqtt/v5.0/os/mqtt-v5.0-os.html#_Toc3901020
//    https://docs.oasis-open.org/mqtt/mqtt/v5.0/os/mqtt-v5.0-os.html#_Toc3901021
// Remaining Length and Variable Byte Integer sections:
//    https://docs.oasis-open.org/mqtt/mqtt/v5.0/os/mqtt-v5.0-os.html#_Toc3901024
//    https://docs.oasis-open.org/mqtt/mqtt/v5.0/os/mqtt-v5.0-os.html#_Toc3901011

// The outputs on the right pane should be:
//
// Control Packet: ConnAck
// Remaining Length: 9

public static class Test
{

    public static void Execute()
    {
        var hexStream = "200900000622000a210014";
        Span<Byte> inputBuffer = stackalloc byte[hexStream.Length >> 1];
        inputBuffer = CreateBuffer(hexStream, ref inputBuffer);
        var controlPacket = ReadControlPacket(inputBuffer);
        var remainingLength = GetRemainingLength(inputBuffer);

        Console.WriteLine($"Control Packet: {controlPacket}");
        Console.WriteLine($"Remaining Length: {remainingLength}");
    }

    public static Span<byte> CreateBuffer(string hexStream, ref Span<Byte> spanOfBytes)
    {
        if (hexStream.Length % 2 != 0)
            throw new ArgumentException("The hexadecimal stream must have an even number of characters");

        for (int i = 0; i < hexStream.Length >> 1; ++i)
        {
            spanOfBytes[i] = (byte)((GetHexValLowerCase(hexStream[i << 1]) << 4) + (GetHexValLowerCase(hexStream[(i << 1) + 1])));
        }

        return spanOfBytes;
    }

    public static ControlPacket ReadControlPacket(ReadOnlySpan<byte> inputBuffer)
    {
        byte byteOfControlPacket = inputBuffer[0];

        Span<int> bitsOfTheByteOfControlPacket = stackalloc int[8];
        for (int i = 0; i < 8; i++)
        {
            bitsOfTheByteOfControlPacket[i] = (byteOfControlPacket >> i) & 1;
        }

        int unsignedIntegerInBits7to4 = 0;

        int timesToDoLeftShift = 0;
        for (int i = 4; i <= 7; i++)
        {
            int thisBitAsInteger = (1 << timesToDoLeftShift++);

            if (bitsOfTheByteOfControlPacket[i] != 0)
                unsignedIntegerInBits7to4 += thisBitAsInteger;
        }

        return (ControlPacket)unsignedIntegerInBits7to4;
    }

    public static int GetRemainingLength(ReadOnlySpan<byte> inputBuffer)
    {
        int byteOfRemainingLength = inputBuffer[1];

        return byteOfRemainingLength;
    }

    public static Span<Byte> HexStringToSpanOfBytes(string hexStream, Span<Byte> spanOfBytes)
    {
        if (hexStream.Length % 2 != 0)
            throw new ArgumentException("The hexadecimal stream must have an even number of characters");

        for (int i = 0; i < hexStream.Length >> 1; ++i)
        {
            spanOfBytes[i] = (byte)((GetHexValLowerCase(hexStream[i << 1]) << 4) + (GetHexValLowerCase(hexStream[(i << 1) + 1])));
        }

        return spanOfBytes;
    }

    public static int GetHexValLowerCase(char hexChar)
    {
        int val = (int)hexChar;
        return val - (val < 58 ? 48 : 87);
    }

    
    // Method to be used when the stream will have upper case letters    
    //public static int GetHexValUpperCase(char hexChar)
    //{
    //    int val = (int)hexChar;
    //    return val - (val < 58 ? 48 : 55);
    //}
}



public enum ControlPacket : byte
{
    Reserved = 0,
    Connect = 1,
    ConnAck = 2,
    Publish = 3,
    PubAck = 4,
    PubRec = 5,
    PubRel = 6,
    PubComp = 7,
    Subscribe = 8,
    SubAck = 9,
    Unsubscribe = 10,
    UnsubAck = 11,
    PingReq = 12,
    PingResp = 13,
    Disconnect = 14,
    Auth = 15
}