[![Build Status](https://dev.azure.com/jasonsigler0724/HackerNews/_apis/build/status/Jasigler.Hacker-News-Challenge-Client?branchName=master)](https://dev.azure.com/jasonsigler0724/HackerNews/_build/latest?definitionId=1&branchName=master)
<p align="center">

  <h3 align="center">Hacker News Challenge API</h3>

  <p align="center">
     API for the Nextech Hacker News Challenge
    <br />
    <br />
  </p>
</p>


## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Installation](#installation)
* [Usage](#usage)
* [Contact](#contact)



## About The Project

This is a .NET Core API for the Nextech Hacker News challenge. It simply retreives the array of new story ids as well as individual stories from the Hacker News API. 

Due to the Hacker News API design, it poses an interesting challenge for consumers. Rather than returning a nested JSON object of many stories, a GET request must be made for each individual story and comment. Because of this, the major concern is both caching and avoiding socket exhaustion from requests and retry retries. 

To avoid these problems, the API uses a named HTTP client (an implementation of the IHttpClientFactory) to avoid any lifecycle issues that often cause sockets to be orphaned to go into a waittime state. The retry count is also randomized to allow call staggering, altough this might not be much of an issue with only a few consumers. In a production environment, I would probably add something like Polly to more gracefully handle high request loads. 

This API also implements a very basic request cache using the built-in memory cache. Because the point of this API is to serve 'latest' stories, cached stories are evicted after they have not been requested for more than 60 seconds. It will be up to the client how to consume these stories, but caching will help reduce network calls. To scale this application, it may be beneficial to move to a distrubuted cache like Redis. Depending on the tolerance for 'newness', a function could cache the first n number of stories (it is highly unlikely that a consumer will require all 500 stories at any given time), and automatically evict and repopulate the cache. This will have its own coordination challenges, but could be significantly more performant.

This was incredibly fun to make, and I learned quite a lot along the way (especially concerning .Net Core HttpClient).


### Built With

* [.NET Core 3.1](https://angular.io/)


## Getting Started

1. Download the .NET Core 3.1 SDK[here.](https://nodejs.org/en);

2. Veryify that you have the DotNet CLI by running:
```sh
dotnet 
```


### Installation

1. Clone the repo
```sh
git@github.com:Jasigler/Hacker-News-Challenge-API.git
```
3. Navigate to the solution folder
```sh
cd NewsApi/nextech-news-api
```
4. Build the solution
```sh
dotnet build
```

### Running Tests

A seperate Xunit project exists within the solution.

To run tests: 
```sh
dotnet test
```

### Usage

A Postman collection is included in the 'Postman' folder of this repo.

Ports:
  1. Debug: 5104(HTTPS), 51073(HTTP)
  2. IIS Express: 1770(HTTPS), 1790(HTTP)

Endpoints:
  1. GET: route/api/story/{id}
  2. GET: route/api/story/new




##### Get Story Ids for the latest stories
![GET Latest](https://github.com/Jasigler/Hacker-News-Challenge-API/blob/master/Images/Get_New.PNG)



##### Get Story By Id
![GETStory](https://github.com/Jasigler/Hacker-News-Challenge-API/blob/master/Images/Get_Story.PNG)




## Contact

Jason Sigler - jason.sigler@outlook.com
