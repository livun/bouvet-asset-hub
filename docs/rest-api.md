# REST API


| Resource | GET | POST | PUT | DELETE |
|-|-|-|-|-|
| /assets | Retrieve all assets | Create new asset | Update assets from list of id's | -
| /assets/1 | Retrieve asset by Id | Error | Update asset by Id | Delete asset by Id|
| /assets/6B29FC40... | Retrieve asset by Guid | - | - | - 
| /assets/1/loans | Retrieve all loans for asset by Id |- | - | - 
| /loans | Retrive all loans | Create new loan | - | - 
| /loans/1 | Retrieve loan by Id | - | Update loan by Id | Delete loan by Id |
| /employees/1/loans | Retrieve all loans for employee 1 | - | - | - 
| /loanhistory | Retrive all loanhistory | - | - | - 
| /categories | Retrive all categories | Create new category | - | - 
| /categories/1 | Retrive category by Id | - | Update category by Id | Delete category by Id
| /categories/1/assets | Retrieve assets by category | - | - | - 


