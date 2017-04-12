# Technical Task
## Overview
This application provides API for data that related the structure of company.

## Getting Started
For starting application use Microsoft Visual Studio 2017.

Application host is: https://localhost:44336

LocalDB is used in the application and no any configuration needed for run.

Before using POST/PUT/DELETE request you can manually authenticate with LinkedIn account go to https://localhost:44336/login

Otherwise you will be automatically redirected to the login page when you try to make a call to the methods that required for authorization.

To logout from application go to https://localhost:44336/logout

## External authentication
LinkedIn external provider is used for authentication.
 
Go to the [LinkedIn Developer Portal](https://developer.linkedin.com/) and select My Apps from the top menu. Click on the Create Application button. You will need to complete all the information for your application, and once you are done click on the Submit button.
You will be taken to a page which displays the detail about your new application, including the Client ID and Client Secret.

In Visual Studio you may right click on the TechnicalTask project and select "Manage User Secrets" in context menu.

You should paste you own values between brackets in the secret.json file.

Example:
```json
"Authentication:LinkedIn:ClientID": <client_id>, 
"Authentication:LinkedIn:ClientSecret": <client_secret>.
```

Or execute the following commands in your project working directory to store the user secrets:
```powershell
dotnet user-secrets set Authentication:LinkedIn:ClientID <client_id>
dotnet user-secrets set Authentication:LinkedIn:ClientSecret <client-secret>
```

## Request examples
Json templates for API testing.

Tip: Arrays in objects are not required, but if you want to create/update included object(-s) you may initialize array of the current (each) object.

### /api/Business 
#### [POST]
```json
{
  "name": "string",
  "countryId": 0,
  "families": [
    {
      "name": "string",
      "businessId": 0,
      "offerings": [
        {
          "name": "string",
          "familyId": 0,
          "departments": [
            {
              "name": "string",
              "offeringId": 0
            }
          ]
        }
      ]
    }
  ]
}
```
### /api/Business/{id}
#### [PUT]
```json
{
  "id": 0,
  "name": "string",
  "countryId": 0,
  "families": [
    {
      "id": 0,
      "name": "string",
      "businessId": 0,
      "offerings": [
        {
          "id": 0,
          "name": "string",
          "familyId": 0,
          "departments": [
            {
              "id": 0,
              "name": "string",
              "offeringId": 0
            }
          ]
        }
      ]
    }
  ]
}
```
------------------------------------------------

### /api/Countries
#### [POST]
```json
{
  "organizationId": 0,
  "name": "string",
  "code": "string"
}
```
### /api/Countries/{id}
#### [PUT]
```json
{
  "organizationId": 0,
  "name": "string",
  "code": "string"
}
```
------------------------------------------------

### /api/Departments
#### [POST]
```json
{
  "name": "string",
  "offeringId": 0
}
```
### /api/Departments/{id}
#### [PUT]
```json
{
  "id": 0,
  "name": "string",
  "offeringId": 0
}
```
------------------------------------------------

### /api/Families
#### [POST]
```json
{
  "name": "string",
  "businessId": 0,
  "offerings": [
    {
      "name": "string",
      "familyId": 0,
      "departments": [
        {
          "name": "string",
          "offeringId": 0
        }
      ]
    }
  ]
}
```
### /api/Families/{id}
#### [PUT]
```json
{
  "id": 0,
  "name": "string",
  "businessId": 0,
  "offerings": [
    {
      "id": 0,
      "name": "string",
      "familyId": 0,
      "departments": [
        {
          "id": 0,
          "name": "string",
          "offeringId": 0
        }
      ]
    }
  ]
}
```
------------------------------------------------

### /api/Offerings
#### [POST]
```json
{
  "name": "string",
  "familyId": 0,
  "departments": [
    {
      "name": "string",
      "offeringId": 0
    }
  ]
}
```
### /api/Offerings/{id}
#### [PUT]
```json
{
  "id": 0,
  "name": "string",
  "familyId": 0,
  "departments": [
    {
      "id": 0,
      "name": "string",
      "offeringId": 0
    }
  ]
}
```
------------------------------------------------

### /api/Organizations
#### [POST]
```json
{
  "name": "string",
  "code": "string",
  "organizationType": 1,
  "owner": "string",
  "organizationCountries": [
    {
      "organizationId": 0,
      "countryId": 0,
      "country": {
        "name": "string",
        "code": "string",
        "businesses": [
          {
            "name": "string",
            "countryId": 0,
            "families": [
              {
                "name": "string",
                "businessId": 0,
                "offerings": [
                  {
                    "name": "string",
                    "familyId": 0,
                    "departments": [
                      {
                        "name": "string",
                        "offeringId": 0
                      }
                    ]
                  }
                ]
              }
            ]
          }
        ]
      }
    }
  ]
}
```
### /api/Organizations{id}
#### [PUT]
```json
{
  "id": 0,
  "name": "string",
  "code": "string",
  "organizationType": 1,
  "owner": "string",
  "organizationCountries": [
    {
      "id": 0,
      "organizationId": 0,
      "countryId": 0,
      "country": {
        "id": 0,
        "name": "string",
        "code": "string",
        "businesses": [
          {
            "id": 0,
            "name": "string",
            "countryId": 0,
            "families": [
              {
                "id": 0,
                "name": "string",
                "businessId": 0,
                "offerings": [
                  {
                    "id": 0,
                    "name": "string",
                    "familyId": 0,
                    "departments": [
                      {
                        "id": 0,
                        "name": "string",
                        "offeringId": 0
                      }
                    ]
                  }
                ]
              }
            ]
          }
        ]
      }
    }
  ]
}
```
------------------------------------------------

### /api/Users
#### [POST]
```json
{
  "name": "string",
  "surname": "string",
  "email": "string",
  "address": "string"
}
```
### /api/Users/{id}
#### [PUT]
```json
{
  "id": 0,
  "name": "string",
  "surname": "string",
  "email": "string",
  "address": "string"
}
```
------------------------------------------------

## Issues
+ In Swagger UI response model examples include collection, but real response is not.
+ Application cannot start without trusted sertificate. To install it use following link: https://blogs.msdn.microsoft.com/robert_mcmurray/2013/11/15/how-to-trust-the-iis-express-self-signed-certificate/