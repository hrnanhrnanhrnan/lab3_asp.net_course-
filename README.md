
# Lab3_asp.NET API

API to get persons, links and interests, as well as post new links and connect persons to new interests


## API Reference

### PERSONS
#### Get all persons

```http
  GET /api/persons
```
Fetches all persons
#### Get person by name
```http
  GET /api/persons/${name}
```
| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | *Required* name of person |

Fecthes first person with a name that contains name parameter

### Interests
#### Get all interests

```http
  GET /api/interests
```
Fetches all interests
#### Get all interests for person by name of person
```http
  GET /api/interests/${name}
```

| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | *Required* name of person |

Fetches all interests for the first person that has a name that contains the name parameter

#### Connect person to a new interest
```http
POST /api/interests
```
| body parameters | Type | Description                       |
| :-------- | :------- | :-------------------------------- |    
| `personId`      | `int` | *Required* id of person |
|`interestId`      | `int` | *Required* id of interest |

#### --Example--
To create new interest with id 2 for person with id 1
#### url
"api/interests"

#### body
{"personId": 1, "interestId": 2}


### Links
#### Get all links

```http
  GET /api/links
```
Fetches all links

#### Get all links for person by name of person
```http
  GET /api/links/${name}
```

| Url Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `name`      | `string` | *Required* name of person to filter on |

Fetches all links 
#### Add new link for specific person and specific interest
```http
POST /api/links/${nameOfPerson}/${nameOfInterest}
```
| Url Parameter | Type     | Description                      |
| :-------- | :------- | :------------------------------- |
 `nameOfPerson`      | `string` | *Required* name of person to filter on |
  `nameOfInterest`      | `string` | *Required* name of interest to filter on |

| body parameters | Type | Description                      |     
| :-------- | :------- | :--------------------------------- |
| `url`      | `string` | *Required* the url to save to the interest |

#### --Example--
To create new link for person with name "Robin" and interest "Programming"
#### url
"api/links/robin/programming"

#### body
{"url": "https://docs.chain.link/docs/link-token-contracts/"}
