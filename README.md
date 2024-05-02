<h1 align="center">MultiLib(Multi Purpose Library)</h1>

# .NET WEB API

.NET API solution with multiple Projects.
- This Is My Personal Project To Explore and Learn .NET Web API.
- I Have Used Multiple projects in single solution Method.
 
## Tech Stack

<p align="left"> 
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="50" height="50"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40"/> 
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/mysql/mysql-original-wordmark.svg" alt="mysql" width="40" height="40"/>
  <img src="https://www.vectorlogo.zone/logos/getpostman/getpostman-icon.svg" alt="postman" width="40" height="40"/> 
</p>

## Authors

- [@Dhruv-Variya](https://www.github.com/Dhruv-Variya)

## Run Locally

Clone the project

```bash
  git clone https://github.com/Dhruv-Variya/MovieMainService.git
```
Setup In Visual Studio, Build And Run 🚀

## Structure of this Project
```code
MultiLib Solution
│
├───MultiLib.Auth
│   ├───Controllers             // Controllers for additional APIs
│   ├───Data                    // Data access layer (Repositories)
│   ├───Dtos                    // Data Transfer Objects
│   ├───Helper                  // Helper Classes like Password_Hasher.
│   ├───Migrations              // Ef Core Migrations
│   ├───Models                  // Models.
│   ├───AutoMapper.cs           // AutoMapper Configuration Class.
│   └───Services                // Service classes for additional APIs
│
├───MultiLib.ContentAPI
│   ├───Controllers             // Controllers for additional APIs
│   ├───Data                    // Data access layer (Repositories)
│   ├───Dtos                    // Data Transfer Objects
│   ├───Migrations              // Ef Core Migrations
│   ├───Models                  // Models.
│   ├───AutoMapper.cs           // AutoMapper Configuration Class.
│   └───Services                // Service classes for additional APIs
│
├───MultiLib.Core    // Class Library
│   └───Different Classes       // To Add Reusable or Common Classes in this library for all the projects.
│
└───MultiLib.EcommerceAPI (Future)
    ├───Controllers             // Controllers for additional APIs
    ├───Data                    // Data access layer (Repositories)
    ├───Dtos                    // Data Transfer Objects
    ├───Migrations              // Ef Core Migrations
    ├───Models                  // Models.
    └───Services                // Service classes for additional APIs
```
## API Reference

#### Get all series 
- Not Reuired any Parameters.
```http
  GET /Series/GetAll
```


#### Get series  By Id

```http
  GET /Series/GetseriesById/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `integer` | **Required**. Id of item to fetch |

#### Add series 
```http
  Post /Series/AddSeries
```

- Object to Pass In Parameters
```Code
{
  "itemCode": "string",
  "itemTitle": "string",
  "itemType": "string",
  "itemCast": "string",
  "itemRating": "string",
  "itemReleaseDate": "string",
  "itemTrailerURL": "string",
  "itemDescription": "string",
  "itemPoster": "string",
  "itemBackPoster": "string",
  "itemSeasons": 0,
  "isUpcoming": true,
  "seriesCategoryIds": [
    0
  ],
  "seriesLanguagesIds": [
    0
  ]
}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `itemCode`      | `string` | **Required**. Code That can be Uniue |
| `itemTitle`      | `string` | **Required**. Title |
| `itemType`      | `string` | **Required**. Type Of Content(Web series, TV series , Anime Etc.) |
| `itemCast`      | `string` | **Required**. Cast |
| `itemRating`      | `string` | **Required**. Ratings |
| `itemReleaseDate`      | `string` | **Required**. Release Date |
| `itemTrailerURL`      | `string` | **Required**. Trailer URL(Probably Youtube URL) |
| `itemDescription`      | `string` | **Required**. Description |
| `itemPoster`      | `string` | **Required**. Image Url |
| `itemBackPoster`      | `string` | **Required**. Back poster Url |
| `itemSeasons`      | `integer` | **Required** |
| `isUpcoming`      | `Bool` | **Required** |
| `seriesCategoryIds`      | `Array Of Integers` | **Required** Id of Catagories from Catagories table|
| `seriesLanguagesIds`      | `Array Of Integers` | **Required**. Id of Languages from languages table |


#### Update series 
```http
  Put /Series/UpdateSeries
```
- Object to pass in parameters is same as **Add series**
  
#### Delete series 

```http
  Delete /Series/DleteSeries/{id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `integer` | **Required**. Id of item to Delete |


#### Add Seasons and Episodes to respective series 
```http
  Post /Series/AddSeriesAndEpisodes
```
- Object to Pass In Parameters
```Code
[
  {
    "itemCode": "string",
    "seasonNumber": 0,
    "seasonName": "string",
    "episodes": [
      {
        "episodeNumber": 0,
        "episodeName": "string",
        "episodeDescription": "string",
        "episodeRating": "string",
        "episodeReleaseDate": "string",
        "poster": "string"
      }
    ]
  }
]
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `itemCode`      | `string` | **Required**. Uniue Item Code Of series |
| `seasonNumber`      | `Integer` | **Required**. Season Number |
| `seasonName`      | `string` | **Required**. Season Name |
| `episodes`      | `List Of 'episodes' Object` | **Required**. Uniue Item Code Of series |
| `episodeNumber`      | `Integer` | **Required**. Episode Number |
| `episodeName`      | `string` | **Required**. Episode Name |
| `episodeDescription`      | `string` | **Required**. Description |
| `episodeRating`      | `string` | **Required**. Rating |
| `episodeReleaseDate`      | `string` | **Required**. Release Date |
| `poster`      | `string` | **Required**. Poster Image Url |



#### Update Season

```http
  Put /Series/UpdateSeason
```
- Object to Pass In Parameters
```Code
{
  "itemCode": "string",
  "seasonNumber": 0,
  "seasonName": "string"
}
```

#### Update Episode
```http
  Put /Series/UpdateEpisode
```
- Object to Pass In Parameters
```Code
  {
  "itemCode": "string",
  "seasonNumber": 0,
  "episodeNumber": 0,
  "episodeName": "string",
  "episodeDescription": "string",
  "episodeRating": "string",
  "episodeReleaseDate": "string",
  "poster": "string"
}
```


- Movie API Example : 
```
{
  "data": [
    {
      "movieId": 1,
      "movieCode": "ukjwf",
      "movieTitle": "AVATAR",
      "movieCast": "Sam Worthington",
      "movieRating": "7.9",
      "movieReleaseDate": "18/12/2003",
      "movieTrailerURL": "https://www.youtube.com/watch?v=5PSNL1qE6VY",
      "movieDescription": "AVATAR",
      "moviePoster": "https://i.etsystatic.com/18242346/r/il/864ece/3727281843/il_570xN.3727281843_n4vs.jpg",
      "movieBackPoster": "https://economictimes.indiatimes.com/thumb/msid-94687295,width-1200,height-900,resizemode-4,imgsize-95348/avatar-4-producer-jon-landau-says-first-act-of-movie-is-complete.jpg?from=mdr",
      "isUpcoming": false,
      "categories": [
        "Action",
        "Adventure",
        "Comedy"
      ],
      "languages": [
        "English",
        "Spanish",
        "French",
        "Hindi"
      ]
    }
  ],
  "success": true,
  "message": "Success"
}
```
- Seriese API Example : 
```
{
  "data": [
    {
      "itemId": 1,
      "itemCode": "technology",
      "itemTitle": "technology",
      "itemType": "Web Series",
      "itemCast": "john doe, alice, bob",
      "itemRating": "9.0",
      "itemReleaseDate": "12-2-2024",
      "itemTrailerURL": "www.youtube.com",
      "itemDescription": "technology",
      "itemPoster": "poster.jpg",
      "itemBackPoster": "backposter.jpg",
      "itemSeasons": 2,
      "isUpcoming": false,
      "categories": null,
      "languages": null,
      "numberOfSeasons": 2,
      "seasonData": [
        {
          "seasonNumber": 1,
          "seasonName": "Season 1: The Beginning",
          "totalEpisodes": 1,
          "episodes": [
            {
              "episodeNumber": 1,
              "episodeName": "Introduction to Coding",
              "episodeDescription": "An introduction to the basics of coding and programming languages.",
              "episodeRating": "8.2",
              "episodeReleaseDate": "2022-01-10",
              "poster": "https://example.com/poster1.jpg"
            }
          ]
        },
        {
          "seasonNumber": 2,
          "seasonName": "Season 2: Advanced Techniques",
          "totalEpisodes": 2,
          "episodes": [
            {
              "episodeNumber": 1,
              "episodeName": "Advanced JavaScript Concepts",
              "episodeDescription": "Exploring closures, prototypes, and asynchronous programming in JavaScript.",
              "episodeRating": "9.1",
              "episodeReleaseDate": "2022-02-14",
              "poster": "https://example.com/poster6.jpg"
            },
            {
              "episodeNumber": 2,
              "episodeName": "Server-Side Development with Node.js",
              "episodeDescription": "Understanding server-side development using Node.js and Express.",
              "episodeRating": "9.3",
              "episodeReleaseDate": "2022-02-21",
              "poster": "https://example.com/poster7.jpg"
            }
          ]
        }
      ]
    }
  ],
  "success": true,
  "message": "Success"
}
```

## Roadmap

- Add Fully Functional CRUD Endpoints For All Type Of Content.

- Add Another Functionalities Like Analysis, User Roles.

- Implement Complete Authentication And Role Based Authorization.

- Improve Api Performance By Applying Some Optimal Solutions Like caching, Api Gateway, Request Limit and many more.


