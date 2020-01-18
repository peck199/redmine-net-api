namespace Redmine.Net.Api.Internals
{
    internal interface ISyncRestClient
    {
        RestResponse Create<T>(RestRequest<T> request) where T: class;
        RestResponse Update<T>(RestRequest<T> request) where T: class;
        RestResponse Delete(RestRequest<int> request);
        RestResponse Get(RestRequest request);
    }
}