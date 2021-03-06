﻿using System;
using txstudio.ApiSignature;
using Xunit;

namespace ApiSignatureTest
{
    public sealed class SignatureManagerTest
    {
        private readonly SignatureManager _signatureManager;

        private string _apikey;
        private string _content;
        private string _signature;
        private long _timestamp;

        public SignatureManagerTest()
        {
            _signatureManager = new SignatureManager(new TestTimestampGenerator()
                                                    , new DefaultTimeStampValidator()
                                                    , new DefaultHashAlgorithm()
                                                    , new DefaultSignatureCombiner()
                                                    , new TestApiKeyManager());

            _apikey = "txstudio";
            _content = @"{""Email"":""test-user@txstudio.com""}";
            _timestamp = 0;
            _signature = "0111a40c356c3133a1cfa98acc1c44c129ce1076890a80a486a6fe5a6b9ae8ad";
        }
        
        [Fact]
        public void GetSignagure()
        {
            string _expected;
            string _actual;

            _expected = _signature;
            _actual = _signatureManager.GetSignature(_apikey, _content, _timestamp);

            Assert.Equal(_expected, _actual);
        }

        [Fact]
        public void ValidSignatureWithNormalCase()
        {
            bool _condition;

            _condition = _signatureManager.ValidSignature(_apikey, _content, _signature, _timestamp);

            Assert.True(_condition);
        }

        [Fact]
        public void ValidSignatureWithInvalidTimeStamp()
        {
            bool _condition;
            long _invalidTimeStamp;

            _invalidTimeStamp = 301;

            Assert.Throws<TimeStampInvalidException>(() => {
                _condition = _signatureManager.ValidSignature(_apikey, _content, _signature, _invalidTimeStamp);
            });
        }


        [Fact]
        public void ValidSignatureWithInvalidSignature()
        {
            bool _condition;
            string _invalidSignature;

            _invalidSignature = "abcdefg";

            Assert.Throws<SignatureInvalidException>(() => {
                _condition = _signatureManager.ValidSignature(_apikey, _content, _invalidSignature, _timestamp);
            });
        }

        [Fact]  
        public void GetTimeStampWithTestCase()
        {
            long _expected;
            long _actual;

            _expected = 0;
            _actual = this._signatureManager.GetTimeStamp();

            Assert.Equal(_expected, _actual);
        }
    }

    #region 測試使用的實作物件

    public class TestApiKeyManager : IApiKeyManager
    {
        private StringComparison _comparison = StringComparison.OrdinalIgnoreCase;

        public string GetSecret(string apikey)
        {
            if (string.Equals(apikey, "txstudio", _comparison) == true)
                return "txstudio-secret";

            return string.Empty;
        }
    }

    public class TestTimestampGenerator : ITimeStampGenerator
    {
        public long GetTimeStamp(DateTime? datetime = null)
        {
            return 0;
        }
    }

    #endregion
}
