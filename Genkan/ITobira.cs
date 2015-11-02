namespace Genkan
{
    public interface ITobira
    {
        /// <summary>
        /// Processes a <paramref name="request"/> and calls <paramref name="response"/>.SetResult.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <param name="response">The response object to fill with the answer.</param>
        void Call(IRequest request, IResponse response);
    }
}
