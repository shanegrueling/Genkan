namespace Genkan
{
    public interface IGenkan
    {
        void Call<T>(IRequest request, ref T response) where T : IResponse;
    }
}
