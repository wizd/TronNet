﻿using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TronNet.Crypto
{
    public static class Sha3KeccackExtension
    {

        public static string CalculateHashFromHex(params string[] hexValues)
        {
            var joinedHex = string.Join("", hexValues.Select(x => x.RemoveHexPrefix()).ToArray());
            return joinedHex.HexToByteArray().ToSha3Hash().ToHex();
        }

        public static byte[] ToSha3Hash(this byte[] value)
        {
            var digest = new KeccakDigest(256);
            digest.BlockUpdate(value, 0, value.Length);
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            return output;
        }
        public static string ToSha3Hash(this string value)
        {
            var input = Encoding.UTF8.GetBytes(value);
            var output = input.ToSha3Hash();

            return output.ToHex();
        }
        public static byte[] ToSM3Hash(this byte[] value)
        {
            var digest = new SM3Digest();
            digest.BlockUpdate(value, 0, value.Length);
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            return output;
        }
        public static byte[] ToSHA256Hash(this byte[] value)
        {
            var digest = new Sha256Digest();
            digest.BlockUpdate(value, 0, value.Length);
            var output = new byte[digest.GetDigestSize()];

            digest.DoFinal(output, 0);
            return output;

        }
    }
}