namespace invocative.mathm
{
    using System;

    /// <summary>
    /// Extension methods for the Decimal data type.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Swaps the value between two variables.
        /// </summary>
        public static void Swap<T>(this ref T lhs, ref T rhs) where T : struct
        {
            var temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        /// <summary>
        /// Prime number to use to begin a hash of an object.
        /// </summary>
        /// <remarks>
        /// See: http://stackoverflow.com/questions/263400
        /// </remarks>
        public const int HashStart = 17;
        private const int HashPrime = 397;

        /// <summary>
        /// Adds a hash of an object to a running hash value.
        /// </summary>
        /// <param name="hash">A running hash value.</param>
        /// <param name="obj">The object to hash and incorporate into the running hash.</param>
        public static int HashObject(this int hash, object obj)
        {
            unchecked { return hash * HashPrime ^ (obj?.GetHashCode() ?? 0); }
        }

        /// <summary>
        /// Adds a hash of a struct to a running hash value.
        /// </summary>
        /// <param name="hash">A running hash value.</param>
        /// <param name="value">The struct to hash and incorporate into the running hash.</param>
        public static int HashValue<T>(this int hash, T value) where T : struct
        {
            unchecked { return hash * HashPrime ^ value.GetHashCode(); }
        }
        /// <summary>
        /// Tests whether or not a given value is within the upper and lower limit, inclusive.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        public static bool InRangeIncl(this decimal value, decimal lowerLimit, decimal upperLimit)
        {
            if (upperLimit < lowerLimit)
                throw new Exception("Upper limit is less than lower limit!");

            return (value >= lowerLimit) && (value <= upperLimit);
        }
        /// <summary>
        /// Tests whether or not a given value is within the upper and lower limit, exclusive.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="lowerLimit">The lower limit.</param>
        /// <param name="upperLimit">The upper limit.</param>
        public static bool InRangeExcl(this decimal value, decimal lowerLimit, decimal upperLimit)
        {
            if (upperLimit < lowerLimit)
                throw new Exception("Upper limit is less than lower limit!");

            return (value > lowerLimit) && (value < upperLimit);
        }

        /// <summary>
        /// Rounds a number away from zero to the given number of decimal places.
        /// </summary>
        public static decimal RoundFromZero(this decimal d, int decimals)
        {
            if (decimals < 0) 
                throw new ArgumentOutOfRangeException("decimals", "Decimals must be greater than or equal to 0.");

            var scaleFactor = Mathm.PowersOf10[decimals];
            var roundingFactor = d > 0 ? 0.5m : -0.5m;

            return decimal.Truncate(d * scaleFactor + roundingFactor) / scaleFactor;
        }
    }
}