namespace Alastack.Authentication
{
    /// <summary>
    /// The default implementation of <see cref="ITimestampCalculator"/>.
    /// </summary>
    public class TimestampCalculator : ITimestampCalculator
    {
        /// <inheritdoc />
        public long Calculate(long timeOffset)
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + timeOffset;
        }
    }
}