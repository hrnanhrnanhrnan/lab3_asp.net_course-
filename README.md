
# Lab3_asp.NET API

API to get persons, links and interests, as well as post new links and connect persons to new interests


## API Reference

### PERSONS
#### Get all persons
Fetches all persons
```http
  GET /api/persons
```

#### Get person by name
Fecthes first person with a name that contains name parameter
```http
  GET /api/persons/${name}
```
| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | *Required* name of person |



#### Get person by id
Fetches person by id
```http
  GET /api/persons/byid:${id}
```
| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | *Required* id of person |


### Interests
#### Get all interests
Fetches all interests
```http
  GET /api/interests
```

#### Get all interests for person by name of person
Fetches all interests for the first person that has a name that contains the name parameter
```http
  GET /api/interests/${name}
```

| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | *Required* name of person |


#### Get interest by id
Fetches interest by id
```http
  GET /api/interests/interestbyid:${id}
```
| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | *Required* id of interest |


#### Get PersonInterest by id
Fetches PersonInterest by id
```http
  GET /api/interests/personinterestbyid:${id}
```
| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `int` | *Required* id of PersonInterest |



#### Connect person to a new interest
Add new row in the PersonInterest table which connects a person by id to a interest by id
```http
POST /api/interests
```
| Body parameters | Type | Description                       |
| :-------- | :------- | :-------------------------------- |    
| `personId`      | `int` | *Required* id of person |
|`interestId`      | `int` | *Required* id of interest |

#### --Example--
To create new interest with id 2 for person with id 1
#### url
"/api/interests"

#### body
{"personId": 1, "interestId": 2}


### Links
#### Get all links
Fetches all links
```http
  GET /api/links
```


#### Get all links for person by name of person
Fetches all links for the first person that has a name that contains the name parameter
```http
  GET /api/links/${name}
```

| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | *Required* name of person to filter on |

 
#### Add new link for specific person and specific interest
Adds new link to a specified interest and to a specified person.
The specified person has to have the specified interest as a interest. 
Will otherwise result in a status 400 Bad Request response
```http
POST /api/links/${nameOfPerson}/${nameOfInterest}
```
| Url Parameter | Type     | Description                      |
| :-------- | :------- | :------------------------------- |
 `nameOfPerson`      | `string` | *Required* name of person to filter on |
  `nameOfInterest`      | `string` | *Required* name of interest to filter on |

| Body parameters | Type | Description                      |     
| :-------- | :------- | :--------------------------------- |
| `url`      | `string` | *Required* the url to save to the interest |

#### --Example--
To create new link for person with name "Robin" and interest "Programming"
#### url
"/api/links/robin/programming"

#### body
{"url": "https://docs.chain.link/docs/link-token-contracts/"}
