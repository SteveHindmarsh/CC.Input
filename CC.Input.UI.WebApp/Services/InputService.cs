using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CC.Input.UI.WebApp.Services;

public class InputService : IInputService
{
    private readonly HttpClient _httpClient;

    public InputService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException();
        //_settings = settings.Value;//TODO
        InitialiseHttpClient();
    }

    private void InitialiseHttpClient()
    {
        //TODO
        //httpClient.DefaultRequestHeaders.Add("Authorization", _settings.Token);
        //httpClient.DefaultRequestHeaders.Add("User-Agent", _settings.UserAgent);
        _httpClient.BaseAddress = new Uri("http://localhost:5079");//TODO: URL setting
    }

    public async Task<IEnumerable<Logic.Model.Input>?> RetrieveAsync()
    {
        IEnumerable<Logic.Model.Input>? inputs = await _httpClient.GetFromJsonAsync<IEnumerable<Logic.Model.Input>?>($"api/input");

        return inputs;
    }

    public async Task<Logic.ValidationResult?> UploadAsync(Stream stream, bool commitIfValid)
    {
        using var multipartFormContent = new MultipartFormDataContent();
        StreamContent fileStreamContent = new StreamContent(stream);
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
        multipartFormContent.Add(fileStreamContent, name: "file", fileName: "Input.txt");
        multipartFormContent.Add(new StringContent(commitIfValid.ToString()), name: "commit");
        HttpResponseMessage response = await _httpClient.PostAsync($"api/input/upload", multipartFormContent);
        response.EnsureSuccessStatusCode();
        stream.Close();
        return JsonConvert.DeserializeObject<Logic.ValidationResult?>(await response.Content.ReadAsStringAsync());
    }
}
