namespace Genkan
{
    public interface IApiDiscoverer
    {
        /// <summary>
        /// Returns the <see cref="RPCInfo"/> for <paramref name="controllerName"/>.<paramref name="methodName"/>.
        /// </summary>
        /// <param name="controllerName">The controller to call.</param>
        /// <param name="methodName">The method to call on the <paramref name="controllerName"/></param>
        /// <returns>The info about the function.</returns>
        RPCInfo Resolve(string controllerName, string methodName);
    }
}
