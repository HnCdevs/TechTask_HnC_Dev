Json templates for API testing.
Tip: Arrays in objects are not required, but if you want to create/update included object(-s) you may initialize array of the current (each) object.


/api/Business 
[POST]
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

[PUT]
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
------------------------------------------------

/api/Countries
[POST]
{
  "organizationId": 0,
  "name": "string",
  "code": "string"
}

[PUT]
{
  "organizationId": 0,
  "name": "string",
  "code": "string"
}
------------------------------------------------

/api/Departments
[POST]
{
  "name": "string",
  "offeringId": 0
}

[PUT]
{
  "id": 0,
  "name": "string",
  "offeringId": 0
}
------------------------------------------------

/api/Families
[POST]
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

[PUT]
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
------------------------------------------------

/api/Offerings
[POST]
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

[PUT]
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
------------------------------------------------

/api/Organizations
[POST]
{
  "name": "string",
  "code": "string",
  "organizationType": 1 (You can use numbers from 1 to 6),
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

[PUT]
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
------------------------------------------------

/api/Users
[POST]
{
  "name": "string",
  "surname": "string",
  "email": "string",
  "address": "string"
}

[PUT]
{
  "id": 0,
  "name": "string",
  "surname": "string",
  "email": "string",
  "address": "string"
}