using System;
using System.Collections.Generic;

[Serializable]
public class WeatherResponse
{
    public WeatherProperties properties;
}

[Serializable]
public class WeatherProperties
{
    public List<WeatherPeriod> periods;
}

[Serializable]
public class WeatherPeriod
{
    public int temperature;
    public string shortForecast;
}

[Serializable]
public class DogFactsResponse
{
    public List<DogFact> data;
}

[Serializable]
public class DogFactResponse
{
    public DogFact data;
}

[Serializable]
public class DogFact
{
    public string id;
    public string type;
    public DogFactAttributes attributes;
}

[Serializable]
public class DogFactAttributes
{
    public string name;
    public string description;
}

