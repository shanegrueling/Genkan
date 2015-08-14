namespace Genkan
{
    public interface IRequest
    {
        string GetControllerName();
        string GetMethodName();

        T GetParameter<T>(int i);
        T GetParameter<T>(string name);
    }
}
