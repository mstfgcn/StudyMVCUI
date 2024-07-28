namespace WS.MvcUI.ApiServices
{
    public interface IHttpApiService
    {
        //get post put delete

        Task<T> GetData<T>(string requestUri, string token=null);
        Task<T> PostData<T>(string requestUri, string jsonData);

        Task<bool>Delete(string requestUri );


    }
}
