namespace Genkan
{
    public interface IResponse
    {
        /// <summary>
        /// Sets the result to return to the requestee.
        /// </summary>
        /// <param name="result">The result to return.</param>
        void SetResult(object result);
    }
}
