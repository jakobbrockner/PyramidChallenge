namespace Pyramid.Core.Extensions
{
    public static class IntExtender
    {

        public static bool IsEvenNumber(this int number)
        {
            return (number % 2 == 0);
        }
    }
}
