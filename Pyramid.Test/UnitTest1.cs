using NUnit.Framework;
using Pyramid.Core.Extensions;
using Pyramid.Business.Classes;
using Pyramid.Data.HelperClasses;

namespace Pyramid.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void OddNumber()
        {
            int number = 123;
            Assert.True(!number.IsEvenNumber()) ;
        }

        [Test]
        public void EvenNUmber()
        {
            int number = 124;
            Assert.True(number.IsEvenNumber());
        }

        [Test]
        public void MaximumSum()
        {
            var pyramid = new PyramidGraph(new ValueGenerator(), 2,true);
            pyramid.Build();
            pyramid.CreatePath();
            Assert.AreEqual(8186,pyramid.Max);
        }
    }
}