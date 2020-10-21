[![Build Status](https://dev.azure.com/jasonsigler0724/HackerNews/_apis/build/status/Jasigler.Hacker-News-Challenge-API?branchName=master)](https://dev.azure.com/jasonsigler0724/HackerNews/_build/latest?definitionId=2&branchName=master)

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

This is a .NET Core API for the Nextech Hacker News challenge. It simply retrieves the array of new story ids as well as individual stories from the Hacker News API. 

<br/>


### Built With

* [.NET Core 3.1](https://angular.io/)
<br/>

## Getting Started

1. Download the .NET Core 3.1 SDK[here.](https://nodejs.org/en);

2. Veryify that you have the DotNet CLI by running:
```sh
dotnet 
```
<br/>

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
<br/>

### Running Tests

A seperate Xunit project exists within the solution.

To run tests: 
```sh
dotnet test
```
<br/>

### Usage

A Postman collection is included in the 'Postman' folder of this repo.

Ports:
  1. Debug: 5104(HTTPS), 51073(HTTP)
  2. IIS Express: 1770(HTTPS), 1790(HTTP)
<br/>
Endpoints:
  1. GET: route/api/story/{id}
  2. GET: route/api/story/new
<br/>

##### Get Story Ids for the latest stories

![Latest_Swagger](https://github.com/Jasigler/Hacker-News-Challenge-API/blob/master/Images/Get_New_Swagger.PNG)


![GET Latest](https://github.com/Jasigler/Hacker-News-Challenge-API/blob/master/Images/Get_New.PNG)


##### Get Story By Id

![Get Story By Swagger](https://github.com/Jasigler/Hacker-News-Challenge-API/blob/master/Images/Get_Story_Swagger.PNG)



![GETStory](https://github.com/Jasigler/Hacker-News-Challenge-API/blob/master/Images/Get_Story.PNG)
<br/>

## Contact

Jason Sigler - jason.sigler@outlook.com
