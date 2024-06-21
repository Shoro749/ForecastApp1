using System.Text;

namespace ForecastApp1;

class ForecastQueryBuilder
{
    private const string _baseApi = "https://api.openweathermap.org/data/2.5/forecast";
    private const string _key = "64075c0474a37b0c3882e2da330b425f";
    private Dictionary<string, string> _queryParameters = new Dictionary<string, string>();

    private StringBuilder _apiBuilder = new StringBuilder(_baseApi);

    public ForecastQueryBuilder AddCity(string city)
    {
       _queryParameters.Add("q", city);
        return this;
    }

    public ForecastQueryBuilder AddUnits(string units)
    {
        _queryParameters.Add("units", units);
        return this;
    }

    public string Build()
    {
        var queryBuilder = new StringBuilder(_baseApi);
        queryBuilder.Append($"?appid={_key}");
        foreach ( var item in _queryParameters )
        {
            queryBuilder.Append($"&{item.Key}={item.Value}");
        }

        return queryBuilder.ToString();
    }
}
