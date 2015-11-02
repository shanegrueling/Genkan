using System;

namespace Genkan
{
    public interface IRequest
    {
        /// <summary>
        /// Returns the name of the controller the request wants to call.
        /// </summary>
        /// <returns></returns>
        string GetControllerName();
        /// <summary>
        /// Returns the name of the method the request wants to call.
        /// </summary>
        /// <returns></returns>
        string GetMethodName();
        /// <summary>
        /// Returns the parameter at position <paramref name="i"/> as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type you want to return.</typeparam>
        /// <param name="i">The position at which the parameter is.</param>
        /// <returns>The value of the parameter.</returns>
        T GetParameter<T>(int i);
        /// <summary>
        /// Returns the parameter named <paramref name="name"/> as <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type you want to return.</typeparam>
        /// <param name="name">The name of the parameter you want.</param>
        /// <returns>The value of the parameter.</returns>
        T GetParameter<T>(string name);
        /// <summary>
        /// Returns all parameter at once as a object[] but casts them according to <paramref name="parameterInfo"/>.
        /// </summary>
        /// <param name="parameterInfo">All types in the correct order for the parameter.</param>
        /// <returns>All parameters send with the request.</returns>
        object[] GetParameters(Type[] parameterInfo);
    }
}
